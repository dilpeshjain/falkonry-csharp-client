using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using falkonry_csharp_client.helper.models;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using log4net;

namespace falkonry_csharp_client.service
{
    public class HttpService
    {
        private readonly string _host;
        private readonly string _token;
        private readonly string _defaultHeder;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        public bool RemoteCertificateValidationCallback(

                  object sender,

                  X509Certificate certificate,

                  X509Chain chain,

                  SslPolicyErrors sslPolicyErrors

           )
        { return true; }


        public HttpService(string host, string token, SortedDictionary<string, string> _piOptions = null)
        {


            _host = host ?? "https://service.falkonry.io";

            _token = token;

            if(_piOptions != null && _piOptions.Count > 0 && _piOptions["header"] != null)
            {
                _defaultHeder = _piOptions["header"];
            }
            else
            {
                _defaultHeder = "c-sharp-client";
            }

                ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidationCallback;
        }

        public string HandleGetReponse(HttpWebRequest request)
        {
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    throw new FalkonryException("Request Time Out");
                }else if(e.Status == WebExceptionStatus.NameResolutionFailure || e.Status == WebExceptionStatus.ProxyNameResolutionFailure)
                {
                    throw new FalkonryException("Host unreachable");
                }
                response = (HttpWebResponse)e.Response;
            }
            try
            {
               var stream = response.GetResponseStream();
                if (stream != null)
                {
                    var resp = new StreamReader(stream).ReadToEnd();

                    if (Convert.ToInt32(response.StatusCode) == 401)
                    {
                        throw new FalkonryException("Unauthorized : Invalid token " + Convert.ToString(response.StatusCode));
                    }
                    else if (Convert.ToInt32(response.StatusCode) < 400 || Convert.ToInt32(response.StatusCode) == 409)
                    {
                        return resp;
                    }
                    else
                    {
                        try
                        {
                            ErrorMessage errMsgJson = new ErrorMessage();
                            errMsgJson = JsonConvert.DeserializeObject<ErrorMessage>(resp);
                            if (errMsgJson.Message.Length > 0)
                            {
                                throw new FalkonryException(Convert.ToString(errMsgJson.Message));
                            }
                            else
                            {
                                throw new FalkonryException("Internal Server Error.");
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }else
                {
                    throw new FalkonryException("Internal Server Error.");
                }
            }catch(Exception exception1)
            {
                throw;
            }
        }

        public string Get(string path)
        {
            try
            {
                var url = _host + path;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer " + _token);
                request.Headers.Add("x-falkonry-source", _defaultHeder);
                request.Method = "GET";
                request.ContentType = "application/json";

                return HandleGetReponse(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Post(string path, string data)
        {
            try
            {
                var url = _host + path;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer " + _token);
                request.Headers.Add("x-falkonry-source", _defaultHeder);
                request.Method = "POST";
                request.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(data);

                    streamWriter.Flush();

                    streamWriter.Close();
                }

                return HandleGetReponse(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Put(string path, string data)
        {
            try
            {
                var url = _host + path;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer " + _token);
                request.Headers.Add("x-falkonry-source", _defaultHeder);
                request.Method = "PUT";
                request.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(data);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                return HandleGetReponse(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> Fpost(string path, SortedDictionary<string, string> options, byte[] stream)
        {

            try
            {
                var rnd = new Random();
                var randomNumber = Convert.ToString(rnd.Next(1, 200));
                var url = _host + path;

                string sd;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                var client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
                client.DefaultRequestHeaders.Add("x-falkonry-source", _defaultHeder);
                client.DefaultRequestHeaders.ExpectContinue = false;
                using (var form = new MultipartFormDataContent())
                {
                    form.Add(new StringContent(options["name"]), "name");

                    form.Add(new StringContent(options["timeIdentifier"]), "timeIdentifier");

                    form.Add(new StringContent(options["timeFormat"]), "timeFormat");

                    if (stream != null)
                    {
                        var tempFileName = "input" + randomNumber + "." + options["fileFormat"];

                        var bytearraycontent = new ByteArrayContent(stream);
                        bytearraycontent.Headers.Add("Content-Type", "text/" + options["fileFormat"]);
                        form.Add(bytearraycontent, "data", tempFileName);
                    }

                    var result = client.PostAsync(url, form).Result;

                    sd = await result.Content.ReadAsStringAsync();
                }

                return sd;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Delete(string path)
        {
            try
            {
                var url = _host + path;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer " + _token);
                request.Headers.Add("x-falkonry-source", _defaultHeder);
                request.Method = "DELETE";
                request.ContentType = "application/json";
                return HandleGetReponse(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Upstream(string path, byte[] data)
        {
            try
            {
                var url = _host + path;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;

                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "POST";
                request.Headers.Add("Authorization", "Bearer " + _token);
                request.Headers.Add("x-falkonry-source", _defaultHeder);
                request.ContentType = "text/plain";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = data.Length;
                // Get the request stream.

                var dataStream = request.GetRequestStream();
                // Write the data to the request stream.

                dataStream.Write(data, 0, data.Length);
                // Close the Stream object.

                dataStream.Close();
                // Get the response.

                return HandleGetReponse(request);
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EventSource Downstream(string path)
        {
            try
            {
                var url = _host + path;
                var eventSource = new EventSource(url)
                {
                    Headers = new NameValueCollection { { "Authorization", "Bearer " + _token } }
                };

                eventSource.Connect();

                return eventSource;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string PostData(string path, string data)
        {
            try
            {
                var url = _host + path;

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer " + _token);
                request.Headers.Add("x-falkonry-source", _defaultHeder);
                request.Method = "POST";
                request.ContentType = "text/plain";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    //initiate the request
                    streamWriter.Write(data);

                    streamWriter.Flush();

                    streamWriter.Close();
                }

                return HandleGetReponse(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HttpResponse GetOutput(string path, string responseFormat)
        {
            var httpResponse = new HttpResponse();
            try
            {
                var url = _host + path;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer " + _token);
                request.Headers.Add("x-falkonry-source", _defaultHeder);
                request.Accept = responseFormat;
                request.Method = "GET";
                request.ContentType = "application/json";

                Log.Debug($"GetOutput: URL - {url}");
                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException e)
                {
                    response = (HttpWebResponse)e.Response;
                }

                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    var resp = new StreamReader(stream).ReadToEnd();
                    httpResponse.StatusCode = Convert.ToInt32(response.StatusCode);
                    httpResponse.Response = resp;
                }
                Log.Debug($"GetOutput: Response - {httpResponse.Response}");
                Log.Debug($"GetOutput: StatusCode - {httpResponse.StatusCode}");

                return httpResponse;
            }
            catch (Exception e)
            {
                httpResponse.StatusCode = 500;
                httpResponse.Response = e.Message;
                return httpResponse;
            }

        }
    }
}
