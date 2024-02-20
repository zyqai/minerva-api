CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `businesses_view` AS
    SELECT 
        `_businesses`.`businessId` AS `businessId`,
        `_businesses`.`tenantId` AS `tenantId`,
        `_businesses`.`businessName` AS `businessName`,
        `_businesses`.`businessAddress` AS `businessAddress`,
        `_businesses`.`businessType` AS `businessType`,
        `_businesses`.`industry` AS `industry`,
        `_businesses`.`annualRevenue` AS `annualRevenue`,
        `_businesses`.`incorporationDate` AS `incorporationDate`,
        `_businesses`.`businessRegistrationNumber` AS `businessRegistrationNumber`,
        `_businesses`.`rootDocumentFolder` AS `rootDocumentFolder`,
        `_businesses`.`businessAddress1` AS `businessAddress1`,
        `_businesses`.`createDateTime` AS `CreateDateTime`,
        `_businesses`.`createdBy` AS `createdBy`,
        `_businesses`.`modifiedDateTime` AS `modifiedDateTime`,
        `_businesses`.`modifiedBy` AS `modifiedBy`
    FROM
        `_businesses`