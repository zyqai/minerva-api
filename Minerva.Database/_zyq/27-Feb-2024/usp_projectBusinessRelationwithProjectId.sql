CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_projectBusinessRelationwithProjectId`(in in_projectId int)
BEGIN
	select projectBusinessId, tenantId, projectId, businessId, businessName, businessType 
    FROM projectbusinessrelation_view
    Where projectId=in_projectId;
END