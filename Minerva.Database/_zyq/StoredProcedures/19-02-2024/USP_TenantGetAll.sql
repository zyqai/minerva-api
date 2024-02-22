CREATE PROCEDURE `USP_TenantGetAll`()
BEGIN
    SELECT tenantId, tenantName, tenantDescription, tenantDomain, tenantLogoPath,
	   tenantAddress, tenantAddress1, tenantPhone, tenantContactName, tenantContactEmail,
       postalCode, city, stateid 
	FROM tenants_view ;
END