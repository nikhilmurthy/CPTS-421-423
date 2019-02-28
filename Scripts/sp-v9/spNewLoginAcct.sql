
DROP PROCEDURE if exists spNewLoginAcct;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spNewLoginAcct`(IN _login_name varchar(30), 
     IN _passwrd varchar(30),
	 IN _repeat_passwrd varchar(30),
     IN _is_admin tinyint,
     IN _first_name varchar(30),
     IN _last_name varchar(30),
     IN _email varchar(75),
     IN _phone varchar(20),
     OUT _rc int,
     OUT _errmsg text
     )
MAIN: 
BEGIN

-- Diag info 

declare code char(5) default '00000';
declare msg text;

declare len int;

declare exit handler for sqlexception 
   BEGIN
	   set _rc = -1;
	   GET DIAGNOSTICS CONDITION 1
			code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT;
	   set _errmsg = CONCAT(code, "  ", msg); 
   END;

set len = length(_login_name);
if ((len < 4) || (len > 30) || (_login_name NOT REGEXP '^[A-Z0-9]+$')) then
	 set _rc  = -1;
	 set _errmsg = "Invalid Login Name";
     LEAVE MAIN;
end if;
set len = length(_passwrd);
set _passwrd = TRIM(_passwrd);
if ((len < 4) || (len > 30) || (_passwrd NOT REGEXP '^[A-Z0-9&]+$')) then
	 set _rc  = -1;
	 set _errmsg = "Invalid Password";
     LEAVE MAIN;
end if;

set _repeat_passwrd = TRIM(_repeat_passwrd);
if (_passwrd != _repeat_passwrd) then
	 set _rc  = -1;
	 set _errmsg = "Password Dont Match";
     LEAVE MAIN;
end if;

set len = length(_first_name);
set _first_name = TRIM(_first_name);
if ((len < 4) || (len > 30) || (_first_name NOT REGEXP BINARY '^[A-Z]+[a-zA-Z]+$')) then
	set _rc = -1;
	set _errmsg = "Invalid First Name";
    LEAVE MAIN;
end if;

set len = length(_last_name);
set _last_name = TRIM(_last_name);
if ((len < 4) || (len > 30) || (_last_name NOT REGEXP BINARY '^[A-Z]+[a-zA-Z]+$')) then
	set _rc = -1;
	set _errmsg = "Invalid Last Name";
    LEAVE MAIN;
end if;

set len = length(_email);
set _email = TRIM(_email);
if ((len < 4) || (len > 30) || (_email NOT REGEXP '^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$')) then
	set _rc = -1;
	set _errmsg = "Invalid Email";
    LEAVE MAIN;
end if;

 insert into admin(login_name, passwrd, is_admin, first_name, last_name, phone, email) 
			values (_login_name, sha2(_passwrd, 224), _is_admin, _first_name, _last_name, _phone, _email);
 set _rc  = last_insert_id();
 set _errmsg = "";
END$$
DELIMITER ;
