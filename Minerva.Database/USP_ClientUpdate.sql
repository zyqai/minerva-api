

USE `minerva`;
DROP procedure IF EXISTS `minerva`.`USP_ClientUpdate`;
;

DELIMITER $$
USE `minerva`$$
/*
Author: [Santhosh]
Created Date: [2024-02-01]
Purpose: This procedure updates client information in the '_clients' table.
Modified Date: [2024-02-13]  modfied by chaged int to varchar
*/

CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `USP_ClientUpdate`(
    IN p_clientId INT,
    IN p_userId VARCHAR(45),
    IN p_tenantId INT,
    IN p_clientName VARCHAR(255),
    IN p_firstName VARCHAR(45),
    IN p_lastName VARCHAR(45),
    IN p_dob DATETIME,
    IN p_socialsecuritynumber VARCHAR(45),
    IN p_postalnumber VARCHAR(10),
    IN p_stateid INT,
    IN p_clientAddress VARCHAR(255),
    IN p_phoneNumber VARCHAR(45),
    IN p_email VARCHAR(255),
    IN p_preferredContact VARCHAR(45),
    IN p_creditScore VARCHAR(45),
    IN p_lendabilityScore VARCHAR(45),
    IN p_marriedStatus INT,
    IN p_spouseClientId INT,
    IN p_rootFolder VARCHAR(255),
    IN p_modifiedBy varchar(50),
    IN p_City VARCHAR(100),
	IN p_clientAddress1 VARCHAR(255)
)
BEGIN
    UPDATE `_clients`
    SET
        `userId` = p_userId,
        `tenantId` = p_tenantId,
        `clientName` = p_clientName,
        `firstName` = p_firstName,
        `lastName` = p_lastName,
        `dob` = p_dob,
        `socialsecuritynumber` = p_socialsecuritynumber,
        `postalnumber` = p_postalnumber,
        `stateid` = p_stateid,
        `clientAddress` = p_clientAddress,
        `phoneNumber` = p_phoneNumber,
        `email` = p_email,
        `preferredContact` = p_preferredContact,
        `creditScore` = p_creditScore,
        `lendabilityScore` = p_lendabilityScore,
        `marriedStatus` = p_marriedStatus,
        `spouseClientId` = p_spouseClientId,
        `rootFolder` = p_rootFolder,
        `modifiedTime` = NOW(),
        `modifiedBy` = p_modifiedBy,
        `city`=p_city,
        `clientAddress1` = p_clientAddress1 
	WHERE `clientId` = p_clientId;
END$$

DELIMITER ;
;

