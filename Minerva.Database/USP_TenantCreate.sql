

DELIMITER //
CREATE PROCEDURE `USP_TenantCreate`(
    IN p_tenantName VARCHAR(255),
    IN p_tenantDomain VARCHAR(255),
    IN p_tenantLogoPath VARCHAR(255),
    IN p_tenantAddress VARCHAR(1000),
    IN p_tenentAddress1 VARCHAR(1000),
    IN p_tenantPhone VARCHAR(45),
    IN p_tenantContactName VARCHAR(45),
    IN p_tenantContactEmail VARCHAR(100),
    IN p_postalCode VARCHAR(45),
    IN p_city VARCHAR(100),
    IN p_stateId INT,
    OUT p_last_insert_id INT 
)
BEGIN
    INSERT INTO `_tenants` (
        `tenantName`, `tenantDomain`, `tenantLogoPath`,
        `tenantAddress`, `tenentAddress1`, `tenantPhone`,
        `tenantContactName`, `tenantContactEmail`, `postalCode`,
        `city`, `stateid`, `createDateTime`
    )
    VALUES (
        p_tenantName, p_tenantDomain, p_tenantLogoPath,
        p_tenantAddress, p_tenentAddress1, p_tenantPhone,
        p_tenantContactName, p_tenantContactEmail, p_postalCode,
        p_city, p_stateId, CURRENT_TIMESTAMP
    );
    SET p_last_insert_id := LAST_INSERT_ID();
END//
DELIMITER ;