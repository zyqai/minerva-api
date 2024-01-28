DELIMITER //
CREATE  PROCEDURE `USP_GetUserByUserName`(
				IN p_userName VARCHAR(255)
)
BEGIN
    SELECT * FROM `_users`  a 
	Left Join `_tenants` b on a.tenantId=b.tenantId
    WHERE `username` = p_userName;
END//
DELIMITER ;