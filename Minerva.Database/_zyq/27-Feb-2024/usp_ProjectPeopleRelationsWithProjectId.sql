CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_ProjectPeopleRelationsWithProjectId`(IN in_ProjectId int)
BEGIN
	select ProjectId, peopleId, userId, tenantId, clientName, clientAddress, phoneNumber, email, preferredContact,
			firstName, lastName, primaryBorrower, personaAutoId, projectPeopleId, personaId, 
			personaName, projectPersona 
	from projectpeoplerelation_view a
	Where Projectid=in_ProjectId;
END