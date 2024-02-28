CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `notes_view` AS
    SELECT 
        `a`.`projectNotesId` AS `projectNotesId`,
        `a`.`projectId` AS `projectId`,
        `a`.`tenantId` AS `tenantId`,
        `a`.`notes` AS `notes`,
        `a`.`createdByUserId` AS `createdByUserId`,
        `a`.`createdOn` AS `createdOn`,
        `b`.`FirstName` AS `firstname`
    FROM
        (`_projectnotes` `a`
        JOIN `_users` `b` ON ((`a`.`createdByUserId` = `b`.`userId`)))