using System;
using System.Data;
using VKATalkDb;

namespace VKATalkBusinessLayer
{
    public class ReportBL
    {
        public DataSet GetRaceCardReport(string racedate, int centerid)
        {
            return new ReportDL().GetRaceCardReport(racedate, centerid);
        }

        public DataSet GetRaceGuide(string racedate, int centerid, int raceid)
        {
            return new ReportDL().GetRaceGuide(racedate, centerid, raceid);
        }

        public DataSet GetHorseFamily(string horsenameid, string racedate)
        {
            return new ReportDL().GetHorseFamily(horsenameid, racedate);
        }

        public DataTable GetTotalRaceDetail(string centerid, string racedate)
        {
            return new ReportDL().GetTotalRaceDetail(centerid, racedate);
        }

            public DataSet GetHorseFamilyMoreDetail(string horseid, string racedate, int raceid)
        {
            return new ReportDL().GetHorseFamilyMoreDetail(horseid, racedate, raceid);
        }

        public DataSet GetReportSwimmingTrckViet(string horseid, string racedate, string racedate2)
        {
            return new ReportDL().GetReportSwimmingTrckViet(horseid, racedate, racedate2);
        }

        public DataSet GetHorsePerformance(int horseid, string racedate)
        {
            return new ReportDL().GetHorsePerformance(horseid, racedate);
        }
    }
}
