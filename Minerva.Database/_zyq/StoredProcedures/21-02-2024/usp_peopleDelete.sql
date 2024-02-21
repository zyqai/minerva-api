CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_peopleDelete`(
    IN p_peopleId INT
)
BEGIN
    DELETE FROM `_people` WHERE `peopleId` = p_peopleId;
END