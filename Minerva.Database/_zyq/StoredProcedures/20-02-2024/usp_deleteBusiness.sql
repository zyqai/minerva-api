CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_deleteBusiness`(IN in_businessId int)
BEGIN
DELETE FROM `_businesses` Where `businessId`=in_businessId;
END