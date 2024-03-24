CREATE DEFINER=`root`@`localhost` PROCEDURE `Usp_filetypes`(IN in_tenantId int)
BEGIN
	SELECT fileTypeAutoId, fileTypeId, tenantId, fileTypeName 
    FROM _zyq._filetypes
    Where tenantId=in_tenantId=-1;
END