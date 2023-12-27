DELIMITER //
CREATE PROCEDURE `USP_CreateUser`(
    IN p_userId VARCHAR(45),
    IN p_tenantId INT,
    IN p_userName VARCHAR(16),
    IN p_email VARCHAR(255),
    IN p_isActive TINYINT,
    IN p_isDeleted TINYINT,
    IN p_createdBy VARCHAR(100),
    IN p_phoneNumber VARCHAR(45),
    IN p_notificationsEnabled TINYINT,
    IN p_mfaEnabled TINYINT,
    IN p_isTenantUser INT,
    IN p_isAdminUser INT
)
BEGIN
    INSERT INTO `_users` (
        `userId`, `tenantId`, `userName`, `email`, `isActive`, `isDeleted`,
        `createdBy`, `phoneNumber`, `notificationsEnabled`,
        `mfaEnabled`, `isTenantUser`, `isAdminUser`
    ) VALUES (
        p_userId, p_tenantId, p_userName, p_email, p_isActive, p_isDeleted,
        p_createdBy, p_phoneNumber, p_notificationsEnabled,
        p_mfaEnabled, p_isTenantUser, p_isAdminUser
    );
END//
DELIMITER ;