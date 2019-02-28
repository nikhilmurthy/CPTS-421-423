
DROP PROCEDURE IF EXISTS spEditCategory;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spEditCategory`(IN _name varchar(75), IN _isDelete tinyint(1), IN _newname varchar(75), OUT _rc int, OUT _errmsg varchar(255))
BEGIN
-- Declare variables to hold diagnostics area information
declare code char(5) default '00000';
declare msg varchar(255);
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
   delete from category where name =  _name;
   set _rc = row_count();
else
    BEGIN
	set _newname = TRIM(_newname);
	if ((_newname = "") || (_newname NOT REGEXP '^[A-Z0-9& ]+$')) then
		set _rc = -1;
        set _errmsg = "invalid name";
	else
		update category set name = _newname where name =  _name;
		set _rc = row_count();
	end if;
	END;
end if;
END $$
DELIMITER ;