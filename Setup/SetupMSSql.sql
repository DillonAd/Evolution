BEGIN TRY
  create user c##appUser identified by appPassword
  grant dba to c##appUser
  grant create session to c##appUser
END TRY
BEGIN CATCH
  SELECT ERROR_NUMBER() AS ErrorNumber;
END CATCH