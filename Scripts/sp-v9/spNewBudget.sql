use imdb;

DROP PROCEDURE IF EXISTS spNewBudget;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spNewBudget`
	(IN _budget_no int, 
     IN _description varchar(75),
     OUT _rc int,
     OUT _errmsg text
     )
     
 
BEGIN

-- Diag info 

declare code char(5) default '00000';
declare msg text;

declare exit handler for sqlexception 
   BEGIN
	   set _rc = -1;
	   GET DIAGNOSTICS CONDITION 1
			code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT;
	   set _errmsg = CONCAT(code, "  ", msg); 
   END;
   
if (_budget_no <= 0) then
	set _budget_no = null;
end if;
set _description = TRIM(_description);
if (_description = "") then
	set _description = null;
elseif (_description NOT REGEXP '^[A-Z0-9& ]+$') then
	set _description = null;
end if;

 insert into budget (budget_no, description) values (_budget_no, _description);
 set _rc  = _budget_no;
 set _errmsg = "";
END $$
DELIMITER ;