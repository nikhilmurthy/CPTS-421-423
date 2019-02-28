

DROP PROCEDURE IF EXISTS spNewBuilding;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spNewBuilding`
	(IN _abbr varchar(20), IN _name varchar(75),
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

set _abbr = TRIM(_abbr);
if (_abbr = "") then
	set _abbr = null;
elseif (_abbr NOT REGEXP '^[A-Z]+$') then
	set _abbr = null;
end if;
set _name = TRIM(_name);
if (_name = "") then
	set _name = null;
end if;

insert into building (abbr, name) values (_abbr, _name);
set _rc  = last_insert_id();
set _errmsg = "";
END $$
DELIMITER ;