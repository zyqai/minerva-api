DELIMITER //
CREATE PROCEDURE `USP_GetProjectById`(
    IN p_id INT
)
BEGIN
    SELECT * FROM _projects WHERE id_Projects = p_id;
END//
DELIMITER ;