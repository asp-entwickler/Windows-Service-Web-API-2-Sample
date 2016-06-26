using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Windows_Service_Web_API_2_Sample.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InfoController : ApiController
    {
        public string Get([FromUri] dynamic controllerParam)
        {
            var stringParam = Convert.ToString(controllerParam);
            var responceParam = string.IsNullOrEmpty(stringParam) || stringParam == "System.Object" ? " No parameters sent. " : " Parameter = " + stringParam;
            return "Windows Web API Sample Service On-Line (responced GET Method). " + responceParam;
        }

        public string Post([FromBody] dynamic controllerParam)
        {
            var stringParam = Convert.ToString(controllerParam);
            var responceParam = string.IsNullOrEmpty(stringParam) ? " No parameters sent. " : " Parameter = " + stringParam;
            return "Windows Web API Sample Service On-Line (responced POST Method). " + responceParam;
        }

    }

}