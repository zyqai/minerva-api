CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_tenantDelete`(IN p_tenantId INT)
BEGIN
	DELETE FROM _tenants WHERE tenantId = p_tenantId;
END