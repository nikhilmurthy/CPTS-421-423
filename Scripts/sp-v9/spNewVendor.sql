
DROP PROCEDURE IF EXISTS spNewVendor;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spNewVendor`
	(IN _name varchar(50), IN _address varchar(200), 
IN _phone varchar(25), IN _fax varchar(25), IN _website varchar(100),
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

set _name = TRIM(_name);
if (_name = "") then
	set _name = null;
elseif (_name NOT REGEXP '^[A-Z0-9& ]+$') then
	set _name = null;
end if;

set _address = TRIM(_address);
if (_address = "") then
	set _address = null;
end if;

set _phone = TRIM(_phone);
if (_phone = "") then
	set _phone = null;
end if;

set _fax = TRIM(_fax);
if (_fax = "") then
	set _fax = null;
end if;

set _website = TRIM(_website);
if (_website = "") then
	set _website = null;

end if;

 insert into 
vendor (name, address, phone, fax, website) 
values 
(_name, _address, _phone, _fax, _website);

set _rc  = last_insert_id();
set _errmsg = "";
END $$
DELIMITER ;