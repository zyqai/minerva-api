CREATE DEFINER=`root`@`localhost` PROCEDURE `USP_ProjectGetByID`(
    IN in_projectId INT
)
BEGIN
    SELECT 
        projectId,tenantId, projectName, projectDescription, industryId,amount, 
        purpose, createdByUserId, createdDateTime, assignedToUserId,modifiedByUserId, 
        modifiedDateTime,loanTypeAutoId,statusAutoId, projectFilesPath 
    FROM projects_view 
    WHERE projectId = in_projectId;
END