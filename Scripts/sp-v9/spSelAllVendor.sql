
DROP PROCEDURE IF EXISTS spSelAllVendor;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllVendor`()
BEGIN
select name, address, phone, fax, website, vendor_id
from vendor
order by name asc;
END$$
DELIMITER ;