using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CamList.Controllers
{
    [Route("api/[controller]")]
    public class IndoorController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var Configuration = builder.Build();

            var files = Directory.GetFiles(Configuration["BasePath"] + "Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record");
            return files ;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
