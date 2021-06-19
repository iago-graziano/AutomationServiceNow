using AutomationServiceNow.Model.Model;
using System.Collections.Generic;

namespace AutomationServiceNow.Business.Interface
{
    public interface IJobBusiness
    {
        IList<JobModel> FetchJobsWithError();
    }
}
