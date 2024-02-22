CREATE PROCEDURE `USP_ClientPersonasByBusinessId`(IN in_BusinessId INT)
BEGIN
    -- Select specific columns from the _clients and personas table based on relation businessId
    SELECT 
        v.clientId,
        v.clientName,
        v.userId,
        v.tenantId,
        v.firstName,
        v.lastName,
        v.email,
        v.phoneNumber,
        v.personaId,
        v.personaName,
        v.stateid,
        v.clientBusinessId
    FROM 
        _viewbusinesspersonpersona v
    WHERE 
        v.businessId = in_BusinessId;
END