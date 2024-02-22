CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_businessGetId`(
    IN in_businessId INT
)
BEGIN
      SELECT 
        `businessId` AS `Business_ID`, -- Unique identifier for the business
        `tenantId` AS `Tenant_ID`, -- Identifier for the tenant
        `businessName` AS `Business_Name`, -- Name of the business
        `businessAddress` AS `Business_Address`, -- Address of the business
        `businessType` AS `Business_Type`, -- Type of the business
        `industry` AS `Industry`, -- Industry of the business
        `annualRevenue` AS `Annual_Revenue`, -- Annual revenue of the business
        `incorporationDate` AS `Incorporation_Date`, -- Date of incorporation of the business
        `businessRegistrationNumber` AS `Business_Registration_Number`, -- Registration number of the business
        `rootDocumentFolder` AS `Root_Document_Folder`, -- Root folder for documents related to the business
        `businessAddress1` AS `Business_Address1`,
         `CreatedBy` As `CreatedBy`,
        `modifiedBy` As `modifiedBy`
    FROM `businesses_view`
    WHERE `businessId` = in_businessId;
END