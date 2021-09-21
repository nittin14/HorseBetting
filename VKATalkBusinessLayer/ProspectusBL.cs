using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKATalkBusinessLayer
{
    using System.Data;

    using VKATalkDb;

    public class ProspectusBL
    {
        public DataSet GetProspectusNameWithCombination(int prospectusId, string taskType)
        {
            return new ProspectusDL().GetProspectusNameWithCombination(prospectusId, taskType);
        }

        public DataTable GetprospectusAutoFillerWithParameters(string autoFillName, string prefix, string value)
        {
            return new ProspectusDL().GetprospectusAutoFillerWithParameters(autoFillName, prefix, value);
        }

        public DataSet GetProspectusNameWithCombinationGeneral(int prospectusId, string taskType)
        {
            return new ProspectusDL().GetProspectusNameWithCombinationGeneral(prospectusId, taskType);
        }

        public DataTable GetDropdownBind(string dropdownname)
        {
            return new ProspectusDL().GetDropdownBind(dropdownname);
        }

        public DataTable GetDropdownBindMultipleValues(string dropdownname, string racedate, string centerid)
        {
            return new ProspectusDL().GetDropdownBindMultipleValues(dropdownname, racedate, centerid);
        }

        public DataTable GetProspectusGeneralId(string MasterRaceName, string centername, string sessionname, string yearname)
        {
            return new ProspectusDL().GetProspectusGeneralId(MasterRaceName, centername, sessionname, yearname);
        }

        public DataTable MasterRaceName(
            int prospectusId,
            string prospectusname,
            string prospectusalias,
            int centerid,
            int seasonid,
            string dateofnamechange,
            string myComments,
            int userId,
            string taskType)
        {

            return new ProspectusDL().MasterRaceName(prospectusId,prospectusname,prospectusalias,centerid,seasonid,dateofnamechange,myComments,userId,taskType);
        }

        public DataTable GeneralRaceName(
            int prospectusId,
            int prospectusmasterid,
            string prospectusname,
            string prospectusalias,
            int centerid,
            int seasonid,
            int yearid,
            string dateofnamechange,
            string myComments,
            int userId,
            string taskType,
            string RaceNameStatus)
        {

            return new ProspectusDL().GeneralRaceName(prospectusId, prospectusmasterid, prospectusname, prospectusalias, centerid, seasonid, yearid, dateofnamechange, myComments, userId, taskType, RaceNameStatus);
        }

        public DataTable ImportExcel(DataTable dt, string pagename, int globalid)
        {
            return new ProspectusDL().ImportExcel(dt, pagename, globalid);
        }

        public DataTable GetprospectusAutoFiller(string autoFillName, string prefix)
        {
            return new ProspectusDL().GetprospectusAutoFiller(autoFillName, prefix);
        }


		public DataTable GetprospectusAutoFiller(string autoFillName, string prefix, string multiplevalue)
		{
			return new ProspectusDL().GetprospectusAutoFiller(autoFillName, prefix, multiplevalue);
		}

		public DataTable GetProspectusGeneralAutoFill(string autoFillName, string prefix, int masterid, string multiplevalue)
        {
            return new ProspectusDL().GetProspectusGeneralAutoFill(autoFillName, prefix, masterid, multiplevalue);
        }

        public DataTable MasterRaceNameNew(
            int prospectusId,
            string prospectusname,
            string prospectusalias,
            string dateofnamechange,
            string myComments,
            int userId,
            string taskType)
        {
            return new ProspectusDL().MasterRaceNameNew(
                prospectusId,
                prospectusname,
                prospectusalias,
                dateofnamechange,
                myComments,
                userId,
                taskType);
        }

        public DataSet GetExport(int prospectusId, string taskType)
        {
            return new ProspectusDL().GetExport(prospectusId, taskType);
        }

        public DataTable GeneralRaceNameNew(
           int prospectusmasterId,
             int prospectgeneralracenameid,
           string prospectusname,
           string prospectusalias,
           string dateofnamechange,
           string myComments,
           int userId,
           string taskType)
        {
            return new ProspectusDL().GeneralRaceNameNew(
                prospectusmasterId,
                prospectgeneralracenameid,
                prospectusname,
                prospectusalias,
                dateofnamechange,
                myComments,
                userId,
                taskType);
        }

        public DataSet GetProspectusCompleteInformation(int prospectusid, string TaskType)
        {
            return new ProspectusDL().GetProspectusCompleteInformation(prospectusid, TaskType);
        }

        public DataSet sp_GetProspectusMasterInGeneral(int prospectusid, string TaskType)
        {
            return new ProspectusDL().sp_GetProspectusMasterInGeneral(prospectusid, TaskType);
        }

        public DataSet GetProspectusCompleteInformationGeneral(int prospectusid, string TaskType)
        {
            return new ProspectusDL().GetProspectusCompleteInformationGeneral(prospectusid, TaskType);
        }

        public DataTable GetProspectusId(string MasterRaceName, string centername, string seassionname)
        {
            return new ProspectusDL().GetProspectusId(MasterRaceName, centername, seassionname);
        }

        public DataSet GetProspectusTillDateValidation(int racemasterid, string taskType, string fromyear, string actiontype)
        {
            return new ProspectusDL().GetProspectusTillDateValidation(racemasterid, taskType, fromyear, actiontype);
        }

        public DataSet GetGeneralProspectusTillDateValidation(int racemasterid, string taskType, string btntext)
        {
            return new ProspectusDL().GetGeneralProspectusTillDateValidation(racemasterid, taskType, btntext);
        }

        public int InsertRaceMemoryOf(
            int masterraceid,
            int memoirtypeID,
            int memoirnameID,
            int fromyearid,
            int tillyearid,
            string otherdetails,
            string MyComments,
            int UserID,
            string TaskType)
        {
            return new ProspectusDL().InsertRaceMemoryOf(
                masterraceid,
                memoirtypeID,
                memoirnameID,
                fromyearid,
                tillyearid,
                otherdetails,
                MyComments,
                UserID,
                TaskType);
        }


        public int InsertGeneralRaceMemoryOf(
            int masterraceid,
            string memoirtype,
            string memoirname,
            string FromDate,
            string TillDate,
            string otherdetails,
            string MyComments,
            int UserID,
            string TaskType)
        {
            return new ProspectusDL().InsertGeneralRaceMemoryOf(
                masterraceid,
                memoirtype,
                memoirname,
                FromDate,
                TillDate,
                otherdetails,
                MyComments,
                UserID,
                TaskType);
        }

        public int Sponcer(
            int masterraceid,
            int professionalid,
            int fromyearid,
            int tillyearid,
            string otherdetails,
            string mycomments,
            int userid,
            string tasktype)
        {

            return new ProspectusDL().Sponcer(
                masterraceid,
                professionalid,
                fromyearid,
                tillyearid,
                otherdetails,
                mycomments,
                userid,
                tasktype);
        }

        public int GeneralSponcer(
            int masterraceid,
            int professionalid,
            string fromdate,
            string tilldate,
            string otherdetails,
            string mycomments,
            int userid,
            string tasktype)
        {

            return new ProspectusDL().GeneralSponcer(
                masterraceid,
                professionalid,
                fromdate,
                tilldate,
                otherdetails,
                mycomments,
                userid,
                tasktype);
        }

        public int Presenter(
            int masterraceid,
            int professionalid,
             int fromyearid,
            int tillyearid,
            string otherdetails,
            string mycomments,
            int userid,
            string tasktype)
        {

            return new ProspectusDL().Presenter(
                masterraceid,
                professionalid,
                fromyearid,
                tillyearid,
                otherdetails,
                mycomments,
                userid,
                tasktype);
        }



        public int GeneralPresenter(
            int masterraceid,
            int professionalid,
            string fromdate,
            string tilldate,
            string otherdetails,
            string mycomments,
            int userid,
            string tasktype)
        {

            return new ProspectusDL().GeneralPresenter(
                masterraceid,
                professionalid,
                fromdate,
                tilldate,
                otherdetails,
                mycomments,
                userid,
                tasktype);
        }

        public int MasterDistance(
           int masterraceid,
           int distanceid,
           int fromyearid,
           int tillyearid,
           string mycomments,
           int userid,
           string tasktype,
            string tilldate)
        {

            return new ProspectusDL().MasterDistance(
                masterraceid,
                distanceid,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype,
                tilldate);
        }

        public int GeneralMasterDistance(
           int masterraceid,
           int distanceid,
           string fromdate,
           string tilldate,
           string mycomments,
           int userid,
           string tasktype)
        {

            return new ProspectusDL().GeneralMasterDistance(
                masterraceid,
                distanceid,
                fromdate,
                tilldate,
                mycomments,
                userid,
                tasktype);
        }

        public int MasterRaceType(
          int masterraceid,
          string racetype,
          int fromyearid,
           int tillyearid,
          string mycomments,
          int userid,
          string tasktype,
            string category,
            string TillDate)
        {

            return new ProspectusDL().MasterRaceType(
                masterraceid,
                racetype,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype,
                category, TillDate);
        }


        public int GeneralRaceType(
          int masterraceid,
          string racetype,
          string fromdate,
          string tilldate,
          string mycomments,
          int userid,
          string tasktype)
        {

            return new ProspectusDL().GeneralRaceType(
                masterraceid,
                racetype,
                fromdate,
                tilldate,
                mycomments,
                userid,
                tasktype);
        }

        public int MasterRatingRange(
            int masterraceid,
            int handicapratingrangeid,
            int categoryid,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype,
            string TillDate)
        {
            return new ProspectusDL().MasterRatingRange(
                masterraceid,
                handicapratingrangeid,
                categoryid,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype, TillDate);
        }


        public int MasterEligbleRatingRange(
           int masterraceid,
           int handicapratingrangeid,
           int fromyearid,
           int tillyearid,
           string mycomments,
           int userid,
           string tasktype,
            string TillDate)
        {
            return new ProspectusDL().MasterEligbleRatingRange(
                masterraceid,
                handicapratingrangeid,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype,
                TillDate);
        }


        public int MasterAgeCondition(
            int masterraceid,
            int ageconditionid,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProspectusDL().MasterAgeCondition(
                masterraceid,
                ageconditionid,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }


        public int MasterMillion(
           int masterraceid,
           string million,
           int fromyearid,
            int tillyearid,
           string mycomments,
           int userid,
           string tasktype)
        {
            return new ProspectusDL().MasterMillion(
                masterraceid,
                million,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }

        public int MasterBunch(
           int masterraceid,
           string bunch,
           int fromyearid,
            int tillyearid,
           string mycomments,
           int userid,
           string tasktype)
        {
            return new ProspectusDL().MasterBunch(
                masterraceid,
                bunch,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }

        public int MasterRaceStatus(
           int masterraceid,
           string racestatus,
           int fromyearid,
           int tillyearid,
           string mycomments,
           int userid,
           string tasktype)
        {
            return new ProspectusDL().MasterRaceStatus(
                masterraceid,
                racestatus,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }


        public int MasterSweepStake(
          int masterraceid,
          string sweepstake,
          int fromyearid,
          int tillyearid,
          string mycomments,
          int userid,
          string tasktype)
        {
            return new ProspectusDL().MasterSweepStake(
                masterraceid,
                sweepstake,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }


        public int MasterClassic(
          int masterraceid,
          string classic,
          int fromyearid,
          int tillyearid,
          string mycomments,
          int userid,
          string tasktype)
        {
            return new ProspectusDL().MasterClassic(
                masterraceid,
                classic,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }


        public int MasterGraded(
          int masterraceid,
          string graded,
          int fromyearid,
          int tillyearid,
          string mycomments,
          int userid,
          string tasktype)
        {
            return new ProspectusDL().MasterGraded(
                masterraceid,
                graded,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }


        public int MasterGradeNo(
          int masterraceid,
          string graded,
          int fromyearid,
          int tillyearid,
          string mycomments,
          int userid,
          string tasktype)
        {
            return new ProspectusDL().MasterGradeNo(
                masterraceid,
                graded,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }

        public int AddProspectus(
            int prospectusid,
            string abbreviation,
            string foreignhorseallowed,
            string maidenhorseallowed,
            int userid,
            string tasktype)
        {
            return new ProspectusDL().AddProspectus(
                prospectusid,
                abbreviation,
                foreignhorseallowed,
                maidenhorseallowed,
                userid,
                tasktype);
        }

        public int MomenttoType(
            int masterraceid,
            int momenttotypeid,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProspectusDL().MomenttoType(
                masterraceid,
                momenttotypeid,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }


        public int MomenttoCost(
            int masterraceid,
            string momenttocost,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProspectusDL().MomenttoCost(
                masterraceid,
                momenttocost,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }

        public DataTable GetMasterProspectusData(string PageName)
        {
            return new ProspectusBL().GetMasterProspectusData(PageName);
        }

        public int AddGeneralProspectus(
            int prospectusid,
            string raceday,
            string nameofraceday,
            string timeslotofraceday,
            int mainraceofday,
            string serialnumber,
            string yearofbirth,
            int userid,
            string tasktype)
        {
            return new ProspectusDL().AddGeneralProspectus(
                prospectusid,
                raceday,
                nameofraceday,
                timeslotofraceday,
                mainraceofday,
                serialnumber,
                yearofbirth,
                userid,
                tasktype);
        }

        public DataTable Import30(DataTable dt, string PageName, int globalid)
        {
            return new ProspectusDL().Import30(dt, PageName, globalid);
        }


        public int ProspectusMasterRemove(
           int prospectusid,
           int userid)
        {
            return new ProspectusDL().ProspectusMasterRemove(
                prospectusid,
                userid);
        }

        public int Observation(
           int generalracenameid,
           string observation,
           string aimedduration,
           string FromDate,
           string TillDate,
           string reason,
           string MyComments,
           int UserID,
           string TaskType,
		   string typeid,
			string relatednameid)
        {
            return new ProspectusDL().Observation(
                generalracenameid,
               observation,
               aimedduration,
               FromDate,
               TillDate,
               reason,
               MyComments,
               UserID,
               TaskType,
			   typeid,relatednameid);
        }

        public int GeneralDates(
           int generalracenameid,
           int datetypeid,
           string allowed,
           string dateterm,
           string date,
           string time,
           int UserID,
           string TaskType,
             int? fees,
			 string amountpercentage,
			string amountinwords,
			string reasonofchange,
            string mycomments)
        {
            return new ProspectusDL().GeneralDates(
                generalracenameid,
               datetypeid,
               allowed,
               dateterm,
               date,
               time,
               UserID,
               TaskType,fees,amountpercentage,
			    amountinwords, reasonofchange,mycomments);
        }

        public DataSet GetTillDateGridview(int masterracenameid)
        {
            return new ProspectusDL().GetTillDateGridview(
                masterracenameid);
        }

		public int GetProspectusGeneralLastId(string pagename)
		{
			return new ProspectusDL().GetProspectusGeneralLastId(pagename);
		}

			public DataSet GetProfessionalProfileDetail(int professionalnameid)
        {
            return new ProspectusDL().GetProfessionalProfileDetail(
                professionalnameid);
        }

        public int PermanentCondition(
           int masterraceid,
           int permanentconditionid,
           int fromyearid,
           int tillyearid,
           string mycomments,
           int userid,
           string tasktype)
        {
            return new ProspectusDL().PermanentCondition(
                masterraceid, permanentconditionid, fromyearid, tillyearid, mycomments, userid,tasktype);
        }

        public int OtherCondition(
          int masterraceid,
          int permanentconditionid,
          int fromyearid,
          int tillyearid,
          string mycomments,
          int userid,
          string tasktype)
        {
            return new ProspectusDL().OtherCondition(
                masterraceid, permanentconditionid, fromyearid, tillyearid, mycomments, userid, tasktype);
        }

        public int MasterProfessionalBackground(
            int masterraceid,
            int professionalbackgroundid,
            int fromyearid,
            int tillyearid,
            string othercomment,
            string mycomments,
            int userid,
            string tasktype)
        {
        return new ProspectusDL().MasterProfessionalBackground(
                masterraceid, professionalbackgroundid, fromyearid, tillyearid, othercomment, mycomments, userid,tasktype);
        }

        public int MasterHWPCondition(
            int masterraceid,
            string srno,
            string partno,
            string secno,
            int hwpconditionid,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProspectusDL().MasterHWPCondition(
                masterraceid, srno,partno,secno,hwpconditionid, fromyearid, tillyearid, mycomments, userid, tasktype);

        }

        public int MasterBunchCondition(
          int masterraceid,
          int bunchconditionid,
          int fromyearid,
          int tillyearid,
          string mycomments,
          int userid,
          string tasktype)
        {
            return new ProspectusDL().MasterBunchCondition(
                masterraceid, bunchconditionid, fromyearid, tillyearid, mycomments, userid, tasktype);
        }

        public int GeneralSeasonalCondition(
          int generalraceid,
          int seasonaconditionalid,
          string FromDate,
          string TillDate,
          string MyComments,
          int UserID,
          string TaskType)
        {
            return new ProspectusDL().GeneralSeasonalCondition(
                generalraceid, seasonaconditionalid, FromDate, TillDate, MyComments, UserID, TaskType);
        }

        public int RaceCardCondition(
         int generalraceid,
         int racecardconditionid,
         string FromDate,
         string TillDate,
         string MyComments,
         int UserID,
         string TaskType)
        {
            return new ProspectusDL().RaceCardCondition(
                            generalraceid, racecardconditionid, FromDate, TillDate, MyComments, UserID, TaskType);
        }

        public int MasterHandicapWeight(
           int masterraceid,
           int genderid,
           decimal handicapweight,
           int fromyearid,
           int tillyearid,
           string mycomments,
           int userid,
           string tasktype)
        {
            return new ProspectusDL().MasterHandicapWeight(
                            masterraceid, genderid, handicapweight, fromyearid, tillyearid, mycomments, userid, tasktype);
        }

        public int HandicapWeightAsPerAge(
            int masterraceid,
            int age,
            string weight,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype,
            string horsesexid)
        {
            return new ProspectusDL().HandicapWeightAsPerAge(
                 masterraceid,
                 age,
                 weight,
                 fromyearid,
                 tillyearid,
                 mycomments,
                 userid,
                 tasktype,
                 horsesexid
                );
        }


        public int HandicapRaceHistory(
            int masterraceid,
            string srnumber,
            string history,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProspectusDL().HandicapRaceHistory(
                 masterraceid,
                 srnumber,
                 history,
                 mycomments,
                 userid,
                 tasktype
                );

        }

        public DataTable GetMasterRaceDetail(string masterraceid)
        {
            return new ProspectusDL().GetMasterRaceDetail(
                 masterraceid
                );
        }

        public int StakeMoneyAddition(
            int masterraceid,
            int additiontypeid,
            string amount,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProspectusDL().StakeMoneyAddition(
                masterraceid,
                additiontypeid,
                amount,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }


        public int RaceAbbriviation(
            int masterraceid,
            int additiontypeid,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProspectusDL().RaceAbbriviation(
                masterraceid,
                additiontypeid,
                fromyearid,
                tillyearid,
                mycomments,
                userid,
                tasktype);
        }
    }
}
