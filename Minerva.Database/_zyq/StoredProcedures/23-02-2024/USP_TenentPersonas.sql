CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `USP_TenentPersonas`(IN p_tenantId int)
BEGIN
	select personaAutoId, personaId, tenantId, personaName, projectPersona 
	from _personas
	where tenantId=p_tenantId;
END