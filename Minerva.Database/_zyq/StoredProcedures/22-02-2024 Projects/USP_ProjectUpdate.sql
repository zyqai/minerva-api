CREATE DEFINER=`root`@`localhost` PROCEDURE `USP_ProjectUpdate`(
    IN in_projectId INT,
    IN in_tenantId INT,
    IN in_projectName VARCHAR(255),
    IN in_projectDescription TEXT,
    IN in_industryId INT,
    IN in_amount VARCHAR(45),
    IN in_purpose VARCHAR(2000),
    IN in_assignedToUserId INT,
    IN in_loanTypeAutoId INT,
    IN in_statusAutoId INT,
    IN in_projectFilesPath VARCHAR(2000),
    IN in_modifiedByUserId varchar(45)
)
BEGIN
    UPDATE _zyq._projects 
    SET 
        tenantId = in_tenantId,
        projectName = in_projectName,
        projectDescription = in_projectDescription,
        industryId = in_industryId,
        amount = in_amount,
        purpose = in_purpose,
        assignedToUserId = in_assignedToUserId,
        modifiedByUserId=in_modifiedByUserId,
        modifiedDateTime = NOW(),
        loanTypeAutoId = in_loanTypeAutoId,
        statusAutoId = in_statusAutoId,
        projectFilesPath = in_projectFilesPath
    WHERE projectId = in_projectId;
END