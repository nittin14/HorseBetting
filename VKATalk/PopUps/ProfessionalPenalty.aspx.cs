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

    public partial class ProfessionalPenalty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
                if (!Request.QueryString["ProfessionalNameID"].Equals(""))
                {
                    hdnfieldProfessionalNameID.Value = Request.QueryString["ProfessionalNameID"];
                    if (Session["ProfessionalName"] != null)
                    {
                        ViewState["professionialname"] = Session["ProfessionalName"].ToString();
                        txtbxProfessionalName.Text = ViewState["professionialname"].ToString();
                        txtbxProfessionalName.Enabled = false;
                    }
                }
                else
                {
                    txtbxProfessionalName.Enabled = true;
                }
                if (!IsPostBack)
                {
                    
                    try
                    {
                        BindDropDown(drpdwnPenalty, "ProfessionalPenalty", "Penalty", "PenaltyPMID");
                        drpdwnPenalty.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                        BindDropDown(drpdwnPenaltyReason, "ProfessionalPenaltyReason", "PenaltyReason", "PenaltyReasonPMID");
                        drpdwnPenaltyReason.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                        BindData();
                    }
                    catch (Exception ex)
                    {
                        ErrorHandling.SendErrorToText(ex);
                        string message = "Incorrect Information.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = new MasterHorseBL().GetDropdownBind(TableName_);
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
        public static List<string> ProfessionalName(string prefixText, int count)
        {
            var dt = new ProfessionalBL().GetProfessionalNameAutoFiller("ProfessionalTrainee", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string studlist =
                    AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0]));
                horseList.Add(studlist);
            }
            return horseList;
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
                if (hdnfieldProfessionalNameID.Value.Equals("0"))
                {
                   string message = "Please select Professional name.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        status = new ProfessionalBL().ProfessionalPenalty(
                            Convert.ToInt32(hdnfieldProfessionalNameID.Value),
                            Convert.ToInt32(drpdwnPenalty.SelectedItem.Value),
                            Convert.ToInt32(drpdwnPenaltyReason.SelectedItem.Value),
                            txtbxPenaltyDetail.Text,
                            txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            Convert.ToInt32((chkbxWorkingonApeal.Checked.Equals(true))?1:0),
                            txtbxCommentNew.Text,
                            1,
                            "Insert");
                    }
                    else
                    {
                        status = new ProfessionalBL().ProfessionalPenalty(
                             Convert.ToInt32(ViewState["ProfessionalRecordID"]),
                             Convert.ToInt32(drpdwnPenalty.SelectedItem.Value),
                             Convert.ToInt32(drpdwnPenaltyReason.SelectedItem.Value),
                             txtbxPenaltyDetail.Text,
                             txtbxFromDate.Text,
                             txtbxTillDate.Text,
                             Convert.ToInt32((chkbxWorkingonApeal.Checked.Equals(true)) ? 1 : 0),
                             txtbxCommentNew.Text,
                             1,
                             "Update");
                    }
                    if (status == 1)
                    {
                        var message = "Record added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        txtbxProfessionalName.Text = ViewState["professionialname"].ToString();

                    }
                    else if (status == 2)
                    {
                        var message = "Record updated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        txtbxProfessionalName.Text = ViewState["professionialname"].ToString();
                        btnSave.Text = "Add";
                    }
                    else if (status == 5)
                    {
                        var message = "Record activated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        txtbxProfessionalName.Text = ViewState["professionialname"].ToString();
                        btnSave.Text = "Add";
                    }
                    else if (status == 4)
                    {
                        string message = "Record already exist.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ClearAllSelection(this);
                        txtbxProfessionalName.Text = ViewState["professionialname"].ToString();
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
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }



        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddPenaltyDetail(string prefixText, int count)
        {
            DataTable dt = new ProfessionalBL().GetProfessionalNameAutoFiller("ProfessionalPenaltyDetail", prefixText);
            List<string> commentList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                commentList.Add(dt.Rows[i][0].ToString());
            }
            return commentList;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddCommentsList(string prefixText, int count)
        {
            DataTable dt = new ProfessionalBL().GetProfessionalNameAutoFiller("ProfessionalPenaltyMyComments", prefixText);
            List<string> commentList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                commentList.Add(dt.Rows[i][0].ToString());
            }
            return commentList;
        }

        public void ClearAllSelection(Control parent)
        {
            chkbxWorkingonApeal.Checked = false;
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
                DataSet ds = new ProfessionalBL().GetProfessionalNameWithCombination(Convert.ToInt32(hdnfieldProfessionalNameID.Value), "Professional_Penalty");

               if (ds.Tables[0].Rows.Count > 0)
                {
                    this.GvHomeDistance.DataSource = ds.Tables[0];
                    this.GvHomeDistance.DataBind();
                }
                else
                {
                    this.GvHomeDistance.DataSource = new DataTable();
                    this.GvHomeDistance.DataBind();
                }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            txtbxProfessionalName.Text = ViewState["professionialname"].ToString();
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
                    var status = new ProfessionalBL().ProfessionalPenalty(
                            Convert.ToInt32(ViewState["ProfessionalRecordID"]),
                            Convert.ToInt32(drpdwnPenalty.SelectedItem.Value),
                            Convert.ToInt32(drpdwnPenaltyReason.SelectedItem.Value),
                            txtbxPenaltyDetail.Text,
                            txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            Convert.ToInt32((chkbxWorkingonApeal.Checked.Equals("true")) ? 1 : 0),
                            txtbxCommentNew.Text,
                            1,
                            "Delete");


                    if (status > 0)
                    {
                        ClearAllSelection(this);
                        txtbxProfessionalName.Text = ViewState["professionialname"].ToString();
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
                string message = "Incorrect Information.";
                ErrorHandling.SendErrorToText(exception);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void GvHomeDistance_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = GvHomeDistance.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvHomeDistance.DataKeys[row.RowIndex];
				if (dataKey != null)
				{
					ClearAllSelection(this);
                    txtbxProfessionalName.Text= ViewState["professionialname"].ToString();
                    ViewState["ProfessionalRecordID"] = dataKey.Value;
                    drpdwnPenalty.Items.FindByText(hdnval.Value).Selected = true;
                    drpdwnPenaltyReason.Items.FindByText(row.Cells[1].Text).Selected = true;
                    if (!row.Cells[2].Text.Contains("&nbsp;"))
                    {
                        txtbxPenaltyDetail.Text = row.Cells[2].Text;
                    }
                    if (!row.Cells[3].Text.Contains("&nbsp;"))
                    {
                        txtbxFromDate.Text = row.Cells[3].Text;
                    }
                    if (!row.Cells[4].Text.Contains("&nbsp;"))
                    {
                        txtbxTillDate.Text = row.Cells[4].Text;
                    }
                    if (!row.Cells[5].Text.Equals("false"))
                    {
                        chkbxWorkingonApeal.Checked = true;
                    }
                    else
                    {
                        chkbxWorkingonApeal.Checked = false;
                    }
                    if (row.Cells[6].Text.Contains("&quot;"))
					{
						txtbxCommentNew.Text = row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&quot;") ? row.Cells[6].Text.Replace("&quot;", "\"") : row.Cells[6].Text;
					}
					else
					{
						txtbxCommentNew.Text = row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&#39;") ? row.Cells[6].Text.Replace("&#39;", "'") : row.Cells[6].Text;
					}
				}

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
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
                if (hdnfieldProfessionalNameID.Value != "")
                {
                    Session["ProfessionalID"] = hdnfieldProfessionalNameID.Value;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
               
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Professional_Penalty"))
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
                    var dtErrorResult = new ProfessionalBL().Import30(dt, "Professional_Penalty");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Professional_Penalty");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Professional_Penalty.xlsx");
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
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                using (DataSet ds = new ProfessionalBL().GetExport(Convert.ToInt32(hdnfieldProfessionalNameID.Value), "Professional_Penalty"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Professional_Penalty");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Professional_Penalty.xlsx");
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