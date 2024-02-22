CREATE DEFINER=`root`@`localhost` PROCEDURE `USP_GetState`(
 IN p_id INT
)
BEGIN
	 SELECT * FROM `_states` where id = p_id; 
END