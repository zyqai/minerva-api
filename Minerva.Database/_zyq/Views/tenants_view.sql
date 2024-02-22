CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `tenants_view` AS
    SELECT 
        `_tenants`.`tenantId` AS `tenantId`,
        `_tenants`.`tenantName` AS `tenantName`,
        `_tenants`.`tenantDescription` AS `tenantDescription`,
        `_tenants`.`tenantDomain` AS `tenantDomain`,
        `_tenants`.`tenantLogoPath` AS `tenantLogoPath`,
        `_tenants`.`tenantAddress` AS `tenantAddress`,
        `_tenants`.`tenantAddress1` AS `tenantAddress1`,
        `_tenants`.`tenantPhone` AS `tenantPhone`,
        `_tenants`.`tenantContactName` AS `tenantContactName`,
        `_tenants`.`tenantContactEmail` AS `tenantContactEmail`,
        `_tenants`.`postalCode` AS `postalCode`,
        `_tenants`.`city` AS `city`,
        `_tenants`.`stateid` AS `stateid`,
        `_tenants`.`createdBy` AS `createdBy`,
        `_tenants`.`createdDateTime` AS `createdDateTime`,
        `_tenants`.`updatedBy` AS `updatedBy`,
        `_tenants`.`updateDateTime` AS `updateDateTime`
    FROM
        `_tenants`