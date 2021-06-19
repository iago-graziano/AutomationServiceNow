using System;
using System.Net.Http;
using System.Text;
using System.Configuration;
using System.Net.Http.Headers;
using AutomationServiceNow.Model.Model;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace AutomationServiceNow.Console
{
    public class Requests
    {
        public void OpenServiceNowTicket(TicketModel ticket)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;
            
            var authenticationString = $"{ConfigurationManager.AppSettings["serviceNowUser"]}:{ConfigurationManager.AppSettings["serviceNowPassword"]}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

            var task = client.PostAsJsonAsync(ConfigurationManager.AppSettings["serviceNowUrl"], ticket);
            var response = task.Result;
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> DownloadLogFile(string url)
        {
            var cookie = await GetAuthCookie();

            var cookieContainer = new CookieContainer();
            var uri = new Uri(url);
            var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = cookieContainer,
                UseCookies = true
            };

            cookieContainer.Add(uri, cookie);

            try
            {
                using (HttpClient client = new HttpClient(httpClientHandler))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ConnectionClose = true;

                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    client.DefaultRequestHeaders.Add("Accept", "text/html");

                    using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                    using (GZipStream streamToReadFrom = new GZipStream(await response.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                    {
                        string fileToWriteTo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SAMPLE.AET");
                        using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                        {
                            await streamToReadFrom.CopyToAsync(streamToWriteTo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        public async Task<Cookie> GetAuthCookie()
        {
            var cookieContainer = new CookieContainer();
            var uri = new Uri(ConfigurationManager.AppSettings["loginUrl"]);
            var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = cookieContainer,
                UseCookies = true
            };

            HttpClient client = new HttpClient(httpClientHandler);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = new StringContent($"userid={ConfigurationManager.AppSettings["peopleSoftUser"]}&pwd={ConfigurationManager.AppSettings["peopleSoftPassword"]}",
                                    Encoding.UTF8,
                                    "application/x-www-form-urlencoded");

            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            if (cookieContainer.Count > 0)
            {
                var cookies = cookieContainer.GetCookies(uri);
                return cookies["PS_TOKEN"];
            }

            return null;
        }
    }
}
