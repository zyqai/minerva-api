CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `usp_ProjectListWithDetails`(IN in_tenantid int)
BEGIN
	select projectId, tenantId, projectName, projectDescription, industryId, amount, purpose, loanTypeAutoId,
    statusAutoId, industrySectorAutoId, industrySector, industryDescription, assignedToUserId, assignedTousername, 
    assignedToemail, assignedToName, statusId, statusName, statusDescription ,loanType,loanTypeDescription, createdByUserId, createdDateTime
    from projectlist_view 
    where tenantId=in_tenantid
    LIMIT 10;
END