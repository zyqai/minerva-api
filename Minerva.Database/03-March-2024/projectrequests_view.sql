CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `projectrequests_view` AS
    SELECT 
        `a`.`projectRequestId` AS `projectRequestId`,
        `a`.`projectId` AS `projectId`,
        `a`.`tenantId` AS `tenantId`,
        `a`.`remindersAutoId` AS `remindersAutoId`,
        `a`.`projectRequestName` AS `projectRequestName`,
        `a`.`projectRequestDescription` AS `projectRequestDescription`,
        `a`.`createdOn` AS `createdOn`,
        `a`.`createdBy` AS `createdBy`,
        `a`.`modifiedOn` AS `modifiedOn`,
        `a`.`modifiedBy` AS `modifiedBy`,
        `b`.`projectrequestDetailsId` AS `projectrequestDetailsId`,
        `b`.`label` AS `label`,
        `b`.`documentTypeAutoId` AS `documentTypeAutoId`,
        `c`.`projectRequestSentId` AS `projectRequestSentId`,
        `c`.`sentTo` AS `sentTo`,
        `c`.`sentcc` AS `sentcc`,
        `c`.`sentOn` AS `sentOn`,
        `c`.`uniqueLink` AS `uniqueLink`,
        `c`.`statusAutoId` AS `statusAutoId`,
        `d`.`documentTypeId` AS `documentTypeId`,
        `d`.`documentTypeName` AS `documentTypeName`,
        `d`.`documentTypeDescription` AS `documentTypeDescription`,
        `d`.`documentClassificationId` AS `documentClassificationId`,
        `d`.`templateFilePath` AS `templateFilePath`,
        `e`.`statusId` AS `statusId`,
        `e`.`statusName` AS `statusName`,
        `e`.`statusDescription` AS `statusDescription`,
        `e`.`projectRequestTemplateStatus` AS `projectRequestTemplateStatus`
    FROM
        ((((`_projectrequests` `a`
        JOIN `_projectrequestdetails` `b` ON ((`a`.`projectRequestId` = `b`.`projectrequestTemplateId`)))
        JOIN `_projectrequestsentto` `c` ON ((`a`.`projectRequestId` = `c`.`projectRequestTemplateId`)))
        JOIN `_documenttypes` `d` ON ((`b`.`documentTypeAutoId` = `d`.`documentTypeAutoId`)))
        JOIN `_statuses` `e` ON ((`c`.`statusAutoId` = `e`.`statusAutoId`)))