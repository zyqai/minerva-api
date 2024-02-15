DELIMITER //

-- CREATE PROCEDURE: Insert a new record into the `_clientsbusinessrelation` table
CREATE PROCEDURE USP_ClientBusinessRelationInsert(
    IN in_clientId INT,
    IN in_businessId INT,
    IN in_personaId INT,
    OUT p_last_insert_id INT 
)
BEGIN
    -- Insert a new record into the table with provided values
    INSERT INTO `_clientsbusinessrelation` (`clientId`, `businessId`, `personaId`)
    VALUES (in_clientId, in_businessId, in_personaId);
    SET p_last_insert_id := LAST_INSERT_ID();
END//

DELIMITER //
-- READ PROCEDURE: Retrieve records from the `_clientsbusinessrelation` table based on clientBusinessId
CREATE PROCEDURE USP_ClientBusinessRelationSelect(
    IN in_clientBusinessId INT
)
BEGIN
    -- Retrieve the record from the table based on the provided clientBusinessId
     SELECT 
        `clientBusinessId`, 
        `clientId`, 
        `businessId`, 
        `personaId`
    FROM `_clientsbusinessrelation`
    WHERE `clientBusinessId` = in_clientBusinessId;
END//

DELIMITER //
-- UPDATE PROCEDURE: Update a record in the `_clientsbusinessrelation` table
CREATE PROCEDURE USP_ClientBusinessRelationUpdate(
    IN in_clientBusinessId INT,
    IN in_clientId INT,
    IN in_businessId INT,
    IN in_personaId INT
)
BEGIN
    -- Update the record in the table based on the provided clientBusinessId
    UPDATE `_clientsbusinessrelation`
    SET 
        `clientId` = in_clientId,
        `businessId` = in_businessId,
        `personaId` = in_personaId
    WHERE `clientBusinessId` = in_clientBusinessId;
END//


DELIMITER //
-- DELETE PROCEDURE: Delete a record from the `_clientsbusinessrelation` table
CREATE PROCEDURE USP_ClientBusinessRelationDelete(
    IN in_clientBusinessId INT
)
BEGIN
    -- Delete the record from the table based on the provided clientBusinessId
    DELETE FROM `_clientsbusinessrelation`
    WHERE `clientBusinessId` = in_clientBusinessId;
END//

DELIMITER ;



DELIMITER //
-- READ PROCEDURE: Retrieve records from the `_clientsbusinessrelation` 
CREATE PROCEDURE USP_ClientBusinessRelationSelectALL(
)
BEGIN
    -- Retrieve the record from the table based on the provided clientBusinessId
     SELECT 
        `clientBusinessId`, 
        `clientId`, 
        `businessId`, 
        `personaId`
    FROM `_clientsbusinessrelation`;
    
END//

DELIMITER //
