CREATE PROCEDURE `USP_GetProjectURL`(IN in_token text)
BEGIN
	SELECT RequestUrlId, projectId, tenantId, projectRequestId, requestURL, 
	token, createdOn, createBy, modifiedOn, modifiedBy 
	FROM _zyq._projectrequesturls
	-- Where token=in_token;
    Where requestURL=in_token;
END