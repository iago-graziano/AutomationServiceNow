using AutomationServiceNow.Business.Interface;
using AutomationServiceNow.Model.Interface;
using AutomationServiceNow.Model.Model;
using System.Collections.Generic;

namespace AutomationServiceNow.Business.Concrete
{
    public class JobBusiness : IJobBusiness
    {
        private readonly IJobRepository jobRepository;

        public JobBusiness(IJobRepository _jobRepository)
        {
            jobRepository = _jobRepository;
        }

        public IList<JobModel> FetchJobsWithError()
        {
            return jobRepository.FetchJobsWithError();
        }
    }
}