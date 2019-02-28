
DROP PROCEDURE IF EXISTS spSelAllFaculty;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllFaculty`()
BEGIN
select email, first_name, last_name, phone, user_id
from user
where is_faculty = 1
order by first_name asc;
END$$
DELIMITER ;