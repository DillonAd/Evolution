CREATE OR REPLACE PACKAGE C##TMPUSER.PACK_APP_TABLE
IS

  PROCEDURE UPDATE_APP_DESCRIPTION
  (
    I_APP_ID IN TMP_USER.APP_TABLE.APP_ID, 
	  I_APP_DESCRIPTION IN TMP_USER.APP_TABLE.APP_DESCRIPTION%TYPE
  );

END PACK_APP_TABLE;

/

CREATE OR REPLACE PACKAGE BODY C##TMPUSER.PACK_APP_TABLE
IS

  PROCEDURE DELETE_APP
  (
    I_APP_ID IN C##TMPUSER.APP_TABLE.APP_ID
  )
  AS
  BEGIN
    DELETE C##TMPUSER.APP_TABLE
	WHERE APP_ID = I_APP_ID;
  END;

END PACK_APP_TABLE;

/