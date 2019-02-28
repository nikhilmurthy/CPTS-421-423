
 drop procedure if exists spNewInventory;
 
 DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spNewInventory`
	(
    IN _inv_id int,
	IN _description varchar(75),
	IN _catg_id smallint,
#	IN _inv_status tinyint,
	IN _bldg_id smallint,
	IN _room_no varchar(10),
	IN _owner_id smallint,
	IN _order_no int,
	IN _line_no smallint,
	IN _vendor_id smallint,
	IN _dept_id smallint,
	IN _delivery_date date,
	IN _purchase_date date,
	IN _purchase_price decimal(9,2),
	IN _manf varchar(45),
	IN _trans_no varchar(45),
	IN _serial_no varchar(45),
	IN _model_no varchar(45),
	IN _comment varchar(150),
#	IN _bt tinyint,
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

 
insert inventory (inv_id, description, catg_id, inv_status, bldg_id, room_no, owner_id, order_id, line_no, vendor_id, dept_id, delivery_date, purchase_date, purchase_price, manf, trans_no, serial_no, model_no, comment, bt) 
VALUES
(_inv_id, _description, _catg_id, 1, _bldg_id, _room_no, _owner_id, _order_no, _line_no, _vendor_id, _dept_id, _delivery_date, _purchase_date, _purchase_price, _manf, _trans_no, _serial_no, _model_no, _comment, 1);

update orders set total_qty_recv = total_qty_recv + 1 
       where order_id = _order_no;
update orders set is_delivered = true   
       where order_id = _order_no and total_qty = total_qty_recv;
update lineitem set qty_recv = qty_recv + 1 
       where order_id = _order_no and line_no = _line_no;
update lineitem set is_delivered = true 
       where order_id = _order_no and line_no = _line_no and qty = qty_recv;
set _rc = _inv_id;
set _errmsg = "";
END $$

DELIMITER ;
