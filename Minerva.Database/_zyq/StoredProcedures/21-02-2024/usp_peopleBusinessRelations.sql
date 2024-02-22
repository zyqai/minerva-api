CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_peopleBusinessRelation`(IN p_peopleBusinessId INT)
BEGIN
    SELECT peopleBusinessId, tenantId, peopleId, businessId, personaAutoId as personaId, details
    FROM _zyq._peoplebusinessrelation
    WHERE peopleBusinessId=p_peopleBusinessId;
END