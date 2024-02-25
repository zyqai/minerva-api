CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_loanTypes`()
BEGIN
	SELECT loanTypeAutoId, loanTypeId, tenantId, loanType, loanTypeDescription 
	FROM _zyq._loantypes
	Where tenantId=-1;
END