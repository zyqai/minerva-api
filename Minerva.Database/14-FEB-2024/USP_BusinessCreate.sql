USE `minerva`;
DROP procedure IF EXISTS `USP_BusinessCreate`;

USE `minerva`;
DROP procedure IF EXISTS `minerva`.`USP_BusinessCreate`;
;

DELIMITER $$
USE `minerva`$$
CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `USP_BusinessCreate`(
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
     IN in_createBy VARCHAR(45),
      OUT p_last_insert_id INT 
)
BEGIN
    INSERT INTO `_businesses` (`businessId`,
        `tenantId`, `businessName`, `businessAddress`, `businessType`,
        `industry`, `annualRevenue`, `incorporationDate`,
        `businessRegistrationNumber`, `rootDocumentFolder`, `businessAddress1`,`createBy`
    ) VALUES (in_businessId,
        in_tenantId, in_businessName, in_businessAddress, in_businessType,
        in_industry, in_annualRevenue, in_incorporationDate,
        in_businessRegistrationNumber, in_rootDocumentFolder, in_businessAddress1,in_createBy
    );
     SET p_last_insert_id := LAST_INSERT_ID();
END$$

DELIMITER ;
;




