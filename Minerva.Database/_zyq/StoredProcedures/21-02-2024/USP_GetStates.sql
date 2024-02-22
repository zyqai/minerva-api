CREATE DEFINER=`root`@`localhost` PROCEDURE `USP_GetStates`()
BEGIN
	SELECT id, code, name FROM _states;
END