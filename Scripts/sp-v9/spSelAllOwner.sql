
DROP PROCEDURE IF EXISTS spSelAllOwner;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllOwner`()
BEGIN
select email, first_name, last_name, phone, user_id, user.bldg_id as bldg_id, room_no, abbr
from user, building
where is_owner = 1 and user.bldg_id = building.bldg_id
order by first_name asc;
END$$
DELIMITER ;