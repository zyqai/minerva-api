ALTER TABLE `_projects` DROP FOREIGN KEY `Assignrdstaff_FK`;
SET foreign_key_checks = 0;
DROP TABLE `_users`;
SET foreign_key_checks = 1;

CREATE TABLE `_users` (
  `userId` VARCHAR(36) NOT NULL,
  `tenantId` INT DEFAULT NULL,
  `userName` VARCHAR(16) NOT NULL,
  `email` VARCHAR(255) NOT NULL,
  `isActive` TINYINT DEFAULT 1,
  `isDeleted` TINYINT DEFAULT 0,
  `createTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
  `modifiedTime` TIMESTAMP NULL DEFAULT NULL,
  `createdBy` VARCHAR(50) DEFAULT NULL,
  `modifiedBy` VARCHAR(50) DEFAULT NULL,
  `phoneNumber` VARCHAR(45) DEFAULT NULL,
  `notificationsEnabled` TINYINT DEFAULT NULL,
  `mfaEnabled` TINYINT DEFAULT NULL,
  `isTenantUser` INT DEFAULT NULL,
  `isAdminUser` INT DEFAULT NULL,
  PRIMARY KEY (`userId`),
  KEY `userTenantId_idx` (`tenantId`),
  CONSTRAINT `userTenantId` FOREIGN KEY (`tenantId`) REFERENCES `_tenants` (`tenantId`)
);







