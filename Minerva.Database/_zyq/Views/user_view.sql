CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `users_view` AS
    SELECT 
        `_users`.`userId` AS `UserID`,
        `_users`.`tenantId` AS `TenantID`,
        `_users`.`userName` AS `UserName`,
        `_users`.`email` AS `Email`,
        `_users`.`isActive` AS `isActive`,
        `_users`.`isDeleted` AS `isDeleted`,
        `_users`.`createTime` AS `CreateTime`,
        `_users`.`modifiedTime` AS `ModifiedTime`,
        `_users`.`createdBy` AS `CreatedBy`,
        `_users`.`modifiedBy` AS `ModifiedBy`,
        `_users`.`phoneNumber` AS `PhoneNumber`,
        `_users`.`notificationsEnabled` AS `NotificationsEnabled`,
        `_users`.`mfaEnabled` AS `MFAEnabled`,
        `_users`.`isTenantUser` AS `isTenantUser`,
        `_users`.`isAdminUser` AS `isAdminUser`,
        `_users`.`FirstName` AS `FirstName`,
        `_users`.`LastName` AS `LastName`,
        `_users`.`Roles` AS `Roles`
    FROM
        `_users`