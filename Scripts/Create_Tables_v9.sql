DROP DATABASE IF EXISTS imdb;
CREATE DATABASE imdb;

USE imdb;

CREATE TABLE `admin` (
  `login_id` smallint(4) unsigned NOT NULL AUTO_INCREMENT,
  `login_name` varchar(30) NOT NULL,
  `passwrd` char(56) NOT NULL, /* encrypted password */
  `is_admin` tinyint(1) DEFAULT '0',
  `first_name` varchar(30) NOT NULL,
  `last_name` varchar(30) NOT NULL,
  `phone` varchar(25) DEFAULT NULL,
  `email` varchar(75) DEFAULT NULL,
  `creation_date` datetime DEFAULT CURRENT_TIMESTAMP(),
  `lastaccess_date` datetime ON UPDATE current_timestamp(),
  PRIMARY KEY (`login_id`),
  UNIQUE KEY `name_UNIQUE` (`login_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 
 CREATE TABLE `budget` (
  `budget_no` int unsigned NOT NULL,
  `description` varchar(75) NOT NULL,
  PRIMARY KEY (`budget_no`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `building` (
  `bldg_id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `abbr` varchar(4) NOT NULL,
  `name` varchar(75) NOT NULL,
  PRIMARY KEY (`bldg_id`),
  UNIQUE KEY `abbr_UNIQUE` (`abbr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `category` (
  `catg_id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(25) NOT NULL,
  PRIMARY KEY (`catg_id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `dept` (
  `dept_id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `abbr` varchar(25) NOT NULL,
  `name` varchar(75) NOT NULL,
  PRIMARY KEY (`dept_id`),
  UNIQUE KEY `abbr_UNIQUE` (`abbr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `user` (
  `user_id` int unsigned NOT NULL,
  `email` varchar(75) NOT NULL,
  `first_name` varchar(30) NOT NULL,
  `last_name` varchar(30) NOT NULL,
  `phone` varchar(25) DEFAULT NULL,
   `bldg_id` smallint unsigned NOT NULL,
  `room_no` varchar(10) NOT NULL,
  `is_owner` tinyint(1) DEFAULT '0',
  `is_purchaser` tinyint(1) DEFAULT '0',
  `is_exp_authority` tinyint(1) DEFAULT '0',
  `is_faculty` tinyint(1) DEFAULT '0',
  `is_staff` tinyint(1) DEFAULT '0',
  `is_student` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  KEY `isOwner` (`user_id`,`is_owner`),
  KEY `isPurchaser` (`user_id`,`is_purchaser`),
  KEY `isExpAuth` (`user_id`,`is_exp_authority`),
  KEY `isFaculty` (`user_id`,`is_faculty`),
  KEY `fk_bldg_id_idx` (`bldg_id`),
  CONSTRAINT `fk_user_bldg` FOREIGN KEY (`bldg_id`) REFERENCES `building` (`bldg_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `vendor` (
  `vendor_id` smallint(4) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(30) NOT NULL,
  `address` varchar(200) NOT NULL,
  `phone` varchar(25) DEFAULT NULL,
  `fax` varchar(25) DEFAULT NULL,
  `website` varchar(100) NOT NULL,
  PRIMARY KEY (`vendor_id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `orders` (
  `order_id` int unsigned NOT NULL,
  `description` varchar(125) DEFAULT NULL,
  `date` date NOT NULL,
  `vendor_id` smallint unsigned NOT NULL,
  `ship_mode` tinyint unsigned NOT NULL DEFAULT '0',
  `order_status` tinyint unsigned NOT NULL DEFAULT '0',
  `budget_no` int unsigned NOT NULL,
  `purchaser_id` int unsigned NOT NULL,
  `owner_id` int unsigned NOT NULL,
  `dept_id` smallint  unsigned NOT NULL,
  `payment_type` tinyint unsigned NOT NULL DEFAULT '0',
  `sign_faculty_id` int unsigned NOT NULL,
  `sign_auth_id` int unsigned NOT NULL,
  `total_amt` decimal(9,2) NOT NULL,
  `total_qty` smallint unsigned NOT NULL DEFAULT '0',
  `total_qty_recv` smallint unsigned NOT NULL DEFAULT '0',
  `lineitem_count` tinyint NOT NULL,
  `is_delivered` tinyint(1) NOT NULL DEFAULT '0',
   `comment` varchar(250) DEFAULT NULL,
  `bt` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`order_id`),
  KEY `vendor_id_idx` (`vendor_id`),
  KEY `fk_dept_id_idx` (`dept_id`),
  KEY `fk_budget_no_idx` (`budget_no`),
  KEY `fk_owner_idx` (`owner_id`,`bt`),
  KEY `fk_purchaser_idx` (`purchaser_id`,`bt`),
  KEY `fk_auth_id_idx` (`sign_auth_id`,`bt`),
  KEY `fk_faculty_id_idx` (`sign_faculty_id`,`bt`),
  CONSTRAINT `fk_auth_id` FOREIGN KEY (`sign_auth_id`, `bt`) REFERENCES `user` (`user_id`, `is_exp_authority`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_budget_no` FOREIGN KEY (`budget_no`) REFERENCES `budget` (`budget_no`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_dept_id` FOREIGN KEY (`dept_id`) REFERENCES `dept` (`dept_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_faculty_id` FOREIGN KEY (`sign_faculty_id`, `bt`) REFERENCES `user` (`user_id`, `is_faculty`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_owner_id` FOREIGN KEY (`owner_id`, `bt`) REFERENCES `user` (`user_id`, `is_owner`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_purchaser_id` FOREIGN KEY (`purchaser_id`, `bt`) REFERENCES `user` (`user_id`, `is_purchaser`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_vendor_id` FOREIGN KEY (`vendor_id`) REFERENCES `vendor` (`vendor_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `lineitem` (
  `order_id` int unsigned NOT NULL,
  `line_no` tinyint unsigned NOT NULL,
  `catelog_no` varchar(25) NOT NULL,
  `description` varchar(75) NOT NULL,
  `qty` smallint unsigned NOT NULL,
  `qty_recv` smallint unsigned NOT NULL DEFAULT '0',
  `unit` smallint unsigned NOT NULL DEFAULT '1',
  `unit_price` decimal(9,2) NOT NULL,
  `total_amt` decimal(9,2) NOT NULL,
  `is_delivered` tinyint(1) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`order_id`,`line_no`),
  CONSTRAINT `fk_order_id` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `inventory` (
  `inv_id` int unsigned NOT NULL,
  `description` varchar(75) DEFAULT NULL,
  `catg_id` smallint unsigned NOT NULL,
  `inv_status` tinyint unsigned NOT NULL DEFAULT '0',
  `bldg_id` smallint unsigned NOT NULL,
  `room_no` varchar(10) NOT NULL,
  `owner_id` int unsigned NOT NULL,
  `order_id` int unsigned NOT NULL,
  `line_no` tinyint unsigned NOT NULL,
  `vendor_id` smallint unsigned NOT NULL,
  `dept_id` smallint unsigned NOT NULL,
  `delivery_date` date NOT NULL,
  `purchase_date` date NOT NULL,
  `purchase_price` decimal(9,2) NOT NULL,
  `manf` varchar(45) DEFAULT NULL,
  `trans_no` varchar(45) DEFAULT NULL,
  `serial_no` varchar(45) DEFAULT NULL,
  `model_no` varchar(45) DEFAULT NULL,
  `comment` varchar(250) DEFAULT NULL,
  `bt` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`inv_id`),
  UNIQUE KEY `inv_id_UNIQUE` (`inv_id`),
  KEY `fk_catg_id_idx` (`catg_id`),
  KEY `fk_bldg_id_idx` (`bldg_id`),
  KEY `fk_owner_id_idx` (`owner_id`,`bt`),
  KEY `fk_lineitem_idx` (`order_id`,`line_no`),
  KEY `fk_vendor_id_idx` (`vendor_id`),
  KEY `fk_dept_id_idx` (`dept_id`),
  CONSTRAINT `fk_inv_bldg` FOREIGN KEY (`bldg_id`) REFERENCES `building` (`bldg_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_inv_catg` FOREIGN KEY (`catg_id`) REFERENCES `category` (`catg_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_inv_dept` FOREIGN KEY (`dept_id`) REFERENCES `dept` (`dept_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_inv_lineitem` FOREIGN KEY (`order_id`, `line_no`) REFERENCES `lineitem` (`order_id`, `line_no`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_inv_user_owner` FOREIGN KEY (`owner_id`, `bt`) REFERENCES `user` (`user_id`, `is_owner`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_inv_vendor` FOREIGN KEY (`vendor_id`) REFERENCES `vendor` (`vendor_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

