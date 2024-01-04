
DELIMITER //
CREATE PROCEDURE USP_DeleteBusiness(
    IN in_businessId INT
)
BEGIN
    DELETE FROM `_businesses` WHERE `businessId` = in_businessId;
END //
DELIMITER ;
