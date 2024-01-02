DELIMITER //
CREATE PROCEDURE `USP_InsertProject`(
    IN p_Filename VARCHAR(50),
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
    INSERT INTO _projects (
        Filename,
        Loanamount,
        Assignrdstaff,
        Filedescription,
        staffnote,
        primaryborrower,
        Primarybusiness,
        startdate,
        desiredclosingdate,
        initialphase
    ) VALUES (
        p_Filename,
        p_Loanamount,
        p_Assignrdstaff,
        p_Filedescription,
        p_staffnote,
        p_primaryborrower,
        p_Primarybusiness,
        p_startdate,
        p_desiredclosingdate,
        p_initialphase
    );
END//
DELIMITER ;