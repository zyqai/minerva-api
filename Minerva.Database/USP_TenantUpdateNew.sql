DELIMITER //

CREATE PROCEDURE USP_TenantUpdate(
    IN p_tenantId INT,
    IN p_tenantName VARCHAR(255),
    IN p_tenantDomain VARCHAR(255),
    IN p_tenantLogoPath VARCHAR(255),
    IN p_tenantAddress VARCHAR(1000),
    IN p_tenantPhone VARCHAR(45),
    IN p_tenantContactName VARCHAR(45),
    IN p_tenantContactEmail VARCHAR(100)
)
BEGIN
    UPDATE _tenants
    SET
        tenantName = p_tenantName,
        tenantDomain = p_tenantDomain,
        tenantLogoPath = p_tenantLogoPath,
        tenantAddress = p_tenantAddress,
        tenantPhone = p_tenantPhone,
        tenantContactName = p_tenantContactName,
        tenantContactEmail = p_tenantContactEmail
    WHERE
        tenantId = p_tenantId;
END //

DELIMITER ;
