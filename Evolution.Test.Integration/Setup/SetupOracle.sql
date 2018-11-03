WHENEVER SQLERROR EXIT SQL.SQLCODE
WHENEVER OSERROR EXIT FAILURE

create user c##appUser identified by appPassword
/
grant dba to c##appUser
/
grant create session to c##appUser
/