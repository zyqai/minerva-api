CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `Usp_LenderInsert`(
    
    IN in_tenantId int,
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
    IN in_specializedServices VARCHAR(255),
    OUT p_last_insert_id int
)
BEGIN
    INSERT INTO _zyq._lenders (
        tenantId,name,contactAddress,phoneNumber,email,licensingDetails,commercialMortgageProducts,
        interestRates,terms,loanToValueRatio,applicationProcessDetails,underwritingGuidelines,
        closingCostsAndFees,specializedServices
    ) VALUES (
        in_tenantId,in_name,in_contactAddress,in_phoneNumber,in_email,in_licensingDetails,
        in_commercialMortgageProducts,in_interestRates,in_terms,in_loanToValueRatio,in_applicationProcessDetails,
        in_underwritingGuidelines,in_closingCostsAndFees,in_specializedServices
    );
    SET p_last_insert_id := LAST_INSERT_ID();
END