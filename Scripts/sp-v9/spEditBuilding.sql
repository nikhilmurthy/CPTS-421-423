DROP PROCEDURE IF EXISTS spEditBuilding;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spEditBuilding`
	(IN _abbr varchar(20), IN _isDelete tinyint(1), IN _newabbr varchar(20),
	  IN _newname varchar(75),
     OUT _rc int,
     OUT _errmsg text
     )
     
 
BEGIN

-- Diag info 

declare code char(5) default '00000';
declare msg text;

declare exit handler for sqlexception 
   BEGIN
	   set _rc = -1;
	   GET DIAGNOSTICS CONDITION 1
			code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT;
	   set _errmsg = CONCAT(code, "  ", msg); 
   END;

set _rc = -1;
set _errmsg = "";
if (_isDelete) then
   delete from building where abbr =  _abbr;
   set _rc = row_count();
else
    BEGIN
	set _newabbr = TRIM(_newabbr);
	set _newname = TRIM(_newname);
	if ((_newabbr = "") || (_newabbr NOT REGEXP '^[A-Z]+$')) then
		set _rc = -1;
        set _errmsg = "invalid abbr";
	elseif (_newname = "") then
		set _rc = -1;
		set _errmsg = "invalid name";
	else
		update building 
		set abbr = _newabbr, name = _newname 
		where abbr =  _abbr;
		set _rc = row_count();
	end if;
	END;
end if;
END $$
DELIMITER ;
