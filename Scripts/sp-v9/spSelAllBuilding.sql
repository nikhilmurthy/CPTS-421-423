
DROP PROCEDURE IF EXISTS spSelAllBuilding;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllBuilding`()
BEGIN
select abbr, name, bldg_id
from building
order by abbr asc;
END$$
DELIMITER ;