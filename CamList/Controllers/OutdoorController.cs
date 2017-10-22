using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using CamList.Models;

namespace CamList.Controllers
{
    [Route("api/[controller]/[action]")]
    public class OutdoorController : Controller
    {
        // GET api/values
        [HttpGet("{date?}")]
        public JsonResult List(string date = "")
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
                            if (date == "" || date == video.GetDate() )
                            {
                                videos.Add(video);
                            }
                        }
                    }
                }
            }

            return Json(videos);
        }

        [HttpGet("{key}")]
        public IActionResult File(string key)
        {
            var video = new Video(CiphererService.Decrypt(key));
            var stream = new FileStream(video.GetPath(), FileMode.Open, FileAccess.Read);
            return File(stream, "video/mp4", video.GetName());
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

        public static string DecryptString(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
