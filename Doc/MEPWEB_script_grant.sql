USE sataconsulting_test
go

GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_acli TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_cito TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_crrg TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_olca TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_orpa TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_orpb TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_tatv TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_tbcp TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_tbpn TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_usr1 TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.mtxde00     TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.mtxge22     TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.mtxpa01     TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.mtxzz12     TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.PSC_CO01    TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.PSC_CO02    TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.PSC_CO03    TO mepweb
GRANT SELECT, INSERT, UPDATE, DELETE ON dba.flusso_macc TO mepweb

GRANT VIEW DEFINITION ON dba.flusso_acli                TO mepweb 
GRANT VIEW DEFINITION ON dba.flusso_cito                TO mepweb 
GRANT VIEW DEFINITION ON dba.flusso_crrg                TO mepweb 
GRANT VIEW DEFINITION ON dba.flusso_olca                TO mepweb 
GRANT VIEW DEFINITION ON dba.flusso_orpa                TO mepweb 
GRANT VIEW DEFINITION ON dba.flusso_orpb                TO mepweb 
GRANT VIEW DEFINITION ON dba.flusso_tatv                TO mepweb 
GRANT VIEW DEFINITION ON dba.flusso_tbcp                TO mepweb 
GRANT VIEW DEFINITION ON dba.flusso_tbpn                TO mepweb 
GRANT VIEW DEFINITION ON dba.flusso_usr1                TO mepweb 
GRANT VIEW DEFINITION ON dba.mtxde00                    TO mepweb 
GRANT VIEW DEFINITION ON dba.mtxge22                    TO mepweb 
GRANT VIEW DEFINITION ON dba.mtxpa01                    TO mepweb 
GRANT VIEW DEFINITION ON dba.mtxzz12                    TO mepweb 
GRANT VIEW DEFINITION ON dba.PSC_CO01                   TO mepweb 
GRANT VIEW DEFINITION ON dba.PSC_CO02                   TO mepweb 
GRANT VIEW DEFINITION ON dba.PSC_CO03                   TO mepweb 
GRANT VIEW DEFINITION ON dba.flusso_macc                TO mepweb 

GRANT SELECT ON dba.mvxpa01                             TO mepweb
GRANT SELECT ON dba.vs_pp_sprint_cons_COM_ISL           TO mepweb
GRANT SELECT ON dba.mvxzz12                             TO mepweb 
GRANT VIEW DEFINITION ON dba.vs_pp_sprint_cons_COM_ISL  TO mepweb 
GRANT VIEW DEFINITION ON dba.mvxpa01                    TO mepweb 
GRANT VIEW DEFINITION ON dba.mvxzz12                    TO mepweb 
go