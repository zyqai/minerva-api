CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_peopleGetAll`()
BEGIN
    SELECT 
clientId,userId,tenantId,clientName,firstName,lastName,dob,socialsecuritynumber,postalnumber,stateid,clientAddress
,phoneNumber,email,preferredContact,creditScore,lendabilityScore,marriedStatus,spouseClientId,rootFolder,createdTime,
modifiedTime,createdBy,modifiedBy,City,clientAddress1
    FROM `people_view`;
END