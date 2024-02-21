CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `people_view` AS
    SELECT 
        `_people`.`peopleId` AS `peopleId`,
        `_people`.`userId` AS `userId`,
        `_people`.`tenantId` AS `tenantId`,
        `_people`.`clientName` AS `clientName`,
        `_people`.`clientAddress` AS `clientAddress`,
        `_people`.`phoneNumber` AS `phoneNumber`,
        `_people`.`email` AS `email`,
        `_people`.`preferredContact` AS `preferredContact`,
        `_people`.`creditScore` AS `creditScore`,
        `_people`.`lendabilityScore` AS `lendabilityScore`,
        `_people`.`marriedStatus` AS `marriedStatus`,
        `_people`.`spouseClientId` AS `spouseClientId`,
        `_people`.`rootFolder` AS `rootFolder`,
        `_people`.`createdTime` AS `createdTime`,
        `_people`.`modifiedTime` AS `modifiedTime`,
        `_people`.`createdBy` AS `createdBy`,
        `_people`.`modifiedBy` AS `modifiedBy`,
        `_people`.`firstName` AS `firstName`,
        `_people`.`lastName` AS `lastName`,
        `_people`.`dob` AS `dob`,
        `_people`.`socialsecuritynumber` AS `socialsecuritynumber`,
        `_people`.`postalnumber` AS `postalnumber`,
        `_people`.`stateid` AS `stateid`,
        `_people`.`City` AS `City`,
        `_people`.`clientAddress1` AS `clientAddress1`
    FROM
        `_people`