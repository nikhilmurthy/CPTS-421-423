

DROP PROCEDURE IF EXISTS spSelAllPurchaser;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllPurchaser`()
BEGIN
select email, first_name, last_name, phone, user_id
from user
where is_purchaser = 1
order by first_name asc;
END$$
DELIMITER ;