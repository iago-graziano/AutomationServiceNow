using AutomationServiceNow.Model.Model;
using System.Collections.Generic;

namespace AutomationServiceNow.Model.Interface
{
    public interface IJobRepository
    {
        IList<JobModel> FetchJobsWithError();
    }
}