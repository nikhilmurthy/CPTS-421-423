
DROP PROCEDURE IF EXISTS spSelAllBudget;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllBudget`()
BEGIN
select budget_no, description
from budget
order by budget_no asc;
END$$
DELIMITER ;