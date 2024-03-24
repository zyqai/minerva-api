CREATE PROCEDURE `USP_projectDocumentDetails`(IN in_project int)
BEGIN
	SELECT a.projectrequestDetailsId, a.projectrequestTemplateId, a.projectId, a.tenantId, a.label, a.documentTypeAutoId, 
	b.documentTypeName,b.documentTypeDescription,c.documentClassificationName,b.documentTypeAutoId,b.documentTypeId,
    c.documentClassificationAutoId,c.documentClassificationId
	FROM _zyq._projectrequestdetails a
	inner join _zyq._documenttypes b on a.documentTypeAutoId=b.documentTypeAutoId
	inner join _zyq._documentclassifications c on b.documentClassificationId=c.documentClassificationId
	where projectId=in_project;
END