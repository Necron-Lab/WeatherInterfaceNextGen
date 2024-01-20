using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WeatherInterface.Data
{
    public class SendPost
    {

        
        public async Task MakePostRequest(bool state)
        {
            var apiUrl = "http://localhost:5000/api/changestate";
            int responseCode;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var requestBody = new RequestBody
                    {
                        State = state
                    };

                    var jsonBody = JsonConvert.SerializeObject(requestBody);

                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    var fetcherresponse = await httpClient.PostAsync(apiUrl, content);

                    responseCode = (int) fetcherresponse.StatusCode;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }
        }
    }
}
