CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `_zyq`.`projectrequeststable_view` AS
    SELECT 
        `_zyq`.`_projectrequests`.`projectRequestId` AS `projectRequestId`,
        `_zyq`.`_projectrequests`.`projectId` AS `projectId`,
        `_zyq`.`_projectrequests`.`tenantId` AS `tenantId`,
        `_zyq`.`_projectrequests`.`remindersAutoId` AS `remindersAutoId`,
        `_zyq`.`_projectrequests`.`projectRequestName` AS `projectRequestName`,
        `_zyq`.`_projectrequests`.`projectRequestDescription` AS `projectRequestDescription`,
        `_zyq`.`_projectrequests`.`createdOn` AS `createdOn`,
        `_zyq`.`_projectrequests`.`createdBy` AS `createdBy`,
        `_zyq`.`_projectrequests`.`modifiedOn` AS `modifiedOn`,
        `_zyq`.`_projectrequests`.`modifiedBy` AS `modifiedBy`
    FROM
        `_zyq`.`_projectrequests`