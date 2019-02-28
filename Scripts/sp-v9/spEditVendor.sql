
DROP PROCEDURE IF EXISTS spEditVendor;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spEditVendor`
	(IN _name varchar(50), IN _isDelete tinyint(1), IN _newname varchar(50), IN _newaddress varchar(200), 
IN _newphone varchar(25), IN _newfax varchar(25), IN _newwebsite varchar(100),
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
   delete from vendor where name =  _name;
   set _rc = row_count();
else
    BEGIN
	set _newname = TRIM(_newname);
	set _newaddress = TRIM(_newaddress);
	set _newphone = TRIM(_newphone);
	set _newfax = TRIM(_newfax);
	set _newwebsite = TRIM(_newwebsite);
	if (_newname = "") then
		set _rc = -1;
        set _errmsg = "invalid name";
	elseif (_newaddress = "") then
		set _rc = -1;
		set _errmsg = "invalid address";
	elseif (_newphone = "") then
		set _rc = -1;
		set _errmsg = "invalid phone";
	elseif (_newfax = "") then
		set _rc = -1;
		set _errmsg = "invalid fax";
	elseif (_newwebsite = "") then
		set _rc = -1;
		set _errmsg = "invalid website";
	else
		update vendor 
		set name = _newname, address = _newaddress, phone = _newphone, fax = _newfax, website = _newwebsite
		where name =  _name;
		set _rc = row_count();
	end if;
	END;
end if;
END $$
DELIMITER ;