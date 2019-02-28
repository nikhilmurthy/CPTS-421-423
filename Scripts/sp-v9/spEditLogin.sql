
DROP PROCEDURE IF EXISTS spEditLogin;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spEditLogin`(
IN _login_name varchar(30),
IN _first_name varchar(20), 
IN _last_name varchar(20),
IN _passwrd varchar(30), /* if empty - no change to password */
IN _email varchar(75),
IN _phone varchar(75),
IN _is_admin tinyint(1), 
IN _is_delete tinyint(1), 
OUT _rc int,
OUT _errmsg text)
MAIN:
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

set _rc := 
(
	select is_admin
    from admin
    where login_name = _login_name
);

if (_rc = null) then
	set _rc = -1;
    set _errmsg = "Login Name not found!";
    LEAVE MAIN;
end if;

if (_is_delete) then
   delete from admin where login_name =  _login_name;
   set _rc = row_count();
else
    BEGIN
	set @len = length(_passwrd);
	set _passwrd = TRIM(_passwrd);
    set _first_name = TRIM(_first_name);
	set _last_name = TRIM(_last_name);
	set _phone = TRIM(_phone);
	set _email = TRIM(_email);

	if ((_first_name = "") || (_first_name NOT REGEXP BINARY '^[A-Z][a-zA-Z]+$')) then
			set _rc = -1;
            set _errmsg = "Invalid First Name";
            LEAVE MAIN;
	end if;
	if ((_last_name = "") || (_last_name NOT REGEXP BINARY '^[A-Z][a-zA-Z]+$')) then
			set _rc = -1;
            set _errmsg = "Invalid Last Name";
            LEAVE MAIN;
	end if;
	if ((_email = "") || (_email NOT REGEXP '^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$')) then
			set _rc = -1;
            set _errmsg = "Invalid Email";
            LEAVE MAIN;
	end if;
    if (_passwrd = "") then
		update admin 
			set is_admin = _is_admin, first_name = _first_name, last_name = _last_name,
				phone = _phone, email = _email
		where login_name =  _login_name;
		set _rc = row_count();
	else if ((@len < 4) || (@len > 30) || (_passwrd NOT REGEXP '^[A-Z0-9&]+$')) then
			 set _rc  = -1;
			 set _errmsg = "Invalid Password";
			 LEAVE MAIN;
		else
			update admin 
			set passwrd = SHA2(_passwrd, 224),
			is_admin = _is_admin, first_name = _first_name, last_name = _last_name,
			phone = _phone, email = _email
			where login_name =  _login_name;
			set _rc = row_count();
		end if;
	end if;
	END;
end if;

END$$
DELIMITER ;