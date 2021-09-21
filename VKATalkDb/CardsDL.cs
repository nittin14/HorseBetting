using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VKADB
{
    public class CardsDL
    {
        private SqlConnection _conn;
        public CardsDL()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);//sdlfjsldfj
        }

        /// <summary>
        /// Bind dropdown on the basis of the selected value
        /// </summary>
        /// <param name="DropDownName"></param>
        /// <returns></returns>
        public DataTable GetHorseName(string DropDownName, string racedate)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@DropdownName", SqlDbType.VarChar, 100);
                arParams[0].Value = DropDownName;

                arParams[1] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardHandicapHorseName", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }

        public DataTable GetRaceCenterName(string racedate)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[1];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetRaceCenterInformation", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        public DataTable GetWorkOutHorseInformation(string racedate)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[1];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetHorseInformationAcceptance", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }

        public DataSet GetEntryDateInformation(int generalracenameid, string cardname, string season, string  year)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[4];

                arParams[0] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalracenameid };
                arParams[1] = new SqlParameter("@CardName", SqlDbType.VarChar, 500) { Value = cardname };
				arParams[2] = new SqlParameter("@Season", SqlDbType.VarChar, 500) { Value = season };
				arParams[3] = new SqlParameter("@Year", SqlDbType.VarChar, 500) { Value = year };
				ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetEntryDateInformation  ", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }

        /// <summary>
        /// Global Dropdown Bind - Just pass the dropdown Name and in SP add the if condition
        /// </summary>
        /// <param name="PageName"></param>
        /// <returns></returns>
        public DataTable GetDropdownBind(string DropDownName)
        {
            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                arParams[0] = new SqlParameter("@DropdownName", SqlDbType.VarChar, 100);
                arParams[0].Value = DropDownName;
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetDropdownBind", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }

        public DataTable GetRaceGeneralRaceDetail(string racedate, int centerid)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[2];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //  arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetRaceGeneralInformation", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


		public DataTable GetRaceGeneralRaceDetailAcceptance(string racedate, int centerid)
		{
			var dt = new DataTable();
			try
			{
				SqlParameter[] arParams = new SqlParameter[2];

				arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
				if (racedate.Equals("__-__-____"))
				{
					arParams[0].Value = DBNull.Value;
				}
				else
				{
					string[] dateString = racedate.Split('-');
					DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
					arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
				}
				arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
				dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetRaceCardAcceptance", arParams);
			}
			catch (Exception ex)
			{
				ErrorHandling.CheckEachSteps(ex.StackTrace);
				ErrorHandling.SendErrorToText(ex);
				throw;
			}

			return dt;
		}

        public DataTable GetRaceGeneralRaceDetailAcceptanceBaseCenter(string racedate, int centerid)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[2];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //  arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetRaceCardAcceptance", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }



        public DataTable GetFinalHorseGenTrainOwenerInformation(int horsenameid, string racedate)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[2];

                arParams[0] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                arParams[1] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enterDate =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enterDate.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardHorseGenderTraiOwner", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        public DataTable HorseInformation(string racedate, int centerid, int generalraceid, int entrytypeid, int horseserialnumber,
            int horseid, int genderid, int trainerid, int ownerid, int userid, string action, int entryid, int formid, string entrydate,
                bool sweepstakeentry, bool struckout, int struckoutstageid)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[17];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalraceid };
                arParams[3] = new SqlParameter("@EntryTypeID", SqlDbType.Int) { Value = entrytypeid };
                arParams[4] = new SqlParameter("@HSerialNumber", SqlDbType.Int) { Value = horseserialnumber };
                arParams[5] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[6] = new SqlParameter("@GenderID", SqlDbType.Int) { Value = genderid };
                arParams[7] = new SqlParameter("@TrainerNameID", SqlDbType.Int) { Value = trainerid };
                arParams[8] = new SqlParameter("@OwnerNameID", SqlDbType.Int) { Value = ownerid };
                arParams[9] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[10] = new SqlParameter("@Action", SqlDbType.VarChar, 50) { Value = action };
                arParams[11] = new SqlParameter("@EntryID", SqlDbType.Int) { Value = entryid };
                arParams[12] = new SqlParameter("@FormID", SqlDbType.Int) { Value = formid };
                arParams[13] = new SqlParameter("@EntryDate", SqlDbType.VarChar, 30);
                if (entrydate.Equals("__-__-____"))
                {
                    arParams[13].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = entrydate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[13].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[14] = new SqlParameter("@SweepStakeEntry", SqlDbType.Bit) { Value = sweepstakeentry };
                arParams[15] = new SqlParameter("@Struckout", SqlDbType.Bit) { Value = struckout };
                arParams[16] = new SqlParameter("@Struckoutstageid", SqlDbType.Int) { Value = struckoutstageid };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_CardHorseInformation", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


      

        public DataTable GetHorseNameAutoFiller(string autoFillName, string prefix, string multipleconditions)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                string[] conditions = multipleconditions.Split(',');
                if(conditions.Length > 1)
                {
                    arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                    arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
                    arParams[2] = new SqlParameter("@HorseCenter", SqlDbType.VarChar, 100) { Value = conditions[0].ToString() };
                    arParams[3] = new SqlParameter("@HandicapRatingHorse", SqlDbType.VarChar, 100) { Value = conditions[1].ToString() };
                    arParams[4] = new SqlParameter("@AgeHorse", SqlDbType.VarChar, 100) { Value = conditions[2].ToString() };
                    arParams[5] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = Convert.ToInt32(conditions[3]) };
                    arParams[6] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = Convert.ToInt32(conditions[4]) };
                    arParams[7] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                    if (conditions[5].Equals("__-__-____"))
                    {
                        arParams[7].Value = DBNull.Value;
                    }
                    else
                    {
                        string[] dateString = conditions[5].Split('-');
                        DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                        arParams[7].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                        //arParams[7].Value = Convert.ToDateTime(dtformat);
                    }
                }
                else
                {
                    arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                    arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
                    arParams[2] = new SqlParameter("@HorseCenter", SqlDbType.VarChar, 100) { Value = string.Empty };
                    arParams[3] = new SqlParameter("@HandicapRatingHorse", SqlDbType.VarChar, 100) { Value = string.Empty };
                    arParams[4] = new SqlParameter("@AgeHorse", SqlDbType.VarChar, 100) { Value = string.Empty };
                    arParams[5] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = 0 };
                    arParams[6] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = 0 };
                    arParams[7] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30) { Value = DBNull.Value } ;
                    
                }
                
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardHorseNameAutoFill", arParams);
            }
            catch (Exception ex)
            {

                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }

        public DataTable GetHorseNameAutoFillerHotliner(string autoFillName, string prefix, string multipleconditions)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {

                string[] conditions = multipleconditions.Split(',');
                arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
                arParams[2] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (multipleconditions.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = multipleconditions.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[7].Value = Convert.ToDateTime(dtformat);
                }
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardHorseNameAutoFillHotliner", arParams);
            }
            catch (Exception ex)
            {

                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }

        public DataTable GetHorseNameAutoFillerMultiple(string autoFillName, string prefix, string multipleconditions)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {

                string[] conditions = multipleconditions.Split(',');
                arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
                arParams[2] = new SqlParameter("@NextParameter", SqlDbType.VarChar, 100) { Value = conditions[0].ToString() };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardHorseNameAutoFillMultiple", arParams);
            }
            catch (Exception ex)
            {

                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }

        public DataTable GetHorseNameAutoFillerMultiplewithoutsplit(string autoFillName, string prefix, string multipleconditions)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {

               // string[] conditions = multipleconditions.Split(',');
                arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
                arParams[2] = new SqlParameter("@NextParameter", SqlDbType.VarChar, 100) { Value = multipleconditions };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardHorseNameAutoFillMultiplewithoutsplit", arParams);
            }
            catch (Exception ex)
            {

                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }

        public DataTable GetCardAutoFiller(string autoFillName, string prefix)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardAutoFill", arParams);
            }
            catch (Exception ex)
            {

                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }





        public DataTable Handicap(string racedate, int centerid, int generalraceid, string entrytype, int horseserialnumber, int horseid, int genderid, int trainerid, int ownerid, int userid, string action, int entryid)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[12];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@GeneralRaceID", SqlDbType.Int) { Value = generalraceid };
                arParams[3] = new SqlParameter("@EntryType", SqlDbType.VarChar, 100) { Value = entrytype };
                arParams[4] = new SqlParameter("@HSerialNumber", SqlDbType.Int) { Value = horseserialnumber };
                arParams[5] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[6] = new SqlParameter("@GenderID", SqlDbType.Int) { Value = genderid };
                arParams[7] = new SqlParameter("@TrainerID", SqlDbType.Int) { Value = trainerid };
                arParams[8] = new SqlParameter("@OwnerID", SqlDbType.Int) { Value = ownerid };
                //arParams[9] = new SqlParameter("@RowNumber", SqlDbType.Int) { Value = rownumber };
                arParams[9] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[10] = new SqlParameter("@Action", SqlDbType.VarChar, 50) { Value = action };
                arParams[11] = new SqlParameter("@EntryID", SqlDbType.Int) { Value = entryid };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_CardHandicap", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        public DataSet HandicapHorseInformationEntry(string racedate, int centerid, int generalraceid, int entrytypeid, int horseserialnumber,
                        int horseid, int genderid, int trainerid, int ownerid, int userid, string action, int entryid, int formid,
                        string yearname, string seasonname, string handicapratingrange, string handicapweightlowerRaised, decimal handicapweightlowerRaisedvalue,
                        string handicapdate, string classtypeid, string racetype)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[20];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalraceid };
                arParams[3] = new SqlParameter("@EntryTypeID", SqlDbType.Int) { Value = entrytypeid };
                arParams[4] = new SqlParameter("@HSerialNumber", SqlDbType.Int) { Value = horseserialnumber };
                arParams[5] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[6] = new SqlParameter("@GenderID", SqlDbType.Int) { Value = genderid };
                arParams[7] = new SqlParameter("@TrainerID", SqlDbType.Int) { Value = trainerid };
                arParams[8] = new SqlParameter("@OwnerID", SqlDbType.Int) { Value = ownerid };
                arParams[9] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[10] = new SqlParameter("@Action", SqlDbType.VarChar, 50) { Value = action };
                arParams[11] = new SqlParameter("@EntryID", SqlDbType.Int) { Value = entryid };
                arParams[12] = new SqlParameter("@FormID", SqlDbType.Int) { Value = formid };
                arParams[13] = new SqlParameter("@YearName", SqlDbType.VarChar, 100) { Value = yearname };
                arParams[14] = new SqlParameter("@SeasonName", SqlDbType.VarChar, 100) { Value = seasonname };
                arParams[15] = new SqlParameter("@HandicapRatingRange", SqlDbType.VarChar, 100) { Value = handicapratingrange };
                arParams[16] = new SqlParameter("@HandicapWeightLowerRaised", SqlDbType.VarChar, 100) { Value = handicapweightlowerRaised };
                arParams[17] = new SqlParameter("@HandicapWeightLowerRaisedValue", SqlDbType.Decimal) { Value = handicapweightlowerRaisedvalue };
				arParams[18] = new SqlParameter("@ClassTypeID", SqlDbType.VarChar, 100) { Value = classtypeid };
                arParams[19] = new SqlParameter("@RaceType", SqlDbType.VarChar, 100) { Value = racetype };
                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_CardHandicapHorse_Entry", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }

        public DataTable HandicapHorseInformation(string racedate, int centerid, int generalraceid, int entrytypeid, int horseserialnumber,
                        int horseid, int genderid, int trainerid, int ownerid, int userid, string action, int entryid, int formid,
                        string yearname, string seasonname, string handicapratingrange, string handicapweightlowerRaised, decimal handicapweightlowerRaisedvalue)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[18];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalraceid };
                arParams[3] = new SqlParameter("@EntryTypeID", SqlDbType.Int) { Value = entrytypeid };
                arParams[4] = new SqlParameter("@HSerialNumber", SqlDbType.Int) { Value = horseserialnumber };
                arParams[5] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[6] = new SqlParameter("@GenderID", SqlDbType.Int) { Value = genderid };
                arParams[7] = new SqlParameter("@TrainerID", SqlDbType.Int) { Value = trainerid };
                arParams[8] = new SqlParameter("@OwnerID", SqlDbType.Int) { Value = ownerid };
                //arParams[9] = new SqlParameter("@RowNumber", SqlDbType.Int) { Value = rownumber };
                arParams[9] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[10] = new SqlParameter("@Action", SqlDbType.VarChar, 50) { Value = action };
                arParams[11] = new SqlParameter("@EntryID", SqlDbType.Int) { Value = entryid };
                arParams[12] = new SqlParameter("@FormID", SqlDbType.Int) { Value = formid };
                arParams[13] = new SqlParameter("@YearName", SqlDbType.VarChar, 100) { Value = yearname };
                arParams[14] = new SqlParameter("@SeasonName", SqlDbType.VarChar, 100) { Value = seasonname };
                arParams[15] = new SqlParameter("@HandicapRatingRange", SqlDbType.VarChar, 100) { Value = handicapratingrange };
                arParams[16] = new SqlParameter("@HandicapWeightLowerRaised", SqlDbType.VarChar, 100) { Value = handicapweightlowerRaised };
                arParams[17] = new SqlParameter("@HandicapWeightLowerRaisedValue", SqlDbType.Decimal) { Value = handicapweightlowerRaisedvalue };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_CardHandicapHorse", arParams);
                //dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_CardHandicapHorse", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }

        public DataTable GetRaceGeneralRaceDetailFuture(int centerid, int seasonid, int yearid)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[3];
                arParams[0] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[1] = new SqlParameter("@SeasonID", SqlDbType.Int) { Value = seasonid };
                arParams[2] = new SqlParameter("@YearID", SqlDbType.Int) { Value = yearid };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetRaceGeneralInformationFuture", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        public int AddHandicap(DataTable dt)
        {
            DataTable dtresult;
            int result = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    using (SqlBulkCopy bulkCopy =
                            new SqlBulkCopy(_conn.ConnectionString))
                    {
                        bulkCopy.ColumnMappings.Add("HandicapEnterDate", "HandicapEnterDate");
                        bulkCopy.ColumnMappings.Add("RaceDate", "RaceDate");

                        bulkCopy.ColumnMappings.Add("GeneralRaceID", "GeneralRaceID");
                        bulkCopy.ColumnMappings.Add("GeneralRaceNameID", "GeneralRaceNameID");
                        bulkCopy.ColumnMappings.Add("HrSrNo", "HrSrNo");
                        bulkCopy.ColumnMappings.Add("HorseID", "HorseID");
                        bulkCopy.ColumnMappings.Add("HorseNameID", "HorseNameID");
                        bulkCopy.ColumnMappings.Add("Age", "Age");
                        bulkCopy.ColumnMappings.Add("GenderID", "GenderID");
                        bulkCopy.ColumnMappings.Add("HandicapRating", "HandicapRating");
                        bulkCopy.ColumnMappings.Add("MyHandicapRating", "MyHandicapRating");
                        bulkCopy.ColumnMappings.Add("HdWghHRSOW", "HdWghHRSOW");
                        bulkCopy.ColumnMappings.Add("HdWghMyHRSOW", "HdWghMyHRSOW");
                        bulkCopy.ColumnMappings.Add("HdWghAPGender", "HdWghAPGender");
                        bulkCopy.ColumnMappings.Add("TotalHdWghPenalty", "TotalHdWghPenalty");
                        bulkCopy.ColumnMappings.Add("HdWghHWP", "HdWghHWP");
                        bulkCopy.ColumnMappings.Add("MyHdWghHWP", "MyHdWghHWP");
                        bulkCopy.ColumnMappings.Add("HdWghAPGenderHWP", "HdWghAPGenderHWP");
                        bulkCopy.ColumnMappings.Add("HdWghRaisedLowered", "HdWghRaisedLowered");
                        bulkCopy.ColumnMappings.Add("HdWghRaisedLoweredValue", "HdWghRaisedLoweredValue");
                        bulkCopy.ColumnMappings.Add("FinalHdWghAWRL", "FinalHdWghAWRL");
                        bulkCopy.ColumnMappings.Add("FinalMyHdWghAWRL", "FinalMyHdWghAWRL");
                        bulkCopy.ColumnMappings.Add("FinalHdWghAPGenderAWRL", "FinalHdWghAPGenderAWRL");
                        bulkCopy.ColumnMappings.Add("HdWghGBC", "HdWghGBC");
                        bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                        bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                        bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                        bulkCopy.ColumnMappings.Add("CenterID", "CenterID");
						bulkCopy.ColumnMappings.Add("HWAC", "HWAC");
						bulkCopy.ColumnMappings.Add("HWACHWP", "HWACHWP");
						bulkCopy.ColumnMappings.Add("FHWACAWRL", "FHWACAWRL");
						bulkCopy.ColumnMappings.Add("BanDetail", "BanDetail");
						bulkCopy.DestinationTableName = "dbo.Card_Handicap";
                        bulkCopy.WriteToServer(dt);
                    }
                    result = 1;
                }

                _conn.Close();
            }
            catch (Exception ex)
            {
                result = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
        }


        public int UpdateHandicapHorseInformation(string serialnumber, int horseage, int horseid, int horsenameid, int horsegenderid,
                        string handicaprating, string MyHandicapRatingRange, string HandicapWeight, string MyHandicapWeight,
                        string HandicapWeightAsPerGender, string TotalHandicapWeightAsperGender, string HWHWP, string MyHWHWP, string HWGHWP,
                        string FHWAWRL, string FMyHWAWRL, string FHWGAWRL, string handicapratinggivenbyclub, int generalracenameid, string racedate,
						string HandicapWeightAsPerAgeCondition, string HWACHWP, string FHWACAWRL)
        {

            int checkRecord;

            SqlParameter[] arParams = new SqlParameter[23];
            try
            {
                arParams[0] = new SqlParameter("@SerialNumber", SqlDbType.Int) { Value = serialnumber };
                arParams[1] = new SqlParameter("@HorseAge", SqlDbType.Int) { Value = horseage };
                arParams[2] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[3] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                arParams[4] = new SqlParameter("@HorseGenderID", SqlDbType.Int) { Value = horsegenderid };
                arParams[5] = new SqlParameter("@HandicapRating", SqlDbType.VarChar, 30) { Value = handicaprating };
                arParams[6] = new SqlParameter("@MyHandicapRatingRange", SqlDbType.VarChar, 30) { Value = MyHandicapRatingRange };
                arParams[7] = new SqlParameter("@HandicapWeight", SqlDbType.VarChar, 30) { Value = HandicapWeight };
                arParams[8] = new SqlParameter("@MyHandicapWeight", SqlDbType.VarChar, 30) { Value = MyHandicapWeight };
                arParams[9] = new SqlParameter("@HandicapWeightAsPerGender", SqlDbType.VarChar, 30) { Value = HandicapWeightAsPerGender };
                arParams[10] = new SqlParameter("@TotalHandicapWeightAsperGender", SqlDbType.VarChar, 30) { Value = TotalHandicapWeightAsperGender };
                arParams[11] = new SqlParameter("@HWHWP", SqlDbType.VarChar, 30) { Value = HWHWP };
                arParams[12] = new SqlParameter("@MyHWHWP", SqlDbType.VarChar, 30) { Value = MyHWHWP };
                arParams[13] = new SqlParameter("@HWGHWP", SqlDbType.VarChar, 30) { Value = HWGHWP };
                arParams[14] = new SqlParameter("@FHWAWRL", SqlDbType.VarChar, 30) { Value = FHWAWRL };
                arParams[15] = new SqlParameter("@FMyHWAWRL", SqlDbType.VarChar, 30) { Value = FMyHWAWRL };
                arParams[16] = new SqlParameter("@FHWGAWRL", SqlDbType.VarChar, 30) { Value = FHWGAWRL };
                arParams[17] = new SqlParameter("@Handicapratinggivenbyclub", SqlDbType.VarChar, 30) { Value = handicapratinggivenbyclub };
                arParams[18] = new SqlParameter("@Generalracenameid", SqlDbType.Int) { Value = generalracenameid };
                arParams[19] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[19].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[19].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[19].Value = Convert.ToDateTime(dtformat);
                }
				arParams[20] = new SqlParameter("@HWAC", SqlDbType.VarChar, 30) { Value = HandicapWeightAsPerAgeCondition };
				arParams[21] = new SqlParameter("@HWACHWP", SqlDbType.VarChar, 30) { Value = HWACHWP };
				arParams[22] = new SqlParameter("@FHWACAWRL", SqlDbType.VarChar, 30) { Value = FHWACAWRL };
				checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_UpdateHandicapHorseInformation", arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;

        }



        public int AddAcceptanceDivision(DataTable dt)
        {
            DataTable dtresult;
            int result = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    using (SqlBulkCopy bulkCopy =
                            new SqlBulkCopy(_conn.ConnectionString))
                    {
                        bulkCopy.ColumnMappings.Add("GeneralRaceID", "GeneralRaceID");
                        bulkCopy.ColumnMappings.Add("GeneralRaceNameID", "GeneralRaceNameID");
                        bulkCopy.ColumnMappings.Add("NoOfDivisions", "NoOfDivisions");
                        bulkCopy.ColumnMappings.Add("DivisionRaceName", "DivisionRaceName");
                        bulkCopy.ColumnMappings.Add("DivisionRaceDate", "DivisionRaceDate");
                        bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                        bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                        bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                        bulkCopy.ColumnMappings.Add("GeneralRaceDate", "GeneralRaceDate");
                        bulkCopy.ColumnMappings.Add("CenterID", "CenterID");
                        bulkCopy.ColumnMappings.Add("EntryDate", "EntryDate");
						bulkCopy.ColumnMappings.Add("SerialNo", "SerialNo");
						bulkCopy.DestinationTableName = "dbo.Prospectus_Division";
                        bulkCopy.WriteToServer(dt);
                    }
                    result = 1;
                }

                _conn.Close();
            }
            catch (Exception ex)
            {
                result = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
        }


        public int UpdateAcceptanceGenrealRaceInformation(int generalracenameid, int generalraceid, string generalracename,  int centerid,
						string entryracedate, string generalracedate, int divisioncount, string divisionracename , string divisionracedate, 
							string tasktype, int serialno)
        {

            int checkRecord;

            SqlParameter[] arParams = new SqlParameter[11];
            try
            {
                arParams[0] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalracenameid };
                arParams[1] = new SqlParameter("@GeneralRaceID", SqlDbType.Int) { Value = generalracenameid };
				arParams[2] = new SqlParameter("@GeneralRaceName", SqlDbType.VarChar, 2000) { Value = generalracename };
				arParams[3] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
				arParams[4] = new SqlParameter("@EntryRaceDate", SqlDbType.VarChar, 30);
				if (entryracedate.Equals("__-__-____"))
				{
					arParams[4].Value = DBNull.Value;
				}
				else
				{
					string[] dateString = entryracedate.Split('-');
					DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
					arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
				}
				arParams[5] = new SqlParameter("@GeneralRaceDate", SqlDbType.VarChar, 30);
				if (generalracedate.Equals("__-__-____"))
				{
					arParams[5].Value = DBNull.Value;
				}
				else
				{
					string[] dateString = generalracedate.Split('-');
					DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
					arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
					//arParams[7].Value = Convert.ToDateTime(dtformat);
				}

				arParams[6] = new SqlParameter("@DivisionCount", SqlDbType.Int) { Value = divisioncount };
				arParams[7] = new SqlParameter("@DivisionRaceName", SqlDbType.VarChar, 2000) { Value = divisionracename };
				arParams[8] = new SqlParameter("@DivisionRaceDate", SqlDbType.VarChar, 30);
                if (divisionracedate.Equals("__-__-____"))
                {
                    arParams[8].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = divisionracedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[8].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }
                arParams[9] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
				arParams[10] = new SqlParameter("@SerialNO", SqlDbType.Int) { Value = serialno };


				checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_AcceptanceGeneralRaceNameUpdate", arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;

        }


		public int DivisionRaceCount(int generalracenameid, int centerid, string divisionracedate)
		{

			int checkRecord;

			SqlParameter[] arParams = new SqlParameter[3];
			try
			{
				arParams[0] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalracenameid };
				arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
				arParams[2] = new SqlParameter("@DivisionRaceDate", SqlDbType.VarChar, 30);
				if (divisionracedate.Equals("__-__-____"))
				{
					arParams[2].Value = DBNull.Value;
				}
				else
				{
					string[] dateString = divisionracedate.Split('-');
					DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
					arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
					//arParams[4].Value = Convert.ToDateTime(dtformat);
				}
				
				checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_DivisionRaceCount", arParams));
			}
			catch (Exception ex)
			{
				checkRecord = 2;
				ErrorHandling.CheckEachSteps(ex.StackTrace);
				ErrorHandling.SendErrorToText(ex);
				throw;
			}
			finally
			{
				if (_conn.State == ConnectionState.Open)
				{
					_conn.Close();
				}
			}
			return checkRecord;

		}


		public DataTable GetCardAcceptance(int centerid, string racedate)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[2];
                arParams[0] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[1] = new SqlParameter("@GeneralRaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardDivisionAcceptance", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        public DataTable GetCardAcceptanceDivisionRace(int centerid, string racedate, int generalracenameid, string pagename)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[4];
                arParams[0] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[1] = new SqlParameter("@GeneralRaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }
                arParams[2] = new SqlParameter("@GeneralRaceNameId", SqlDbType.Int) { Value = generalracenameid };
                arParams[3] = new SqlParameter("@PageName", SqlDbType.VarChar, 100) { Value = pagename };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardDivisionRace", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        public int UpdateProvisionRaceDetail(DataTable dt)
        {
            //var dt = new DataTable();
            try
            {
                string connstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connstring))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateDivisionRaceDetail"))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@tblProvisionRaceDate", dt);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            //con.Close();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return 1;
        }


		public int UpdateProvisionRaceDetailSingleRow(DataTable dt, int divisionraceid, string divisionracename)
		{
			//var dt = new DataTable();
			try
			{
				string connstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
				using (SqlConnection con = new SqlConnection(connstring))
				{
					using (SqlCommand cmd = new SqlCommand("sp_UpdateDivisionRaceDetailSingleRow"))
					{
						try
						{
							cmd.CommandType = CommandType.StoredProcedure;
							cmd.Connection = con;
							cmd.Parameters.AddWithValue("@tblProvisionRaceDate", dt);
							cmd.Parameters.AddWithValue("@DivionRaceID", divisionraceid);
                            cmd.Parameters.AddWithValue("@DivionRaceName", divisionracename);
                            con.Open();
							cmd.ExecuteNonQuery();
							//con.Close();
						}
						catch (Exception ex)
						{
							throw;
						}
						finally
						{
							con.Close();
						}
					}
				}

			}
			catch (Exception ex)
			{
				ErrorHandling.CheckEachSteps(ex.StackTrace);
				ErrorHandling.SendErrorToText(ex);
				throw;
			}

			return 1;
		}


		public DataSet GetExport(string racedate, int centerid, string taskType)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {
                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[1].Value = centerid;
                arParams[2] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[2].Value = taskType;
                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_CardsExport",
                    arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return ds;
        }

        public DataTable ImportCardFiles(string field1, string field2, string field3, string field4, string field5, string field6, string field7, string field8, string field9,
                                            string field10, string field11, string field12, string field13, string field14, string field15, string field16, string field17, 
                                            string field18, string field19, string field20, string field21, string field22, string field23, string field24, string field25, 
                                            string field26, string field27, string field28, string field29, string field30, string field31, string pagename)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[32];

                arParams[0] = new SqlParameter("@Field1", SqlDbType.VarChar, 5000) { Value = field1 };
                arParams[1] = new SqlParameter("@Field2", SqlDbType.VarChar, 5000) { Value = field2 };
                arParams[2] = new SqlParameter("@Field3", SqlDbType.VarChar, 5000) { Value = field3 };
                arParams[3] = new SqlParameter("@Field4", SqlDbType.VarChar, 5000) { Value = field4 };
                arParams[4] = new SqlParameter("@Field5", SqlDbType.VarChar, 5000) { Value = field5 };
                arParams[5] = new SqlParameter("@Field6", SqlDbType.VarChar, 5000) { Value = field6 };
                arParams[6] = new SqlParameter("@Field7", SqlDbType.VarChar, 5000) { Value = field7 };
                arParams[7] = new SqlParameter("@Field8", SqlDbType.VarChar, 5000) { Value = field8 };
                arParams[8] = new SqlParameter("@Field9", SqlDbType.VarChar, 5000) { Value = field9 };
                arParams[9] = new SqlParameter("@Field10", SqlDbType.VarChar, 5000) { Value = field10 };
                arParams[10] = new SqlParameter("@Field11", SqlDbType.VarChar, 5000) { Value = field11 };
                arParams[11] = new SqlParameter("@Field12", SqlDbType.VarChar, 5000) { Value = field12 };
                arParams[12] = new SqlParameter("@Field13", SqlDbType.VarChar, 5000) { Value = field13 };
                arParams[13] = new SqlParameter("@Field14", SqlDbType.VarChar, 5000) { Value = field14 };
                arParams[14] = new SqlParameter("@Field15", SqlDbType.VarChar, 5000) { Value = field15 };
                arParams[15] = new SqlParameter("@Field16", SqlDbType.VarChar, 5000) { Value = field16 };
                arParams[16] = new SqlParameter("@Field17", SqlDbType.VarChar, 5000) { Value = field17 };
                arParams[17] = new SqlParameter("@Field18", SqlDbType.VarChar, 5000) { Value = field18 };
                arParams[18] = new SqlParameter("@Field19", SqlDbType.VarChar, 5000) { Value = field19 };
                arParams[19] = new SqlParameter("@Field20", SqlDbType.VarChar, 5000) { Value = field20 };
                arParams[20] = new SqlParameter("@Field21", SqlDbType.VarChar, 5000) { Value = field21 };
                arParams[21] = new SqlParameter("@Field22", SqlDbType.VarChar, 5000) { Value = field22 };
                arParams[22] = new SqlParameter("@Field23", SqlDbType.VarChar, 5000) { Value = field23 };
                arParams[23] = new SqlParameter("@Field24", SqlDbType.VarChar, 5000) { Value = field24 };
                arParams[24] = new SqlParameter("@Field25", SqlDbType.VarChar, 5000) { Value = field25 };
                arParams[25] = new SqlParameter("@Field26", SqlDbType.VarChar, 5000) { Value = field26 };
                arParams[26] = new SqlParameter("@Field27", SqlDbType.VarChar, 5000) { Value = field27 };
                arParams[27] = new SqlParameter("@Field28", SqlDbType.VarChar, 5000) { Value = field28 };
                arParams[28] = new SqlParameter("@Field29", SqlDbType.VarChar, 5000) { Value = field29 };
                arParams[29] = new SqlParameter("@Field30", SqlDbType.VarChar, 5000) { Value = field30 };
                arParams[30] = new SqlParameter("@Field31", SqlDbType.VarChar, 5000) { Value = field31 };
                arParams[31] = new SqlParameter("@PageName", SqlDbType.VarChar, 500) { Value = pagename };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_CardImport", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        /// <summary>
        /// Insert Excel data in the database
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable Import30(DataTable dt, string PageName, int globalid)
        {
            DataTable dtresult;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    copy.ColumnMappings.Add(0, 0);
                    copy.ColumnMappings.Add(1, 1);
                    copy.ColumnMappings.Add(2, 2);
                    copy.ColumnMappings.Add(3, 3);
                    copy.ColumnMappings.Add(4, 4);
                    copy.ColumnMappings.Add(5, 5);
                    copy.ColumnMappings.Add(6, 6);
                    copy.ColumnMappings.Add(7, 7);
                    copy.ColumnMappings.Add(8, 8);
                    copy.ColumnMappings.Add(9, 9);
                    copy.ColumnMappings.Add(10, 10);
                    copy.ColumnMappings.Add(11, 11);
                    copy.ColumnMappings.Add(12, 12);
                    copy.ColumnMappings.Add(13, 13);
                    copy.ColumnMappings.Add(14, 14);
                    copy.ColumnMappings.Add(15, 15);
                    copy.ColumnMappings.Add(16, 16);
                    copy.ColumnMappings.Add(17, 17);
                    copy.ColumnMappings.Add(18, 18);
                    copy.ColumnMappings.Add(19, 19);
                    copy.ColumnMappings.Add(20, 20);
                    copy.ColumnMappings.Add(21, 21);
                    copy.ColumnMappings.Add(22, 22);
                    copy.ColumnMappings.Add(23, 23);
                    copy.ColumnMappings.Add(24, 24);
                    copy.ColumnMappings.Add(25, 25);
                    copy.ColumnMappings.Add(26, 26);
                    copy.ColumnMappings.Add(27, 27);
                    copy.ColumnMappings.Add(28, 28);
                    copy.ColumnMappings.Add(29, 29);
					copy.ColumnMappings.Add(30, 30);
					copy.ColumnMappings.Add(31, 31);
					copy.DestinationTableName = "dbo.Master_Import30";
                    try
                    {
                        copy.WriteToServer(dt);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandling.CheckEachSteps(ex.StackTrace);
                        ErrorHandling.SendErrorToText(ex);
                        throw;
                    }
                }

                arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                arParams[1] = new SqlParameter("@GlobalID", SqlDbType.Int);
                arParams[1].Value = globalid;

                dtresult = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_ImportProspectusMASTER", arParams);

                _conn.Close();
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dtresult;
        }



        public int AddAcceptance(DataTable dtAcceptance, DataTable dtAcceptanceStuckOut)
        {
            DataTable dtresult;
            int result = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                _conn.Open();
                using (SqlBulkCopy bulkCopy =
                            new SqlBulkCopy(_conn.ConnectionString))
                {
                    bulkCopy.ColumnMappings.Add("AcceptanceEnterDate", "AcceptanceEnterDate");
                    bulkCopy.ColumnMappings.Add("GeneralRaceDate", "GeneralRaceDate");
                    bulkCopy.ColumnMappings.Add("CenterID", "CenterID");
                    bulkCopy.ColumnMappings.Add("GeneralRaceID", "GeneralRaceID");
                    bulkCopy.ColumnMappings.Add("GeneralRaceNameID", "GeneralRaceNameID");
                    bulkCopy.ColumnMappings.Add("StruckOutHorseNo", "StruckOutHorseNo");
                    bulkCopy.ColumnMappings.Add("HorseID", "HorseID");
                    bulkCopy.ColumnMappings.Add("HorseNameID", "HorseNameID");
                    bulkCopy.ColumnMappings.Add("AcptWghRaisedLowered", "AcptWghRaisedLowered");
                    bulkCopy.ColumnMappings.Add("AcptWghRaisedLoweredValue", "AcptWghRaisedLoweredValue");
                    bulkCopy.ColumnMappings.Add("AcceptanceWeightAWRL", "AcceptanceWeightAWRL");
                    bulkCopy.ColumnMappings.Add("AcceptanceWeightGBC", "AcceptanceWeightGBC");
                    bulkCopy.ColumnMappings.Add("StruckOutType", "StruckOutType");
                    bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                    bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                    bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                    bulkCopy.DestinationTableName = "dbo.Card_Acceptance_StruckOut";
                    bulkCopy.WriteToServer(dtAcceptanceStuckOut);
                }

                using (SqlBulkCopy bulkCopy1 =
                            new SqlBulkCopy(_conn.ConnectionString))
                {
                    bulkCopy1.ColumnMappings.Add("AcceptanceEnterDate", "AcceptanceEnterDate");
                    bulkCopy1.ColumnMappings.Add("GeneralRaceDate", "GeneralRaceDate");
                    bulkCopy1.ColumnMappings.Add("CenterMID", "CenterMID");
                    bulkCopy1.ColumnMappings.Add("GeneralRaceID", "GeneralRaceID");
                    bulkCopy1.ColumnMappings.Add("GeneralRaceNameID", "GeneralRaceNameID");
                    bulkCopy1.ColumnMappings.Add("DivisionedRaceID", "DivisionedRaceID");
                    bulkCopy1.ColumnMappings.Add("HorseNo", "HorseNo");
                    bulkCopy1.ColumnMappings.Add("HorseID", "HorseID");
                    bulkCopy1.ColumnMappings.Add("HorseNameID", "HorseNameID");
                    bulkCopy1.ColumnMappings.Add("AcptWghRaisedLowered", "AcptWghRaisedLowered");
                    bulkCopy1.ColumnMappings.Add("AcptWghRaisedLoweredValue", "AcptWghRaisedLoweredValue");
                    bulkCopy1.ColumnMappings.Add("AcceptanceWeightAWRL", "AcceptanceWeightAWRL");
                    bulkCopy1.ColumnMappings.Add("AcceptanceWeightGBC", "AcceptanceWeightGBC");
                    bulkCopy1.ColumnMappings.Add("CreatedDate", "CreatedDate");
                    bulkCopy1.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                    bulkCopy1.ColumnMappings.Add("IsActive", "IsActive");
                    bulkCopy1.DestinationTableName = "dbo.Card_Acceptance";
                    bulkCopy1.WriteToServer(dtAcceptance);


                }

                result = 1;

                _conn.Close();
            }
            catch (Exception ex)
            {
                result = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
        }


        public DataSet GetDisplayGridviewData(string racedate, int generalracenameid, string taskType, string pagename)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@GeneralRaceNameId", SqlDbType.Int);
                arParams[1].Value = generalracenameid;
                arParams[2] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[2].Value = taskType;
                arParams[3] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                arParams[3].Value = pagename;
                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetAcceptanceData",
                    arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return ds;
        }


        public int AcceptanceUpdate(int id, string generalracename, int generalracenameid, string generalracedate, int divisionraceid, 
                                        string divisionracename, int hno, string awgbcs, string strucouttype, 
                                            int acceptanceid, int acceptancestruckoutid, int horseid, int horsenameid)
        {
            int status = 0;
            try
            {
             
                SqlParameter[] arParams = new SqlParameter[13];

                arParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                arParams[1] = new SqlParameter("@GeneralRaceName", SqlDbType.VarChar, 1000) { Value = generalracename };
                arParams[2] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalracenameid };
                arParams[3] = new SqlParameter("@GeneralRaceDate", SqlDbType.VarChar, 30);
                if (generalracedate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    generalracedate = generalracedate.Split(' ')[0];
                    ErrorHandling.CheckEachSteps("generalracedate:  " + generalracedate);
                    string[] dateString;
                    if (generalracedate.Contains("/"))
                        dateString = generalracedate.Split('/');
                    else
                        dateString = generalracedate.Split('-');
                        DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                        arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[4] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[5] = new SqlParameter("@DivisionRaceName", SqlDbType.VarChar,500) { Value = divisionracename };
                arParams[6] = new SqlParameter("@HNo", SqlDbType.Int) { Value = hno };
                arParams[7] = new SqlParameter("@AWGBCS", SqlDbType.VarChar, 500) { Value = awgbcs };
                arParams[8] = new SqlParameter("@StrucOutType", SqlDbType.VarChar, 500) { Value = strucouttype };
                arParams[9] = new SqlParameter("@AcceptanceID", SqlDbType.Int) { Value = acceptanceid };
                arParams[10] = new SqlParameter("@AcceptanceStruckOutID", SqlDbType.Int) { Value = acceptancestruckoutid };
                arParams[11] = new SqlParameter("@horseID", SqlDbType.Int) { Value = horseid };
                arParams[12] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_AcceptanceUpdate", arParams));
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps("AcceptanceUpdate:  " + id + "," + generalracename + "," + generalracenameid + "," + generalracedate + "," + divisionraceid + "," +
                                        divisionracename + "," + hno + "," + awgbcs + "," + strucouttype + "," +
                                            acceptanceid + "," + acceptancestruckoutid + "," + horseid);
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return status;
        }


        public DataTable GetAcceptanceDivisionDetail(string racedate, int centerid, string pagename)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[3];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@PageName", SqlDbType.VarChar, 100) { Value = pagename };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetDeclarationDivision", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        public DataSet GetCardVeterinary(string racedate, int centerid, string pagename)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[3];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@PageName", SqlDbType.VarChar, 100) { Value = pagename };
                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetDeclarationDivision", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }


        public DataTable GetRevisedRatingData(string racedate, int centerid, string pagename)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[3];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@PageName", SqlDbType.VarChar, 100) { Value = pagename };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetRevisedRatingData", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }

        public DataSet GetAcceptanceDivisionDetailMultipleReturn(string racedate, int centerid, string pagename)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[3];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@PageName", SqlDbType.VarChar, 100) { Value = pagename };
                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetDeclarationDivision", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }

        public DataSet GetDeclaration(int divisionraceid, int generalracenameid, string divisionracename, string status, string divisionracedate, int centerid)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[6];

                arParams[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@GENERALRACENAMEID", SqlDbType.Int) { Value = generalracenameid };
                arParams[2] = new SqlParameter("@DivisionRaceName", SqlDbType.VarChar, 1000) { Value = divisionracename };
                arParams[3] = new SqlParameter("@DivisionRaceDate", SqlDbType.VarChar, 30);
                if (divisionracedate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = divisionracedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[5] = new SqlParameter("@Status", SqlDbType.VarChar, 100) { Value = status };

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetDeclaration", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }


        public DataSet GetCardResultDisplayData(int divisionraceid, int generalracenameid, string divisionracename, string status, string divisionracedate, int centerid)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[6];

                arParams[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@GENERALRACENAMEID", SqlDbType.Int) { Value = generalracenameid };
                arParams[2] = new SqlParameter("@DivisionRaceName", SqlDbType.VarChar, 1000) { Value = divisionracename };
                arParams[3] = new SqlParameter("@DivisionRaceDate", SqlDbType.VarChar, 30);
                if (divisionracedate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = divisionracedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[5] = new SqlParameter("@Status", SqlDbType.VarChar, 100) { Value = status };

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetCardResultDisplayData", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }


        public DataSet GetResultselectedRowforUpdate(int GlobalID, int divisionraceid,int horsenameid)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[3];

                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = GlobalID };
                arParams[1] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[2] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetCardResultInformationUpdateClick", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }

        public int AddDeclaration(DataTable dt)
        {
            DataTable dtresult;
            int result = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    using (SqlBulkCopy bulkCopy =
                            new SqlBulkCopy(_conn.ConnectionString))
                    {
                        bulkCopy.ColumnMappings.Add("DataEntryDate", "DataEntryDate");
                        bulkCopy.ColumnMappings.Add("DivisionRaceDate", "DivisionRaceDate");
                        bulkCopy.ColumnMappings.Add("CenterMID", "CenterMID");
                        bulkCopy.ColumnMappings.Add("GeneralRaceID", "GeneralRaceID");
                        bulkCopy.ColumnMappings.Add("GeneralRaceNameID", "GeneralRaceNameID");
                        bulkCopy.ColumnMappings.Add("DivisionRaceID", "DivisionRaceID");
                        bulkCopy.ColumnMappings.Add("HorseNo", "HorseNo");
                        bulkCopy.ColumnMappings.Add("HorseID", "HorseID");
                        bulkCopy.ColumnMappings.Add("HorseNameID", "HorseNameID");
                        bulkCopy.ColumnMappings.Add("JockeyID", "JockeyID");
                        bulkCopy.ColumnMappings.Add("DrawNo", "DrawNo");
                        bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                        bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                        bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
						bulkCopy.ColumnMappings.Add("ShoeMID", "ShoeMID");
						bulkCopy.ColumnMappings.Add("ShoeMetalMID", "ShoeMetalMID");
						bulkCopy.ColumnMappings.Add("JockeyNamePID", "JockeyNamePID");
                        bulkCopy.ColumnMappings.Add("DeclareJockeyAllowance", "DeclareJockeyAllowance");
                        bulkCopy.ColumnMappings.Add("DeclareWeight", "DeclareWeight");
                        bulkCopy.ColumnMappings.Add("HorseBandageID", "HorseBandageID");
                        bulkCopy.DestinationTableName = "dbo.Card_Declaration";
                        bulkCopy.WriteToServer(dt);
                    }
                    result = 1;
                }

                _conn.Close();
            }
            catch (Exception ex)
            {
                result = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
        }

        public int DeclarationUpdate(int globalid, int jockeynameid, int drawno, int shoemid, int shoemetalmid, string declareweight,string dja, 
                int horseno, int horseid, int horsenameid, string bit, string equipment, int horsebandageid)
        {

            int checkRecord;

            SqlParameter[] arParams = new SqlParameter[13];
            try
            {
                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@JockeyNameID", SqlDbType.Int) { Value = jockeynameid };
                arParams[2] = new SqlParameter("@DrawNo", SqlDbType.Int) { Value = drawno };
				arParams[3] = new SqlParameter("@ShoeMID", SqlDbType.Int) { Value = shoemid };
				arParams[4] = new SqlParameter("@ShoeMetalMID", SqlDbType.Int) { Value = shoemetalmid };
                arParams[5] = new SqlParameter("@DeclareWeight", SqlDbType.VarChar, 50) { Value = declareweight };
                arParams[6] = new SqlParameter("@DeclareJockeyAllowance", SqlDbType.VarChar, 50) { Value = dja };
                arParams[7] = new SqlParameter("@HorseNo", SqlDbType.Int) { Value = horseno };
                arParams[8] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[9] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                arParams[10] = new SqlParameter("@Bit", SqlDbType.VarChar,500) { Value = bit };
                arParams[11] = new SqlParameter("@Equipment", SqlDbType.VarChar, 500) { Value = equipment };
                arParams[12] = new SqlParameter("@HorseBandageID", SqlDbType.Int) { Value = horsebandageid };
                checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_UpdateDeclaration", arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;

        }

        public int AddCardRace(DataTable dt)
        {
            DataTable dtresult;
            int result = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    using (SqlBulkCopy bulkCopy =
                            new SqlBulkCopy(_conn.ConnectionString))
                    {
                        bulkCopy.ColumnMappings.Add("DataEntryDate", "DataEntryDate");
                        bulkCopy.ColumnMappings.Add("DivisionRaceDate", "DivisionRaceDate");
                        bulkCopy.ColumnMappings.Add("CenterMID", "CenterMID");
                        bulkCopy.ColumnMappings.Add("GeneralRaceID", "GeneralRaceID");
                        bulkCopy.ColumnMappings.Add("GeneralRaceNameID", "GeneralRaceNameID");
                        bulkCopy.ColumnMappings.Add("DivisionRaceID", "DivisionRaceID");
                        bulkCopy.ColumnMappings.Add("HorseNo", "HorseNo");
                        bulkCopy.ColumnMappings.Add("HorseID", "HorseID");
                        bulkCopy.ColumnMappings.Add("HorseNameID", "HorseNameID");
                        bulkCopy.ColumnMappings.Add("OwnerCapColorID", "OwnerCapColorID");
                        bulkCopy.ColumnMappings.Add("EmergencyOwnerColor", "EmergencyOwnerColor");
                        bulkCopy.ColumnMappings.Add("ChangedJockeyID", "ChangedJockeyID");
                        bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                        bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                        bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                        bulkCopy.ColumnMappings.Add("ChangedJockeyNamePID", "ChangedJockeyNamePID");
                        bulkCopy.ColumnMappings.Add("ChangedDeclareJockeyAllowance", "ChangedDeclareJockeyAllowance");
                        bulkCopy.ColumnMappings.Add("ChangedDeclareWeight", "ChangedDeclareWeight");
                        bulkCopy.ColumnMappings.Add("JockeyChangeReasonCMID", "JockeyChangeReasonCMID");
                        bulkCopy.ColumnMappings.Add("HorseRunningStatusCMID", "HorseRunningStatusCMID");
                        bulkCopy.DestinationTableName = "dbo.Card_RaceCard";
                        bulkCopy.WriteToServer(dt);
                    }
                    result = 1;
                }

                _conn.Close();
            }
            catch (Exception ex)
            {
                result = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
        }


        public int RaceCardUpdate(int globalid, int ownercolorcapid, string emergencycolorcap, int jockeyid, string pagename, 
            string revisedhandicparating, int changereasonid, int runningstatusid, string revisedmyhandicparating,
                int horseno, int horseid, int horsenameid)
        {

            int checkRecord;

            SqlParameter[] arParams = new SqlParameter[12];
            try
            {
                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@OwnerColorCapID", SqlDbType.Int) { Value = ownercolorcapid };
                arParams[2] = new SqlParameter("@EmergencyColor", SqlDbType.VarChar, 100) { Value = emergencycolorcap };
                arParams[3] = new SqlParameter("@JockeyID", SqlDbType.Int) { Value = jockeyid };
                arParams[4] = new SqlParameter("@PageName", SqlDbType.VarChar, 100) { Value = pagename };
                arParams[5] = new SqlParameter("@RevisedHandicapRating", SqlDbType.VarChar, 100) { Value = revisedhandicparating };
                arParams[6] = new SqlParameter("@ChangeReasonID", SqlDbType.Int) { Value = changereasonid };
                arParams[7] = new SqlParameter("@RunningStatusID", SqlDbType.Int) { Value = runningstatusid };
                arParams[8] = new SqlParameter("@RevisedMyHandicapRating", SqlDbType.VarChar, 100) { Value = revisedmyhandicparating };
                arParams[9] = new SqlParameter("@HorseNo", SqlDbType.Int) { Value = horseno };
                arParams[10] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[11] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };

                checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_UpdateRaceCard", arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;

        }


        public int RevisedRatingCardUpdate(int id, string revisedhandicaprating, string revisedmyhandicaprating, string eventdetail)
        {

            int checkRecord;

            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                arParams[1] = new SqlParameter("@HandicapRating", SqlDbType.VarChar,50) { Value = revisedhandicaprating };
                arParams[2] = new SqlParameter("@MyHandicapRating", SqlDbType.VarChar, 50) { Value = revisedmyhandicaprating };
                arParams[3] = new SqlParameter("@EventDetail", SqlDbType.VarChar, 50) { Value = eventdetail };

                checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_RevisedRatinigUpdate", arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;

        }


        public int CheckDuplicateRecordRevisedRating(int divisionraceid, int horseid)
        {

            int checkRecord;

            try
            {
                SqlParameter[] arParams1 = new SqlParameter[2];
                arParams1[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams1[1] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_CheckDuplicateReviseRating", arParams1));
            }
            catch (Exception ex)
            {
                checkRecord = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;

        }


        public int AddCardRevisedHandicapRating(DataTable dt)
        {
            DataTable dtresult;
            int result = 0;

            int checkRecord;

                SqlParameter[] arParams = new SqlParameter[12];
                try
                {
                arParams[0] = new SqlParameter("@DataEntryDate", SqlDbType.VarChar, 100) { Value = dt.Rows[0]["DataEntryDate"].ToString() };
                arParams[1] = new SqlParameter("@DivisionRaceDate", SqlDbType.VarChar, 100) { Value = dt.Rows[0]["DivisionRaceDate"].ToString() };
                arParams[2] = new SqlParameter("@CenterMID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["CenterMID"]) };
                arParams[3] = new SqlParameter("@GeneralRaceID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["GeneralRaceID"]) };
                arParams[4] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["GeneralRaceNameID"]) };
                arParams[5] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = dt.Rows[0]["DivisionRaceID"] };
                arParams[6] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["HorseID"]) };
                arParams[7] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["HorseNameID"]) };
                arParams[8] = new SqlParameter("@RevisedHandicapRating", SqlDbType.VarChar, 100) { Value = dt.Rows[0]["RevisedHandicapRating"].ToString() };
                arParams[9] = new SqlParameter("@HorseNo", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["HorseNo"]) };
                arParams[10] = new SqlParameter("@RevisedMyHandicapRating", SqlDbType.VarChar, 100) { Value = dt.Rows[0]["RevisedMyHandicapRating"].ToString() };
                arParams[11] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = "Insert" };
                checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_RevisedHandicapRating", arParams));
                checkRecord = 1;
                //        bulkCopy.ColumnMappings.Add("DataEntryDate", "DataEntryDate");
                //        bulkCopy.ColumnMappings.Add("DivisionRaceDate", "DivisionRaceDate");
                //        bulkCopy.ColumnMappings.Add("CenterMID", "CenterMID");
                //        bulkCopy.ColumnMappings.Add("GeneralRaceID", "GeneralRaceID");
                //        bulkCopy.ColumnMappings.Add("GeneralRaceNameID", "GeneralRaceNameID");
                //        bulkCopy.ColumnMappings.Add("DivisionRaceID", "DivisionRaceID");
                //        bulkCopy.ColumnMappings.Add("HorseNo", "HorseNo");
                //        bulkCopy.ColumnMappings.Add("HorseID", "HorseID");
                //        bulkCopy.ColumnMappings.Add("HorseNameID", "HorseNameID");
                //        bulkCopy.ColumnMappings.Add("RevisedHandicapRating", "RevisedHandicapRating");
                //        bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                //        bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                //        bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                //        bulkCopy.ColumnMappings.Add("RevisedMyHandicapRating", "RevisedMyHandicapRating");
                //        bulkCopy.DestinationTableName = "dbo.Card_RevisedHandicapRating";
                //        bulkCopy.WriteToServer(dt);
                //    }
                //    result = 1;
                //}

                //_conn.Close();
            }
                catch (Exception ex)
                {
                checkRecord = 2;
                    ErrorHandling.CheckEachSteps(ex.StackTrace);
                    ErrorHandling.SendErrorToText(ex);
                    throw;
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }
            
            return result;
        }



        public int AddCardResult(DataTable dt)
        {
            int checkRecord;
            SqlParameter[] arParams = new SqlParameter[19];
            try
            {
                arParams[0] = new SqlParameter("@DataEntryDate", SqlDbType.VarChar,100) { Value = dt.Rows[0]["DataEntryDate"].ToString() };
                arParams[1] = new SqlParameter("@DivisionRaceDate", SqlDbType.VarChar, 100) { Value = dt.Rows[0]["DivisionRaceDate"].ToString() };
                arParams[2] = new SqlParameter("@CenterMID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["CenterMID"]) };
                arParams[3] = new SqlParameter("@GeneralRaceID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["GeneralRaceID"]) };
                arParams[4] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["GeneralRaceNameID"]) };
                arParams[5] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = dt.Rows[0]["DivisionRaceID"] };
                arParams[6] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["HorseID"]) };
                arParams[7] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["HorseNameID"]) };
                arParams[8] = new SqlParameter("@Placing", SqlDbType.VarChar,100) { Value = dt.Rows[0]["Placing"].ToString() };
                arParams[9] = new SqlParameter("@JockeyID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["JockeyID"]) };
                arParams[10] = new SqlParameter("@CarriedWeight", SqlDbType.VarChar, 100) { Value = dt.Rows[0]["CarriedWeight"].ToString() };
                arParams[11] = new SqlParameter("@FinishTime", SqlDbType.VarChar,100) { Value = dt.Rows[0]["FinishTime"].ToString() };
                arParams[12] = new SqlParameter("@VerdictMarginMID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["VerdictMarginMID"]) };
                arParams[13] = new SqlParameter("@HorseBodyWeight", SqlDbType.VarChar,100) { Value = dt.Rows[0]["HorseBodyWeight"].ToString() };
                arParams[14] = new SqlParameter("@RevisedRating", SqlDbType.VarChar,100) { Value = dt.Rows[0]["RevisedRating"].ToString() };
                arParams[15] = new SqlParameter("@JockeyNamePID", SqlDbType.Int) { Value = Convert.ToInt32(dt.Rows[0]["JockeyNamePID"]) };
                arParams[16] = new SqlParameter("@JockeyAllowance", SqlDbType.VarChar,100) { Value = dt.Rows[0]["JockeyAllowance"].ToString() };
                arParams[17] = new SqlParameter("@FinalDeclareWeight", SqlDbType.VarChar,100) { Value = dt.Rows[0]["FinalDeclareWeight"].ToString() };
                arParams[18] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = dt.Rows[0]["Action"].ToString() };

                checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "card_CardResult", arParams));
                checkRecord = 1;
            }
            catch (Exception ex)
            {
                checkRecord = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
            //DataTable dtresult;
            //int result = 0;
            //SqlParameter[] arParams = new SqlParameter[2];
            //try
            //{

            //    _conn.Open();
            //    //using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
            //    //{
            //    //    using (SqlBulkCopy bulkCopy =
            //    //            new SqlBulkCopy(_conn.ConnectionString))
            //    //    {
            //    //        bulkCopy.ColumnMappings.Add("DataEntryDate", "DataEntryDate");
            //    //        bulkCopy.ColumnMappings.Add("DivisionRaceDate", "DivisionRaceDate");
            //    //        bulkCopy.ColumnMappings.Add("CenterMID", "CenterMID");
            //    //        bulkCopy.ColumnMappings.Add("GeneralRaceID", "GeneralRaceID");
            //    //        bulkCopy.ColumnMappings.Add("GeneralRaceNameID", "GeneralRaceNameID");
            //    //        bulkCopy.ColumnMappings.Add("DivisionRaceID", "DivisionRaceID");
            //    //        bulkCopy.ColumnMappings.Add("HorseID", "HorseID");
            //    //        bulkCopy.ColumnMappings.Add("HorseNameID", "HorseNameID");
            //    //        bulkCopy.ColumnMappings.Add("Placing", "Placing");
            //    //        bulkCopy.ColumnMappings.Add("JockeyID", "JockeyID");
            //    //        bulkCopy.ColumnMappings.Add("CarriedWeight", "CarriedWeight");
            //    //        bulkCopy.ColumnMappings.Add("FinishTime", "FinishTime");
            //    //        bulkCopy.ColumnMappings.Add("VerdictMarginMID", "VerdictMarginMID");
            //    //        bulkCopy.ColumnMappings.Add("HorseBodyWeight", "HorseBodyWeight");
            //    //        bulkCopy.ColumnMappings.Add("RevisedRating", "RevisedRating");
            //    //        bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
            //    //        bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
            //    //        bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
            //    //        bulkCopy.DestinationTableName = "dbo.Card_Result";
            //    //        bulkCopy.WriteToServer(dt);
            //    //    }
            //    //    result = 1;
            //    //}

            //    _conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    result = 2;
            //    ErrorHandling.CheckEachSteps(ex.StackTrace);
            //    ErrorHandling.SendErrorToText(ex);
            //    throw;
            //}
            //finally
            //{
            //    if (_conn.State == ConnectionState.Open)
            //    {
            //        _conn.Close();
            //    }
            //}
            //return result;
        }


        public int CardResultUpdate(int globalid, string placing, int professionalnameid, string carriedweight, string finishtime, 
                                int verdictmarginid, string horsebodyweight, string revisedhandicaprating,
                                string jockeyallowance, string declareweight)
        {

            int checkRecord;

            SqlParameter[] arParams = new SqlParameter[10];
            try
            {
                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@Placing", SqlDbType.VarChar, 100) { Value = placing };
                arParams[2] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int) { Value = professionalnameid };
                arParams[3] = new SqlParameter("@CarriedWeight", SqlDbType.VarChar, 100) { Value = carriedweight };
                arParams[4] = new SqlParameter("@FinishTime", SqlDbType.VarChar, 100) { Value = finishtime };
                arParams[5] = new SqlParameter("@VerdictMarginID", SqlDbType.Int) { Value = verdictmarginid };
                arParams[6] = new SqlParameter("@HorseBodyWeight", SqlDbType.VarChar, 100) { Value = horsebodyweight };
                arParams[7] = new SqlParameter("@RevisedHandicapRating", SqlDbType.VarChar, 100) { Value = revisedhandicaprating };
                arParams[8] = new SqlParameter("@JockeyAllowance", SqlDbType.VarChar, 100) { Value = jockeyallowance };
                arParams[9] = new SqlParameter("@DeclareWeight", SqlDbType.VarChar, 100) { Value = declareweight };

                checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_UpdateRaceCardResult", arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;

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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[12];
            try
            {

                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@DataEntryDate", SqlDbType.VarChar, 30);
                if (dateentrydate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dateentrydate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[2] = new SqlParameter("@DivisionRaceDate", SqlDbType.VarChar, 30);
                if (divisionracedate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = divisionracedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[3] = new SqlParameter("@CenterMID", SqlDbType.Int) { Value = centermid };
                arParams[4] = new SqlParameter("@GeneralRaceID", SqlDbType.Int) { Value = generalraceid };
                arParams[5] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalracenameid };
                arParams[6] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[7] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[8] = new SqlParameter("@ToteVariantID", SqlDbType.Int) { Value = totevariantid };
                arParams[9] = new SqlParameter("@ToteDividentAmount", SqlDbType.BigInt) { Value = totevairantamount };
                arParams[10] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[11] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ToteVariant",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
        }

        public DataTable GetToteDividentDetail(int divisionid, string pagename, string racedate, string centerid)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {

                arParams[0] = new SqlParameter("@DivisionID", SqlDbType.Int);
                arParams[0].Value = divisionid;

                arParams[1] = new SqlParameter("@Pagename", SqlDbType.VarChar, 30) { Value = pagename };
                arParams[2] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30) { Value = racedate };
                arParams[3] = new SqlParameter("@CenterID", SqlDbType.VarChar, 30) { Value = centerid };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardPopupDetail", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[2] = new SqlParameter("@CommentatorPNameID", SqlDbType.Int) { Value = personnameid };
                arParams[3] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                arParams[4] = new SqlParameter("@Commentary", SqlDbType.VarChar, 1000) { Value = commentary };
                arParams[5] = new SqlParameter("@Highlight", SqlDbType.VarChar, 50) { Value = highlight };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ResultRaceCommentary",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[2] = new SqlParameter("@CommentatorPNameID", SqlDbType.Int) { Value = personnameid };
                arParams[3] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                arParams[4] = new SqlParameter("@Commentary", SqlDbType.VarChar, 1000) { Value = commentary };
                arParams[5] = new SqlParameter("@Highlight", SqlDbType.VarChar, 50) { Value = highlight };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ResultRaceIncident",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
        }

        public int CheckDuplicateSectionalTiming(
            int divisionraceid,
            string sectointimingproviderid,
            string sectiondistanceid,
            string sectiontiming)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {

                arParams[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@SectionTimingProviderNamePID", SqlDbType.Int) { Value = sectointimingproviderid };
                arParams[2] = new SqlParameter("@SectionDistanceBreakUpMID", SqlDbType.Int) { Value = sectiondistanceid };
                arParams[3] = new SqlParameter("@SectionTiming", SqlDbType.VarChar, 100) { Value = sectiontiming };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_CheckDuplicateSectionalTiming",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
        }


        public int CheckDuplicateLapTiming(
            int divisionraceid,
            string professionalprovidernameid,
            string HorseNameID,
            string lapdistance,
            string laptiming)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[5];
            try
            {

                arParams[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@ProfessionalLapProviderNameID", SqlDbType.Int) { Value = professionalprovidernameid };
                arParams[2] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = HorseNameID };
                arParams[3] = new SqlParameter("@LapDistance", SqlDbType.VarChar, 100) { Value = lapdistance };
                arParams[4] = new SqlParameter("@LapTiming", SqlDbType.VarChar, 100) { Value = laptiming };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_CheckDuplicateLapTiming",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
        }

        public int AddSectionalTiming(DataTable dt)
        {
            DataTable dtresult;
            int result = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    using (SqlBulkCopy bulkCopy =
                            new SqlBulkCopy(_conn.ConnectionString))
                    {
                        bulkCopy.ColumnMappings.Add("DataEntryDate", "DataEntryDate");
                        bulkCopy.ColumnMappings.Add("DivisionRaceID_FK", "DivisionRaceID_FK");
                        bulkCopy.ColumnMappings.Add("SectionalTimingProviderID", "SectionalTimingProviderID");
                        bulkCopy.ColumnMappings.Add("SectionDistanceBreakUpMID", "SectionDistanceBreakUpMID");
                        bulkCopy.ColumnMappings.Add("SectionalTiming", "SectionalTiming");
                        bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                        bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                        bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                        bulkCopy.ColumnMappings.Add("SectionTimingProviderNamePID", "SectionTimingProviderNamePID");
                        bulkCopy.DestinationTableName = "dbo.Card_Result_SectionalTiming";
                        bulkCopy.WriteToServer(dt);
                    }
                    result = 1;
                }

                _conn.Close();
            }
            catch (Exception ex)
            {
                result = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@DivionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[2] = new SqlParameter("@SectionalTimingProviderID", SqlDbType.Int) { Value = sectionaltimingproviderid };
                arParams[3] = new SqlParameter("@SectionalTiming", SqlDbType.VarChar, 100) { Value = sectionaltiming };
                arParams[4] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[5] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };
                arParams[6] = new SqlParameter("@SectionDistanceBreakupMID", SqlDbType.Int) { Value = distancebreakupmid };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ResultSectionalTiming",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
        }


        public int AddLapTiming(DataTable dt)
        {
            DataTable dtresult;
            int result = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    using (SqlBulkCopy bulkCopy =
                            new SqlBulkCopy(_conn.ConnectionString))
                    {
                        bulkCopy.ColumnMappings.Add("DataEntryDate", "DataEntryDate");
                        bulkCopy.ColumnMappings.Add("DivisionRaceID_FK", "DivisionRaceID_FK");
                        bulkCopy.ColumnMappings.Add("LapTimingProviderID", "LapTimingProviderID");
                        bulkCopy.ColumnMappings.Add("HorseID", "HorseID");
                        bulkCopy.ColumnMappings.Add("LapDistanceBreakUpMID", "LapDistanceBreakUpMID");
                        bulkCopy.ColumnMappings.Add("LapTiming", "LapTiming");
                        bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                        bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                        bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                        bulkCopy.ColumnMappings.Add("LapTimingProviderNamePID", "LapTimingProviderNamePID");
                        bulkCopy.ColumnMappings.Add("HorseNameID", "HorseNameID");
                        bulkCopy.DestinationTableName = "dbo.Card_Result_LapTiming";
                        bulkCopy.WriteToServer(dt);
                    }
                    result = 1;
                }

                _conn.Close();
            }
            catch (Exception ex)
            {
                result = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
        }


        public DataTable GetHorseDetail(
            int divisionraceid,
            int horseno)
        {

            DataTable dt;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@DivionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@HorseNo", SqlDbType.Int) { Value = horseno };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetLaptimingHorseDetail", arParams);
            }
            catch (Exception ex)
            {
                //checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }

        public DataTable GetHorseDetail(
            int divisionraceid,
            int horseno,
            int pageno)
        {

            DataTable dt;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {

                arParams[0] = new SqlParameter("@DivionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@HorseNo", SqlDbType.Int) { Value = horseno };
                arParams[2] = new SqlParameter("@PageNo", SqlDbType.Int) { Value = pageno };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetcardLaptimingHorseDetail", arParams);
            }
            catch (Exception ex)
            {
                //checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@DivionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[2] = new SqlParameter("@LapTimingProviderID", SqlDbType.Int) { Value = Laptimingproviderid };
                arParams[3] = new SqlParameter("@LapTiming", SqlDbType.VarChar, 100) { Value = Laptiming };
                arParams[4] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[5] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };
                arParams[6] = new SqlParameter("@LapDistance", SqlDbType.Int) { Value = masterdistance };
                arParams[7] = new SqlParameter("@HorseNameID", SqlDbType.VarChar,100) { Value = horsenameid };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ResultLapTiming",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[10];
            try
            {

                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@DataEntryDate", SqlDbType.VarChar, 30);
                if (dataentrydate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dataentrydate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }
                arParams[2] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[3] = new SqlParameter("@ReporterID", SqlDbType.Int) { Value = reporterid };
                arParams[4] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[5] = new SqlParameter("@Report", SqlDbType.VarChar, 1000) { Value = report };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };
                arParams[8] = new SqlParameter("@Highlight", SqlDbType.VarChar, 100) { Value = highlight };
                arParams[9] = new SqlParameter("@IncidentID", SqlDbType.VarChar, 100) { Value = incidentid };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_RaceDayReport",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
        }

        public DataSet GetCardIncidentDetail(int divisionid, int centerid, string dataentrydate, string pagename)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {

                arParams[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int);
                arParams[0].Value = divisionid;

                arParams[1] = new SqlParameter("@DataEntryDate", SqlDbType.VarChar, 30);
                if (dataentrydate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dataentrydate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }
                arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[3] = new SqlParameter("@Action", SqlDbType.VarChar, 30) { Value = pagename };

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetCardIncident", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return ds;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@DataEntryDate", SqlDbType.VarChar, 30);
                if (dateentrydate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dateentrydate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[2] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[3] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[4] = new SqlParameter("@IncidentMID", SqlDbType.Int) { Value = incidentmid };
                arParams[5] = new SqlParameter("@IncidentDetail", SqlDbType.VarChar, 1000) { Value = incidentdetail };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_CardIncident",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
        }


        public int AddRaceReview(DataTable dt, int divisionraceid)
        {
            DataTable dtresult;
            int result = 0;
            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@PageName", SqlDbType.VarChar, 30) { Value = "RaceReview" };
                checkRecord =
                  Convert.ToInt32(
                      SqlHelper.ExecuteScalar(
                          _conn,
                          CommandType.StoredProcedure,
                          "sp_CardsAddStatusCheck",
                          arParams));
                if (checkRecord == 0)
                {
                    // _conn.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                    {
                        using (SqlBulkCopy bulkCopy =
                                new SqlBulkCopy(_conn.ConnectionString))
                        {
                            bulkCopy.ColumnMappings.Add("DataEntryDate", "DataEntryDate");
                            bulkCopy.ColumnMappings.Add("DivisionRaceID_Fk", "DivisionRaceID_Fk");
                            bulkCopy.ColumnMappings.Add("HorseID", "HorseID");
                            bulkCopy.ColumnMappings.Add("PaddockConditionMID", "PaddockConditionMID");
                            bulkCopy.ColumnMappings.Add("EarlyGategoer", "EarlyGategoer");
                            bulkCopy.ColumnMappings.Add("FirstSectionalPosition", "FirstSectionalPosition");
                            bulkCopy.ColumnMappings.Add("BandPosition", "BandPosition");
                            bulkCopy.ColumnMappings.Add("LastSectionalPosition", "LastSectionalPosition");
                            bulkCopy.ColumnMappings.Add("RunQualityMID", "RunQualityMID");
                            bulkCopy.ColumnMappings.Add("TrainerOnBoardEffortMID", "TrainerOnBoardEffortMID");
                            bulkCopy.ColumnMappings.Add("JockeyOnBoardEffortMID", "JockeyOnBoardEffortMID");
                            bulkCopy.ColumnMappings.Add("MateTypeMID", "MateTypeMID");
                            bulkCopy.ColumnMappings.Add("InBettingOrderMID", "InBettingOrderMID");
                            bulkCopy.ColumnMappings.Add("PerformanceUpdated", "PerformanceUpdated");
                            bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                            bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                            bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                            bulkCopy.DestinationTableName = "dbo.Card_RaceReview";
                            bulkCopy.WriteToServer(dt);
                        }
                        result = 1;
                    }

                    _conn.Close();
                }
                else
                {
                    result = 4;
                }
            }
            catch (Exception ex)
            {
                result = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
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
            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[13];
            try
            {
                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@PeddockConditionID", SqlDbType.Int) { Value = peddockconditionid };
                arParams[2] = new SqlParameter("@EarlyGategoer", SqlDbType.Bit) { Value = earlygategoer };
                arParams[3] = new SqlParameter("@FirstSectionalPosition", SqlDbType.VarChar, 5) { Value = firstsectionalpostion };
                arParams[4] = new SqlParameter("@BandPosition", SqlDbType.VarChar, 5) { Value = bandposition };
                arParams[5] = new SqlParameter("@LastSectionalPosition", SqlDbType.VarChar, 5) { Value = lastsectionalpostion };
                arParams[6] = new SqlParameter("@RunQualityMID", SqlDbType.Int) { Value = runqualityid };
                arParams[7] = new SqlParameter("@TrainerOnBoardEffortMID", SqlDbType.Int) { Value = traineronboardefforid };
                arParams[8] = new SqlParameter("@JockeyOnBoardEffortMID", SqlDbType.Int) { Value = jockeyonboardeffortid };
                arParams[9] = new SqlParameter("@MateTypeMID", SqlDbType.Int) { Value = matetypeid };
                arParams[10] = new SqlParameter("@InBettingOrderMID", SqlDbType.Int) { Value = bettingorderid };
                arParams[11] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[12] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_CardRaceReview",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
        }


        public int AddCardOdds(DataTable dt, int divisionraceid)
        {
            DataTable dtresult;
            int result = 0;
            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@PageName", SqlDbType.VarChar, 30) { Value = "CardOdds" };
                checkRecord =
                  Convert.ToInt32(
                      SqlHelper.ExecuteScalar(
                          _conn,
                          CommandType.StoredProcedure,
                          "sp_CardsAddStatusCheck",
                          arParams));
                if (checkRecord == 0)
                {
                    // _conn.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                    {
                        using (SqlBulkCopy bulkCopy =
                                new SqlBulkCopy(_conn.ConnectionString))
                        {
                            bulkCopy.ColumnMappings.Add("DivisionRaceID_Fk", "DivisionRaceID_Fk");
                            bulkCopy.ColumnMappings.Add("DayRaceNo", "DayRaceNo");
                            bulkCopy.ColumnMappings.Add("HorseNo", "HorseNo");
                            bulkCopy.ColumnMappings.Add("HorseID", "HorseID");
                            bulkCopy.ColumnMappings.Add("NOW", "NOW");
                            bulkCopy.ColumnMappings.Add("MOW", "MOW");
                            bulkCopy.ColumnMappings.Add("LOOW", "LOOW");
                            bulkCopy.ColumnMappings.Add("OOW", "OOW");
                            bulkCopy.ColumnMappings.Add("OOP", "OOP");
                            bulkCopy.ColumnMappings.Add("MDW", "MDW");
                            bulkCopy.ColumnMappings.Add("MOP", "MOP");
                            bulkCopy.ColumnMappings.Add("COW", "COW");
                            bulkCopy.ColumnMappings.Add("COP", "COP");
                            bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                            bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                            bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                            bulkCopy.DestinationTableName = "dbo.Card_Odds";
                            bulkCopy.WriteToServer(dt);
                        }
                        result = 1;
                    }

                    _conn.Close();
                }
                else
                {
                    result = 4;
                }
            }
            catch (Exception ex)
            {
                result = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
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
            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[12];
            try
            {
                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@Now", SqlDbType.Decimal) { Value = now };
                arParams[2] = new SqlParameter("@Mow", SqlDbType.Decimal) { Value = mow };
                arParams[3] = new SqlParameter("@Loow", SqlDbType.Decimal) { Value = loow };
                arParams[4] = new SqlParameter("@Oow", SqlDbType.Decimal) { Value = oow };
                arParams[5] = new SqlParameter("@Oop", SqlDbType.Decimal) { Value = oop };
                arParams[6] = new SqlParameter("@Mdw", SqlDbType.Decimal) { Value = mdw };
                arParams[7] = new SqlParameter("@Mop", SqlDbType.Decimal) { Value = mop };
                arParams[8] = new SqlParameter("@Cow", SqlDbType.Decimal) { Value = cow };
                arParams[9] = new SqlParameter("@Cop", SqlDbType.Decimal) { Value = cop };
                arParams[10] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[11] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_CardOdds",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
        }


        public int AddRaceSituation(DataTable dt, int divisionraceid)
        {
            DataTable dtresult;
            int result = 0;
            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@PageName", SqlDbType.VarChar, 30) { Value = "RaceSituation" };
                checkRecord =
                  Convert.ToInt32(
                      SqlHelper.ExecuteScalar(
                          _conn,
                          CommandType.StoredProcedure,
                          "sp_CardsAddStatusCheck",
                          arParams));
                if (checkRecord == 0)
                {
                    // _conn.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                    {
                        using (SqlBulkCopy bulkCopy =
                                new SqlBulkCopy(_conn.ConnectionString))
                        {
                            bulkCopy.ColumnMappings.Add("DivisionRaceID_Fk", "DivisionRaceID_Fk");
                            bulkCopy.ColumnMappings.Add("RealRaceTime", "RealRaceTime");
                            bulkCopy.ColumnMappings.Add("FalseRails", "FalseRails");
                            bulkCopy.ColumnMappings.Add("FRMeasurement", "FRMeasurement");
                            bulkCopy.ColumnMappings.Add("PenetrometerReading", "PenetrometerReading");
                            bulkCopy.ColumnMappings.Add("GoingMID", "GoingMID");
                            bulkCopy.ColumnMappings.Add("WeatherMID", "WeatherMID");
                            bulkCopy.ColumnMappings.Add("Temperature", "Temperature");
                            bulkCopy.ColumnMappings.Add("RainInMorning", "RainInMorning");
                            bulkCopy.ColumnMappings.Add("RainBeforeRace", "RainBeforeRace");
                            bulkCopy.ColumnMappings.Add("RainDuringRace", "RainDuringRace");
                            bulkCopy.ColumnMappings.Add("OtherFactor", "OtherFactor");
                            bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                            bulkCopy.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                            bulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                            bulkCopy.DestinationTableName = "dbo.Card_RaceDaySituation";
                            bulkCopy.WriteToServer(dt);
                        }
                        result = 1;
                    }

                    _conn.Close();
                }
                else
                {
                    result = 4;
                }
            }
            catch (Exception ex)
            {
                result = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
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
            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[12];
            try
            {
                arParams[0] = new SqlParameter("@GlobalID", SqlDbType.Int) { Value = globalid };
                arParams[1] = new SqlParameter("@RealRaceTime", SqlDbType.VarChar, 100) { Value = realracetime };
                arParams[2] = new SqlParameter("@PenetrometerReading", SqlDbType.Decimal) { Value = penetrometerreading };
                arParams[3] = new SqlParameter("@GoingID", SqlDbType.Int) { Value = goingid };
                arParams[4] = new SqlParameter("@WeatherID", SqlDbType.Int) { Value = weatherid };
                arParams[5] = new SqlParameter("@Temperature", SqlDbType.VarChar, 500) { Value = temprature };
                arParams[6] = new SqlParameter("@RainInMorning", SqlDbType.Bit) { Value = raininmorning };
                arParams[7] = new SqlParameter("@RainbeforeRace", SqlDbType.Bit) { Value = rainbeforerace };
                arParams[8] = new SqlParameter("@RainDuringRace", SqlDbType.Bit) { Value = rainduringrace };
                arParams[9] = new SqlParameter("@OtherFactor", SqlDbType.VarChar, 500) { Value = otherfactor };
                arParams[10] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[11] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_RaceCardSituation",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
        }


        public DataTable HotLine(int hotlinerid, string racedate, int centerid, int Professionalnameid, int horseid, string hotline, int userid, string action)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[8];
                arParams[0] = new SqlParameter("@HotlinerID", SqlDbType.Int) { Value = hotlinerid };
                arParams[1] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[3] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int) { Value = Professionalnameid };
                arParams[4] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[5] = new SqlParameter("@Hotliner", SqlDbType.VarChar, 1000) { Value = hotline };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@Action", SqlDbType.VarChar, 50) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_CardHotliner", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        public DataTable Tip(int hotlinerid, string racedate, int centerid, int Professionalnameid, int horseid, int tiptypeid, int userid, string action)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[8];
                arParams[0] = new SqlParameter("@HotlinerID", SqlDbType.Int) { Value = hotlinerid };
                arParams[1] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[3] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int) { Value = Professionalnameid };
                arParams[4] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[5] = new SqlParameter("@Hotliner", SqlDbType.Int) { Value = tiptypeid };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@Action", SqlDbType.VarChar, 50) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_CardTip", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        public int RaceCardSelection(
           VKATalkClassLayer.CardSelection cs,
           string racedate,
            string action)
        {
            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[23];
            try
            {
                arParams[0] = new SqlParameter("@SelectionCID", SqlDbType.Int) { Value = cs.SelectionCID };
                arParams[1] = new SqlParameter("@SelectionPID", SqlDbType.Int) { Value = cs.SelectionPID };
                arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = cs.CenterID };
                arParams[3] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@DRNo1", SqlDbType.VarChar,500) { Value = cs.DRNo1 };
                arParams[5] = new SqlParameter("@DRNo2", SqlDbType.VarChar, 500) { Value = cs.DRNo2 };
                arParams[6] = new SqlParameter("@DRNo3", SqlDbType.VarChar, 500) { Value = cs.DRNo3 };
                arParams[7] = new SqlParameter("@DRNo4", SqlDbType.VarChar, 500) { Value = cs.DRNo4 };
                arParams[8] = new SqlParameter("@DRNo5", SqlDbType.VarChar, 500) { Value = cs.DRNo5 };
                arParams[9] = new SqlParameter("@DRNo6", SqlDbType.VarChar, 500) { Value = cs.DRNo6 };
                arParams[10] = new SqlParameter("@DRNo7", SqlDbType.VarChar, 500) { Value = cs.DRNo7 };
                arParams[11] = new SqlParameter("@DRNo8", SqlDbType.VarChar, 500) { Value = cs.DRNo8 };
                arParams[12] = new SqlParameter("@DRNo9", SqlDbType.VarChar, 500) { Value = cs.DRNo9 };
                arParams[13] = new SqlParameter("@DRNo10", SqlDbType.VarChar, 500) { Value = cs.DRNo10 };
                arParams[14] = new SqlParameter("@DRNo11", SqlDbType.VarChar, 500) { Value = cs.DRNo11 };
                arParams[15] = new SqlParameter("@DayBest", SqlDbType.VarChar, 500) { Value = cs.DayBest };
                arParams[16] = new SqlParameter("@GoodDouble", SqlDbType.VarChar, 500) { Value = cs.GoodDouble };
                arParams[17] = new SqlParameter("@GoodPlace", SqlDbType.VarChar, 500) { Value = cs.GoodPlace };
                arParams[18] = new SqlParameter("@Rolling", SqlDbType.VarChar, 500) { Value = cs.Rolling };
                arParams[19] = new SqlParameter("@Upsetter", SqlDbType.VarChar, 500) { Value = cs.Upsetter};
                arParams[20] = new SqlParameter("@EatableBet", SqlDbType.VarChar, 500) { Value = cs.EatableBet};
                arParams[21] = new SqlParameter("@UserID", SqlDbType.Int) { Value = cs.UserId };
                arParams[22] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_CardSelection",
                            arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 0;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
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
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {
                arParams[0] = new SqlParameter("@MockRaceID", SqlDbType.Int) { Value = mockraceentryid };
                arParams[1] = new SqlParameter("@DateEntry", SqlDbType.VarChar, 30);
                if (dataentrydate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dataentrydate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }

                arParams[2] = new SqlParameter("@MockRaceDate", SqlDbType.VarChar, 30);
                if (mockracedate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = mockracedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[4] = new SqlParameter("@SourceNameID", SqlDbType.Int) { Value = sourcenameid };
                arParams[5] = new SqlParameter("@MockRaceNo", SqlDbType.Int) { Value = mockraceno };
                arParams[6] = new SqlParameter("@DistanceID", SqlDbType.Int) { Value = distanceid};
                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[8] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_MockRaceEntry", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
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
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[13];
            try
            {
                arParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = participatigHorseid };
                arParams[1] = new SqlParameter("@MockRaceID", SqlDbType.Int) { Value = mockraceid_fk };
                arParams[2] = new SqlParameter("@DateEntry", SqlDbType.VarChar, 30);
                if (dataentrydate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dataentrydate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@Placing", SqlDbType.Int) { Value = placing };
                if (drawno == 0.0)
                {
                    arParams[4] = new SqlParameter("@DrawNo", SqlDbType.Decimal) { Value = null };
                }
                else
                {
                    arParams[4] = new SqlParameter("@DrawNo", SqlDbType.Decimal) { Value = drawno };
                }
                
                arParams[5] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                arParams[6] = new SqlParameter("@RiderPNameID", SqlDbType.Int) { Value = ridernameid };
                if (carriedweight == 0)
                {
                    arParams[7] = new SqlParameter("@MRCarriedWeight", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[7] = new SqlParameter("@MRCarriedWeight", SqlDbType.Int) { Value = carriedweight };
                }
                arParams[8] = new SqlParameter("@FinishTime", SqlDbType.VarChar,50) { Value = finishtime };
                arParams[9] = new SqlParameter("@VardictMarginID", SqlDbType.Int) { Value = verdictmarginid };
                arParams[10] = new SqlParameter("@Comments", SqlDbType.VarChar,1000) { Value = comments };
                arParams[11] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[12] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_Card_MockRaceHorseEntry", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
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
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {
                arParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = MRDistanceBreakUpCID };
                arParams[1] = new SqlParameter("@MockRaceID", SqlDbType.Int) { Value = mockraceid_fk };
                if (sourcepnameid == 0)
                {
                    arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = sourcepnameid };
                }
                
                arParams[3] = new SqlParameter("@DistanceBreakupID", SqlDbType.Int) { Value = distancebreakupmid };
                arParams[4] = new SqlParameter("@TimeTaken", SqlDbType.VarChar, 50) { Value = timetaken };
                arParams[5] = new SqlParameter("@Comments", SqlDbType.VarChar, 1000) { Value = comments };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_MockRace_DistanceBreakUp", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
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
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {
                arParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = MREquipmentCID };
                arParams[1] = new SqlParameter("@MockRaceID", SqlDbType.Int) { Value = mockraceid_fk };
                if (sourcepnameid == 0)
                {
                    arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = sourcepnameid };
                }
                if (horsenameid == 0)
                {
                    arParams[3] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[3] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                }
                
                arParams[4] = new SqlParameter("@EquipmentID", SqlDbType.Int) { Value = equipmentid };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_MockRace_Equipment", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }



        public DataTable MockRaceIndividual(
           int MRIHCommentCID,
           int mockraceid_fk,
           int sourcepnameid,
           int horsenameid,
           string comments,
           int userid,
           string action)
        {
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {
                arParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = MRIHCommentCID };
                arParams[1] = new SqlParameter("@MockRaceID", SqlDbType.Int) { Value = mockraceid_fk };
                if (sourcepnameid == 0)
                {
                    arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = sourcepnameid };
                }
                if (horsenameid == 0)
                {
                    arParams[3] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[3] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                }
                
                
                arParams[4] = new SqlParameter("@Comments", SqlDbType.VarChar, 1000) { Value = comments};
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_MockRace_Individual", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
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
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[10];
            try
            {
                arParams[0] = new SqlParameter("@MockRaceID", SqlDbType.Int) { Value = mockraceentryid };
                arParams[1] = new SqlParameter("@DateEntry", SqlDbType.VarChar, 30);
                if (dataentrydate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dataentrydate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }
                arParams[2] = new SqlParameter("@SourceNameID", SqlDbType.Int) { Value = sourcenameid };
                arParams[3] = new SqlParameter("@MockRaceDate", SqlDbType.VarChar, 30);
                if (mockracedate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = mockracedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[4] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[5] = new SqlParameter("@LotID", SqlDbType.Int) { Value = lotid };
                arParams[6] = new SqlParameter("@DistanceID", SqlDbType.Int) { Value = distanceid };
                arParams[7] = new SqlParameter("@TrackID", SqlDbType.Int) { Value = trackid };
                arParams[8] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[9] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GatePracticeEntry", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
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
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[13];
            try
            {
                arParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = participatigHorseid };
                arParams[1] = new SqlParameter("@MockRaceID", SqlDbType.Int) { Value = mockraceid_fk };
                arParams[2] = new SqlParameter("@DateEntry", SqlDbType.VarChar, 30);
                if (dataentrydate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dataentrydate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@Placing", SqlDbType.Int) { Value = placing };
                arParams[4] = new SqlParameter("@DrawNo", SqlDbType.Decimal) { Value = null };
                arParams[5] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                arParams[6] = new SqlParameter("@RiderPNameID", SqlDbType.Int) { Value = ridernameid };
                arParams[7] = new SqlParameter("@MRCarriedWeight", SqlDbType.Int) { Value = null };
                arParams[8] = new SqlParameter("@FinishTime", SqlDbType.VarChar, 50) { Value = finishtime };
                arParams[9] = new SqlParameter("@VardictMarginID", SqlDbType.Int) { Value = verdictmarginid };
                arParams[10] = new SqlParameter("@Comments", SqlDbType.VarChar, 1000) { Value = comments };
                arParams[11] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[12] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_Card_GatePracticeHorseEntry", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
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
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {
                arParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = MRDistanceBreakUpCID };
                arParams[1] = new SqlParameter("@MockRaceID", SqlDbType.Int) { Value = mockraceid_fk };
                if (sourcepnameid == 0)
                {
                    arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = sourcepnameid };
                }

                arParams[3] = new SqlParameter("@DistanceBreakupID", SqlDbType.Int) { Value = distancebreakupmid };
                arParams[4] = new SqlParameter("@TimeTaken", SqlDbType.VarChar, 50) { Value = timetaken };
                arParams[5] = new SqlParameter("@Comments", SqlDbType.VarChar, 1000) { Value = comments };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GatePractice_DistanceBreakUp", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }

        public DataTable GetPracticeIndividual(
           int MRIHCommentCID,
           int mockraceid_fk,
           int sourcepnameid,
           int horsenameid,
           string comments,
           int userid,
           string action)
        {
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {
                arParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = MRIHCommentCID };
                arParams[1] = new SqlParameter("@MockRaceID", SqlDbType.Int) { Value = mockraceid_fk };
                if (sourcepnameid == 0)
                {
                    arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = sourcepnameid };
                }
                if (horsenameid == 0)
                {
                    arParams[3] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[3] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                }


                arParams[4] = new SqlParameter("@Comments", SqlDbType.VarChar, 1000) { Value = comments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GatePractice_Individual", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
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
            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[28];
            try
            {
                arParams[0] = new SqlParameter("@TrackBunchCID", SqlDbType.Int) { Value = TrackBunchCID };
                arParams[1] = new SqlParameter("@DataEntryDate", SqlDbType.VarChar, 30);
                if (DataEntryDate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = DataEntryDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = sourcepnameid };
                arParams[3] = new SqlParameter("@CenterMID", SqlDbType.Int) { Value = centerid };
                arParams[4] = new SqlParameter("@TrackDate", SqlDbType.VarChar, 30);
                if (trackdate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = trackdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[5] = new SqlParameter("@DistanceMID", SqlDbType.Int) { Value = distanceid };
                arParams[6] = new SqlParameter("@TrackMID", SqlDbType.Int) { Value = trackid };
                if (horsenameid == 0)
                {
                    arParams[7] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[7] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                }
                if (ridernameid == 0)
                {
                    arParams[8] = new SqlParameter("@RiderPNameID", SqlDbType.Int) { Value = null };
                }
                else
                {
                    arParams[8] = new SqlParameter("@RiderPNameID", SqlDbType.Int) { Value = ridernameid };
                }
                
                arParams[9] = new SqlParameter("@DistanceBreakUpMID", SqlDbType.Int) { Value = distancebreakupid };
                arParams[10] = new SqlParameter("@TimeTaken", SqlDbType.VarChar,50) { Value = timetaken };
                arParams[11] = new SqlParameter("@PaceMID", SqlDbType.Int) { Value = paceid };
                arParams[12] = new SqlParameter("@1stEquipmentMID", SqlDbType.Int) { Value = firstequimentid };
                arParams[13] = new SqlParameter("@2ndEquipmentMID", SqlDbType.Int) { Value = secondequipmentid };
                arParams[14] = new SqlParameter("@VerdictMarginMID", SqlDbType.Int) { Value = verdictmarginid };
                arParams[15] = new SqlParameter("@CommonComment", SqlDbType.VarChar, 1000) { Value = comments };
                arParams[16] = new SqlParameter("@IndividualHorseComment", SqlDbType.VarChar, 1000) { Value = individualcomments };
                arParams[17] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[18] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };
                arParams[19] = new SqlParameter("@WorkoutType", SqlDbType.VarChar, 100) { Value = workouttype };
                arParams[20] = new SqlParameter("@DrawNo", SqlDbType.VarChar, 100) { Value = drawno };
                arParams[21] = new SqlParameter("@CarriedWeight", SqlDbType.VarChar, 100) { Value = carriedweight };

                arParams[22] = new SqlParameter("@DBC", SqlDbType.Int) { Value = dbc };
                arParams[23] = new SqlParameter("@IHCC", SqlDbType.Int) { Value = ihcc };
                arParams[24] = new SqlParameter("@WorkQuality", SqlDbType.Int) { Value = workquality };
                arParams[25] = new SqlParameter("@WR", SqlDbType.Int) { Value = wr };
                arParams[26] = new SqlParameter("@WIM", SqlDbType.Int) { Value = wim };
                arParams[27] = new SqlParameter("@ISSHOW", SqlDbType.Int) { Value = isshow };


                checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_CardTrack", arParams));
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;
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
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[19];
            try
            {
                arParams[0] = new SqlParameter("@TrackBunchCID", SqlDbType.Int) { Value = TrackBunchCID };
                arParams[1] = new SqlParameter("@DataEntryDate", SqlDbType.VarChar, 30);
                if (DataEntryDate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = DataEntryDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[2] = new SqlParameter("@SourcePNameID", SqlDbType.Int) { Value = sourcepnameid };
                arParams[3] = new SqlParameter("@CenterMID", SqlDbType.Int) { Value = centerid };
                arParams[4] = new SqlParameter("@TrackDate", SqlDbType.VarChar, 30);
                if (trackdate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = trackdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[5] = new SqlParameter("@DistanceMID", SqlDbType.Int) { Value = distanceid };
                arParams[6] = new SqlParameter("@TrackMID", SqlDbType.Int) { Value = trackid };
                arParams[7] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                arParams[8] = new SqlParameter("@RiderPNameID", SqlDbType.Int) { Value = ridernameid };
                arParams[9] = new SqlParameter("@DistanceBreakUpMID", SqlDbType.Int) { Value = distancebreakupid };
                arParams[10] = new SqlParameter("@TimeTaken", SqlDbType.VarChar, 50) { Value = timetaken };
                arParams[11] = new SqlParameter("@PaceMID", SqlDbType.Int) { Value = paceid };
                arParams[12] = new SqlParameter("@1stEquipmentMID", SqlDbType.Int) { Value = firstequimentid };
                arParams[13] = new SqlParameter("@2ndEquipmentMID", SqlDbType.Int) { Value = secondequipmentid };
                arParams[14] = new SqlParameter("@VerdictMarginMID", SqlDbType.Int) { Value = verdictmarginid };
                arParams[15] = new SqlParameter("@CommonComment", SqlDbType.VarChar, 1000) { Value = comments };
                arParams[16] = new SqlParameter("@IndividualHorseComment", SqlDbType.VarChar, 1000) { Value = individualcomments };
                arParams[17] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[18] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = action };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_CardTrack", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;
        }


		public void HandicapUpdate(string generalracieid, string generalracenameid, string weightdetail, string value)
		{
			try
			{
				SqlParameter[] arParams = new SqlParameter[4];

				arParams[0] = new SqlParameter("@GeneralRaceID", SqlDbType.VarChar, 50) { Value = generalracieid };
				arParams[1] = new SqlParameter("@GeneralRaceNameID", SqlDbType.VarChar, 50) { Value = generalracenameid };
				arParams[2] = new SqlParameter("@Weight", SqlDbType.VarChar, 50) { Value = weightdetail };
				arParams[3] = new SqlParameter("@WeightValue", SqlDbType.VarChar, 50) { Value = value };
				SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_HandicapWeight", arParams);
			}
			catch (Exception ex)
			{
				ErrorHandling.CheckEachSteps(ex.StackTrace);
				ErrorHandling.SendErrorToText(ex);
				throw;
			}

		}


		public DataTable GetAcceptanceWeight(int divisionraceid, int handicapid)
		{
			var dt = new DataTable();
			try
			{
				SqlParameter[] arParams = new SqlParameter[2];

				arParams[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid};
				arParams[1] = new SqlParameter("@HandicapID", SqlDbType.Int) { Value = handicapid };
				dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetAcceptanceUpdateWeight", arParams);
			}
			catch (Exception ex)
			{
				ErrorHandling.CheckEachSteps(ex.StackTrace);
				ErrorHandling.SendErrorToText(ex);
				throw;
			}

			return dt;
		}


        public DataTable GetCardDJA(int professinalnameid, string racedate, int centerid, int divisionraceid)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[4];

                arParams[0] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int) { Value = professinalnameid };
                arParams[1] = new SqlParameter("@RaceDate", SqlDbType.DateTime);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enterDate =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enterDate.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[2] = new SqlParameter("@CENTERID", SqlDbType.Int) { Value = centerid };
                arParams[3] = new SqlParameter("@DIVISIONRACEID", SqlDbType.Int) { Value = divisionraceid };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetDeclarationDJA", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }

        public DataTable GetCardResultInformation(int horsenameid, int divisionraceid)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[2];
                arParams[0] = new SqlParameter("@DIVISIONRACEID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardResultInformation", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }

        public DataTable GetCardResultInformation1(int jockenameid, int divisionraceid)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[2];
                arParams[0] = new SqlParameter("@DIVISIONRACEID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@JockeyNameID", SqlDbType.Int) { Value = jockenameid };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetCardResultInformation", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }


        public DataTable GetAcceptanceOldRecordStatus(int generalracenameid, string generalracedate, int centerid, string season, string year)
		{
			var dt = new DataTable();
			try
			{
				SqlParameter[] arParams = new SqlParameter[5];

				arParams[0] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalracenameid };
				arParams[1] = new SqlParameter("@GeneralRaceDate", SqlDbType.VarChar, 30);
				if (generalracedate.Equals("__-__-____"))
				{
					arParams[1].Value = DBNull.Value;
				}
				else
				{
					string[] dateString = generalracedate.Split('-');
					DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
					arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
				}
				arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
				arParams[3] = new SqlParameter("@Seasonname", SqlDbType.VarChar,50) { Value = season };
				arParams[4] = new SqlParameter("@Year", SqlDbType.VarChar, 50) { Value = year };

				dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_AcceptanceOldRecord", arParams);
			}
			catch (Exception ex)
			{
				ErrorHandling.CheckEachSteps(ex.StackTrace);
				ErrorHandling.SendErrorToText(ex);
				throw;
			}

			return dt;
		}


		public DataTable GetDeclareOldRecordStatus(int generalracenameid, string generalracedate, int centerid, 
                                                        string season, string year, string pagename, int divisionraceid,
                                                        int horsenameid)
		{
			var dt = new DataTable();
			try
			{
				SqlParameter[] arParams = new SqlParameter[8];

				arParams[0] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalracenameid };
				arParams[1] = new SqlParameter("@GeneralRaceDate", SqlDbType.VarChar, 30);
				if (generalracedate.Equals("__-__-____"))
				{
					arParams[1].Value = DBNull.Value;
				}
				else
				{
					string[] dateString = generalracedate.Split('-');
					DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
					arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
				}
				arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
				arParams[3] = new SqlParameter("@Seasonname", SqlDbType.VarChar, 50) { Value = season };
				arParams[4] = new SqlParameter("@Year", SqlDbType.VarChar, 50) { Value = year };
                arParams[5] = new SqlParameter("@PageName", SqlDbType.VarChar, 50) { Value = pagename };
                arParams[6] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[7] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_CardDeclarationOldRecord", arParams);
			}
			catch (Exception ex)
			{
				ErrorHandling.CheckEachSteps(ex.StackTrace);
				ErrorHandling.SendErrorToText(ex);
				throw;
			}

			return dt;
		}


        public void InsertDeclarationBitEquipemnt(int divisionraceid, int horseid,
                                                        int horsenameid,string bit, string equipment,string pagename)
        {
            var dt = new DataTable();
            try
            {
                SqlParameter[] arParams = new SqlParameter[6];

                arParams[0] = new SqlParameter("@DivisionRaceID", SqlDbType.Int) { Value = divisionraceid };
                arParams[1] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[2] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                arParams[3] = new SqlParameter("@Bit", SqlDbType.VarChar,500) { Value = bit };
                arParams[4] = new SqlParameter("@Equipment", SqlDbType.VarChar,500) { Value = equipment };
                arParams[5] = new SqlParameter("@PageName", SqlDbType.VarChar,500) { Value = pagename };

                SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_DeclarationBitEquipment", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

        }


        public int GetProfessionalID(int professionalnameid)
		{

			int checkRecord;

			SqlParameter[] arParams = new SqlParameter[1];
			try
			{
				arParams[0] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int) { Value = professionalnameid };
				
				checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_GetCardProfessionalID", arParams));
			}
			catch (Exception ex)
			{
				checkRecord = 2;
				ErrorHandling.CheckEachSteps(ex.StackTrace);
				ErrorHandling.SendErrorToText(ex);
				throw;
			}
			finally
			{
				if (_conn.State == ConnectionState.Open)
				{
					_conn.Close();
				}
			}
			return checkRecord;

		}


        public DataTable GetHorseAchivementData(int horseid, int horsenameid)
        {

            int checkRecord;
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                arParams[1] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetHorseAchivement", arParams);
               // checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_GetHorseAchivement", arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;

        }


        public DataTable GetCardWorkoutSourceName()
        {
            var dt = new DataTable();
            try
            {
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetWorkoutSourceName");
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }



        public DataSet GetCardTrackGridviewData(string racedate, int centerid, string sourcenameid, int horseid)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[4];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int) { Value = sourcenameid };
                arParams[3] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_getCardTrackProfessionalName", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }


        public DataSet GetCardTrackGridviewDataWorkOutType(string racedate, int centerid, string sourcenameid, int horseid)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[4];

                arParams[0] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[0].Value = Convert.ToDateTime(dtformat);
                }
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int) { Value = sourcenameid };
                arParams[3] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };
                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_getCardTrackProfessionalName_WorkoutType", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }

        public int HandicapDuplicateCheck(int generalraceid, int generalracenameid, int horseid)
        {

            int checkRecord;

            SqlParameter[] arParams = new SqlParameter[3];
            try
            {
                arParams[0] = new SqlParameter("@GeneralRaceID", SqlDbType.Int) { Value = generalraceid };
                arParams[1] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int) { Value = generalracenameid };
                arParams[2] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = horseid };

                checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_HandicapDuplicateCheck", arParams));
            }
            catch (Exception ex)
            {
                checkRecord = 2;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return checkRecord;

        }


        public DataTable GetTrackInformation(string entrydate, string trackdate, int horsenameid, int ridernameid, int centerid)
        {
            var dt = new DataTable();
            SqlParameter[] arParams = new SqlParameter[5];
            try
            {
                arParams[0] = new SqlParameter("@EntryDate", SqlDbType.VarChar, 30);
                if (entrydate.Equals("__-__-____"))
                {
                    arParams[0].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = entrydate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[0].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[1] = new SqlParameter("@TrackDate", SqlDbType.VarChar, 30);
                if (trackdate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = trackdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[2] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = horsenameid };
                arParams[3] = new SqlParameter("@RiderPNameID", SqlDbType.Int) { Value = ridernameid };
                arParams[4] = new SqlParameter("@CenterMID", SqlDbType.Int) { Value = centerid };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetTrackInformation", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dt;

        }
    }
}
