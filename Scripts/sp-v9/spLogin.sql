
use imdb;
DROP PROCEDURE IF EXISTS spLogin;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spLogin`
	(IN _login_name varchar(30), 
     IN _passwrd varchar(30),
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

# Set _rc to isAdmin 

set _rc := (
select is_admin 
from admin 
where  login_name = _login_name and binary passwrd = SHA2(_passwrd,224));


# No record found (ie login failed)

if (_rc = null) then
	set _rc = -1;
	set _errmsg = "Login Failed!";
	LEAVE MAIN;
end if;

set _errmsg = "";

END $$
DELIMITER ;