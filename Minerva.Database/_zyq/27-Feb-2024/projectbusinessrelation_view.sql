CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `projectbusinessrelation_view` AS
    SELECT 
        `a`.`projectBusinessId` AS `projectBusinessId`,
        `a`.`tenantId` AS `tenantId`,
        `a`.`projectId` AS `projectId`,
        `a`.`businessId` AS `businessId`,
        `b`.`businessName` AS `businessName`,
        `b`.`businessType` AS `businessType`
    FROM
        (`_projectbusinesses` `a`
        JOIN `_businesses` `b` ON ((`a`.`businessId` = `b`.`businessId`)))