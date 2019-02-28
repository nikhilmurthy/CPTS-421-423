

DROP PROCEDURE IF EXISTS spSelLineItemOne;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelLineItemOne`(IN _order_id int)
BEGIN
select *
from lineitem
where order_id = _order_id  
order by line_no;
END$$
DELIMITER ;