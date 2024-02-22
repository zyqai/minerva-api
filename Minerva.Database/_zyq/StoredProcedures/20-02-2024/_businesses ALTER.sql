


ALTER TABLE `_zyq`.`_peoplebusinessrelation` DROP FOREIGN KEY `peoplebusinessBusinessId`;

-- Drop the foreign key constraint
ALTER TABLE `_zyq`.`_projectbusinesses` DROP FOREIGN KEY `projectbusinessBusinessId`;

-- Alter the column in `_businesses` table
ALTER TABLE `_zyq`.`_businesses`
CHANGE COLUMN `businessId` `businessId` INT AUTO_INCREMENT NOT NULL;

-- Recreate the foreign key constraint
ALTER TABLE `_zyq`.`_projectbusinesses`
ADD CONSTRAINT `projectbusinessBusinessId` FOREIGN KEY (`businessId`) REFERENCES `_zyq`.`_businesses` (`businessId`);

ALTER TABLE `_zyq`.`_businesses`
CHANGE COLUMN `businessId` `businessId` INT AUTO_INCREMENT NOT NULL;

ALTER TABLE `_zyq`.`_peoplebusinessrelation`
ADD CONSTRAINT `peoplebusinessBusinessId` FOREIGN KEY (`businessId`) REFERENCES `_zyq`.`_businesses` (`businessId`);





ALTER TABLE `_zyq`.`_businesses`
AUTO_INCREMENT = 1;

