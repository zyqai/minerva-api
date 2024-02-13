ALTER TABLE `minerva`.`_clients` 
CHANGE COLUMN `createdBy` `createdBy` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `modifiedBy` `modifiedBy` VARCHAR(50) NULL DEFAULT NULL ;