
DROP PROCEDURE IF EXISTS spNewCategory;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spNewCategory`
	(IN _name varchar(40),
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
elseif (_name NOT REGEXP '^[A-Z& ]+$') then
	set _name = null;
end if;

insert into category (name) values (_name);
set _rc  = last_insert_id();
set _errmsg = "";
END $$
DELIMITER ;