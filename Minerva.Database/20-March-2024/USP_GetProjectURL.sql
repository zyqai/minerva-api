CREATE PROCEDURE `USP_GetProjectURL`(IN in_token text)
BEGIN
	SELECT a.RequestUrlId, a.projectId, a.tenantId, a.projectRequestId, a.requestURL, 
	a.token, a.createdOn, a.createBy, a.modifiedOn, a.modifiedBy,b.peopleId
	FROM _zyq._projectrequesturls a
    inner join _zyq._projectrequests b on a.projectId=b.projectId
	 Where token=in_token and b.peopleid>0 
     Limit 1 
     ;
    -- Where requestURL=in_token;
END