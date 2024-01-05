DELIMITER //
CREATE PROCEDURE USP_ClientCreate(
    IN p_userId VARCHAR(45),
    IN p_tenantId INT,
    IN p_clientName VARCHAR(255),
    IN p_clientAddress VARCHAR(255),
    IN p_phoneNumber VARCHAR(45),
    IN p_email VARCHAR(255),
    IN p_preferredContact VARCHAR(45),
    IN p_creditScore VARCHAR(45),
    IN p_lendabilityScore VARCHAR(45),
    IN p_marriedStatus INT,
    IN p_spouseClientId INT,
    IN p_rootFolder VARCHAR(255),
    IN p_createdBy INT
)
BEGIN
    INSERT INTO `_clients` (
        `userId`, `tenantId`, `clientName`, `clientAddress`, `phoneNumber`,
        `email`, `preferredContact`, `creditScore`, `lendabilityScore`,
        `marriedStatus`, `spouseClientId`, `rootFolder`, `createdBy`
    )
    VALUES (
        p_userId, p_tenantId, p_clientName, p_clientAddress, p_phoneNumber,
        p_email, p_preferredContact, p_creditScore, p_lendabilityScore,
        p_marriedStatus, p_spouseClientId, p_rootFolder, p_createdBy
    );
END //
DELIMITER ;