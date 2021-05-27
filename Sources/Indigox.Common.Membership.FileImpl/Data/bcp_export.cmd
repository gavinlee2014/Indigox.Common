@echo off

SET db=D_EIP_UUM
SET server="192.168.0.61"
SET username=sa
SET password=P@ssw0rd


SET table=v_allprincipals

BCP "DECLARE @colnames VARCHAR(max);SELECT @colnames = COALESCE(@colnames + '	', '') + column_name from %db%.INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='%table%'; select @colnames;" queryout "%table%_header.csv" -c -S %server% -U %username% -P %password%

BCP %db%..%table% out "%table%_rows.csv" -c -S %server% -U %username% -P %password%

COPY /B %table%_header.csv+%table%_rows.csv member.csv

DEL %table%_header.csv
DEL %table%_rows.csv



SET table=membership


BCP "DECLARE @colnames VARCHAR(max);SELECT @colnames = COALESCE(@colnames + '	', '') + column_name from %db%.INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='%table%'; select @colnames;" queryout "%table%_header.csv" -c -S %server% -U %username% -P %password%

BCP %db%..%table% out "%table%_rows.csv" -c -S %server% -U %username% -P %password%

COPY /B %table%_header.csv+%table%_rows.csv %table%.csv

DEL %table%_header.csv
DEL %table%_rows.csv




@pause
