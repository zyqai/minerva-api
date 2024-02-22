CREATE DEFINER=`root`@`localhost` PROCEDURE `USP_BusinessesPersonasByclientIds`(
    IN p_clientId INT
)
BEGIN

	select 
		v.businessId as businessId,
        v.businessName as businessName,
		v.businessType as businessType,
		v.industry as industry,
		v.personaId as personaId,
		v.personaName as personaName,
		v.clientId as clientId,
		v.tenantId as tenantId,
		v.businessAddress as businessAddress,
		v.clientBusinessId as clientBusinessId
	from 
		_viewpersonbusinesspersona v
	where 
		v.clientId=p_clientId;

END