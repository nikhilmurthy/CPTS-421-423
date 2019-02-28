
DROP PROCEDURE IF EXISTS spSelInvOne;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelInvOne`(IN _inv_id int)
BEGIN
select *
from inventory
where inv_id = _inv_id and inv_status != 0;
END$$
DELIMITER ;