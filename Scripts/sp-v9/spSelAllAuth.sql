
DROP PROCEDURE IF EXISTS spSelAllAuth;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllAuth`()
BEGIN
select email, first_name, last_name, phone, user_id
from user
where is_exp_authority = 1
order by first_name asc;
END$$
DELIMITER ;