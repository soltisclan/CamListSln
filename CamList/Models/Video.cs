using System;

namespace CamList.Models
{
    public class Video {

        private long Id {get; set;}
        private string Name {get; set;}
        private string Path { get; set; }
        public DateTime Date {get; set;}
        public string Key { get; set; }
        public long Size { get; set; }

        public Video(string filename) {

            // "S:\\t\\Amcrest\\Outdoor\\AMC0200KBE29V0X8B2\\2017-10-16\\001\\dav\\21\\21.06.24-21.06.47[M][0@0][0].mp4"
            // "C:\\Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record\\MDalarm_20171016_214239.mkv"

            var filenameparts = filename
                        .Substring(filename.IndexOf("Amcrest"), filename.Length-filename.IndexOf("Amcrest"))
                        .Split("\\");
            String name;
            DateTime date;

            if (filename.Contains("Indoor"))
            {
                name = filenameparts[4];

                var datetimeparts = name.Substring(0,name.Length-4).Split("_");
                var time = name.Substring(0, name.Length - 4).Split("_")[2];

                var datetimestring = datetimeparts[1].Substring(0, 4) + "-" +
                                      datetimeparts[1].Substring(4, 2) + "-" +
                                      datetimeparts[1].Substring(6, 2) + "T" +
                                       time.Substring(0, 2) + ":" +
                                       time.Substring(2, 2) + ":" +
                                       time.Substring(4, 2);
                date = DateTime.Parse(datetimestring);

            }
            else
            {
                name = filenameparts[7];
                var time = name.Split("-")[0].Replace(".",":");

                date = DateTime.Parse(filenameparts[3]+"T"+time);
            }

            Id = 1;
            Name = name;
            Date = date;
            Path = filename;
            Key = CiphererService.Encrypt(filename);
            Size = new System.IO.FileInfo(filename).Length;
        }

        public string GetDate()
        {
            return Date.Date.ToString("yyyy-MM-dd");
        }

        public string GetName()
        {
            return Name;
        }
        public string GetPath()
        {
            return Path;
        }
    }
}