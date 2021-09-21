using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace VKADB
{
    public class MenuDL
    {
        private SqlConnection _conn;
       // Errorhandle err = new Errorhandle();

        public MenuDL()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        }
        public DataSet GetUserDetails(string ActionName)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            try
            {

                arParams[0] = new SqlParameter("@ActionName", SqlDbType.VarChar, 50);
                arParams[0].Value = ActionName;

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_AdminAuthentication", arParams);


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



        public DataSet GetMenuDetail(string UserID, string ActionName)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            try
            {

                arParams[0] = new SqlParameter("@UserId", SqlDbType.VarChar,50);
                arParams[0].Value = UserID;

                arParams[1] = new SqlParameter("@ActionName", SqlDbType.VarChar, 50);
                arParams[1].Value = ActionName;

                ds = SqlHelper.ExecuteDataset(_conn, CommandType.StoredProcedure, "sp_AdminAuthentication", arParams);


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

        public int InsertMenuData(string RequestType, string UserId, string UserType, string UserStatus, int MenuID, char UserFormDisplay,
            char UserAdd, char UserShow, char UserImport, char UserExport, char UserPrint, char UserEdit,char UserDelete)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[13];
            try
            {

                arParams[0] = new SqlParameter("@RequestType", SqlDbType.VarChar, 100);
                arParams[0].Value = RequestType;

                arParams[1] = new SqlParameter("@UserId", SqlDbType.VarChar, 100);
                arParams[1].Value = UserId;

                arParams[2] = new SqlParameter("@UserType", SqlDbType.VarChar, 100);
                arParams[2].Value = UserType;

                arParams[3] = new SqlParameter("@UserStatus", SqlDbType.VarChar, 50);
                arParams[3].Value = UserStatus;


                arParams[4] = new SqlParameter("@MenuID", SqlDbType.Int);
                arParams[4].Value = MenuID;

                arParams[5] = new SqlParameter("@UserFormDisplay", SqlDbType.Char,1);
                arParams[5].Value = UserFormDisplay;

                arParams[6] = new SqlParameter("@UserAdd", SqlDbType.Char, 1);
                arParams[6].Value = UserAdd;

                arParams[7] = new SqlParameter("@UserShow", SqlDbType.Char, 1);
                arParams[7].Value = UserShow;

                arParams[8] = new SqlParameter("@UserImport", SqlDbType.Char, 1);
                arParams[8].Value = UserImport;

                
                arParams[9] = new SqlParameter("@UserExport", SqlDbType.Char, 1);
                arParams[9].Value = UserExport;

                arParams[10] = new SqlParameter("@UserPrint", SqlDbType.Char, 1);
                arParams[10].Value = UserPrint;

                arParams[11] = new SqlParameter("@UserEdit", SqlDbType.Char, 1);
                arParams[11].Value = UserEdit;

                arParams[12] = new SqlParameter("@UserDelete", SqlDbType.Char, 1);
                arParams[12].Value = UserDelete;

                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_AdminManageuser", arParams));
                //checkRecord = Convert.ToInt32(arParams[5].Value);

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
        /// Insert the User Contact Information
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>

        public int InsertUserContactInformation(string ContactName, string contactEmailID, string CAddress, string ContactNo, string ContactCountry)
        {
            int checkRecord = 0;
            SqlParameter[] arParams = new SqlParameter[6];
            try
            {

                arParams[0] = new SqlParameter("@ContactName", SqlDbType.VarChar, 100);
                arParams[0].Value = ContactName;

                arParams[1] = new SqlParameter("@contactEmailID", SqlDbType.VarChar, 500);
                arParams[1].Value = contactEmailID;

                arParams[2] = new SqlParameter("@CAddress", SqlDbType.VarChar, 500);
                arParams[2].Value = CAddress;

                arParams[3] = new SqlParameter("@ContactNo", SqlDbType.VarChar, 15);
                arParams[3].Value = ContactNo;


                arParams[4] = new SqlParameter("@ContactCountry", SqlDbType.VarChar, 500);
                arParams[4].Value = ContactCountry;

                arParams[5] = new SqlParameter("@count", SqlDbType.Int);
                arParams[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_InsertUserContactDetail", arParams);
                checkRecord = Convert.ToInt32(arParams[5].Value);

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

        public int InserUserDetails(string flag, string UserName, string Password, string FirstName, string MiddleName, string LastName, DateTime DateofBirth, 
            string Qualification, string ProfMobileNo,string PersonalMobileNo, string HomeNumber, string EmailAddress, string FatherName, string Address, 
            string RelationStatus, DateTime AnniversaryDate)
        {
            int status = 0;
            SqlParameter[] arParams = new SqlParameter[16];
            try
            {

                arParams[0] = new SqlParameter("@flag", SqlDbType.VarChar, 50);
                arParams[0].Value = flag;

                arParams[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 100);
                arParams[1].Value = UserName;


                arParams[2] = new SqlParameter("@password", SqlDbType.VarChar, 100);
                arParams[2].Value = Password;

                
                arParams[3] = new SqlParameter("@FirstName", SqlDbType.VarChar, 100);
                arParams[3].Value = FirstName;

                arParams[4] = new SqlParameter("@MiddleName", SqlDbType.VarChar, 100);
                arParams[4].Value = MiddleName;

                arParams[5] = new SqlParameter("@LastName", SqlDbType.VarChar, 100);
                arParams[5].Value = LastName;

                arParams[6] = new SqlParameter("@DateofBirth", SqlDbType.DateTime);
                arParams[6].Value = DateofBirth;

                arParams[7] = new SqlParameter("@Qualification", SqlDbType.VarChar, 100);
                arParams[7].Value = Qualification;

                arParams[8] = new SqlParameter("@ProfMobileNo", SqlDbType.VarChar, 100);
                arParams[8].Value = ProfMobileNo;

                arParams[9] = new SqlParameter("@PersonalMobileNo", SqlDbType.VarChar, 100);
                arParams[9].Value = PersonalMobileNo;

                arParams[10] = new SqlParameter("@HomeNumber", SqlDbType.VarChar, 100);
                arParams[10].Value = HomeNumber;

                arParams[11] = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 100);
                arParams[11].Value = EmailAddress;

                arParams[12] = new SqlParameter("@FatherName", SqlDbType.VarChar, 100);
                arParams[12].Value = FatherName;

                arParams[13] = new SqlParameter("@Address", SqlDbType.VarChar, 100);
                arParams[13].Value = Address;

                arParams[14] = new SqlParameter("@RelationStatus", SqlDbType.VarChar, 100);
                arParams[14].Value = RelationStatus;

                arParams[15] = new SqlParameter("@AnniversaryDate", SqlDbType.DateTime);
                arParams[15].Value = AnniversaryDate;

                
                status = Convert.ToInt32(SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, "sp_AdminUserCreation", arParams));
                //checkRecord = Convert.ToInt32(arParams[5].Value);

            }
            catch (Exception ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(ex);
                // err.ErrorEmail(ex);
                ErrorHandling.CheckEachSteps(ex.StackTrace);
                ErrorHandling.SendErrorToText(ex);
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
    

    }
}
