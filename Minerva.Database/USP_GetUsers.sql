DELIMITER //
CREATE PROCEDURE `USP_GetUsers`()
BEGIN
    SELECT * FROM `_users` a
    Left Join `_tenants` b on a.tenantId=b.tenantId
    where a.isActive=1;
END//
DELIMITER ;
