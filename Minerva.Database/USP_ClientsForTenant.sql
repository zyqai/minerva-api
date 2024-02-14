-- DROP PROCEDURE `USP_ClientsForTenant`
DELIMITER //
CREATE PROCEDURE `USP_ClientsForTenant`(
    IN in_tenantId INT
)
BEGIN
    -- Select specific columns from the _clients table based on tenantId
    SELECT 
        `clientId`, -- Unique identifier for the client
        `tenantId`, -- Unique identifier for the tenent
        `userId`, -- User ID associated with the client
        `clientName`, -- Name of the client
        `firstName`, -- First name of the client
        `lastName`, -- Last name of the client
        `dob`, -- Date of birth of the client
        `socialsecuritynumber`, -- Social security number of the client
        `postalnumber`, -- Postal number of the client
        `stateid`, -- State ID of the client
        `clientAddress`, -- Address of the client
        `phoneNumber`, -- Phone number of the client
        `email`, -- Email address of the client
        `preferredContact`, -- Preferred contact method of the client
        `creditScore`, -- Credit score of the client
        `lendabilityScore`, -- Lendability score of the client
        `marriedStatus`, -- Marital status of the client
        `spouseClientId`, -- Client ID of the spouse
        `rootFolder`, -- Root folder of the client
        `createdTime`, -- Timestamp of when the client was created
        `modifiedTime`, -- Timestamp of when the client was last modified
        `createdBy`, -- User ID of the creator
        `modifiedBy`, -- User ID of the last modifier
        `city`,`clientAddress1`
    FROM `_clients`
    WHERE `tenantId` = in_tenantId; -- Filter by the provided tenantId
END//

DELIMITER ;