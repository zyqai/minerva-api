CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_tenantUpdate`(
    IN p_tenantId INT,
    IN p_tenantName VARCHAR(255),
    IN p_tenantDomain VARCHAR(255),
    IN p_tenantLogoPath VARCHAR(255),
    IN p_tenantAddress VARCHAR(1000),
    IN p_tenantAddress1 VARCHAR(1000),
    IN p_tenantPhone VARCHAR(45),
    IN p_tenantContactName VARCHAR(45),
    IN p_tenantContactEmail VARCHAR(100),
    IN p_postalCode VARCHAR(45),
    IN p_city VARCHAR(100),
    IN p_stateId INT,
    IN p_modifiedBy VARCHAR(45)
)
BEGIN
    UPDATE `_tenants`
    SET
        `tenantName` = p_tenantName,
        `tenantDomain` = p_tenantDomain,
        `tenantLogoPath` = p_tenantLogoPath,
        `tenantAddress` = p_tenantAddress,
        `tenantAddress1` = p_tenantAddress1,
        `tenantPhone` = p_tenantPhone,
        `tenantContactName` = p_tenantContactName,
        `tenantContactEmail` = p_tenantContactEmail,
        `postalCode` = p_postalCode,
        `city` = p_city,
        `stateid` = p_stateId,
        `modifiedBy`=p_modifiedBy,
        `modifiedDateTime`=NOW()
        
    WHERE `tenantId` = p_tenantId;
    END