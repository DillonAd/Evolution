DROP PROCEDURE IF EXISTS PROC_INSERT_APP;
GO
CREATE PROCEDURE PROC_INSERT_APP (
	@I_APP_ID INTEGER, 
	@I_APP_DESCRIPTION VARCHAR(4000)
)
AS
BEGIN
SET NOCOUNT ON;
	
	INSERT INTO APP_TABLE (APP_ID, APP_DESCRIPTION)
	VALUES (@I_APP_ID, @I_APP_DESCRIPTION);

END;
GO