CREATE DEFINER=`root`@`localhost` PROCEDURE `Usp_ProjectRequestWithDetailsInsert`(
    IN in_requestName VARCHAR(45),
    IN in_requestDescription VARCHAR(2000),
    IN in_projectId INT,
    IN in_tenantId INT,
    IN in_reminderId INT,
    IN in_requestSendTo JSON,
    IN in_requestDetails JSON,
    IN in_createdBy varchar(45),
    OUT out_message VARCHAR(1000) -- Change the name of the output parameter
    
)
BEGIN
    DECLARE error_occurred INT DEFAULT 0;
     DECLARE last_requestId INT;
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
        SET error_occurred := 1;
    END;

    START TRANSACTION;
    BEGIN
        -- Insert into _projectrequests table
        INSERT INTO _projectrequests (projectId, tenantId, remindersAutoId, projectRequestName, projectRequestDescription, createdOn, createdBy)
        VALUES (in_projectId, in_tenantId, in_reminderId, in_requestName, in_requestDescription, NOW(), in_createdBy);
        
       
        SET last_requestId := LAST_INSERT_ID();

        -- Insert into _projectrequestsentto table
        INSERT INTO _projectrequestsentto (projectRequestTemplateId, projectId, tenantId, sentTo, sentcc, sentOn, uniqueLink, statusAutoId)
        SELECT last_requestId, in_projectId, in_tenantId, JSON_UNQUOTE(JSON_EXTRACT(value, '$.SendTo')), JSON_UNQUOTE(JSON_EXTRACT(value, '$.SendCC')), NOW(), '', JSON_UNQUOTE(JSON_EXTRACT(value, '$.Status'))
        FROM JSON_TABLE(in_requestSendTo, '$[*]' COLUMNS (
            value JSON PATH '$'
        )) AS sendTo;

        -- Insert into _projectrequestdetails table
        INSERT INTO _projectrequestdetails (projectrequestTemplateId, projectId, tenantId, label, documentTypeAutoId)
        SELECT last_requestId, in_projectId, in_tenantId, JSON_UNQUOTE(JSON_EXTRACT(value, '$.Label')), JSON_UNQUOTE(JSON_EXTRACT(value, '$.DocumentTypeAutoId'))
        FROM JSON_TABLE(in_requestDetails, '$[*]' COLUMNS (
            value JSON  PATH '$'
        )) AS requestDetails;

        IF error_occurred = 1 THEN
            ROLLBACK;
            SET out_message := 'Error occurred during insertion.'; -- Set the value of the output parameter
        ELSE
            COMMIT;
            SET out_message := 'Insertion successful.'; -- Set the value of the output parameter
        END IF;
    END;
END