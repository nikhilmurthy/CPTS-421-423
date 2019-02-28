
DROP PROCEDURE IF EXISTS spSelInvReport;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelInvReport`(IN _min_ownerID int, IN _max_ownerID int, 
IN _min_bldgID int, IN _max_bldgID int, IN _min_catgID int, IN _max_catgID int, IN _min_orderno int, 
IN _max_orderno int, IN _min_invID int, IN _max_invID int, IN _sDate datetime, IN _eDate datetime)
BEGIN
select inv_id, first_name, last_name, abbr, inventory.room_no as room_no, category.name, 
	orders.order_id, serial_no, inventory.description, delivery_date
from inventory, user, building, category, orders
where (user_id <= _max_ownerID and user_id >= _min_ownerID) 
and (building.bldg_id <= _max_bldgID and building.bldg_id >= _min_bldgID)
and (category.catg_id <= _max_catgID and category.catg_id >= _min_catgID)
and (orders.order_id <= _max_orderno and orders.order_id >= _min_orderno)
and (inv_id <= _max_invID and inv_id >= _min_invID)
and (inventory.owner_id = user.user_id)
and (inventory.bldg_id = building.bldg_id)
and (inventory.catg_id = category.catg_id)
and (inventory.order_id = orders.order_id)
and (inventory.delivery_date <= _eDate and inventory.delivery_date >= _sDate)
and (inventory.inv_status > 0)
order by inv_id desc
LIMIT 1000;
END$$
DELIMITER ;