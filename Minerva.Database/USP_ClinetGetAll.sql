-- DROP PROCEDURE `USP_ClinetGetAll`
DELIMITER //
CREATE PROCEDURE `USP_ClinetGetAll`()
BEGIN
    SELECT 
clientId
,userId
,tenantId
,clientName
,firstName
,lastName
,dob
,socialsecuritynumber
,postalnumber
,stateid
,clientAddress
,phoneNumber
,email
,preferredContact
,creditScore
,lendabilityScore
,marriedStatus
,spouseClientId
,rootFolder
,createdTime
,modifiedTime
,createdBy
,modifiedBy
,City
,clientAddress1
    FROM `_clients`;
END
//

DELIMITER ;
