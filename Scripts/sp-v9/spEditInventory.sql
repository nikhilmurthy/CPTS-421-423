
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
