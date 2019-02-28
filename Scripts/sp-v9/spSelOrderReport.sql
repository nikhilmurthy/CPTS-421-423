
DROP PROCEDURE IF EXISTS spSelOrderReport;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spSelOrderReport`( 
IN _min_reqID int, IN _max_reqID int, IN _min_purchaserID int, IN _max_purchaserID int,
IN _min_authID int, IN _max_authID int, IN _min_facultyID int, IN _max_facultyID int, 
IN _min_deptID int, IN _max_deptID int, IN _min_budgetID int, IN _max_budgetID int, 
IN _min_vendorID int, IN _max_vendorID int, IN _min_orderno int, IN _max_orderno int, IN _sDate DateTime, IN _eDate DateTime, IN _qty_diff int)
BEGIN
select order_id, date, vendor.name, (case ship_mode when 0 then "Next Day" when 1 then "Two Day" when 2  then "Three Day" when 3 then "Ground"
when 4 then "Least Expensive" when 5 then "Electronic Delivery" end) as ship_mode, 
budget.budget_no,
p.first_name as pFName, p.last_name as pLName, o.first_name as oFName, o.last_name as oLName, dept.abbr, 
(case payment_type when 0 then "Credit Card" when 1 then "Dept. Req." 
when 2 then "IRI" when 3 then "Purchase Order" end) as payment_type, 
f.first_name as fFName, f.last_name as fLName, a.first_name as aFName, a.last_name as aLName, total_amt, total_qty, total_qty_recv, is_delivered, comment
from dept, budget, orders, vendor, user as a, user as f, user as o, user as p
where (orders.owner_id <= _max_reqID and orders.owner_id >= _min_reqID) 
and (orders.purchaser_id <= _max_purchaserID and orders.purchaser_id >= _min_purchaserID)
and (orders.sign_auth_id <= _max_authID and orders.sign_auth_id >= _min_authID)
and (orders.sign_faculty_id <= _max_facultyID and orders.sign_faculty_id >= _min_facultyID)
and (dept.dept_id <= _max_deptID and dept.dept_id >= _min_deptID)
and (budget.budget_no <= _max_budgetID and budget.budget_no >= _min_budgetID)
and (orders.order_id <= _max_orderno and orders.order_id >= _min_orderno)
and (vendor.vendor_id <= _max_vendorID and vendor.vendor_id >= _min_vendorID)
and (orders.vendor_id = vendor.vendor_id)
and (orders.budget_no = budget.budget_no)
and (orders.dept_id = dept.dept_id)
and (orders.purchaser_id = p.user_id)
and (orders.owner_id = o.user_id)
and (orders.sign_auth_id = a.user_id)
and (orders.sign_faculty_id = f.user_id)
and (orders.date <= _eDate and orders.date >= _sDate)
and (orders.total_qty - orders.total_qty_recv >= _qty_diff)
order by order_id desc
LIMIT 1000;
END$$
DELIMITER ;