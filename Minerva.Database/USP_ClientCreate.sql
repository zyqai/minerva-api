
/*
Author: [Santhosh]
Created Date: [2024-01-09]
Modified Date: [2024-02-13] Createdby and UpdatedBy changed Int to varchar
*/
DELIMITER //

-- Create the stored procedure for client createion 
CREATE PROCEDURE `USP_ClientCreate`(
    -- Parameters for input
    IN p_userId VARCHAR(45),
    IN p_tenantId INT,
    IN p_clientName VARCHAR(255),
    IN p_firstName VARCHAR(45),
    IN p_lastName VARCHAR(45),
    IN p_dob DATETIME,
    IN p_socialsecuritynumber VARCHAR(45),
    IN p_postalnumber VARCHAR(10),
    IN p_stateid INT,
    IN p_clientAddress VARCHAR(255),
    IN p_phoneNumber VARCHAR(45),
    IN p_email VARCHAR(255),
    IN p_preferredContact VARCHAR(45),
    IN p_creditScore VARCHAR(45),
    IN p_lendabilityScore VARCHAR(45),
    IN p_marriedStatus INT,
    IN p_spouseClientId INT,
    IN p_rootFolder VARCHAR(255),
    IN p_createdBy VARCHAR(50),
    IN p_City VARCHAR(100), -- Added parameter for city
    IN p_clientAddress1 VARCHAR(255), -- Added parameter for client address line 1
    -- Output parameter
    OUT p_last_insert_id INT
)
BEGIN
    -- Insert data into the '_clients' table
    INSERT INTO `_clients` (
        `userId`, `tenantId`, `clientName`, `firstName`, `lastName`, `dob`,
        `socialsecuritynumber`, `postalnumber`, `stateid`, `clientAddress`,
        `phoneNumber`, `email`, `preferredContact`, `creditScore`, `lendabilityScore`,
        `marriedStatus`, `spouseClientId`, `rootFolder`, `createdTime`,
        `createdBy`, `modifiedBy`, `city`, `clientAddress1`
    )
    VALUES (
        p_userId, p_tenantId, p_clientName, p_firstName, p_lastName, p_dob,
        p_socialsecuritynumber, p_postalnumber, p_stateid, p_clientAddress,
        p_phoneNumber, p_email, p_preferredContact, p_creditScore, p_lendabilityScore,
        p_marriedStatus, p_spouseClientId, p_rootFolder, NOW(), p_createdBy,
        p_createdBy, p_city, p_clientAddress1
    );

    -- Set the output parameter to the last inserted ID
    SET p_last_insert_id := LAST_INSERT_ID();
END//

-- Reset delimiter to semicolon
DELIMITER ;
