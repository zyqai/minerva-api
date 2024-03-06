DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_requesttemplatesDelete`(
    IN p_requestTemplateId INT
)
BEGIN
    DELETE FROM `_requesttemplates` WHERE `requestTemplateId` = p_requestTemplateId;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_requesttemplatesUpdate`(
    IN p_requestTemplateId INT,
    IN p_tenantId INT,
    IN p_requestTemplateName varchar(45),
    IN p_requestTemplateDescription varchar(2000),
    IN p_remindersAutoId INT
)
BEGIN
   
      UPDATE `_requesttemplates`
		SET
		`tenantId` = p_tenantId,
		`requestTemplateName` = p_requestTemplateName,
        `requestTemplateDescription` = p_requestTemplateDescription,
        `remindersAutoId` = p_remindersAutoId
		WHERE `requestTemplateId` = p_requestTemplateId;


END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_requesttemplatesGetAll`()
BEGIN
SELECT `_requesttemplates`.`requestTemplateId`,
    `_requesttemplates`.`tenantId`,
    `_requesttemplates`.`requestTemplateName`,
    `_requesttemplates`.`requestTemplateDescription`,
    `_requesttemplates`.`remindersAutoId`
FROM `_requesttemplates`;

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_requesttemplatesCreate`(
    -- Parameters for input
    IN p_requestTemplateId INT,
    IN p_tenantId INT,
    IN p_requestTemplateName varchar(45),
    IN p_requestTemplateDescription varchar(2000),
    IN p_remindersAutoId INT,
    -- Output parameter
    OUT p_last_insert_id INT
)
BEGIN
    -- Insert data into the '_requesttemplates' table
        INSERT INTO `_requesttemplates`
        (`requestTemplateId`,
        `tenantId`,
        `requestTemplateName`,
        `requestTemplateDescription`,
        `remindersAutoId`) 
        VALUES
        (p_requestTemplateId,
        p_tenantId,
        p_requestTemplateName,
        p_requestTemplateDescription,
        p_remindersAutoId);

    -- Set the output parameter to the last inserted ID
    SET p_last_insert_id := LAST_INSERT_ID();
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_requesttemplateGetById`(IN p_requestTemplateId INT)
BEGIN

		SELECT `_requesttemplates`.`requestTemplateId`,
                `_requesttemplates`.`tenantId`,
                `_requesttemplates`.`requestTemplateName`,
                `_requesttemplates`.`requestTemplateDescription`,
                `_requesttemplates`.`remindersAutoId`
                    FROM `_requesttemplates`
		WHERE requestTemplateId = p_requestTemplateId;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_requesttemplatedetailsUpdate`(
    IN p_requestTemplateDetailsId INT,
    IN p_requestTemplateId INT,
    IN p_tenantId INT,
    IN p_label varchar(45),
    IN p_documentTypeAutoId INT
)
BEGIN
   
      UPDATE `_requesttemplatedetails`
		SET
        `requestTemplateId` = p_requestTemplateId,
		`tenantId` = p_tenantId,
		`label` = p_label,
        `documentTypeAutoId` = p_documentTypeAutoId
		WHERE `requestTemplateDetailsId` = p_requestTemplateDetailsId;


END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_requesttemplatedetailsGetById`(IN p_requestTemplateDetailsId INT)
BEGIN

		SELECT `_requesttemplatedetails`.`requestTemplateDetailsId`,
                `_requesttemplatedetails`.`requestTemplateId`,
                `_requesttemplatedetails`.`tenantId`,
                `_requesttemplatedetails`.`label`,
                `_requesttemplatedetails`.`documentTypeAutoId`
                    FROM `_requesttemplatedetails`
		WHERE requestTemplateDetailsId = p_requestTemplateDetailsId;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_requesttemplatedetailsGetAll`()
BEGIN
SELECT `_requesttemplatedetails`.`requestTemplateDetailsId`,
    `_requesttemplatedetails`.`requestTemplateId`,
    `_requesttemplatedetails`.`tenantId`,
    `_requesttemplatedetails`.`label`,
    `_requesttemplatedetails`.`documentTypeAutoId`
FROM `_requesttemplatedetails`;

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_requesttemplatedetailsDelete`(
    IN p_requestTemplateDetailsId INT
)
BEGIN
    DELETE FROM `_requesttemplatedetails` WHERE `requestTemplateDetailsId` = p_requestTemplateDetailsId;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_requesttemplatedetailsCreate`(
    -- Parameters for input
    IN p_requestTemplateId INT,
    IN p_tenantId INT,
    IN p_label varchar(45),
    IN p_documentTypeAutoId INT,
    -- Output parameter
    OUT p_last_insert_id INT
)
BEGIN
    -- Insert data into the '_requesttemplatedetails' table
        INSERT INTO `_requesttemplatedetails`
        (`requestTemplateId`,
        `tenantId`,
        `label`,
        `documentTypeAutoId`)
        VALUES
        (p_requestTemplateId,
        p_tenantId,
        p_label,
        p_documentTypeAutoId);

    -- Set the output parameter to the last inserted ID
    SET p_last_insert_id := LAST_INSERT_ID();
END$$
DELIMITER ;
