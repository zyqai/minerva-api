ALTER TABLE `_zyq`.`_projectrequests` 
ADD COLUMN `peopleId` INT NULL AFTER `modifiedBy`,
ADD INDEX `projectreqestpeople_idx` (`peopleId` ASC) VISIBLE;
;
ALTER TABLE `_zyq`.`_projectrequests` 
ADD CONSTRAINT `projectreqestpeople`
  FOREIGN KEY (`peopleId`)
  REFERENCES `_zyq`.`_people` (`peopleId`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
