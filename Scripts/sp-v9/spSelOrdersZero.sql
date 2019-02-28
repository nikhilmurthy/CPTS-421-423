
DROP PROCEDURE IF EXISTS spSelOrdersZero;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelOrdersZero`()
BEGIN
select order_id
from orders
where is_delivered = 0
order by order_id desc;
END$$
DELIMITER ;