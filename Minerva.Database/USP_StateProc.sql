DELIMITER //

CREATE PROCEDURE USP_InsertState(
    IN p_code CHAR(2),
    IN p_name VARCHAR(128)
)
BEGIN
    INSERT INTO `_states` (code, name)
    VALUES (p_code, p_name);
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE USP_GetStates()
BEGIN
    SELECT * FROM `_states`;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE USP_GetState(
 IN p_id INT
)
BEGIN
    SELECT * FROM `_states` where id = p_id; 
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE USP_UpdateState(
    IN p_id INT,
    IN p_code CHAR(2),
    IN p_name VARCHAR(128)
)
BEGIN
    UPDATE `_states`
    SET code = p_code, name = p_name
    WHERE id = p_id;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE USP_DeleteState(
    IN p_id INT
)
BEGIN
    DELETE FROM `_states`
    WHERE id = p_id;
END //

DELIMITER ;