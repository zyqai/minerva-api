CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_peopleBusinessRelationDelete`(
    IN p_peopleBusinessId INT
)
BEGIN
    DELETE FROM _zyq._peoplebusinessrelation
    WHERE peopleBusinessId = p_peopleBusinessId;
END