CREATE PROCEDURE Usp_ProjectRequestDetailsInsert (
    IN in_projectrequestTemplateId INT,
    IN in_projectId INT,
    IN in_tenantId INT,
    IN in_label VARCHAR(45),
    IN in_documentTypeAutoId INT,
    OUT out_message VARCHAR(1000)
)
BEGIN
    DECLARE error_occurred INT DEFAULT 0;
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
        SET error_occurred := 1;
    END;

    START TRANSACTION;
    BEGIN
        -- Insert into _projectrequestdetails table
        INSERT INTO _projectrequestdetails (projectrequestTemplateId, projectId, tenantId, label, documentTypeAutoId)
        VALUES (in_projectrequestTemplateId, in_projectId, in_tenantId, in_label, in_documentTypeAutoId);

        IF error_occurred = 1 THEN
            ROLLBACK;
            SET out_message := 'Error occurred during insertion.';
        ELSE
            COMMIT;
            SET out_message := 'Insertion successful.';
        END IF;
    END;
END