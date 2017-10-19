using NUnit.Framework;
namespace CamListTest
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        [TestCase("S:\\t\\Amcrest\\Outdoor\\AMC0200KBE29V0X8B2\\2017-10-16\\001\\dav\\21\\21.06.24-21.06.47[M] [0@0] [0].mp4", "S:\\t\\Amcrest\\Outdoor\\AMC0200KBE29V0X8B2\\2017-10-16\\001\\dav\\21\\21.06.24-21.06.47[M] [0@0] [0].mp4")]
        [TestCase("C:\\Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record\\MDalarm_20171016_214239.mkv", "C:\\Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record\\MDalarm_20171016_214239.mkv")]
        [TestCase("S:\\Amcrest\\Outdoor\\AMC0200KBE29V0X8B2\\2017-10-16\\001\\dav\\21\\21.06.24-21.06.47[M] [0@0] [0].mp4", "S:\\Amcrest\\Outdoor\\AMC0200KBE29V0X8B2\\2017-10-16\\001\\dav\\21\\21.06.24-21.06.47[M] [0@0] [0].mp4")]
        [TestCase("C:\\t\\Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record\\MDalarm_20171016_214239.mkv", "C:\\t\\Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record\\MDalarm_20171016_214239.mkv")]
        public void ParseFullPath(string file, string result)
        {
            var video = new CamList.Models.Video(file);
            Assert.AreEqual(result, video.FullPath);

        }

        [Test]
        [TestCase("S:\\t\\Amcrest\\Outdoor\\AMC0200KBE29V0X8B2\\2017-10-16\\001\\dav\\21\\21.06.24-21.06.47[M] [0@0] [0].mp4", "2017-10-16T21:06:24")]
        [TestCase("C:\\Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record\\MDalarm_20171016_214239.mkv", "2017-10-16T21:42:39")]
        [TestCase("S:\\Amcrest\\Outdoor\\AMC0200KBE29V0X8B2\\2017-10-16\\001\\dav\\21\\21.06.24-21.06.47[M] [0@0] [0].mp4", "2017-10-16T21:06:24")]
        [TestCase("C:\\t\\Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record\\MDalarm_20171016_214239.mkv", "2017-10-16T21:42:39")]
        public void ParseDate(string file, string result)
        {
            var video = new CamList.Models.Video(file);

            var resultdate = System.DateTime.Parse(result);

            Assert.AreEqual(resultdate, video.Date);

        }
        [Test]
        [TestCase("S:\\t\\Amcrest\\Outdoor\\AMC0200KBE29V0X8B2\\2017-10-16\\001\\dav\\21\\21.06.24-21.06.47[M] [0@0] [0].mp4", "21.06.24-21.06.47[M] [0@0] [0].mp4")]
        [TestCase("C:\\Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record\\MDalarm_20171016_214239.mkv", "MDalarm_20171016_214239.mkv")]
        [TestCase("S:\\Amcrest\\Outdoor\\AMC0200KBE29V0X8B2\\2017-10-16\\001\\dav\\21\\21.06.24-21.06.47[M] [0@0] [0].mp4", "21.06.24-21.06.47[M] [0@0] [0].mp4")]
        [TestCase("C:\\t\\Amcrest\\Indoor\\FI9821W_C4D6553CA13C\\record\\MDalarm_20171016_214239.mkv", "MDalarm_20171016_214239.mkv")]
        public void ParseName(string file, string result)
        {
            var video = new CamList.Models.Video(file);

            Assert.AreEqual(result, video.Name);

        }
    }
}
