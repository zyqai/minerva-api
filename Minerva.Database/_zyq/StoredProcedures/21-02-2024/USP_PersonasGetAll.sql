CREATE DEFINER=`root`@`localhost` PROCEDURE `USP_PersonasGetAll`()
BEGIN
	SELECT `personaId`,`personaName` FROM _personas;
END