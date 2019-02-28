

DROP PROCEDURE IF EXISTS spChangePW;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spChangePW`
	(IN _login_name varchar(30), 
     IN _old_passwrd varchar(30),
     IN _new_passwrd varchar(30),
     IN _repeat_passwrd varchar(30),
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
   
-- Check if input username and password exists in login table

set _new_passwrd = TRIM(_new_passwrd);
set _repeat_passwrd = TRIM(_repeat_passwrd);

# Invalid password
set len = length(_login_name);
if ((len < 4) || (len > 30) || (_new_passwrd NOT REGEXP '^[A-Z0-9&]+$')) then
	 set _rc  = -1;
	 set _errmsg = "Invalid New Password";
     LEAVE MAIN;
end if;

# Password and Confirmation Passwords don't match

if (_new_passwrd != _repeat_passwrd) then
	 set _rc  = -1;
	 set _errmsg = "Passwords Don't Match";
     LEAVE MAIN;
end if;

# Update password

update admin
set passwrd = SHA2(_new_passwrd, 224)
where login_name = _login_name and passwrd = SHA2(_old_passwrd, 224);
set _rc = row_count();

if (_rc = 0) then
	set _rc = -1;
	set _errmsg = "Invalid Old Password";
    LEAVE MAIN;
end if;
    
set _errmsg = "";

END $$
DELIMITER ;
