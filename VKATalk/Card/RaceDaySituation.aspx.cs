using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using VKATalkBusinessLayer;

namespace VKATalk.Card
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;
    using System.Web.WebSockets;

    using OfficeOpenXml;
    using VKATalk.Common;

    public partial class RaceDaySituation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxDeclarationEnterDate.Text = CommonMethods.CurrentDate();
                BindDropDown(drpdwnGoing, "MasterGoing", "Going", "GoingMID");
                BindDropDown(drpdwnWeather, "MasterWeather", "Weather", "WeatherMID");
                drpdwnGoing.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                drpdwnWeather.Items.Insert(0, new ListItem("-- Please select --", "-1"));
            }
        }

        protected void txtbxDivisionRaceDate_OnTextChanged(object sender, EventArgs e)
        {
            var dt = new CardsBL().GetRaceCenterName(txtbxDivisionRaceDate.Text);
            if (dt.Rows.Count > 0)
            {
                drpdwnCenterName.DataSource = dt;
                drpdwnCenterName.DataTextField = "CenterName";
                drpdwnCenterName.DataValueField = "ID";
                drpdwnCenterName.DataBind();
                drpdwnCenterName.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                drpdwnCenterName.Focus();
            }
        }

        /// <summary>
        /// AddStudOwnerList
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddGoingList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CommentatorList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0])));
            }
            return horseList;
        }


        /// <summary>
        /// AddStudOwnerList
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddOtherFactorList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("AddReportList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0])));
            }
            return horseList;
        }


        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                if (drpdwnCenterName.SelectedItem.Value.Equals("-1"))
                {
                    GvShowALL.DataSource = new DataTable();
                    GvShowALL.DataBind();
                }
                else
                {
                    ShowDeclaration();
                }
                
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        
        protected void GvShowALL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var globalid = 0;
                var realracetime = string.Empty;
                var penetroreading = string.Empty;
                var temperature = string.Empty;
                var otherfactor = string.Empty;
                var result = 0;
                GridViewRow row = GvShowALL.SelectedRow;
               // HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                //HiddenField hdnfieldgeneralraceid = (HiddenField)row.FindControl("hdnfieldGeneralRaceID");

                var dataKey = GvShowALL.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    globalid = Convert.ToInt32(dataKey.Value);
                }

                TextBox txtbxFinishTimeMMG = (TextBox)row.FindControl("txtbxFinishTimeMMG");
                TextBox txtbxFinishTimeSSG = (TextBox)row.FindControl("txtbxFinishTimeSSG");

                if (!(txtbxFinishTimeMMG.Text.Equals("") || txtbxFinishTimeSSG.Text.Equals("")))
                {
                    realracetime = txtbxFinishTimeMMG.Text + ":" + txtbxFinishTimeSSG.Text;
                }

                TextBox txtbxPenetrometerG = (TextBox)row.FindControl("txtbxPenetrometerG");
                if (!txtbxPenetrometerG.Text.Equals(""))
                    penetroreading = txtbxPenetrometerG.Text;

                DropDownList drpdwnGoingG = (DropDownList)row.FindControl("drpdwnGoingG");
                DropDownList drpdwnWeatherG = (DropDownList)row.FindControl("drpdwnWeatherG");


                TextBox txtbxtemperatureG = (TextBox)row.FindControl("txtbxtemperatureG");
                if (!txtbxtemperatureG.Text.Equals(""))
                    temperature = txtbxtemperatureG.Text;

                CheckBox chkbxRainInMorningG = (CheckBox)row.FindControl("chkbxRainInMorningG");
                CheckBox chkbxRainBeforeRaceG = (CheckBox)row.FindControl("chkbxRainBeforeRaceG");
                CheckBox chkbxRainDuringRaceG = (CheckBox)row.FindControl("chkbxRainDuringRaceG");

                TextBox txtbxOtherFactor = (TextBox)row.FindControl("txtbxOtherFactor");
                if (!txtbxOtherFactor.Text.Equals(""))
                    otherfactor = txtbxOtherFactor.Text;

                result = new CardsBL().RaceCardSituation(globalid, realracetime, Convert.ToDouble(penetroreading), 
                    Convert.ToInt32(drpdwnGoingG.SelectedItem.Value), Convert.ToInt32(drpdwnWeatherG.SelectedItem.Value), temperature, 
                    Convert.ToBoolean(chkbxRainInMorningG.Checked), Convert.ToBoolean(chkbxRainBeforeRaceG.Checked), 
                    Convert.ToBoolean(chkbxRainDuringRaceG.Checked),otherfactor,1,"Update");
                if (result == 2)
                {
                    ShowDeclaration();
                    var message = "Record updated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    var message = "Issue in Record.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        private void ShowDeclaration()
        {
            var dt = new CardsBL().GetAcceptanceDivisionDetail(
                    txtbxDivisionRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "RaceDaySituation");
            if (dt.Rows.Count > 0)
            {
                lblSeason.Text = dt.Rows[0][13].ToString();
                lblYear.Text = dt.Rows[0][14].ToString();
               // GvShowALL.Columns[0].Visible = true;
                GvShowALL.DataSource = dt;
                GvShowALL.DataBind();
            }
            else
            {
                //dvgridview.Visible = false;
               // GvShowALL.Columns[0].Visible = false;
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
            }
            if (dt.Rows[0][0].ToString().Equals("Yes"))
            {
                GvShowALL.Columns[0].Visible = true;
            }
            else
            {
                GvShowALL.Columns[0].Visible = false;
            }
        }

        protected void GvShowALL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdfieldRealTime = (e.Row.FindControl("hdnfieldRealRaceTime") as HiddenField);
                if (!hdfieldRealTime.Value.Equals(""))
                {
                    string[] realtime = hdfieldRealTime.Value.Split(':');
                    TextBox txtbxFinishTimeMMG = (e.Row.FindControl("txtbxFinishTimeMMG") as TextBox);
                    TextBox txtbxFinishTimeSSG = (e.Row.FindControl("txtbxFinishTimeSSG") as TextBox);
                    txtbxFinishTimeMMG.Text = realtime[0];
                    txtbxFinishTimeSSG.Text = realtime[1];
                }
                HiddenField hdnfieldRainInMorning = (e.Row.FindControl("hdnfieldRainInMorning") as HiddenField);
                CheckBox chkbxRainInMorning = (e.Row.FindControl("chkbxRainInMorningG") as CheckBox);
                if (hdnfieldRainInMorning.Value.Equals("True"))
                    chkbxRainInMorning.Checked = true;

                HiddenField hdnfieldRainBeforeRace = (e.Row.FindControl("hdnfieldRainBeforeRace") as HiddenField);
                CheckBox chkbxRainBeforeRace = (e.Row.FindControl("chkbxRainBeforeRaceG") as CheckBox);
                if (hdnfieldRainBeforeRace.Value.Equals("True"))
                    chkbxRainBeforeRace.Checked = true;

                HiddenField hdnfieldRainDuringRace = (e.Row.FindControl("hdnfieldRainDuringRace") as HiddenField);
                CheckBox chkbxRainDuringRace = (e.Row.FindControl("chkbxRainDuringRaceG") as CheckBox);
                if (hdnfieldRainDuringRace.Value.Equals("True"))
                    chkbxRainDuringRace.Checked = true;

                

                HiddenField hdfieldGoing = (e.Row.FindControl("hdfieldGoing") as HiddenField);
                DropDownList drpdwnGoingG = (e.Row.FindControl("drpdwnGoingG") as DropDownList);
                BindDropDown(drpdwnGoingG, "MasterGoing", "Going", "GoingMID");
                drpdwnGoingG.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                if(!hdfieldGoing.Value.Equals("&nbsp"))
                {
                    drpdwnGoingG.ClearSelection();
                    drpdwnGoingG.Items.FindByText(hdfieldGoing.Value).Selected = true;
                }

                HiddenField hdfieldWeather = (e.Row.FindControl("hdfieldWeather") as HiddenField);
                DropDownList drpdwnWeatherG = (e.Row.FindControl("drpdwnWeatherG") as DropDownList);
                BindDropDown(drpdwnWeatherG, "MasterWeather", "Weather", "WeatherMID");
                drpdwnWeatherG.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                if (!hdfieldWeather.Value.Equals("&nbsp"))
                {
                    drpdwnWeatherG.ClearSelection();
                    drpdwnWeatherG.Items.FindByText(hdfieldWeather.Value).Selected = true;
                }
            }

        }  



        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = new CardsBL().GetDropdownBind(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
        }

        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddJockeyList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CardDeclarationJockeyList", prefixText);

            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //  horseList.Add(dt.Rows[i][1].ToString());
                horseList.Add(
                    AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0])));
            }
            return horseList;
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var divisionraceid = 0;
            DataTable dtDeclaration = new DataTable("Declaration");
            try
            {
                dtDeclaration.Columns.Add("DivisionRaceID_Fk", typeof(int));
                dtDeclaration.Columns.Add("RealRaceTime", typeof(string));
                dtDeclaration.Columns.Add("FalseRails", typeof(bool));
                dtDeclaration.Columns.Add("FRMeasurement", typeof(string));
                dtDeclaration.Columns.Add("PenetrometerReading", typeof(Double));
                dtDeclaration.Columns.Add("GoingMID", typeof(int));
                dtDeclaration.Columns.Add("WeatherMID", typeof(int));
                dtDeclaration.Columns.Add("Temperature", typeof(string));
                dtDeclaration.Columns.Add("RainInMorning", typeof(bool));
                dtDeclaration.Columns.Add("RainBeforeRace", typeof(bool));
                dtDeclaration.Columns.Add("RainDuringRace", typeof(bool));
                dtDeclaration.Columns.Add("OtherFactor", typeof(string));
                dtDeclaration.Columns.Add("CreatedDate", typeof(DateTime));
                dtDeclaration.Columns.Add("CreatedUserID", typeof(int));
                dtDeclaration.Columns.Add("IsActive", typeof(int));

                int rowcount = 0;
                foreach (GridViewRow row in GvShowALL.Rows)
                {
                    dtDeclaration.Rows.Add();
                    divisionraceid = Convert.ToInt32(GvShowALL.DataKeys[row.RowIndex]["GlobalID"]);
                    dtDeclaration.Rows[rowcount][0] = Convert.ToInt32(GvShowALL.DataKeys[row.RowIndex]["GlobalID"]);
                    dtDeclaration.Rows[rowcount][1] = string.Empty;
                    if (chkbxFalse.Checked.Equals(true))
                    {
                        dtDeclaration.Rows[rowcount][2] = true;
                    }
                    else
                    {
                        dtDeclaration.Rows[rowcount][2] = false;
                    }
                    dtDeclaration.Rows[rowcount][3] = txtbxMeasurement.Text;
                    dtDeclaration.Rows[rowcount][4] = Convert.ToDouble(txtbxPenetrometer.Text);
                    dtDeclaration.Rows[rowcount][5] = Convert.ToInt32(drpdwnGoing.SelectedItem.Value);
                    dtDeclaration.Rows[rowcount][6] = Convert.ToInt32(drpdwnWeather.SelectedItem.Value);
                    dtDeclaration.Rows[rowcount][7] = string.Empty;
                    dtDeclaration.Rows[rowcount][8] = false;
                    dtDeclaration.Rows[rowcount][9] = false;
                    dtDeclaration.Rows[rowcount][10] = false;
                    dtDeclaration.Rows[rowcount][11] = string.Empty;
                    dtDeclaration.Rows[rowcount][12] = DateTime.Now;
                    dtDeclaration.Rows[rowcount][13] = 1;
                    dtDeclaration.Rows[rowcount][14] = 1;
                    rowcount++;
                }

                var result = new CardsBL().AddRaceSituation(dtDeclaration, divisionraceid);
                if (result == 1)
                {
                    //AcceptanceShow();
                    ShowDeclaration();
                    ClearEntrySection();
                    var message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    var message = "Issue in Record.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        private void ClearEntrySection()
        {
            chkbxFalse.Checked = false;
            txtbxMeasurement.Text = string.Empty;
            txtbxPenetrometer.Text = string.Empty;
            drpdwnGoing.ClearSelection();
            drpdwnWeather.ClearSelection();
        }


       protected void btnClear_Click(object sender, EventArgs e)
        {
            chkbxFalse.Checked = false;
            txtbxDivisionRaceDate.Text = string.Empty;
            drpdwnCenterName.ClearSelection();
            drpdwnGoing.ClearSelection();
            drpdwnWeather.ClearSelection();
            lblSeason.Text = string.Empty;
            lblYear.Text = string.Empty;
            txtbxMeasurement.Text = string.Empty;
            txtbxPenetrometer.Text = string.Empty;
            
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_RaceDaySituation"))
                    {
                        string Extension = Path.GetExtension(flupload.PostedFile.FileName);
                        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                        bool exists = System.IO.Directory.Exists(Server.MapPath(FolderPath));
                        if (!exists) System.IO.Directory.CreateDirectory(Server.MapPath(FolderPath));

                        string FilePath = Server.MapPath(FolderPath + FileName);
                        flupload.SaveAs(FilePath);
                        Import_To_Grid(FilePath, Extension);
                    }
                    else
                    {
                        var message = "Please select the correct File.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        private void Import_To_Grid(string FilePath, string Extension)
        {
            try
            {
                string strConn;
                bool hasHeaders = true;
                string HDR = hasHeaders ? "Yes" : "No";
                if (FilePath.Substring(FilePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
                OleDbConnection connExcel = new OleDbConnection(strConn);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = string.Empty;
                if (dtExcelSchema.Rows[0]["TABLE_NAME"].ToString().Contains("FilterDatabase"))
                {
                    SheetName = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();

                }
                else
                {
                    SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                }
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                if (dt.Rows.Count > 0)
                {
                    var dtErrorResult = new CardsBL().Import30(dt, "Card_RaceDaySituation", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_RaceDaySituation");

                            int rowstart = 1;
                            int colstart = 1;
                            int rowend = rowstart;
                            int colend = colstart + (dtErrorResult.Columns.Count - 1);
                            //  int colend = colstart;
                            rowend = rowstart + dtErrorResult.Rows.Count;
                            ws.Cells[rowstart, colstart].LoadFromDataTable(dtErrorResult, true);
                            int i = 1;
                            foreach (DataColumn dc in dtErrorResult.Columns)
                            {
                                i++;
                                if (dc.DataType == typeof(decimal)) ws.Column(i).Style.Numberformat.Format = "#0.00";
                            }
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                                    ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                                        ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style =
                                            OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            Response.AddHeader("content-disposition", "attachment;filename=Card_RaceDaySituation.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        //BindData();
                        var message1 = "All Record has been added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message1 + "');", true);
                    }
                }
                else
                {
                    var message = "No Record Found.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
            }


        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "CardRaceDaySituation"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_RaceDaySituation");
                            int rowstart = 1;
                            int colstart = 1;
                            int rowend = rowstart;
                            int colend = colstart + (dt.Columns.Count - 1);
                            rowend = rowstart + dt.Rows.Count;
                            ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);
                            int i = 1;
                            foreach (DataColumn dc in dt.Columns)
                            {
                                i++;
                                if (dc.DataType == typeof(decimal))
                                    ws.Column(i).Style.Numberformat.Format = "#0.00";
                            }
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                               ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                               ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                               ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            Response.AddHeader("content-disposition", "attachment;filename=Card_RaceDaySituation.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnHandicapShow_Click(object sender, EventArgs e)
        {
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //ClearAll();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }
    }
}