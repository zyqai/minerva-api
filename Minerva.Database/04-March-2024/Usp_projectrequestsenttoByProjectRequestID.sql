CREATE DEFINER=`root`@`localhost` PROCEDURE `Usp_projectrequestsenttoByProjectRequestID`(IN in_ProjectRequestId int)
BEGIN
	select projectRequestSentId, projectRequestTemplateId , projectId, tenantId, sentTo, sentcc, 
    sentOn, uniqueLink, statusAutoId,b.statusName,b.statusDescription
	from projectrequestsentto_view a
	inner join _statuses b on a.statusAutoId=b.statusAutoId
	where projectRequestTemplateId=in_ProjectRequestId;
END