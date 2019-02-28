
DROP PROCEDURE IF EXISTS spSelAllCategory;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllCategory`()
BEGIN
select name, catg_id
from category
order by name asc;
END$$
DELIMITER ;