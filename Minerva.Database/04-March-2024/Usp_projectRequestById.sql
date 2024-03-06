CREATE PROCEDURE `Usp_projectRequestById`(IN in_projectRequestId int)
BEGIN
	select projectRequestId, projectId, a.tenantId, remindersAutoId, projectRequestName, projectRequestDescription, 
	createdOn,b.UserName as createdBy,modifiedOn,c.UserName as  modifiedBy 
	from projectrequeststable_view a
    inner join users_view b on a.createdBy=b.UserId
    inner join users_view c on a.modifiedBy=b.UserId
	Where projectRequestId=in_projectRequestId
    Limit 1;
END