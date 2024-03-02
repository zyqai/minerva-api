CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `Usp_LenderDelete`(
    IN in_lenderID INT
)
BEGIN
    DECLARE error_occurred INT DEFAULT 0;
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
        SET error_occurred := 1;
    END;

    START TRANSACTION;
    BEGIN
        DELETE FROM _zyq._lenders WHERE lenderID = in_lenderID;

        IF error_occurred = 1 THEN
            ROLLBACK;
            SELECT 'Error occurred during deletion.' AS message;
        ELSE
            COMMIT;
            SELECT 'Deletion successful.' AS message;
        END IF;
    END;
END