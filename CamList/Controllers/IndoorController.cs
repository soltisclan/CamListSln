using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using CamList.Models;

namespace CamList.Controllers
{
    [Route("api/[controller]")]
    public class IndoorController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Video> Get()
        {

            var videos = new List<Video>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var Configuration = builder.Build();

            var files = Directory.GetFiles(Configuration["BasePath"] + "Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record");


            foreach (string file in files)
            {
                var video = new Video(file);
                videos.Add(video);
            }

            return videos ;
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
