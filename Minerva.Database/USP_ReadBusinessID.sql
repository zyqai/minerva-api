
DELIMITER //
CREATE PROCEDURE USP_ReadBusinessID(
    IN in_businessId INT
)
BEGIN
    SELECT * FROM `_businesses` WHERE `businessId` = in_businessId;
END //
DELIMITER ;
