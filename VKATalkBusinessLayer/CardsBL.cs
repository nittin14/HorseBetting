using System;
using System.Data;
using VKADB;

namespace VKATalkBusinessLayer
{
    public class CardsBL
    {
        public DataTable GetRaceCenterName(string racedate)
        {
           return new CardsDL().GetRaceCenterName(racedate);
        }

        public DataTable GetWorkOutHorseInformation(string racedate)
        {
            return new CardsDL().GetWorkOutHorseInformation(racedate);
        }

        public DataTable GetRaceGeneralRaceDetail(string racedate, int centerid)
        {
            return new CardsDL().GetRaceGeneralRaceDetail(racedate,centerid);
        }

		public DataTable GetRaceGeneralRaceDetailAcceptance(string racedate, int centerid)
		{
			return new CardsDL().GetRaceGeneralRaceDetailAcceptance(racedate, centerid);
		}

        public DataTable GetCardDJA(int professinalnameid, string racedate, int centerid, int divisionraceid)
        {
            return new CardsDL().GetCardDJA(professinalnameid, racedate, centerid, divisionraceid);
        }
        public DataSet GetResultselectedRowforUpdate(int GlobalID, int divisionraceid, int horsenameid)
        {
            return new CardsDL().GetResultselectedRowforUpdate(GlobalID, divisionraceid, horsenameid);
        }
        public DataTable GetCardResultInformation(int horsenameid, int divisionraceid)
        {
            return new CardsDL().GetCardResultInformation(horsenameid, divisionraceid);
        }


        public DataTable GetCardResultInformation1(int jockeynameid, int divisionraceid)
        {
            return new CardsDL().GetCardResultInformation1(jockeynameid, divisionraceid);
        }

        public DataSet GetEntryDateInformation(int generalracenameid, string cardname, string season, string year)
        {
            return new CardsDL().GetEntryDateInformation(generalracenameid, cardname, season, year);
        }

        public DataTable GetHorseName(string DropDownName, string racedate)
        {
            return (new CardsDL().GetHorseName(DropDownName, racedate));
        }

        public DataTable GetDropdownBind(string DropDownName)
        {
            return new CardsDL().GetDropdownBind(DropDownName);
        }

        public DataTable HorseInformation(
            string racedate,
            int centerid,
            int generalraceid,
            int entrytypeid,
            int horseserialnumber,
            int horseid,
            int genderid,
            int trainerid,
            int ownerid,
            int userid,
            string action,
            int entryid,
            int formid,
            string entrydate,
            bool sweepstakeentry, bool struckout, int struckoutstageid)
        {
            return new CardsDL().HorseInformation(
                racedate,
                centerid,
                generalraceid,
                entrytypeid,
                horseserialnumber,
                horseid,
                genderid,
                trainerid,
                ownerid,
                userid,
                action,
                entryid,
                formid,
                entrydate,
                sweepstakeentry, struckout, struckoutstageid);
        }

        public DataTable Handicap(
           string racedate,
           int centerid,
           int generalraceid,
           string entrytype,
           int horseserialnumber,
           int horseid,
           int genderid,
           int trainerid,
           int ownerid,
           int userid,
           string action,
           int entryid)
        {
            return new CardsDL().Handicap(
                racedate,
                centerid,
                generalraceid,
                entrytype,
                horseserialnumber,
                horseid,
                genderid,
                trainerid,
                ownerid,
                userid,
                action,
                entryid);
        }

        public DataTable GetHorseNameAutoFiller(string autoFillName, string prefix, string multipleconditions)
        {
            return new CardsDL().GetHorseNameAutoFiller(autoFillName, prefix, multipleconditions);
        }

        public DataTable GetHorseNameAutoFillerHotliner(string autoFillName, string prefix, string multipleconditions)
        {
            return new CardsDL().GetHorseNameAutoFillerHotliner(autoFillName, prefix, multipleconditions);
        }

        public DataTable GetCardAutoFiller(string autoFillName, string prefix)
        {
            return new CardsDL().GetCardAutoFiller(autoFillName, prefix);
        }


        public DataTable GetFinalHorseGenTrainOwenerInformation(int horsenameid, string racedate)
        {
            return new CardsDL().GetFinalHorseGenTrainOwenerInformation(horsenameid, racedate);
        }

        public int AddHandicap(DataTable dt)
        {
            return new CardsDL().AddHandicap(dt);
        }

		public int DivisionRaceCount(int generalracenameid, int centerid, string divisionracedate)
		{
			return new CardsDL().DivisionRaceCount(generalracenameid, centerid, divisionracedate);
		}


        public int CheckDuplicateSectionalTiming(int divisionraceid, string sectointimingproviderid, string sectiondistanceid, string sectiontiming)
        {
            return new CardsDL().CheckDuplicateSectionalTiming(divisionraceid, sectointimingproviderid, sectiondistanceid, sectiontiming);
        }

        public int CheckDuplicateLapTiming(int divisionraceid,
                                                string professionalprovidernameid,
                                                string HorseNameID,
                                                string lapdistance,
                                                string laptiming)
        {
            return new CardsDL().CheckDuplicateLapTiming(divisionraceid, professionalprovidernameid,HorseNameID,lapdistance,laptiming);
        }

        public int AddAcceptance(DataTable dtAcceptance, DataTable dtAcceptanceStuckOut)
        {
            return new CardsDL().AddAcceptance(dtAcceptance, dtAcceptanceStuckOut);
        }
        
        public DataSet HandicapHorseInformationEntry(
            string racedate,
            int centerid,
            int generalraceid,
            int entrytypeid,
            int horseserialnumber,
            int horseid,
            int genderid,
            int trainerid,
            int ownerid,
            int userid,
            string action,
            int entryid,
            int formid, string yearname, string seasonname, string handicapratingrange,
            string handicapweightlowerRaised, decimal handicapweightlowerRaisedvalue, string handicapdate, string classtypeid,
            string racetype)
        {
            return new CardsDL().HandicapHorseInformationEntry(
                racedate,
                centerid,
                generalraceid,
                entrytypeid,
                horseserialnumber,
                horseid,
                genderid,
                trainerid,
                ownerid,
                userid,
                action,
                entryid,
                formid, yearname, seasonname, handicapratingrange, handicapweightlowerRaised, handicapweightlowerRaisedvalue, 
                handicapdate, classtypeid, racetype);
        }

        public DataTable HandicapHorseInformation(
            string racedate,
            int centerid,
            int generalraceid,
            int entrytypeid,
            int horseserialnumber,
            int horseid,
            int genderid,
            int trainerid,
            int ownerid,
            int userid,
            string action,
            int entryid,
            int formid, string yearname, string seasonname, string handicapratingrange,
            string handicapweightlowerRaised, decimal handicapweightlowerRaisedvalue)
        {
            return new CardsDL().HandicapHorseInformation(
                racedate,
                centerid,
                generalraceid,
                entrytypeid,
                horseserialnumber,
                horseid,
                genderid,
                trainerid,
                ownerid,
                userid,
                action,
                entryid,
                formid, yearname, seasonname, handicapratingrange, handicapweightlowerRaised, handicapweightlowerRaisedvalue);
        }

        public int UpdateHandicapHorseInformation(string serialnumber, int horseage, int horseid, int horsenameid, int horsegenderid,
                        string handicaprating, string MyHandicapRatingRange, string HandicapWeight, string MyHandicapWeight,
                        string HandicapWeightAsPerGender, string TotalHandicapWeightAsperGender, string HWHWP, string MyHWHWP, string HWGHWP,
                        string FHWAWRL, string FMyHWAWRL, string FHWGAWRL, string handicapratinggivenbyclub, int generalracenameid, string racedate, string HandicapWeightAsPerAgeCondition,
						string HWACHWP, string FHWACAWRL)
        {
            return new CardsDL().UpdateHandicapHorseInformation(serialnumber, horseage, horseid, horsenameid, horsegenderid,
                         handicaprating, MyHandicapRatingRange, HandicapWeight,MyHandicapWeight,
                         HandicapWeightAsPerGender, TotalHandicapWeightAsperGender, HWHWP, MyHWHWP, HWGHWP,
                         FHWAWRL, FMyHWAWRL, FHWGAWRL, handicapratinggivenbyclub, generalracenameid, racedate, HandicapWeightAsPerAgeCondition,HWACHWP, FHWACAWRL);
        }

      
       
        public int AddAcceptanceDivision(DataTable dt)
        {
            return new CardsDL().AddAcceptanceDivision(dt);
        }

        public int UpdateAcceptanceGenrealRaceInformation(int generalracenameid, int generalraceid, string generalracename, int centerid,
						string entryracedate, string generalracedate, int divisioncount, string divisionracename, string divisionracedate, string tasktype, int serialno)
        {
            return new CardsDL().UpdateAcceptanceGenrealRaceInformation(generalracenameid, generalraceid, generalracename, centerid,
						   entryracedate, generalracedate, divisioncount, divisionracename, divisionracedate, tasktype, serialno);
        }

        public DataTable GetCardAcceptance(int centerid, string racedate)
        {
            return new CardsDL().GetCardAcceptance(centerid, racedate);
        }

        public DataTable GetCardAcceptanceDivisionRace(int centerid, string racedate, int generalracenameid, string pagename)
        {
            return new CardsDL().GetCardAcceptanceDivisionRace(centerid, racedate, generalracenameid, pagename);
        }


        public int UpdateProvisionRaceDetail(DataTable dt)
        {
            return new CardsDL().UpdateProvisionRaceDetail(dt);
        }


		public int UpdateProvisionRaceDetailSingleRow(DataTable dt, int divisionraceid, string divisionracename)
		{
			return new CardsDL().UpdateProvisionRaceDetailSingleRow(dt, divisionraceid, divisionracename);
		}

		public DataSet GetExport(string racedate, int centerid, string taskType)
        {
            return new CardsDL().GetExport(racedate, centerid, taskType);
        }
        
        public DataTable Import30(DataTable dt, string PageName, int globalid)
        {
            return new CardsDL().Import30(dt, PageName, globalid);
         }

        public DataSet GetDisplayGridviewData(string racedate, int generalracenameid, string taskType, string pagename)
        {
            return new CardsDL().GetDisplayGridviewData(racedate, generalracenameid, taskType, pagename);
        }

        public int AcceptanceUpdate(int id, string generalracename, int generalracenameid, string generalracedate, int divisionraceid, string divisionracename, int hno, string awgbcs, string strucouttype, int acceptanceid, int acceptancestruckoutid, int horseid, int horsenameid)
        {
            return new CardsDL().AcceptanceUpdate(id, generalracename, generalracenameid, generalracedate, divisionraceid, divisionracename, hno, awgbcs, strucouttype, acceptanceid, acceptancestruckoutid,horseid, horsenameid);
        }
        public DataTable GetAcceptanceDivisionDetail(string racedate, int centerid, string pagename)
        {
            return new CardsDL().GetAcceptanceDivisionDetail(racedate, centerid, pagename);
        }

        public DataSet GetCardVeterinary(string racedate, int centerid, string pagename)
        {
            return new CardsDL().GetCardVeterinary(racedate, centerid, pagename);
        }

        public DataTable GetRevisedRatingData(string racedate, int centerid, string pagename)
        {
            return new CardsDL().GetRevisedRatingData(racedate, centerid, pagename);
        }

        public DataSet GetAcceptanceDivisionDetailMultipleReturn(string racedate, int centerid, string pagename)
        {
            return new CardsDL().GetAcceptanceDivisionDetailMultipleReturn(racedate, centerid, pagename);
        }

        public DataSet GetDeclaration(int divisionraceid, int generalracenameid, string divisionracename, string status, string divisionracedate, int centerid)
        {
            return new CardsDL().GetDeclaration(divisionraceid, generalracenameid, divisionracename, status,divisionracedate, centerid);
        }

        public DataSet GetCardResultDisplayData(int divisionraceid, int generalracenameid, string divisionracename, string status, string divisionracedate, int centerid)
        {
            return new CardsDL().GetCardResultDisplayData(divisionraceid, generalracenameid, divisionracename, status, divisionracedate, centerid);
        }

        public int AddDeclaration(DataTable dt)
        {
            return new CardsDL().AddDeclaration(dt);
        }
        public int DeclarationUpdate(int globalid, int jockeynameid, int drawno, int shoemid, int shoemetalmid, string declareweight, string dja,
                int horseno, int horseid, int horsenameid, string bit, string equipment, int horsebandageID)
        {
            return new CardsDL().DeclarationUpdate(globalid, jockeynameid, drawno, shoemid, shoemetalmid, 
                            declareweight,dja, horseno, horseid, horsenameid, bit, equipment, horsebandageID);
        }

        public int AddCardRace(DataTable dt)
        {
            return new CardsDL().AddCardRace(dt);
        }
        public int RaceCardUpdate(int globalid, int ownercolorcapid, string emergencycolorcap, int jockeyid, string pagename, string revisedhandicaprating, int changereasonid, 
                    int runningstatusid, string revisedmyhandicparating, int horseno, int horseid, int horsenameid)
        {
            return new CardsDL().RaceCardUpdate(globalid, ownercolorcapid, emergencycolorcap, jockeyid, pagename, revisedhandicaprating, changereasonid, 
                    runningstatusid, revisedmyhandicparating, horseno, horseid, horsenameid);
        }

        public int RevisedRatingCardUpdate(int id, string revisedhandicaprating, string revisedmyhandicaprating, string eventdetail)
        {
            return new CardsDL().RevisedRatingCardUpdate(id, revisedhandicaprating, revisedmyhandicaprating, eventdetail);
        }

        public int CheckDuplicateRecordRevisedRating(int divisionraceid, int horseid)
        {
            return new CardsDL().CheckDuplicateRecordRevisedRating(divisionraceid,horseid);
        }

        public int AddCardRevisedHandicapRating(DataTable dt)
        {
            return new CardsDL().AddCardRevisedHandicapRating(dt);
        }

        public int AddCardResult(DataTable dt)
        {
            return new CardsDL().AddCardResult(dt);
        }

        public int CardResultUpdate(int globalid, string placing, int professionalnameid, string carriedweight, 
            string finishtime, int verdictmarginid, string horsebodyweight, string revisedhandicaprating,
            string jockeyallowance, string declareweight)
        {
            return new CardsDL().CardResultUpdate(globalid, placing, professionalnameid, carriedweight, 
                finishtime, verdictmarginid, horsebodyweight, revisedhandicaprating, jockeyallowance, declareweight);
        }

        public int ToteDivident(
            int globalid,
            string dateentrydate,
            string divisionracedate,
            int centermid,
            int generalraceid,
            int generalracenameid,
            int divisionraceid,
            int centerid,
            int totevariantid,
            Int64 totevairantamount,
            int userid,
            string action)
        {
            return new CardsDL().ToteDivident(
            globalid,
            dateentrydate,
            divisionracedate,
            centermid,
            generalraceid,
            generalracenameid,
            divisionraceid,
            centerid,
            totevariantid,
            totevairantamount,
            userid,
            action);
        }
        public DataTable GetToteDividentDetail(int divisionid, string pagename, string racedate, string centerid)
        {
            return new CardsDL().GetToteDividentDetail(divisionid,pagename, racedate, centerid);
            
        }

        public int ResultRaceCommentary(
            int globalid,
            int divisionraceid,
            int personnameid,
            int horsenameid,
            string commentary,
            string highlight,
            int userid,
            string action)
        {
            return new CardsDL().ResultRaceCommentary(
             globalid,
             divisionraceid,
             personnameid,
             horsenameid,
             commentary,
             highlight,
             userid,
             action);
        }


        public int ResultRaceIncident(
            int globalid,
            int divisionraceid,
            int personnameid,
            int horsenameid,
            string commentary,
            string highlight,
            int userid,
            string action)
        {
            return new CardsDL().ResultRaceIncident(
             globalid,
             divisionraceid,
             personnameid,
             horsenameid,
             commentary,
             highlight,
             userid,
             action);
        }

        public int AddSectionalTiming(DataTable dt)
        {
            return new CardsDL().AddSectionalTiming(dt);
        }

        public int ResultSectionalTiming(
            int globalid,
            int divisionraceid,
            int sectionaltimingproviderid,
            string sectionaltiming,
            int userid,
            string action,
            int distancebreakupmid)
        {
            return new CardsDL().ResultSectionalTiming(
            globalid,
            divisionraceid,
            sectionaltimingproviderid,
            sectionaltiming,
            userid,
            action,
            distancebreakupmid);
        }

        public int AddLapTiming(DataTable dt)
        {
            return new CardsDL().AddLapTiming(dt);
        }

        public DataTable GetHorseDetail(
            int divisionraceid,
            int horseno)
        {
            return new CardsDL().GetHorseDetail(divisionraceid, horseno);
        }


        public DataTable GetHorseDetail(
            int divisionraceid,
            int horseno,
            int pageno)
        {
            return new CardsDL().GetHorseDetail(divisionraceid, horseno, pageno);
        }

        public int ResultLapTiming(
           int globalid,
           int divisionraceid,
           int Laptimingproviderid,
           string Laptiming,
           int userid,
           string action,
           int masterdistance,
           string horsenameid)
        {
            return new CardsDL().ResultLapTiming(
           globalid,
           divisionraceid,
           Laptimingproviderid,
           Laptiming,
           userid,
           action, masterdistance, horsenameid);
        }

        public DataTable GetHorseNameAutoFillerMultiple(string autoFillName, string prefix, string multipleconditions)
        {
             return new CardsDL().GetHorseNameAutoFillerMultiple(autoFillName, prefix, multipleconditions);
        }

        public DataTable GetHorseNameAutoFillerMultiplewithoutsplit(string autoFillName, string prefix, string multipleconditions)
        {
            return new CardsDL().GetHorseNameAutoFillerMultiplewithoutsplit(autoFillName, prefix, multipleconditions);
        }

        public int RaceDayReport(
           int globalid,
           string dataentrydate,
           int divisionraceid,
           int reporterid,
           int horseid,
           string report,
           int userid,
           string action,
           string highlight,
           string incidentid)
        {
            return new CardsDL().RaceDayReport(globalid,
                    dataentrydate,
                    divisionraceid,
                    reporterid,
                    horseid,
                    report,
                    userid,
                    action,
                    highlight,
                    incidentid);
        }
        public DataSet GetCardIncidentDetail(int divisionid, int centerid, string dataentrydate, string pagename)
        {
            return new CardsDL().GetCardIncidentDetail(divisionid,
                    centerid,
                    dataentrydate,
                    pagename);
        }
        public int CardIncident(
           int globalid,
           string dateentrydate,
           int divisionraceid,
           int horseid,
           int incidentmid,
           string incidentdetail,
           int userid,
           string action)
        {
            return new CardsDL().CardIncident(
             globalid,
           dateentrydate,
           divisionraceid,
           horseid,
           incidentmid,
           incidentdetail,
           userid,
           action);
        }

        public int AddRaceReview(DataTable dt, int divisionraceid)
        {
            return new CardsDL().AddRaceReview(dt, divisionraceid);
        }


        public int CardRaceReview(
            int globalid,
            int peddockconditionid,
            bool earlygategoer,
            string firstsectionalpostion,
            string bandposition,
            string lastsectionalpostion,
            int runqualityid,
            int traineronboardefforid,
            int jockeyonboardeffortid,
            int matetypeid,
            int bettingorderid,
            int userid,
            string action)
        {
            return new CardsDL().CardRaceReview(
             globalid,
            peddockconditionid,
            earlygategoer,
            firstsectionalpostion,
            bandposition,
            lastsectionalpostion,
             runqualityid,
             traineronboardefforid,
             jockeyonboardeffortid,
             matetypeid,
             bettingorderid,
             userid,
            action);
        }

        public int AddCardOdds(DataTable dt, int divisionraceid)
        {
            return new CardsDL().AddCardOdds(dt, divisionraceid);
        }

        public int CardOdd(
          int globalid,
          Double? now,
          Double? mow,
          Double? loow,
          Double? oow,
          Double? oop,
          Double? mdw,
          Double? mop,
          Double? cow,
          Double? cop,
          int userid,
          string action)
        {
            return new CardsDL().CardOdd(
             globalid,
            now,
            mow,
            loow,
            oow,
            oop,
             mdw,
             mop,
             cow,
             cop,
             userid,
            action);

        }

        public int AddRaceSituation(DataTable dt, int divisionraceid)
        {
            return new CardsDL().AddRaceSituation(dt, divisionraceid);
        }


        
        public int RaceCardSituation(
           int globalid,
           string realracetime,
           Double? penetrometerreading,
            int goingid,
            int weatherid,
            string temprature,
            bool raininmorning,
                bool rainbeforerace,
                    bool rainduringrace,
                        string otherfactor,
           int userid,
           string action)
        {

         return new CardsDL().RaceCardSituation(
           globalid,
           realracetime,
           penetrometerreading,
           goingid,
           weatherid,
           temprature,
           raininmorning,
           rainbeforerace,
           rainduringrace,
           otherfactor,
           userid,
           action);
         }

        public DataTable HotLine(int hotlinerid, string racedate, int centerid, int Professionalnameid, int horseid, string hotline, int userid, string action)
        {
            return new CardsDL().HotLine(
          hotlinerid,
          racedate,
          centerid,
          Professionalnameid,
          horseid,
          hotline,
          userid,
          action);

        }


        public DataTable Tip(int hotlinerid, string racedate, int centerid, int Professionalnameid, int horseid, int tiptypeid, int userid, string action)
        {
            return new CardsDL().Tip(
          hotlinerid,
          racedate,
          centerid,
          Professionalnameid,
          horseid,
          tiptypeid,
          userid,
          action);

        }

        public int RaceCardSelection(
           VKATalkClassLayer.CardSelection cs,
           string racedate, string action)
        {
            return new CardsDL().RaceCardSelection(
           cs,
           racedate, action);
        }


        public DataTable MockRaceEntry(
            int mockraceentryid,
           string dataentrydate,
           string mockracedate,
            int centerid,
            int sourcenameid,
            int mockraceno,
            int distanceid,
            int userid,
            string action)
        {
            return new CardsDL().MockRaceEntry(
          mockraceentryid,
          dataentrydate,mockracedate,
          centerid,
          sourcenameid,
          mockraceno,
          distanceid,
          userid,
          action);
        }


        public DataTable MockRaceHorseEntry(
            int participatigHorseid,
            int mockraceid_fk,
            string dataentrydate,
            int placing,
            Double drawno,
            int horsenameid,
            int ridernameid,
            int carriedweight,
            string finishtime,
            int verdictmarginid,
            string comments,
            int userid,
            string action)
        {

            return new CardsDL().MockRaceHorseEntry(
                participatigHorseid,
         mockraceid_fk,
          dataentrydate,
            placing,
            drawno,
            horsenameid,
            ridernameid,
            carriedweight,
            finishtime,
            verdictmarginid,
            comments,
            userid,
            action);
        }

        public DataTable MockRaceDistanceBreakUp(
            int MRDistanceBreakUpCID,
            int mockraceid_fk,
            int sourcepnameid,
            int distancebreakupmid,
            string timetaken,
            string comments,
            int userid,
            string action)
        {
            return new CardsDL().MockRaceDistanceBreakUp(
                MRDistanceBreakUpCID,
                mockraceid_fk,
                sourcepnameid,
                distancebreakupmid,
                timetaken,
                comments,
                userid,
                action
                );

        }

        public DataTable MockRaceEquipment(
           int MREquipmentCID,
           int mockraceid_fk,
           int sourcepnameid,
           int horsenameid,
           int equipmentid,
           int userid,
           string action)
        {
            return new CardsDL().MockRaceEquipment(
           MREquipmentCID,
           mockraceid_fk,
           sourcepnameid,
           horsenameid,
           equipmentid,
           userid,
           action
                );
        }


        public DataTable MockRaceIndividual(
          int MREquipmentCID,
          int mockraceid_fk,
          int sourcepnameid,
          int horsenameid,
          string comments,
          int userid,
          string action)
        {
            return new CardsDL().MockRaceIndividual(
           MREquipmentCID,
           mockraceid_fk,
           sourcepnameid,
           horsenameid,
           comments,
           userid,
           action
                );
        }


        public DataTable GatePracticeEntry(
            int mockraceentryid,
           string dataentrydate,
            int sourcenameid,
           string mockracedate,
            int centerid,
            int lotid,
            int distanceid,
            int trackid,
            int userid,
            string action)
        {
            return new CardsDL().GatePracticeEntry(
            mockraceentryid,
            dataentrydate,
            sourcenameid,
            mockracedate,
            centerid,
            lotid,
            distanceid,
            trackid,
            userid,
            action);
        }

        public DataTable GatePracticeHorseEntry(
            int participatigHorseid,
            int mockraceid_fk,
            string dataentrydate,
            int placing,
            Double drawno,
            int horsenameid,
            int ridernameid,
            int carriedweight,
            string finishtime,
            int verdictmarginid,
            string comments,
            int userid,
            string action)
        {

            return new CardsDL().GatePracticeHorseEntry(
                participatigHorseid,
         mockraceid_fk,
          dataentrydate,
            placing,
            drawno,
            horsenameid,
            ridernameid,
            carriedweight,
            finishtime,
            verdictmarginid,
            comments,
            userid,
            action);
        }

        public DataTable GatePracticeDistanceBreakUp(
            int MRDistanceBreakUpCID,
            int mockraceid_fk,
            int sourcepnameid,
            int distancebreakupmid,
            string timetaken,
            string comments,
            int userid,
            string action)
        {
            return new CardsDL().GatePracticeDistanceBreakUp(
                MRDistanceBreakUpCID,
                mockraceid_fk,
                sourcepnameid,
                distancebreakupmid,
                timetaken,
                comments,
                userid,
                action
                );

        }


        public DataTable GetPracticeIndividual(
          int MREquipmentCID,
          int mockraceid_fk,
          int sourcepnameid,
          int horsenameid,
          string comments,
          int userid,
          string action)
        {
            return new CardsDL().GetPracticeIndividual(
           MREquipmentCID,
           mockraceid_fk,
           sourcepnameid,
           horsenameid,
           comments,
           userid,
           action
                );
        }

        public int CardTrack(
           int TrackBunchCID,
           string DataEntryDate,
           int sourcepnameid,
           int centerid,
           string trackdate,
           int distanceid,
           int trackid,
           int horsenameid,
           int ridernameid,
           int distancebreakupid,
           string timetaken,
            int paceid,
            int firstequimentid,
            int secondequipmentid,
            int verdictmarginid,
           string comments,
            string individualcomments,
           int userid,
           string action,
           string workouttype,
           string drawno,
           string carriedweight,
           int dbc,
           int ihcc,
           int workquality,
           int wr,
           int wim,
           int isshow)
        {
            return new CardsDL().CardTrack(
           TrackBunchCID,
           DataEntryDate,
           sourcepnameid,
           centerid,
           trackdate,
           distanceid,
           trackid,
           horsenameid,
           ridernameid,
           distancebreakupid,
           timetaken,
           paceid,
           firstequimentid,
           secondequipmentid,
           verdictmarginid,
           comments,
           individualcomments,
           userid,
           action,
           workouttype,
           drawno,
           carriedweight,
           dbc,
           ihcc,
           workquality,
           wr,wim,isshow
           );
        }


        public DataTable CardTrackSelect(
           int TrackBunchCID,
           string DataEntryDate,
           int sourcepnameid,
           int centerid,
           string trackdate,
           int distanceid,
           int trackid,
           int horsenameid,
           int ridernameid,
           int distancebreakupid,
           string timetaken,
            int paceid,
            int firstequimentid,
            int secondequipmentid,
            int verdictmarginid,
           string comments,
            string individualcomments,
           int userid,
           string action)
        {
            return new CardsDL().CardTrackSelect(
           TrackBunchCID,
           DataEntryDate,
           sourcepnameid,
           centerid,
           trackdate,
           distanceid,
           trackid,
           horsenameid,
           ridernameid,
           distancebreakupid,
           timetaken,
           paceid,
           firstequimentid,
           secondequipmentid,
           verdictmarginid,
           comments,
           individualcomments,
           userid,
           action);
        }


		public void HandicapUpdate(string generalracieid, string generalracenameid, string weightdetail, string value)
		{
			new CardsDL().HandicapUpdate(generalracieid, generalracenameid, weightdetail, value);
		}

		public DataTable GetAcceptanceWeight(int divisionraceid, int handicapid)
		{
			return new CardsDL().GetAcceptanceWeight(divisionraceid, handicapid);
		}

		public DataTable GetAcceptanceOldRecordStatus(int generalracenameid, string generalracedate, int centerid, string season, string year)
		{
			return new CardsDL().GetAcceptanceOldRecordStatus(generalracenameid, generalracedate, centerid, season, year);
		}

		public DataTable GetDeclareOldRecordStatus(int generalracenameid, string generalracedate, int centerid, string season, string year, string pagename, int divisionraceid, int horsenameid)
		{
			return new CardsDL().GetDeclareOldRecordStatus(generalracenameid, generalracedate, centerid, season, year, pagename, divisionraceid, horsenameid);
		}

        public void InsertDeclarationBitEquipemnt(int divisionraceid, int horseid,
                                                        int horsenameid, string bit, string equipment, string pagename)
        {
            new CardsDL().InsertDeclarationBitEquipemnt(divisionraceid, horseid, horsenameid, bit, equipment, pagename);
        }

        public int GetProfessionalID(int professionalnameid)
		{
			return new CardsDL().GetProfessionalID(professionalnameid);
		}

        public DataTable GetHorseAchivementData(int horseid, int horsenameid)
        {
            return new CardsDL().GetHorseAchivementData(horseid, horsenameid);
        }

        public DataTable GetCardWorkoutSourceName()
        {
            return new CardsDL().GetCardWorkoutSourceName();
        }

        public DataSet GetCardTrackGridviewData(string racedate, int centerid, string sourcenameid, int horseid)
        {
            return new CardsDL().GetCardTrackGridviewData(racedate, centerid, sourcenameid, horseid);
        }

        public DataSet GetCardTrackGridviewDataWorkOutType(string racedate, int centerid, string sourcenameid, int horseid)
        {
            return new CardsDL().GetCardTrackGridviewDataWorkOutType(racedate, centerid, sourcenameid, horseid);
        }

        public int HandicapDuplicateCheck(int generalraceid, int generalracenameid, int horseid)
        {
            return new CardsDL().HandicapDuplicateCheck(generalraceid, generalracenameid, horseid);
        }

        public DataTable ImportCardFiles(string field1, string field2, string field3, string field4, string field5, string field6, string field7, string field8, string field9,
                                           string field10, string field11, string field12, string field13, string field14, string field15, string field16, string field17,
                                           string field18, string field19, string field20, string field21, string field22, string field23, string field24, string field25,
                                           string field26, string field27, string field28, string field29, string field30, string field31, string pagename)
        {
            return new CardsDL().ImportCardFiles(field1, field2, field3, field4, field5, field6, field7, field8, field9,
                                                field10,field11,field12,field13, field14,field15,field16,field17,
                                                field18,field19,field20,field21, field22,field23,field24,field25,
                                                field26,field27,field28,field29, field30,field31,pagename);
        }

        public DataTable GetTrackInformation(string entrydate, string trackdate, int horsenameid, int ridernameid, int centerid)
        {
            return new CardsDL().GetTrackInformation(entrydate, trackdate, horsenameid, ridernameid,centerid);
        }

    }
}
