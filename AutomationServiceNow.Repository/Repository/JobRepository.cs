using AutomationServiceNow.Model.Interface;
using AutomationServiceNow.Model.Model;
using AutomationServiceNow.Repository.DataAccess;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace AutomationServiceNow.Repository.Repository
{
    public class JobRepository : IJobRepository
    {
        public IList<JobModel> FetchJobsWithError()
        {
            var query = $@"SELECT R.PRCSINSTANCE as Process
                 , R.PRCSJOBSEQ as JobSeq
                 , R.PRCSITEMLEVEL
                 , R.JOBINSTANCE as Job
                 , R.MAINJOBINSTANCE
                 , R.PRCSJOBNAME as JobName
                 , R.GENPRCSTYPE
                 , R.PRCSNAME as ProcessName
                 , R.PRCSTYPE as ProcessType
                 , R.RUNSTATUS as RunStatus
                 , R.DISTSTATUS
                 , R.RESTARTENABLED
                 , R.RETRYCOUNT as RetryCount
                 , R.OUTDESTTYPE
                 , R.SERVERNAMERQST
                 , R.OPSYS
                 , R.RUNSERVEROPTION
                 , R.MAINJOBSEQ
                 , R.USERNOTIFIED
                 , R.PRCSWINPOP
                 , R.MCFREN_URL_ID
                 , DECODE(R.RUNSTATUS, '1', 'CANCELAR'
                         , '2', 'EXCLUIDO'
                         , '3', 'ERRO'
                         , '4', 'SUSPENSO'
                         , '5', 'EM FILA'
                         , '6', 'INICIADO'
                         , '7', 'EM PROCESSAMENTO'
                         , '8', 'CANCELADO'
                         , '9', 'COM EXITO'
                         , '10', 'SEM EXITO'
                         , '16', 'PENDENTE'
                         , '17', 'EXITO COM ALERTA') as Status,
                         (SELECT A.URL || '/' || A.CONTENTID || '/' || B.FILENAME
                            FROM FDSPPRD.PS_CDM_LIST_VW A, FDSPPRD.PS_CDM_FILELIST_VW B
                            WHERE A.PRCSINSTANCE = R.PRCSINSTANCE
                            AND   B.PRCSINSTANCE = A.PRCSINSTANCE
                            AND   B.CDM_FILE_TYPE = 'AET') Url
                FROM FDSPPRD.PSPRCSQUE R
                WHERE R.RUNSTATUS IN('3','10')
                ORDER
                BY R.PRCSITEMLEVEL ASC, R.JOBINSTANCE ASC, R.PRCSJOBSEQ ASC";

            using (var dbConn = new OracleDataAccess())
            {
                dbConn.OpenConnection();
                return dbConn.Connection.Query<JobModel>(query).ToList();
            }
        }
    }
}
