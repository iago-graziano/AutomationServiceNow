using AutomationServiceNow.Model.Model;
using AutomationServiceNow.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AutomationServiceNow.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var requests = new Requests();
            await requests.DownloadLogFile("http://dppshml02.dpsp.int:8000/psreports/fdpsphml/16825136/AE_DPSP_RPR_MCD_49702570.AET");
            JobRepository jobRepository = new JobRepository();
            var jobsList = jobRepository.FetchJobsWithError();

            foreach (var job in jobsList)
            {
                if (job.Url == null)
                {
                    var ticket = new TicketModel();
                    ticket.assignment_group = "F-WIPRO-SUST-SUSTENTACAO_ERP-WIPRO";
                    ticket.caller_id = "Valmir Tavares - Wipro";
                    ticket.category = "Redespacho";
                    ticket.subcategory = "Redespacho de Ressuprimento";
                    ticket.description = "Chamado de Teste";
                    ticket.short_description = "PeopleSoft Sistemas - Problemas Gerais";
                    ticket.u_dsp_affected_person = "Valmir Tavares - Wipro";

                    //requests.OpenServiceNowTicket(ticket);
                }
                else
                {

                }
            }
        }

        private static async Task DownloadFile(string url, Cookie authCookie)
        {
            //HttpClient client = new HttpClient(httpClientHandler);
            //Uri baseUri = new Uri(baseUrl);
            //client.BaseAddress = baseUri;
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.ConnectionClose = true;

            //string basicAuthBase64 = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(string.Format("{0}:{1}", userName, password)));
            //request.Headers.Add("Proxy-Authorization", string.Format("Basic {0}", basicAuthBase64));

            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

            //var values = new List<KeyValuePair<string, string>>();
            //values.Add(new KeyValuePair<string, string>("userid", "WBOLIVEIRA"));
            //values.Add(new KeyValuePair<string, string>("pwd", "Salmo*127"));

            //var content = new FormUrlEncodedContent(values);

            //var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUrl);
            //requestMessage.Content = content;

            //var task = client.SendAsync(requestMessage);
            //var response = task.Result;
            //response.EnsureSuccessStatusCode();

            //var cookie = new Cookie();
            //if (cookieContainer.Count > 0)
            //{
            //    var cookies = cookieContainer.GetCookies(baseUri); /*"wFz2gMmphzmTn6ZRfB0Q9yB1jsvHKNXz!761307648"*/
            //    cookie = cookies["dppshml02-8000-PORTAL-PSJSESSIONID"];
            //}

            ////Call
            //await DownloadFile("http://dppshml02.dpsp.int:8000/psreports/fdpsphml/16825136/AE_DPSP_RPR_MCD_49702570.AET", cookie);
            //var cookieContainer = new CookieContainer();
            //var uri = new Uri(url);
            //var httpClientHandler = new HttpClientHandler
            //{
            //    CookieContainer = cookieContainer,
            //    UseCookies = true
            //};

            //cookieContainer.Add(uri, authCookie);

            //try
            //{
            //    using (HttpClient client = new HttpClient(httpClientHandler))
            //    {
            //        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

            //        using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            //        using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
            //        {
            //            string fileToWriteTo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SAMPLE.AET");
            //            using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
            //            {
            //                await streamToReadFrom.CopyToAsync(streamToWriteTo);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }
    }
}