use imdb;
DROP PROCEDURE IF EXISTS spSelAllLogin;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllLogin`()
BEGIN
select login_id, login_name, is_admin, first_name, last_name, phone, email, creation_date
from admin
order by login_name asc;
END$$
DELIMITER ;