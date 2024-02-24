CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_industrys`()
BEGIN
	SELECT industrySectorAutoId, industryId, tenantId, industrySector, industryDescription 
	FROM _zyq._industrysectors;
	
END