CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_user`(p_userId Varchar(45))
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
    WHERE UserID=p_userId;
END