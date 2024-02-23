USE `minerva`;
DROP procedure IF EXISTS `USP_BusinessUpdate`;

USE `minerva`;
DROP procedure IF EXISTS `minerva`.`USP_BusinessUpdate`;
;

DELIMITER $$
USE `minerva`$$
CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `USP_BusinessUpdate`(
    IN in_businessId INT,
    IN in_tenantId INT,
    IN in_businessName VARCHAR(255),
    IN in_businessAddress VARCHAR(255),
    IN in_businessType VARCHAR(45),
    IN in_industry VARCHAR(45),
    IN in_annualRevenue DECIMAL(6,2),
    IN in_incorporationDate DATE,
    IN in_businessRegistrationNumber VARCHAR(255),
    IN in_rootDocumentFolder VARCHAR(255),
    IN in_businessAddress1 VARCHAR(255),
    IN in_updateBy VARCHAR(45)
)
BEGIN
    UPDATE `_businesses`
    SET
        `tenantId` = in_tenantId,
        `businessName` = in_businessName,
        `businessAddress` = in_businessAddress,
        `businessType` = in_businessType,
        `industry` = in_industry,
        `annualRevenue` = in_annualRevenue,
        `incorporationDate` = in_incorporationDate,
        `businessRegistrationNumber` = in_businessRegistrationNumber,
        `rootDocumentFolder` = in_rootDocumentFolder,
		`businessAddress1`=in_businessAddress1,
        `updateBy`=in_UpdateBy,
        `updateDateTime`=CURRENT_TIMESTAMP
    WHERE
        `businessId` = in_businessId;
END$$

DELIMITER ;
;

