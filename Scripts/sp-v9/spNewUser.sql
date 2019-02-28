
DROP PROCEDURE IF EXISTS spNewUser;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spNewUser`
	(IN _user_id int, IN _email varchar(75), IN _first_name varchar(30)
, IN _last_name varchar(30), IN _phone varchar(25), IN _bldg_id smallint, IN _room_no varchar(10), IN _is_owner tinyint(1), IN _is_purchaser tinyint(1)
, IN _is_exp_authority tinyint(1), IN _is_faculty tinyint(1), IN _is_staff tinyint(1), IN _is_student tinyint(1), 
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

if (_user_id <= 0) then
	set _user_id = null;
end if;
set _email = TRIM(_email);
if (_email = "") then
	set _email = null;
end if;

set _first_name = TRIM(_first_name);
if (_first_name = "") then
	set _first_name = null;
elseif (_first_name NOT REGEXP '^[A-Z]+$') then
	set _first_name = null;
end if;

set _last_name = TRIM(_last_name);
if (_last_name = "") then
	set _last_name = null;
elseif (_last_name NOT REGEXP '^[A-Z]+$') then
	set _last_name = null;
end if;

set _phone = TRIM(_phone);
if (_phone = "") then
	set _phone = null;
end if;

 insert into 
user (user_id, email, first_name, last_name, phone, bldg_id, room_no, is_owner, is_purchaser, 
is_exp_authority, is_faculty, is_staff, is_student) 
values 
(_user_id, _email, _first_name, _last_name, _phone, _bldg_id, _room_no,  _is_owner, _is_purchaser, 
_is_exp_authority, _is_faculty, _is_staff, _is_student);

set _rc  = _user_id;
set _errmsg = "";
END $$
DELIMITER ;