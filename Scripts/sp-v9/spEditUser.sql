
DROP PROCEDURE IF EXISTS spEditUser;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spEditUser`
	(IN _user_id int, IN _isDelete tinyint(1), IN _first_name varchar(30)
, IN _last_name varchar(30), IN _phone varchar(25), IN _email varchar(75), IN _bldg_id smallint, IN _room_no varchar(10), IN _is_owner tinyint(1), IN _is_purchaser tinyint(1)
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

set _rc = -1;
set _errmsg = "";
if (_isDelete) then
   delete from user where user_id =  _user_id;
   set _rc = row_count();
else
    BEGIN
	set _email = TRIM(_email);
	set _first_name = TRIM(_first_name);
	set _last_name = TRIM(_last_name);
	set _phone = TRIM(_phone);
	if (_email = "") then
		set _rc = -1;
        set _errmsg = "invalid email";
	elseif ((_first_name = "") || (_first_name NOT REGEXP '^[A-Z0]+$')) then
		set _rc = -1;
        set _errmsg = "invalid first name";
	elseif ((_last_name = "") || (_last_name NOT REGEXP '^[A-Z0]+$')) then
		set _rc = -1;
        set _errmsg = "invalid last name";
	elseif (_phone = "") then
		set _rc = -1;
        set _errmsg = "invalid phone";
	else
		update user 
		set email = _email, first_name = _first_name, last_name = _last_name, phone = _phone,
        bldg_id = _bldg_id, room_no = _room_no,
		is_owner = _is_owner, is_purchaser = _is_purchaser, is_exp_authority = _is_exp_authority,
		is_faculty = _is_faculty, is_staff = _is_staff, is_student = _is_student
		where user_id =  _user_id;
		set _rc = row_count();
	end if;
	END;
end if;
END $$
DELIMITER ;