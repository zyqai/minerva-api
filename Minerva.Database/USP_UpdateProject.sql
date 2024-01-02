DELIMITER //
CREATE PROCEDURE `USP_UpdateProject`(
    IN p_id INT,
    IN p_Loanamount DECIMAL(8,0),
    IN p_Assignrdstaff VARCHAR(50),
    IN p_Filedescription VARCHAR(5000),
    IN p_staffnote VARCHAR(5000),
    IN p_primaryborrower VARCHAR(50),
    IN p_Primarybusiness VARCHAR(45),
    IN p_startdate DATE,
    IN p_desiredclosingdate DATE,
    IN p_initialphase VARCHAR(45)
)
BEGIN
    UPDATE _projects
    SET
        Loanamount = p_Loanamount,
        Assignrdstaff = p_Assignrdstaff,
        Filedescription = p_Filedescription,
        staffnote = p_staffnote,
        primaryborrower = p_primaryborrower,
        Primarybusiness = p_Primarybusiness,
        startdate = p_startdate,
        desiredclosingdate = p_desiredclosingdate,
        initialphase = p_initialphase
    WHERE id_Projects = p_id;
END//
DELIMITER ;