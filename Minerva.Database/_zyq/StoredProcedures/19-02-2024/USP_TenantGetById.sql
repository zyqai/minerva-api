CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_tenantGetById`(IN p_tenantId INT)
BEGIN
    SELECT tenantId, tenantName, tenantDescription, tenantDomain, tenantLogoPath,
	   tenantAddress, tenantAddress1, tenantPhone, tenantContactName, tenantContactEmail,
       postalCode, city, stateid,createdBy,createdDateTime, updatedBy,updateDateTime
	FROM tenants_view 
    
    WHERE tenantId = p_tenantId;
END