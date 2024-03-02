CREATE DEFINER=`minerva_admin_dev`@`%` PROCEDURE `Usp_LenderSelectById`(In in_lenderID int)
BEGIN
    SELECT
        lenderID,tenantId,name,contactAddress,phoneNumber,email,licensingDetails,
        commercialMortgageProducts,interestRates,terms,loanToValueRatio,applicationProcessDetails,
        underwritingGuidelines,closingCostsAndFees,specializedServices
    FROM
        _zyq._lenders
	where 
		lenderID=in_lenderID;
END