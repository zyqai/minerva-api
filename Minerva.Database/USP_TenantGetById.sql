DELIMITER //

CREATE PROCEDURE USP_TenantGetById(IN p_tenantId INT)
BEGIN
    SELECT * FROM _tenants WHERE tenantId = p_tenantId;
END //

DELIMITER ;
