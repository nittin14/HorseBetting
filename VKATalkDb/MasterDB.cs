using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VKATalkClassLayer;
using System.Collections.Specialized;

namespace VKADB
{
    public class MasterDB
    {
        private SqlConnection _conn;
        public MasterDB()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);//sdlfjsldfj
        }

        public int InsertMasterPagesData(string PageName, string PageFieldName, string PageFieldAlias, int UserID_FK)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[4];

            arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
            arParams[0].Value = PageName;

            arParams[1] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
            arParams[1].Value = PageFieldName;

            arParams[2] = new SqlParameter("@FieldAlias", SqlDbType.VarChar, 100);
            arParams[2].Value = PageFieldAlias;

            arParams[3] = new SqlParameter("@UserID", SqlDbType.Int);
            arParams[3].Value = UserID_FK;

            status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertMasterData", arParams));

            return status;
        }

        public int InsertMasterPagesDataSelectorTipster(string PageName, string SerialNumber, string PageFieldName, string PageFieldAlias, int UserID_FK)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[5];
            //try
            //{

            arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
            arParams[0].Value = PageName;

            arParams[1] = new SqlParameter("@SerialNumber", SqlDbType.VarChar, 50);
            arParams[1].Value = SerialNumber;


            arParams[2] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
            arParams[2].Value = PageFieldName;

            arParams[3] = new SqlParameter("@FieldAlias", SqlDbType.VarChar, 100);
            arParams[3].Value = PageFieldAlias;

            arParams[4] = new SqlParameter("@UserID", SqlDbType.Int);
            arParams[4].Value = UserID_FK;

            status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertMasterData", arParams));
            return status;
        }


        /// <summary>
        /// Get Master Data
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public DataTable GetMasterData(string PageName)
        {
            DataTable dt = null; ;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {

                arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetMasterData", arParams);
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


        /// <summary>
        /// Update the Master Records
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int UpdateMasterPagesDataSelectorTipster(string PageName, string SerialNumber, int RecordID, string PageFieldName, string PageFieldAlias, int UserID_FK, string RecordStatus)
        {
            int checkRecord;

            SqlParameter[] arParams = new SqlParameter[7];
            //try
            //{

            arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
            arParams[0].Value = PageName;

            arParams[1] = new SqlParameter("@SerialNumber", SqlDbType.VarChar, 50);
            arParams[1].Value = SerialNumber;

            arParams[2] = new SqlParameter("@RecordID", SqlDbType.Int);
            arParams[2].Value = RecordID;

            arParams[3] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
            arParams[3].Value = PageFieldName;

            arParams[4] = new SqlParameter("@FieldAlias", SqlDbType.VarChar, 100);
            arParams[4].Value = PageFieldAlias;

            arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
            arParams[5].Value = UserID_FK;

            arParams[6] = new SqlParameter("@RecordStatus", SqlDbType.VarChar, 100);
            arParams[6].Value = RecordStatus;

            checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_UpdateMasterData", arParams));

            return checkRecord;
        }


        /// <summary>
        /// Update the Master Records
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int UpdateMasterPagesData(string PageName, int RecordID, string PageFieldName, string PageFieldAlias, int UserID_FK, string RecordStatus)
        {
            int value = 0;
            SqlParameter[] arParams = new SqlParameter[6];
            arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
            arParams[0].Value = PageName;

            arParams[1] = new SqlParameter("@RecordID", SqlDbType.Int);
            arParams[1].Value = RecordID;

            arParams[2] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
            arParams[2].Value = PageFieldName;

            arParams[3] = new SqlParameter("@FieldAlias", SqlDbType.VarChar, 100);
            arParams[3].Value = PageFieldAlias;

            arParams[4] = new SqlParameter("@UserID", SqlDbType.Int);
            arParams[4].Value = UserID_FK;

            arParams[5] = new SqlParameter("@RecordStatus", SqlDbType.VarChar, 100);
            arParams[5].Value = RecordStatus;

            value = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_UpdateMasterData", arParams));
            return value;
        }


        /// <summary>
        /// Update Master Age Record
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="RecordID"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="UserID_FK"></param>
        /// <param name="RecordStatus"></param>
        /// <returns></returns>
        public bool UpdateMasterPagesDataClass(string PageName, int RecordID, string ClassName, string ClassAlias, string EquivalentToClass, string handicapRatingRange, int MaxHandicapRating, int MinHandicapRating, int UserID_FK, string RecordStatus)
        {
            bool checkRecord;
            SqlParameter[] arParams = new SqlParameter[10];
            try
            {
                arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                arParams[1] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[1].Value = RecordID;

                arParams[2] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
                arParams[2].Value = ClassName;

                arParams[3] = new SqlParameter("@FieldAlias", SqlDbType.VarChar, 50);
                arParams[3].Value = ClassAlias;

                arParams[4] = new SqlParameter("@Field1", SqlDbType.VarChar, 100);
                arParams[4].Value = EquivalentToClass;

                arParams[5] = new SqlParameter("@Field2", SqlDbType.VarChar, 100);
                arParams[5].Value = handicapRatingRange;

                arParams[6] = new SqlParameter("@FieldName1", SqlDbType.Int);
                arParams[6].Value = Convert.ToInt32(MaxHandicapRating);

                arParams[7] = new SqlParameter("@FieldName2", SqlDbType.Int);
                arParams[7].Value = Convert.ToInt32(MinHandicapRating);

                arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[8].Value = UserID_FK;

                arParams[9] = new SqlParameter("@RecordStatus", SqlDbType.VarChar, 100);
                arParams[9].Value = RecordStatus;
                int value = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_UpdateMasterDataCondition", arParams));
                checkRecord = true;
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

        /// <summary>
        /// Insert Incident Data in Database
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="IncidentName"></param>
        /// <param name="IncidentInShort"></param>
        /// <param name="IncidentShortAlias"></param>
        /// <param name="impact"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int InsertMasterPagesDataIncident(string PageName, string IncidentName, string IncidentInShort, string IncidentShortAlias, string impact, int UserID_FK, int recordid)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[6];
            try
            {

                arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                arParams[1] = new SqlParameter("@Incident", SqlDbType.VarChar, 100);
                arParams[1].Value = IncidentName;

                arParams[2] = new SqlParameter("@Alias", SqlDbType.VarChar, 100);
                arParams[2].Value = IncidentInShort;

                arParams[3] = new SqlParameter("@Impact", SqlDbType.VarChar, 500);
                arParams[3].Value = IncidentShortAlias;

                arParams[4] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[4].Value = UserID_FK;

                arParams[5] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[5].Value = recordid;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertMasterDataIncident", arParams));

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
            return status;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionname"></param>
        /// <param name="centerid"></param>
        /// <param name="fromyearid"></param>
        /// <param name="tillyearid"></param>
        /// <param name="fromseasonid"></param>
        /// <param name="tillseasonid"></param>
        /// <param name="jockeyage"></param>
        /// <param name="totalwinfrom"></param>
        /// <param name="totalwintill"></param>
        /// <param name="allowance"></param>
        /// <param name="UserID_FK"></param>
        /// <param name="allowanceid"></param>
        /// <returns></returns>
        public int InsertMasterAllowance(string actionname, int centerid, int fromyearid, int tillyearid, int fromseasonid, int tillseasonid, string jockeyage, string totalwinfrom, string totalwintill, string allowance, int UserID_FK, int allowanceid)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[12];
            try
            {
                arParams[0] = new SqlParameter("@ActionName", SqlDbType.VarChar, 100) { Value = actionname };
                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@FromSeasonID", SqlDbType.Int) { Value = fromseasonid };
                arParams[5] = new SqlParameter("@TillSeasonID", SqlDbType.Int) { Value = tillseasonid };
                arParams[6] = new SqlParameter("@JockeyAge", SqlDbType.VarChar, 500) { Value = jockeyage };
                arParams[7] = new SqlParameter("@TotalWinFrom", SqlDbType.VarChar, 500) { Value = totalwinfrom };
                arParams[8] = new SqlParameter("@TotalWinTill", SqlDbType.VarChar, 500) { Value = totalwintill };
                arParams[9] = new SqlParameter("@Allowance", SqlDbType.VarChar, 500) { Value = allowance };
                arParams[10] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID_FK };
                arParams[11] = new SqlParameter("@AllowanceID", SqlDbType.Int) { Value = allowanceid };

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_MasterDataAllowance", arParams));
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
            return status;
        }


        /// <summary>
        /// Insert Racing website details Data in database
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int InsertMasterPagesDataAllowance(string PageName, string FieldName, string FieldName2, string FieldAlias, string FieldName3, string FieldName4, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[7];
            try
            {
                if (PageName.Equals("RacingWebsites") || PageName.Equals("MasterHorseSex") || PageName.Equals("MasterHorseColor"))
                {


                    arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = PageName;

                    arParams[1] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
                    arParams[1].Value = FieldName;

                    arParams[2] = new SqlParameter("@FieldName2", SqlDbType.VarChar, 100);
                    arParams[2].Value = FieldName2;

                    arParams[3] = new SqlParameter("@FieldAlias", SqlDbType.VarChar, 100);
                    arParams[3].Value = FieldAlias;

                    arParams[4] = new SqlParameter("@FieldName3", SqlDbType.VarChar, 100);
                    arParams[4].Value = FieldName3;

                    arParams[5] = new SqlParameter("@FieldName4", SqlDbType.VarChar, 100);
                    arParams[5].Value = FieldName4;


                    arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                    arParams[6].Value = UserID_FK;

                    // status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_InsertMasterDataCondition", arParams));
                }

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertMasterDataAllowance", arParams));
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
            return status;


        }

        /// <summary>
        /// Insert Master Shoe
        /// </summary>
        public int InsertUpdateMasterDataShoe(int recordID, string action, string shoe, string shoedetail, 
            int shoemetalid, int leftforelegid, int rightforelegid, int lefthindlegid, int righthindlegid, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[10];
            try
            {
                arParams[0] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[0].Value = recordID;

                arParams[1] = new SqlParameter("@Action", SqlDbType.VarChar, 100);
                arParams[1].Value = action;

                arParams[2] = new SqlParameter("@Shoe", SqlDbType.VarChar , 1000);
                arParams[2].Value = shoe;

                arParams[3] = new SqlParameter("@ShoeDetail", SqlDbType.VarChar, 1000);
                arParams[3].Value = shoedetail;

                arParams[4] = new SqlParameter("@ShoeMetalID", SqlDbType.Int);
                arParams[4].Value = shoemetalid;

                arParams[5] = new SqlParameter("@LFLShoeBaseMID", SqlDbType.Int);
                arParams[5].Value = leftforelegid;

                arParams[6] = new SqlParameter("@RFLShoeBaseMID", SqlDbType.Int);
                arParams[6].Value = rightforelegid;

                arParams[7] = new SqlParameter("@LHLShoeBaseMID", SqlDbType.Int);
                arParams[7].Value = lefthindlegid;

                arParams[8] = new SqlParameter("@RHLShoeBaseMID", SqlDbType.Int);
                arParams[8].Value = righthindlegid;

                arParams[9] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[9].Value = UserID_FK;
                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateMasterDataShoe", arParams));
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
            return status;
        }

        /// <summary>
        /// Insert Master DiscardRules
        /// </summary>
        public int InsertUpdateMasterDataDiscardRules(int recordID, string action, int Centerid, int FromYearid, int TillYearid, int FromSeasonid, int TillSeasonid, string RuleApplyHorseAge, string MaxHandicapRating, string MinHandicapRating, string RuleApplyDate, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[12];
            try
            {
                arParams[0] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[0].Value = recordID;

                arParams[1] = new SqlParameter("@Action", SqlDbType.VarChar, 100);
                arParams[1].Value = action;

                arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[2].Value = Centerid;

                arParams[3] = new SqlParameter("@FromYearID", SqlDbType.Int);
                arParams[3].Value = FromYearid;

                arParams[4] = new SqlParameter("@TillYearID", SqlDbType.Int);
                arParams[4].Value = TillYearid;

                arParams[5] = new SqlParameter("@FromSeasonID", SqlDbType.Int);
                arParams[5].Value = FromSeasonid;

                arParams[6] = new SqlParameter("@TillSeasonID", SqlDbType.Int);
                arParams[6].Value = TillSeasonid;

                arParams[7] = new SqlParameter("@RuleApplyHorseAge", SqlDbType.VarChar, 50);
                arParams[7].Value = RuleApplyHorseAge;

                arParams[8] = new SqlParameter("@MaxHandicapRating", SqlDbType.VarChar, 50);
                arParams[8].Value = MaxHandicapRating;

                arParams[9] = new SqlParameter("@MinHandicapRating", SqlDbType.VarChar, 50);
                arParams[9].Value = MinHandicapRating;

                arParams[10] = new SqlParameter("@RuleApplyDate", SqlDbType.VarChar, 30);
                if (RuleApplyDate.Equals("__-__-____"))
                {
                    arParams[10].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = RuleApplyDate.Split('-');
                    DateTime enterDate =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[10].Value = enterDate.ToString("yyyy-MM-dd 00:00:00");

                    //string dtformat = enterDate.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[10].Value = Convert.ToDateTime(dtformat);
                }

                arParams[11] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[11].Value = UserID_FK;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateMasterDataDiscardRules", arParams));
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
            return status;
        }

        /// <summary>
        /// Update the admin Records
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int UpdateAdminPagesData(string PageName, string ActionName, int RecordID, string FieldName1, string FieldName2, string FieldName3, string FieldName4, string FieldName5, int UserID_FK)
        {
            int checkRecord;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                arParams[1] = new SqlParameter("@ActionName", SqlDbType.VarChar, 50);
                arParams[1].Value = ActionName;

                arParams[2] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[2].Value = RecordID;

                arParams[3] = new SqlParameter("@FieldName1", SqlDbType.VarChar, 100);
                arParams[3].Value = FieldName1;

                arParams[4] = new SqlParameter("@FieldName2", SqlDbType.VarChar, 100);
                arParams[4].Value = FieldName2;

                arParams[5] = new SqlParameter("@FieldName3", SqlDbType.VarChar, 100);
                arParams[5].Value = FieldName3;

                arParams[6] = new SqlParameter("@FieldName4", SqlDbType.VarChar, 100);
                arParams[6].Value = FieldName4;

                arParams[7] = new SqlParameter("@FieldName5", SqlDbType.VarChar, 100);
                arParams[7].Value = FieldName5;

                arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[8].Value = UserID_FK;

                checkRecord = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_UpdateAdminPagesData", arParams));
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



        /// <summary>
        /// Insert Master HorseRetirementAge
        /// </summary>
        public int InsertUpdateMasterDataHorseRetirementAge(int recordID, string action, int Centerid, int FromYearid, int TillYearid, int FromSeasonid, int TillSeasonid, string HorseRetirementAge, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[9];
            try
            {
                arParams[0] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[0].Value = recordID;

                arParams[1] = new SqlParameter("@Action", SqlDbType.VarChar, 50);
                arParams[1].Value = action;

                arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[2].Value = Centerid;

                arParams[3] = new SqlParameter("@FromYearID", SqlDbType.Int);
                arParams[3].Value = FromYearid;

                arParams[4] = new SqlParameter("@TillYearID", SqlDbType.Int);
                arParams[4].Value = TillYearid;

                arParams[5] = new SqlParameter("@FromSeasonID", SqlDbType.Int);
                arParams[5].Value = FromSeasonid;

                arParams[6] = new SqlParameter("@TillSeasonID", SqlDbType.Int);
                arParams[6].Value = TillSeasonid;

                arParams[7] = new SqlParameter("@HorseRetirementAge", SqlDbType.VarChar, 50);
                arParams[7].Value = HorseRetirementAge;

                arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[8].Value = UserID_FK;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateMasterDataHorseRetirementAge", arParams));
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
            return status;
        }
        /// <summary>
        /// Insert Master Season
        /// </summary>
        public int InsertUpdateMasterDataSeason(int recordID, string action, string SeasonName, string SeasonAlias, string SubSeason, string SubSeasonAlias, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[7];
            try
            {
                arParams[0] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[0].Value = recordID;

                arParams[1] = new SqlParameter("@Action", SqlDbType.VarChar, 100);
                arParams[1].Value = action;

                arParams[2] = new SqlParameter("@SeasonName", SqlDbType.VarChar, 50);
                arParams[2].Value = SeasonName;

                arParams[3] = new SqlParameter("@SeasonAlias", SqlDbType.VarChar, 50);
                arParams[3].Value = SeasonAlias;

                arParams[4] = new SqlParameter("@SubSeason", SqlDbType.VarChar, 50);
                arParams[4].Value = SubSeason;

                arParams[5] = new SqlParameter("@SubSeasonAlias", SqlDbType.VarChar, 50);
                arParams[5].Value = SubSeasonAlias;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = UserID_FK;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateMasterDataSeason", arParams));
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
            return status;
        }
        /// <summary>
        /// Insert Master SeasonDescription
        /// </summary>
        /// 
        public int InsertUpdateMasterDataSeasonDescription(int recordID, string action, int CenterID, int YearID, int SeasonID, string SeasonStartDate, string SeasonEndDate,
                  int SubSeasonID, string SubSeasonStartDate, string SubSeasonEndDate, string SeasonStartingNumber, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[12];
            try
            {
                arParams[0] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[0].Value = recordID;

                arParams[1] = new SqlParameter("@Action", SqlDbType.VarChar, 100);
                arParams[1].Value = action;

                arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[2].Value = CenterID;

                arParams[3] = new SqlParameter("@YearID", SqlDbType.Int);
                arParams[3].Value = YearID;

                arParams[4] = new SqlParameter("@SeasonID", SqlDbType.Int);
                arParams[4].Value = SeasonID;

                //arParams[5] = new SqlParameter("@SeasonStartDt", SqlDbType.DateTime);
                //arParams[5].Value = SeasonStartDate;
                if (action == "Delete")
                {
                    arParams[5] = new SqlParameter("@SeasonStartDt", SqlDbType.DateTime);
                    arParams[5].Value = DBNull.Value;
                    arParams[6] = new SqlParameter("@SeasonEndDt", SqlDbType.DateTime);
                    arParams[6].Value = DBNull.Value;
                }
                else
                {

                    arParams[5] = new SqlParameter("@SeasonStartDt", SqlDbType.VarChar, 30);
                    if (SeasonStartDate.Equals("__-__-____"))
                    {
                        arParams[5].Value = DBNull.Value;
                    }
                    else
                    {
                        string[] dateString = SeasonStartDate.Split('-');
                        DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                        arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                        //string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                        //arParams[5].Value = Convert.ToDateTime(dtformat);
                    }

                    arParams[6] = new SqlParameter("@SeasonEndDt", SqlDbType.VarChar, 30);
                    if (SeasonEndDate.Equals("__-__-____"))
                    {
                        arParams[6].Value = DBNull.Value;
                    }
                    else
                    {
                        string[] dateString = SeasonEndDate.Split('-');
                        DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                        arParams[6].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");

                        //string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                        //arParams[6].Value = Convert.ToDateTime(dtformat);
                    }

                }
                arParams[7] = new SqlParameter("@SubSeasonID", SqlDbType.Int);
                arParams[7].Value = 0;
                arParams[8] = new SqlParameter("@SubSeasonStartDt", SqlDbType.DateTime);
                arParams[8].Value = DBNull.Value;
                arParams[9] = new SqlParameter("@SubSeasonEndDt", SqlDbType.DateTime);
                arParams[9].Value = DBNull.Value;
                
                arParams[10] = new SqlParameter("@SeasonRNumberChange", SqlDbType.VarChar, 50);
                arParams[10].Value = SeasonStartingNumber;

                arParams[11] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[11].Value = UserID_FK;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateMasterDataSeasonDescription", arParams));
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
            return status;
        }


        /// <summary>
        /// Insert Master AGe condition Data in database
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        // public int InsertMasterPagesDataCondition(string PageName, string PageFieldName, string PageFieldAlias, string PageFieldName1, string PageFieldName2, string PageFieldName3, string PageFieldName4, int UserID_FK)
        public int InsertMasterPagesDataCondition(string PageName, string PageFieldName, string PageFieldAlias, string PageFieldName1, string PageFieldName2, string PageFieldName3, string PageFieldName4, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[8];

            arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
            arParams[0].Value = PageName;

            arParams[1] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
            arParams[1].Value = PageFieldName;

            arParams[2] = new SqlParameter("@FieldAlias", SqlDbType.VarChar, 100);
            arParams[2].Value = PageFieldAlias;

            arParams[3] = new SqlParameter("@PageFieldName1", SqlDbType.VarChar, 100);
            arParams[3].Value = PageFieldName1;

            arParams[4] = new SqlParameter("@PageFieldName2", SqlDbType.VarChar, 100);
            arParams[4].Value = PageFieldName2;

            arParams[5] = new SqlParameter("@PageFieldName3", SqlDbType.VarChar, 100);
            arParams[5].Value = PageFieldName3;

            arParams[6] = new SqlParameter("@PageFieldName4", SqlDbType.VarChar, 100);
            arParams[6].Value = PageFieldName4;

            arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
            arParams[7].Value = UserID_FK;
            status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertMasterDataCondition", arParams));

            return status;
        }

        /// <summary>
        /// Update Master Age Record
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="RecordID"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="UserID_FK"></param>
        /// <param name="RecordStatus"></param>
        /// <returns></returns>
        public int UpdateMasterPagesDataCondition(string PageName, int RecordID, string PageFieldName, string PageFieldAlias, string FieldName1, string FieldName2, string FieldName3, string FieldName4, int UserID_FK, string RecordStatus)
        {
            //bool checkRecord;

            SqlParameter[] arParams = new SqlParameter[10];
            arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
            arParams[0].Value = PageName;

            arParams[1] = new SqlParameter("@RecordID", SqlDbType.Int);
            arParams[1].Value = RecordID;

            arParams[2] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
            arParams[2].Value = PageFieldName;

            arParams[3] = new SqlParameter("@FieldAlias", SqlDbType.VarChar, 100);
            arParams[3].Value = PageFieldAlias;

            arParams[4] = new SqlParameter("@FieldName1", SqlDbType.VarChar, 100);
            arParams[4].Value = FieldName1;

            arParams[5] = new SqlParameter("@FieldName2", SqlDbType.VarChar, 100);
            arParams[5].Value = FieldName2;

            arParams[6] = new SqlParameter("@FieldName3", SqlDbType.VarChar, 100);
            arParams[6].Value = FieldName3;

            arParams[7] = new SqlParameter("@FieldName4", SqlDbType.VarChar, 100);
            arParams[7].Value = FieldName4;

            arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
            arParams[8].Value = UserID_FK;

            arParams[9] = new SqlParameter("@RecordStatus", SqlDbType.VarChar, 100);
            arParams[9].Value = RecordStatus;

            int value = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_UpdateMasterDataCondition", arParams));

            return value;
        }

        /// <summary>
        /// InsertUpdateMasterData Horse Distance/Class Performance data based on flag IsDistanceOrClass
        /// <returns>int</returns>
        public int InsertUpdateMasterDataHorsePerformance(string IsDistanceOrClass, int RecordID, string action_, StringBuilder xml_, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[4];
            try
            {

                arParams[0] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[0].Value = RecordID;

                arParams[1] = new SqlParameter("@action", SqlDbType.VarChar, 100);
                arParams[1].Value = action_;

                arParams[2] = new SqlParameter("@HorseXML", SqlDbType.Xml);
                arParams[2].Value = xml_.ToString();

                arParams[3] = new SqlParameter("@UserId", SqlDbType.Int);
                arParams[3].Value = UserID_FK;

                if (IsDistanceOrClass == "Distance")
                    status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateMasterDataHorseDistancePerformance", arParams));
                else
                    status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateMasterDataHorseClassPerformance", arParams));

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
            return status;
        }
        /// <summary>
        /// InsertUpdateMasterData Horse Distance/Class PerformanceOLD based on flag IsDistanceOrClass
        /// <returns>int</returns>
        public int InsertUpdateMasterDataHorsePerformanceOLD(string IsDistanceOrClass, int RecordID, string action_, StringBuilder xml_, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[0].Value = RecordID;

                arParams[1] = new SqlParameter("@action", SqlDbType.VarChar, 100);
                arParams[1].Value = action_;

                arParams[2] = new SqlParameter("@HorseXML", SqlDbType.Xml);
                arParams[2].Value = xml_.ToString();

                arParams[3] = new SqlParameter("@UserId", SqlDbType.Int);
                arParams[3].Value = UserID_FK;

                if (IsDistanceOrClass == "Distance")
                    status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateMasterDataHorseDistancePerformanceOLD", arParams));
                else
                    status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateMasterDataHorseClassPerformanceOLD", arParams));

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
            return status;
        }


        /// <summary>
        /// Insert Master Center Data 
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="strCenterOldName"></param>
        /// <param name="strCenterOldNameAlias"></param>
        /// <param name="strDate"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int InsertMasterPagesData(string PageName, string PageFieldName, string PageFieldAlias, string strCenterOldName, string strCenterOldNameAlias, string strDate, int UserID_FK)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                arParams[1] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
                arParams[1].Value = PageFieldName;

                arParams[2] = new SqlParameter("@FieldAlias", SqlDbType.VarChar, 100);
                arParams[2].Value = PageFieldAlias;

                arParams[3] = new SqlParameter("@OldFieldName", SqlDbType.VarChar, 100);
                arParams[3].Value = strCenterOldName;

                arParams[4] = new SqlParameter("@OldFieldAlias", SqlDbType.VarChar, 100);
                arParams[4].Value = strCenterOldNameAlias;


                arParams[5] = new SqlParameter("@Date", SqlDbType.VarChar, 30);
                if (strDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = strDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");

                    //string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }



                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = UserID_FK;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertMasterDataCenter", arParams));

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
            return status;
        }


        // <summary>
        /// Update the Master Records
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int UpdateMasterPagesData(string PageName, int RecordID, string PageFieldName, string PageFieldAlias, string strCenterOldName, string strCenterOldNameAlias, int UserID_FK, string RecordStatus, string NameofDateChange)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@Master_PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                arParams[1] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[1].Value = RecordID;

                arParams[2] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
                arParams[2].Value = PageFieldName;

                arParams[3] = new SqlParameter("@FieldAlias", SqlDbType.VarChar, 100);
                arParams[3].Value = PageFieldAlias;


                arParams[4] = new SqlParameter("@OldFieldName", SqlDbType.VarChar, 100);
                arParams[4].Value = strCenterOldName;

                arParams[5] = new SqlParameter("@OldFieldAlias", SqlDbType.VarChar, 100);
                arParams[5].Value = strCenterOldNameAlias;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = UserID_FK;

                arParams[7] = new SqlParameter("@RecordStatus", SqlDbType.VarChar, 100);
                arParams[7].Value = RecordStatus;

                arParams[8] = new SqlParameter("@NameofDateChange", SqlDbType.VarChar, 30);
                if (NameofDateChange.Equals("__-__-____"))
                {
                    arParams[8].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = NameofDateChange.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[8].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");

                    //string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[8].Value = Convert.ToDateTime(dtformat);
                }

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_UpdateMasterDataCenter", arParams));
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
            return status;
        }




        // 20 jAN 2016

        public int InsertRaceTimings(VKATalkClassLayer.RaceTimings clsRaceTiming, int racetimingsid, int userid, string action)
        {

            int status = 0;
            //  string clsType, int UserID_FK

            SqlParameter[] arParams = new SqlParameter[20];
            try
            {

                arParams[0] = new SqlParameter("@RaceTimingsID", SqlDbType.Int) { Value = racetimingsid };
                arParams[1] = new SqlParameter("@Action", SqlDbType.VarChar, 50) { Value = action };
                arParams[2] = new SqlParameter("@RaceTimingType", SqlDbType.VarChar, 50) { Value = clsRaceTiming.RaceTimingType };
                arParams[3] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = clsRaceTiming.CenterID };
                arParams[4] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = clsRaceTiming.FromYearID };
                arParams[5] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = clsRaceTiming.TillYearID };
                arParams[6] = new SqlParameter("@FromSeasonID", SqlDbType.Int) { Value = clsRaceTiming.FromSeasonID };
                arParams[7] = new SqlParameter("@TillSeasonID", SqlDbType.Int) { Value = clsRaceTiming.TillSeasonID };
                arParams[8] = new SqlParameter("@TrackID", SqlDbType.Int) { Value = clsRaceTiming.TrackID };
                arParams[9] = new SqlParameter("@DistanceID", SqlDbType.Int) { Value = clsRaceTiming.DistanceID };
                arParams[10] = new SqlParameter("@RaceType", SqlDbType.VarChar, 50) { Value = clsRaceTiming.RaceType };
                arParams[11] = new SqlParameter("@ClassID", SqlDbType.Int) { Value = clsRaceTiming.ClassID };
                arParams[12] = new SqlParameter("@RaceStatus", SqlDbType.VarChar, 50) { Value = clsRaceTiming.RaceStatus };
                arParams[13] = new SqlParameter("@RaceDate", SqlDbType.DateTime);
                if (clsRaceTiming.RaceDate.Equals("__-__-____"))
                {
                    arParams[13].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = clsRaceTiming.RaceDate.Split('-');
                    DateTime enterDate =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);

                    arParams[13].Value = enterDate.ToString("yyyy-MM-dd 00:00:00");

                    //string dtformat = enterDate.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[13].Value = Convert.ToDateTime(dtformat);
                }
                arParams[14] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = clsRaceTiming.HorseNameID };
                arParams[15] = new SqlParameter("@CarriedWeight", SqlDbType.VarChar, 50) { Value = clsRaceTiming.CarriedWeight };
                arParams[16] = new SqlParameter("@PenetrometerReading", SqlDbType.VarChar, 50) { Value = clsRaceTiming.PenetrometerReading };
                arParams[17] = new SqlParameter("@FalseRails", SqlDbType.VarChar, 50) { Value = clsRaceTiming.FalseRails };
                arParams[18] = new SqlParameter("@Timing", SqlDbType.VarChar, 50) { Value = clsRaceTiming.Timing };
                arParams[19] = new SqlParameter("@USERID", SqlDbType.Int) { Value = userid };

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertRaceTimings", arParams));
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
            return status;
        }



        /// <summary>
        /// Insert Year Data in database
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="FieldName"></param>
        /// <param name="FieldName2"></param>
        /// <param name="FieldAlias"></param>
        /// <param name="FieldName3"></param>
        /// <param name="FieldName4"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int InsertMasterPagesDataYear(string Status, string FieldName, string FieldAlias, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[4];
            try
            {

                arParams[0] = new SqlParameter("@InsertUpdate", SqlDbType.VarChar, 100);
                arParams[0].Value = Status;

                arParams[1] = new SqlParameter("@Year", SqlDbType.VarChar, 100);
                arParams[1].Value = FieldName;

                arParams[2] = new SqlParameter("@Alias", SqlDbType.VarChar, 100);
                arParams[2].Value = FieldAlias;

                //arParams[3] = new SqlParameter("@StartDate", SqlDbType.VarChar, 30);
                //if (strDate.Equals("__-__-____"))
                //{
                //    arParams[3].Value = DBNull.Value;
                //}
                //else
                //{
                //    string[] dateString = strDate.Split('-');
                //    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                //    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                //}

                //arParams[4] = new SqlParameter("@EndDate", SqlDbType.VarChar, 30);
                //if (strEndDate.Equals("__-__-____"))
                //{
                //    arParams[4].Value = DBNull.Value;
                //}
                //else
                //{
                //    string[] dateString = strEndDate.Split('-');
                //    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                //    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");

                //    //string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                //    //arParams[4].Value = Convert.ToDateTime(dtformat);
                //}

                arParams[3] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[3].Value = UserID_FK;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateYear", arParams));
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
            return status;


        }

        /// <summary>
        /// Update Year Master Record
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="FieldName"></param>
        /// <param name="FieldAlias"></param>
        /// <param name="strDate"></param>
        /// <param name="strEndDate"></param>
        /// <param name="RecordID"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int UpdateMasterPagesDataYear(string Status, string FieldName, string FieldAlias, int RecordID, int UserID_FK)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[5];
            try
            {

                arParams[0] = new SqlParameter("@InsertUpdate", SqlDbType.VarChar, 100);
                arParams[0].Value = Status;

                arParams[1] = new SqlParameter("@Year", SqlDbType.VarChar, 100);
                arParams[1].Value = FieldName;

                arParams[2] = new SqlParameter("@Alias", SqlDbType.VarChar, 100);
                arParams[2].Value = FieldAlias;

                arParams[3] = new SqlParameter("@RecordID", SqlDbType.Int);
                arParams[3].Value = RecordID;

                arParams[4] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[4].Value = UserID_FK;

                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_InsertUpdateYear", arParams));
                // checkstatus = true;
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
            return status;
        }

        /// <summary>
        /// Insert and UPdate Owner Color
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="FieldName"></param>
        /// <param name="FieldAlias"></param>
        /// <param name="CenterName"></param>
        /// <param name="strChangeofDate"></param>
        /// <param name="RecordID"></param>
        /// <param name="UserID_FK"></param>
        public int InsertUpdateOwnerColor(string Status, string FieldName, string Capcolor, int RecordID, int UserID_FK)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[5];
            try
            {

                arParams[0] = new SqlParameter("@InsertUpdate", SqlDbType.VarChar, 100);
                arParams[0].Value = Status;

                arParams[1] = new SqlParameter("@OwnerColor", SqlDbType.VarChar, 1000);
                arParams[1].Value = FieldName;

                arParams[2] = new SqlParameter("@CapColor", SqlDbType.VarChar, 100);
                arParams[2].Value = Capcolor;

                arParams[3] = new SqlParameter("@RECORDID", SqlDbType.Int);
                arParams[3].Value = RecordID;

                arParams[4] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[4].Value = UserID_FK;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateOwnerColor", arParams));
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
            return status;
        }


        /// <summary>
        /// Insert Stake Money Value
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="Field1"></param>
        /// <param name="Field2"></param>
        /// <param name="Field3"></param>
        /// <param name="Field4"></param>
        /// <param name="Field5"></param>
        /// <param name="Field6"></param>
        /// <param name="Field7"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public DataTable InsertStakeMoney(VKATalkClassLayer.StakeMoney clsType, int UserID_FK, string action, int stakemoneyid, int stakemoneyearnerid)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[22];
            try
            {
                arParams[0] = new SqlParameter("@Center", SqlDbType.VarChar, 50);
                arParams[0].Value = clsType.Center;

                arParams[1] = new SqlParameter("@RaceStatus", SqlDbType.VarChar, 50);
                arParams[1].Value = clsType.RaceStatus;

                arParams[2] = new SqlParameter("@MasterRaceNameID", SqlDbType.VarChar, 50);
                arParams[2].Value = clsType.MasterRaceNameID;

                arParams[3] = new SqlParameter("@RaceType", SqlDbType.VarChar, 50);
                arParams[3].Value = clsType.RaceType;

                arParams[4] = new SqlParameter("@HandicapRatingRangeID", SqlDbType.VarChar, 50);
                arParams[4].Value = clsType.HandicapRatingRange;

                arParams[5] = new SqlParameter("@AgeConditionID", SqlDbType.VarChar, 50);
                arParams[5].Value = clsType.AgeCondition;

                arParams[6] = new SqlParameter("@StakeMoneyTableNo", SqlDbType.VarChar, 50);
                arParams[6].Value = clsType.StakeMoneyTableNo;

                arParams[7] = new SqlParameter("@StakeMoney", SqlDbType.VarChar, 50);
                arParams[7].Value = clsType.StakeMoneyVar;

                arParams[8] = new SqlParameter("@MomenttoCost", SqlDbType.VarChar, 50);
                arParams[8].Value = clsType.MomenttoCost;

                arParams[9] = new SqlParameter("@FromYearID", SqlDbType.VarChar, 50);
                arParams[9].Value = clsType.FromYearId;

                arParams[10] = new SqlParameter("@TillDate", SqlDbType.VarChar, 50);
                if (clsType.TillDate.Equals("__-__-____"))
                {
                    arParams[10].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = clsType.TillDate.Split('-');
                    DateTime enter_date =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[10].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[11] = new SqlParameter("@TillYearID", SqlDbType.VarChar, 50);
                arParams[11].Value = clsType.TillYearID;

                arParams[12] = new SqlParameter("@FromSeasonID", SqlDbType.VarChar, 50);
                arParams[12].Value = clsType.FromSeason;
                arParams[13] = new SqlParameter("@TillSeasonID", SqlDbType.VarChar, 50);
                arParams[13].Value = clsType.TillSeason;
                arParams[14] = new SqlParameter("@Placing", SqlDbType.VarChar, 50);
                arParams[14].Value = clsType.Placing;
                arParams[15] = new SqlParameter("@SMEProfileTypeID", SqlDbType.VarChar, 50);
                arParams[15].Value = clsType.SMEProfileTypeID;
                arParams[16] = new SqlParameter("@Percentage", SqlDbType.VarChar, 50);
                arParams[16].Value = clsType.Percentage;
                arParams[17] = new SqlParameter("@Amount", SqlDbType.VarChar, 50);
                arParams[17].Value = clsType.Amount;
                arParams[18] = new SqlParameter("@UserId", SqlDbType.Int);
                arParams[18].Value = UserID_FK;

                arParams[19] = new SqlParameter("@Action", SqlDbType.VarChar,100);
                arParams[19].Value = action;

                arParams[20] = new SqlParameter("@StakeMoneyID", SqlDbType.Int);
                arParams[20].Value = stakemoneyid;

                arParams[21] = new SqlParameter("@StakeMoneyEarnerID", SqlDbType.Int);
                arParams[21].Value = stakemoneyearnerid;
                
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_InsertStakeMoney", arParams);


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

        public DataTable GetStakeMoney(VKATalkClassLayer.StakeMoney clsType)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[21];
            try
            {
                arParams[0] = new SqlParameter("@Center", SqlDbType.VarChar, 50);
                arParams[0].Value = clsType.Center;

                arParams[1] = new SqlParameter("@RaceStatus", SqlDbType.VarChar, 50);
                arParams[1].Value = clsType.RaceStatus;

                arParams[2] = new SqlParameter("@MasterRaceNameID", SqlDbType.VarChar, 50);
                arParams[2].Value = clsType.MasterRaceNameID;

                arParams[3] = new SqlParameter("@RaceType", SqlDbType.VarChar, 50);
                arParams[3].Value = clsType.RaceType;

                arParams[4] = new SqlParameter("@HandicapRatingRangeID", SqlDbType.VarChar, 50);
                arParams[4].Value = clsType.HandicapRatingRange;

                arParams[5] = new SqlParameter("@AgeConditionID", SqlDbType.VarChar, 50);
                arParams[5].Value = clsType.AgeCondition;

                arParams[6] = new SqlParameter("@StakeMoneyTableNo", SqlDbType.VarChar, 50);
                arParams[6].Value = clsType.StakeMoneyTableNo;

                arParams[7] = new SqlParameter("@StakeMoney", SqlDbType.VarChar, 50);
                arParams[7].Value = clsType.StakeMoneyVar;

                arParams[8] = new SqlParameter("@MomenttoCost", SqlDbType.VarChar, 50);
                arParams[8].Value = clsType.MomenttoCost;

                arParams[9] = new SqlParameter("@FromYearID", SqlDbType.VarChar, 50);
                arParams[9].Value = clsType.FromYearId;

                arParams[10] = new SqlParameter("@TillDate", SqlDbType.VarChar, 50);
                if (clsType.TillDate.Equals("__-__-____"))
                {
                    arParams[10].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = clsType.TillDate.Split('-');
                    DateTime enter_date =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[10].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[11] = new SqlParameter("@TillYearID", SqlDbType.VarChar, 50);
                arParams[11].Value = clsType.TillYearID;

                arParams[12] = new SqlParameter("@FromSeasonID", SqlDbType.VarChar, 50);
                arParams[12].Value = clsType.FromSeason;
                arParams[13] = new SqlParameter("@TillSeasonID", SqlDbType.VarChar, 50);
                arParams[13].Value = clsType.TillSeason;
                arParams[14] = new SqlParameter("@Placing", SqlDbType.VarChar, 50);
                arParams[14].Value = clsType.Placing;
                arParams[15] = new SqlParameter("@SMEProfileTypeID", SqlDbType.VarChar, 50);
                arParams[15].Value = clsType.SMEProfileTypeID;
                arParams[16] = new SqlParameter("@Percentage", SqlDbType.VarChar, 50);
                arParams[16].Value = clsType.Percentage;
                arParams[17] = new SqlParameter("@Amount", SqlDbType.VarChar, 50);
                arParams[17].Value = clsType.Amount;
                arParams[18] = new SqlParameter("@UserId", SqlDbType.Int);
                arParams[18].Value = 0;

                arParams[19] = new SqlParameter("@Action", SqlDbType.VarChar, 100);
                arParams[19].Value = string.Empty;

                arParams[20] = new SqlParameter("@StakeMoneyID", SqlDbType.Int);
                arParams[20].Value = 0;

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetStakeMoney", arParams);


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
        /// <summary>
        /// Insert Stake Money Earner
        /// </summary>
        /// <param name="StakeMoneyEarner"></param>
        /// <param name="TableNo"></param>
        /// <param name="RelationID"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int InsertStakeMoneyEarner(string StakeMoneyEarner, string TableNo, int RelationID, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[4];
            try
            {

                //arParams[0] = new SqlParameter("@x", SqlDbType.Xml);
                //arParams[0].Value = clsType;

                arParams[0] = new SqlParameter("@StakeMoneyEarner", SqlDbType.VarChar, 1000);
                arParams[0].Value = StakeMoneyEarner;

                arParams[1] = new SqlParameter("@TableNo", SqlDbType.VarChar, 50);
                arParams[1].Value = TableNo;

                arParams[2] = new SqlParameter("@RelationID", SqlDbType.Int);
                arParams[2].Value = RelationID;

                arParams[3] = new SqlParameter("@UserID", SqlDbType.VarChar, 50);
                arParams[3].Value = UserID_FK;
                // status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_InsertStakeMoney", arParams));
                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertStakeMoneyEarner", arParams));


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
            return status;
        }

        /// <summary>
        /// Insert Distance/Age Parameter of Scale of Weight
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <param name="ParameterName"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int InsertDistanceAgeParameter(int ParameterID, string ParameterName, int UserID_FK)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[3];
            try
            {

                //arParams[0] = new SqlParameter("@x", SqlDbType.Xml);
                //arParams[0].Value = clsType;

                arParams[0] = new SqlParameter("@ParameterID", SqlDbType.Int);
                arParams[0].Value = ParameterID;

                arParams[1] = new SqlParameter("@ParameterName", SqlDbType.VarChar, 50);
                arParams[1].Value = ParameterName;

                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = UserID_FK;
                // status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_InsertStakeMoney", arParams));
                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertDistanceAgeParameter", arParams));


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
            return status;
        }

        /// <summary>
        /// Insert Class Group Parameter
        /// </summary>
        /// <param name="Center"></param>
        /// <param name="FromYear"></param>
        /// <param name="TillYear"></param>
        /// <param name="FromSeason"></param>
        /// <param name="TillSeason"></param>
        /// <param name="ClassGroup"></param>
        /// <param name="RaceType"></param>
        /// <param name="RaceStatus"></param>
        /// <param name="Million"></param>
        /// <param name="SweepStake"></param>
        /// <param name="Graded"></param>
        /// <param name="Category"></param>
        /// <param name="Class"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int InsertClassGroup(int CenterID, int FromYearID, int TillYearID, int FromSeasonID, int TillSeasonID, int ClassGrouptypeid, string RaceType, string RaceStatus, string Million, string SweepStake, string Graded, string category, int classtypeid, int UserID_FK, string Classic, string tasktype, int classgroupid, int classgroupaliasid, int ageconditionid)
        {

            int status = 0;
            //  string clsType, int UserID_FK

            SqlParameter[] arParams = new SqlParameter[19];
            try
            {

                arParams[0] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[0].Value = CenterID;

                arParams[1] = new SqlParameter("@FromYearID", SqlDbType.Int);
                arParams[1].Value = FromYearID;

                arParams[2] = new SqlParameter("@TillYearID", SqlDbType.Int);
                arParams[2].Value = TillYearID;

                arParams[3] = new SqlParameter("@FromSeasonID", SqlDbType.Int);
                arParams[3].Value = FromSeasonID;

                arParams[4] = new SqlParameter("@TillSeasonID", SqlDbType.Int);
                arParams[4].Value = TillSeasonID;

                arParams[5] = new SqlParameter("@ClassGroupTypeID", SqlDbType.Int);
                arParams[5].Value = ClassGrouptypeid;

                arParams[6] = new SqlParameter("@RaceType", SqlDbType.VarChar, 50);
                arParams[6].Value = RaceType;

                arParams[7] = new SqlParameter("@ClassTypeID", SqlDbType.Int) { Value = classtypeid };

                arParams[8] = new SqlParameter("@Category", SqlDbType.VarChar, 50) { Value = category };
                arParams[9] = new SqlParameter("@RaceStatus", SqlDbType.VarChar, 50);
                arParams[9].Value = RaceStatus;

                arParams[10] = new SqlParameter("@Million", SqlDbType.VarChar, 50);
                arParams[10].Value = Million;

                arParams[11] = new SqlParameter("@SweepStake", SqlDbType.VarChar, 50);
                arParams[11].Value = SweepStake;

                arParams[12] = new SqlParameter("@Classic", SqlDbType.VarChar, 50);
                arParams[12].Value = Classic;

                arParams[13] = new SqlParameter("@Graded", SqlDbType.VarChar, 50);
                arParams[13].Value = Graded;

                arParams[14] = new SqlParameter("@USERID", SqlDbType.Int);
                arParams[14].Value = UserID_FK;

                arParams[15] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                arParams[16] = new SqlParameter("@ClassGroupID", SqlDbType.VarChar, 50) { Value = classgroupid };
                arParams[17] = new SqlParameter("@ClassGroupAliasID", SqlDbType.Int) { Value = classgroupaliasid };
                arParams[18] = new SqlParameter("@AgeConditionID", SqlDbType.Int) { Value = ageconditionid };

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertClassGroup", arParams));
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
            return status;
        }


        /// <summary>
        /// Insert Master class
        /// </summary>
        public int InserUpdatetMasterPagesDataClass(int Center, int FromYear, int TillYear, int FromSeason, int TillSeason, string Category, int classtypeid, int classtypealiasid, int handicapRatingRangeID, string MaxHandicapRating, string MinHandicapRating, int UserID_FK, string ClassStatus, int ClassID)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[14];
            try
            {

                arParams[0] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[0].Value = Center;

                arParams[1] = new SqlParameter("@FromYearID", SqlDbType.Int);
                arParams[1].Value = FromYear;

                arParams[2] = new SqlParameter("@TillYearID", SqlDbType.Int);
                arParams[2].Value = TillYear;


                arParams[3] = new SqlParameter("@FromSeasonID", SqlDbType.Int);
                arParams[3].Value = FromSeason;

                arParams[4] = new SqlParameter("@TillSeasonID", SqlDbType.Int);
                arParams[4].Value = TillSeason;

                arParams[5] = new SqlParameter("@ClassTypeID", SqlDbType.Int);
                arParams[5].Value = classtypeid;

                arParams[6] = new SqlParameter("@ClassTypeAliasID", SqlDbType.Int);
                arParams[6].Value = classtypealiasid;


                arParams[7] = new SqlParameter("@Category", SqlDbType.VarChar, 100);
                arParams[7].Value = Category;

                arParams[8] = new SqlParameter("@HandicapRatingRangeID", SqlDbType.Int);
                arParams[8].Value = handicapRatingRangeID;

                arParams[9] = new SqlParameter("@MinHandicapRating", SqlDbType.VarChar, 50);
                arParams[9].Value = MinHandicapRating;

                arParams[10] = new SqlParameter("@MaxHandicapRating", SqlDbType.VarChar, 50);
                arParams[10].Value = MaxHandicapRating;



                arParams[11] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[11].Value = UserID_FK;

                arParams[12] = new SqlParameter("@ClassStatus", SqlDbType.VarChar, 50);
                arParams[12].Value = ClassStatus;

                arParams[13] = new SqlParameter("@ClassID", SqlDbType.Int);
                arParams[13].Value = ClassID;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateClass", arParams));
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
            return status;
        }


        /// <summary>
        /// Insert Master class
        /// </summary>
        public int InsertUpdateMasterHabit(string Habit, string HabitAlias, int HabitTypeID, int UserID_FK, string Status, int HabitID)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[6];
            try
            {

                arParams[0] = new SqlParameter("@Habit", SqlDbType.VarChar, 50);
                arParams[0].Value = Habit;

                arParams[1] = new SqlParameter("@HabitAlias", SqlDbType.VarChar, 50);
                arParams[1].Value = HabitAlias;

                arParams[2] = new SqlParameter("@HabitTypeID", SqlDbType.Int);
                arParams[2].Value = HabitTypeID;

                arParams[3] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[3].Value = UserID_FK;

                arParams[4] = new SqlParameter("@Status", SqlDbType.VarChar, 50);
                arParams[4].Value = Status;

                arParams[5] = new SqlParameter("@HabitID", SqlDbType.Int);
                arParams[5].Value = HabitID;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateMasterHabit", arParams));
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
            return status;
        }


        /// <summary>
        /// Insert Scale of weight
        /// </summary>
        public int InsertUpdateMasterScaleofWeight(VKATalkClassLayer.ScaleofWeight clsScaleofWeight, int UserID_FK, string action, int sowid)
        {

            int status = 0;

            SqlParameter[] arParams = new SqlParameter[24];
            try
            {

                arParams[0] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[0].Value = clsScaleofWeight.CenterID;

                arParams[1] = new SqlParameter("@FromYearID", SqlDbType.Int);
                arParams[1].Value = clsScaleofWeight.FromYearID;

                arParams[2] = new SqlParameter("@TillYearID", SqlDbType.Int);
                arParams[2].Value = clsScaleofWeight.TillYearID;

                arParams[3] = new SqlParameter("@FromSeasonID", SqlDbType.Int);
                arParams[3].Value = clsScaleofWeight.FromSeasonID;

                arParams[4] = new SqlParameter("@TillSeasonID", SqlDbType.Int);
                arParams[4].Value = clsScaleofWeight.TillSeasonID;

                if (action.Equals("Delete"))
                {
                    arParams[5] = new SqlParameter("@ScaleofWeight", SqlDbType.VarChar, 50);
                    arParams[5].Value = "";

                    arParams[6] = new SqlParameter("@CI", SqlDbType.VarChar, 50);
                    arParams[6].Value = "";

                    arParams[7] = new SqlParameter("@CII", SqlDbType.VarChar, 50);
                    arParams[7].Value = "";

                    arParams[8] = new SqlParameter("@CIII", SqlDbType.VarChar, 50);
                    arParams[8].Value = "";

                    arParams[9] = new SqlParameter("@CIV", SqlDbType.VarChar, 50);
                    arParams[9].Value = "";

                    arParams[10] = new SqlParameter("@CV", SqlDbType.VarChar, 50);
                    arParams[10].Value = "";

                    arParams[11] = new SqlParameter("@HandicapWeight", SqlDbType.VarChar, 50);
                    arParams[11].Value = "";

                    arParams[12] = new SqlParameter("@Month", SqlDbType.VarChar, 50);
                    arParams[12].Value = "";

                    arParams[13] = new SqlParameter("@DistanceParameter", SqlDbType.VarChar, 50);
                    arParams[13].Value = "";

                    arParams[14] = new SqlParameter("@NationID", SqlDbType.Int);
                    arParams[14].Value = 0;

                    arParams[15] = new SqlParameter("@AgeParameter", SqlDbType.VarChar, 50);
                    arParams[15].Value = "";

                    arParams[16] = new SqlParameter("@AgeParameterHandicapWeight", SqlDbType.VarChar, 50);
                    arParams[16].Value = "";

                    arParams[17] = new SqlParameter("@WeightSystemType", SqlDbType.VarChar, 100);
                    arParams[17].Value = "";
                }
                else
                {
                    arParams[5] = new SqlParameter("@ScaleofWeight", SqlDbType.VarChar, 50);
                    arParams[5].Value = clsScaleofWeight.ScaleofWeightType;

                    arParams[6] = new SqlParameter("@CI", SqlDbType.VarChar, 50);
                    arParams[6].Value = clsScaleofWeight.CIHandicapRating;

                    arParams[7] = new SqlParameter("@CII", SqlDbType.VarChar, 50);
                    arParams[7].Value = clsScaleofWeight.CIIHandicapRating;

                    arParams[8] = new SqlParameter("@CIII", SqlDbType.VarChar, 50);
                    arParams[8].Value = clsScaleofWeight.CIIIHandicapRating;

                    arParams[9] = new SqlParameter("@CIV", SqlDbType.VarChar, 50);
                    arParams[9].Value = clsScaleofWeight.CIVHandicapRating;

                    arParams[10] = new SqlParameter("@CV", SqlDbType.VarChar, 50);
                    arParams[10].Value = clsScaleofWeight.CVHandicapRating;

                    arParams[11] = new SqlParameter("@HandicapWeight", SqlDbType.VarChar, 50);
                    arParams[11].Value = clsScaleofWeight.HandicapWeight;

                    arParams[12] = new SqlParameter("@Month", SqlDbType.VarChar, 50);
                    arParams[12].Value = clsScaleofWeight.Month;

                    arParams[13] = new SqlParameter("@DistanceParameter", SqlDbType.VarChar, 50);
                    arParams[13].Value = clsScaleofWeight.DistanceParameter;

                    arParams[14] = new SqlParameter("@NationID", SqlDbType.Int);
                    arParams[14].Value = clsScaleofWeight.NationID;

                    arParams[15] = new SqlParameter("@AgeParameter", SqlDbType.VarChar, 50);
                    arParams[15].Value = clsScaleofWeight.AgeParameter;

                    arParams[16] = new SqlParameter("@AgeParameterHandicapWeight", SqlDbType.VarChar, 50);
                    arParams[16].Value = clsScaleofWeight.AgeHandicapWeight;

                    arParams[17] = new SqlParameter("@WeightSystemType", SqlDbType.VarChar, 100);
                    arParams[17].Value = clsScaleofWeight.WeightSystemType;


                }

                arParams[18] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[18].Value = UserID_FK;

                arParams[19] = new SqlParameter("@Action", SqlDbType.VarChar, 50);
                arParams[19].Value = action;

                arParams[20] = new SqlParameter("@SowID", SqlDbType.Int);
                arParams[20].Value = sowid;

				if (action.Equals("Delete"))
				{
					arParams[21] = new SqlParameter("@HorseGender", SqlDbType.VarChar, 50);
					arParams[21].Value = "";

					arParams[22] = new SqlParameter("@SowID", SqlDbType.VarChar, 50);
					arParams[22].Value = "";

					arParams[23] = new SqlParameter("@AgeConditionID", SqlDbType.VarChar, 50);
					arParams[23].Value = "";
				}
				else
				{
					arParams[21] = new SqlParameter("@HorseGender", SqlDbType.VarChar, 50);
					arParams[21].Value = clsScaleofWeight.HorseGender;

					arParams[22] = new SqlParameter("@HorseHandicapWeight", SqlDbType.VarChar, 50);
					arParams[22].Value = clsScaleofWeight.HorseHandicapWeight;

					arParams[23] = new SqlParameter("@AgeConditionID", SqlDbType.VarChar, 50);
					arParams[23].Value = clsScaleofWeight.AgeCondition;
				}

				status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_InsertUpdateScaleOfWeight", arParams));
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
            return status;
        }


        /// <summary>
        /// This is basically use for test purpose no need to add code on the basis of this method (Not usable code)
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="PageFieldName"></param>
        /// <param name="PageFieldAlias"></param>
        /// <param name="UserID_FK"></param>
        /// <returns></returns>
        public int InsertPopUp(string Name, string zipcode)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@name", SqlDbType.VarChar, 100);
                arParams[0].Value = Name;

                arParams[1] = new SqlParameter("@zip", SqlDbType.VarChar, 100);
                arParams[1].Value = zipcode;

                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_InsertTestDate", arParams));

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
            return status;
        }

        /// <summary>
        /// test get data not use it for other code
        /// </summary>
        /// <returns></returns>
        public DataTable GetTestData()
        {
            DataTable dt = null; ;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                arParams[0] = new SqlParameter("@value", SqlDbType.Int);
                arParams[0].Value = 4;
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetTestData", arParams);
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

        ///Added by Nitika ProspectusMaster

        /// <summary>
        /// GetDataCentre
        /// <returns>DataTable value</returns>
        public DataTable GetProspectusCentre()
        {

            DataTable dt = null;
            try
            {
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "GetCenterDetails");
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

        /// <summary>
        /// GetDataSeason
        /// <returns>DataTable value</returns>
        public DataTable GetProspectusSeason()
        {

            DataTable dt = null;
            try
            {
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "GetSeasonDetails");
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
        /// <summary>
        /// RaceInMemory
        /// </summary>
        /// <param name="MName"></param>
        /// <param name="MType"></param>
        /// <param name="MProfile"></param>
        /// <param name="MRelatedTo"></param>
        /// <param name="MRelatedType"></param>
        /// <param name="MOther"></param>
        /// <param name="MComment"></param>
        /// <param name="Active"></param>
        /// <param name="UserID_FK"></param>
        /// <param name="show"></param>
        /// <returns></returns>
        //public int InsertProspecusMasterRaceInMemory(string MName, string MType, string MProfile, string MRelatedTo, string MRelatedType, string MOther, string MComment, int Active, int UserID_FK, int show, string unique)
        //{
        //    int status = 0;
        //    SqlParameter[] arParams = new SqlParameter[11];
        //    try
        //    {
        //        arParams[0] = new SqlParameter("@MName", SqlDbType.VarChar, 100);
        //        arParams[0].Value = MName.ToString();
        //        arParams[1] = new SqlParameter("@MType", SqlDbType.VarChar, 100);
        //        arParams[1].Value = MType.ToString();
        //        arParams[2] = new SqlParameter("@MProfile", SqlDbType.VarChar, 100);
        //        arParams[2].Value = MProfile.ToString();
        //        arParams[3] = new SqlParameter("@MRelatedTo", SqlDbType.VarChar, 100);
        //        arParams[3].Value = MRelatedTo.ToString();
        //        arParams[4] = new SqlParameter("@MRelatedType", SqlDbType.VarChar, 100);
        //        arParams[4].Value = MRelatedType.ToString();
        //        arParams[5] = new SqlParameter("@MOther", SqlDbType.VarChar, 500);
        //        arParams[5].Value = MOther.ToString();
        //        arParams[6] = new SqlParameter("@MComment", SqlDbType.VarChar, 1000);
        //        arParams[6].Value = MComment.ToString();
        //        arParams[7] = new SqlParameter("@Active", SqlDbType.Int);
        //        arParams[7].Value = Active;
        //        arParams[8] = new SqlParameter("@UserID_FK", SqlDbType.Int);
        //        arParams[8].Value = UserID_FK;
        //        arParams[9] = new SqlParameter("@show", SqlDbType.Int);
        //        arParams[9].Value = show;
        //        arParams[10] = new SqlParameter("@Unique", SqlDbType.VarChar, 200);
        //        arParams[10].Value = unique.ToString();
        //        status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_InsertProsMasterRaceInMemory", arParams));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (_conn.State == ConnectionState.Open)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //    return status;
        //}

        //public DataTable GetProsMasterRaceInMemory()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetProsMasterRaceInMemory");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (_conn.State == ConnectionState.Open)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //    return dt;

        //}

        //public int UpdateProspecusMasterRaceInMemory(string MName, string MType, string MProfile, string MRelatedTo, string MRelatedType, string MOther, string MComment, int UserID_FK, string Pname, int MID, int show)
        //{
        //    int status = 0;
        //    SqlParameter[] arParams = new SqlParameter[11];
        //    try
        //    {
        //        arParams[0] = new SqlParameter("@MName", SqlDbType.VarChar, 100);
        //        arParams[0].Value = MName.ToString();
        //        arParams[1] = new SqlParameter("@MType", SqlDbType.VarChar, 100);
        //        arParams[1].Value = MType.ToString();
        //        arParams[2] = new SqlParameter("@MProfile", SqlDbType.VarChar, 100);
        //        arParams[2].Value = MProfile.ToString();
        //        arParams[3] = new SqlParameter("@MRelatedTo", SqlDbType.VarChar, 100);
        //        arParams[3].Value = MRelatedTo.ToString();
        //        arParams[4] = new SqlParameter("@MRelationType", SqlDbType.VarChar, 100);
        //        arParams[4].Value = MRelatedType.ToString();
        //        arParams[5] = new SqlParameter("@MOther", SqlDbType.VarChar, 500);
        //        arParams[5].Value = MOther.ToString();
        //        arParams[6] = new SqlParameter("@MComment", SqlDbType.VarChar, 1000);
        //        arParams[6].Value = MComment.ToString();
        //        arParams[7] = new SqlParameter("@UserID_FK", SqlDbType.Int);
        //        arParams[7].Value = UserID_FK;
        //        arParams[8] = new SqlParameter("@MID", SqlDbType.Int);
        //        arParams[8].Value = MID;
        //        arParams[9] = new SqlParameter("@show", SqlDbType.Int);
        //        arParams[9].Value = show;
        //        arParams[10] = new SqlParameter("@Action", SqlDbType.VarChar, 50);
        //        arParams[10].Value = Pname.ToString();

        //        status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UpdateProsMasterRaceInMemory", arParams));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (_conn.State == ConnectionState.Open)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //    return status;
        //}
        //public DataTable GetFinRaceInMemory()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetFinalRaceInMemory");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (_conn.State == ConnectionState.Open)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //    return dt;

        //}

        /// <summary>
        /// AddRaceName
        /// </summary>
        /// <returns></returns>
        public DataTable GetPropspectusAddRaceName()
        {
            DataTable dt = null;
            try
            {
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetPropspectusAddRaceName");
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


        ///<summary>
        ///OtherDDLValues
        ///</summary>
        public DataTable GetProspectusValues(string col)
        {
            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                arParams[0] = new SqlParameter("@ColValue", SqlDbType.VarChar, 100);
                arParams[0].Value = col.ToString();

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetProspectusMasterValues", arParams);
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


        public string GetProsMasterID(string Unique)
        {
            string pid = null;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                arParams[0] = new SqlParameter("@Unique", SqlDbType.VarChar, 200);
                arParams[0].Value = Unique.ToString();

                pid = Convert.ToString(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_GetProsMasterID", arParams));
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
            return pid;

        }


        public DataTable SubmitMomentName()
        {
            DataTable dt = null;
            try
            {
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_SubmitMoment");
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

        /// <summary>
        /// Insert Excel data in the database
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UploadExcelRecordBulk(DataTable dt, string PageName)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                DataTable dtCenter = new DataTable("Center");
                DataColumn CenterName = new DataColumn();
                CenterName.ColumnName = "CenterName";
                CenterName.DataType = System.Type.GetType("System.String");
                dtCenter.Columns.Add(CenterName);


                DataColumn CenterAlias = new DataColumn();
                CenterAlias.ColumnName = "CenterAlias";
                CenterAlias.DataType = System.Type.GetType("System.String");
                dtCenter.Columns.Add(CenterAlias);

                DataColumn CenterOldName = new DataColumn();
                CenterOldName.ColumnName = "CenterOldName";
                CenterOldName.DataType = System.Type.GetType("System.String");
                dtCenter.Columns.Add(CenterOldName);

                DataColumn CenterOldNameAlias = new DataColumn();
                CenterOldNameAlias.ColumnName = "CenterOldNameAlias";
                CenterOldNameAlias.DataType = System.Type.GetType("System.String");
                dtCenter.Columns.Add(CenterOldNameAlias);

                if (PageName == "Center")
                {
                    DataColumn dcDate = new DataColumn();
                    dcDate.ColumnName = "DateOfNameChange";
                    dcDate.DataType = System.Type.GetType("System.String");
                    dtCenter.Columns.Add(dcDate);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dtCenter.NewRow();
                        dr[0] = dt.Rows[i].ItemArray[0].ToString();
                        dr[1] = dt.Rows[i].ItemArray[1].ToString();
                        dr[2] = dt.Rows[i].ItemArray[2].ToString();
                        dr[3] = dt.Rows[i].ItemArray[3].ToString();
                        dr[4] = dt.Rows[i].ItemArray[4].ToString();
                        dtCenter.Rows.Add(dr);
                    }
                    int count = 0;
                    string d = "";
                    foreach (DataRow dr in dtCenter.Rows)
                    {

                        d = dr["DateOfNameChange"].ToString();
                        if (d != "")
                        {
                            string[] dateString = d.Split('-');
                            string[] dateyear = dateString[2].Split(':');
                            string[] dateyearSubBreak = dateyear[0].Split(' ');
                            DateTime enter_date = Convert.ToDateTime(dateyearSubBreak[0] + "-" + dateString[1] + "-" + dateString[0]);
                            dtCenter.Rows[count]["DateOfNameChange"] = enter_date.ToString("yyyy-MM-dd 00:00:00");
                            //string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                            //dtCenter.Rows[count]["DateOfNameChange"] = dtformat;
                        }
                        count++;
                    }


                    _conn.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                    {
                        copy.ColumnMappings.Add(0, 0);
                        copy.ColumnMappings.Add(1, 1);
                        copy.ColumnMappings.Add(2, 2);
                        copy.ColumnMappings.Add(3, 3);
                        copy.ColumnMappings.Add(4, 4);
                        copy.DestinationTableName = "dbo.Temp_ExcelUpload";
                        try
                        {
                            copy.WriteToServer(dtCenter);
                        }
                        catch (Exception ex)
                        {
                            ErrorHandling.CheckEachSteps(ex.StackTrace);
                            ErrorHandling.SendErrorToText(ex);
                            throw;
                        }
                    }

                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "Center";

                    status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UploadExcelData", arParams));

                    _conn.Close();
                }
                else
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr1 = dtCenter.NewRow();
                        dr1[0] = dt.Rows[i].ItemArray[0].ToString();
                        dr1[1] = dt.Rows[i].ItemArray[1].ToString();
                        dr1[2] = dt.Rows[i].ItemArray[2].ToString();
                        dr1[3] = dt.Rows[i].ItemArray[3].ToString();
                        dtCenter.Rows.Add(dr1);
                    }
                    _conn.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                    {
                        copy.ColumnMappings.Add(0, 0);
                        copy.ColumnMappings.Add(1, 1);
                        copy.ColumnMappings.Add(2, 2);
                        copy.ColumnMappings.Add(3, 3);
                        copy.DestinationTableName = "dbo.Temp_ExcelUpload";
                        try
                        {
                            copy.WriteToServer(dtCenter);
                        }
                        catch (Exception ex)
                        {
                            ErrorHandling.CheckEachSteps(ex.StackTrace);
                            ErrorHandling.SendErrorToText(ex);
                            throw;
                        }
                    }

                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "AgeCondition";

                    status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UploadExcelData", arParams));
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_RemoveRowFromTable");
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return status;
        }

        /// <summary>
        /// Upload year detail
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UploadExcelRecordBulkYear(DataTable dt)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                DataTable dtCenter = new DataTable("Year");
                DataColumn CenterName = new DataColumn();
                CenterName.ColumnName = "YearName";
                CenterName.DataType = System.Type.GetType("System.String");
                dtCenter.Columns.Add(CenterName);


                DataColumn CenterAlias = new DataColumn();
                CenterAlias.ColumnName = "YearAlias";
                CenterAlias.DataType = System.Type.GetType("System.String");
                dtCenter.Columns.Add(CenterAlias);


                DataColumn dcDate = new DataColumn();
                dcDate.ColumnName = "YearStartDate";
                dcDate.DataType = System.Type.GetType("System.String");
                dtCenter.Columns.Add(dcDate);

                DataColumn dcEndDate = new DataColumn();
                dcEndDate.ColumnName = "YearEndDate";
                dcEndDate.DataType = System.Type.GetType("System.String");
                dtCenter.Columns.Add(dcEndDate);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr1 = dtCenter.NewRow();
                    dr1[0] = dt.Rows[i].ItemArray[0].ToString();
                    dr1[1] = dt.Rows[i].ItemArray[1].ToString();
                    dr1[2] = dt.Rows[i].ItemArray[2].ToString();
                    dr1[3] = dt.Rows[i].ItemArray[3].ToString();
                    //dr1[4] = dt.Rows[i].ItemArray[4].ToString();
                    dtCenter.Rows.Add(dr1);
                }
                int count = 0;
                string d = "";
                string d1 = "";
                foreach (DataRow dr in dtCenter.Rows)
                {

                    d = dr["YearStartDate"].ToString();
                    if (d != "")
                    {
                        string[] dateString = d.Split('-');
                        string[] dateyear = dateString[2].Split(':');
                        string[] dateyearSubBreak = dateyear[0].Split(' ');
                        DateTime enter_date = Convert.ToDateTime(dateyearSubBreak[0] + "-" + dateString[1] + "-" + dateString[0]);

                        string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                        dtCenter.Rows[count]["YearStartDate"] = dtformat;
                    }

                    d1 = dr["YearEndDate"].ToString();
                    if (d1 != "")
                    {
                        string[] dateString = d1.Split('-');
                        string[] dateyear = dateString[2].Split(':');
                        string[] dateyearSubBreak = dateyear[0].Split(' ');
                        DateTime enter_date = Convert.ToDateTime(dateyearSubBreak[0] + "-" + dateString[1] + "-" + dateString[0]);
                        string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                        dtCenter.Rows[count]["YearEndDate"] = dtformat;
                    }
                    //d = dr["DateOfNameChange"].ToString();
                    //if (d != "")
                    //{
                    //    string[] dateString = d.Split('-');
                    //    string[] dateyear = dateString[2].Split(':');
                    //    string[] dateyearSubBreak = dateyear[0].Split(' ');
                    //    DateTime enter_date = Convert.ToDateTime(dateyearSubBreak[0] + "-" + dateString[1] + "-" + dateString[0]);
                    //    string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //    dtCenter.Rows[count]["DateOfNameChange"] = dtformat;
                    //}
                    count++;
                }


                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    copy.ColumnMappings.Add(0, 0);
                    copy.ColumnMappings.Add(1, 1);
                    copy.ColumnMappings.Add(2, 2);
                    copy.ColumnMappings.Add(3, 3);
                    //  copy.ColumnMappings.Add(4, 4);
                    copy.DestinationTableName = "dbo.Temp_ExcelUpload";
                    try
                    {
                        copy.WriteToServer(dtCenter);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandling.CheckEachSteps(ex.StackTrace);
                        ErrorHandling.SendErrorToText(ex);
                        throw;
                    }
                }

                arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = "Year";

                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UploadExcelData", arParams));
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_RemoveRowFromTable");
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return status;
        }

        /// <summary>
        /// SEason Excel sheet data add in database
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UploadExcelRecordBulkSeason(DataTable dt)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                DataTable dtcommon = new DataTable("Season");
                DataColumn Name = new DataColumn();
                Name.ColumnName = "SeasonName";
                Name.DataType = System.Type.GetType("System.String");
                dtcommon.Columns.Add(Name);


                DataColumn Alias = new DataColumn();
                Alias.ColumnName = "SeasonAlias";
                Alias.DataType = System.Type.GetType("System.String");
                dtcommon.Columns.Add(Alias);

                //DataColumn SubName = new DataColumn();
                //SubName.ColumnName = "SubSeason";
                //SubName.DataType = System.Type.GetType("System.String");
                //dtcommon.Columns.Add(SubName);


                //DataColumn SubAlias = new DataColumn();
                //SubAlias.ColumnName = "SubSeasonAlias";
                //SubAlias.DataType = System.Type.GetType("System.String");
                //dtcommon.Columns.Add(SubAlias);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr1 = dtcommon.NewRow();
                    dr1[0] = dt.Rows[i].ItemArray[0].ToString();
                    dr1[1] = dt.Rows[i].ItemArray[1].ToString();
                    //dr1[2] = dt.Rows[i].ItemArray[2].ToString();
                    //dr1[3] = dt.Rows[i].ItemArray[3].ToString();
                    dtcommon.Rows.Add(dr1);
                }

                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    copy.ColumnMappings.Add(0, 0);
                    copy.ColumnMappings.Add(1, 1);
                    //copy.ColumnMappings.Add(2, 2);
                    //copy.ColumnMappings.Add(3, 3);
                    //  copy.ColumnMappings.Add(4, 4);
                    copy.DestinationTableName = "dbo.Temp_ExcelUpload";
                    try
                    {
                        copy.WriteToServer(dtcommon);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandling.CheckEachSteps(ex.StackTrace);
                        ErrorHandling.SendErrorToText(ex);
                        throw;
                    }
                }

                arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = "Season";

                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UploadExcelData", arParams));
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
            return status;
        }


        /// <summary>
        /// Upload Season Description details
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UploadExcelRecordBulkSeasonDescription(DataTable dt)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                DataTable dtGlobal = new DataTable("SeasonDescription");

                DataColumn CenterName = new DataColumn();
                CenterName.ColumnName = "CenterID";
                CenterName.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(CenterName);


                DataColumn YearID = new DataColumn();
                YearID.ColumnName = "YearID";
                YearID.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(YearID);

                DataColumn SeasonID = new DataColumn();
                SeasonID.ColumnName = "SeasonID";
                SeasonID.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(SeasonID);


                DataColumn dcDate = new DataColumn();
                dcDate.ColumnName = "SeasonStartDt";
                dcDate.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(dcDate);

                DataColumn dcEndDate = new DataColumn();
                dcEndDate.ColumnName = "SeasonEndDt";
                dcEndDate.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(dcEndDate);

                DataColumn SubSeason = new DataColumn();
                SubSeason.ColumnName = "SubSeason";
                SubSeason.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(SubSeason);

                DataColumn SSStartDate = new DataColumn();
                SSStartDate.ColumnName = "SubSeasonStartDt";
                SSStartDate.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(SSStartDate);

                DataColumn SSEStartDate = new DataColumn();
                SSEStartDate.ColumnName = "SubSeasonEndDt";
                SSEStartDate.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(SSEStartDate);

                DataColumn SeasonRNumberChange = new DataColumn();
                SeasonRNumberChange.ColumnName = "SeasonRNumberChange";
                SeasonRNumberChange.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(SeasonRNumberChange);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr1 = dtGlobal.NewRow();
                    dr1[0] = dt.Rows[i].ItemArray[0].ToString();
                    dr1[1] = dt.Rows[i].ItemArray[1].ToString();
                    dr1[2] = dt.Rows[i].ItemArray[2].ToString();
                    dr1[3] = dt.Rows[i].ItemArray[3].ToString();
                    dr1[4] = dt.Rows[i].ItemArray[4].ToString();
                    dr1[5] = dt.Rows[i].ItemArray[5].ToString();
                    dr1[6] = dt.Rows[i].ItemArray[6].ToString();
                    dr1[7] = dt.Rows[i].ItemArray[7].ToString();
                    dr1[8] = dt.Rows[i].ItemArray[8].ToString();
                    dtGlobal.Rows.Add(dr1);
                }
                int count = 0;
                string d = "";
                string d1 = "";
                string d2 = "";
                string d3 = "";
                foreach (DataRow dr in dtGlobal.Rows)
                {

                    d = dr["SeasonStartDt"].ToString();
                    if (d != "")
                    {
                        string[] dateString = d.Split('-');
                        string[] dateyear = dateString[2].Split(':');
                        string[] dateyearSubBreak = dateyear[0].Split(' ');
                        DateTime enter_date = Convert.ToDateTime(dateyearSubBreak[0] + "-" + dateString[1] + "-" + dateString[0]);
                        string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                        dtGlobal.Rows[count]["SeasonStartDt"] = dtformat;
                    }

                    d1 = dr["SeasonEndDt"].ToString();
                    if (d1 != "")
                    {
                        string[] dateString = d1.Split('-');
                        string[] dateyear = dateString[2].Split(':');
                        string[] dateyearSubBreak = dateyear[0].Split(' ');
                        DateTime enter_date = Convert.ToDateTime(dateyearSubBreak[0] + "-" + dateString[1] + "-" + dateString[0]);
                        string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                        dtGlobal.Rows[count]["SeasonEndDt"] = dtformat;
                    }

                    d2 = dr["SubSeasonStartDt"].ToString();
                    if (d2 != "")
                    {
                        string[] dateString = d2.Split('-');
                        if (dateString.Count() > 1)
                        {
                            string[] dateyear = dateString[2].Split(':');
                            string[] dateyearSubBreak = dateyear[0].Split(' ');
                            DateTime enter_date = Convert.ToDateTime(dateyearSubBreak[0] + "-" + dateString[1] + "-" + dateString[0]);
                            string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                            dtGlobal.Rows[count]["SubSeasonStartDt"] = dtformat;
                        }
                        else
                        {
                            string[] dateString1 = d2.Split('-');

                            if (dateString1[2].Contains(":"))
                            {
                                string[] dateyear = dateString1[2].Split(':');
                                DateTime enter_date = Convert.ToDateTime(dateyear[0] + "-" + dateString1[1] + "-" + dateString1[0]);
                                string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                                dtGlobal.Rows[count]["SubSeasonStartDt"] = dtformat;
                            }
                            else
                            {
                                DateTime enter_date = Convert.ToDateTime(dateString1[2] + "-" + dateString1[1] + "-" + dateString1[0]);
                                string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                                dtGlobal.Rows[count]["SubSeasonStartDt"] = dtformat;
                            }
                            //string[] dateyearSubBreak = dateyear[0].Split(' ');

                        }




                    }

                    d3 = dr["SubSeasonEndDt"].ToString();
                    if (d3 != "")
                    {
                        //string[] dateString = d3.Split('-');
                        //string[] dateyear = dateString[2].Split(':');
                        //string[] dateyearSubBreak = dateyear[0].Split(' ');
                        //DateTime enter_date = Convert.ToDateTime(dateyearSubBreak[0] + "-" + dateString[1] + "-" + dateString[0]);
                        //string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                        //dtGlobal.Rows[count]["SubSeasonEndDt"] = dtformat;
                        string[] dateString = d3.Split('-');
                        if (dateString.Count() > 1)
                        {
                            string[] dateyear = dateString[2].Split(':');
                            string[] dateyearSubBreak = dateyear[0].Split(' ');
                            DateTime enter_date = Convert.ToDateTime(dateyearSubBreak[0] + "-" + dateString[1] + "-" + dateString[0]);
                            string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                            dtGlobal.Rows[count]["SubSeasonEndDt"] = dtformat;
                        }
                        else
                        {
                            string[] dateString1 = d3.Split('-');

                            if (dateString1[2].Contains(":"))
                            {
                                string[] dateyear = dateString1[2].Split(':');
                                DateTime enter_date = Convert.ToDateTime(dateyear[0] + "-" + dateString1[1] + "-" + dateString1[0]);
                                string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                                dtGlobal.Rows[count]["SubSeasonEndDt"] = dtformat;
                            }
                            else
                            {
                                DateTime enter_date = Convert.ToDateTime(dateString1[2] + "-" + dateString1[1] + "-" + dateString1[0]);
                                string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
                                dtGlobal.Rows[count]["SubSeasonEndDt"] = dtformat;
                            }
                            //string[] dateyearSubBreak = dateyear[0].Split(' ');

                        }
                    }
                    count++;
                }


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
                    copy.DestinationTableName = "dbo.Temp_ExcelUpload";
                    try
                    {
                        copy.WriteToServer(dtGlobal);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandling.CheckEachSteps(ex.StackTrace);
                        ErrorHandling.SendErrorToText(ex);
                        throw;
                    }
                }

                arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = "SeasonDescription";

                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UploadExcelData", arParams));
            }
            catch (Exception ex)
            {
                status = -1;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
                // throw ex;
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

        /// <summary>
        /// Upload Season Description details
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UploadExcelRecordBulkDistanceGroup(DataTable dt, string PageName)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                DataTable dtGlobal = new DataTable("DistanceGroup");

                DataColumn distancegrp = new DataColumn();
                distancegrp.ColumnName = "DistanceGroup";
                distancegrp.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(distancegrp);


                DataColumn distance = new DataColumn();
                distance.ColumnName = "Distance";
                distance.DataType = System.Type.GetType("System.String");
                dtGlobal.Columns.Add(distance);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr1 = dtGlobal.NewRow();
                    dr1[0] = dt.Rows[i].ItemArray[0].ToString();
                    dr1[1] = dt.Rows[i].ItemArray[1].ToString();
                    dtGlobal.Rows.Add(dr1);
                }
                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    copy.ColumnMappings.Add(0, 0);
                    copy.ColumnMappings.Add(1, 1);
                    copy.DestinationTableName = "dbo.Temp_ExcelUpload";
                    try
                    {
                        copy.WriteToServer(dtGlobal);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandling.CheckEachSteps(ex.StackTrace);
                        ErrorHandling.SendErrorToText(ex);
                        throw;
                    }
                }
                if (PageName.Equals("DistanceGroup"))
                {
                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "DistanceGroup";
                }
                else if (PageName.Equals("Rating"))
                {
                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "Rating";
                }
                else
                {
                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "Equipment";
                }


                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UploadExcelData", arParams));
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
            return status;
        }


        // <summary>
        /// Distance Excel sheet data add in database
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UploadExcelRecordBulkDistance(DataTable dt, string PageName)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                DataTable dtcommon = new DataTable("Distance");
                DataColumn Name = new DataColumn();
                Name.ColumnName = "Distance";
                Name.DataType = System.Type.GetType("System.String");
                dtcommon.Columns.Add(Name);


                DataColumn Alias = new DataColumn();
                Alias.ColumnName = "Alias";
                Alias.DataType = System.Type.GetType("System.String");
                dtcommon.Columns.Add(Alias);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr1 = dtcommon.NewRow();
                    dr1[0] = dt.Rows[i].ItemArray[0].ToString();
                    dr1[1] = dt.Rows[i].ItemArray[1].ToString();
                    dtcommon.Rows.Add(dr1);
                }

                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    copy.ColumnMappings.Add(0, 0);
                    copy.ColumnMappings.Add(1, 1);
                    copy.DestinationTableName = "dbo.Temp_ExcelUpload";
                    try
                    {
                        copy.WriteToServer(dtcommon);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandling.CheckEachSteps(ex.StackTrace);
                        ErrorHandling.SendErrorToText(ex);
                        throw;
                    }
                }

                if (PageName.Equals("Distance"))
                {
                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "Distance";
                }
                else if (PageName.Equals("Bit"))
                {
                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "Bit";
                }
                else if (PageName.Equals("Equipment"))
                {
                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "Equipment";
                }
                else if (PageName.Equals("Shoe"))
                {
                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "Shoe";
                }
                else
                {
                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "Nation";
                }


                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UploadExcelData", arParams));
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
            return status;
        }

        /// <summary>
        /// Hotliner
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UploadExcelRecordBulkHotliner(DataTable dt, string PageName)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                DataTable dtcommon = new DataTable("GlobalTable");

                DataColumn Number = new DataColumn();
                Number.ColumnName = "Number";
                Number.DataType = System.Type.GetType("System.String");
                dtcommon.Columns.Add(Number);

                DataColumn Name = new DataColumn();
                Name.ColumnName = "Name";
                Name.DataType = System.Type.GetType("System.String");
                dtcommon.Columns.Add(Name);


                DataColumn Alias = new DataColumn();
                Alias.ColumnName = "Alias";
                Alias.DataType = System.Type.GetType("System.String");
                dtcommon.Columns.Add(Alias);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr1 = dtcommon.NewRow();
                    dr1[0] = dt.Rows[i].ItemArray[0].ToString();
                    dr1[1] = dt.Rows[i].ItemArray[1].ToString();
                    dr1[2] = dt.Rows[i].ItemArray[2].ToString();
                    dtcommon.Rows.Add(dr1);
                }

                _conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
                {
                    copy.ColumnMappings.Add(0, 0);
                    copy.ColumnMappings.Add(1, 1);
                    copy.ColumnMappings.Add(2, 2);
                    copy.DestinationTableName = "dbo.Temp_ExcelUpload";
                    try
                    {
                        copy.WriteToServer(dtcommon);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandling.CheckEachSteps(ex.StackTrace);
                        ErrorHandling.SendErrorToText(ex);
                        throw;
                    }
                }

                if (PageName.Equals("Hotliner"))
                {
                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "Hotliner";
                }
                else if (PageName.Equals("Selector"))
                {
                    arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                    arParams[0].Value = "Selector";
                }

                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UploadExcelData", arParams));
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
            return status;
        }


        /// <summary>
        /// Upload NB Condition File
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UploadNBCondition(DataTable dt)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[1];
            //try
            //{
            DataTable dtGlobal = new DataTable("NBCondition");

            DataColumn CenterName = new DataColumn();
            CenterName.ColumnName = "CenterName";
            CenterName.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(CenterName);


            DataColumn FromYearID = new DataColumn();
            FromYearID.ColumnName = "FromYear";
            FromYearID.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(FromYearID);

            DataColumn TillYearID = new DataColumn();
            TillYearID.ColumnName = "TillYear";
            TillYearID.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(TillYearID);


            DataColumn RaceType = new DataColumn();
            RaceType.ColumnName = "RaceType";
            RaceType.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(RaceType);

            DataColumn TopWeight = new DataColumn();
            TopWeight.ColumnName = "TopWeight";
            TopWeight.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(TopWeight);

            DataColumn LowWeight = new DataColumn();
            LowWeight.ColumnName = "LowWeight";
            LowWeight.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(LowWeight);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr1 = dtGlobal.NewRow();
                dr1[0] = dt.Rows[i].ItemArray[0].ToString();
                dr1[1] = dt.Rows[i].ItemArray[1].ToString();
                dr1[2] = dt.Rows[i].ItemArray[2].ToString();
                dr1[3] = dt.Rows[i].ItemArray[3].ToString();
                dr1[4] = dt.Rows[i].ItemArray[4].ToString();
                dr1[5] = dt.Rows[i].ItemArray[5].ToString();
                dtGlobal.Rows.Add(dr1);
            }


            _conn.Open();
            using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
            {
                copy.ColumnMappings.Add(0, 0);
                copy.ColumnMappings.Add(1, 1);
                copy.ColumnMappings.Add(2, 2);
                copy.ColumnMappings.Add(3, 3);
                copy.ColumnMappings.Add(4, 4);
                copy.ColumnMappings.Add(5, 5);
                copy.DestinationTableName = "dbo.Temp_ExcelUpload";
                try
                {
                    copy.WriteToServer(dtGlobal);
                }
                catch (Exception ex)
                {
                    ErrorHandling.CheckEachSteps(ex.StackTrace);
                    ErrorHandling.SendErrorToText(ex);
                    throw;
                }
            }

            arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
            arParams[0].Value = "NBCondition";

            status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UploadExcelData", arParams));

            return status;
        }



        /// <summary>
        /// Upload Shoe Description File
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UploadShoeDescription(DataTable dt)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[1];
            //try
            //{
            DataTable dtGlobal = new DataTable("ShoeDescription");

            DataColumn shoeDes = new DataColumn();
            shoeDes.ColumnName = "ShoeDescription";
            shoeDes.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(shoeDes);


            DataColumn FullForm = new DataColumn();
            FullForm.ColumnName = "FullForm";
            FullForm.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(FullForm);

            DataColumn ShoeAlias = new DataColumn();
            ShoeAlias.ColumnName = "ShoeAlias";
            ShoeAlias.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(ShoeAlias);


            DataColumn frontRightLeg = new DataColumn();
            frontRightLeg.ColumnName = "frontRightLeg";
            frontRightLeg.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(frontRightLeg);

            DataColumn frontLeftLeg = new DataColumn();
            frontLeftLeg.ColumnName = "frontLeftLeg";
            frontLeftLeg.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(frontLeftLeg);

            DataColumn rareRightLeg = new DataColumn();
            rareRightLeg.ColumnName = "rareRightLeg";
            rareRightLeg.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(rareRightLeg);


            DataColumn rareLeftLeg = new DataColumn();
            rareLeftLeg.ColumnName = "rareLeftLeg";
            rareLeftLeg.DataType = System.Type.GetType("System.String");
            dtGlobal.Columns.Add(rareLeftLeg);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr1 = dtGlobal.NewRow();
                dr1[0] = dt.Rows[i].ItemArray[0].ToString();
                dr1[1] = dt.Rows[i].ItemArray[1].ToString();
                dr1[2] = dt.Rows[i].ItemArray[2].ToString();
                dr1[3] = dt.Rows[i].ItemArray[3].ToString();
                dr1[4] = dt.Rows[i].ItemArray[4].ToString();
                dr1[5] = dt.Rows[i].ItemArray[5].ToString();
                dr1[6] = dt.Rows[i].ItemArray[6].ToString();
                dtGlobal.Rows.Add(dr1);
            }


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
                copy.DestinationTableName = "dbo.Temp_ExcelUpload";
                try
                {
                    copy.WriteToServer(dtGlobal);
                }
                catch (Exception ex)
                {
                    ErrorHandling.CheckEachSteps(ex.StackTrace);
                    ErrorHandling.SendErrorToText(ex);
                    throw;
                }
            }

            arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
            arParams[0].Value = "ShoeDescription";

            status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UploadExcelData", arParams));
            return status;
        }


        public DataTable ImportExcel(DataTable dt, string PageName)
        {
            DataTable dtresult;
            SqlParameter[] arParams = new SqlParameter[1];
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

                    copy.DestinationTableName = "dbo.Master_Import";
                    ErrorHandling.CheckEachSteps("DestinationTableName: Step Completed");
                    copy.WriteToServer(dt);
                    ErrorHandling.CheckEachSteps("WriteToServer(Completed)");
                }

                arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                dtresult = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_ImportMASTER", arParams);

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

        public DataTable ImportExcel17(DataTable dt, string PageName)
        {
            DataTable dtresult;
            SqlParameter[] arParams = new SqlParameter[1];
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

                    copy.DestinationTableName = "dbo.Master_Import17";
                    copy.WriteToServer(dt);
                }

                arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                dtresult = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_ImportMASTER", arParams);

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

        public DataTable ImportExcel30(DataTable dt, string PageName)
        {
            DataTable dtresult;
            SqlParameter[] arParams = new SqlParameter[1];
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
                    copy.WriteToServer(dt);
                }

                arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                dtresult = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_ImportMASTER", arParams);

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
                SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_RemoveRowFromTable");
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return dtresult;
        }


        public int VerdictMargin(
           int verdictmarginId,
           string verdictmargin,
           string verdictmarginalias,
           string measurement,
           int userid,
           string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[6];
            try
            {

                arParams[0] = new SqlParameter("@VerdictMarginID", SqlDbType.Int);
                arParams[0].Value = verdictmarginId;

                arParams[1] = new SqlParameter("@VerdictMargin", SqlDbType.VarChar, 100);
                arParams[1].Value = verdictmargin;

                arParams[2] = new SqlParameter("@VerdictMarginAlias", SqlDbType.VarChar, 100);
                arParams[2].Value = verdictmarginalias;

                arParams[3] = new SqlParameter("@Measurement", SqlDbType.VarChar, 100);
                arParams[3].Value = measurement;

                arParams[4] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[4].Value = userid;

                arParams[5] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[5].Value = tasktype;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_MasterVerdictMargin",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[11];
            try
            {

                arParams[0] = new SqlParameter("@DiseaseID", SqlDbType.Int);
                arParams[0].Value = diseaseid;

                arParams[1] = new SqlParameter("@Action", SqlDbType.VarChar, 100);
                arParams[1].Value = tasktype;

                arParams[2] = new SqlParameter("@Disease", SqlDbType.VarChar, 4000);
                arParams[2].Value = disease;

                arParams[3] = new SqlParameter("@DiseaseAlias", SqlDbType.VarChar, 4000);
                arParams[3].Value = alias;

                arParams[4] = new SqlParameter("@Detail", SqlDbType.VarChar, 4000) { Value = diseasedetail };
                arParams[5] = new SqlParameter("@MedicalName", SqlDbType.VarChar, 4000) { Value = medicalname };
                arParams[6] = new SqlParameter("@PerformanceImpact", SqlDbType.VarChar, 4000) { Value = performanceimpact };
                arParams[7] = new SqlParameter("@Treatment", SqlDbType.VarChar, 4000) { Value = treatment };
                arParams[8] = new SqlParameter("@Precautions", SqlDbType.VarChar, 4000) { Value = precautions };
                arParams[9] = new SqlParameter("@MyComments", SqlDbType.VarChar, 4000) { Value = comments };
                arParams[10] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_InsertUpdateDisease",
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


        public int PermanentCondition(int permanentconditionid, string permanentcondition, int UserID_FK, string tasktype)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@PermanentConditionID", SqlDbType.Int);
                arParams[0].Value = permanentconditionid;

                arParams[1] = new SqlParameter("@PermanentCondition", SqlDbType.VarChar, 500);
                arParams[1].Value = permanentcondition;

                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = UserID_FK;

                arParams[3] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[3].Value = tasktype;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_PermanentCondition", arParams));
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
            return status;
        }


        public int SeasonalCondition(int seasonalconditionid, string seasonalcondition, int UserID_FK, string tasktype)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@SeasonalConditionID", SqlDbType.Int);
                arParams[0].Value = seasonalconditionid;

                arParams[1] = new SqlParameter("@SeasonalCondition", SqlDbType.VarChar, 500);
                arParams[1].Value = seasonalcondition;

                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = UserID_FK;

                arParams[3] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[3].Value = tasktype;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_SeasonalCondition", arParams));
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
            return status;
        }


        public int BunchCondition(int bunchconditionid, string bunchcondition, int UserID_FK, string tasktype, string alias)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[5];
            try
            {
                arParams[0] = new SqlParameter("@BunchConditionID", SqlDbType.Int);
                arParams[0].Value = bunchconditionid;

                arParams[1] = new SqlParameter("@BunchCondition", SqlDbType.VarChar, 500);
                arParams[1].Value = bunchcondition;

                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = UserID_FK;

                arParams[3] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[3].Value = tasktype;

                arParams[4] = new SqlParameter("@Alias", SqlDbType.VarChar, 100) { Value=alias };

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_BunchCondition", arParams));
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
            return status;
        }


        public int RaceCardCondition(int racecardconditionid, string racecardcondition, int UserID_FK, string tasktype)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@RaceCardConditionID", SqlDbType.Int);
                arParams[0].Value = racecardconditionid;

                arParams[1] = new SqlParameter("@RaceCardCondition", SqlDbType.VarChar, 500);
                arParams[1].Value = racecardcondition;

                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = UserID_FK;

                arParams[3] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[3].Value = tasktype;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_RaceCardCondition", arParams));
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
            return status;
        }


        public int HandicapRatingRange(int handicapratingrangeid, string handicapratingrange,int min, int max, int UserID_FK, string tasktype)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[6];
            try
            {
                arParams[0] = new SqlParameter("@HandicapRatingRangeID", SqlDbType.Int);
                arParams[0].Value = handicapratingrangeid;

                arParams[1] = new SqlParameter("@HandicapRatingRange", SqlDbType.VarChar, 500);
                arParams[1].Value = handicapratingrange;

                arParams[2] = new SqlParameter("@MinRating", SqlDbType.Int);
                arParams[2].Value = min;

                arParams[3] = new SqlParameter("@MaxRating", SqlDbType.Int);
                arParams[3].Value = max;

                arParams[4] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[4].Value = UserID_FK;

                arParams[5] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[5].Value = tasktype;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_MasterHandicapRatingRange", arParams));
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
            return status;
        }

        public int MasterReligion(int religionid, string religion, int UserID_FK, string tasktype)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@ReligionID", SqlDbType.Int);
                arParams[0].Value = religionid;

                arParams[1] = new SqlParameter("@Religion", SqlDbType.VarChar, 500);
                arParams[1].Value = religion;

                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = UserID_FK;

                arParams[3] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[3].Value = tasktype;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_MasterReligion", arParams));
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
            return status;
        }


        public int ProfessionalBackground(int professionalbackgroundid, string professionalbackground, int UserID_FK, string tasktype)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@ProfessionalBackgroundID", SqlDbType.Int);
                arParams[0].Value = professionalbackgroundid;

                arParams[1] = new SqlParameter("@ProfessionalBackground", SqlDbType.VarChar, 500);
                arParams[1].Value = professionalbackground;

                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = UserID_FK;

                arParams[3] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[3].Value = tasktype;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_ProfessionalBackground", arParams));
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
            return status;
        }


        public int CurrentForm(int currentformid, string currentform, int UserID_FK, string tasktype)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@CurrentFormID", SqlDbType.Int);
                arParams[0].Value = currentformid;

                arParams[1] = new SqlParameter("@CurrentForm", SqlDbType.VarChar, 500);
                arParams[1].Value = currentform;

                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = UserID_FK;

                arParams[3] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[3].Value = tasktype;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_CurrentForm", arParams));
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
            return status;
        }


        public int HandicapWeightCondition(int handicapwieghtconditionid, string condition, int UserID_FK, string tasktype)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@HandicapWeightConditionID", SqlDbType.Int);
                arParams[0].Value = handicapwieghtconditionid;

                arParams[1] = new SqlParameter("@Condition", SqlDbType.VarChar, 500);
                arParams[1].Value = condition;

                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = UserID_FK;

                arParams[3] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[3].Value = tasktype;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_MasterHandicapWeightCondition", arParams));
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
            return status;
        }


        public int HandicapWeightCriteria(int handicapwieghtriteriaid, string criteria, int UserID_FK, string tasktype)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@HandicapWeightCriteriaID", SqlDbType.Int);
                arParams[0].Value = handicapwieghtriteriaid;

                arParams[1] = new SqlParameter("@Criteria", SqlDbType.VarChar, 500);
                arParams[1].Value = criteria;

                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = UserID_FK;

                arParams[3] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[3].Value = tasktype;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_MasterHandicapWeightCriteria", arParams));
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
            return status;
        }


        public int MasterCenterYearWise(int centerwiseid, int centerid, int yearid, string yearstartdate,string yearenddate, int UserID_FK, string tasktype)
        {

            int status = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {
                arParams[0] = new SqlParameter("@YearCWID", SqlDbType.Int);
                arParams[0].Value = centerwiseid;

                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[1].Value = centerid;

                arParams[2] = new SqlParameter("@YearID", SqlDbType.Int);
                arParams[2].Value = yearid;

                arParams[3] = new SqlParameter("@StartDate", SqlDbType.VarChar,30);
                if (yearstartdate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = yearstartdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00"); ;
                }

                arParams[4] = new SqlParameter("@EndDate", SqlDbType.VarChar,30);
                if (yearenddate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = yearenddate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID_FK;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[6].Value = tasktype;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_MasterYearCenterWise", arParams));
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
            return status;
        }


        public DataTable GetprospectusAutoFiller(string autoFillName, string prefix)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetProspectusNameAutoFill", arParams);
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


        public DataTable ImportMasterFiles(string field1, string field2, string field3, string field4, string field5, string field6, string field7, string field8, string field9,
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

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_MasterCardImport", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }

            return dt;
        }
    }
}
