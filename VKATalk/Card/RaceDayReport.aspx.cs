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

    public partial class RaceDayReport : System.Web.UI.Page
    {
        private DataTable dtCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxDeclarationEnterDate.Text = CommonMethods.CurrentDate();
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
        public static List<string> AddCommentatorList(string prefixText, int count)
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
        public static List<string> AddReportList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("AddReportList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
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
        public static List<string> AddIncidentList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("AddIncidentList", prefixText);
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
        public static List<string> AddHorseList(string prefixText, int count, string contextKey)
        {
            DataTable dt = new CardsBL().GetHorseNameAutoFillerMultiple("RaceDayReportHorstList", prefixText, contextKey);
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
                var dt = new CardsBL().GetAcceptanceDivisionDetail(
                    txtbxDivisionRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "CardResult");
                if (dt.Rows.Count > 0)
                {
                    lblSeason.Text = dt.Rows[0][7].ToString();
                    lblYear.Text = dt.Rows[0][8].ToString();
                    dvgridview.Visible = true;
                    grdvwRaceDetail.DataSource = dt;
                    grdvwRaceDetail.DataBind();
                }
                else
                {
                    dvgridview.Visible = false;
                    grdvwRaceDetail.DataSource = new DataTable();
                    grdvwRaceDetail.DataBind();
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

        
        protected void grdvwRaceDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //BtnSubmit.Text = "Update";
                divEntry.Visible = true;
                GridViewRow row = grdvwRaceDetail.SelectedRow;
                HiddenField hdnfielddivisionracename = (HiddenField)row.FindControl("hdnfielddivisionracename");
                HiddenField hdnfieldHorseNameID = (HiddenField)row.FindControl("hdnfieldHorseNameID");
                var dataKey = grdvwRaceDetail.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ViewState["DivisionRaceID"] = dataKey.Value; //generalraceid
                    hdnfieldDivisionRaceMID.Value = ViewState["DivisionRaceID"].ToString();
                    hdnfieldDivisionRaceMID.Value = dataKey.Value.ToString();
                    lblGeneralRaceName.Text = hdnfielddivisionracename.Value;
                }
                ShowDeclaration();
                
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
            var dt = new CardsBL().GetDeclaration(Convert.ToInt32(ViewState["DivisionRaceID"]), 0, "", "RaceDayReporting", txtbxDivisionRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
            dtCount = dt.Tables[0];
            if (dt.Tables[0].Rows.Count > 0)
            {
                GvShowALL.DataSource = dt;
                GvShowALL.DataBind();
            }
            else
            {
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
            }
        }

        
        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = new ProspectusBL().GetDropdownBind(TableName_);
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
           // DataTable dtDeclaration = new DataTable("Declaration");
            try
            {
                var result = 0;
                var highlight = string.Empty;
                if(chkbxHighlight.Checked.Equals(true))
                {
                    highlight = "true";
                }
                else
                {
                    highlight = "false";
                }
                if (btnAdd.Text.Equals("Add"))
                {
                    result = new CardsBL().RaceDayReport(0, txtbxDivisionRaceDate.Text, 
                        Convert.ToInt32(hdnfieldDivisionRaceMID.Value), Convert.ToInt32(hdnfieldCommentator.Value), 
                        Convert.ToInt32(hdnfieldHorseNameID.Value), txtbxReport.Text, 1, "Insert",highlight, 
                        hdnfieldIncidentID.Value);
                }
                else if (btnAdd.Text.Equals("Update"))
                {
                    result = new CardsBL().RaceDayReport(Convert.ToInt32(ViewState["GlobalID"]), txtbxDivisionRaceDate.Text, Convert.ToInt32(hdnfieldDivisionRaceMID.Value), 
                        Convert.ToInt32(hdnfieldCommentator.Value), Convert.ToInt32(hdnfieldHorseNameID.Value), 
                        txtbxReport.Text, 1, "Update",highlight, hdnfieldIncidentID.Value);
                }
                if (result == 1)
                {
                    var message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearEntrySection();
                    ShowDeclaration();
                }
                else if (result == 2)
                {
                    var message = "Record updated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearEntrySection();
                    ShowDeclaration();
                    btnAdd.Text = "Add";
                    
                }
                else if (result == 4)
                {
                    var message = "Record already exist.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else if (result == 5)
                {
                    var message = "Record activated successfully.";
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
            btnAdd.Text = "Add";
            if (chkbxAferReport.Checked.Equals(true))
            {
                txtbxHorseName.Text = string.Empty;
                hdnfieldHorseNameID.Value = string.Empty;
                chkbxHighlight.Checked = false;
            }
            else if (chkbxFix.Checked.Equals(true))
            {
                txtbxHorseName.Text = string.Empty;
                hdnfieldHorseNameID.Value = string.Empty;
                txtbxReport.Text = string.Empty;
                //hdnfieldReport.Value = string.Empty;
                chkbxHighlight.Checked = false;
            }
            else
            {
                txtbxCommentator.Text = string.Empty;
                hdnfieldCommentator.Value = string.Empty;
                txtbxHorseName.Text = string.Empty;
                hdnfieldHorseNameID.Value = string.Empty;
                txtbxReport.Text = string.Empty;
                //hdnfieldReport.Value = string.Empty;
                chkbxHighlight.Checked = false;
            }
        }


        protected void GvShowALL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try{
                GridViewRow row = GvShowALL.SelectedRow;
                var dataKey = GvShowALL.DataKeys[row.RowIndex];
                
                if (dataKey != null)
                {
                    ViewState["GlobalID"] = dataKey.Value;
                    HiddenField hdnfieldProfessionalnameid = (HiddenField)row.FindControl("hdnfieldProfessionalNameID");
                    HiddenField hdnfieldProfessionalName = (HiddenField)row.FindControl("hdnfieldProfessionalName");
                    hdnfieldCommentator.Value = hdnfieldProfessionalnameid.Value;
                    txtbxCommentator.Text = hdnfieldProfessionalName.Value;
                    if (row.Cells[1].Text.Contains("&amp;"))
                    {
                        txtbxReport.Text = row.Cells[1].Text.Replace("&amp;","&");
                    }
                    else
                    {
                        txtbxReport.Text = row.Cells[1].Text;
                    }
                    
                    if (row.Cells[2].Text.Equals("Highlight"))
                    {
                        chkbxHighlight.Checked = true;
                    }
                    else
                    {
                        chkbxHighlight.Checked = false;
                    }
                    HiddenField horsenameid = (HiddenField)row.FindControl("hdnfielHorseNameID");
                    HiddenField hdnfieldHorseName = (HiddenField)row.FindControl("hdnfieldHorseName");
                    hdnfieldHorseNameID.Value = horsenameid.Value;
                    txtbxHorseName.Text = hdnfieldHorseName.Value;
                    //txtbxReport.Text = row.Cells[3].Text;
                }

                btnAdd.Text = "Update";
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtbxCommentator.Text = string.Empty;
            hdnfieldCommentator.Value = string.Empty;
            chkbxFix.Checked = false;
            txtbxReport.Text = string.Empty;
            chkbxAferReport.Checked = false;
            chkbxHighlight.Checked = false;
            txtbxHorseName.Text = string.Empty;
            hdnfieldHorseNameID.Value = string.Empty;
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_RaceCard"))
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
                    var dtErrorResult = new CardsBL().Import30(dt, "Card_RaceCard", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_RaceCard");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_RaceCard.xlsx");
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
                using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "RaceDayReport"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_RaceDayReport");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_RaceDayReport.xlsx");
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