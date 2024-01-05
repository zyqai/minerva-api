DELIMITER //
CREATE PROCEDURE USP_ClinetGetById(
    IN p_clientId INT
)
BEGIN
    SELECT * FROM `_clients` WHERE `clientId` = p_clientId;
END //
DELIMITER ;