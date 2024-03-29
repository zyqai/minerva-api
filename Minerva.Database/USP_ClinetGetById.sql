-- DROP PROCEDURE `USP_ClinetGetById`
DELIMITER //
CREATE PROCEDURE `USP_ClinetGetById`(
    IN p_clientId INT
)
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
    FROM `_clients` WHERE `clientId` = p_clientId;
END
//

DELIMITER ;
