DELIMITER //

CREATE PROCEDURE USP_TenantDelete(IN p_tenantId INT)
BEGIN
    DELETE FROM _tenants WHERE tenantId = p_tenantId;
END //

DELIMITER ;
