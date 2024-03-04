CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `_zyq`.`projectrequestsentto_view` AS
    SELECT 
        `_zyq`.`_projectrequestsentto`.`projectRequestSentId` AS `projectRequestSentId`,
        `_zyq`.`_projectrequestsentto`.`projectRequestTemplateId` AS `projectRequestTemplateId`,
        `_zyq`.`_projectrequestsentto`.`projectId` AS `projectId`,
        `_zyq`.`_projectrequestsentto`.`tenantId` AS `tenantId`,
        `_zyq`.`_projectrequestsentto`.`sentTo` AS `sentTo`,
        `_zyq`.`_projectrequestsentto`.`sentcc` AS `sentcc`,
        `_zyq`.`_projectrequestsentto`.`sentOn` AS `sentOn`,
        `_zyq`.`_projectrequestsentto`.`uniqueLink` AS `uniqueLink`,
        `_zyq`.`_projectrequestsentto`.`statusAutoId` AS `statusAutoId`
    FROM
        `_zyq`.`_projectrequestsentto`