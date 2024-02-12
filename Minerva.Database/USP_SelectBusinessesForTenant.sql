

DELIMITER //

CREATE PROCEDURE USP_SelectBusinessesForTenant(
    IN in_tenantId INT
)
BEGIN
    -- Select specific columns from the _businesses table based on tenantId
    SELECT 
        `businessId`, -- Unique identifier for the business
        `tenantId`,-- Unique identifier for the tenant
        `businessName`, -- Name of the business
        `businessAddress`, -- Address of the business
        `businessType`, -- Type of the business
        `industry`, -- Industry of the business
        `annualRevenue`, -- Annual revenue of the business
        `incorporationDate`, -- Date of incorporation of the business
        `businessRegistrationNumber`, -- Registration number of the business
        `rootDocumentFolder` -- Folder containing documents related to the business
    FROM `_businesses`
    WHERE `tenantId` = in_tenantId; -- Filter by the provided tenantId
END//

DELIMITER ;
