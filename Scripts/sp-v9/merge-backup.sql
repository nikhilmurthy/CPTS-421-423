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

DROP PROCEDURE IF EXISTS spNewBuilding;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spNewBuilding`
	(IN _abbr varchar(20), IN _name varchar(75),
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

set _abbr = TRIM(_abbr);
if (_abbr = "") then
	set _abbr = null;
elseif (_abbr NOT REGEXP '^[A-Z]+$') then
	set _abbr = null;
end if;
set _name = TRIM(_name);
if (_name = "") then
	set _name = null;
end if;

insert into building (abbr, name) values (_abbr, _name);
set _rc  = last_insert_id();
set _errmsg = "";
END $$
DELIMITER ;
DROP PROCEDURE IF EXISTS spNewCategory;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spNewCategory`
	(IN _name varchar(40),
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

set _name = TRIM(_name);
if (_name = "") then
	set _name = null;
elseif (_name NOT REGEXP '^[A-Z& ]+$') then
	set _name = null;
end if;

insert into category (name) values (_name);
set _rc  = last_insert_id();
set _errmsg = "";
END $$
DELIMITER ;
DROP PROCEDURE IF EXISTS spNewDept;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spNewDept`
	(IN _abbr varchar(25), IN _name varchar(75),
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

set _abbr = TRIM(_abbr);
if (_abbr = "") then
	set _abbr = null;
elseif (_abbr NOT REGEXP '^[A-Z0-9& ]+$') then
	set _abbr = null;
end if;
set _name = TRIM(_name);
if (_name = "") then
	set _name = null;
end if;

insert into dept (abbr, name) values (_abbr, _name);
set _rc  = last_insert_id();
set _errmsg = "";
END $$
DELIMITER ;
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
DROP PROCEDURE IF EXISTS spNewUser;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spNewUser`
	(IN _user_id int, IN _email varchar(75), IN _first_name varchar(30)
, IN _last_name varchar(30), IN _phone varchar(25), IN _is_owner tinyint(1), IN _is_purchaser tinyint(1)
, IN _is_exp_authority tinyint(1), IN _is_faculty tinyint(1), IN _is_staff tinyint(1), IN _is_student tinyint(1),
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

if (_user_id <= 0) then
	set _user_id = null;
end if;
set _email = TRIM(_email);
if (_email = "") then
	set _email = null;
end if;

set _first_name = TRIM(_first_name);
if (_first_name = "") then
	set _first_name = null;
elseif (_first_name NOT REGEXP '^[A-Z]+$') then
	set _first_name = null;
end if;

set _last_name = TRIM(_last_name);
if (_last_name = "") then
	set _last_name = null;
elseif (_last_name NOT REGEXP '^[A-Z]+$') then
	set _last_name = null;
end if;

set _phone = TRIM(_phone);
if (_phone = "") then
	set _phone = null;
end if;

 insert into 
user (user_id, email, first_name, last_name, phone, is_owner, is_purchaser, 
is_exp_authority, is_faculty, is_staff, is_student) 
values 
(_user_id, _email, _first_name, _last_name, _phone, _is_owner, _is_purchaser, 
_is_exp_authority, _is_faculty, _is_staff, _is_student);

set _rc  = _user_id;
set _errmsg = "";
END $$
DELIMITER ;
DROP PROCEDURE IF EXISTS spNewVendor;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spNewVendor`
	(IN _name varchar(50), IN _address varchar(200), 
IN _phone varchar(25), IN _fax varchar(25), IN _website varchar(100),
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

set _name = TRIM(_name);
if (_name = "") then
	set _name = null;
elseif (_name NOT REGEXP '^[A-Z0-9& ]+$') then
	set _name = null;
end if;

set _address = TRIM(_address);
if (_address = "") then
	set _address = null;
end if;

set _phone = TRIM(_phone);
if (_phone = "") then
	set _phone = null;
end if;

set _fax = TRIM(_fax);
if (_fax = "") then
	set _fax = null;
end if;

set _website = TRIM(_website);
if (_website = "") then
	set _website = null;

end if;

 insert into 
vendor (name, address, phone, fax, website) 
values 
(_name, _address, _phone, _fax, _website);

set _rc  = last_insert_id();
set _errmsg = "";
END $$
DELIMITER ;
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
DROP PROCEDURE IF EXISTS spEditBuilding;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spEditBuilding`
	(IN _abbr varchar(20), IN _isDelete tinyint(1), IN _newabbr varchar(20),
	  IN _newname varchar(75),
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

set _rc = -1;
set _errmsg = "";
if (_isDelete) then
   delete from building where abbr =  _abbr;
   set _rc = row_count();
else
    BEGIN
	set _newabbr = TRIM(_newabbr);
	set _newname = TRIM(_newname);
	if ((_newabbr = "") || (_newabbr NOT REGEXP '^[A-Z]+$')) then
		set _rc = -1;
        set _errmsg = "invalid abbr";
	elseif (_newname = "") then
		set _rc = -1;
		set _errmsg = "invalid name";
	else
		update building 
		set abbr = _newabbr, name = _newname 
		where abbr =  _abbr;
		set _rc = row_count();
	end if;
	END;
end if;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS spEditCategory;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spEditCategory`(IN _name varchar(75), IN _isDelete tinyint(1), IN _newname varchar(75), OUT _rc int, OUT _errmsg varchar(255))
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
   delete from category where name =  _name;
   set _rc = row_count();
else
    BEGIN
	set _newname = TRIM(_newname);
	if ((_newname = "") || (_newname NOT REGEXP '^[A-Z0-9& ]+$')) then
		set _rc = -1;
        set _errmsg = "invalid name";
	else
		update category set name = _newname where name =  _name;
		set _rc = row_count();
	end if;
	END;
end if;
END $$
DELIMITER ;
DROP PROCEDURE IF EXISTS spEditDept;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spEditDept`
	(IN _abbr varchar(20), IN _isDelete tinyint(1), IN _newabbr varchar(20),
	  IN _newname varchar(75),
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

set _rc = -1;
set _errmsg = "";
if (_isDelete) then
   delete from dept where abbr =  _abbr;
   set _rc = row_count();
else
    BEGIN
	set _newabbr = TRIM(_newabbr);
	set _newname = TRIM(_newname);
	if ((_newabbr = "") || (_newabbr NOT REGEXP '^[A-Z0-9& ]+$')) then
		set _rc = -1;
        set _errmsg = "invalid abbr";
	elseif (_newname = "") then
		set _rc = -1;
		set _errmsg = "invalid name";
	else
		update dept 
		set abbr = _newabbr, name = _newname 
		where abbr =  _abbr;
		set _rc = row_count();
	end if;
	END;
end if;
END $$
DELIMITER ;
 drop procedure if exists spEditInventory;
 
 DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spEditInventory`
	(
    IN _inv_id int,
	IN _isDelete tinyint(1),
	IN _description varchar(75),
	IN _catg_id smallint,
#	IN _inv_status tinyint,
	IN _bldg_id smallint,
	IN _room_no varchar(10),
	IN _owner_id smallint,
	IN _vendor_id smallint,
	IN _dept_id smallint,
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

 
set _rc = -1;
set _errmsg = "";
if (_isDelete) then
	update inventory set inv_status = 0, comment = _comment where inv_id = _inv_id;
   set _rc = row_count();
else
    BEGIN
	set _room_no = TRIM(_room_no);
	set _manf = TRIM(_manf);
	set _trans_no = TRIM(_trans_no);
	set _serial_no = TRIM(_serial_no);
	set _model_no = TRIM(_model_no);
	set _comment = TRIM(_comment);
	if (_room_no = "") then
		set _rc = -1;
        set _errmsg = "invalid room number";
	elseif (_manf = "") then
		set _rc = -1;
        set _errmsg = "invalid manufacturer";
	elseif (_trans_no = "") then
		set _rc = -1;
        set _errmsg = "invalid transaction number";
	elseif (_serial_no = "") then
		set _rc = -1;
        set _errmsg = "invalid serial number";
	elseif (_model_no = "") then
		set _rc = -1;
        set _errmsg = "invalid model number";
	elseif (_comment = "") then
		set _rc = -1;
        set _errmsg = "invalid comment";
	else
		update inventory 
		set description = _description, catg_id = _catg_id, bldg_id = _bldg_id, room_no = _room_no, owner_id = _owner_id,
		vendor_id = _vendor_id, dept_id = _dept_id, purchase_date = _purchase_date,
		purchase_price = _purchase_price, 
		manf = _manf, trans_no = _trans_no, serial_no = _serial_no, model_no = _model_no, comment = _comment
		where inv_id =  _inv_id;
		set _rc = row_count();
	end if;
	END;
end if;
END $$

DELIMITER ;

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

DROP PROCEDURE IF EXISTS spEditUser;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spEditUser`
	(IN _user_id int, IN _isDelete tinyint(1), IN _email varchar(75), IN _first_name varchar(30)
, IN _last_name varchar(30), IN _phone varchar(25), IN _is_owner tinyint(1), IN _is_purchaser tinyint(1)
, IN _is_exp_authority tinyint(1), IN _is_faculty tinyint(1), IN _is_staff tinyint(1), IN _is_student tinyint(1),
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

set _rc = -1;
set _errmsg = "";
if (_isDelete) then
   delete from user where user_id =  _user_id;
   set _rc = row_count();
else
    BEGIN
	set _email = TRIM(_email);
	set _first_name = TRIM(_first_name);
	set _last_name = TRIM(_last_name);
	set _phone = TRIM(_phone);
	if (_email = "") then
		set _rc = -1;
        set _errmsg = "invalid email";
	elseif ((_first_name = "") || (_first_name NOT REGEXP '^[A-Z0]+$')) then
		set _rc = -1;
        set _errmsg = "invalid first name";
	elseif ((_last_name = "") || (_last_name NOT REGEXP '^[A-Z0]+$')) then
		set _rc = -1;
        set _errmsg = "invalid last name";
	elseif (_phone = "") then
		set _rc = -1;
        set _errmsg = "invalid phone";
	else
		update user 
		set email = _email, first_name = _first_name, last_name = _last_name, phone = _phone,
		is_owner = _is_owner, is_purchaser = _is_purchaser, is_exp_authority = _is_exp_authority,
		is_faculty = _is_faculty, is_staff = _is_staff, is_student = _is_student
		where user_id =  _user_id;
		set _rc = row_count();
	end if;
	END;
end if;
END $$
DELIMITER ;
DROP PROCEDURE IF EXISTS spEditVendor;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spEditVendor`
	(IN _name varchar(50), IN _isDelete tinyint(1), IN _newname varchar(50), IN _newaddress varchar(200), 
IN _newphone varchar(25), IN _newfax varchar(25), IN _newwebsite varchar(100),
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

set _rc = -1;
set _errmsg = "";
if (_isDelete) then
   delete from vendor where name =  _name;
   set _rc = row_count();
else
    BEGIN
	set _newname = TRIM(_newname);
	set _newaddress = TRIM(_newaddress);
	set _newphone = TRIM(_newphone);
	set _newfax = TRIM(_newfax);
	set _newwebsite = TRIM(_newwebsite);
	if (_newname = "") then
		set _rc = -1;
        set _errmsg = "invalid name";
	elseif (_newaddress = "") then
		set _rc = -1;
		set _errmsg = "invalid address";
	elseif (_newphone = "") then
		set _rc = -1;
		set _errmsg = "invalid phone";
	elseif (_newfax = "") then
		set _rc = -1;
		set _errmsg = "invalid fax";
	elseif (_newwebsite = "") then
		set _rc = -1;
		set _errmsg = "invalid website";
	else
		update vendor 
		set name = _newname, address = _newaddress, phone = _newphone, fax = _newfax, website = _newwebsite
		where name =  _name;
		set _rc = row_count();
	end if;
	END;
end if;
END $$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelAllAuth;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllAuth`()
BEGIN
select email, first_name, last_name, phone, user_id
from user
where is_exp_authority = 1
order by first_name asc;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelAllBudget;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllBudget`()
BEGIN
select budget_no, description
from budget
order by budget_no asc;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelAllBuilding;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllBuilding`()
BEGIN
select abbr, name, bldg_id
from building
order by abbr asc;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelAllCategory;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllCategory`()
BEGIN
select name, catg_id
from category
order by name asc;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelAllDept;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllDept`()
BEGIN
select abbr, name, dept_id
from dept
order by abbr asc;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelAllFaculty;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllFaculty`()
BEGIN
select email, first_name, last_name, phone, user_id
from user
where is_faculty = 1
order by first_name asc;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelAllOwner;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllOwner`()
BEGIN
select email, first_name, last_name, phone, user_id
from user
where is_owner = 1
order by first_name asc;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS spSelAllPurchaser;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllPurchaser`()
BEGIN
select email, first_name, last_name, phone, user_id
from user
where is_purchaser = 1
order by first_name asc;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelAllUser;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllUser`()
BEGIN
select email, first_name, last_name, phone, user_id, is_owner, is_purchaser, is_exp_authority, is_faculty, is_staff, is_student
from user
order by first_name asc;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelAllVendor;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllVendor`()
BEGIN
select name, address, phone, fax, website, vendor_id
from vendor
order by name asc;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS spSelLineItemOne;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelLineItemOne`(IN _order_id int)
BEGIN
select *
from lineitem
where order_id = _order_id  
order by line_no;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelOrdersOne;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelOrdersOne`(IN _order_id int)
BEGIN
select *
from orders
where order_id = _order_id and order_status != 0
order by order_id desc;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelOrdersZero;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelOrdersZero`()
BEGIN
select order_id
from orders
where is_delivered = 0
order by order_id desc;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelInvOne;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelInvOne`(IN _inv_id int)
BEGIN
select *
from inventory
where inv_id = _inv_id and inv_status != 0;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spSelInvReport;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelInvReport`(IN _min_ownerID int, IN _max_ownerID int, 
IN _min_bldgID int, IN _max_bldgID int, IN _min_catgID int, IN _max_catgID int, IN _min_orderno int, 
IN _max_orderno int, IN _min_invID int, IN _max_invID int, IN _sDate datetime, IN _eDate datetime)
BEGIN
select inv_id, first_name, last_name, abbr, room_no, category.name, 
	orders.order_id, serial_no, inventory.description, delivery_date
from inventory, user, building, category, orders
where (user_id <= _max_ownerID and user_id >= _min_ownerID) 
and (building.bldg_id <= _max_bldgID and building.bldg_id >= _min_bldgID)
and (category.catg_id <= _max_catgID and category.catg_id >= _min_catgID)
and (orders.order_id <= _max_orderno and orders.order_id >= _min_orderno)
and (inv_id <= _max_invID and inv_id >= _min_invID)
and (inventory.owner_id = user.user_id)
and (inventory.bldg_id = building.bldg_id)
and (inventory.catg_id = category.catg_id)
and (inventory.order_id = orders.order_id)
and (inventory.delivery_date <= _eDate and inventory.delivery_date >= _sDate)
order by inv_id desc
LIMIT 1000;
END$$
DELIMITER ;
DROP PROCEDURE IF EXISTS spEditLogin;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spEditLogin`(
IN _login_name varchar(30),
IN _first_name varchar(20), 
IN _last_name varchar(20),
IN _passwrd varchar(30), /* if empty - no change to password */
IN _email varchar(75),
IN _phone varchar(75),
IN _is_admin tinyint(1), 
IN _is_delete tinyint(1), 
OUT _rc int,
OUT _errmsg text)
MAIN:
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

set _rc = -1;
set _errmsg = "";

set _rc := 
(
	select is_admin
    from admin
    where login_name = _login_name
);

if (_rc = null) then
	set _rc = -1;
    set _errmsg = "Login Name not found!";
    LEAVE MAIN;
end if;

if (_is_delete) then
   delete from admin where login_name =  _login_name;
   set _rc = row_count();
else
    BEGIN
	set @len = length(_passwrd);
	set _passwrd = TRIM(_passwrd);
    set _first_name = TRIM(_first_name);
	set _last_name = TRIM(_last_name);
	set _phone = TRIM(_phone);
	set _email = TRIM(_email);

	if ((_first_name = "") || (_first_name NOT REGEXP BINARY '^[A-Z][a-zA-Z]+$')) then
			set _rc = -1;
            set _errmsg = "Invalid First Name";
            LEAVE MAIN;
	end if;
	if ((_last_name = "") || (_last_name NOT REGEXP BINARY '^[A-Z][a-zA-Z]+$')) then
			set _rc = -1;
            set _errmsg = "Invalid Last Name";
            LEAVE MAIN;
	end if;
	if ((_email = "") || (_email NOT REGEXP '^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$')) then
			set _rc = -1;
            set _errmsg = "Invalid Email";
            LEAVE MAIN;
	end if;
    if (_passwrd = "") then
		update admin 
			set is_admin = _is_admin, first_name = _first_name, last_name = _last_name,
				phone = _phone, email = _email
		where login_name =  _login_name;
		set _rc = row_count();
	else if ((@len < 4) || (@len > 30) || (_passwrd NOT REGEXP '^[A-Z0-9&]+$')) then
			 set _rc  = -1;
			 set _errmsg = "Invalid Password";
			 LEAVE MAIN;
		else
			update admin 
			set passwrd = SHA2(_passwrd, 224),
			is_admin = _is_admin, first_name = _first_name, last_name = _last_name,
			phone = _phone, email = _email
			where login_name =  _login_name;
			set _rc = row_count();
		end if;
	end if;
	END;
end if;

END$$
DELIMITER ;
use imdb;
DROP PROCEDURE IF EXISTS spLogin;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spLogin`
	(IN _login_name varchar(30), 
     IN _passwrd varchar(30),
     OUT _rc int,
     OUT _errmsg text
     )
     
MAIN: 
BEGIN

-- Diag info 

declare code char(5) default '00000';
declare msg text;

declare len int;

declare exit handler for sqlexception 
   BEGIN
	   set _rc = -1;
	   GET DIAGNOSTICS CONDITION 1
			code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT;
	   set _errmsg = CONCAT(code, "  ", msg); 
   END;
   
-- Check if input username and password exists in login table

# Set _rc to isAdmin 

set _rc := (
select is_admin 
from admin 
where  login_name = _login_name and binary passwrd = SHA2(_passwrd,224));


# No record found (ie login failed)

if (_rc = null) then
	set _rc = -1;
	set _errmsg = "Login Failed!";
	LEAVE MAIN;
end if;

set _errmsg = "";

END $$
DELIMITER ;
DROP PROCEDURE if exists spNewLoginAcct;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spNewLoginAcct`(IN _login_name varchar(30), 
     IN _passwrd varchar(30),
	 IN _repeat_passwrd varchar(30),
     IN _is_admin tinyint,
     IN _first_name varchar(30),
     IN _last_name varchar(30),
     IN _email varchar(75),
     IN _phone varchar(20),
     OUT _rc int,
     OUT _errmsg text
     )
MAIN: 
BEGIN

-- Diag info 

declare code char(5) default '00000';
declare msg text;

declare len int;

declare exit handler for sqlexception 
   BEGIN
	   set _rc = -1;
	   GET DIAGNOSTICS CONDITION 1
			code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT;
	   set _errmsg = CONCAT(code, "  ", msg); 
   END;

set len = length(_login_name);
if ((len < 4) || (len > 30) || (_login_name NOT REGEXP '^[A-Z0-9]+$')) then
	 set _rc  = -1;
	 set _errmsg = "Invalid Login Name";
     LEAVE MAIN;
end if;
set len = length(_passwrd);
set _passwrd = TRIM(_passwrd);
if ((len < 4) || (len > 30) || (_passwrd NOT REGEXP '^[A-Z0-9&]+$')) then
	 set _rc  = -1;
	 set _errmsg = "Invalid Password";
     LEAVE MAIN;
end if;

set _repeat_passwrd = TRIM(_repeat_passwrd);
if (_passwrd != _repeat_passwrd) then
	 set _rc  = -1;
	 set _errmsg = "Password Dont Match";
     LEAVE MAIN;
end if;

set len = length(_first_name);
set _first_name = TRIM(_first_name);
if ((len < 4) || (len > 30) || (_first_name NOT REGEXP BINARY '^[A-Z]+[a-zA-Z]+$')) then
	set _rc = -1;
	set _errmsg = "Invalid First Name";
    LEAVE MAIN;
end if;

set len = length(_last_name);
set _last_name = TRIM(_last_name);
if ((len < 4) || (len > 30) || (_last_name NOT REGEXP BINARY '^[A-Z]+[a-zA-Z]+$')) then
	set _rc = -1;
	set _errmsg = "Invalid Last Name";
    LEAVE MAIN;
end if;

set len = length(_email);
set _email = TRIM(_email);
if ((len < 4) || (len > 30) || (_email NOT REGEXP '^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$')) then
	set _rc = -1;
	set _errmsg = "Invalid Email";
    LEAVE MAIN;
end if;

 insert into admin(login_name, passwrd, is_admin, first_name, last_name, phone, email) 
			values (_login_name, sha2(_passwrd, 224), _is_admin, _first_name, _last_name, _phone, _email);
 set _rc  = last_insert_id();
 set _errmsg = "";
END$$
DELIMITER ;
use imdb;
DROP PROCEDURE IF EXISTS spSelAllLogin;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelAllLogin`()
BEGIN
select login_id, login_name, is_admin, first_name, last_name, phone, email, creation_date
from admin
order by login_name asc;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS spChangePW;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` 
PROCEDURE `spChangePW`
	(IN _login_name varchar(30), 
     IN _old_passwrd varchar(30),
     IN _new_passwrd varchar(30),
     IN _repeat_passwrd varchar(30),
     OUT _rc int,
     OUT _errmsg text
     )
     
MAIN: 
BEGIN

-- Diag info 

declare code char(5) default '00000';
declare msg text;

declare len int;

declare exit handler for sqlexception 
   BEGIN
	   set _rc = -1;
	   GET DIAGNOSTICS CONDITION 1
			code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT;
	   set _errmsg = CONCAT(code, "  ", msg); 
   END;
   
-- Check if input username and password exists in login table

set _new_passwrd = TRIM(_new_passwrd);
set _repeat_passwrd = TRIM(_repeat_passwrd);

# Invalid password
set len = length(_login_name);
if ((len < 4) || (len > 30) || (_new_passwrd NOT REGEXP '^[A-Z0-9&]+$')) then
	 set _rc  = -1;
	 set _errmsg = "Invalid New Password";
     LEAVE MAIN;
end if;

# Password and Confirmation Passwords don't match

if (_new_passwrd != _repeat_passwrd) then
	 set _rc  = -1;
	 set _errmsg = "Passwords Don't Match";
     LEAVE MAIN;
end if;

# Update password

update admin
set passwrd = SHA2(_new_passwrd, 224)
where login_name = _login_name and passwrd = SHA2(_old_passwrd, 224);
set _rc = row_count();

if (_rc = 0) then
	set _rc = -1;
	set _errmsg = "Invalid Old Password";
    LEAVE MAIN;
end if;
    
set _errmsg = "";

END $$
DELIMITER ;
