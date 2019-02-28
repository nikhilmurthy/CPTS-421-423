
DROP PROCEDURE IF EXISTS spSelAllUser;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllUser`()
BEGIN
select email, first_name, last_name, phone, bldg_id, room_no, user_id, is_owner, is_purchaser, is_exp_authority, is_faculty, is_staff, is_student
from user
order by user_id asc;
END$$
DELIMITER ;