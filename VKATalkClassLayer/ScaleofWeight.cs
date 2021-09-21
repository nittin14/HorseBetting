using System;

namespace VKATalkClassLayer
{
    public class ScaleofWeight
    {
        public Int32 CenterID { get; set; }
        public Int32 FromYearID { get; set; }
        public Int32 TillYearID { get; set; }
        public Int32 FromSeasonID { get; set; }
        public Int32 TillSeasonID { get; set; }
        public string ScaleofWeightType { get; set; }
        public string CIHandicapRating { get; set; }
        public string CIIHandicapRating { get; set; }
        public string CIIIHandicapRating { get; set; }
        public string CIVHandicapRating { get; set; }
        public string CVHandicapRating { get; set; }

        public string WeightSystemType { get; set; }
        public string Month { get; set; }
        public string DistanceParameter { get; set; }

        public Int32 NationID { get; set; }
        public string HandicapWeight { get; set; }
        public string AgeHandicapWeight { get; set; }
        public string AgeParameter { get; set; }

		public string HorseGender { get; set; }

		public string HorseHandicapWeight { get; set; }

		public string AgeCondition { get; set; }
	}
}
