CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `Usp_UpdateLender`(
    IN in_lenderID INT,
    IN in_name VARCHAR(255),
    IN in_contactAddress VARCHAR(255),
    IN in_phoneNumber VARCHAR(20),
    IN in_email VARCHAR(100),
    IN in_licensingDetails VARCHAR(255),
    IN in_commercialMortgageProducts VARCHAR(255),
    IN in_interestRates DECIMAL(10,2),
    IN in_terms VARCHAR(100),
    IN in_loanToValueRatio DECIMAL(5,2),
    IN in_applicationProcessDetails TEXT,
    IN in_underwritingGuidelines TEXT,
    IN in_closingCostsAndFees DECIMAL(10,2),
    IN in_specializedServices VARCHAR(255)
)
BEGIN
    DECLARE error_occurred INT DEFAULT 0;
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
        SET error_occurred := 1;
    END;

    START TRANSACTION;
    BEGIN
        UPDATE _zyq._lenders
        SET
            name = in_name,
            contactAddress = in_contactAddress,
            phoneNumber = in_phoneNumber,
            email = in_email,
            licensingDetails = in_licensingDetails,
            commercialMortgageProducts = in_commercialMortgageProducts,
            interestRates = in_interestRates,
            terms = in_terms,
            loanToValueRatio = in_loanToValueRatio,
            applicationProcessDetails = in_applicationProcessDetails,
            underwritingGuidelines = in_underwritingGuidelines,
            closingCostsAndFees = in_closingCostsAndFees,
            specializedServices = in_specializedServices
        WHERE
            lenderID = in_lenderID;

        IF error_occurred = 1 THEN
            ROLLBACK;
            SELECT 'Error occurred during update.' AS message;
        ELSE
            COMMIT;
            SELECT 'Update successful.' AS message;
        END IF;
    END;
END