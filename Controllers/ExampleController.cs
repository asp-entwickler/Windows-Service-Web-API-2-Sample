using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using Windows_Service_Web_API_2_Sample.App_Data;

namespace Windows_Service_Web_API_2_Sample.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExampleController : ApiController
    {
        public IHttpActionResult Get()
        {
            var cg = new CarsGarage();
            var items = cg.Cars.Select(i => i);
            return Ok(items);
        }

        public IHttpActionResult Get(int id)
        {
            var cg = new CarsGarage();
            var items = cg.Cars.Where(p => p.id == id);
            if (items.Count() == 0)
                return Content(HttpStatusCode.NotFound, "There are no car in the Garage with given index.");

            return Ok(items);
        }

    }
}
