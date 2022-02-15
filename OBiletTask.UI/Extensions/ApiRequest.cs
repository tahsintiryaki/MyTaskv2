using Newtonsoft.Json;
using OBiletTask.Common;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OBiletTask.UI.Extensions
{
    public static class ApiRequest
    {
        public static async Task<BaseResponseModel> GetDataFromApis(IHttpClientFactory clientFactory, object content, string endPoint, string httpMethodParameter = "POST")
        {
            var httpMethodType = new HttpMethod(httpMethodParameter);

            var request = new HttpRequestMessage(httpMethodType, endPoint);
            var client = clientFactory.CreateClient("MyTask");
            var json = JsonConvert.SerializeObject(content);

            using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                request.Content = stringContent;


                var token = client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Basic", "JEcYcEMyantZV095WVc3G2JtVjNZbWx1");


                using (var response = await client
                       .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                       .ConfigureAwait(false))

                {
                    var isSuccessStatusCode = response.IsSuccessStatusCode;


                    var resultContent = new BaseResponseModel();

                    //authorize 401 donunce EnsureSuccessStatusCode kodunda patlıyor.
                    string resultContentString = await response.Content.ReadAsStringAsync();

                    if (resultContentString != "")
                    {
                        resultContent = JsonConvert.DeserializeObject<BaseResponseModel>(resultContentString);
                        if (resultContent.Status != "Success")
                        {
                            resultContent.IsError = true;
                            resultContent.Message = resultContent.Message;
                        }
                    }
                    else
                    {
                        resultContent.IsError = true;
                        resultContent.Message = "Api ile bağlantı kurulamadı";
                    }


                  

                    return resultContent;

                }

            }

        }



    }
}