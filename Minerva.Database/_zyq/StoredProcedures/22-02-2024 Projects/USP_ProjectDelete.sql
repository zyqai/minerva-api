CREATE DEFINER=`root`@`localhost` PROCEDURE `USP_ProjectDelete`(
    IN in_projectId INT
)
BEGIN
    DELETE FROM _zyq._projects WHERE projectId = in_projectId;
END