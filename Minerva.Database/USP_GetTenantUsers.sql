DELIMITER //
CREATE PROCEDURE `USP_GetTenantUsers`(
				IN p_tenantId int
)
BEGIN
    SELECT * FROM `_users`  a 
	Left Join `_tenants` b on a.tenantId=b.tenantId
    WHERE a.`tenantid` = p_tenantId;
END//
DELIMITER ;