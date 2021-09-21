using System;
using System.Text;
using System.Data;
using VKADB;
using VKATalkClassLayer;

namespace VKATalkBusinessLayer
{
    public class MasterBL
    {
        MasterDB DB = new MasterDB();
        public int InsertMasterPagesData(string PageName, string PageFieldName, string PageFieldAlias, int UserID_FK)
        {

            return (DB.InsertMasterPagesData(PageName, PageFieldName, PageFieldAlias, UserID_FK));
        }
        public int InsertMasterPagesDataSelectorTipster(string PageName, string SerialNumber, string PageFieldName, string PageFieldAlias, int UserID_FK)
        {

            return (DB.InsertMasterPagesDataSelectorTipster(PageName, SerialNumber, PageFieldName, PageFieldAlias, UserID_FK));
        }

        public int InsertMasterPagesDataIncident(string PageName, string IncidentName, string IncidentInShort, string IncidentShortAlias, string Impact, int UserID_FK, int recordid)
        {
            return (DB.InsertMasterPagesDataIncident(PageName, IncidentName, IncidentInShort, IncidentShortAlias, Impact, UserID_FK, recordid));
        }

        //public bool UpdateMasterPagesDataIncident(string PageName, int RecordID, string Incident, string IncidentInShort, string IncidentShortAlias, string Impact, int UserID_FK, string RecordStatus)
        //{

        //    return (DB.UpdateMasterPagesDataIncident(PageName, RecordID, Incident, IncidentInShort, IncidentShortAlias, Impact, UserID_FK, RecordStatus));
        //}

        public int UpdateMasterPagesData(string PageName, int RecordID, string PageFieldName, string PageFieldAlias, int UserID_FK, string RecordStatus)
        {

            return (DB.UpdateMasterPagesData(PageName, RecordID, PageFieldName, PageFieldAlias, UserID_FK, RecordStatus));
        }
        public int UpdateMasterPagesDataSelectorTipster(string PageName, string SerialNumber, int RecordID, string PageFieldName, string PageFieldAlias, int UserID_FK, string RecordStatus)
        {
            return (DB.UpdateMasterPagesDataSelectorTipster(PageName, SerialNumber, RecordID, PageFieldName, PageFieldAlias, UserID_FK, RecordStatus));
        }

        public DataTable GetMasterData(string PageName)
        {
            return (DB.GetMasterData(PageName));
        }

        public DataTable GetDropdownBind(string DropDownName)
        {
            return (DB.GetDropdownBind(DropDownName));
        }


        public int InsertMasterAllowance(string actionname, int centerid, int fromyearid, int tillyearid, int fromseasonid, int tillseasonid, string jockeyage, string totalwinfrom, string totalwintill, string allowance, int UserID_FK, int allowanceid)
        {

            return DB.InsertMasterAllowance(actionname, centerid, fromyearid, tillyearid, fromseasonid, tillseasonid, jockeyage, totalwinfrom, totalwintill, allowance, UserID_FK, allowanceid);
        }


        public int InsertMasterPagesDataAllowance(string PageName, string FieldName, string FieldName2, string FieldAlias, string @FieldName3, string @FieldName4, int UserID_FK)
        {

            return (DB.InsertMasterPagesDataAllowance(PageName, FieldName, FieldName2, FieldAlias, FieldName3, FieldName4, UserID_FK));
        }
        public int InsertUpdateMasterDataShoe(int recordID, string action, string shoe, string shoedetail,
            int shoemetalid, int leftforelegid, int rightforelegid, int lefthindlegid, int righthindlegid, int UserID_FK)
        {

            return (DB.InsertUpdateMasterDataShoe(recordID, action, shoe, shoedetail, shoemetalid, leftforelegid, rightforelegid, lefthindlegid, righthindlegid, UserID_FK));
        }
        public int InsertUpdateMasterDataDiscardRules(int recordID, string action, int Centerid, int FromYearid, int TillYearid, int FromSeasonid, int TillSeasonid, string RuleApplyHorseAge, string MaxHandicapRating, string MinHandicapRating, string RuleApplyDate, int UserID_FK)
        {
            return (DB.InsertUpdateMasterDataDiscardRules(recordID, action, Centerid, FromYearid, TillYearid, FromSeasonid, TillSeasonid, RuleApplyHorseAge, MaxHandicapRating, MinHandicapRating, RuleApplyDate, UserID_FK));
        }
        public int UpdateAdminPagesData(string PageName, string ActionName, int RecordID, string FieldName1, string FieldName2, string FieldName3, string FieldName4, string FieldName5, int UserID_FK)
        {

            return DB.UpdateAdminPagesData(PageName, ActionName, RecordID, FieldName1, FieldName2, FieldName3, FieldName4, FieldName5, UserID_FK);
        }
       
        public int InsertUpdateMasterDataHorseRetirementAge(int recordID, string action, int Centerid, int FromYearid, int TillYearid, int FromSeasonid, int TillSeasonid, string HorseRetirementAge, int UserID_FK)
        {
            return (DB.InsertUpdateMasterDataHorseRetirementAge(recordID, action, Centerid, FromYearid, TillYearid, FromSeasonid, TillSeasonid, HorseRetirementAge, UserID_FK));
        }
        public int InsertUpdateMasterDataSeason(int recordID, string action, string SeasonName, string SeasonAlias, string SubSeason, string SubSeasonAlias, int UserID_FK)
        {
            return (DB.InsertUpdateMasterDataSeason(recordID, action, SeasonName, SeasonAlias, SubSeason, SubSeasonAlias, UserID_FK));
        }
        //public int InsertUpdateMasterDataSeasonDescription(int recordID, string action, string Center, string Year, string Season, DateTime SeasonStartDate, DateTime SeasonEndDate, string SubSeason, DateTime SubSeasonStartDate, DateTime SubSeasonEndDate, string SeasonStartingNumber, int UserID_FK)
        //{
        //    return (DB.InsertUpdateMasterDataSeasonDescription(recordID, action, Center, Year, Season, SeasonStartDate, SeasonEndDate, SubSeason, SubSeasonStartDate, SubSeasonEndDate, SeasonStartingNumber, UserID_FK));
        //}
        public int InsertUpdateMasterDataSeasonDescription(int recordID, string action, int CenterID, int YearID, int SeasonID, string SeasonStartDate, string SeasonEndDate,
                  int SubSeasonID, string SubSeasonStartDate, string SubSeasonEndDate, string SeasonStartingNumber, int UserID_FK)
        {
            return (DB.InsertUpdateMasterDataSeasonDescription(recordID, action, CenterID, YearID, SeasonID, SeasonStartDate, SeasonEndDate, SubSeasonID, SubSeasonStartDate, SubSeasonEndDate, SeasonStartingNumber, UserID_FK));
        }
        //public int InsertMasterStudData(StudProp objProp)
        //{

        //    return (DB.InsertMasterStudData(objProp));
        //}

        public DataTable InsertStakeMoney(VKATalkClassLayer.StakeMoney clsType, int UserID_FK, string action, int stakemoneyid, int stakemoneyearnerid)
        {
            return (DB.InsertStakeMoney(clsType, UserID_FK, action, stakemoneyid, stakemoneyearnerid));
        }

        public DataTable GetStakeMoney(VKATalkClassLayer.StakeMoney clsType)
        {
            return (DB.GetStakeMoney(clsType));
        }
        //public data

        public int InsertMasterPagesDataCondition(string PageName, string PageFieldName, string PageFieldAlias, string PageFieldName1, string PageFieldName2, string PageFieldName3, string PageFieldName4, int UserID_FK)
        {

            return (DB.InsertMasterPagesDataCondition(PageName, PageFieldName, PageFieldAlias, PageFieldName1, PageFieldName2, PageFieldName3, PageFieldName4, UserID_FK));
        }
        public int UpdateMasterPagesDataCondition(string PageName, int RecordID, string PageFieldName, string PageFieldAlias, string PageFieldName1, string PageFieldName2, string PageFieldName3, string PageFieldName4, int UserID_FK, string RecordStatus)
        {

            return (DB.UpdateMasterPagesDataCondition(PageName, RecordID, PageFieldName, PageFieldAlias, PageFieldName1, PageFieldName2, PageFieldName3, PageFieldName4, UserID_FK, RecordStatus));
        }

        public int InsertUpdateMasterDataHorsePerformance(string IsDistanceOrClass, int RecordID, string action_, StringBuilder xml_, int UserID_FK)
        {
            return (DB.InsertUpdateMasterDataHorsePerformance(IsDistanceOrClass, RecordID, action_, xml_, UserID_FK));
        }
        public int InsertUpdateMasterDataHorsePerformanceOLD(string IsDistanceOrClass, int RecordID, string action_, StringBuilder xml_, int UserID_FK)
        {
            return (DB.InsertUpdateMasterDataHorsePerformanceOLD(IsDistanceOrClass, RecordID, action_, xml_, UserID_FK));
        }
       
        public int InsertMasterPagesData(string PageName, string PageFieldName, string PageFieldAlias, string strCenterOldName, string strCenterOldNameAlias, string strDate, int UserID_FK)
        {

            return (DB.InsertMasterPagesData(PageName, PageFieldName, PageFieldAlias, strCenterOldName, strCenterOldNameAlias, strDate, UserID_FK));
        }

        public int UpdateMasterPagesData(string PageName, int RecordID, string PageFieldName, string PageFieldAlias, string strCenterOldName, string strCenterOldNameAlias, int UserID_FK, string RecordStatus, string NameofDateChange)
        {

            return (DB.UpdateMasterPagesData(PageName, RecordID, PageFieldName, PageFieldAlias, strCenterOldName, strCenterOldNameAlias, UserID_FK, RecordStatus, NameofDateChange));
        }


        //public data
        //20 Jan 16

        public int InsertRaceTimings(VKATalkClassLayer.RaceTimings clsRaceTiming, int racetimingsid, int userid, string action)
        {
            return DB.InsertRaceTimings(clsRaceTiming, racetimingsid,userid,action);
        }

        public int InsertMasterPagesDataYear(string Status, string FieldName, string FieldAlias, int UserID_FK)
        {

            return DB.InsertMasterPagesDataYear(Status, FieldName, FieldAlias, UserID_FK);
        }

        public int UpdateMasterPagesDataYear(string Status, string FieldName, string FieldAlias, int RecordID, int UserID_FK)
        {
            return (DB.UpdateMasterPagesDataYear(Status, FieldName, FieldAlias, RecordID, UserID_FK));
        }

        public int InsertUpdateOwnerColor(string Status, string FieldName, string capcolor, int RecordID, int UserID_FK)
        {
            return DB.InsertUpdateOwnerColor(Status, FieldName, capcolor, RecordID, UserID_FK);
        }


        // 26 Jan 2016

        //public DataTable InsertStakeMoney(string Center, string Year, string Season, string RaceType, string Category, string Class, string HandicapRatingRange, string AgeCondition, string AbbreviationofRace, string TableNo, string Money, int UserID_FK)
        //{
        //    return (DB.InsertStakeMoney(Center, Year, Season, RaceType, Category, Class, HandicapRatingRange, AgeCondition, AbbreviationofRace, TableNo, Money, UserID_FK));
        //}

        public int InsertStakeMoneyEarner(string StakeMoneyEarner, string TableNo, int RelationID, int UserID_FK)
        {
            return (DB.InsertStakeMoneyEarner(StakeMoneyEarner, TableNo, RelationID, UserID_FK));
        }

        public int InsertDistanceAgeParameter(int ParameterID, string ParameterName, int UserID_FK)
        {
            return (DB.InsertDistanceAgeParameter(ParameterID, ParameterName, UserID_FK));
        }
        public int InsertClassGroup(int CenterID, int FromYearID, int TillYearID, int FromSeasonID, int TillSeasonID, int ClassGrouptypeid, string RaceType, string RaceStatus, string Million, string SweepStake, string Graded, string category, int classtypeid, int UserID_FK, string Classic, string tasktype, int classgroupid, int classgroupaliasid, int ageconditionid)
        {
            return DB.InsertClassGroup(CenterID, FromYearID, TillYearID, FromSeasonID, TillSeasonID, ClassGrouptypeid, RaceType, RaceStatus, Million, SweepStake, Graded, category, classtypeid, UserID_FK, Classic, tasktype, classgroupid, classgroupaliasid, ageconditionid);
        }

        public int InserUpdatetMasterPagesDataClass(
            int Center,
            int FromYear,
            int TillYear,
            int FromSeason,
            int TillSeason,
            string Category,
            int classtypeid,
            int classtypealiasid,
            int handicapRatingRangeid,
            string MaxHandicapRating,
            string MinHandicapRating,
            int UserID_FK,
            string ClassStatus,
            int ClassID)
        {
            return (DB.InserUpdatetMasterPagesDataClass(Center, FromYear, TillYear, FromSeason, TillSeason, Category, classtypeid, classtypealiasid, handicapRatingRangeid, MaxHandicapRating, MinHandicapRating, UserID_FK, ClassStatus, ClassID));
        }

        public int InsertUpdateMasterHabit(string Habit, string HabitAlias, int HabitTypeID, int UserID_FK, string Status, int HabitID)
        {
            return (DB.InsertUpdateMasterHabit(Habit, HabitAlias, HabitTypeID, UserID_FK, Status, HabitID));
        }
        public int InsertUpdateMasterScaleofWeight(VKATalkClassLayer.ScaleofWeight clsScaleofWeight, int UserID_FK, string action, int sowid)
        {
            return (DB.InsertUpdateMasterScaleofWeight(clsScaleofWeight, UserID_FK, action, sowid));
        }
       
        public int InsertPopUp(string Name, string zipcode)
        {
            return (DB.InsertPopUp(Name, zipcode));
        }
        public DataTable GetTestData()
        {
            return (DB.GetTestData());
        }

        //Nitika Work

        public DataTable GetProspectusCentre()
        { return (DB.GetProspectusCentre()); }

        public DataTable GetProspectusSeason()
        { return (DB.GetProspectusSeason()); }


        //public int InsertProspecusMasterRaceInMemory(string MName, string MType, string MProfile, string MRelatedTo, string MRelatedType, string MOther, string MComment, int Active, int UserID_FK, int show, string Unique)
        //{ return (DB.InsertProspecusMasterRaceInMemory(MName, MType, MProfile, MRelatedTo, MRelatedType, MOther, MComment, Active, UserID_FK, show, Unique)); }


        //public DataTable GetProsMasterRaceInMemoryPop1()
        //{ return (DB.GetProsMasterRaceInMemory()); }

        //public int UpdateProspecusMasterRaceInMemory(string MName, string MType, string MProfile, string MRelatedTo, string MRelatedType, string MOther, string MComment, int UserID_FK, string Pname, int MID, int show)
        //{ return (DB.UpdateProspecusMasterRaceInMemory(MName, MType, MProfile, MRelatedTo, MRelatedType, MOther, MComment, UserID_FK, Pname, MID, show)); }

        //public DataTable GetFinalRaceInMemory()
        //{ return (DB.GetFinRaceInMemory()); }

        //RaceName

        public DataTable GetPropspectusAddRaceName() { return (DB.GetPropspectusAddRaceName()); }

        //public int InsertProspecusRaceName(string UniqueId, string RName, string RDistance, string RHandicap, string RAgeCondition, string RDate, string RComments, int UserID_FK, int Active, int show)
        //{ return (DB.InsertProspecusRaceName(UniqueId, RName, RDistance, RHandicap, RAgeCondition, RDate, RComments, UserID_FK, Active, show)); }

        public DataTable GetProspectusValues(string col) { return (DB.GetProspectusValues(col)); }
       
        public string GetProsMasterID(string Unique)
        {
            return (DB.GetProsMasterID(Unique));
        }

        public DataTable SubmitMoment() { return (DB.SubmitMomentName()); }

        public int UploadExcelRecordBulk(DataTable dt, string PageName)
        {
            return (DB.UploadExcelRecordBulk(dt,PageName));
        }

        public int UploadExcelRecordBulkYear(DataTable dt)
        {
            return (DB.UploadExcelRecordBulkYear(dt));
        }

        public int UploadExcelRecordBulkSeason(DataTable dt)
        {
            return (DB.UploadExcelRecordBulkSeason(dt));
        }
        public int UploadExcelRecordBulkSeasonDescription(DataTable dt)
        {
            return (DB.UploadExcelRecordBulkSeasonDescription(dt));
        }

        public int UploadExcelRecordBulkDistance(DataTable dt,string PageName)
        {
            return (DB.UploadExcelRecordBulkDistance(dt, PageName));
        }
        public int UploadExcelRecordBulkDistanceGroup(DataTable dt,string PageName)
        {
            return (DB.UploadExcelRecordBulkDistanceGroup(dt,PageName));
        }
        public int UploadExcelRecordBulkHotliner(DataTable dt, string PageName)
        {
            return (DB.UploadExcelRecordBulkHotliner(dt,PageName));
        }
        public int UploadNBCondition(DataTable dt)
        {
            return (DB.UploadNBCondition(dt));
        }
        public int UploadShoeDescription(DataTable dt)
        {
            return DB.UploadShoeDescription(dt);
        }

        public DataTable ImportExcel(DataTable dt, string PageName)
        {
            return DB.ImportExcel(dt,PageName);
        }

        public DataTable ImportExcel17(DataTable dt, string PageName)
        {
            return DB.ImportExcel17(dt, PageName);
        }
        public DataTable ImportExcel30(DataTable dt, string PageName)
        {
            return DB.ImportExcel30(dt, PageName);
        }

        
        public int VerdictMargin(
            int verdictmarginId,
            string verdictmargin,
            string verdictmarginalias,
            string measurement,
            int userid,
            string tasktype)
        {
            return DB.VerdictMargin(verdictmarginId, verdictmargin, verdictmarginalias, measurement, userid, tasktype);
        }

        public int MasterDisease(
            int diseaseid,
            string disease,
            string alias,
            string diseasedetail,
            string medicalname,
            string performanceimpact,
            string treatment,
            string precautions,
            string comments,
            int userid,
            string tasktype)
        {
            return DB.MasterDisease(
                diseaseid,
                disease,
                alias,
                diseasedetail,
                medicalname,
                performanceimpact,
                treatment,
                precautions,
                comments,
                userid,
                tasktype);
        }

        public int PermanentCondition(int permanentconditionid, string permanentcondition, int UserID_FK, string tasktype)
        {
            return DB.PermanentCondition(permanentconditionid, permanentcondition, UserID_FK, tasktype);
        }

        public int SeasonalCondition(int seasonalconditionid, string seasonalcondition, int UserID_FK, string tasktype)
        {
            return DB.SeasonalCondition(seasonalconditionid, seasonalcondition, UserID_FK, tasktype);
        }

        public int BunchCondition(int bunchconditionid, string bunchcondition, int UserID_FK, string tasktype, string alias)
        {
            return DB.BunchCondition(bunchconditionid, bunchcondition, UserID_FK, tasktype, alias);
        }

        public int RaceCardCondition(int racecardconditionid, string racecardcondition, int UserID_FK, string tasktype)
        {
            return DB.RaceCardCondition(racecardconditionid, racecardcondition, UserID_FK, tasktype);
        }

        public int HandicapRatingRange(int handicapratingrangeid, string handicapratingrange, int min, int max, int UserID_FK, string tasktype)
        {
            return DB.HandicapRatingRange(handicapratingrangeid, handicapratingrange, min, max, UserID_FK, tasktype);
        }

        public int MasterReligion(int religionid, string religion, int UserID_FK, string tasktype)
        {
            return DB.MasterReligion(religionid, religion, UserID_FK, tasktype);
        }

        public int ProfessionalBackground(int professionalbackgroundid, string professionalbackground, int UserID_FK, string tasktype)
        {
            return DB.ProfessionalBackground(professionalbackgroundid, professionalbackground, UserID_FK, tasktype);
        }

        public int CurrentForm(int currentformid, string currentform, int UserID_FK, string tasktype)
        {
            return DB.CurrentForm(currentformid, currentform, UserID_FK, tasktype);
        }

        public int HandicapWeightCondition(int handicapwieghtconditionid, string condition, int UserID_FK, string tasktype)
        {
            return DB.HandicapWeightCondition(handicapwieghtconditionid, condition, UserID_FK, tasktype);
        }


        public int HandicapWeightCriteria(int handicapwieghtriteriaid, string criteria, int UserID_FK, string tasktype)
        {
            return DB.HandicapWeightCriteria(handicapwieghtriteriaid, criteria, UserID_FK, tasktype);
        }

        public int MasterCenterYearWise(int centerwiseid, int centerid, int yearid, string yearstartdate, string yearenddate, int UserID_FK, string tasktype)
        {
            return  new MasterDB().MasterCenterYearWise(centerwiseid, centerid, yearid, yearstartdate, yearenddate, UserID_FK, tasktype);
        }

        public DataTable GetprospectusAutoFiller(string autoFillName, string prefix)
        {
            return new MasterDB().GetprospectusAutoFiller(autoFillName, prefix);
        }

        public DataTable ImportMasterFiles(string field1, string field2, string field3, string field4, string field5, string field6, string field7, string field8, string field9,
                                           string field10, string field11, string field12, string field13, string field14, string field15, string field16, string field17,
                                           string field18, string field19, string field20, string field21, string field22, string field23, string field24, string field25,
                                           string field26, string field27, string field28, string field29, string field30, string field31, string pagename)
        {
            return new MasterDB().ImportMasterFiles(field1, field2, field3, field4, field5, field6, field7, field8, field9,
                                                field10, field11, field12, field13, field14, field15, field16, field17,
                                                field18, field19, field20, field21, field22, field23, field24, field25,
                                                field26, field27, field28, field29, field30, field31, pagename);
        }
    }
}
