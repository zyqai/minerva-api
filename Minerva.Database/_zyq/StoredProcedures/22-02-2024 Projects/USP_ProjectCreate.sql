CREATE DEFINER=`root`@`localhost` PROCEDURE `USP_ProjectCreate`(
    IN in_tenantId INT,
    IN in_projectName VARCHAR(255),
    IN in_projectDescription TEXT,
    IN in_industryId INT,
    IN in_amount VARCHAR(45),
    IN in_purpose VARCHAR(2000),
    IN in_createdByUserId INT,
    IN in_assignedToUserId INT,
    IN in_loanTypeAutoId INT,
    IN in_statusAutoId INT,
    IN in_projectFilesPath VARCHAR(2000)
)
BEGIN
    INSERT INTO _zyq._projects (
        tenantId, 
        projectName, 
        projectDescription, 
        industryId, 
        amount, 
        purpose, 
        createdByUserId, 
        createdDateTime, 
        assignedToUserId, 
       loanTypeAutoId, 
        statusAutoId, 
        projectFilesPath
    ) VALUES (
        in_tenantId, 
        in_projectName, 
        in_projectDescription, 
        in_industryId, 
        in_amount, 
        in_purpose, 
        in_createdByUserId, 
        NOW(), 
        in_assignedToUserId, 
        in_loanTypeAutoId, 
        in_statusAutoId, 
        in_projectFilesPath
    );
END