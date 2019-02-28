
DROP PROCEDURE IF EXISTS spNewPurchaseRequest;
 
 DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spNewPurchaseRequest`
	( 
	IN _order_id int,
 #    IN _description varchar(200),
     IN _date date,
     IN _vendor_id smallint,
     IN _ship_mode tinyint,
 #  IN _order_status tinyint,
     IN _budget_no int,
     IN _purchaser_id smallint,
     IN _owner_id smallint,
     IN _dept_id smallint,
     IN _payment_type tinyint,
     IN _sign_faculty_id smallint,
     IN _sign_auth_id smallint,
     IN _total_amt decimal,
     IN _count tinyint,
  # IN _is_delivered tinyint,
	IN _comment varchar(250),
     
     IN _ca_list varchar(500), 
     IN _desc_list varchar(1000),
     IN _qty_list varchar (100),
     IN _unit_list varchar (100),
	 IN _up_list varchar(200),
	 IN _amt_list varchar(200),
     OUT _rc int,
     OUT _errmsg text
     )
     

 
BEGIN

-- Declare variables to hold diagnostics area information
declare code char(5) default '00000';
declare msg text;


declare pos int;
declare lc tinyint;
declare item varchar(100);
declare amt decimal(9,2);

declare _ca varchar(100);
declare _desc varchar(100);
declare _qty int;
declare _unit varchar(10);
declare _up decimal (9,2);
declare _amt decimal (9,2);

declare _total_qty int;

declare exit handler for sqlexception 
   BEGIN
	   ROLLBACK;
	   set _rc = -1;
	   GET DIAGNOSTICS CONDITION 1
			code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT;
	   set _errmsg = CONCAT(code, "  ", msg); 
   END;

/* Insert the orders record */



START transaction;

INSERT INTO orders
        (order_id, date, vendor_id, ship_mode, 
         order_status, budget_no, purchaser_id, owner_id, 
         dept_id, payment_type, sign_faculty_id, sign_auth_id, 
         total_amt, lineitem_count,is_delivered, comment, bt)
		VALUES (_order_id, _date, _vendor_id, _ship_mode, 1, _budget_no, _purchaser_id, _owner_id,
				_dept_id, _payment_type, _sign_faculty_id, _sign_auth_id, _total_amt, _count, 0, _comment, true);

if right(_ca_list, 1) <> '|' then set  _ca_list= concat(_ca_list, '|'); 
end if;
if right(_desc_list, 1) <> '|' then set  _desc_list= concat(_desc_list, '|'); 
end if;
if right(_qty_list, 1) <> '|' then set  _qty_list= concat(_qty_list, '|'); 
end if;
if right(_unit_list, 1) <> '|' then set  _unit_list= concat(_unit_list, '|'); 
end if;
if right(_up_list, 1) <> '|' then set  _up_list= concat(_up_list, '|'); 
end if;
if right(_amt_list, 1) <> '|' then set  _ca_list= concat(_amt_list, '|'); 
end if;
 

set lc = 0;
set _total_qty = 0;

LABEL1: 

while   (length(_ca_list) > 1)  
		and (length(_desc_list) > 1) 
		and (length(_qty_list) > 1) 
		and (length(_up_list) > 1)
		and (length(_amt_list) > 1) do

	set lc = lc + 1;
    
	set pos = INSTR(_ca_list, '|');
	set _ca = LEFT(_ca_list, pos - 1);
	set _ca_list = substring(_ca_list, pos + 1);

	set pos = INSTR(_desc_list, '|');
	set _desc = LEFT(_desc_list, pos - 1);
	set _desc_list = substring(_desc_list, pos + 1);
    
	set pos = INSTR(_qty_list, '|');
	set item = LEFT(_qty_list, pos - 1);
    set _qty = CAST(item as unsigned Integer);
    set _total_qty = _total_qty + _qty;
	set _qty_list = substring(_qty_list, pos + 1);
    
	set pos = INSTR(_unit_list, '|');
	set _unit = LEFT(_unit_list, pos - 1);
	set _unit_list = substring(_unit_list, pos + 1);
    
	set pos = INSTR(_up_list, '|');
	set item = LEFT (_up_list, pos - 1);
 	set _up = CAST(item as DECIMAL(9,2));
	set _up_list = substring(_up_list, pos + 1);
    
	set pos = INSTR(_amt_list, '|');
	set item = LEFT (_amt_list, pos - 1);
 	set _amt = CAST(item as DECIMAL(9,2));
	set _amt_list = substring(_amt_list, pos + 1);
    
    INSERT INTO lineitem (order_id, line_no, catelog_no, description, qty, unit, unit_price, total_amt, is_delivered)
		VALUES (_order_id, lc, _ca, _desc, _qty, _unit,  _up, _amt, 0);
	
	if lc = _count then
		leave LABEL1;
	end if;
    
end while;

if (lc <> _count) then 
	rollback; 
	set _rc = -1;
	set _errmsg = "Line item count does not match";
else
    update orders set total_qty = _total_qty
		where order_id = _order_id;
	set _rc = _order_id;
	set _errmsg = "";
	COMMIT;

end if;

END $$

DELIMITER ;