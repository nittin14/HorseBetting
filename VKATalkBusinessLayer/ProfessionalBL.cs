using System;
using System.Data;
using VKADB;

namespace VKATalkBusinessLayer
{
    public class ProfessionalBL
    {
        public DataTable GetProfessionalNameAutoFiller(string autoFillName, string prefix)
        {
            return new ProfessionalDL().GetProfessionalNameAutoFiller(autoFillName, prefix);
        }

        public DataSet GetProfessionalNameWithCombination(int professionalId, string taskType)
        {
            return new ProfessionalDL().GetProfessionalNameWithCombination(professionalId, taskType);
        }

       public DataTable ProfessionalName(
            int professionalId,
            string professionalName,
            string professionalWs,
            string professionalshortname,
            string professionalNameAlias,
              string mycomments,
           int userId,
           string taskType,
            int profileid,
            int basecenterid,
            int professionalprofileid,
            int professionalbasecenterid,
            string dob, string professionaltypeid)
        {
            return new ProfessionalDL().ProfessionalName(professionalId, professionalName, professionalWs, professionalshortname, professionalNameAlias, 
                            mycomments, userId, taskType, profileid, basecenterid, professionalprofileid, professionalbasecenterid, dob, professionaltypeid);
        }

        public DataTable ProfessionalNewName(
            int professionalId,
            string professionalName,
            string professionalWs,
            string professionalshortname,
            string professionalNameAlias,
            string dateofnamechange,
            string myComments,
            int userId,
            string taskType)
        {
            return new ProfessionalDL().ProfessionalNewName(professionalId, professionalName, professionalWs, professionalshortname, professionalNameAlias, dateofnamechange, myComments, userId, taskType);
        }

        public int ProfessionalProfileP(
            int professionalId,
            int profileid,
            string fromDate,
            string tillDate,
            string mycomments,
            int userId,
            string taskType,
            string profileindetails)
        {
            return new ProfessionalDL().ProfessionalProfileP(professionalId, profileid, fromDate, tillDate, mycomments, userId, taskType, profileindetails);
        }

        /// <summary> bind dropdown value
        /// </summary>
        /// <param name="dropdownname"> dropdown name
        /// </param>
        /// <param name="value"> integer value
        /// </param>
        /// <returns> professional name
        /// </returns>
        public DataTable GetProfessionalName(string dropdownname, int value)
        {
            return new ProfessionalDL().GetProfessionalName(dropdownname, value);
        }

        /// <summary>
        /// professional name
        /// </summary>
        /// <param name="dropdownname">dropdown name</param>
        /// <returns>professional name</returns>
        public DataTable GetProfessionalName(string dropdownname)
        {
            return new ProfessionalDL().GetProfessionalName(dropdownname);
        }

        public int ProfessionalBaseCenter(
           int professionalId,
           int basecenterId,
           string fromDate,
           string tillDate,
           string reason,
           string mycomments,
           int userId,
           string taskType)
        {
            return new ProfessionalDL().ProfessionalBaseCenter(professionalId, basecenterId, fromDate, tillDate, reason, mycomments, userId, taskType);
        }

        public DataSet GetProfessionalTillDateValidation(int professionalid, string tasktype, string fromDate, string action)
        {
            return new ProfessionalDL().GetProfessionalTillDateValidation(professionalid, tasktype, fromDate, action);
        }

        public DataSet GetProfessionalTillDateValidationProfile(int professionalid, string tasktype, string fromDate, string action, int profileid)
        {
            return new ProfessionalDL().GetProfessionalTillDateValidationProfile(professionalid, tasktype, fromDate, action, profileid);
        }

        public int ProfessionalProfile(int ProfessionalId, int isCheckBox, string profile, string fromDate, string tillDate,
           string myComments, int userId, string taskType)
        {
            return new ProfessionalDL().ProfessionalProfile(ProfessionalId, isCheckBox, profile, fromDate, tillDate, myComments, userId, taskType);
        }

        public int ProfessionalOwnerColor(int ProfessionalId, int ownercolorid, int centerid, string fromDate, string tillDate,
           string myComments, int userId, string taskType)
        {
            return new ProfessionalDL().ProfessionalOwnerColor(ProfessionalId, ownercolorid, centerid, fromDate, tillDate, myComments, userId, taskType);
        }

        public int InsertProfessionalStatus(
            int professionalId,
            string status,
            string fromdate,
            string tilldate,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().InsertProfessionalStatus(professionalId, status, fromdate, tilldate, mycomments, userid, tasktype);
        }

        public int InsertProfessionalCurrentStatus(
            int professionalId,
            int status,
            string fromdate,
            string tilldate,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().InsertProfessionalCurrentStatus(professionalId, status, fromdate, tilldate, mycomments, userid, tasktype);
        }

        public int InsertProfessionalJockeyWeight(
            int professionalId,
            string bodyweight,
            string minridingweight,
            string maxridingweight,
            string fromdate,
            string tilldate,
            string mycomments,
            int userid,
            string tasktype,
            int jockeyweighttypeid,
			string overweight)
        {
            return new ProfessionalDL().InsertProfessionalJockeyWeight(professionalId, bodyweight, minridingweight, maxridingweight, fromdate, tilldate, mycomments, userid, tasktype, jockeyweighttypeid,overweight);
        }

        public int ProfessionalOtherName(
            int professionalId,
            string jockeyothername,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().ProfessionalOtherName(professionalId, jockeyothername, userid, tasktype);
        }

        public int ProfessionalAprenticeOf(
            int professionalId,
            int trainerid,
            string fromdate,
            string tilldate,
            string reasonofchange,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().ProfessionalAprenticeOf(professionalId, trainerid,fromdate,tilldate,reasonofchange,mycomments, userid, tasktype);
        }

        public int ProfessionalTrainingAssistant(
            int professionalId,
            int trainerid,
            string fromdate,
            string tilldate,
            string reasonofchange,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().ProfessionalTrainingAssistant(professionalId, trainerid, fromdate, tilldate, reasonofchange, mycomments, userid, tasktype);
        }

        public int ProfessionalPartners(
            int professionalId,
            int trainerid,
            string fromdate,
            string tilldate,
            string percentage,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().ProfessionalPartners(professionalId, trainerid, fromdate, tilldate, percentage, mycomments, userid, tasktype);
        }

        public int ProfessionalHomeDistance(
            int professionalId,
            int distanceId,
            string fromdate,
            string tilldate,
            int ratingmarkformatstyleId,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().ProfessionalHomeDistance(professionalId, distanceId, fromdate, tilldate, ratingmarkformatstyleId, mycomments, userid, tasktype);
        }

        public int ProfessionalHomeFavDistanceGroup(
            int professionalId,
            int distanceId,
            string fromdate,
            string tilldate,
            int ratingmarkformatstyleId,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().ProfessionalHomeFavDistanceGroup(professionalId, distanceId, fromdate, tilldate, ratingmarkformatstyleId, mycomments, userid, tasktype);
        }

        public int HomeClass(
            int professionalId,
            int distanceId,
            string fromdate,
            string tilldate,
            int ratingmarkformatstyleId,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().HomeClass(professionalId, distanceId, fromdate, tilldate, ratingmarkformatstyleId, mycomments, userid, tasktype);
        }

        public int HomeFavClassGroup(
            int professionalId,
            int distanceId,
            string fromdate,
            string tilldate,
            int ratingmarkformatstyleId,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().HomeFavClassGroup(professionalId, distanceId, fromdate, tilldate, ratingmarkformatstyleId, mycomments, userid, tasktype);
        }

        public int ProfessionalHabit(
            int professionalId,
            string habit,
            string fromdate,
            string tilldate,
            string habitdetails,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().ProfessionalHabit(professionalId, habit, fromdate, tilldate, habitdetails, mycomments, userid, tasktype);
        }

        public int ProfessionalImportantDates(
            int professionalId,
            string dates,
            string relatedto,
            string relatedtoname,
            string occassion,
            string mycomments,
            int userid,
            string tasktype)
        {
            return new ProfessionalDL().ProfessionalImportantDates(professionalId, dates,relatedto, relatedtoname, occassion, mycomments, userid, tasktype);
        }

        public int ProfessionalMyObservations(
            int professionalId,
            string myobservation,
            string myobservationindetail,
            int userid,
            string tasktype,
			string fromdate,
		  string tilldate,
		  string mycomments)
        {
            return new ProfessionalDL().ProfessionalMyObservations(professionalId, myobservation, myobservationindetail, userid, tasktype,fromdate,tilldate,mycomments);
        }

        public int ProfessionalRelation(
            int professionalId,
            int professionalnameid,
            int relationtypeid,
            int relationchildid,
            string fromdate,
            string tilldate,
            string relationbreak,
            string mycomments,
            int userid,
            string tasktype,
			string groupid)
        {
            return new ProfessionalDL().ProfessionalRelation(professionalId, professionalnameid,relationtypeid,relationchildid,fromdate,tilldate,relationbreak,mycomments,userid, tasktype, groupid);
        }

        public DataTable GetProfessionalId(string professionalname, string profilename, string center)
        {
            return new ProfessionalDL().GetProfessionalId(professionalname,profilename,center);
        }

        public DataSet GetProfessionalCompleteInformation(int professionalid, string TaskType)
        {
            return new ProfessionalDL().GetProfessionalCompleteInformation(professionalid, TaskType);
        }

        public int ProfessionalCompleteInformation(
            int professionalid,
            string dobdoi,
            string jklicencedate,
            string trlicencedate,
            string bodyweight,
            int userid,
            string tasktype,
            string professionaltype)
        {
            return new ProfessionalDL().ProfessionalCompleteInformation(professionalid, dobdoi, jklicencedate, trlicencedate, bodyweight, userid, tasktype, professionaltype);
        }

        public DataTable Import30(DataTable dt, string PageName)
        {
            return new ProfessionalDL().Import30(dt, PageName);
        }

        public DataSet GetExport(int prospectusId, string taskType)
        {
            return new ProfessionalDL().GetExport(prospectusId, taskType);
        }

        public int ProfessionalReligion(
           int professionalId,
           int religionid,
           string fromDate,
           string tillDate,
           string mycomments,
           int userId,
           string taskType)
        {
            return new ProfessionalDL().ProfessionalReligion(professionalId, religionid, fromDate, tillDate, mycomments, userId, taskType);
        }

        public int ProfessionalAntiPerson(
           int professionalId,
           int antipersonid,
           string fromDate,
           string tillDate,
           string reason,
           string mycomments,
           int userId,
           string taskType)
        {
            return new ProfessionalDL().ProfessionalAntiPerson(professionalId, antipersonid, fromDate, tillDate, reason, mycomments, userId, taskType);
        }


        public int ProfessionalBackground(
            int professionalId,
            int backgroundid,
             string fromDate,
           string tilldate,
            string mycomments,
            int userId,
            string taskType)
        {
            return new ProfessionalDL().ProfessionalBackground(professionalId, backgroundid, fromDate, tilldate, mycomments, userId, taskType);
        }

		public int ProfessionalCount()
		{
			return new ProfessionalDL().ProfessionalCount();
		}

		public DataSet GetProfessionalMultipleTillDateString(int professionalid, string tasktype, string fromdate, string actiontype, string value)
        {
            return new ProfessionalDL().GetProfessionalMultipleTillDateString(professionalid, tasktype, fromdate, actiontype, value);
        }


        public int JockeyAllowanceStage(
              int professionalnameid,
              int jockeyallowancestageid,
              string stagestartdate,
              string startdayraceno,
              string enddayraceno,
              string stageenddate,
              int userid,
              string mycomments,
              string tasktype)
        {
            return new ProfessionalDL().JockeyAllowanceStage(
                  professionalnameid,
                  jockeyallowancestageid,
                  stagestartdate,
                  startdayraceno,
                  enddayraceno,
                  stageenddate,
                  userid,
                  mycomments,
                  tasktype
                );
        }


        public int ProfessionalPenalty(
              int professionalnameid,
              int penaltyid,
              int penaltyreasonid,
              string penaltydetail,
              string fromdate,
              string tilldate,
              int workonappeal,
              string mycomments,
              int userid,
              string tasktype)
        {
            return new ProfessionalDL().ProfessionalPenalty(
                      professionalnameid,
                      penaltyid,
                      penaltyreasonid,
                      penaltydetail,
                      fromdate,
                      tilldate,
                      workonappeal,
                      mycomments,
                      userid,
                      tasktype
                );
        }
    }
}
