
DROP PROCEDURE IF EXISTS spSelOrdersOne;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelOrdersOne`(IN _order_id int)
BEGIN
select *
from orders
where order_id = _order_id and order_status != 0
order by order_id desc;
END$$
DELIMITER ;