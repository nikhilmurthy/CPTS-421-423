
DROP PROCEDURE IF EXISTS spEditBudget;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spEditBudget`(IN _budget_no int, IN _isDelete tinyint(1), IN _description varchar(75), OUT _rc int, OUT _errmsg varchar(255))
BEGIN
-- Declare variables to hold diagnostics area information
declare code char(5) default '00000';
declare msg varchar(255);
declare exit handler for sqlexception 
   BEGIN
	   set _rc = -1;
	   GET DIAGNOSTICS CONDITION 1
			code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT;
	   set _errmsg = CONCAT(code, "  ", msg); 
   END;

set _rc = -1;
set _errmsg = "";
if (_isDelete) then
   delete from budget where budget_no =  _budget_no;
   set _rc = row_count();
else
    BEGIN
	set _description = TRIM(_description);
	if ((_description = "") || (_description NOT REGEXP '^[A-Z0-9& ]+$')) then
		set _rc = -1;
        set _errmsg = "invalid description";
	else
		update budget set description = _description where budget_no =  _budget_no;
		set _rc = row_count();
	end if;
	END;
end if;
END $$
DELIMITER ;
