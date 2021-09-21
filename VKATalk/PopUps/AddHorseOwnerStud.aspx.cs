using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VKATalkBusinessLayer;

namespace VKATalk.PopUps
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;

    using OfficeOpenXml;

    public partial class AddHorseOwnerStud : System.Web.UI.Page
    {
        MasterHorseBL Bl = new MasterHorseBL();
        int _horseId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
                if (!Request.QueryString["HorseNameID"].Equals(""))
                {
                    _horseId = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                    horseId.Value = Request.QueryString["HorseNameID"];
                }

                if (!IsPostBack)
                {
                    if (_horseId != 0)
                        {
                            var reqQueryValue = string.Empty;
                            if (!Request.QueryString["HorseName"].Equals(""))
                            {
                                reqQueryValue = Request.QueryString["HorseName"];
                            }
                        
                            string[] horseName = reqQueryValue.Split(',');
                            lblHorseNameFirst.Text = horseName[1];
                            lblHorseNameSecond.Text = horseName[2];
						    BindDropDown(this.drpdwnOwnershipEngagement, "HorseOwnerStudOE", "HorseOwnerStudOE", "OwnerStudOEHMID");
							drpdwnOwnershipEngagement.Items.Insert(0, new ListItem("-- Please select --", "-1"));
						BindData();
                        }
              
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        private void BindDropDown(DropDownList ddl, String tablename, string textfield, String valuefield)
        {
            DataTable dt = new MasterHorseBL().GetDropdownBind(tablename);
            ddl.DataSource = dt;
            ddl.DataTextField = textfield;
            ddl.DataValueField = valuefield;
            ddl.DataBind();
        }


        /// <summary>
        /// AddStudOwnerList
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddStudOwnerList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseStudList", prefixText);
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
                if (btnSave.Text.Equals("Add"))
                {
                    ds = Bl.GetHorseTillDateValidation(_horseId, "HorseOwnerStud", txtbxFromDate.Text, btnSave.Text);
                }
                else
                {
                    ds = Bl.GetHorseTillDateValidation((int)ViewState["horcapacityValue"], "HorseOwnerStud", txtbxFromDate.Text, btnSave.Text);
                }
                Int64 checkTillDate = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                if (_horseId == 0)
                {
                    string message = "Please select Horse name.";
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
                        status = Bl.InsertHorseOwnerStud(
                            _horseId,
                            //txtbxOwnerStud.Text,
                            hdnfieldOwnerStudNameID.Value,
                            drpdwnOwnershipEngagement.SelectedItem.Value,
                            txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            txtbxComment.Text,
                            1,
                            "Insert");
                    }
                    else if (btnSave.Text.Equals("Update"))
                    {
                        status = Bl.InsertHorseOwnerStud(
                            (int)ViewState["horcapacityValue"],
                            hdnfieldOwnerStudNameID.Value,
                            drpdwnOwnershipEngagement.SelectedItem.Value,
                            txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            txtbxComment.Text,
                            1,
                            "Update");
                        //btnSave.Text = "Add";
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
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseOwnerStud", prefixText);
            List<string> commentList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                commentList.Add(dt.Rows[i][0].ToString());
            }
            return commentList;
        }

        /// <summary>
        /// BindData with Gridview
        /// </summary>
        private void BindData()
        {
                DataSet ds = Bl.GetHorseNameWithCombination(_horseId, "HorseOwnerStud");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //dt.Columns.Remove("HorseOwnerStudID");
                    //ds.Tables[0].Columns.Remove("HorseOwnerStudID");
                    GvHorseGlobal.DataSource = ds.Tables[0];
                    GvHorseGlobal.DataBind();
                }
                else
                {
                    GvHorseGlobal.DataSource = new DataTable();
                    GvHorseGlobal.DataBind();
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
                if (horseId.Value != "")
                {
                    Session["HorseID"] = horseId.Value;
                }
               ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Incorrect Information.');", true);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
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
                if (!ViewState["horcapacityValue"].Equals(""))
                {
                    var status = Bl.InsertHorseOwnerStud((int)ViewState["horcapacityValue"], "",
                        "", txtbxFromDate.Text, txtbxTillDate.Text,
                    txtbxComment.Text, 1, "Delete");
                    ClearAllSelection(this);
                    BindData();
                    btnSave.Text = "Add";
                    var message = "Record Deleted Successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["horcapacityValue"] = "";
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Incorrect Information.');", true);
                    var message = "Incorrect Information.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
            catch (Exception exception)
            {
                ErrorHandling.SendErrorToText(exception);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void GvHorseGlobal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = GvHorseGlobal.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvHorseGlobal.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["horcapacityValue"] = dataKey.Value;
                    if (!hdnval.Value.Contains("&nbsp;"))
                    {
                        HiddenField hdnfieldOwnerStudNameIDG = (HiddenField)row.FindControl("hdnfieldOwnerStudNameID");
                        hdnfieldOwnerStudNameID.Value = hdnfieldOwnerStudNameIDG.Value;
                        txtbxOwnerStud.Text = hdnval.Value;
                    }
                    drpdwnOwnershipEngagement.Items.FindByText(row.Cells[1].Text).Selected = true;
                    txtbxFromDate.Text = row.Cells[2].Text;
                    if (!row.Cells[3].Text.Contains("&nbsp;"))
                    {
                        txtbxTillDate.Text = row.Cells[3].Text;
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
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Incorrect Information.');", true);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            btnSave.Text = "Add";
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

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Horse_OwnerStud"))
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
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=1\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";
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
                    //var dtErrorResult = Bl.UploadExcelRecordBulkMinimumColumns(dt, "HorseOwnerStud");
                    var dtErrorResult = Bl.Import30(dt, "HorseOwnerStud");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                       // Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("HorseOwnerStud");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_OwnerStud.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                            
                        }
                    }
                    else
                    {
                        var message = "All Record has been added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('All Record has been added successfully');", true);
                    }
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Issue found in record');", true);
                    var message = "Issue found in record.";
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
        /// Export Gridview Data in Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //using (DataSet ds = new MasterHorseBL().GetHorseNameWithCombination(_horseId, "HorseOwnerStud"))
                using (DataSet ds = new MasterHorseBL().GetExport(_horseId, "HorseOwnerStud"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
 //                       dt.Columns.Remove("HorseOwnerStudID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("HorseOwnerStud");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_OwnerStud.xlsx");
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