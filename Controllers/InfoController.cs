using System.Web.Http;
using System.Web.Http.Cors;

namespace Windows_Service_Web_API_2_Sample.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InfoController : ApiController
    {
        public string Get()
        {
            return "Windows Web API Sample Service On-Line (responced GET Method)";
        }

        public string Get(string param)
        {
            return "Windows Web API Sample Service On-Line (responced GET Method). Parameter = " + param;
        }

        public string Post()
        {
            return "Windows Web API Sample Service On-Line (responced POST Method)";
        }

        public string Post(string param)
        {
            return "Windows Web API Sample Service On-Line (responced POST Method). Parameter = " + param;
        }

    }

}