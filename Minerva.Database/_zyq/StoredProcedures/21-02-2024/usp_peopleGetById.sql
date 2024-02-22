CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_peopleGetById`(IN p_peopleId int)
BEGIN
    SELECT 
peopleId
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
    FROM `people_view`
    WHERE peopleid=p_peopleId ;
END