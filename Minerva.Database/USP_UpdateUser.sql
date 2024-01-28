
    DELIMITER //
    CREATE  PROCEDURE `USP_UpdateUser`(
    IN p_userId VARCHAR(255),
    IN p_userName VARCHAR(255),
    IN p_email VARCHAR(255),
    IN p_isActive TINYINT,
    IN p_modifiedBy VARCHAR(100),
    IN p_phoneNumber VARCHAR(45),
    IN p_notificationsEnabled TINYINT,
    IN p_mfaEnabled TINYINT,
    IN p_isTenantUser INT,
    IN p_isAdminUser INT,
    IN p_tenantId INT,
    IN p_isDeleted TINYINT
)
BEGIN
    UPDATE `_users`
    SET
        `userName` = p_userName,
        `email` = p_email,
        `isActive` = p_isActive,
        `modifiedBy` = p_modifiedBy,
        `phoneNumber` = p_phoneNumber,
        `notificationsEnabled` = p_notificationsEnabled,
        `mfaEnabled` = p_mfaEnabled,
        `isTenantUser` = p_isTenantUser,
        `isAdminUser` = p_isAdminUser,
        `TenantId` = p_tenantId,
        `modifiedTime` = current_timestamp(),
        `isDeleted` = p_isDeleted
    WHERE
        `userId` = p_userId;
END//
DELIMITER ;