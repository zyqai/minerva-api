USE `minerva`;
DROP procedure IF EXISTS `USP_TenantCreate`;

USE `minerva`;
DROP procedure IF EXISTS `minerva`.`USP_TenantCreate`;
;

DELIMITER $$
USE `minerva`$$
CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `USP_TenantCreate`(
    IN p_tenantName VARCHAR(255),
    IN p_tenantDomain VARCHAR(255),
    IN p_tenantLogoPath VARCHAR(255),
    IN p_tenantAddress VARCHAR(1000),
    IN p_tenantAddress1 VARCHAR(1000),
    IN p_tenantPhone VARCHAR(45),
    IN p_tenantContactName VARCHAR(45),
    IN p_tenantContactEmail VARCHAR(100),
    IN p_postalCode VARCHAR(45),
    IN p_city VARCHAR(100),
    IN p_stateId INT,
    IN p_createdBy Varchar(45),
     OUT p_last_insert_id INT 
)
BEGIN
    INSERT INTO `_tenants` (
        `tenantName`, `tenantDomain`, `tenantLogoPath`,
        `tenantAddress`, `tenantAddress1`, `tenantPhone`,
        `tenantContactName`, `tenantContactEmail`, `postalCode`,
        `city`, `stateid`, `createDateTime`,`createdBy`
    )
    VALUES (
        p_tenantName, p_tenantDomain, p_tenantLogoPath,
        p_tenantAddress, p_tenantAddress1, p_tenantPhone,
        p_tenantContactName, p_tenantContactEmail, p_postalCode,
        p_city, p_stateId, CURRENT_TIMESTAMP,p_createdBy
    );
    SET p_last_insert_id := LAST_INSERT_ID();
END$$

DELIMITER ;
;

