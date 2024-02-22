ALTER TABLE `_zyq`.`_people` 
ADD COLUMN `firstName` VARCHAR(250) NULL DEFAULT NULL AFTER `modifiedBy`,
ADD COLUMN `lastName` VARCHAR(250) NULL DEFAULT NULL AFTER `firstName`,
ADD COLUMN `dob` DATE NULL DEFAULT NULL AFTER `lastName`,
ADD COLUMN `socialsecuritynumber` VARCHAR(10) NULL DEFAULT NULL AFTER `dob`,
ADD COLUMN `postalnumber` VARCHAR(10) NULL DEFAULT NULL AFTER `socialsecuritynumber`,
ADD COLUMN `stateid` INT NULL DEFAULT NULL AFTER `postalnumber`,
ADD COLUMN `City` VARCHAR(45) NULL DEFAULT NULL AFTER `stateid`,
ADD COLUMN `clientAddress1` VARCHAR(250) NULL AFTER `City`,
ADD INDEX `stateStateId_idx` (`stateid` ASC) VISIBLE;
;
ALTER TABLE `_zyq`.`_people` 
ADD CONSTRAINT `stateStateId`
  FOREIGN KEY (`stateid`)
  REFERENCES `_zyq`.`_statuses` (`statusId`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
