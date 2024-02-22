CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_peopleBusinessRelationInsert`(
    IN p_tenantId INT,
    IN p_peopleId INT,
    IN p_businessId INT,
    IN p_personaAutoId INT,
    IN p_details VARCHAR(255),
     OUT p_last_insert_id INT 
    
)
BEGIN
    INSERT INTO _zyq._peoplebusinessrelation (tenantId, peopleId, businessId, personaAutoId, details)
    VALUES (p_tenantId, p_peopleId, p_businessId, p_personaAutoId, p_details);
    SET p_last_insert_id := LAST_INSERT_ID();
END