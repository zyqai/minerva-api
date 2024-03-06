CREATE DEFINER=`root`@`localhost` PROCEDURE `Usp_RequestTemplateWithDetailsInsert`(
    IN in_tenantId INT,
    IN in_requestTemplateName VARCHAR(45),
    IN in_requestTemplateDescription VARCHAR(2000),
    IN in_remindersAutoId INT,
    IN in_requestTemplateDetails JSON
)
BEGIN
    DECLARE last_requestTemplateId INT;
    DECLARE error_occurred INT DEFAULT 0;
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
        SET error_occurred := 1;
    END;

    START TRANSACTION;
    BEGIN
        -- Insert into _requesttemplates table
        INSERT INTO _requesttemplates (tenantId, requestTemplateName, requestTemplateDescription, remindersAutoId)
        VALUES (in_tenantId, in_requestTemplateName, in_requestTemplateDescription, in_remindersAutoId);
        
        SET last_requestTemplateId := LAST_INSERT_ID();

        -- Insert into _requesttemplatedetails table
        INSERT INTO _requesttemplatedetails (requestTemplateId, tenantId, label, documentTypeAutoId)
        SELECT last_requestTemplateId, in_tenantId, JSON_UNQUOTE(JSON_EXTRACT(value, '$.label')), JSON_UNQUOTE(JSON_EXTRACT(value, '$.documentTypeAutoId'))
        FROM JSON_TABLE(in_requestTemplateDetails, '$[*]' COLUMNS (
            value JSON PATH '$'
        )) AS requestTemplateDetails;

        IF error_occurred = 1 THEN
            ROLLBACK;
            SELECT 'Error occurred during insertion.' AS message;
        ELSE
            COMMIT;
            SELECT 'Insertion successful.' AS message;
        END IF;
    END;
END