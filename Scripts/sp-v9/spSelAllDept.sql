
DROP PROCEDURE IF EXISTS spSelAllDept;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllDept`()
BEGIN
select abbr, name, dept_id
from dept
order by abbr asc;
END$$
DELIMITER ;