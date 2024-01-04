DELIMITER //
Create PROCEDURE `USP_GetUserById` (
				IN p_userId VARCHAR(45)
)
BEGIN
    SELECT * FROM `_users`  a 
	Left Join `_tenants` b on a.tenantId=b.tenantId
    WHERE `userId` = p_userId;
END//
DELIMITER ;