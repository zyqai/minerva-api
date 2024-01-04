
DELIMITER //
Create PROCEDURE USP_CreateBusiness(
	IN in_businessId INT,
    IN in_tenantId INT,
    IN in_businessName VARCHAR(255),
    IN in_businessAddress VARCHAR(255),
    IN in_businessType VARCHAR(45),
    IN in_industry VARCHAR(45),
    IN in_annualRevenue DECIMAL(6,2),
    IN in_incorporationDate DATE,
    IN in_businessRegistrationNumber VARCHAR(255),
    IN in_rootDocumentFolder VARCHAR(255)
)
BEGIN
    INSERT INTO `_businesses` (`businessId`,
        `tenantId`, `businessName`, `businessAddress`, `businessType`,
        `industry`, `annualRevenue`, `incorporationDate`,
        `businessRegistrationNumber`, `rootDocumentFolder`
    ) VALUES (in_businessId,
        in_tenantId, in_businessName, in_businessAddress, in_businessType,
        in_industry, in_annualRevenue, in_incorporationDate,
        in_businessRegistrationNumber, in_rootDocumentFolder
    );
END //
DELIMITER ;