CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `_zyq`.`projectrequestdetails_view` AS
    SELECT 
        `a`.`projectrequestDetailsId` AS `projectrequestDetailsId`,
        `a`.`projectrequestTemplateId` AS `projectrequestTemplateId`,
        `a`.`projectId` AS `projectId`,
        `a`.`tenantId` AS `tenantId`,
        `a`.`label` AS `label`,
        `b`.`documentTypeAutoId` AS `documentTypeAutoId`,
        `b`.`documentTypeName` AS `documentTypeName`,
        `b`.`documentTypeDescription` AS `documentTypeDescription`,
        `b`.`templateFilePath` AS `templateFilePath`
    FROM
        (`_zyq`.`_projectrequestdetails` `a`
        JOIN `_zyq`.`_documenttypes` `b` ON ((`a`.`documentTypeAutoId` = `b`.`documentTypeAutoId`)))