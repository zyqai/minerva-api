CREATE DEFINER=`root`@`localhost` PROCEDURE `Usp_projectrequestsList`()
BEGIN
	select projectRequestId,projectId,tenantId,projectRequestName,label,sentTo,sentcc,documentTypeName,statusName 
    from projectrequests_view;
END