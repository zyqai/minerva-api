CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `_viewbusinesspersonpersona` AS
    SELECT 
        `b`.`peopleId` AS `clientId`,
        `b`.`clientName` AS `clientName`,
        `b`.`userId` AS `userId`,
        `b`.`tenantId` AS `tenantId`,
        `b`.`firstName` AS `firstName`,
        `b`.`lastName` AS `lastName`,
        `b`.`email` AS `email`,
        `b`.`phoneNumber` AS `phoneNumber`,
        `c`.`personaAutoId` AS `personaId`,
        `c`.`personaName` AS `personaName`,
        `b`.`stateid` AS `stateid`,
        `a`.`businessId` AS `businessId`,
        `a`.`peopleBusinessId` AS `clientBusinessId`
    FROM
        ((`_peoplebusinessrelation` `a`
        JOIN `_people` `b` ON ((`a`.`peopleId` = `b`.`peopleId`)))
        JOIN `_personas` `c` ON ((`a`.`personaAutoId` = `c`.`personaAutoId`)))