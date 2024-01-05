DELIMITER //
CREATE PROCEDURE USP_ClientDelete(
    IN p_clientId INT
)
BEGIN
    DELETE FROM `_clients` WHERE `clientId` = p_clientId;
END //
DELIMITER ;