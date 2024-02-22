CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `projects_view` AS
    SELECT 
        `_projects`.`projectId` AS `projectId`,
        `_projects`.`tenantId` AS `tenantId`,
        `_projects`.`projectName` AS `projectName`,
        `_projects`.`projectDescription` AS `projectDescription`,
        `_projects`.`industryId` AS `industryId`,
        `_projects`.`amount` AS `amount`,
        `_projects`.`purpose` AS `purpose`,
        `_projects`.`createdByUserId` AS `createdByUserId`,
        `_projects`.`createdDateTime` AS `createdDateTime`,
        `_projects`.`assignedToUserId` AS `assignedToUserId`,
        `_projects`.`modifiedByUserId` AS `modifiedByUserId`,
        `_projects`.`modifiedDateTime` AS `modifiedDateTime`,
        `_projects`.`loanTypeAutoId` AS `loanTypeAutoId`,
        `_projects`.`statusAutoId` AS `statusAutoId`,
        `_projects`.`projectFilesPath` AS `projectFilesPath`
    FROM
        `_projects`