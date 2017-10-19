using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using CamList.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CamList.Controllers
{
    [Route("api/[controller]")]
    public class OutdoorController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Video> Get()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var Configuration = builder.Build();

            var days = Directory.GetDirectories(Configuration["BasePath"] + "Amcrest\\Outdoor\\AMC0200KBE29V0X8B2");

            var videos = new List<Video>();

            foreach (string day in days) {
                var hours = Directory.GetDirectories(day + "\\001\\dav");
                foreach (string hour in hours) {
                    var tmpFiles = Directory.GetFiles(hour);            
                    foreach (string file in tmpFiles) {
                        if (file.Substring(file.Length-4,4) == ".mp4") {
                            var video = new Video(file);
                            videos.Add(video);
                        }
                    }
                }
            }

            return videos;
        }

        // GET api/values/5
        [HttpGet("{filename}")]
        public FileStreamResult Get(string filename)
        {
            var video = new Video(Base64Decode(filename));
            var stream = new FileStream(video.FullPath, FileMode.Open);

            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = video.Name
            };
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

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
