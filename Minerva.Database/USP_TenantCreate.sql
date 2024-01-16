DELIMITER //

CREATE PROCEDURE USP_TenantCreate(
    IN p_tenantName VARCHAR(255),
    IN p_tenantDomain VARCHAR(255),
    IN p_tenantLogoPath VARCHAR(255),
    IN p_tenantAddress VARCHAR(1000),
    IN p_tenantPhone VARCHAR(45),
    IN p_tenantContactName VARCHAR(45),
    IN p_tenantContactEmail VARCHAR(100)
)
BEGIN
    INSERT INTO _tenants (
        tenantName,
        tenantDomain,
        tenantLogoPath,
        tenantAddress,
        tenantPhone,
        tenantContactName,
        tenantContactEmail
    ) VALUES (
        p_tenantName,
        p_tenantDomain,
        p_tenantLogoPath,
        p_tenantAddress,
        p_tenantPhone,
        p_tenantContactName,
        p_tenantContactEmail
    );
END //

DELIMITER ;