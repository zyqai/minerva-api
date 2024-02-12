-- DROP PROCEDURE USP_ClientPersonasByBusinessId
-- call USP_ClientPersonasByBusinessId (1)
DELIMITER //

CREATE PROCEDURE USP_ClientPersonasByBusinessId(IN in_BusinessId INT)
BEGIN
    -- Select specific columns from the _clients and personas table based on relation businessId
    SELECT 
        b.clientId,
        b.clientName,
        b.userId,
        b.tenantId,
        b.firstName,
        b.lastName,
        b.email,
        b.phoneNumber,
        c.personaId,
        c.personaName,
        b.stateid
        -- ,b.dob,b.socialsecuritynumber,b.postalnumber,b.stateid,b.clientAddress,b.phoneNumber,b.creditScore,b.lendabilityScore
        -- ,b.marriedStatus,b.spouseClientId,b.rootFolder
    FROM 
        _clientsbusinessrelation a
    INNER JOIN 
        _clients b ON a.clientId = b.clientId
    INNER JOIN 
        _personas c ON a.personaId = c.personaId
    WHERE 
        a.businessId = in_BusinessId;
END//

DELIMITER ;
