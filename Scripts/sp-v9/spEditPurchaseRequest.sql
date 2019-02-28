
 drop procedure if exists spEditPurchaseRequest;
 
 DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spEditPurchaseRequest`
	(
    IN _order_id int,
	IN _isDelete tinyint(1),
	IN _owner_id smallint,
	IN _purchaser_id smallint,
	IN _sign_faculty_id smallint,
	IN _sign_auth_id smallint,
	IN _vendor_id smallint,
	IN _dept_id smallint,
	IN _budget_no int,
	IN _comment varchar(150),
	IN _ship_mode int,
	IN _payment_type int,
     OUT _rc int,
     OUT _errmsg text
     )
     
 
BEGIN

-- Diag info 
declare code char(5) default '00000';
declare msg text;

declare exit handler for sqlexception 
   BEGIN
	   ROLLBACK;
	   set _rc = -1;
	   GET DIAGNOSTICS CONDITION 1
			code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT;
	   set _errmsg = CONCAT(code, "  ", msg); 
   END;

 
set _rc = -1;
set _errmsg = "";
if (_isDelete) then
	update orders set order_status = 0, comment = _comment where order_id = _order_id;
   set _rc = row_count();
else
    BEGIN
	set _comment = TRIM(_comment);
	if (_comment = "") then
		set _rc = -1;
        set _errmsg = "invalid comment";
	else
		update orders 
		set owner_id = _owner_id, 
		sign_faculty_id = _sign_faculty_id, sign_auth_id = _sign_auth_id,
		purchaser_id = _purchaser_id,
		vendor_id = _vendor_id, dept_id = _dept_id,
		comment = _comment, ship_mode = _ship_mode, payment_type = _payment_type
		where order_id =  _order_id;
		set _rc = row_count();
	end if;
	END;
end if;
END $$

DELIMITER ;
