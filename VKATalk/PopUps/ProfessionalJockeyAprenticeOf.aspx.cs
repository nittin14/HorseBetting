using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using VKATalkBusinessLayer;

namespace VKATalk.PopUps
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;

    using OfficeOpenXml;

    public partial class ProfessionalJockeyAprenticeOf : System.Web.UI.Page
    {
        int _userId = 1;
        private int _professionalId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
                if (!Request.QueryString["ProfessionalValue"].Equals(""))
                {
                    _professionalId = Convert.ToInt32(Request.QueryString["ProfessionalValue"]);
                    professionalId.Value = Request.QueryString["ProfessionalValue"];
                }
                if (!IsPostBack)
                {
                    try
                    {

                        if (_professionalId != 0)
                        {
                            var reqQueryValue = string.Empty;
                            if (!Request.QueryString["ProfessionalName"].Equals(""))
                            {
                                //reqQueryValue = Request.QueryString["ProfessionalName"];
                                reqQueryValue = Session["ProfessionalName"].ToString();
                            }
                            lblProfessionalNameFirst.Text = reqQueryValue;

                            //string[] horseName = reqQueryValue.Split(',');
                            //lblProfessionalNameFirst.Text = horseName[1];
                            //lblProfessionalNameSecond.Text = horseName[2];
                            //BindDropDown(this.drpdwnProfile, "JockeyAprenticeTrainerName", "ProfessionalName", "ProfessionalNameID");
                            //drpdwnProfile.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                            BindData();
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorHandling.SendErrorToText(ex);
                        var message = "Incorrect Information.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        /// <summary>
        /// bind drop down 
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="tableName_"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        private void BindDropDown(DropDownList ddl, String tableName_, string textField, String valueField)
        {
            DataTable dt;
            dt = new ProfessionalBL().GetProfessionalName(tableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = textField;
            ddl.DataValueField = valueField;
            ddl.DataBind();
        }

        /// <summary>
        /// This button Save the HorseName popup data in database and bind with dropdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int status = 0;
                DataSet ds = null;
                int checkcondition = 0;
                if (txtbxReason.Text.Equals(""))
                {
                    if (txtbxTillDate.Text.Equals("__-__-____"))
                    {
                        checkcondition = 1;
                    }
                    else
                    {
                        checkcondition = 0;
                    }
                }
                else
                {
                    if (txtbxTillDate.Text.Equals("__-__-____"))
                    {
                        checkcondition = 0;
                    }
                    else
                    {
                        checkcondition = 1;
                    }
                }

                if (checkcondition == 1)
                {
					//if (btnSave.Text.Equals("Add"))
					//{
					//    ds = new ProfessionalBL().GetProfessionalTillDateValidation(_professionalId, "ProfessionalAprnticeOf", txtbxFromDate.Text, btnSave.Text);
					//}
					//else
					//{
					//    ds = new ProfessionalBL().GetProfessionalTillDateValidation((int)ViewState["ProfessionalRecordID"], "ProfessionalAprnticeOf", txtbxFromDate.Text, btnSave.Text);
					//}
					//Int64 checkTillDate = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
					Int64 checkTillDate = 0;

					if (_professionalId == 0)
                    {
                        string message = "Please select Professional name.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else if (checkTillDate == 2)
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Check the existing record TillDate.');", true);
                        string message = "Please Check the existing record TillDate.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        if (btnSave.Text.Equals("Add"))
                        {
                            status = new ProfessionalBL().ProfessionalAprenticeOf(
                                _professionalId,
                                Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                txtbxFromDate.Text,
                                txtbxTillDate.Text,
                                txtbxReason.Text,
                                txtbxComment.Text,
                                _userId,
                                "Insert");
                            hdnfieldProfessionalName.Value = txtbxSourceName.Text;
                        }
                        else
                        {
                            status = new ProfessionalBL().ProfessionalAprenticeOf(
                                (int)ViewState["ProfessionalRecordID"],
                                Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                txtbxFromDate.Text,
                                txtbxTillDate.Text,
                                txtbxReason.Text,
                                txtbxComment.Text,
                                _userId,
                                "Update");
                            btnSave.Text = "Add";
                        }
                        if (status == 1)
                        {
                            var message = "Record added successfully.";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            BindData();
                            ClearAllSelection(this);

                        }
                        else if (status == 2)
                        {
                            var message = "Record updated successfully.";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            BindData();
                            ClearAllSelection(this);
                            btnSave.Text = "Add";
                        }
                        else if (status == 5)
                        {
                            var message = "Record activated successfully.";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            BindData();
                            ClearAllSelection(this);
                            btnSave.Text = "Add";
                        }
                        else if (status == 4)
                        {
                            string message = "Record already exist.";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            ClearAllSelection(this);
                            btnSave.Text = "Add";
                        }
                        else
                        {
                            ErrorHandling.CheckEachSteps(Convert.ToString(status));
                            string message = "Issue in Record. (Status) : " + status;
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        }
                    }
                }
                else
                {
                    var message = "Please enter Reason of Change.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }



        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddCommentsList(string prefixText, int count)
        {
            var dt = new ProfessionalBL().GetProfessionalNameAutoFiller("ProfessionalAprnticeOf", prefixText);
            var commentList = new List<string>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                commentList.Add(dt.Rows[i][0].ToString());
            }
            return commentList;
        }

        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddSourceNameList(string prefixText, int count)
        {
            DataTable dt = new ProfessionalBL().GetProfessionalNameAutoFiller("ProfessionalTraineeAprenticeOf", prefixText);

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


        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddReasonList(string prefixText, int count)
        {
            var dt = new ProfessionalBL().GetProfessionalNameAutoFiller("ProfessionalAprnticeOfReason", prefixText);
            var commentList = new List<string>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                commentList.Add(dt.Rows[i][0].ToString());
            }
            return commentList;
        }

        public void ClearAllSelection(Control parent)
        {

            foreach (Control x in parent.Controls)
            {
                if ((x.GetType() == typeof(TextBox)))
                {

                    ((TextBox)(x)).Text = "";
                }

                if ((x.GetType() == typeof(DropDownList)))
                {

                    ((DropDownList)(x)).ClearSelection();
                }

                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }
        }
         /// <summary>
        /// BindData with Gridview
        /// </summary>
        private void BindData()
        {
                var ds = new ProfessionalBL().GetProfessionalNameWithCombination(_professionalId, "ProfessionalAprnticeOf");

               if (ds.Tables[0].Rows.Count > 0)
                {
                    GvHorseProfile.DataSource = ds.Tables[0];
                    GvHorseProfile.DataBind();

                }
                else
                {
                    GvHorseProfile.DataSource = new DataTable();
                    GvHorseProfile.DataBind();
                }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            btnSave.Text = "Add";
        }


        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ViewState["ProfessionalRecordID"].Equals(""))
                {
                    var status = new ProfessionalBL().ProfessionalAprenticeOf((int)ViewState["ProfessionalRecordID"], 0, "__-__-____", "__-__-____", "", "", _userId, "Delete");
                    if (status > 0)
                    {
                        ClearAllSelection(this);
                        BindData();
                        btnSave.Text = "Add";
                        var message = "Record deleted successfully.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ViewState["ProfessionalRecordID"] = "";
                    }
                    else
                    {
                        var message = "Incorrect Information.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
                else
                {
                    var message = "No record Found.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception exception)
            {
                var message = "Incorrect Information.";
                ErrorHandling.SendErrorToText(exception);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void GvHorseProfile_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                var row = GvHorseProfile.SelectedRow;
                var hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvHorseProfile.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["ProfessionalRecordID"] = dataKey.Value;
                    //drpdwnProfile.Items.FindByText(hdnval.Value).Selected = true;

                    HiddenField hdnfieldProfessionalNameIDG = (HiddenField)row.FindControl("hdnfieldProfessionalNameIDG");
                    HiddenField hdnfieldProfessionalNameG = (HiddenField)row.FindControl("hdnfieldProfessionalNameG");
                    hdnfieldProfessionalnameid.Value = hdnfieldProfessionalNameIDG.Value;
                    txtbxSourceName.Text = hdnfieldProfessionalNameG.Value;

                    txtbxFromDate.Text = row.Cells[1].Text;
                    txtbxTillDate.Text = row.Cells[2].Text;
                    if (row.Cells[3].Text.Contains("&quot;") || row.Cells[3].Text.Contains("&nbsp;"))
                    {
                        txtbxReason.Text = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&quot;") ? row.Cells[3].Text.Replace("&quot;", "\"") : row.Cells[3].Text;
                    }
                    else
                    {
                        txtbxReason.Text = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&#39;") ? row.Cells[3].Text.Replace("&#39;", "'") : row.Cells[3].Text;
                    }
                    if (row.Cells[4].Text.Contains("&quot;"))
                    {
                        txtbxComment.Text = row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&quot;") ? row.Cells[4].Text.Replace("&quot;", "\"") : row.Cells[4].Text;
                    }
                    else
                    {
                        txtbxComment.Text = row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&#39;") ? row.Cells[4].Text.Replace("&#39;", "'") : row.Cells[4].Text;
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        
        /// <summary>
        /// Close the window and pass the value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (professionalId.Value != "")
                {
                    Session["ProfessionalID"] = professionalId.Value;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
               
            }
            catch (Exception ex)
            {
                var message = "Record already exist.";
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Professional_JockeyMasterTrainer"))
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
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
                    var dtErrorResult = new ProfessionalBL().Import30(dt, "ProfessionalAprentice");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Professional_JockeyMasterTrainer");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Professional_JockeyMasterTrainer.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        BindData();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('All Record has been added successfully.');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('No Record Found.');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
            }
        }

        /// <summary>
        /// Export Gridview Data in Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // var ds = new ProfessionalBL().GetProfessionalNameWithCombination(_professionalId, "ProfessionalBaseCenter");
                using (DataSet ds = new ProfessionalBL().GetExport(_professionalId, "ProfessionalAprnticeOf"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Professional_JockeyMasterTrainer");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Professional_JockeyMasterTrainer.xlsx");
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
    }
}