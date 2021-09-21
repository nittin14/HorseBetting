using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace VKADB
{
    using System.Data.SqlTypes;

    using Microsoft.SqlServer.Server;

    /// <summary>
    /// Professional Application
    /// </summary>
    public class ProfessionalDL
    {
        /// <summary>
        /// Connecting String
        /// </summary>
        private SqlConnection conn;

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        public ProfessionalDL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        }

        /// <summary>
        ///  Auto fill comment box
        /// </summary>
        /// <param name="autoFillName">textbox name</param>
        /// <param name="prefix">character details</param>
        /// <returns>text value of comment box</returns>
        public DataTable GetProfessionalNameAutoFiller(string autoFillName, string prefix)
        {
            DataTable dt = null;
            SqlParameter[] arparams = new SqlParameter[2];
            try
            {
                arparams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arparams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
                dt = SqlHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, "sp_GetProfessionalNameAutoFill", arparams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return dt;
        }


        public DataSet GetProfessionalNameWithCombination(int professionalId, string taskType)
        {
            DataSet ds = null;
            ;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = taskType;
                ds = SqlHelper.ExecuteDataset(
                    conn,
                    CommandType.StoredProcedure,
                    "sp_GetProfessionalNameWithCombination",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }

        public DataTable ProfessionalName(
            int professionalId,
            string professionalName,
            string professionalWs,
            string professionalshortname,
            string professionalNameAlias,
            string myComments,
            int userId,
            string taskType,
            int profileid,
            int basecenterid,
            int professionalprofileid,
            int professionalbasecenterid,
            string dob,
            string professionaltypeid
            )
        {
            // int checkRecord = 0;
            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[14];
            try
            {
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int) { Value = professionalId };
                arParams[1] = new SqlParameter("@ProfessionalName", SqlDbType.VarChar, 5000) { Value=professionalName };
                arParams[2] = new SqlParameter("@ProfessionalNameWithSolutation", SqlDbType.VarChar, 5000) { Value = professionalWs };
                arParams[3] = new SqlParameter("@ProfessionalShortName", SqlDbType.VarChar, 5000) { Value=professionalshortname };
                arParams[4] = new SqlParameter("@ProfessionalNameAlias", SqlDbType.VarChar, 5000)
                                  {
                                      Value =
                                          professionalNameAlias
                                  };
                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = myComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userId;

                arParams[7] = new SqlParameter("@Tasktype", SqlDbType.VarChar, 50);
                arParams[7].Value = taskType;
                arParams[8] = new SqlParameter("@ProfileID", SqlDbType.Int) { Value = profileid };
                arParams[9] = new SqlParameter("@BaseCenterID", SqlDbType.Int) { Value = basecenterid };
                arParams[10] = new SqlParameter("@ProfessionalProfileId", SqlDbType.Int) { Value = professionalprofileid };
                arParams[11] = new SqlParameter("@ProfessionalBaseCenterId", SqlDbType.Int) { Value = professionalbasecenterid };
                arParams[12] = new SqlParameter("@DateofBirth", SqlDbType.VarChar, 30);
                if (dob.Equals("__-__-____"))
                {
                    arParams[12].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dob.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[12].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[13] = new SqlParameter("@ProfessionalTypeID", SqlDbType.VarChar, 50) { Value = professionaltypeid };
                dt = SqlHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, "sp_ProfessionalName", arParams);

            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
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
            // int checkRecord = 0;
            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int) { Value = professionalId };
                arParams[1] = new SqlParameter("@ProfessionalName", SqlDbType.VarChar, 5000) { Value = professionalName };
                arParams[2] = new SqlParameter("@ProfessionalNameWithSolutation", SqlDbType.VarChar, 5000) { Value = professionalWs };
                arParams[3] = new SqlParameter("@ProfessionalShortName", SqlDbType.VarChar, 5000) { Value = professionalshortname };
                arParams[4] = new SqlParameter("@ProfessionalNameAlias", SqlDbType.VarChar, 5000)
                {
                    Value =
                        professionalNameAlias
                };
                arParams[5] = new SqlParameter("@DateofNameChange", SqlDbType.VarChar, 30);
                if (dateofnamechange.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dateofnamechange.Split('-');
                    DateTime enterDate =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enterDate.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }
                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@Tasktype", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                dt = SqlHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, "sp_ProfessionalNewName", arParams);

            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
        }


        public int ProfessionalProfileP(
            int professionalId,
            int profileId,
            string fromDate,
            string tillDate,
            string mycomments,
            int userId,
            string taskType,
            string profileindetails)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[8];
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@ProfileID", SqlDbType.Int);
                arParams[1].Value = profileId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = mycomments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = userId;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = taskType;
                arParams[7] = new SqlParameter("@ProfileInDetails", SqlDbType.VarChar, 100) { Value = profileindetails };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_ProfessionalProfile", arParams));
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
        }


        

        /// <summary>
        /// Get Multiple value for show popup
        /// </summary>
        /// <param name="dropdownname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetProfessionalName(string dropdownname, int value)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@DropdownName", SqlDbType.VarChar, 100);
                arParams[0].Value = dropdownname;

                arParams[1] = new SqlParameter("@Value", SqlDbType.Int);
                arParams[1].Value = value;

                dt = SqlHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, "sp_GetProfessionalName", arParams);
            }
            catch (Exception ex)
            {

                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
        }



        public DataTable GetProfessionalName(string dropdownname)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {

                arParams[0] = new SqlParameter("@DropdownName", SqlDbType.VarChar, 100);
                arParams[0].Value = dropdownname;

                dt = SqlHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, "sp_GetProfessionalName", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
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
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[8];
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[1].Value = basecenterId;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@Reason", SqlDbType.VarChar, 100);
                arParams[4].Value = reason;


                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userId;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_ProfessionalBaseCenter", arParams));
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
        }

       /// <summary>
       ///  Till date validation
       /// </summary>
       /// <param name="horseId"> professional id </param>
       /// <param name="tasktype"> name of the page </param>
       /// <param name="btntext">Insert/Update</param>
       /// <returns>validation</returns>
        public DataSet GetProfessionalTillDateValidation(int professionalid, string tasktype, string fromdate, string actiontype)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalid;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = tasktype;
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
                
                ds = SqlHelper.ExecuteDataset(
                    conn,
                    CommandType.StoredProcedure,
                    "sp_GetProfessionalTillDateValidation",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }


        public DataSet GetProfessionalTillDateValidationProfile(int professionalid, string tasktype, string fromdate, string actiontype, int profileID)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[5];
            try
            {
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalid;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = tasktype;
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
                arParams[4] = new SqlParameter("@Profile", SqlDbType.VarChar, 100) { Value = profileID };

                ds = SqlHelper.ExecuteDataset(
                    conn,
                    CommandType.StoredProcedure,
                    "sp_GetProfessionalMultipleTillDateValidation",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }


        

        /// <summary>
        /// Insert,Update and Delete Horse Profile
        /// </summary>
        /// <param name="professionalId"></param>
        /// <param name="isCheckBox"></param>
        /// <param name="profile"></param>
        /// <param name="fromDate"></param>
        /// <param name="tillDate"></param>
        /// <param name="myComments"></param>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public int ProfessionalProfile(
            int professionalId,
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
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

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

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = myComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userId;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_ProfessionalProfile", arParams));
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
        }


        /// <summary>
        /// Insert,Update and Delete Horse Profile
        /// </summary>
        /// <param name="professionalId"></param>
        /// <param name="ownercolorid"></param>
        /// <param name="centerid"></param>
        /// <param name="fromDate"></param>
        /// <param name="tillDate"></param>
        /// <param name="myComments"></param>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public int ProfessionalOwnerColor(
            int professionalId,
            int ownercolorid,
            int centerid,
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
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@OwnerColorID", SqlDbType.Int);
                arParams[1].Value = ownercolorid;

                arParams[2] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[2].Value = centerid;

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

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = myComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userId;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_ProfessionalOwnerColor", arParams));
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@Status", SqlDbType.VarChar,50);
                arParams[1].Value = status;

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

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = mycomments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = userid;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_InsertUpdateProfessionalStatus",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
        }


        public int InsertProfessionalCurrentStatus(
           int professionalId,
           int currentstatus,
           string fromdate,
           string tilldate,
           string mycomments,
           int userid,
           string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@Status", SqlDbType.Int );
                arParams[1].Value = currentstatus;

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

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = mycomments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = userid;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_InsertUpdateProfessionalCurrentStatus",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[11];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@BodyWeight", SqlDbType.VarChar,50);
                arParams[1].Value = bodyweight;

                arParams[2] = new SqlParameter("@MinRidingWeight", SqlDbType.VarChar, 50);
                arParams[2].Value = minridingweight;

                arParams[3] = new SqlParameter("@MaxRidingWeight", SqlDbType.VarChar, 50);
                arParams[3].Value = maxridingweight;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromdate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tilldate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tilldate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = mycomments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userid;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = tasktype;

                arParams[9] = new SqlParameter("@JockeyWeightTypeID", SqlDbType.Int);
                arParams[9].Value = jockeyweighttypeid;

				arParams[10] = new SqlParameter("@OverWeight", SqlDbType.VarChar, 50);
				arParams[10].Value = overweight;

				checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_InsertUpdateProfessionalJockeyWeight",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
        }


        public int ProfessionalOtherName(
           int professionalId,
           string jockeyothername,
           int userid,
           string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@OtherName", SqlDbType.VarChar, 100);
                arParams[1].Value = jockeyothername;


                arParams[2] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[2].Value = userid;

                arParams[3] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[3].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalJockeyOtherName",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@TrainerID", SqlDbType.Int);
                arParams[1].Value = trainerid;

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

                arParams[4] = new SqlParameter("@ReasonofChange", SqlDbType.VarChar, 500);
                arParams[4].Value = reasonofchange;

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 500);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_JockeyAprenticeOf",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@TrainerID", SqlDbType.Int);
                arParams[1].Value = trainerid;

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

                arParams[4] = new SqlParameter("@ReasonofChange", SqlDbType.VarChar, 500);
                arParams[4].Value = reasonofchange;

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 500);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_JockeyTrainingAssistant",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@TrainerID", SqlDbType.Int);
                arParams[1].Value = trainerid;

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

                arParams[4] = new SqlParameter("@Percentage", SqlDbType.VarChar, 100);
                arParams[4].Value = percentage;

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 500);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalPartners",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@DistanceID", SqlDbType.Int);
                arParams[1].Value = distanceId;

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

                arParams[4] = new SqlParameter("@RatingMarkFormatStyleID", SqlDbType.Int);
                arParams[4].Value = ratingmarkformatstyleId;

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 500);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalHomeDistance",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@DistanceID", SqlDbType.Int);
                arParams[1].Value = distanceId;

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

                arParams[4] = new SqlParameter("@RatingMarkFormatStyleID", SqlDbType.Int);
                arParams[4].Value = ratingmarkformatstyleId;

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 500);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalHomeFavDistanceGroup",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@DistanceID", SqlDbType.Int);
                arParams[1].Value = distanceId;

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

                arParams[4] = new SqlParameter("@RatingMarkFormatStyleID", SqlDbType.Int);
                arParams[4].Value = ratingmarkformatstyleId;

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 500);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalHomeClass",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@DistanceID", SqlDbType.Int);
                arParams[1].Value = distanceId;

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

                arParams[4] = new SqlParameter("@RatingMarkFormatStyleID", SqlDbType.Int);
                arParams[4].Value = ratingmarkformatstyleId;

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 500);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalHomeFavClassGroup",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@Habit", SqlDbType.VarChar, 100);
                arParams[1].Value = habit;

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

                arParams[4] = new SqlParameter("@HabitDetails", SqlDbType.VarChar, 100);
                arParams[4].Value = habitdetails;

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 500);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalHabit",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@Date", SqlDbType.VarChar, 30);
                if (dates.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dates.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[2] = new SqlParameter("@RelatedTo", SqlDbType.VarChar, 100);
                arParams[2].Value = relatedto;

                arParams[3] = new SqlParameter("@RelatedToName", SqlDbType.VarChar, 100);
                arParams[3].Value = relatedtoname;

                arParams[4] = new SqlParameter("@Occassion", SqlDbType.VarChar, 100);
                arParams[4].Value = occassion;

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 500);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalImportantDates",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@MyObservation", SqlDbType.VarChar, 100);
                arParams[1].Value = myobservation;

                arParams[2] = new SqlParameter("@MyObservationinDetails", SqlDbType.VarChar, 100);
                arParams[2].Value = myobservationindetail;

                arParams[3] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[3].Value = userid;

                arParams[4] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[4].Value = tasktype;

				arParams[5] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
				if (fromdate.Equals("__-__-____"))
				{
					arParams[5].Value = DBNull.Value;
				}
				else
				{
					string[] dateString = fromdate.Split('-');
					DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
					arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
				}

				arParams[6] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
				if (tilldate.Equals("__-__-____"))
				{
					arParams[6].Value = DBNull.Value;
				}
				else
				{
					string[] dateString = tilldate.Split('-');
					DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
					arParams[6].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
				}

				arParams[7] = new SqlParameter("@MyComments", SqlDbType.VarChar, 1000);
				arParams[7].Value = mycomments;

				checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalMyObservation",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[11];
            try
            {

                arParams[0] = new SqlParameter("@MainProfessionalNameID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@RelationProfessionalID", SqlDbType.Int);
                arParams[1].Value = professionalnameid;

                arParams[2] = new SqlParameter("@RelationTypeID", SqlDbType.Int);
                arParams[2].Value = relationtypeid;

                arParams[3] = new SqlParameter("@RelationChildID", SqlDbType.Int);
                arParams[3].Value = relationchildid;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromdate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }

                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tilldate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tilldate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@RelationBreak", SqlDbType.VarChar, 500);
                arParams[6].Value = relationbreak;

                arParams[7] = new SqlParameter("@MyComments", SqlDbType.VarChar, 500);
                arParams[7].Value = mycomments;

                arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[8].Value = userid;

                arParams[9] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[9].Value = tasktype;

				arParams[10] = new SqlParameter("@GroupID", SqlDbType.VarChar, 50);
				arParams[10].Value = groupid;

				checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalRelation",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
        }

        
        public DataTable GetProfessionalId(string professionalname, string profilename, string center)
        {
            int horseId = 0;
            DataTable dt;
            SqlParameter[] arParams = new SqlParameter[3];
            string profilealias = string.Empty;
            string centeralias = string.Empty;
            try
            {
                arParams[0] = new SqlParameter("@ProfessionalName", SqlDbType.VarChar, 5000) { Value = professionalname };
                if(profilename.Contains("/"))
                {
                    string[] prfilealias = profilename.Split('/');
                    profilealias=prfilealias[0].ToString();
                }
                else
                {
                    profilealias=profilename;
                }
                arParams[1] = new SqlParameter("@Profile", SqlDbType.VarChar,100){ Value = profilealias};
                if (center.Contains("/"))
                {
                    string[] centrealias = center.Split('/');
                    centeralias = centrealias[0].ToString();
                }
                else
                {
                    centeralias = center;
                }
                arParams[2] = new SqlParameter("@Center", SqlDbType.VarChar, 100) { Value = centeralias };
                dt =
                        SqlHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, "sp_GetProfessionalID", arParams);
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
        }


        public DataSet GetProfessionalCompleteInformation(int professionalid, string TaskType)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalid;

                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = TaskType;

                ds = SqlHelper.ExecuteDataset(
                    conn,
                    CommandType.StoredProcedure,
                    "sp_GetProfessionalCompleteInformation",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalid;
                arParams[1] = new SqlParameter("@DOBDOI", SqlDbType.VarChar, 30);
                if (dobdoi.Equals("__-__-____"))
                {
                    arParams[1].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dobdoi.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[1].Value = Convert.ToDateTime(dtformat);
                }
                arParams[2] = new SqlParameter("@JKLicenceDate", SqlDbType.VarChar, 30);
                if (jklicencedate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = jklicencedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }
                arParams[3] = new SqlParameter("@TRLicenceDate", SqlDbType.VarChar, 30);
                if (trlicencedate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = trlicencedate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }
                arParams[4] = new SqlParameter("@TRBodyWeight", SqlDbType.VarChar, 100) { Value = bodyweight };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = userid;
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = tasktype;
                arParams[7] = new SqlParameter("@ProfessionalType", SqlDbType.VarChar, 100) { Value = professionaltype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "sp_ProfessionalCompleteInformation",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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
                conn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(conn))
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

                dtresult = SqlHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, "sp_ImportProfessional", arParams);

                conn.Close();
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dtresult;
        }


        public DataSet GetExport(int prospectusId, string taskType)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParams[0].Value = prospectusId;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = taskType;
                ds = SqlHelper.ExecuteDataset(
                    conn,
                    CommandType.StoredProcedure,
                    "sp_GetExport",
                    arParams);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
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
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[7];
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@ReligionID", SqlDbType.Int);
                arParams[1].Value = religionid;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = mycomments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = userId;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_ProfessionalReligion", arParams));
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[8];
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@ProfessionalAntiPersonID", SqlDbType.Int);
                arParams[1].Value = antipersonid;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@Reason", SqlDbType.VarChar, 100);
                arParams[4].Value = reason;


                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userId;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_ProfessionalAntiPerson", arParams));
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
        }


        public int ProfessionalBackground(
            int professionalId,
            int backgroundid,
            string fromDate,
            string tillDate,
            string mycomments,
            int userId,
            string taskType)
        {
            int checkRecord = 0;
            try
            {
                SqlParameter[] arParams = new SqlParameter[7];
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalId;

                arParams[1] = new SqlParameter("@ProfessionalBackgroundID", SqlDbType.Int);
                arParams[1].Value = backgroundid;

                arParams[2] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromDate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[2].Value = Convert.ToDateTime(dtformat);
                }

                arParams[3] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tillDate.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = mycomments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = userId;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = taskType;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_ProfessionalProfessionalBackground", arParams));
            }
            catch (Exception ex)
            {
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
        }


		public int ProfessionalCount(
			)
		{
			int checkRecord = 0;
			try
			{
			
				checkRecord =
					Convert.ToInt32(
						SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_ProfessionalCount"));
			}
			catch (Exception ex)
			{
				ErrorHandling.CheckEachSteps(ex.StackTrace);
				ErrorHandling.SendErrorToText(ex);
				throw;
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}
			return checkRecord;
		}

		public DataSet GetProfessionalMultipleTillDateString(int professionalid, string tasktype, string fromdate, string actiontype, string value)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[5];
            try
            {
                arParams[0] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[0].Value = professionalid;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = tasktype;
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
                arParams[4] = new SqlParameter("@Value", SqlDbType.VarChar, 100) { Value = value };

                ds = SqlHelper.ExecuteDataset(
                    conn,
                    CommandType.StoredProcedure,
                    "sp_GetProfessionalMultipleTillDateValidationString",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int);
                arParams[0].Value = professionalnameid;

                arParams[1] = new SqlParameter("@JockeyAllowanceStageID", SqlDbType.Int);
                arParams[1].Value = jockeyallowancestageid;

                arParams[2] = new SqlParameter("@StageStartDate", SqlDbType.VarChar, 30);
                if (stagestartdate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = stagestartdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[2].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[3] = new SqlParameter("@StartDayRaceNo", SqlDbType.VarChar, 30);
                arParams[3].Value = startdayraceno;

                arParams[4] = new SqlParameter("@EndDayRaceNo", SqlDbType.VarChar, 30);
                arParams[4].Value = enddayraceno;

                arParams[5] = new SqlParameter("@StageEndDate", SqlDbType.VarChar, 30);
                if (stageenddate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = stageenddate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@MyComments", SqlDbType.VarChar, 1000);
                arParams[7].Value = mycomments;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "[sp_ProfessionalJockeyAllowanceStage]",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[10];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int);
                arParams[0].Value = professionalnameid;

                arParams[1] = new SqlParameter("@PenaltyID", SqlDbType.Int);
                arParams[1].Value = penaltyid;

                arParams[2] = new SqlParameter("@PenaltyReasonID", SqlDbType.Int);
                arParams[2].Value = penaltyreasonid;

                arParams[3] = new SqlParameter("@PenaltyDetail", SqlDbType.VarChar, 500);
                arParams[3].Value = penaltydetail;

                arParams[4] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
                if (fromdate.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = fromdate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[5] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (tilldate.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = tilldate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[6] = new SqlParameter("@WorkOnAppeal", SqlDbType.Int);
                arParams[6].Value = workonappeal;

                arParams[7] = new SqlParameter("@MyComments", SqlDbType.VarChar, 1000);
                arParams[7].Value = mycomments;

                arParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[8].Value = userid;

                arParams[9] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[9].Value = tasktype;

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            conn,
                            CommandType.StoredProcedure,
                            "[sp_ProfessionalPenalty]",
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return checkRecord;
        }
    }
}
