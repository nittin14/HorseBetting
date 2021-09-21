using System;

namespace VKATalkClassLayer
{
    public class RaceTimings
    {
        public string RaceTimingType { get; set; }
        public int CenterID { get; set; }
        public int FromYearID { get; set; }
        public int TillYearID { get; set; }
        public int FromSeasonID { get; set; }
        public int TillSeasonID { get; set; }
        public int TrackID { get; set; }
        public int DistanceID { get; set; }
        public string RaceType { get; set; }
        public int ClassID { get; set; }
        public string RaceStatus { get; set; }
        public string RaceDate { get; set; }
        public int HorseNameID { get; set; }
        public string CarriedWeight { get; set; }
        public string PenetrometerReading { get; set; }
        public string FalseRails { get; set; }
        public string Timing { get; set; }
    }
}
