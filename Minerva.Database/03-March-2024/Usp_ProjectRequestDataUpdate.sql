CREATE DEFINER=`root`@`localhost` PROCEDURE `Usp_ProjectRequestDataUpdate`(
    IN in_projectRequestId INT,
    IN in_projectId INT,
    IN in_tenantId INT,
    IN in_remindersAutoId INT,
    IN in_projectRequestName VARCHAR(45),
    IN in_projectRequestDescription VARCHAR(2000),
    IN in_modifiedBy VARCHAR(45),
    IN in_projectRequestSentId INT,
    IN in_sentTo VARCHAR(255),
    IN in_sentcc VARCHAR(255),
    IN in_statusAutoId INT,
    IN in_projectRequestDetailsId INT,
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
        -- Update _projectrequests table
        UPDATE _projectrequests
        SET projectId = in_projectId,
            tenantId = in_tenantId,
            remindersAutoId = in_remindersAutoId,
            projectRequestName = in_projectRequestName,
            projectRequestDescription = in_projectRequestDescription,
            modifiedOn = NOW(),
            modifiedBy = in_modifiedBy
        WHERE projectRequestId = in_projectRequestId;

        -- Update _projectrequestsentto table
        UPDATE _projectrequestsentto
        SET sentTo = in_sentTo,
            sentcc = in_sentcc,
            statusAutoId = in_statusAutoId
        WHERE projectRequestSentId = in_projectRequestSentId;

        -- Update _projectrequestdetails table
        UPDATE _projectrequestdetails
        SET label = in_label,
            documentTypeAutoId = in_documentTypeAutoId
        WHERE projectrequestDetailsId = in_projectRequestDetailsId;

        IF error_occurred = 1 THEN
            ROLLBACK;
            SET out_message := 'Error occurred during update.';
        ELSE
            COMMIT;
            SET out_message := 'Update successful.';
        END IF;
    END;
END