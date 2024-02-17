CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_userDelete`(
    IN in_userId VARCHAR(45)
)
BEGIN
    DELETE FROM `_users` WHERE `userId` = in_userId;
END