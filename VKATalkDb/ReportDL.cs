using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VKATalkDb
{
    public class ReportDL
    {
        private SqlConnection _conn;
        public ReportDL()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);//sdlfjsldfj
        }


        public DataSet GetRaceCardReport(string racedate, int centerid)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[2];

                arParams[0] = new SqlParameter("@DivisionRaceDate", SqlDbType.VarChar, 30);
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

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetRaceCardReport", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }


        public DataSet GetRaceGuide(string racedate, int centerid, int raceid)
        {
            var ds = new DataSet();
            try
            {
                SqlParameter[] arParams = new SqlParameter[3];

                arParams[0] = new SqlParameter("@DivisionRaceDate", SqlDbType.VarChar, 30);
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
                arParams[2] = new SqlParameter("@DayRaceNo", SqlDbType.Int) { Value = raceid };

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetRaceGuideReport", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return ds;
        }


        public DataSet GetHorseFamily(string horsenameid, string racedate)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@HorseNameID", SqlDbType.VarChar, 50);
                arParams[0].Value = horsenameid;

                arParams[1] = new SqlParameter("@DivisionRaceDate", SqlDbType.VarChar, 30);
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

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetHorseNameOnDamBasisReport", arParams);
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


        public DataTable GetTotalRaceDetail(string centerid, string racedate)
        {

            DataTable dt = null;
            ;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@CenterID", SqlDbType.VarChar, 50);
                arParams[0].Value = centerid;

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

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetRaceNumber", arParams);
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



        public DataSet GetHorseFamilyMoreDetail(string horseid, string racedate, int raceid)
        {

            DataSet ds = null;
            ;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.VarChar, 50);
                arParams[0].Value = horseid;

                arParams[1] = new SqlParameter("@DivisionDate", SqlDbType.VarChar, 30);
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

                arParams[2] = new SqlParameter("@RaceID", SqlDbType.Int) { Value=raceid};

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_RaceGuide_Veit", arParams);
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


        public DataSet GetReportSwimmingTrckViet(string horseid, string racedate, string racedate2)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.VarChar, 50);
                arParams[0].Value = horseid;

                arParams[1] = new SqlParameter("@DivisionDate1", SqlDbType.VarChar, 30);
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

                arParams[2] = new SqlParameter("@DivisionDate2", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate2.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_RaceGuide_SwimmingTrackViet", arParams);
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


        public DataSet GetHorsePerformance(int horseid, string racedate)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseid;

                arParams[1] = new SqlParameter("@DivisionDate", SqlDbType.VarChar, 30);
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

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetHorseBunchPerformance", arParams);
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
    }
}
