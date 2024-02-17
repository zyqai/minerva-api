CREATE DEFINER=`root`@`localhost` PROCEDURE `USP_GetUserByUserName`(p_userName Varchar(255))
BEGIN
    SELECT 
        UserID,
        TenantID,
        UserName,
        Email,
        isActive,
        isDeleted,
        CreateTime,
        ModifiedTime,
        CreatedBy,
        ModifiedBy,
        PhoneNumber,
        NotificationsEnabled,
        MFAEnabled,
        isTenantUser,
        isAdminUser,
        FirstName,
        LastName,
        Roles
    FROM users_view
    WHERE UserName=p_userName;
END