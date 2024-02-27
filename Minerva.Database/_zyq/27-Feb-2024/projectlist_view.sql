CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `projectlist_view` AS
    SELECT 
        `a`.`projectId` AS `projectId`,
        `a`.`tenantId` AS `tenantId`,
        `a`.`projectName` AS `projectName`,
        `a`.`projectDescription` AS `projectDescription`,
        `a`.`industryId` AS `industryId`,
        `a`.`amount` AS `amount`,
        `a`.`purpose` AS `purpose`,
        `a`.`loanTypeAutoId` AS `loanTypeAutoId`,
        `a`.`statusAutoId` AS `statusAutoId`,
        `b`.`industrySectorAutoId` AS `industrySectorAutoId`,
        `b`.`industrySector` AS `industrySector`,
        `b`.`industryDescription` AS `industryDescription`,
        `c`.`userId` AS `assignedToUserId`,
        `c`.`userName` AS `assignedTousername`,
        `c`.`email` AS `assignedToemail`,
        `c`.`FirstName` AS `assignedToName`,
        `e`.`statusId` AS `statusId`,
        `e`.`statusName` AS `statusName`,
        `e`.`statusDescription` AS `statusDescription`,
        `d`.`loanType` AS `loanType`,
        `d`.`loanTypeDescription` AS `loanTypeDescription`
    FROM
        ((((`_projects` `a`
        JOIN `_industrysectors` `b` ON ((`a`.`industryId` = `b`.`industryId`)))
        JOIN `_users` `c` ON ((`a`.`assignedToUserId` = `c`.`userId`)))
        JOIN `_loantypes` `d` ON ((`a`.`loanTypeAutoId` = `d`.`loanTypeAutoId`)))
        JOIN `_statuses` `e` ON ((`a`.`statusAutoId` = `e`.`statusAutoId`)))