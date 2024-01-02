DELIMITER //
CREATE PROCEDURE `USP_DeleteProject`(
    IN p_id INT
)
BEGIN
    DELETE FROM _projects WHERE id_Projects = p_id;
END//
DELIMITER ;