CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_users`()
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
    FROM users_view;
END