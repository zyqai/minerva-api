CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `usp_businessespersonasbyclientids` AS
    SELECT 
        `b`.`businessId` AS `businessId`,
        `b`.`businessName` AS `businessName`,
        `b`.`businessType` AS `businessType`,
        `b`.`industry` AS `industry`,
        `a`.`personaId` AS `personaId`,
        `c`.`personaName` AS `personaName`,
        `a`.`clientId` AS `clientId`,
        `b`.`tenantId` AS `tenantId`,
        `b`.`businessAddress` AS `businessAddress`,
        `a`.`clientBusinessId` AS `clientBusinessId`
    FROM
        ((`minerva`.`_clientsbusinessrelation` `a`
        JOIN `minerva`.`_businesses` `b` ON ((`a`.`businessId` = `b`.`businessId`)))
        JOIN `minerva`.`_personas` `c` ON ((`a`.`personaId` = `c`.`personaId`)))