CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `projectpeoplerelation_view` AS
    SELECT 
        `a`.`projectId` AS `ProjectId`,
        `b`.`peopleId` AS `peopleId`,
        `b`.`userId` AS `userId`,
        `b`.`tenantId` AS `tenantId`,
        `b`.`clientName` AS `clientName`,
        `b`.`clientAddress` AS `clientAddress`,
        `b`.`phoneNumber` AS `phoneNumber`,
        `b`.`email` AS `email`,
        `b`.`preferredContact` AS `preferredContact`,
        `b`.`firstName` AS `firstName`,
        `b`.`lastName` AS `lastName`,
        `a`.`primaryBorrower` AS `primaryBorrower`,
        `c`.`personaAutoId` AS `personaAutoId`,
        `a`.`projectPeopleId` AS `projectPeopleId`,
        `c`.`personaId` AS `personaId`,
        `c`.`personaName` AS `personaName`,
        `c`.`projectPersona` AS `projectPersona`
    FROM
        ((`_projectpeople` `a`
        JOIN `_people` `b` ON ((`a`.`peopleId` = `b`.`peopleId`)))
        JOIN `_personas` `c` ON ((`a`.`personaAutoId` = `c`.`personaAutoId`)))