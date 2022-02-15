using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OBiletTask.RequestModel.GetBusJourneys;
using OBiletTask.RequestModel.GetBusLocations;
using OBiletTask.RequestModel.GetSessions;
using OBiletTask.ResponseModel.GetBusJourneys;
using OBiletTask.ResponseModel.GetBusLocations;
using OBiletTask.ResponseModel.GetSessions;
using OBiletTask.UI.Extensions;
using OBiletTask.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static OBiletTask.RequestModel.GetSessions.GetSession;

namespace OBiletTask.UI.Controllers
{
    public class HomeController : Controller
    {


        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {

            var getSessionId = _httpContextAccessor.HttpContext.Session.GetString("SessionId");
            var getDeviceId = _httpContextAccessor.HttpContext.Session.GetString("DeviceId");
            if (getSessionId is null || getDeviceId is null)
            {
                Connection con = new Connection()
                {
                    IpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                    port = "5117"
                };
                Application app = new Application()
                {
                    version = "1.0.0.0",
                    EquipmentId = "distribusion"
                };

                Browser b = new Browser()
                {
                    name = "chrome",
                    version = "47.0.0.12",
                };
                GetSession session = new GetSession()
                {
                    application = app,
                    connection = con,
                    browser = b,
                    type = 7
                };

                var responseGetSession = await ApiRequest.GetDataFromApis(_httpClientFactory, session, "client/getsession");

                if (responseGetSession.IsError)
                {
                    ViewBag.ErrorMessage = responseGetSession.Message;
                    return View();
                }
                else
                {
                    if (responseGetSession.data != null)
                    {
                        var data = JsonConvert.DeserializeObject<GetSessionsResponseModel>(responseGetSession.data.ToString());

                        var keyUserId = "SessionId";
                        _httpContextAccessor.HttpContext.Session.SetString(keyUserId, data.SessionId);

                        var keyGroupId = "DeviceId";
                        _httpContextAccessor.HttpContext.Session.SetString(keyGroupId, data.DeviceId);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = responseGetSession.Message;
                        return View();
                    }

                }

            }
            var date = Request.Cookies["DepartureDateCookie"];

            ViewBag.CookieDepartureDate = Convert.ToDateTime(date).ToString("yyyy-MM-dd");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAllBusLocations()
        {
            var CookieOriginLocation = Request.Cookies["OriginLocationCookie"];
            var CookieOriginId = Request.Cookies["OriginIdCookie"];
            var CookieDestinationLocation = Request.Cookies["DestinationLocationCookie"];
            var CookieDestinationId = Request.Cookies["DestinationIdCookie"];


            var getSessionId = _httpContextAccessor.HttpContext.Session.GetString("SessionId");
            var getDeviceId = _httpContextAccessor.HttpContext.Session.GetString("DeviceId");

            if (string.IsNullOrEmpty(getSessionId) || string.IsNullOrEmpty(getDeviceId))
            {
                return Json(new { failed = true, message = "Session alınamadı, lütfen tekrar deneyiniz!", });
            }

            var responseData = new List<GetBusLocationsResponseModel>();
            DeviceSession device = new DeviceSession()
            {
                DeviceId = getDeviceId,
                SessionId = getSessionId,
            };
            GetBusLocationsRequestModel busLocationRequest = new GetBusLocationsRequestModel()
            {
                data = null,
                date = DateTime.Now,
                DeviceSession = device,
                language = "tr-TR"
            };

            var responseGetBusLocation = await ApiRequest.GetDataFromApis(_httpClientFactory, busLocationRequest, "location/getbuslocations");

            if (responseGetBusLocation.IsError)
            {

                return Json(new { failed = true, message = responseGetBusLocation.Message, locationList = responseData });
            }
            else
            {

                if (responseGetBusLocation.data != null)
                {
                    responseData = JsonConvert.DeserializeObject<List<GetBusLocationsResponseModel>>(responseGetBusLocation.data.ToString());
                    return Json(new { list = responseData, originId = CookieOriginId, originLocation = CookieOriginLocation, destinationId = CookieDestinationId, destinationLocation = CookieDestinationLocation });
                }
                else
                {
                    return Json(new { failed = true, message = responseGetBusLocation.Message });
                }


            }
        }


        [HttpPost]
        public async Task<IActionResult> GetAllBusLocationsSearch(string search)
        {

            var getSessionId = _httpContextAccessor.HttpContext.Session.GetString("SessionId");

            var getDeviceId = _httpContextAccessor.HttpContext.Session.GetString("DeviceId");
            if (getSessionId is null || getDeviceId is null)
            {
                return Json(new { failed = "SessionEmpty", message = "Hata oluştu", });
            }


            var responseData = new List<GetBusLocationsResponseModel>();
            DeviceSession device = new DeviceSession()
            {
                DeviceId = getDeviceId,
                SessionId = getSessionId,
            };
            GetBusLocationsRequestModel busLocationRequest = new GetBusLocationsRequestModel()
            {
                data = search,
                date = DateTime.Now,
                DeviceSession = device,
                language = "tr-TR"

            };

            var responseGetBusLocation = await ApiRequest.GetDataFromApis(_httpClientFactory, busLocationRequest, "location/getbuslocations");

            if (responseGetBusLocation.IsError)
            {

                return Json(new { failed = true, message = "Hata oluştu", locationList = responseData });
            }
            else
            {
                responseData = JsonConvert.DeserializeObject<List<GetBusLocationsResponseModel>>(responseGetBusLocation.data.ToString());
                return Json(responseData);
            }
        }



        public async Task<IActionResult> GetBusJournerysViewList(GetBusJourneysRequestModel.Data model)
        {

            if (model != null)
            {
                var getSessionId = _httpContextAccessor.HttpContext.Session.GetString("SessionId");
                var getDeviceId = _httpContextAccessor.HttpContext.Session.GetString("DeviceId");
                if (getSessionId is null || getDeviceId is null)
                {
                    return RedirectToAction("Index", "Home");
                }


                var responseData = new List<GetBusJourneysResponseModel>();
                DeviceSession device = new DeviceSession()
                {
                    DeviceId = getDeviceId,
                    SessionId = getSessionId,
                };

                GetBusJourneysRequestModel busJourneysReuqest = new GetBusJourneysRequestModel()
                {
                    date = DateTime.Now,
                    data = model,
                    DeviceSession = device,
                    language = "tr-TR"

                };

                var responseGetBusJournyes = await ApiRequest.GetDataFromApis(_httpClientFactory, busJourneysReuqest, "journey/getbusjourneys");

                if (responseGetBusJournyes.IsError)
                {
                    ViewBag.ErrorMessage = responseGetBusJournyes.Message;
                    return View();

                }
                else
                {
                    if (HttpContext.Request.Cookies.ContainsKey("OriginLocationCookie") || HttpContext.Request.Cookies.ContainsKey("OriginIdCookie") || HttpContext.Request.Cookies.ContainsKey("DestinationLocationCookie") || HttpContext.Request.Cookies.ContainsKey("DestinationIdCookie") || HttpContext.Request.Cookies.ContainsKey("DepartureDateCookie"))
                    {
                        HttpContext.Response.Cookies.Delete("OriginLocationCookie");
                        HttpContext.Response.Cookies.Delete("OriginIdCookie");
                        HttpContext.Response.Cookies.Delete("DestinationLocationCookie");
                        HttpContext.Response.Cookies.Delete("DestinationIdCookie");
                        HttpContext.Response.Cookies.Delete("DepartureDateCookie");

                    }

                    HttpContext.Response.Cookies.Append("OriginLocationCookie", $"{model.OriginLocation}", new CookieOptions { Expires = DateTime.Now.AddDays(2) });
                    HttpContext.Response.Cookies.Append("OriginIdCookie", $"{model.OriginId}", new CookieOptions { Expires = DateTime.Now.AddDays(2) });
                    HttpContext.Response.Cookies.Append("DestinationLocationCookie", $"{model.DestinationLocation}", new CookieOptions { Expires = DateTime.Now.AddDays(2) });
                    HttpContext.Response.Cookies.Append("DestinationIdCookie", $"{model.DestinationId}", new CookieOptions { Expires = DateTime.Now.AddDays(2) });
                    HttpContext.Response.Cookies.Append("DepartureDateCookie", $"{model.DepartureDate}", new CookieOptions { Expires = DateTime.Now.AddDays(2) });




                    responseData = JsonConvert.DeserializeObject<List<GetBusJourneysResponseModel>>(responseGetBusJournyes.data.ToString());
                    return View(responseData);

                }

            }
            return View();
        }



    }
}
