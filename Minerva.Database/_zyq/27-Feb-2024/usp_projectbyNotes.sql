CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_projectbyNotes`(IN in_projectId INT)
BEGIN
	select projectNotesId, projectId, tenantId, notes, createdByUserId, createdOn, firstname from notes_view
	where Projectid = in_projectid;
END