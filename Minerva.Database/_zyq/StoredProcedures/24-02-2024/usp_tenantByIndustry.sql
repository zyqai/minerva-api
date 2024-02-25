CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_tenantByIndustry`()
BEGIN
	SELECT industrySectorAutoId, industryId, tenantId, industrySector, industryDescription 
	FROM _zyq._industrysectors
	WHERE tenantId=in_tenantId;
END