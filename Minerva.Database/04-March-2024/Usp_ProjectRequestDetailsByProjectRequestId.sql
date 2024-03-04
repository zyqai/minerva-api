CREATE DEFINER=`root`@`localhost` PROCEDURE `Usp_ProjectRequestDetailsByProjectRequestId`(IN in_projectRequestId int)
BEGIN
	select projectrequestDetailsId, projectrequestTemplateId, projectId, tenantId, label, documentTypeAutoId,
	documentTypeName, documentTypeDescription, templateFilePath from projectrequestdetails_view
	where projectrequestTemplateId=in_projectRequestId;
END