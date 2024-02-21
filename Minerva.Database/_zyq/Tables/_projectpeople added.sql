

-- Drop the foreign key constraint
ALTER TABLE `_projectpeople` DROP FOREIGN KEY `projectpeoplePeopleId`;
-- Drop the foreign key constraint
ALTER TABLE `_peoplebusinessrelation` DROP FOREIGN KEY `peoplebusinessPeopleId`;

-- Modify the `_people` table
ALTER TABLE `_people`
MODIFY COLUMN `peopleId` INT NOT NULL AUTO_INCREMENT FIRST, 
AUTO_INCREMENT = 1;

-- Recreate the foreign key constraint
ALTER TABLE `_projectpeople`
ADD CONSTRAINT `projectpeoplePeopleId` FOREIGN KEY (`peopleId`) REFERENCES `_people` (`peopleId`);

-- Recreate the foreign key constraint
ALTER TABLE `_peoplebusinessrelation`
ADD CONSTRAINT `peoplebusinessPeopleId` FOREIGN KEY (`peopleId`) REFERENCES `_people` (`peopleId`);

