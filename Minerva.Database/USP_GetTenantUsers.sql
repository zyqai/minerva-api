
DROP PROCEDURE `USP_GetTenantUsers`
DELIMITER //

CREATE PROCEDURE USP_GetTenantUsers(
    IN in_tenantId INT
)
BEGIN
    -- Select specific columns from the _users table based on tenantId
    SELECT 
        `userId`, -- Unique identifier for the user
        `tenantId`, -- Unique inentifier for the tenant
        `userName`, -- Username of the user
        `email`, -- Email address of the user
        `isActive`, -- Flag indicating if the user is active
        `isDeleted`, -- Flag indicating if the user is deleted
        `createTime`, -- Timestamp of when the user was created
        `modifiedTime`, -- Timestamp of when the user was last modified
        `createdBy`, -- Creator of the user
        `modifiedBy`, -- Last modifier of the user
        `phoneNumber`, -- Phone number of the user
        `notificationsEnabled`, -- Flag indicating if notifications are enabled for the user
        `mfaEnabled`, -- Flag indicating if multi-factor authentication is enabled for the user
        `isTenantUser`, -- Flag indicating if the user is associated with a tenant
        `isAdminUser`, -- Flag indicating if the user is an admin user
        `FirstName`, -- First name of the user
        `LastName`, -- Last name of the user
        `Roles` -- Roles assigned to the user
    FROM `_users`
    WHERE `tenantId` = in_tenantId; -- Filter by the provided tenantId
END//

DELIMITER ;
