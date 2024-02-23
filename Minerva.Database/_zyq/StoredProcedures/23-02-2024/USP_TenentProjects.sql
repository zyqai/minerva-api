CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `USP_TenentProjects`(
    IN p_tenantId INT
)
BEGIN
    SELECT 
        projectId,tenantId, projectName, projectDescription, industryId,amount, 
        purpose, createdByUserId, createdDateTime, assignedToUserId,modifiedByUserId, 
        modifiedDateTime,loanTypeAutoId,statusAutoId, projectFilesPath 
    FROM projects_view 
    WHERE tenantId = p_tenantId;
END