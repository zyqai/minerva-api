CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_userUpdate`(
    IN in_userId VARCHAR(45),
    IN in_tenantId INT,
    IN in_userName VARCHAR(16),
    IN in_email VARCHAR(255),
    IN in_isActive TINYINT,
    IN in_isDeleted TINYINT,
    IN in_modifiedTime TIMESTAMP,
    IN in_modifiedBy VARCHAR(45),
    IN in_phoneNumber VARCHAR(45),
    IN in_notificationsEnabled TINYINT,
    IN in_mfaEnabled TINYINT,
    IN in_isTenantUser INT,
    IN in_isAdminUser INT,
    IN in_FirstName VARCHAR(100),
    IN in_LastName VARCHAR(100),
    IN in_Roles VARCHAR(100)
)
BEGIN
    UPDATE `_users`
    SET
        tenantId = in_tenantId,
        userName = in_userName,
        email = in_email,
        isActive = in_isActive,
        isDeleted = in_isDeleted,
        modifiedTime = in_modifiedTime,
        modifiedBy = in_modifiedBy,
        phoneNumber = in_phoneNumber,
        notificationsEnabled = in_notificationsEnabled,
        mfaEnabled = in_mfaEnabled,
        isTenantUser = in_isTenantUser,
        isAdminUser = in_isAdminUser,
        FirstName = in_FirstName,
        LastName = in_LastName,
        Roles = in_Roles
    WHERE userId = in_userId;
END