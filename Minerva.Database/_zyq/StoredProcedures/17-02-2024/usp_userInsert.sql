CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_userInsert`(

    IN in_tenantId INT,
    IN in_userName VARCHAR(255),
    IN in_email VARCHAR(255),
    IN in_isActive TINYINT,
    IN in_isDeleted TINYINT,
    IN in_createdBy VARCHAR(45),
	IN in_phoneNumber VARCHAR(45),
    IN in_notificationsEnabled TINYINT,
    IN in_mfaEnabled TINYINT,
    IN in_isTenantUser INT,
    IN in_isAdminUser INT,
    IN in_FirstName VARCHAR(100),
    IN in_LastName VARCHAR(100),
    IN in_Roles VARCHAR(100),
    OUT p_last_insert_id VARCHAR(45)
)
BEGIN
DECLARE p_userId VARCHAR(50);
SET p_userId = UUID();
    INSERT INTO `_users` (
        userId,
        tenantId,
        userName,
        email,
        isActive,
        isDeleted,
        createTime,
        createdBy,
        phoneNumber,
        notificationsEnabled,
        mfaEnabled,
        isTenantUser,
        isAdminUser,
        FirstName,
        LastName,
        Roles
    ) VALUES (
        p_userId,
        in_tenantId,
        in_userName,
        in_email,
        in_isActive,
        in_isDeleted,
        now(),
        in_createdBy,
        in_phoneNumber,
        in_notificationsEnabled,
        in_mfaEnabled,
        in_isTenantUser,
        in_isAdminUser,
        in_FirstName,
        in_LastName,
        in_Roles
    );
    SET p_last_insert_id := p_userId;
END