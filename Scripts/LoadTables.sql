use imdb;

Set @id = -1;
Set @errmsg = "";
delete from inventory where inv_id < 100000000;
delete from lineitem where order_id < 1000000;
delete from orders where order_id < 100000000;

delete from budget where budget_no < 100000000;

delete from building where bldg_id < 100;

delete from category where catg_id < 100;

delete from dept where dept_id < 100;


delete from vendor where vendor_id < 100;

alter table building auto_increment = 1;

alter table category auto_increment = 1;

alter table dept auto_increment = 1;

alter table vendor auto_increment = 1;

delete from user where user_id < 10000000;


delete from admin where login_id < 1000;

alter table admin auto_increment = 1;

CALL spNewLoginAcct
				("admin", "adminpw", "adminpw", 1, 
                 "FnAdmin", "LnAdmin", "admin@aaa.edu","425-111-2222", @id, @errmsg);

CALL spNewLoginAcct
				("user", "userpw", "userpw", 0, 
                 "FnUser", "LnUser", "user@wsu.edu","425-222-3333", @id, @errmsg);
                 
 -- select @id, @errmsg;

CALL spNewBudget ("8001", "Office Supplies", @id, @errmsg);
CALL spNewBudget ("8002", "Personal Computing", @id, @errmsg);
CALL spNewBudget ("8003", "Server Computing", @id, @errmsg);
CALL spNewBudget ("8004", "Server Software", @id, @errmsg);
CALL spNewBudget ("8005", "Personal Software", @id, @errmsg);
CALL spNewBudget ("8006", "Furniture", @id, @errmsg);
CALL spNewBudget ("8007", "LAB Equipment", @id, @errmsg);
CALL spNewBudget ("8999", "Misc", @id, @errmsg);

CALL spNewBuilding ("ELB", "Engineering Laboratory Building", @id, @errmsg);
CALL spNewBuilding ("EME", "Elecitrical/Mechanical Engineering Building", @id, @errmsg);
CALL spNewBuilding ("ETRL", "Engineering Teaching/Research Lab Building", @id, @errmsg);
CALL spNewBuilding ("ITB", "Information Technology Building", @id, @errmsg);
CALL spNewBuilding ("SLOA", "Sloan Hall", @id, @errmsg);
CALL spNewBuilding ("ZZZZ", "Non Campus", @id, @errmsg);

CALL spNewCategory ("Desktop Computer", @id, @errmsg);
CALL spNewCategory ("Desktop Monitor", @id, @errmsg);
CALL spNewCategory ("Laptop", @id, @errmsg);
CALL spNewCategory ("Printer", @id, @errmsg);
CALL spNewCategory ("Server Computer", @id, @errmsg);
CALL spNewCategory ("Lab Hardware", @id, @errmsg);
CALL spNewCategory ("Furniture", @id, @errmsg);
CALL spNewCategory ("Stationary", @id, @errmsg);
CALL spNewCategory("Misc", @id, @errmsg);

CALL spNewDept ("C M E C", "Composite Material and Engineering Center", @id, @errmsg);
CALL spNewDept ("Elect Engr & Compt Sci", "Electrical Engineering and Computer Science", @id, @errmsg);
CALL spNewDept ("Civil & Envir Engr", "Civil and Environmental Engineering", @id, @errmsg);
CALL spNewDept ("Voiland Chem & BioEngr", "Voiland Chemical and Bio Engineering", @id, @errmsg);
CALL spNewDept ("Mech & Matls Engr", "Mechanical and Materials Engineering", @id, @errmsg);

CALL spNewVendor ("Office Depot", "14211 NE 88th Place, Redmond WA 98052", "425-812-1132", "425-111-1112","www.officedepot.com", @id, @errmsg);
CALL spNewVendor ("Amazon", "14211 NE 82nd Place, Seattle WA 98052", "800-812-1132", "425-888-9999","www.amazon.com", @id, @errmsg);
CALL spNewVendor ("Staples", "13002 NE 82th Place, Redmond WA 98052", "425-112-4132", "425-888-1112","www.staples.com", @id, @errmsg);
CALL spNewVendor ("Walmart", "211 SE 88th Place, Pullman WA 95678", "517-812-1132", "517-111-1112","www.walmart.com", @id, @errmsg);
CALL spNewVendor ("BestBuy", "511 SE 88th Place, Pullman WA 95678", "517-332-1132", "517-333-1112","www.bestbuy.com", @id, @errmsg);


-- spNewUser (ID, Email, First Name, Last Name, Phone #, Owner, Purchaser, Exp_Auth, Faculty, Staff, Student, @id, @errmsg)

-- Administration

CALL spNewUser (1, "shirazi@eecs.wsu.edu", "Behrooz", "Shirazi", "509-335-8148",2, "102F", true, true, true, true, false, false, @id, @errmsg);
CALL spNewUser (2, "arslanay@eecs.wsu.edu", "Sakire", "Ay", "509-335-4089", 2, "102D", true, false, true, true, false, false, @id, @errmsg);

-- Staff (Office)

CALL spNewUser (3, "zimmermanc@wsu.edu", "Cindy", "Zimmerman", "509-335-6603", 2, "102F", true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (4, "ninghsu@eecs.wsu.edu", "Ning", "Hsu", "509-335-6637", 2, "102", true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (5, "knigro@eecs.wsu.edu", "Kelly", "Nigro", "509-335-1406", 2, "102C",  true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (6, "roseanne@eecs.wsu.edu", "Roseanne", "August", "509-335-6602", 2, "102", true, false, false, false, true, false, @id, @errmsg);

-- Staff (Information Services)

CALL spNewUser (7, "jr@eecs.wsu.edu", "John", "Yates", "509-335-8060",  2, "43",true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (8, "apg@eecs.wsu.edu", "Allen", "Guyer", "509-335-6773", 5, "301A",true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (9, "tburt@eecs.wsu.edu", "Tony", "Burt", "509-335-6773", 5, "301A", true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (10, "vbunakov@eecs.wsu.edu", "Vasiliy", "Bunakov", "509-335-6773",5, "358", true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (11, "scott@eecs.wsu.edu", "Scott", "Hanson", "509-335-4959", 2, "43", true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (12, "vic@eecs.wsu.edu", "Victoria", "Sandmeyer", "509-335-5258", 5, "301B",true, false, false, false, true, false, @id, @errmsg);

-- Staff (Advisers)

CALL spNewUser (13, "joshwhiting@wsu.edu", "Josh", "Whiting", "509-335-2446", 2, "302", true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (14, "alliguyer@wsu.edu", "Alli", "Guyer", "509-335-0636", 2, "304",  true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (15, "sidra@eecs.wsu.edu", "Sidra", "Gleason", "509-335-6636", 2, "303", true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (16, "linda.howell@wsu.edu", "Linda", "Howell", "509-335-2483", 2, "307", true, false, false, false, true, false, @id, @errmsg);
CALL spNewUser (17, "pam.loughlin@wsu.edu", "Pam", "Loughlin", "425-259-8898", 6, "GWH-231B",true, false, false, false, true, false, @id, @errmsg);

-- Faculty Last Name A

CALL spNewUser (18, "kranova@eecs.wsu.edu", "Ashley", "Kranov", "",2, "191",true, false, false, true, false, false, @id, @errmsg);

-- Faculty Last Name B

CALL spNewUser (19, "bakken@eecs.wsu.edu", "Dave", "Bakken", "509-335-2399", 2, "55",true, false, false, true, false, false, @id, @errmsg);
CALL spNewUser (20, "belzer@eecs.wsu.edu", "Ben", "Belzer", "509-335-4970",2, "401",true, false, false, true, false, false, @id, @errmsg);
CALL spNewUser (21, "bose@eecs.wsu.edu", "Anjan", "Bose", "509-335-1147",2, "25",true, false, false, true, false, false, @id, @errmsg);
CALL spNewUser (22, "boyce@eecs.wsu.edu", "Doug", "Boyce", "",2, "353",true, false, false, true, false, false, @id, @errmsg);
CALL spNewUser (23, "shira@eecs.wsu.edu", "Shira", "Broschat", "509-335-5693",2, "223",true, false, false, true, false, false, @id, @errmsg);


-- Faculty Last Name C

CALL spNewUser (24, "drcall@vetmed.wsu.edu", "Douglas", "Call", "509-335-6313", 6, "bustad-415",true, false, false, true, false, false, @id, @errmsg);
CALL spNewUser (25, "brent.c@relayapplication.com", "Brent", "Carper", "",2, "261",true, false, false, true, false, false, @id, @errmsg);
CALL spNewUser (26, "ccole@eecs.wsu.edu", "Clint", "Cole", "509-335-1448",2, "506",true, false, false, true, false, false, @id, @errmsg);
CALL spNewUser (27, "cook@eecs.wsu.edu", "Diane", "Cook", "509-335-4985", 2, "121",true, false, false, true, false, false, @id, @errmsg);

-- Faculty Last Name D

CALL spNewUser (28, "zdang@eecs.wsu.edu", "Zhe", "Dang", "509-335-7238",2, "135",true, false, false, true, false, false, @id, @errmsg);
CALL spNewUser (29, "jdelgado@eecs.wsu.edu", "Jose", "Frias", "509-335-1156",2, "502",true, false, false, true, false, false, @id, @errmsg);
CALL spNewUser (30, "jana@eecs.wsu.edu", "Janardhan", "Doppa", "509-335-1846",2, "133",true, false, false, true, false, false, @id, @errmsg);

-- Faculty Last Name E (None)


Set @id =0;
Set @errmsg = "";
CALL spNewPurchaseRequest 
  (
	100102, 
   '2014-01-05', -- date
   1, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   3,  -- requestor id
   2, -- dept id
   3,  -- payment type
   2,  -- faculty id
   2,  -- auth id
   850.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "234-1234|889-1234|012-9989|",  -- catlog list
    "DELL PC|DELL Monitor|DELL Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "500.00|100.00|150.99|",  -- unit price list
    "500.00|200.00|150.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message    

CALL spNewPurchaseRequest 
  (
	100120, 
   '2014-01-15', -- date
   1, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   4,  -- requestor id
   2, -- dept id
   3,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   850.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "234-1234|889-1234|012-9989|",  -- catlog list
    "DELL PC|DELL Monitor|DELL Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "500.00|100.00|150.99|",  -- unit price list
    "500.00|200.00|150.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message    

CALL spNewPurchaseRequest 
  (
	100330, 
   '2014-02-07', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   5,  -- requestor id
   2, -- dept id
   1,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   1150.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|012-1234|",  -- catlog list
    "HP PC|HP Monitor|HP Printer|", -- desciption list
    "1|1|1|",  -- qty list
	"1|1|1",
    "600.00|300.00|250.99|",  -- unit price list
    "600.00|300.00|250.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message   
    
CALL spNewPurchaseRequest 
  (
	100431, 
   '2014-02-25', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   6,  -- requestor id
   2, -- dept id
   0,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   1150.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|012-1234|",  -- catlog list
    "HP PC|HP Monitor|HP Printer|", -- desciption list
    "1|1|1|",  -- qty list
	"1|1|1",
    "600.00|300.00|250.99|",  -- unit price list
    "600.00|300.00|250.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message   
CALL spNewPurchaseRequest 
  (
	100532, 
   '2014-03-05', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   7,  -- requestor id
   2, -- dept id
   0,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "600.00|300.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
    
CALL spNewPurchaseRequest 
  (
	100635, 
   '2014-03-15', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   8,  -- requestor id
   2, -- dept id
   3,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "600.00|300.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  

CALL spNewPurchaseRequest 
  (
	100636, 
   '2014-03-15', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   9,  -- requestor id
   2, -- dept id
   3,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   950.00, -- total amount
   2,      -- line item count
	"PC Refresh Order",
    "834-1234|345-4234|",  -- catlog list
    "DELL PC|DELL Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "650.00|300.00|",  -- unit price list
    "650.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message 
CALL spNewPurchaseRequest 
  (
	100636, 
   '2014-03-15', -- date
   1, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   10,  -- requestor id
   2, -- dept id
   3,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "600.00|300.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
    
CALL spNewPurchaseRequest 
  (
	100777, 
   '2014-05-01', -- date
   3, -- vendor id
   3,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   11,  -- requestor id
   2, -- dept id
   0,  -- payment type
   2,  -- faculty id
   1,  -- auth id
  1050.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "X34-1234|X89-1234|X12-9989|",  -- catlog list
    "LENOVO PC|LENOVO Monitor|LENOVO Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "600.00|150.00|150.99|",  -- unit price list
    "600.00|300.00|150.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
CALL spNewPurchaseRequest 
  (
	100800, 
   '2014-05-07', -- date
   3, -- vendor id
   3,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   12,  -- requestor id
   2, -- dept id
   0,  -- payment type
   2,  -- faculty id
   1,  -- auth id
  1050.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "X34-1234|X89-1234|X12-9989|",  -- catlog list
    "LENOVO PC|LENOVO Monitor|LENOVO Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "600.00|150.00|150.99|",  -- unit price list
    "600.00|300.00|150.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  

CALL spNewPurchaseRequest 
  (
	100801, 
   '2014-05-10', -- date
   3, -- vendor id
   3,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   13,  -- requestor id
   2, -- dept id
   0,  -- payment type
   2,  -- faculty id
   1,  -- auth id
  1050.99, -- total amount
   3,      -- line item count
	"PC Upgrade",
    "X34-1234|X89-1234|X12-9989|",  -- catlog list
    "LENOVO PC|LENOVO Monitor|LENOVO Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "600.00|150.00|150.99|",  -- unit price list
    "600.00|300.00|150.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
CALL spNewPurchaseRequest 
  (
	100832, 
   '2014-09-05', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   14,  -- requestor id
   2, -- dept id
   0,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "600.00|300.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
    
CALL spNewPurchaseRequest 
  (
	100835, 
   '2014-10-15', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   15,  -- requestor id
   2, -- dept id
   3,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "600.00|300.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  

CALL spNewPurchaseRequest 
  (
	100836, 
   '2014-11-15', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   16,  -- requestor id
   2, -- dept id
   3,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   950.00, -- total amount
   2,      -- line item count
	"PC Upgrade",
    "834-1234|345-4234|",  -- catlog list
    "DELL PC|DELL Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "650.00|300.00|",  -- unit price list
    "650.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message 
CALL spNewPurchaseRequest 
  (
	100936, 
   '2014-11-15', -- date
   1, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   17,  -- requestor id
   2, -- dept id
   3,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "600.00|300.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
    
    CALL spNewPurchaseRequest 
  (
	110102, 
   '2014-12-01', -- date
   1, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   18,  -- requestor id
   2, -- dept id
   3,  -- payment type
   18,  -- faculty id
   2,  -- auth id
   850.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "234-1234|889-1234|012-9989|",  -- catlog list
    "DELL PC|DELL Monitor|DELL Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "500.00|100.00|150.99|",  -- unit price list
    "500.00|200.00|150.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message    

/*
CALL spNewPurchaseRequest 
  (
	110120, 
   '2014-12-15', -- date
   1, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   18,  -- requestor id
   2, -- dept id
   3,  -- payment type
   18,  -- faculty id
   1,  -- auth id
   850.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "234-1234|889-1234|012-9989|",  -- catlog list
    "DELL PC|DELL Monitor|DELL Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "500.00|100.00|150.99|",  -- unit price list
    "500.00|200.00|150.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message    
    
    */

CALL spNewPurchaseRequest 
  (
	110330, 
   '2015-01-03', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   19,  -- requestor id
   2, -- dept id
   1,  -- payment type
   19,  -- faculty id
   1,  -- auth id
   1150.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|012-1234|",  -- catlog list
    "HP PC|HP Monitor|HP Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "600.00|150.00|250.99|",  -- unit price list
    "600.00|300.00|250.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message   
    
CALL spNewPurchaseRequest 
  (
	110431, 
   '2015-01-05', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   20,  -- requestor id
   2, -- dept id
   0,  -- payment type
   20,  -- faculty id
   1,  -- auth id
   1150.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|012-1234|",  -- catlog list
    "HP PC|HP Monitor|HP Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "600.00|150.00|250.99|",  -- unit price list
    "600.00|300.00|250.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message   
CALL spNewPurchaseRequest 
  (
	110532, 
   '2015-01-15', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   21,  -- requestor id
   2, -- dept id
   0,  -- payment type
   21,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Upgrade",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|2|",  -- qty list
	"1|1|",
    "600.00|150.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
    
CALL spNewPurchaseRequest 
  (
	110635, 
   '2015-01-15', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   22,  -- requestor id
   2, -- dept id
   3,  -- payment type
   22,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "600.00|300.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  

CALL spNewPurchaseRequest 
  (
	110636, 
   '2015-02-01', -- date
   2, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   23,  -- requestor id
   2, -- dept id
   3,  -- payment type
   23,  -- faculty id
   1,  -- auth id
   950.00, -- total amount
   2,      -- line item count
	"PC Refresh Order",
    "834-1234|345-4234|",  -- catlog list
    "DELL PC|DELL Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "650.00|300.00|",  -- unit price list
    "650.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message 
CALL spNewPurchaseRequest 
  (
	110636, 
   '2015-02-15', -- date
   1, -- vendor id
   2,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   24,  -- requestor id
   2, -- dept id
   3,  -- payment type
   24,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Refresh Order",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|2|",  -- qty list
	"1|1|",
    "600.00|150.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
    
CALL spNewPurchaseRequest 
  (
	110777, 
   '2015-04-05', -- date
   3, -- vendor id
   3,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   25,  -- requestor id
   2, -- dept id
   0,  -- payment type
   25,  -- faculty id
   1,  -- auth id
  1050.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "X34-1234|X89-1234|X12-9989|",  -- catlog list
    "LENOVO PC|LENOVO Monitor|LENOVO Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "600.00|150.00|150.99|",  -- unit price list
    "600.00|300.00|150.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
/*
CALL spNewPurchaseRequest 
  (
	110800, 
   '2014-03-21', -- date
   3, -- vendor id
   3,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   26,  -- requestor id
   2, -- dept id
   0,  -- payment type
   26,  -- faculty id
   1,  -- auth id
  1050.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "X34-1234|X89-1234|X12-9989|",  -- catlog list
    "LENOVO PC|LENOVO Monitor|LENOVO Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "600.00|150.00|150.99|",  -- unit price list
    "600.00|300.00|150.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
*/

CALL spNewPurchaseRequest 
  (
	110801, 
   '2015-04-06', -- date
   3, -- vendor id
   3,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   27,  -- requestor id
   2, -- dept id
   0,  -- payment type
   27,  -- faculty id
   1,  -- auth id
  1050.99, -- total amount
   3,      -- line item count
	"PC Refresh Order",
    "X34-1234|X89-1234|X12-9989|",  -- catlog list
    "LENOVO PC|LENOVO Monitor|LENOVO Printer|", -- desciption list
    "1|2|1|",  -- qty list
	"1|1|1",
    "600.00|150.00|150.99|",  -- unit price list
    "600.00|300.00|150.99|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
CALL spNewPurchaseRequest 
  (
	110832, 
   '2015-04-06', -- date
   2, -- vendor id
   3,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   28,  -- requestor id
   2, -- dept id
   0,  -- payment type
   28,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Upgrade",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|2|",  -- qty list
	"1|1|",
    "600.00|150.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  
    
CALL spNewPurchaseRequest 
  (
	110835, 
   '2015-04-08', -- date
   2, -- vendor id
   3,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   29,  -- requestor id
   2, -- dept id
   3,  -- payment type
   29,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Upgrade",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|1|",  -- qty list
	"1|1|",
    "600.00|300.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  

CALL spNewPurchaseRequest 
  (
	110836, 
   '2015-04-09', -- date
   2, -- vendor id
   3,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   30,  -- requestor id
   2, -- dept id
   3,  -- payment type
   30,  -- faculty id
   1,  -- auth id
   950.00, -- total amount
   2,      -- line item count
	"PC Upgrade",
    "834-1234|345-4234|",  -- catlog list
    "DELL PC|DELL Monitor|", -- desciption list
    "1|2|",  -- qty list
	"1|1|",
    "650.00|150.00|",  -- unit price list
    "650.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message 
CALL spNewPurchaseRequest 
  (
	110936, 
   '2015-04-10', -- date
   1, -- vendor id
   3,  -- ship mode
   8002, -- budget no
   1,  -- purchaser id
   2,  -- requestor id
   2, -- dept id
   3,  -- payment type
   2,  -- faculty id
   1,  -- auth id
   900.00, -- total amount
   2,      -- line item count
	"PC Upgrade",
    "434-1234|345-1234|",  -- catlog list
    "HP PC|HP Monitor|", -- desciption list
    "1|2|",  -- qty list
	"1|1|",
    "600.00|150.00|",  -- unit price list
    "600.00|300.00|",  -- total price list
    @id, -- out order no
    @errmsg); -- out error message  

call spNewInventory
(
	5001, -- inv id
	"DELL PC", -- description
	1, -- category id
	2, -- building id
	"102F", -- room number
	3, -- owner id
	100102, -- order number
	1, -- line number
	1, -- vendor id
	2, -- department id
	'2014-01-12', -- delivery date
	'2014-01-05', -- purchase date
	500.00, -- purchase price
	"DELL", -- manufacturer
	1450, -- trans no
	21634567, -- serial no
	"DELL z440", -- model no
	"Ship delay", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5002, -- inv id
	"DELL Monitor", -- description
	2, -- category id
	2, -- building id
	"102F", -- room number
	3, -- owner id
	100102, -- order number
	2, -- line number
	1, -- vendor id
	2, -- department id
	'2014-01-14', -- delivery date
	'2014-01-05', -- purchase date
	100.00, -- purchase price
	"DELL", -- manufacturer
	1450, -- trans no
	816345778, -- serial no
	"DELL-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5003, -- inv id
	"DELL Monitor", -- description
	2, -- category id
	2, -- building id
	"102F", -- room number
	3, -- owner id
	100102, -- order number
	2, -- line number
	1, -- vendor id
	2, -- department id
	'2014-01-14', -- delivery date
	'2014-01-05', -- purchase date
	100.00, -- purchase price
	"DELL", -- manufacturer
	1450, -- trans no
	816345879, -- serial no
	"DELL-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5004, -- inv id
	"DELL Printer", -- description
	4, -- category id
	2, -- building id
	"102F", -- room number
	3, -- owner id
	100102, -- order number
	3, -- line number
	1, -- vendor id
	2, -- department id
	'2014-01-14', -- delivery date
	'2014-01-05', -- purchase date
	150.99, -- purchase price
	"DELL", -- manufacturer
	1450, -- trans no
	916348778, -- serial no
	"All-in-One 2321", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5005, -- inv id
	"DELL PC", -- description
	1, -- category id
	2, -- building id
	"102", -- room number
	4, -- owner id
	100120, -- order number
	1, -- line number
	1, -- vendor id
	2, -- department id
	'2014-01-22', -- delivery date
	'2014-01-15', -- purchase date
	500.00, -- purchase price
	"DELL", -- manufacturer
	1451, -- trans no
	21634577, -- serial no
	"DELL z440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5006, -- inv id
	"DELL Monitor", -- description
	2, -- category id
	2, -- building id
	"102", -- room number
	4, -- owner id
	100120, -- order number
	2, -- line number
	1, -- vendor id
	2, -- department id
	'2014-01-24', -- delivery date
	'2014-01-15', -- purchase date
	100.00, -- purchase price
	"DELL", -- manufacturer
	1451, -- trans no
	816345992, -- serial no
	"DELL-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5007, -- inv id
	"DELL Monitor", -- description
	2, -- category id
	2, -- building id
	"102", -- room number
	4, -- owner id
	100120, -- order number
	2, -- line number
	1, -- vendor id
	2, -- department id
	'2014-01-24', -- delivery date
	'2014-01-15', -- purchase date
	100.00, -- purchase price
	"DELL", -- manufacturer
	1451, -- trans no
	816347879, -- serial no
	"DELL-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5008, -- inv id
	"DELL Printer", -- description
	4, -- category id
	2, -- building id
	"102", -- room number
	4, -- owner id
	100120, -- order number
	3, -- line number
	1, -- vendor id
	2, -- department id
	'2014-01-24', -- delivery date
	'2014-01-15', -- purchase date
	150.99, -- purchase price
	"DELL", -- manufacturer
	1451, -- trans no
	916448778, -- serial no
	"All-in-One 2321", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5009, -- inv id
	"HP PC", -- description
	1, -- category id
	2, -- building id
	"102C", -- room number
	5, -- owner id
	100330, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2014-02-14', -- delivery date
	'2014-02-07', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1551, -- trans no
	88834577, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5010, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"102C", -- room number
	5, -- owner id
	100330, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2014-02-16', -- delivery date
	'2014-02-07', -- purchase date
	300.00, -- purchase price
	"HP", -- manufacturer
	1551, -- trans no
	881345992, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5011, -- inv id
	"HP Printer", -- description
	4, -- category id
	2, -- building id
	"102C", -- room number
	5, -- owner id
	100330, -- order number
	3, -- line number
	2, -- vendor id
	2, -- department id
	'2014-02-16', -- delivery date
	'2014-02-07', -- purchase date
	250.99, -- purchase price
	"HP", -- manufacturer
	1551, -- trans no
	989448778, -- serial no
	"HP-All-in-One 881", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5012, -- inv id
	"HP PC", -- description
	1, -- category id
	2, -- building id
	"102", -- room number
	6, -- owner id
	100431, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2014-03-01', -- delivery date
	'2014-02-25', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1552, -- trans no
	88834587, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5013, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"102", -- room number
	6, -- owner id
	100431, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2014-03-02', -- delivery date
	'2014-02-25', -- purchase date
	300.00, -- purchase price
	"HP", -- manufacturer
	1552, -- trans no
	881345993, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5014, -- inv id
	"HP Printer", -- description
	4, -- category id
	2, -- building id
	"102", -- room number
	6, -- owner id
	100431, -- order number
	3, -- line number
	2, -- vendor id
	2, -- department id
	'2014-03-03', -- delivery date
	'2014-02-25', -- purchase date
	250.99, -- purchase price
	"HP", -- manufacturer
	1552, -- trans no
	989448878, -- serial no
	"HP-All-in-One 881", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5015, -- inv id
	"HP PC", -- description
	1, -- category id
	2, -- building id
	"43", -- room number
	7, -- owner id
	100532, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2014-03-12', -- delivery date
	'2014-03-05', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1553, -- trans no
	88838587, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5016, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"43", -- room number
	7, -- owner id
	100532, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2014-03-14', -- delivery date
	'2014-03-05', -- purchase date
	300.00, -- purchase price
	"HP", -- manufacturer
	1553, -- trans no
	881347993, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5017, -- inv id
	"HP PC", -- description
	1, -- category id
	5, -- building id
	"301A", -- room number
	8, -- owner id
	100635, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2014-03-20', -- delivery date
	'2014-03-15', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1554, -- trans no
	88839587, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5018, -- inv id
	"HP Monitor", -- description
	2, -- category id
	5, -- building id
	"301A", -- room number
	8, -- owner id
	100635, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2014-03-21', -- delivery date
	'2014-03-15', -- purchase date
	300.00, -- purchase price
	"HP", -- manufacturer
	1554, -- trans no
	881347998, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5019, -- inv id
	"DELL PC", -- description
	1, -- category id
	5, -- building id
	"301A", -- room number
	9, -- owner id
	100636, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2014-03-25', -- delivery date
	'2014-03-15', -- purchase date
	600.00, -- purchase price
	"DELL", -- manufacturer
	1555, -- trans no
	88839987, -- serial no
	"DELL M440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5020, -- inv id
	"DELL Monitor", -- description
	2, -- category id
	5, -- building id
	"301A", -- room number
	9, -- owner id
	100636, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2014-03-26', -- delivery date
	'2014-03-15', -- purchase date
	300.00, -- purchase price
	"DELL", -- manufacturer
	1554, -- trans no
	881347998, -- serial no
	"DELL-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5021, -- inv id
	"LENOVO PC", -- description
	1, -- category id
	2, -- building id
	"43", -- room number
	11, -- owner id
	100777, -- order number
	1, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-03', -- delivery date
	'2014-05-01', -- purchase date
	600.00, -- purchase price
	"LENOVO", -- manufacturer
	1470, -- trans no
	21834567, -- serial no
	"LENOVO z440", -- model no
	"Ship delay", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5022, -- inv id
	"LENOVO Monitor", -- description
	2, -- category id
	2, -- building id
	"43", -- room number
	11, -- owner id
	100777, -- order number
	2, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-04', -- delivery date
	'2014-05-01', -- purchase date
	150.00, -- purchase price
	"LENOVO", -- manufacturer
	1470, -- trans no
	816365778, -- serial no
	"LENOVO-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5023, -- inv id
	"LENOVO Monitor", -- description
	2, -- category id
	2, -- building id
	"43", -- room number
	11, -- owner id
	100777, -- order number
	2, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-04', -- delivery date
	'2014-05-01', -- purchase date
	150.00, -- purchase price
	"LENOVO", -- manufacturer
	1470, -- trans no
	816645879, -- serial no
	"LENOVO-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5024, -- inv id
	"LENOVO Printer", -- description
	4, -- category id
	2, -- building id
	"43", -- room number
	11, -- owner id
	100777, -- order number
	3, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-05', -- delivery date
	'2014-05-01', -- purchase date
	150.99, -- purchase price
	"LENOVO", -- manufacturer
	1470, -- trans no
	918348778, -- serial no
	"LENOVO-All-in-One 2321", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5025, -- inv id
	"LENOVO PC", -- description
	1, -- category id
	2, -- building id
	"530B", -- room number
	12, -- owner id
	100800, -- order number
	1, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-10', -- delivery date
	'2014-05-07', -- purchase date
	600.00, -- purchase price
	"LENOVO", -- manufacturer
	1471, -- trans no
	21854567, -- serial no
	"LENOVO z440", -- model no
	"Ship delay", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5026, -- inv id
	"LENOVO Monitor", -- description
	2, -- category id
	2, -- building id
	"530B", -- room number
	12, -- owner id
	100800, -- order number
	2, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-10', -- delivery date
	'2014-05-07', -- purchase date
	150.00, -- purchase price
	"LENOVO", -- manufacturer
	1471, -- trans no
	816565778, -- serial no
	"LENOVO-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5027, -- inv id
	"LENOVO Monitor", -- description
	2, -- category id
	2, -- building id
	"530B", -- room number
	12, -- owner id
	100800, -- order number
	2, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-10', -- delivery date
	'2014-05-07', -- purchase date
	150.00, -- purchase price
	"LENOVO", -- manufacturer
	1471, -- trans no
	816945879, -- serial no
	"LENOVO-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5028, -- inv id
	"LENOVO Printer", -- description
	4, -- category id
	2, -- building id
	"530B", -- room number
	12, -- owner id
	100800, -- order number
	3, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-12', -- delivery date
	'2014-05-07', -- purchase date
	150.99, -- purchase price
	"LENOVO", -- manufacturer
	1471, -- trans no
	918548778, -- serial no
	"LENOVO-All-in-One 2321", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);

call spNewInventory
(
	5029, -- inv id
	"LENOVO PC", -- description
	1, -- category id
	2, -- building id
	"302", -- room number
	13, -- owner id
	100801, -- order number
	1, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-20', -- delivery date
	'2014-05-10', -- purchase date
	600.00, -- purchase price
	"LENOVO", -- manufacturer
	1472, -- trans no
	21854568, -- serial no
	"LENOVO z440", -- model no
	"Ship delay", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5030, -- inv id
	"LENOVO Monitor", -- description
	2, -- category id
	2, -- building id
	"302", -- room number
	13, -- owner id
	100801, -- order number
	2, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-21', -- delivery date
	'2014-05-10', -- purchase date
	150.00, -- purchase price
	"LENOVO", -- manufacturer
	1472, -- trans no
	816565779, -- serial no
	"LENOVO-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5031, -- inv id
	"LENOVO Monitor", -- description
	2, -- category id
	2, -- building id
	"302", -- room number
	13, -- owner id
	100801, -- order number
	2, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-22', -- delivery date
	'2014-05-10', -- purchase date
	150.00, -- purchase price
	"LENOVO", -- manufacturer
	1472, -- trans no
	816945882, -- serial no
	"LENOVO-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5032, -- inv id
	"LENOVO Printer", -- description
	4, -- category id
	2, -- building id
	"302", -- room number
	13, -- owner id
	100801, -- order number
	3, -- line number
	3, -- vendor id
	2, -- department id
	'2014-05-23', -- delivery date
	'2014-05-10', -- purchase date
	150.99, -- purchase price
	"LENOVO", -- manufacturer
	1471, -- trans no
	918548779, -- serial no
	"LENOVO-All-in-One 2321", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5033, -- inv id
	"HP PC", -- description
	1, -- category id
	2, -- building id
	"304", -- room number
	14, -- owner id
	100832, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2014-09-12', -- delivery date
	'2014-09-05', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1753, -- trans no
	89838587, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5034, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"304", -- room number
	14, -- owner id
	100832, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2014-09-14', -- delivery date
	'2014-09-05', -- purchase date
	300.00, -- purchase price
	"HP", -- manufacturer
	1753, -- trans no
	883347993, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5035, -- inv id
	"HP PC", -- description
	1, -- category id
	2, -- building id
	"303", -- room number
	15, -- owner id
	100835, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2014-10-22', -- delivery date
	'2014-10-15', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1754, -- trans no
	89838597, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5036, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"303", -- room number
	15, -- owner id
	100835, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2014-10-24', -- delivery date
	'2014-10-15', -- purchase date
	300.00, -- purchase price
	"HP", -- manufacturer
	1754, -- trans no
	883347998, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5037, -- inv id
	"DELL PC", -- description
	1, -- category id
	2, -- building id
	"307", -- room number
	16, -- owner id
	100836, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2014-11-22', -- delivery date
	'2014-11-15', -- purchase date
	650.00, -- purchase price
	"DELL", -- manufacturer
	1755, -- trans no
	89998597, -- serial no
	"DELL X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5038, -- inv id
	"DELL Monitor", -- description
	2, -- category id
	2, -- building id
	"307", -- room number
	16, -- owner id
	100836, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2014-11-24', -- delivery date
	'2014-11-15', -- purchase date
	300.00, -- purchase price
	"DELL", -- manufacturer
	1755, -- trans no
	883347999, -- serial no
	"DELL-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5039, -- inv id
	"HP PC", -- description
	1, -- category id
	6, -- building id
	"GWH-231B", -- room number
	17, -- owner id
	100936, -- order number
	1, -- line number
	1, -- vendor id
	2, -- department id
	'2014-12-01', -- delivery date
	'2014-11-15', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1783, -- trans no
	89848587, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5040, -- inv id
	"HP Monitor", -- description
	2, -- category id
	6, -- building id
	"GWH-231B", -- room number
	17, -- owner id
	100936, -- order number
	2, -- line number
	1, -- vendor id
	2, -- department id
	'2014-12-02', -- delivery date
    '2014-11-15', -- purchase date
	300.00, -- purchase price
	"HP", -- manufacturer
	1783, -- trans no
	883347998, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5041, -- inv id
	"DELL PC", -- description
	1, -- category id
	2, -- building id
	"191", -- room number
	18, -- owner id
	110102, -- order number
	1, -- line number
	1, -- vendor id
	2, -- department id
	'2014-12-12', -- delivery date
	'2014-12-01', -- purchase date
	500.00, -- purchase price
	"DELL", -- manufacturer
	1490, -- trans no
	31634567, -- serial no
	"DELL z440", -- model no
	"Ship delay", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5042, -- inv id
	"DELL Monitor", -- description
	2, -- category id
	2, -- building id
	"191", -- room number
	18, -- owner id
	110102, -- order number
	2, -- line number
	1, -- vendor id
	2, -- department id
	'2014-12-14', -- delivery date
	'2014-12-01', -- purchase date
	100.00, -- purchase price
	"DELL", -- manufacturer
	1490, -- trans no
	856345778, -- serial no
	"DELL-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5043, -- inv id
	"DELL Monitor", -- description
	2, -- category id
	2, -- building id
	"191", -- room number
	18, -- owner id
	110102, -- order number
	2, -- line number
	1, -- vendor id
	2, -- department id
	'2014-12-14', -- delivery date
	'2014-12-01', -- purchase date
	100.00, -- purchase price
	"DELL", -- manufacturer
	1490, -- trans no
	856345879, -- serial no
	"DELL-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5044, -- inv id
	"DELL Printer", -- description
	4, -- category id
	2, -- building id
	"191", -- room number
	18, -- owner id
	110102, -- order number
	3, -- line number
	1, -- vendor id
	2, -- department id
	'2014-12-14', -- delivery date
	'2014-12-01', -- purchase date
	150.99, -- purchase price
	"DELL", -- manufacturer
	1490, -- trans no
	926348778, -- serial no
	"All-in-One 2321", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5045, -- inv id
	"HP PC", -- description
	1, -- category id
	2, -- building id
	"55", -- room number
	19, -- owner id
	110330, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-09', -- delivery date
	'2015-01-03', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1551, -- trans no
	88834577, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5046, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"55", -- room number
	19, -- owner id
	110330, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-09', -- delivery date
	'2015-01-03', -- purchase date
	150.00, -- purchase price
	"HP", -- manufacturer
	1551, -- trans no
	881345992, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5047, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"55", -- room number
	19, -- owner id
	110330, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-11', -- delivery date
	'2015-01-03', -- purchase date
	150.00, -- purchase price
	"HP", -- manufacturer
	1551, -- trans no
	881345992, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5048, -- inv id
	"HP Printer", -- description
	4, -- category id
	2, -- building id
	"55", -- room number
	19, -- owner id
	110330, -- order number
	3, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-12', -- delivery date
	'2015-01-03', -- purchase date
	250.99, -- purchase price
	"HP", -- manufacturer
	1551, -- trans no
	989448778, -- serial no
	"HP-All-in-One 881", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5049, -- inv id
	"HP PC", -- description
	1, -- category id
	2, -- building id
	"401", -- room number
	20, -- owner id
	110431, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-13', -- delivery date
	'2015-01-05', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1552, -- trans no
	88834578, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5050, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"401", -- room number
	20, -- owner id
	110431, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-13', -- delivery date
	'2015-01-05', -- purchase date
	300.00, -- purchase price
	"HP", -- manufacturer
	1552, -- trans no
	881345993, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5051, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"401", -- room number
	20, -- owner id
	110431, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-15', -- delivery date
	'2015-01-05', -- purchase date
	300.00, -- purchase price
	"HP", -- manufacturer
	1552, -- trans no
	881345995, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5052, -- inv id
	"HP Printer", -- description
	4, -- category id
	2, -- building id
	"401", -- room number
	20, -- owner id
	110431, -- order number
	3, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-15', -- delivery date
	'2015-01-05', -- purchase date
	250.99, -- purchase price
	"HP", -- manufacturer
	1552, -- trans no
	989448779, -- serial no
	"HP-All-in-One 881", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5053, -- inv id
	"HP PC", -- description
	1, -- category id
	2, -- building id
	"25", -- room number
	21, -- owner id
	110532, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-19', -- delivery date
	'2015-01-15', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1553, -- trans no
	88834578, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5054, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"25", -- room number
	21, -- owner id
	110532, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-20', -- delivery date
	'2015-01-15', -- purchase date
	150.00, -- purchase price
	"HP", -- manufacturer
	1552, -- trans no
	881346993, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5055, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"25", -- room number
	21, -- owner id
	110532, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-20', -- delivery date
	'2015-01-15', -- purchase date
	150.00, -- purchase price
	"HP", -- manufacturer
	1552, -- trans no
	881346995, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5056, -- inv id
	"HP PC", -- description
	1, -- category id
	2, -- building id
	"353", -- room number
	22, -- owner id
	110635, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-22', -- delivery date
	'2015-01-15', -- purchase date
	600.00, -- purchase price
	"HP", -- manufacturer
	1554, -- trans no
	88934578, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5057, -- inv id
	"HP Monitor", -- description
	2, -- category id
	2, -- building id
	"353", -- room number
	22, -- owner id
	110635, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2015-01-23', -- delivery date
	'2015-01-15', -- purchase date
	300.00, -- purchase price
	"HP", -- manufacturer
	1554, -- trans no
	882346993, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5058, -- inv id
	"DELL PC", -- description
	1, -- category id
	2, -- building id
	"223", -- room number
	23, -- owner id
	110636, -- order number
	1, -- line number
	2, -- vendor id
	2, -- department id
	'2015-02-12', -- delivery date
	'2015-02-01', -- purchase date
	650.00, -- purchase price
	"DELL", -- manufacturer
	1555, -- trans no
	98934578, -- serial no
	"HP X440", -- model no
	"no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5059, -- inv id
	"DELL Monitor", -- description
	2, -- category id
	2, -- building id
	"223", -- room number
	23, -- owner id
	110636, -- order number
	2, -- line number
	2, -- vendor id
	2, -- department id
	'2015-02-13', -- delivery date
	'2015-02-01', -- purchase date
	300.00, -- purchase price
	"DELL", -- manufacturer
	1555, -- trans no
	982346993, -- serial no
	"HP-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5060, -- inv id
	"LENOVO Monitor", -- description
	2, -- category id
	2, -- building id
	"261", -- room number
	25, -- owner id
	110777, -- order number
	2, -- line number
	3, -- vendor id
	2, -- department id
	'2015-04-10', -- delivery date
	'2015-04-05', -- purchase date
	150.00, -- purchase price
	"LENOVO", -- manufacturer
	1752, -- trans no
	881356995, -- serial no
	"LENOVO-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5061, -- inv id
	"LENOVO Printer", -- description
	4, -- category id
	2, -- building id
	"261", -- room number
	25, -- owner id
	110777, -- order number
	3, -- line number
	3, -- vendor id
	2, -- department id
	'2015-04-11', -- delivery date
	'2015-04-05', -- purchase date
	150.99, -- purchase price
	"LENOVO", -- manufacturer
	1752, -- trans no
	989468779, -- serial no
	"LENOVO-All-in-One 881", -- model no
	"on time; no damage", -- comment
	@id,
	@errmsg
);
call spNewInventory
(
	5062, -- inv id
	"LENOVO Monitor", -- description
	2, -- category id
	2, -- building id
	"121", -- room number
	27, -- owner id
	110801, -- order number
	2, -- line number
	3, -- vendor id
	2, -- department id
	'2015-04-11', -- delivery date
	'2015-04-06', -- purchase date
	150.00, -- purchase price
	"LENOVO", -- manufacturer
	1762, -- trans no
	881456995, -- serial no
	"LENOVO-HD-17-Z10", -- model no
	"minor scratch", -- comment
	@id,
	@errmsg
);
    select @id;
    select @errmsg;