CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `Usp_LenderSelectAll`()
BEGIN
    SELECT
        lenderID,tenantId,name,contactAddress,phoneNumber,email,licensingDetails,
        commercialMortgageProducts,interestRates,terms,loanToValueRatio,applicationProcessDetails,
        underwritingGuidelines,closingCostsAndFees,specializedServices
    FROM
        _zyq._lenders;
END