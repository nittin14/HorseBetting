using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace VKADB
{
    using System.Data.SqlTypes;
    using System.Threading;

    using Microsoft.SqlServer.Server;

    public class MasterHorseDL
    {
        private SqlConnection _conn;

        public MasterHorseDL()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        }

        /// <summary>
        /// Bind dropdown on the basis of the selected value
        /// </summary>
        /// <param name="DropDownName"></param>
        /// <returns></returns>
        public DataTable GetHorseName(string DropDownName)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {

                arParams[0] = new SqlParameter("@DropdownName", SqlDbType.VarChar, 100);
                arParams[0].Value = DropDownName;

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetHorseName", arParams);
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

        public DataTable GetHorseNameAutoFiller(string autoFillName, string prefix)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetHorseNameAutoFill", arParams);
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


        public DataTable GetHorseNameAutoFillAcceptance(string autoFillName, string prefix, string othervalue)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {

                arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
                arParams[2] = new SqlParameter("@OtherValue", SqlDbType.VarChar, 100) { Value = othervalue };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetAcceptanceHorseDetail", arParams);
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


        public DataTable GetHorseNameAutoFillAcceptanceCard(string autoFillName, string prefix, string othervalue)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {

                arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
                arParams[2] = new SqlParameter("@OtherValue", SqlDbType.VarChar, 100) { Value = othervalue };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetAcceptanceHorseDetailCard", arParams);
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
        /// Get Multiple value for show popup
        /// </summary>
        /// <param name="DropDownName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetHorseName(string DropDownName, int value)
        {

            DataTable dt = null;
            ;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@DropdownName", SqlDbType.VarChar, 100);
                arParams[0].Value = DropDownName;

                arParams[1] = new SqlParameter("@Value", SqlDbType.Int);
                arParams[1].Value = value;

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetHorseName", arParams);
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
        /// Get HorseID of the horse
        /// </summary>
        /// <param name="horseName"></param>
        /// <param name="dob"></param>
        /// <redataturdatatns></returns>
        public DataTable GetHorseId(string horseName, string dob)
        {
            int horseId = 0;
            DataTable dt;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@HorseName", SqlDbType.VarChar, 100) { Value = horseName };
                arParams[1] = new SqlParameter("@HorseDOB", SqlDbType.VarChar, 30);
                if (dob.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dob.Split('-');
                    DateTime enterDate =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enterDate.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }

                dt = SqlHelper.ExecuteDataTable(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetHorseID",
                    arParams);

                //horseId =
                //    Convert.ToInt32(
                //        SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_GetHorseID", arParams));
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


        public DataSet GetExport(int horseid, string taskType)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParams[0].Value = horseid;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = taskType;
                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetExport",
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="horseId"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataSet GetHorseNameWithCombination(int horseId, string taskType)
        {
            DataSet ds = null;
            ;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = taskType;
                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetHorseNameWithCombination",
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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="horseId"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataSet GetHorseTillDateValidation(int horseId, string taskType, string fromdate, string actiontype)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = taskType;
                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromdate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromdate.Split('-');
                    DateTime enterDate =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enterDate.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }
                arParams[3] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value=actiontype };
                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetHorseTillDateValidation",
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="horseId"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataSet GetHorseTillDateValidationMultiple(int horseId, string taskType, string fromdate, string actiontype, string value1, string value2)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[6];
            try
            {
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = taskType;
                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromdate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromdate.Split('-');
                    DateTime enterDate =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enterDate.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }
                arParams[3] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = actiontype };
                arParams[4] = new SqlParameter("@Value1", SqlDbType.VarChar, 100) { Value = value1 };
                arParams[5] = new SqlParameter("@Value2", SqlDbType.VarChar, 100) { Value = value2 };
                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetHorseTillDateValidationMultiple",
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
        
        public DataSet GetHorseMultipleRecords(string horseId, string taskType, String RecordStatus)
        {
            DataSet ds = null;
            ;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.VarChar, 50) { Value = horseId };
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100) { Value = taskType };
                arParams[2] = new SqlParameter("@RecordStatus", SqlDbType.VarChar, 50) { Value = taskType };
                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_GetHorseMultipleIDs", arParams);
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


        /// <summary>
        /// This is use for bind the Gridview just pass the unique ID and Value.
        /// </summary>
        /// <param name="UniqueID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetHorseDetail(string UniqueID, string value)
        {

            DataTable dt = null;
            ;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@UniqueID", SqlDbType.VarChar, 50);
                arParams[0].Value = UniqueID;

                arParams[1] = new SqlParameter("@Value", SqlDbType.VarChar, 50);
                arParams[1].Value = value;
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetHorseDetail", arParams);
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

        public DataTable HorseName(
            int horseId,
            string horseName,
            string horseNameAlias,
            string horseNameShortAlias,
            string dob,
            string myComments,
            int userId,
            string taskType,
            string profiletype)
        {
            // int checkRecord = 0;
            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HorseName", SqlDbType.VarChar, 500);
                arParams[1].Value = horseName;

                arParams[2] = new SqlParameter("@HorseNameAlias", SqlDbType.VarChar, 100);
                arParams[2].Value = horseNameAlias;

                arParams[3] = new SqlParameter("@HorseNameShortAlias", SqlDbType.VarChar, 100);
                arParams[3].Value = horseNameShortAlias;

                arParams[4] = new SqlParameter("@HorseDOB", SqlDbType.VarChar, 30);
                if (dob.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dob.Split('-');
                    DateTime enter_date =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = myComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userId;

                arParams[7] = new SqlParameter("@Tasktype", SqlDbType.VarChar, 50);
                arParams[7].Value = taskType;

                arParams[8] = new SqlParameter("@ProfileType", SqlDbType.VarChar, 50);
                arParams[8].Value = profiletype;

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_HorseName", arParams);

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


        public DataTable HorseNewName(
            int horseId,
            string horseName,
            string horseNameAlias,
            string horseNameShortAlias,
            string dob,
            string myComments,
            int userId,
            string taskType)
        {
            // int checkRecord = 0;
            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HorseName", SqlDbType.VarChar, 100);
                arParams[1].Value = horseName;

                arParams[2] = new SqlParameter("@HorseNameAlias", SqlDbType.VarChar, 100);
                arParams[2].Value = horseNameAlias;

                arParams[3] = new SqlParameter("@HorseNameShortAlias", SqlDbType.VarChar, 100);
                arParams[3].Value = horseNameShortAlias;

                arParams[4] = new SqlParameter("@DateofNameChange", SqlDbType.VarChar, 30);
                if (dob.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dob.Split('-');
                    DateTime enter_date =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);

                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = myComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userId;

                arParams[7] = new SqlParameter("@Tasktype", SqlDbType.VarChar, 50);
                arParams[7].Value = taskType;
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_HorseNewName", arParams);

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
        /// update Horse New name after name change
        /// </summary>
        /// <param name="HorseNewName"></param>
        /// <param name="HorseNewAlias"></param>
        /// <param name="HorseNewShortAlias"></param>
        /// <param name="DateofStatusChange"></param>
        /// <param name="MyComments"></param>
        /// <param name="UserID"></param>
        /// <param name="ValueID"></param>
        /// <returns></returns>
        //public DataTable UpdateHorseDetail(int HorseID, string HorseName, string ShortName, string Alias, string reName, string reNameShort, string reAlias, string DOB, string DateofStatusChange, string MyComments, int UserID, string Tasktype)
        //{

        //    DataTable dt = null;
        //    //int checkRecord = 0;
        //    SqlParameter[] arParams = new SqlParameter[12];
        //    try
        //    {

        //        arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
        //        arParams[0].Value = HorseID;

        //        arParams[1] = new SqlParameter("@Name", SqlDbType.VarChar, 100);
        //        arParams[1].Value = HorseName;

        //        arParams[2] = new SqlParameter("@ShortName", SqlDbType.VarChar, 100);
        //        arParams[2].Value = ShortName;

        //        arParams[3] = new SqlParameter("@Alias", SqlDbType.VarChar, 50);
        //        arParams[3].Value = Alias;

        //        arParams[4] = new SqlParameter("@Rename", SqlDbType.VarChar, 100);
        //        arParams[4].Value = reName;

        //        arParams[5] = new SqlParameter("@RenameShortName", SqlDbType.VarChar, 100);
        //        arParams[5].Value = reNameShort;

        //        arParams[6] = new SqlParameter("@RenameAlias", SqlDbType.VarChar, 50);
        //        arParams[6].Value = reAlias;


        //        arParams[7] = new SqlParameter("@Dob", SqlDbType.VarChar, 30);
        //        if (DOB.Equals("__-__-____"))
        //        {
        //            arParams[7].Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            string[] dateString = DOB.Split('-');
        //            DateTime enter_date = Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
        //            string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
        //            arParams[7].Value = Convert.ToDateTime(dtformat);


        //        }


        //        arParams[8] = new SqlParameter("@DateofNameChange", SqlDbType.VarChar, 30);
        //        if (DateofStatusChange.Equals("__-__-____"))
        //        {
        //            arParams[8].Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            string[] dateString = DateofStatusChange.Split('-');
        //            DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
        //            string dtformat = enter_date.ToString("yyyy-MM-dd 00:00:00");
        //            arParams[8].Value = Convert.ToDateTime(dtformat);


        //        }

        //        arParams[9] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
        //        arParams[9].Value = MyComments;

        //        arParams[10] = new SqlParameter("@UserID", SqlDbType.Int);
        //        arParams[10].Value = UserID;

        //        arParams[11] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
        //        arParams[11].Value = Tasktype;

        //        dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_UpdateHorseName", arParams);
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
        /// 
        /// </summary>
        /// <param name="HorseID"></param>
        /// <param name="isCheckBox"></param>
        /// <param name="Status"></param>
        /// <param name="FromDate"></param>
        /// <param name="TillDate"></param>
        /// <param name="MyComments"></param>
        /// <param name="UserID"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public int InsertHorseStatus(
            int HorseID,
            int isCheckBox,
            int Status,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = HorseID;

                arParams[1] = new SqlParameter("@IsShow", SqlDbType.Int);
                arParams[1].Value = 1;

                arParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                arParams[2].Value = Status;

                arParams[3] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = MyComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = UserID;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_InsertUpdateHorseStatus",
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

        /// <summary>
        /// Return Complete Main Horse form Information
        /// </summary>
        /// <param name="HorseID"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataSet GetHorseCompleteInformation(int HorseID, string TaskType)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = HorseID;

                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = TaskType;

                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetHorseCompleteInformation",
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


        /// <summary>
        /// Insert,update and Delete for Horsepopup (CurrentMission)
        /// </summary>
        /// <param name="HorseID"></param>
        /// <param name="isCheckBox"></param>
        /// <param name="currentMission"></param>
        /// <param name="FromDate"></param>
        /// <param name="TillDate"></param>
        /// <param name="MyComments"></param>
        /// <param name="UserID"></param>
        /// <param name="TaskType"></param>
        public int CurrentMission(
            int HorseID,
            int isCheckBox,
            string currentMission,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {
            var status = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = HorseID;

                arParams[1] = new SqlParameter("@IsShow", SqlDbType.Int);
                arParams[1].Value = isCheckBox;

                arParams[2] = new SqlParameter("@CurrentMission", SqlDbType.VarChar, 50);
                arParams[2].Value = currentMission;

                arParams[3] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = MyComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = UserID;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = TaskType;

                status= Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_CurrentMission", arParams));

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



        public int HorseBan(
            int HorseID,
            int isCheckBox,
            string typeOfBan,
            string banDetail,
            string startDate,
            string TotalDays,
            string endDate,
            string MyComments,
            int UserID,
            string TaskType)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[10];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = HorseID;

                arParams[1] = new SqlParameter("@IsShow", SqlDbType.Int);
                arParams[1].Value = isCheckBox;

                arParams[2] = new SqlParameter("@TypeofBan", SqlDbType.VarChar, 100);
                arParams[2].Value = typeOfBan;

                arParams[3] = new SqlParameter("@BanDetail", SqlDbType.VarChar, 100);
                arParams[3].Value = banDetail;

                arParams[4] = new SqlParameter("@StartDate", SqlDbType.VarChar, 30);
                if (startDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = startDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }


                arParams[5] = new SqlParameter("@TotalDays", SqlDbType.VarChar, 10);
                arParams[5].Value = TotalDays;

                arParams[6] = new SqlParameter("@EndDate", SqlDbType.VarChar, 30);
                if (endDate.Equals("__-__-____"))
                {
                    arParams[6].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = endDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[6].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[6].Value = Convert.ToDateTime(dtformat);
                }

                arParams[7] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[7].Value = MyComments;

                arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[8].Value = UserID;

                arParams[9] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[9].Value = TaskType;

                status = Convert.ToInt32(SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_HorseBan", arParams));
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
        /// Insert,Update and Delete Horse Profile
        /// </summary>
        /// <param name="horseId"></param>
        /// <param name="isCheckBox"></param>
        /// <param name="profile"></param>
        /// <param name="fromDate"></param>
        /// <param name="tillDate"></param>
        /// <param name="myComments"></param>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public int HorseProfile(
            int horseId,
            int isCheckBox,
            string profile,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[8];
                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@IsShow", SqlDbType.Int);
                arParams[1].Value = isCheckBox;

                arParams[2] = new SqlParameter("@Profile", SqlDbType.VarChar, 100);
                arParams[2].Value = profile;

                arParams[3] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 1000);
                arParams[5].Value = myComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userId;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_HorseProfile", arParams));
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
        /// This is for all Horse Class group and Home Distance
        /// </summary>
        /// <param name="capacityXmlString"></param>
        /// <param name="horseId"></param>
        /// <param name="popupId"></param>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <param name="horseCapacityId"></param>
        public void HorseCapacity(
            string capacityXmlString,
            int horseId,
            int popupId,
            int userId,
            string taskType,
            int horseCapacityId)
        {
            SqlParameter[] arParams = new SqlParameter[6];
            try
            {
                arParams[0] = new SqlParameter("@Completevalues", SqlDbType.VarChar, -1) { Value = capacityXmlString };
                arParams[1] = new SqlParameter("@HorseID_FK", SqlDbType.Int) { Value = horseId };
                arParams[2] = new SqlParameter("@PopupID", SqlDbType.Int) { Value = popupId };
                arParams[3] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userId };
                arParams[4] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100) { Value = taskType };
                arParams[5] = new SqlParameter("@HorseCapacityID", SqlDbType.Int) { Value = horseCapacityId };
                SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_HorseCapacity", arParams);
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
        }

        /// <summary>
        /// Update the Horse Capacity value
        /// </summary>
        /// <param name="horseCapacityId"></param>
        /// <param name="isShow"></param>
        /// <param name="fromDate"></param>
        /// <param name="tillDate"></param>
        /// <param name="myComments"></param>
        /// <param name="userId"></param>
        public void HorseCapacityUpdate(
            int horseCapacityId,
            int isShow,
            string fromDate,
            string tillDate,
            string myComments,
            int userId)
        {
            SqlParameter[] arParams = new SqlParameter[6];
            try
            {
                arParams[0] = new SqlParameter("@HorseCapacityID", SqlDbType.Int) { Value = horseCapacityId };
                arParams[1] = new SqlParameter("@IsShow", SqlDbType.Int) { Value = isShow };
                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enterDate = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = Convert.ToDateTime(enterDate.ToString("yyyy-MM-dd 00:00:00"));
                }
                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enterDate = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = Convert.ToDateTime(enterDate.ToString("yyyy-MM-dd 00:00:00"));
                }

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = myComments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userId };
                SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_UpdateHorseCapacity", arParams);
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
        }

        /// <summary>
        /// Bind Drop Down Value
        /// </summary>
        /// <param name="DropDownName"></param>
        /// <returns></returns>
        public DataTable GetDropdownBind(string DropDownName)
        {
            DataTable dt;
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
        /// 
        /// </summary>
        /// <param name="horseId"></param>
        /// <param name="homeDistance"></param>
        /// <param name="supportType"></param>
        /// <param name="supportLevel"></param>
        /// <param name="fromDate"></param>
        /// <param name="tillDate"></param>
        /// <param name="myComments"></param>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public int HomeDistance(
            int horseId,
            int homeDistance,
            int supportType,
            string supportLevel,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[9];
                arParams[0] = new SqlParameter("@HOMEDISTANCEID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HomeDistance", SqlDbType.Int);
                arParams[1].Value = homeDistance;

                arParams[2] = new SqlParameter("@supportType", SqlDbType.Int);
                arParams[2].Value = supportType;

                arParams[3] = new SqlParameter("@SupportLevel", SqlDbType.VarChar, 500);
                arParams[3].Value = supportLevel;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_HomeDistance", arParams));
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


        public int MyHomeDistance(
            int horseId,
            int homeDistance,
            int supportType,
            string supportLevel,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[9];
                arParams[0] = new SqlParameter("@HOMEDISTANCEID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HomeDistance", SqlDbType.Int);
                arParams[1].Value = homeDistance;

                arParams[2] = new SqlParameter("@supportType", SqlDbType.Int);
                arParams[2].Value = supportType;

                arParams[3] = new SqlParameter("@SupportLevel", SqlDbType.VarChar, 500);
                arParams[3].Value = supportLevel;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_MyHomeDistance", arParams));
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
        /// 
        /// </summary>
        /// <param name="horseId"></param>
        /// <param name="homeDistance"></param>
        /// <param name="supportType"></param>
        /// <param name="supportLevel"></param>
        /// <param name="fromDate"></param>
        /// <param name="tillDate"></param>
        /// <param name="myComments"></param>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public int ExpectedHomeDistance(
            int horseId,
            int homeDistance,
            int supportType,
            string supportLevel,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[9];
                arParams[0] = new SqlParameter("@HOMEDISTANCEID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HomeDistance", SqlDbType.Int);
                arParams[1].Value = homeDistance;

                arParams[2] = new SqlParameter("@supportType", SqlDbType.Int);
                arParams[2].Value = supportType;

                arParams[3] = new SqlParameter("@SupportLevel", SqlDbType.VarChar,500);
                arParams[3].Value = supportLevel;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_ExpectedDistance", arParams));
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
        /// 
        /// </summary>
        /// <param name="horseId"></param>
        /// <param name="homeDistance"></param>
        /// <param name="supportType"></param>
        /// <param name="supportLevel"></param>
        /// <param name="fromDate"></param>
        /// <param name="tillDate"></param>
        /// <param name="myComments"></param>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public int FavDistanceGroup(
            int horseId,
            int homeDistance,
            int supportType,
            string supportLevel,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[9];
                arParams[0] = new SqlParameter("@HOMEDISTANCEID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HomeDistance", SqlDbType.Int);
                arParams[1].Value = homeDistance;

                arParams[2] = new SqlParameter("@supportType", SqlDbType.Int);
                arParams[2].Value = supportType;

                arParams[3] = new SqlParameter("@SupportLevel", SqlDbType.VarChar,500);
                arParams[3].Value = supportLevel;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_FavourableDistanceGroup",
                            arParams));
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


        public int HomeClass(
            int horseId,
            int homeDistance,
            int supportType,
            string supportLevel,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[9];
                arParams[0] = new SqlParameter("@HOMEDISTANCEID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HomeDistance", SqlDbType.Int);
                arParams[1].Value = homeDistance;

                arParams[2] = new SqlParameter("@supportType", SqlDbType.Int);
                arParams[2].Value = supportType;

                arParams[3] = new SqlParameter("@SupportLevel", SqlDbType.VarChar, 500);
                arParams[3].Value = supportLevel;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_HorseHomeClass", arParams));
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
        /// 
        /// </summary>
        /// <param name="horseId"></param>
        /// <param name="homeDistance"></param>
        /// <param name="supportType"></param>
        /// <param name="supportLevel"></param>
        /// <param name="fromDate"></param>
        /// <param name="tillDate"></param>
        /// <param name="myComments"></param>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public int MyHomeClass(
            int horseId,
            int homeDistance,
            int supportType,
            string supportLevel,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[9];
                arParams[0] = new SqlParameter("@HOMEDISTANCEID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HomeDistance", SqlDbType.Int);
                arParams[1].Value = homeDistance;

                arParams[2] = new SqlParameter("@supportType", SqlDbType.Int);
                arParams[2].Value = supportType;

                arParams[3] = new SqlParameter("@SupportLevel", SqlDbType.VarChar,500);
                arParams[3].Value = supportLevel;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_MyHorseHomeClass", arParams));
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
        /// 
        /// </summary>
        /// <param name="horseId"></param>
        /// <param name="homeDistance"></param>
        /// <param name="supportType"></param>
        /// <param name="supportLevel"></param>
        /// <param name="fromDate"></param>
        /// <param name="tillDate"></param>
        /// <param name="myComments"></param>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public int ExpectedClass(
            int horseId,
            int homeDistance,
            int supportType,
            string supportLevel,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[9];
                arParams[0] = new SqlParameter("@HOMEDISTANCEID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HomeDistance", SqlDbType.Int);
                arParams[1].Value = homeDistance;

                arParams[2] = new SqlParameter("@supportType", SqlDbType.Int);
                arParams[2].Value = supportType;

                arParams[3] = new SqlParameter("@SupportLevel", SqlDbType.VarChar,500);
                arParams[3].Value = supportLevel;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_ExpectedClass", arParams));
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
        /// 
        /// </summary>
        /// <param name="horseId"></param>
        /// <param name="homeDistance"></param>
        /// <param name="supportType"></param>
        /// <param name="supportLevel"></param>
        /// <param name="fromDate"></param>
        /// <param name="tillDate"></param>
        /// <param name="myComments"></param>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public int FavClassGroup(
            int horseId,
            int homeDistance,
            int supportType,
            string supportLevel,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[9];
                arParams[0] = new SqlParameter("@HOMEDISTANCEID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HomeDistance", SqlDbType.Int);
                arParams[1].Value = homeDistance;

                arParams[2] = new SqlParameter("@supportType", SqlDbType.Int);
                arParams[2].Value = supportType;

                arParams[3] = new SqlParameter("@SupportLevel", SqlDbType.VarChar,500);
                arParams[3].Value = supportLevel;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_FavourableClassGroup", arParams));
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

        //public void DistanceOldPerformance(DataTable dt)
        //{
        //    using (SqlBulkCopy copy = new SqlBulkCopy(_conn))
        //    {
        //        copy.ColumnMappings.Add(0, 1);
        //        copy.ColumnMappings.Add(1, 2);
        //        copy.ColumnMappings.Add(2, 3);
        //        copy.ColumnMappings.Add(3, 4);
        //        copy.ColumnMappings.Add(4, 5);
        //        copy.ColumnMappings.Add(5, 6);
        //        copy.ColumnMappings.Add(6, 7);
        //        copy.ColumnMappings.Add(7, 8);
        //        copy.ColumnMappings.Add(8, 9);
        //        copy.ColumnMappings.Add(9, 10);
        //        copy.ColumnMappings.Add(10, 11);
        //        copy.ColumnMappings.Add(11, 12);
        //        copy.ColumnMappings.Add(12, 13);
        //        copy.ColumnMappings.Add(13, 14);
        //        copy.DestinationTableName = "dbo.Horse_DistancePerformanceOld";
        //        try
        //        {
        //            this._conn.Open();
        //            copy.WriteToServer(dt);
        //        }
        //        catch (Exception e)
        //        {
        //            throw e;
        //        }
        //        finally
        //        {
        //            this._conn.Close();    
        //        }
        //    }
        //}

        public string DistanceOldPerformance(DataTable dt, int userid, string tasktype)
        {
            int checkRecord = 0;
            string tilldate = string.Empty;
            string returnresult = string.Empty;
            try
            {
                StringBuilder result = new StringBuilder();
                SqlParameter[] arParams = new SqlParameter[11];
                for (var count = 0; count < dt.Rows.Count; count++)
                {
                    arParams[0] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = dt.Rows[count][0] };
                    arParams[1] = new SqlParameter("@DistanceID", SqlDbType.Int) { Value = dt.Rows[count][1] };
                    arParams[2] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                    tilldate = dt.Rows[count][2].ToString();
                    if (tilldate.Equals("__-__-____"))
                    {
                        arParams[2].Value = DBNull.Value;
                    }
                    else
                    {
                        string[] dateString;
                        string[] endformat;
                        DateTime enter_date;
                        if(tilldate.Contains("/")){
                        dateString = tilldate.Split('/');
                        endformat = dateString[2].Split(' ');
                        enter_date = Convert.ToDateTime(endformat[0] + "-" + dateString[1] + "-" + dateString[0]);
                        }
                        else{
                            dateString = tilldate.Split('-');
                            endformat = dateString[2].Split(' ');
                            enter_date = Convert.ToDateTime(endformat[0] + "-" + dateString[1] + "-" + dateString[0]);
                        }

                        arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                        //string dtformat = enter_date.ToString("yyyy-MM-dd");
                        //arParams[2].Value = Convert.ToDateTime(dtformat);
                    }


                    arParams[3] = new SqlParameter("@I", SqlDbType.Int) { Value = dt.Rows[count][3] };
                    arParams[4] = new SqlParameter("@II", SqlDbType.Int) { Value = dt.Rows[count][4] };
                    arParams[5] = new SqlParameter("@III", SqlDbType.Int) { Value = dt.Rows[count][5] };
                    arParams[6] = new SqlParameter("@IV", SqlDbType.Int) { Value = dt.Rows[count][6] };
                    arParams[7] = new SqlParameter("@TotalRuns", SqlDbType.Int) { Value = dt.Rows[count][7] };
                    arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                    arParams[8].Value = userid;
                    arParams[9] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                    arParams[9].Value = tasktype;
                    arParams[10] = new SqlParameter("@IsShow", SqlDbType.Int) { Value = dt.Rows[count][8] };
                    checkRecord =
                        Convert.ToInt32(
                            SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_DistancePerformanceOld", arParams));
                    if (checkRecord == 1)
                    {
                        result.Append(dt.Rows[count][9] + "- Insert,");
                      //  result.AppendLine(Environment.NewLine);
                    }
                    else if (checkRecord == 4)
                    {
                        result.Append(dt.Rows[count][9] + "- Already Exist,");
                        //result.AppendLine(Environment.NewLine);
                    }
                    else if (checkRecord == 5)
                    {
                        result.Append(dt.Rows[count][9] + "- Activated,");
                    //    result.AppendLine(Environment.NewLine);
                    }
                }
                returnresult = result.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this._conn.Close();
            }
            return returnresult;
        }

        
        public int DistanceOldGroupEdit(
            int distanceOldId,
            int distanceId,
            string tillDate,
            int I,
            int II,
            int III,
            int IV,
            int totalRuns,
            int userId,
            string taskType,
            int isshow)
        {
            int status = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[11];
                arParams[0] = new SqlParameter("@HorseNameID", SqlDbType.Int) { Value = distanceOldId };
                arParams[1] = new SqlParameter("@DistanceID", SqlDbType.Int) { Value = distanceId };
                arParams[2] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@I", SqlDbType.Int) { Value = I };
                arParams[4] = new SqlParameter("@II", SqlDbType.Int) { Value = II };
                arParams[5] = new SqlParameter("@III", SqlDbType.Int) { Value = III };
                arParams[6] = new SqlParameter("@IV", SqlDbType.Int) { Value = IV };
                arParams[7] = new SqlParameter("@TotalRuns", SqlDbType.Int) { Value = totalRuns };
                arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[8].Value = userId;
                arParams[9] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[9].Value = taskType;
                arParams[10] = new SqlParameter("@IsShow", SqlDbType.Int) { Value = isshow };
                status =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_DistancePerformanceOld",
                            arParams));
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


        public int DistancePerformance(
            int horseId,
            int homeDistance,
            int supportType,
            string supportLevel,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[9];
                arParams[0] = new SqlParameter("@HOMEDISTANCEID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HomeDistance", SqlDbType.Int);
                arParams[1].Value = homeDistance;

                arParams[2] = new SqlParameter("@supportType", SqlDbType.Int);
                arParams[2].Value = supportType;

                arParams[3] = new SqlParameter("@SupportLevel", SqlDbType.VarChar,500);
                arParams[3].Value = supportLevel;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_DistancePerformance",
                            arParams));
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


        public string ClassGroupOldPerformance(DataTable dt, int userid, string tasktype)
        {
            int checkRecord = 0;
            string tilldate = string.Empty;
            string returnresult = string.Empty;
            try
            {
                StringBuilder result = new StringBuilder();
                SqlParameter[] arParams = new SqlParameter[11];
                for (var count = 0; count < dt.Rows.Count; count++)
                {
                    arParams[0] = new SqlParameter("@ClassOldID", SqlDbType.Int) { Value = dt.Rows[count][0] };
                    arParams[1] = new SqlParameter("@ClassGroupID", SqlDbType.Int) { Value = dt.Rows[count][1] };
                    arParams[2] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                    ErrorHandling.CheckEachSteps("Datatable value: " + dt.Rows[count][2]);
                    tilldate = dt.Rows[count][2].ToString();
                    ErrorHandling.CheckEachSteps("Till Date: " + tilldate);
                    if (tilldate.Equals("__-__-____"))
                    {
                        arParams[2].Value = DBNull.Value;
                    }
                    else
                    {
                        string[] dateString;
                        string[] endformat;
                        DateTime enter_date;
                        if (tilldate.Contains("/"))
                        {
                            dateString = tilldate.Split('/');
                            endformat = dateString[2].Split(' ');
                            enter_date = Convert.ToDateTime(endformat[0] + "-" + dateString[1] + "-" + dateString[0]);
                        }
                        else
                        {
                            dateString = tilldate.Split('-');
                            endformat = dateString[2].Split(' ');
                            enter_date = Convert.ToDateTime(endformat[0] + "-" + dateString[1] + "-" + dateString[0]);
                        }

                        //string dtformat = enter_date.ToString("yyyy-MM-dd");
                        //arParams[2].Value = Convert.ToDateTime(dtformat);

                        arParams[2].Value = enter_date.ToString("yyyy-MM-dd");

                     
                    }
                    arParams[3] = new SqlParameter("@I", SqlDbType.Int) { Value = dt.Rows[count][3] };
                    arParams[4] = new SqlParameter("@II", SqlDbType.Int) { Value = dt.Rows[count][4] };
                    arParams[5] = new SqlParameter("@III", SqlDbType.Int) { Value = dt.Rows[count][5] };
                    arParams[6] = new SqlParameter("@IV", SqlDbType.Int) { Value = dt.Rows[count][6] };
                    arParams[7] = new SqlParameter("@TotalRuns", SqlDbType.Int) { Value = dt.Rows[count][7] };
                    arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                    arParams[8].Value = userid;
                    arParams[9] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                    arParams[9].Value = tasktype;
                    arParams[10] = new SqlParameter("@IsShow", SqlDbType.Int) { Value = dt.Rows[count][8] };
                    checkRecord =
                        Convert.ToInt32(
                            SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_ClassGroupPerformanceOld", arParams));
                    if (checkRecord == 1)
                    {
                        result.Append(dt.Rows[count][9] + "- Insert,");
                    }
                    else if (checkRecord == 4)
                    {
                        result.Append(dt.Rows[count][9] + "- Already Exist,");
                    }
                    else if (checkRecord == 5)
                    {
                        result.Append(dt.Rows[count][9] + "- Activated,");
                    }
                }
                returnresult = result.ToString();
            }
            catch (Exception e)
            {
                ErrorHandling.CheckEachSteps(e.StackTrace);
                ErrorHandling.SendErrorToText(e);
                throw;
                //throw new ApplicationException(e);
            }
            finally
            {
                this._conn.Close();
            }
            return returnresult;
        }

        


        public int ClassGroupPerformanceOldGroupEdit(
            int distanceOldId,
            int distanceId,
            string tillDate,
            int I,
            int II,
            int III,
            int IV,
            int totalRuns,
            int userId,
            string taskType,
            int isShow)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[11];
                arParams[0] = new SqlParameter("@ClassOldID", SqlDbType.Int) { Value = distanceOldId };
                arParams[1] = new SqlParameter("@ClassGroupID", SqlDbType.Int) { Value = distanceId };
                arParams[2] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }


                arParams[3] = new SqlParameter("@I", SqlDbType.Int) { Value = I };
                arParams[4] = new SqlParameter("@II", SqlDbType.Int) { Value = II };
                arParams[5] = new SqlParameter("@III", SqlDbType.Int) { Value = III };
                arParams[6] = new SqlParameter("@IV", SqlDbType.Int) { Value = IV };
                arParams[7] = new SqlParameter("@TotalRuns", SqlDbType.Int) { Value = totalRuns };
                arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[8].Value = userId;
                arParams[9] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[9].Value = taskType;
                arParams[10] = new SqlParameter("@IsShow", SqlDbType.Int) { Value = isShow };
                checkRecord =
                 Convert.ToInt32(
                     SqlHelper.ExecuteScalar(
                         _conn,
                         CommandType.StoredProcedure,
                         "sp_ClassGroupPerformanceOld ",
                         arParams));
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

        public int ClassGroupPerformance(
            int horseId,
            int homeDistance,
            int supportType,
            string supportLevel,
            string fromDate,
            string tillDate,
            string myComments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[9];
                arParams[0] = new SqlParameter("@HOMEDISTANCEID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HomeDistance", SqlDbType.Int);
                arParams[1].Value = homeDistance;

                arParams[2] = new SqlParameter("@supportType", SqlDbType.Int);
                arParams[2].Value = supportType;

                arParams[3] = new SqlParameter("@SupportLevel", SqlDbType.VarChar,500);
                arParams[3].Value = supportLevel;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ClassGroupPerformance ",
                            arParams));
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


        public int HorseSex(
            int horseId,
            int sexId,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@SexID", SqlDbType.Int);
                arParams[1].Value = sexId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseSex",
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


        public int HorseStandingNation(
            int horseId,
            int sexId,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@SexID", SqlDbType.Int);
                arParams[1].Value = sexId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseStandingNation",
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


        public int HorseBaseCenter(
            int horseId,
            int sexId,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@SexID", SqlDbType.Int);
                arParams[1].Value = sexId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseBaseCenter",
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

        public int HorseStationCenter(
            int horseId,
            int sexId,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@SexID", SqlDbType.Int);
                arParams[1].Value = sexId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseStationCenter",
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


        public int HorseOwnerRecord(
            int horseId,
            int sexId,
            string FromDate,
            string TillDate,
            string changestatus,
            string reasonofchange,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int);
                arParams[1].Value = sexId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@ChangeStatus", SqlDbType.VarChar, 100) { Value = changestatus };
                arParams[5] = new SqlParameter("@ReasonofChange", SqlDbType.VarChar, 100) { Value = reasonofchange};
                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = MyComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = UserID;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseOwnerRecord",
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


        public int HorseOwnerActual(
            int horseId,
            int sexId,
            string FromDate,
            string TillDate,
            string changestatus,
            string reasonofchange,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int);
                arParams[1].Value = sexId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@ChangeStatus", SqlDbType.VarChar, 100) { Value = changestatus };
                arParams[5] = new SqlParameter("@ReasonofChange", SqlDbType.VarChar, 100) { Value = reasonofchange };
                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = MyComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = UserID;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseOwnerActual",
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

        public int HorseTrainerRecord(
            int horseId,
            int sexId,
            string FromDate,
            string TillDate,
            string changestatus,
            string reasonofchange,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int);
                arParams[1].Value = sexId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@ChangeStatus", SqlDbType.VarChar, 100) { Value = changestatus };
                arParams[5] = new SqlParameter("@ReasonofChange", SqlDbType.VarChar, 100) { Value = reasonofchange };
                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = MyComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = UserID;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseTrainerRecord",
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

        public int HorseTrainerActual(
            int horseId,
            int sexId,
            string FromDate,
            string TillDate,
            string changestatus,
            string reasonofchange,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int);
                arParams[1].Value = sexId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@ChangeStatus", SqlDbType.VarChar, 100) { Value = changestatus };
                arParams[5] = new SqlParameter("@ReasonofChange", SqlDbType.VarChar, 100) { Value = reasonofchange };
                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = MyComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = UserID;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseTrainerActual",
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

        public int HorseTargetRace(
            int horseId,
            int centerId,
            string raceDate,
            int raceGeneralId,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[1].Value = centerId;

                arParams[2] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@RaceGeneralID", SqlDbType.Int) { Value = raceGeneralId };

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }
                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = MyComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = UserID;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseTargetRace",
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

        public int HorseBodyWeight(
            int horseId,
            string bdyweightinPassport,
            string upperrange,
            string lowerrange,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@BWIPassport", SqlDbType.VarChar,100);
                arParams[1].Value = bdyweightinPassport;

                arParams[2] = new SqlParameter("@BWUpperRange", SqlDbType.VarChar, 100);
                arParams[2].Value = upperrange;

                arParams[3] = new SqlParameter("@BWLowerRange", SqlDbType.VarChar, 100);
                arParams[3].Value = lowerrange;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }
                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = MyComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = UserID;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseBodyWeight",
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


        public int HandicapRating(
            int horseId,
            string handicaprating,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HandicapRating", SqlDbType.VarChar, 100);
                arParams[1].Value = handicaprating;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseHandicapRating",
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


        public int MyHandicapRating(
            int horseId,
            string handicaprating,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HandicapRating", SqlDbType.VarChar, 100);
                arParams[1].Value = handicaprating;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseMyHandicapRating",
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


        public int HorseShoe(
            int horseId,
            int ShoeID,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = ShoeID;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseShoe",
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


        public int HorseEquipment(
            int horseId,
            int ShoeID,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = ShoeID;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseEquipment",
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


        public int HorseBit(
           int horseId,
           int ShoeID,
           string FromDate,
           string TillDate,
           string MyComments,
           int UserID,
           string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = ShoeID;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseBit",
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


        public int HorseTrackStar(
          int horseId,
          int ShoeID,
          string FromDate,
          string TillDate,
          string MyComments,
          int UserID,
          string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = ShoeID;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseTrackStar",
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


        public int HorseDirectGate(
          int horseId,
          int ShoeID,
          string FromDate,
          string TillDate,
          string MyComments,
          int UserID,
          string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = ShoeID;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseDirectGate",
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


        public int HorseShoeDescription(
            int horseId,
            int ShoeID,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = ShoeID;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseShoeDescription",
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



        public int HorseSaddleNo(
           int horseId,
           int centerId,
           int yearId,
           string saddleno,
           string FromDate,
           string TillDate,
           string MyComments,
           int UserID,
           string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[1].Value = centerId;

                arParams[2] = new SqlParameter("@YearID", SqlDbType.Int);
                arParams[2].Value = yearId;

                arParams[3] = new SqlParameter("@SaddleNo", SqlDbType.VarChar,100);
                arParams[3].Value = saddleno;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }
                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = MyComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = UserID;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseSaddleNo",
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


        public int HorseLiking(
           int horseId,
           string liking,
           string details,
           string FromDate,
           string TillDate,
           string MyComments,
           int UserID,
           string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@Liking", SqlDbType.VarChar, 100);
                arParams[1].Value = liking;

                arParams[2] = new SqlParameter("@Details", SqlDbType.VarChar, 100);
                arParams[2].Value = details;

                arParams[3] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }
                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = MyComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = UserID;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseLikingEnvironment",
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

        public int HorseRunningStyle(
            int horseId,
            int ShoeID,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = ShoeID;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseRunningStyle",
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


        public int HorseHabits(
            int horseId,
            int habitid,
            int habittypeid,
            string details,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@HabitID", SqlDbType.Int);
                arParams[1].Value = habitid;

                arParams[2] = new SqlParameter("@HabitTypeID", SqlDbType.Int);
                arParams[2].Value = habittypeid;

                arParams[3] = new SqlParameter("@Details", SqlDbType.VarChar,100);
                arParams[3].Value = details;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }
                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = MyComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = UserID;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseHabits",
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


        public int HorseVet(
            int horseId,
            int diseaseId,
            string fromdate,
            string tilldate,
            string startdate,
            string enddate,
            string comments,
            int userid,
            string tasktype,
            int dayup,
            int dayut)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[11];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@DiseaseID", SqlDbType.Int);
                arParams[1].Value = diseaseId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromdate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tilldate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tilldate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@TreatmentStartDate", SqlDbType.VarChar, 30);
                if (startdate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = startdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TreatmentEndDate", SqlDbType.VarChar, 30);
                if (enddate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = enddate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = comments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userid;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = tasktype;

                if(dayup==0)
                    arParams[9] = new SqlParameter("@DaysUP", SqlDbType.Int) { Value = null };
                else
                arParams[9] = new SqlParameter("@DaysUP", SqlDbType.Int) { Value= dayup};

                if (dayut == 0)
                    arParams[10] = new SqlParameter("@DaysUT", SqlDbType.Int) { Value = null };
                else
                    arParams[10] = new SqlParameter("@DaysUT", SqlDbType.Int) { Value = dayut };
                

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseVeterinaryProblems",
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

        public int HorseSwimming(
            int horsenameid,
            string swimmingdate,
            int swimmingrounds,
            int workoutRating,
            string isshow,
            string tasktype,
            string globalid)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@HorseNameID", SqlDbType.Int);
                arParams[0].Value = horsenameid;

                arParams[1] = new SqlParameter("@SwimmingDate", SqlDbType.VarChar, 30);
                if (swimmingdate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = swimmingdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[2] = new SqlParameter("@SwimmingRounds", SqlDbType.Int);
                arParams[2].Value = swimmingrounds;

                arParams[3] = new SqlParameter("@WorkoutRating", SqlDbType.Int);
                arParams[3].Value = workoutRating;

                arParams[4] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[4].Value = 1;

                arParams[5] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[5].Value = tasktype;

                arParams[6] = new SqlParameter("@isShow", SqlDbType.VarChar, 50);
                arParams[6].Value = isshow;

                arParams[7] = new SqlParameter("@GlobalID", SqlDbType.VarChar, 50);
                arParams[7].Value = globalid;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseSwimming",
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


        public int HorseMyObservation(
            int horseId,
            string myobservation,
            string aimedduration,
            string fromdate,
            string tilldate,
            string reason,
            string comments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@MyObservation", SqlDbType.VarChar, 100) { Value = myobservation };
                arParams[2] = new SqlParameter("@AimedDuration", SqlDbType.VarChar, 100) { Value = aimedduration };

                arParams[3] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromdate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tilldate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tilldate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@Reason", SqlDbType.VarChar, 100) { Value = reason };

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = comments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userid;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = tasktype;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseMyObservation",
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


        public int HorseBandage(
            int horseId,
            string bandage,
            int bandagetypeid,
            string fromdate,
            string tilldate,
            string comments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@Bandage", SqlDbType.VarChar, 100) { Value = bandage };
                arParams[2] = new SqlParameter("@BandageTypeID", SqlDbType.Int) { Value = bandagetypeid };

                arParams[3] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromdate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tilldate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tilldate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = comments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseBandage",
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

        public int HorseCompleteInformation(
            int horseId,
            string dobtype,
            string latefoal,
            string hopeagainstlatefoal,
            string dateofdeath,
            int colorid,
            int birthnationid,
            string sirenameid,
            string damnameid,
            string gotabroad,
            string birthstudnameid,
            string breedernameid,
            string classiccantender,
            string classicmaterial,
            string lineage,
            string undervaluedhorse,
            string phsictype,
            string birthdefect,
            string dosageindex,
            string dosageprofile,
            string centerofdistribution,
            string rainydayperformer,
            string whipmustrequired,
            string profilecomplete,
            int userid,
            string tasktype,
            string CarryTopWeight,
            string bodyshape)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[28];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;
                arParams[1] = new SqlParameter("@DOBType", SqlDbType.VarChar, 100) { Value = dobtype};
                arParams[2] = new SqlParameter("@LateFoal", SqlDbType.VarChar, 100) { Value = latefoal };
                arParams[3] = new SqlParameter("@HopeAgainstLateFoal", SqlDbType.VarChar, 100) { Value = hopeagainstlatefoal };
                arParams[4] = new SqlParameter("@DateofDeath", SqlDbType.VarChar, 30);
                if (dateofdeath.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dateofdeath.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }
                arParams[5] = new SqlParameter("@ColorID", SqlDbType.Int) { Value = colorid };
                arParams[6] = new SqlParameter("@BirthNationID", SqlDbType.Int) { Value = birthnationid };
                arParams[7] = new SqlParameter("@SireNameID", SqlDbType.VarChar,100) { Value = sirenameid };
                arParams[8] = new SqlParameter("@DamNameID", SqlDbType.VarChar, 100) { Value = damnameid };
                arParams[9] = new SqlParameter("@GotAbroad", SqlDbType.VarChar, 100) { Value = gotabroad };
                arParams[10] = new SqlParameter("@BirthStudNameID", SqlDbType.VarChar, 100) { Value = birthstudnameid };
                arParams[11] = new SqlParameter("@BreederNameID", SqlDbType.VarChar,100) { Value = breedernameid };
                arParams[12] = new SqlParameter("@ClassicCantender", SqlDbType.VarChar,100) { Value = classiccantender };
                arParams[13] = new SqlParameter("@ClassicMaterial", SqlDbType.VarChar, 100) { Value = classicmaterial };
                arParams[14] = new SqlParameter("@Lineage", SqlDbType.VarChar, 100) { Value = lineage };
                arParams[15] = new SqlParameter("@UnderValuedHorse", SqlDbType.VarChar, 100) { Value = undervaluedhorse };
                arParams[16] = new SqlParameter("@PhysicType", SqlDbType.VarChar, 100) { Value = phsictype };
                arParams[17] = new SqlParameter("@BirthDefect", SqlDbType.VarChar, 100) { Value = birthdefect };
                arParams[18] = new SqlParameter("@DosageIndex", SqlDbType.VarChar, 100) { Value = dosageindex };
                arParams[19] = new SqlParameter("@DosageProfile", SqlDbType.VarChar, 100) { Value = dosageprofile };
                arParams[20] = new SqlParameter("@CenterOfDistribution", SqlDbType.VarChar, 100) { Value = centerofdistribution };
                arParams[21] = new SqlParameter("@RainyDayPerformer", SqlDbType.VarChar, 100) { Value = rainydayperformer };
                arParams[22] = new SqlParameter("@WhipMustRequired", SqlDbType.VarChar, 100) { Value = whipmustrequired};
                arParams[23] = new SqlParameter("@ProfileComplete", SqlDbType.VarChar, 100) { Value = profilecomplete };
                arParams[24] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[24].Value = userid;
                arParams[25] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[25].Value = tasktype;
                arParams[26] = new SqlParameter("@CarryTopWeight", SqlDbType.VarChar, 100) { Value = CarryTopWeight };
                arParams[27] = new SqlParameter("@BodyShape", SqlDbType.VarChar, 100) { Value = bodyshape };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseCompleteInformation",
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


        /// <summary>
        /// Insert Excel data in the database
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable UploadExcelRecordBulk(DataTable dt, string PageName)
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
                        copy.ColumnMappings.Add(32, 32);
                        copy.DestinationTableName = "dbo.Horse_CompleteImport";
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
                    arParams[0].Value = "HorseName";

                    dtresult = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_UploadHorseExcelData", arParams);

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



        /// <summary>
        /// Insert Excel data in the database
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable UploadExcelRecordBulkMinimumColumns(DataTable dt, string PageName)
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
                  //  copy.ColumnMappings.Add(14, 14);
                    
                    copy.DestinationTableName = "dbo.Horse_ImportFiveColumn";
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

                dtresult = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_UploadHorseExcelData", arParams);

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


        public int InsertHorseOwnerStud(
            int HorseID,
            string ownerstud,
            string ownershipengagement,
            string FromDate,
            string TillDate,
            string MyComments,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = HorseID;

                arParams[1] = new SqlParameter("@OwnerStud", SqlDbType.VarChar, 100);
                arParams[1].Value = ownerstud;

                arParams[2] = new SqlParameter("@OwnershipEngagement", SqlDbType.VarChar,100);
                arParams[2].Value = ownershipengagement;

                arParams[3] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3*].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = MyComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = UserID;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseOwnerStud",
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



        public DataTable ImportHorseExcel(DataTable dt, string PageName)
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
                    copy.WriteToServer(dt);
                }

                arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100);
                arParams[0].Value = PageName;

                dtresult = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_ImportHorse", arParams);

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


        /// <summary>
        /// Insert Excel data in the database
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable Import30(DataTable dt, string PageName)
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

                dtresult = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_UploadHorseExcelData", arParams);

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


        public int InsertAchivements(
            int achivementsid,
            int HorseID,
            int centerid,
            int yearid,
            int seasonid,
            string racedate,
            string seasonracenumber,
            string position,
            int raceid,
            string racetype,
            string racestatus,
            string million,
            string sweepstake,
            string classic,
            string graded,
            string gradeno,
            int UserID,
            string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[18];
            try
            {

                arParams[0] = new SqlParameter("@AchivementsID", SqlDbType.Int) { Value = achivementsid };
                arParams[1] = new SqlParameter("@HorseID", SqlDbType.Int) { Value = HorseID };
                arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int) { Value = centerid };
                arParams[3] = new SqlParameter("@YearID", SqlDbType.Int) { Value = yearid };
                arParams[4] = new SqlParameter("@SeasonID", SqlDbType.Int) { Value = seasonid };
                arParams[5] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }
                arParams[6] = new SqlParameter("@SeasonRaceNo", SqlDbType.VarChar, 100) { Value = seasonracenumber };
                arParams[7] = new SqlParameter("@Position", SqlDbType.VarChar, 100) { Value = position };
                arParams[8] = new SqlParameter("@RaceID", SqlDbType.Int) { Value = raceid };
                arParams[9] = new SqlParameter("@RaceType", SqlDbType.VarChar, 100) { Value = racetype };
                arParams[10] = new SqlParameter("@RaceStatus", SqlDbType.VarChar, 100) { Value = racestatus };
                arParams[11] = new SqlParameter("@Million", SqlDbType.VarChar, 100) { Value = million };
                arParams[12] = new SqlParameter("@SweepStake", SqlDbType.VarChar, 100) { Value = sweepstake };
                arParams[13] = new SqlParameter("@Classic", SqlDbType.VarChar, 100) { Value = classic };
                arParams[14] = new SqlParameter("@Graded", SqlDbType.VarChar, 100) { Value = graded};
                arParams[15] = new SqlParameter("@GradeNo", SqlDbType.VarChar, 100) { Value = gradeno };
                arParams[16] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[16].Value = UserID;
                arParams[17] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[17].Value = TaskType;
                
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseAchivements",
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


        public int HorseCurrentForm(
          int horseId,
          int currentformid,
          string FromDate,
          string TillDate,
          string MyComments,
          int UserID,
          string TaskType)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@HorseID", SqlDbType.Int);
                arParams[0].Value = horseId;

                arParams[1] = new SqlParameter("@CurrentFormID", SqlDbType.Int);
                arParams[1].Value = currentformid;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (FromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = FromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = MyComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = UserID;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = TaskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseCurrentForm",
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

        /// <summary>
        /// Return Complete Main Horse form Information
        /// </summary>
        /// <param name="HorseID"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataTable GetHorseHabitInformation(int horsehabitid)
        {

            DataTable ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {

                arParams[0] = new SqlParameter("@HorseHabitID", SqlDbType.Int);
                arParams[0].Value = horsehabitid;

                ds = SqlHelper.ExecuteDataTable(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetHorseHabitInformation",
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


		public int GetHorseCount()
		{
			int horseId = 0;
			//SqlParameter[] arParams = new SqlParameter[2];
			try
			{
				horseId =
					Convert.ToInt32(
						SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_HorseCount"));
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
			return horseId;
		}


		public DataTable GetHorseNameDamBasis(string horsenameid)
		{

			DataTable dt = null;
			;
			SqlParameter[] arParams = new SqlParameter[1];
			try
			{
				arParams[0] = new SqlParameter("@HorseNameID", SqlDbType.VarChar, 50);
				arParams[0].Value = horsenameid;

				dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetHorseNameOnDamBasis", arParams);
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


        public int HorseTreadmill(
            int horsenameid,
            string treadmilldate,
            string valuea,
            string valueb,
            int workoutRating,
            string isshow,
            string tasktype,
            string globalid)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@HorseNameID", SqlDbType.Int);
                arParams[0].Value = horsenameid;

                arParams[1] = new SqlParameter("@TreadmillDate", SqlDbType.VarChar, 30);
                if (treadmilldate.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = treadmilldate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[2] = new SqlParameter("@ValueA", SqlDbType.VarChar, 50);
                arParams[2].Value = valuea;

                arParams[3] = new SqlParameter("@WorkoutRating", SqlDbType.Int);
                arParams[3].Value = workoutRating;

                arParams[4] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[4].Value = 1;

                arParams[5] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[5].Value = tasktype;

                arParams[6] = new SqlParameter("@isShow", SqlDbType.VarChar, 50);
                arParams[6].Value = isshow;

                arParams[7] = new SqlParameter("@GlobalID", SqlDbType.VarChar, 50);
                arParams[7].Value = globalid;

                arParams[8] = new SqlParameter("@ValueB", SqlDbType.VarChar,50);
                arParams[8].Value = valueb;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_HorseTreadmill",
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
    }
}
