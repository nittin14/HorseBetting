using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace VKATalkDb
{
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class ProspectusDL
    {
        private SqlConnection _conn;

        public ProspectusDL()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
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

        public DataTable GetDropdownBindMultipleValues(string DropDownName,string racedate, string centerid)
        {
            DataTable dt;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {
                arParams[0] = new SqlParameter("@DropdownName", SqlDbType.VarChar, 100);
                arParams[0].Value = DropDownName;
                arParams[1] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
                if (racedate.Equals("__-__-____"))
                {
                    arParams[2].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = racedate.Split('-');
                    DateTime enterDate =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[1].Value = enterDate.ToString("yyyy-MM-dd 00:00:00");
                }
                arParams[2] = new SqlParameter("@CenterID", SqlDbType.VarChar, 100) { Value = centerid };
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetDropdownBindMultipleValues", arParams);
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


		
		public DataTable GetprospectusAutoFillerWithParameters(string autoFillName, string prefix, string value)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {

                arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
                arParams[2] = new SqlParameter("@Value", SqlDbType.VarChar, 100) { Value = value };

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetProspectusAutoFillWithParameter", arParams);
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

		public DataTable GetprospectusAutoFiller(string autoFillName, string prefix, string multiplevalue)
		{

			DataTable dt = null;
			SqlParameter[] arParams = new SqlParameter[4];
			try
			{
				string[] conditions = multiplevalue.Split(',');
				arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
				arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
				arParams[2] = new SqlParameter("@CenteID", SqlDbType.VarChar, 100) { Value = conditions[0].ToString() };
				arParams[3] = new SqlParameter("@SeasonID", SqlDbType.VarChar, 100) { Value = conditions[1].ToString() };

				dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetProspectusNameAutoFillMultipleMasterCombination", arParams);
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

		public DataTable GetProspectusGeneralAutoFill(string autoFillName, string prefix, int masterid, string multiplevalue)
        {

            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {
				//string[] conditions = multiplevalue.Split(',');
				arParams[0] = new SqlParameter("@AutoFillName", SqlDbType.VarChar, 100) { Value = autoFillName };
                arParams[1] = new SqlParameter("@Prefix", SqlDbType.VarChar, 100) { Value = prefix };
                arParams[2] = new SqlParameter("@MasterID", SqlDbType.Int) { Value = masterid};
				//arParams[3] = new SqlParameter("@CenterID", SqlDbType.VarChar, 100) { Value = conditions[0].ToString() };
				//arParams[4] = new SqlParameter("@YearID", SqlDbType.VarChar, 100) { Value = conditions[1].ToString() };
				//arParams[5] = new SqlParameter("@SeasonID", SqlDbType.VarChar, 100) { Value = conditions[2].ToString() };
				//arParams[6] = new SqlParameter("@FromDate", SqlDbType.VarChar, 30);
				//if (conditions[3].Equals("__-__-____"))
				//{
				//	arParams[6].Value = DBNull.Value;
				//}
				//else
				//{
				//	string[] dateString = conditions[3].Split('-');
				//	DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
				//	arParams[6].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
				//}
				//arParams[7] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
				//if (conditions[4].Equals("__-__-____"))
				//{
				//	arParams[7].Value = DBNull.Value;
				//}
				//else
				//{
				//	string[] dateString = conditions[4].Split('-');
				//	DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
				//	arParams[7].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
				//	//arParams[7].Value = Convert.ToDateTime(dtformat);
				//}
				//arParams[8] = new SqlParameter("@RaceDate", SqlDbType.VarChar, 30);
				//if (conditions[5].Equals("__-__-____"))
				//{
				//	arParams[8].Value = DBNull.Value;
				//}
				//else
				//{
				//	string[] dateString = conditions[5].Split('-');
				//	DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
				//	arParams[8].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
				//	//arParams[7].Value = Convert.ToDateTime(dtformat);
				//}
				//arParams[9] = new SqlParameter("@MasterRaceNameID", SqlDbType.VarChar, 100) { Value = conditions[6].ToString() };
				dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetProspectusGeneralAutoFill", arParams);
            }
            catch (Exception ex)
            {

                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
                //throw;
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
            // int checkRecord = 0;
            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {
                arParams[0] = new SqlParameter("@ProspectusID", SqlDbType.Int);
                arParams[0].Value = prospectusId;

                arParams[1] = new SqlParameter("@ProspectusName", SqlDbType.VarChar, 100);
                arParams[1].Value = prospectusname;

                arParams[2] = new SqlParameter("@ProspectusNameAlias", SqlDbType.VarChar, 100);
                arParams[2].Value = prospectusalias;

                arParams[3] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[3].Value = centerid;

                arParams[4] = new SqlParameter("@SeasonID", SqlDbType.Int);
                arParams[4].Value = seasonid;

                arParams[5] = new SqlParameter("@DateofNameChange", SqlDbType.VarChar, 30);
                if (dateofnamechange.Equals("__-__-____"))
                {
                    arParams[5].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dateofnamechange.Split('-');
                    DateTime enter_date =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[5].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[5].Value = Convert.ToDateTime(dtformat);
                }

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[6].Value = myComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = userId;

                arParams[8] = new SqlParameter("@Tasktype", SqlDbType.VarChar, 50);
                arParams[8].Value = taskType;
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_PropectusMasterRaceName", arParams);

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
            // int checkRecord = 0;
            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[12];
            try
            {
                arParams[0] = new SqlParameter("@ProspectusID", SqlDbType.Int);
                arParams[0].Value = prospectusId;

                arParams[1] = new SqlParameter("@ProspectusMasterID", SqlDbType.Int) { Value = prospectusmasterid };
                arParams[2] = new SqlParameter("@ProspectusName", SqlDbType.VarChar, 100) { Value = prospectusname };
                arParams[3] = new SqlParameter("@ProspectusNameAlias", SqlDbType.VarChar, 100);
                arParams[3].Value = prospectusalias;

                arParams[4] = new SqlParameter("@CenterID", SqlDbType.Int);
                arParams[4].Value = centerid;

                arParams[5] = new SqlParameter("@SeasonID", SqlDbType.Int);
                arParams[5].Value = seasonid;

                arParams[6] = new SqlParameter("@YearID", SqlDbType.Int);
                arParams[6].Value = yearid;

                arParams[7] = new SqlParameter("@DateofNameChange", SqlDbType.VarChar, 30);
                if (dateofnamechange.Equals("__-__-____"))
                {
                    arParams[7].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dateofnamechange.Split('-');
                    DateTime enter_date =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[7].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[7].Value = Convert.ToDateTime(dtformat);
                }

                arParams[8] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[8].Value = myComments;

                arParams[9] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[9].Value = userId;

                arParams[10] = new SqlParameter("@Tasktype", SqlDbType.VarChar, 50);
                arParams[10].Value = taskType;

                arParams[11] = new SqlParameter("@MasterRaceNameStatus", SqlDbType.VarChar, 50);
                arParams[11].Value = RaceNameStatus;

                ErrorHandling.CheckEachSteps("prospectusId" + prospectusId + "/" +
                    "prospectusmasterid" + prospectusmasterid + "/" +
                    "prospectusname" + prospectusname + "/" +
                    "prospectusalias" + prospectusalias + "/" +
                    "centerid" + centerid + "/" +
                    "seasonid" + seasonid + "/" +
                    "yearid" + yearid + "/" +
                    "dateofnamechange" + dateofnamechange + "/" +
                    "myComments" + myComments + "/" +
                    "userId" + userId + "/" +
                    "taskType" + taskType + "/" +
                    "RaceNameStatus" + RaceNameStatus);

                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_PropectusGeneralRaceName", arParams);

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


        public DataTable MasterRaceNameNew(
            int prospectusId,
            string prospectusname,
            string prospectusalias,
            string dateofnamechange,
            string myComments,
            int userId,
            string taskType)
        {
            // int checkRecord = 0;
            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {
                arParams[0] = new SqlParameter("@ProspectusID", SqlDbType.Int);
                arParams[0].Value = prospectusId;

                arParams[1] = new SqlParameter("@MasterRaceName", SqlDbType.VarChar, 100);
                arParams[1].Value = prospectusname;

                arParams[2] = new SqlParameter("@MasterRaceNameAlias", SqlDbType.VarChar, 100);
                arParams[2].Value = prospectusalias;

                arParams[3] = new SqlParameter("@DateofNameChange", SqlDbType.VarChar, 30);
                if (dateofnamechange.Equals("__-__-____"))
                {
                    arParams[3].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dateofnamechange.Split('-');
                    DateTime enter_date =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[3].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[3].Value = Convert.ToDateTime(dtformat);
                }

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = myComments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = userId;

                arParams[6] = new SqlParameter("@Tasktype", SqlDbType.VarChar, 50);
                arParams[6].Value = taskType;
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_PropectusMasterRaceNameNew", arParams);

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
            // int checkRecord = 0;
            DataTable dt = null;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {
                arParams[0] = new SqlParameter("@ProspectusMasterID", SqlDbType.Int);
                arParams[0].Value = prospectusmasterId;
                arParams[1] = new SqlParameter("@ProspectusGeneralID", SqlDbType.Int) { Value = prospectgeneralracenameid };
                arParams[2] = new SqlParameter("@GeneralRaceName", SqlDbType.VarChar, 100);
                arParams[2].Value = prospectusname;

                arParams[3] = new SqlParameter("@GeneralRaceNameAlias", SqlDbType.VarChar, 100);
                arParams[3].Value = prospectusalias;

                arParams[4] = new SqlParameter("@DateofNameChange", SqlDbType.VarChar, 30);
                if (dateofnamechange.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = dateofnamechange.Split('-');
                    DateTime enter_date =
                        Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = myComments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userId;

                arParams[7] = new SqlParameter("@Tasktype", SqlDbType.VarChar, 50);
                arParams[7].Value = taskType;
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_PropectusGeneralRaceNameNew", arParams);

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


        public DataSet GetProspectusNameWithCombination(int prospectusId, string taskType)
        {
            DataSet ds = null;
            ;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@ProspectusID", SqlDbType.Int);
                arParams[0].Value = prospectusId;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = taskType;
                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetProspectusNameWithCombination",
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

        public DataSet GetTillDateGridview(int masterracenameid)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                arParams[0] = new SqlParameter("@MasterRaceNameID", SqlDbType.Int);
                arParams[0].Value = masterracenameid;
                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetTillDate",
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


        public DataSet GetProspectusNameWithCombinationGeneral(int prospectusId, string taskType)
        {
            DataSet ds = null;
            ;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {
                arParams[0] = new SqlParameter("@ProspectusID", SqlDbType.Int);
                arParams[0].Value = prospectusId;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = taskType;
                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetProspectusNameWithCombinationGeneral",
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

        public DataTable ImportExcel(DataTable dt, string PageName, int globalid)
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

                    copy.DestinationTableName = "dbo.Master_Import";
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

        /// <summary>
        /// Return Complete Main Horse form Information
        /// </summary>
        /// <param name="prospectusid"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataSet GetProspectusCompleteInformation(int prospectusid, string TaskType)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@ProspectusID", SqlDbType.Int);
                arParams[0].Value = prospectusid;

                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = TaskType;

                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetProspectusCompleteInformation",
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
        /// Return Complete Main Horse form Information
        /// </summary>
        /// <param name="prospectusid"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataSet GetProspectusCompleteInformationGeneral(int prospectusid, string TaskType)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@ProspectusID", SqlDbType.Int);
                arParams[0].Value = prospectusid;

                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = TaskType;

                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetProspectusCompleteInformationGeneral",
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


        public DataSet sp_GetProspectusMasterInGeneral(int prospectusid, string TaskType)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@ProspectusMasterRaceID", SqlDbType.Int);
                arParams[0].Value = prospectusid;

                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = TaskType;

                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetProspectusMasterInGeneral",
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
        /// Get HorseID of the horse
        /// </summary>
        /// <param name="horseName"></param>
        /// <param name="dob"></param>
        /// <returns></returns>
        public DataTable GetProspectusId(string MasterRaceName, string centerid, string seassionid)
        {
            int prospectusId = 0;
            DataTable dt;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {
                arParams[0] = new SqlParameter("@MasterRaceName", SqlDbType.VarChar, 100) { Value = MasterRaceName };
                arParams[1] = new SqlParameter("@CenterName", SqlDbType.VarChar, 50) { Value = centerid };
                arParams[2] = new SqlParameter("@SeasonName", SqlDbType.VarChar, 50) { Value = seassionid };
                
                dt=SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetMasterRaceID", arParams);
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

        public DataTable GetProspectusGeneralId(string MasterRaceName, string centername, string sessionname, string yearname)
        {
            int prospectusId = 0;
            DataTable dt;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@MasterRaceName", SqlDbType.VarChar, 100) { Value = MasterRaceName };
                arParams[1] = new SqlParameter("@CenterName", SqlDbType.VarChar, 50) { Value = centername };
                arParams[2] = new SqlParameter("@SeasonName", SqlDbType.VarChar, 50) { Value = sessionname };
                arParams[3] = new SqlParameter("@YearName", SqlDbType.VarChar, 50) { Value = yearname };

                dt=SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetGeneralRaceID", arParams);
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


		public int GetProspectusGeneralLastId(string pagename)
		{
			int prospectusId = 0;
			SqlParameter[] arParams = new SqlParameter[1];
			try
			{
				arParams[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 100) { Value = pagename };
				//arParams[1] = new SqlParameter("@CenterName", SqlDbType.VarChar, 50) { Value = centername };
				//arParams[2] = new SqlParameter("@SeasonName", SqlDbType.VarChar, 50) { Value = sessionname };
				//arParams[3] = new SqlParameter("@YearName", SqlDbType.VarChar, 50) { Value = yearname };

				prospectusId =
					Convert.ToInt32(
						SqlHelper.ExecuteScalar(_conn, CommandType.StoredProcedure, "sp_GetLastGeneralRaceID", arParams));
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
			return prospectusId;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="racemasterid"></param>
		/// <param name="TaskType"></param>
		/// <returns></returns>
		public DataSet GetProspectusTillDateValidation(int racemasterid, string taskType, string fromyear, string actiontype)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[4];
            try
            {
                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = racemasterid;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = taskType;
                arParams[2] = new SqlParameter("@FromYear", SqlDbType.VarChar, 100) { Value = fromyear };
                arParams[3] = new SqlParameter("@Action", SqlDbType.VarChar, 100) { Value = actiontype };
              
                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetProspectusTillDateValidation",
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

        public DataSet GetGeneralProspectusTillDateValidation(int racemasterid, string taskType, string btntext)
        {

            DataSet ds = null;
            ;
            SqlParameter[] arParams = new SqlParameter[3];
            try
            {
                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = racemasterid;
                arParams[1] = new SqlParameter("@TaskType", SqlDbType.VarChar, 100);
                arParams[1].Value = taskType;
                arParams[2] = new SqlParameter("@BtnName", SqlDbType.VarChar, 10) { Value = btntext };

                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "sp_GetGeneralProspectusTillDateValidation",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = masterraceid;

                arParams[1] = new SqlParameter("@MemoirTypeID", SqlDbType.Int);
                arParams[1].Value = memoirtypeID;

                arParams[2] = new SqlParameter("@MemoirNameID", SqlDbType.Int);
                arParams[2].Value = memoirnameID;

                arParams[3] = new SqlParameter("@FromYearID", SqlDbType.Int){ Value= fromyearid};
                arParams[4] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[5] = new SqlParameter("@OtherDetails", SqlDbType.VarChar, 100) { Value = otherdetails };

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
                            "sp_RaceInMemoryOf",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = masterraceid;

                arParams[1] = new SqlParameter("@MemoirType", SqlDbType.VarChar, 500);
                arParams[1].Value = memoirtype;

                arParams[2] = new SqlParameter("@MemoirDescription", SqlDbType.VarChar, 500);
                arParams[2].Value = memoirname;

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
                arParams[5] = new SqlParameter("@OtherDetails", SqlDbType.VarChar, 100) { Value = otherdetails };

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
                            "sp_GeneralRaceInMemoryOf",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = masterraceid;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = professionalid;

                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int){Value = fromyearid};
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@OtherDetails", SqlDbType.VarChar, 100) { Value = otherdetails };

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusSponcer",
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


        public int MomenttoType(
            int masterraceid,
            int momenttotypeid,
           int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int){ Value = masterraceid };
                arParams[1] = new SqlParameter("@MomenttoTypeID", SqlDbType.Int) { Value = momenttotypeid};
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int){ Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMomenttoType",
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


        public int MomenttoCost(
           int masterraceid,
           string momenttoCost,
          int fromyearid,
           int tillyearid,
           string mycomments,
           int userid,
           string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@MomenttoCost", SqlDbType.VarChar, 500) { Value = momenttoCost };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMomenttoCost",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = masterraceid;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = professionalid;

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
                arParams[4] = new SqlParameter("@OtherDetails", SqlDbType.VarChar, 100) { Value = otherdetails };

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusGeneralSponcer",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = masterraceid;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = professionalid;

                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };

                arParams[4] = new SqlParameter("@OtherDetails", SqlDbType.VarChar, 100) { Value = otherdetails };

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusPresenter",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = masterraceid;

                arParams[1] = new SqlParameter("@ProfessionalID", SqlDbType.Int);
                arParams[1].Value = professionalid;

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
                arParams[4] = new SqlParameter("@OtherDetails", SqlDbType.VarChar, 100) { Value = otherdetails };

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[5].Value = mycomments;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = userid;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = tasktype;
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_GeneralProspectusPresenter",
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


        public int MasterDistance(
            int masterraceid,
            int distanceid,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype,
            string TillDate)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = masterraceid;

                arParams[1] = new SqlParameter("@DistanceID", SqlDbType.Int);
                arParams[1].Value = distanceid;

                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = mycomments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = userid;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = tasktype;

                arParams[7] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[7].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[7].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterDistance",
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


        public int GeneralMasterDistance(
            int masterraceid,
            int distanceid,
            string fromdate,
            string tilldate,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = masterraceid;

                arParams[1] = new SqlParameter("@DistanceID", SqlDbType.Int);
                arParams[1].Value = distanceid;

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
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_GeneralProspectusDistance",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = masterraceid;

                arParams[1] = new SqlParameter("@RaceType", SqlDbType.VarChar,50);
                arParams[1].Value = racetype;

                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100);
                arParams[4].Value = mycomments;

                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[5].Value = userid;

                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[6].Value = tasktype;

                
                arParams[7] = new SqlParameter("@Category", SqlDbType.VarChar, 500);
                arParams[7].Value = category;

                arParams[8] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[8].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[8].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterRaceType",
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



        public int GeneralRaceType(
            int masterraceid,
            string racetype,
            string fromdate,
            string tilldate,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int);
                arParams[0].Value = masterraceid;

                arParams[1] = new SqlParameter("@RaceType", SqlDbType.VarChar, 50);
                arParams[1].Value = racetype;

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
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusGeneralRaceType",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid } ;
                arParams[1] = new SqlParameter("@HandicapRatingRangeID", SqlDbType.Int) { Value = handicapratingrangeid };
                arParams[2] = new SqlParameter("@CategoryID", SqlDbType.Int) { Value = categoryid };

                arParams[3] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[4] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                arParams[8] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[8].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[8].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterHandicapRatingRange",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@HandicapRatingRangeID", SqlDbType.Int) { Value = handicapratingrangeid };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                arParams[7] = new SqlParameter("@TillDate", SqlDbType.VarChar, 30);
                if (TillDate.Equals("__-__-____"))
                {
                    arParams[7].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = TillDate.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[7].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                }

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterEligbleHandicapRatingRange",
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

       
        public int MasterAgeCondition(
            int masterraceid,
            int ageconditionid,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@AgeConditionID", SqlDbType.Int) { Value = ageconditionid };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterAgeCondition",
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


        public int MasterMillion(
            int masterraceid,
            string bunch,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@Million", SqlDbType.VarChar, 50) { Value = bunch };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                //checkRecord =
                //    Convert.ToInt32(
                //        SqlHelper.ExecuteScalar(
                //            _conn,
                //            CommandType.StoredProcedure,
                //            "sp_ProspectusMasterBunch",
                //            arParams));

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterMillion",
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

        public int MasterBunch(
            int masterraceid,
            string million,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@Bunch", SqlDbType.VarChar, 50) { Value = million };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterBunch",
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


        public int MasterRaceStatus(
            int masterraceid,
            string racestatus,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@RaceStatus", SqlDbType.VarChar, 50) { Value = racestatus };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterRaceStatus",
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


        public int MasterSweepStake(
            int masterraceid,
            string sweepstake,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@SweepStake", SqlDbType.VarChar, 50) { Value = sweepstake };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterSweepStake",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@Age", SqlDbType.Int) { Value = age };
                arParams[2] = new SqlParameter("@HandicapWeight", SqlDbType.VarChar, 50) { Value = weight };
                arParams[3] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[4] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                arParams[8] = new SqlParameter("@HorseSexID", SqlDbType.VarChar, 50) { Value = horsesexid };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterHandicapWeightAsPerAge",
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

        public int MasterClassic(
            int masterraceid,
            string classic,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@Classic", SqlDbType.VarChar, 50) { Value = classic };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterClassic",
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


        public int MasterGraded(
           int masterraceid,
           string graded,
           int fromyearid,
           int tillyearid,
           string mycomments,
           int userid,
           string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@Graded", SqlDbType.VarChar, 50) { Value = graded };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid }; 
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterGraded",
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


        public int MasterGradeNo(
           int masterraceid,
           string graded,
           int fromyearid,
           int tillyearid,
           string mycomments,
           int userid,
           string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@GradeNo", SqlDbType.VarChar, 50) { Value = graded };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid }; 
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterGradeNo",
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


        public int AddProspectus(
            int prospectusid,
            string abbreviation,
            string foreignhorseallowed,
            string maidenhorseallowed,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[6];
            try
            {

                arParams[0] = new SqlParameter("@ProspectusID", SqlDbType.Int);
                arParams[0].Value = prospectusid;
                arParams[1] = new SqlParameter("@Abbreviation", SqlDbType.VarChar, 100) { Value = abbreviation};
                arParams[2] = new SqlParameter("@ForeginHorseAllowed", SqlDbType.VarChar, 100) { Value = foreignhorseallowed };
                arParams[3] = new SqlParameter("@UserID", SqlDbType.Int){ Value=userid };
                arParams[4] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50){ Value=tasktype };
                arParams[5] = new SqlParameter("@MaidenHorseAllowed", SqlDbType.VarChar, 100) { Value = maidenhorseallowed };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_AddProspectus",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[9];
            try
            {

                arParams[0] = new SqlParameter("@ProspectusID", SqlDbType.Int);
                arParams[0].Value = prospectusid;
                arParams[1] = new SqlParameter("@RaceDay", SqlDbType.VarChar, 100) { Value = raceday };
                arParams[2] = new SqlParameter("@NameofRaceDay", SqlDbType.VarChar, 100) { Value = nameofraceday };
                arParams[3] = new SqlParameter("@TimeSlot", SqlDbType.VarChar, 100) { Value = timeslotofraceday };
                arParams[4] = new SqlParameter("@MainRaceofDay", SqlDbType.Int) { Value = mainraceofday };
                arParams[5] = new SqlParameter("@SerialNumber", SqlDbType.VarChar, 100) { Value = serialnumber };
                arParams[6] = new SqlParameter("@YearofBirth", SqlDbType.VarChar, 100) { Value = yearofbirth };
                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_AddGeneralProspectus",
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
        /// Remove from Prospectus Master 
        /// </summary>
        /// <param name="prospectusid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int ProspectusMasterRemove(
           int prospectusid,
           int userid)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {

                arParams[0] = new SqlParameter("@ProspectusID", SqlDbType.Int);
                arParams[0].Value = prospectusid;
                arParams[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_PropectusMasterRemove",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[11];
            try
            {

                arParams[0] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int);
                arParams[0].Value = generalracenameid;

                arParams[1] = new SqlParameter("@Observation", SqlDbType.VarChar, 1000);
                arParams[1].Value = observation;

                arParams[2] = new SqlParameter("@AimedDuration", SqlDbType.VarChar, 1000);
                arParams[2].Value = aimedduration;

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

                arParams[5] = new SqlParameter("@Reason", SqlDbType.VarChar, 1000);
                arParams[5].Value = reason;

                arParams[6] = new SqlParameter("@MyComments", SqlDbType.VarChar, 1000);
                arParams[6].Value = MyComments;

                arParams[7] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[7].Value = UserID;

                arParams[8] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[8].Value = TaskType;

				arParams[9] = new SqlParameter("@RelatedTypeID", SqlDbType.VarChar, 50);
				arParams[9].Value = typeid;


				arParams[10] = new SqlParameter("@RelatedNameID", SqlDbType.VarChar, 50);
				arParams[10].Value = relatednameid;

				checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_Observation",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[13];
            try
            {

                arParams[0] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int);
                arParams[0].Value = generalracenameid;

                arParams[1] = new SqlParameter("@DateTypeID", SqlDbType.Int);
                arParams[1].Value = datetypeid;

                arParams[2] = new SqlParameter("@Allowed", SqlDbType.VarChar, 100);
                arParams[2].Value = allowed;

                arParams[3] = new SqlParameter("@DateTerm", SqlDbType.VarChar, 100);
                arParams[3].Value = dateterm;

                arParams[4] = new SqlParameter("@Date", SqlDbType.VarChar, 30);
                if (date.Equals("__-__-____"))
                {
                    arParams[4].Value = DBNull.Value;
                }
                else
                {
                    string[] dateString = date.Split('-');
                    DateTime enter_date = Convert.ToDateTime(dateString[2] + "-" + dateString[1] + "-" + dateString[0]);
                    arParams[4].Value = enter_date.ToString("yyyy-MM-dd 00:00:00");
                    //arParams[4].Value = Convert.ToDateTime(dtformat);
                }
                arParams[5] = new SqlParameter("@Time", SqlDbType.VarChar, 100);
                arParams[5].Value = time;

                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int);
                arParams[6].Value = UserID;

                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50);
                arParams[7].Value = TaskType;

                arParams[8] = new SqlParameter("@Fees", SqlDbType.Int);
                arParams[8].Value = fees;

                arParams[9] = new SqlParameter("@ReasonOfChange", SqlDbType.VarChar, 1000);
                arParams[9].Value = reasonofchange;


                arParams[10] = new SqlParameter("@MyComments", SqlDbType.VarChar, 1000);
                arParams[10].Value = mycomments;

				arParams[11] = new SqlParameter("@AmountPercentage", SqlDbType.VarChar, 50);
				arParams[11].Value = amountpercentage;

				arParams[12] = new SqlParameter("@AmountInWords", SqlDbType.VarChar, 100);
				arParams[12].Value = amountinwords;

				checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_GeneralDates",
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
        /// <param name="prospectusid"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataSet GetProfessionalProfileDetail(int professionalnameid)
        {

            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {

                arParams[0] = new SqlParameter("@ProfessionalNameID", SqlDbType.Int);
                arParams[0].Value = professionalnameid;

                ds = SqlHelper.ExecuteDataset(
                    _conn,
                    CommandType.StoredProcedure,
                    "GetProfessionalProfileDetail",
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


        public int PermanentCondition(
            int masterraceid,
            int permanentconditionid,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@PermanentConditionID", SqlDbType.Int) { Value = permanentconditionid };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterPermanentCondition",
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


        public int OtherCondition(
            int masterraceid,
            int permanentconditionid,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@PermanentConditionID", SqlDbType.Int) { Value = permanentconditionid };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterOtherCondition",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@ProfessionalBackgroundID", SqlDbType.Int) { Value = professionalbackgroundid };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@OtherDetails", SqlDbType.VarChar, 1000) { Value = othercomment };
                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 1000) { Value = mycomments };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterProfBackground",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[10];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@SrNo", SqlDbType.VarChar, 100) { Value = srno };
                arParams[2] = new SqlParameter("@PartNo", SqlDbType.VarChar, 100) { Value = partno };
                arParams[3] = new SqlParameter("@SecNo", SqlDbType.VarChar, 100) { Value = secno };
                arParams[4] = new SqlParameter("@HWPConditionID", SqlDbType.Int) { Value = hwpconditionid };
                arParams[5] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[6] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[7] = new SqlParameter("@MyComments", SqlDbType.VarChar, 1000) { Value = mycomments };
                arParams[8] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[9] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterHWPCondition",
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

        public int MasterBunchCondition(
            int masterraceid,
            int bunchconditionid,
            int fromyearid,
            int tillyearid,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@BunchConditionID", SqlDbType.Int) { Value = bunchconditionid };
                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterBunchCondition",
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

        public int GeneralSeasonalCondition(
          int generalraceid,
          int seasonaconditionalid,
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

                arParams[0] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int);
                arParams[0].Value = generalraceid;

                arParams[1] = new SqlParameter("@SeasonalConditionID", SqlDbType.Int);
                arParams[1].Value = seasonaconditionalid;

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
                            "sp_GeneralSeasonalCondition",
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


        public int RaceCardCondition(
         int generalraceid,
         int racecardconditionid,
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

                arParams[0] = new SqlParameter("@GeneralRaceNameID", SqlDbType.Int);
                arParams[0].Value = generalraceid;

                arParams[1] = new SqlParameter("@RaceCardConditionID", SqlDbType.Int);
                arParams[1].Value = racecardconditionid;

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
                            "sp_GeneralRaceCardCondition",
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@GenderID", SqlDbType.Int) { Value = genderid };
                arParams[2] = new SqlParameter("@HandicapWeight", SqlDbType.VarChar, 50) { Value = handicapweight };
                arParams[3] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[4] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };
                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterHandicapWeight",
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


        public int HandicapRaceHistory(
            int masterraceid,
            string srnumber,
            string history,
            string mycomments,
            int userid,
            string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[6];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@SNo", SqlDbType.VarChar,100) { Value = srnumber };
                arParams[2] = new SqlParameter("@RaceHistory", SqlDbType.VarChar, 1000) { Value = history };
                arParams[3] = new SqlParameter("@MyComments", SqlDbType.VarChar, 1000) { Value = mycomments };
                arParams[4] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[5] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };
                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterRaceHistory",
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



        /// <summary>
        /// Bind Drop Down Value
        /// </summary>
        /// <param name="DropDownName"></param>
        /// <returns></returns>
        public DataTable GetMasterRaceDetail(string masterraceid)
        {
            DataTable dt;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {
                arParams[0] = new SqlParameter("@MasterRaceID", SqlDbType.VarChar, 100);
                arParams[0].Value = masterraceid;
                dt = SqlHelper.ExecuteDataTable(_conn, CommandType.StoredProcedure, "sp_GetMasterRaceDetail", arParams);
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

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[8];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@StakeMoneyAdditionMID", SqlDbType.Int) { Value = additiontypeid };
                arParams[2] = new SqlParameter("@Amount", SqlDbType.VarChar, 50) { Value = amount };

                arParams[3] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[4] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };

                arParams[5] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[6] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[7] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterStakeMoneyAddition",
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


        public int RaceAbbriviation(
           int masterraceid,
           int additiontypeid,
           int fromyearid,
           int tillyearid,
           string mycomments,
           int userid,
           string tasktype)
        {

            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[7];
            try
            {

                arParams[0] = new SqlParameter("@RaceMasterID", SqlDbType.Int) { Value = masterraceid };
                arParams[1] = new SqlParameter("@RaceAbbreviationMID", SqlDbType.Int) { Value = additiontypeid };

                arParams[2] = new SqlParameter("@FromYearID", SqlDbType.Int) { Value = fromyearid };
                arParams[3] = new SqlParameter("@TillYearID", SqlDbType.Int) { Value = tillyearid };

                arParams[4] = new SqlParameter("@MyComments", SqlDbType.VarChar, 100) { Value = mycomments };
                arParams[5] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userid };
                arParams[6] = new SqlParameter("@TaskType", SqlDbType.VarChar, 50) { Value = tasktype };

                checkRecord =
                    Convert.ToInt32(
                        SqlHelper.ExecuteScalar(
                            _conn,
                            CommandType.StoredProcedure,
                            "sp_ProspectusMasterRaceAbbreviation",
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
    }
}
