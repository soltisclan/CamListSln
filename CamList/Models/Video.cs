using System;

namespace CamList.Models
{
    public class Video {

        public long Id {get; set;}
        public string Name {get; set;}
        public DateTime Date {get; set;}

        public Video(string filename) {

                // "S:\\t\\Amcrest\\Outdoor\\AMC0200KBE29V0X8B2\\2017-10-16\\001\\dav\\21\\21.06.24-21.06.47[M][0@0][0].mp4"

                var filenameparts = filename.Split("//");
                var date = filenameparts[3];

                Id = 1;
                Name = "";
                Date = DateTime.Today;
        }
    }

}