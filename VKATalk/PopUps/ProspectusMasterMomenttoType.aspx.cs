﻿using System;
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

    public partial class ProspectusMasterMomenttoType : System.Web.UI.Page
    {
        //MasterHorseBL Bl = new MasterHorseBL();
        int _masterraceId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
            if (!Request.QueryString["ProspectusId"].Equals(""))
            {
                _masterraceId = Convert.ToInt32(Request.QueryString["ProspectusId"]);
                hdnfieldRaceId.Value = Request.QueryString["ProspectusId"];
            }

            if (!IsPostBack)
            {
                try
                {
                    if (_masterraceId != 0)
                    {
                        var reqQueryValue = string.Empty;
                        if (!Session["ProspectusMasterRaceName"].Equals(""))
                        {
                            //reqQueryValue = Request.QueryString["RaceName"];
                            reqQueryValue = Session["ProspectusMasterRaceName"].ToString();
                        }
                        
                        string[] RaceName = reqQueryValue.Split(',');
                        lblMasterRaceNameFirst.Text = RaceName[0];
                        try
                        {
                            lblMasterRaceNameSecond.Text = RaceName[1];
                        }
                        catch
                        {
                           // ErrorHandling.SendErrorToText(ex);
                        }

                        BindDropDown(drpdwnMomenttoType, "MomenttoType", "MomenttoType", "MomenttoTypeID");
                        drpdwnMomenttoType.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                        BindDropDown(drpdwnFromYear, "Year", "YearName", "YearID");
                        drpdwnFromYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                        BindDropDown(drpdwnTillYear, "Year", "YearName", "YearID");
                        drpdwnTillYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));

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

        private void BindDropDown(DropDownList ddl, String tablename, string textfield, String valuefield)
        {
            DataTable dt = new ProspectusBL().GetDropdownBind(tablename);
            ddl.DataSource = dt;
            ddl.DataTextField = textfield;
            ddl.DataValueField = valuefield;
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
                if (btnSave.Text.Equals("Add"))
                {
                    ds = new ProspectusBL().GetProspectusTillDateValidation(_masterraceId, "MomenttoType", drpdwnFromYear.SelectedItem.Text, btnSave.Text);
                }
                else
                {
                    ds = new ProspectusBL().GetProspectusTillDateValidation((int)ViewState["MasterRaceID"], "MomenttoType", drpdwnFromYear.SelectedItem.Text, btnSave.Text);
                }
                Int64 checkTillDate = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                if (checkTillDate == 2)
                {
                    var message = "Please Check the existing record TillYear.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        status = new ProspectusBL().MomenttoType(
                            _masterraceId,
                            Convert.ToInt32(drpdwnMomenttoType.SelectedItem.Value),
                            Convert.ToInt32(drpdwnFromYear.SelectedItem.Value),
                            Convert.ToInt32(drpdwnTillYear.SelectedItem.Value),
                            txtbxComment.Text,
                            1,
                            "Insert");
                    }
                    else if (btnSave.Text.Equals("Update"))
                    {
                        status = new ProspectusBL().MomenttoType(
                            (int)ViewState["MasterRaceID"],
                            Convert.ToInt32(drpdwnMomenttoType.SelectedItem.Value),
                           Convert.ToInt32(drpdwnFromYear.SelectedItem.Value),
                            Convert.ToInt32(drpdwnTillYear.SelectedItem.Value),
                            txtbxComment.Text,
                            1,
                            "Update");
                        //   btnSave.Text = "Add";
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
            //DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("MemoryOfCommentList", prefixText);
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusMasterMomenttoTypeCommentList", prefixText);
            List<string> commentList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
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
        public static List<string> AddMemoirType(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusMasterMomenttoType", prefixText);
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
            try
            {
                //DataSet ds= Bl.GetHorseNameWithCombination(_masterraceId, "HorseStatus");
                DataSet ds = new ProspectusBL().GetProspectusNameWithCombination(_masterraceId, "ProspectusMasterMomentto");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvProspectus.DataSource = ds.Tables[0];
                    GvProspectus.DataBind();
                }
                else
                {
                    GvProspectus.DataSource = new DataTable();
                    GvProspectus.DataBind();
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
                if (hdnfieldRaceId.Value != "")
                {
                    Session["ProspectusID"] = hdnfieldRaceId.Value;
                }
               ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                if (!ViewState["MasterRaceID"].Equals(""))
                {
                    var status = new ProspectusBL().MomenttoType((int)ViewState["MasterRaceID"],0, 0,0,txtbxComment.Text, 1, "Delete");

                    ClearAllSelection(this);
                    BindData();
                    btnSave.Text = "Add";
                    var message = "Record Deleted Successfully.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["MasterRaceID"] = "";
                }
                else
                {
                    var message = "Incorrect Information.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception exception)
            {
                ErrorHandling.SendErrorToText(exception);
                var message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void GvHorseStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = GvProspectus.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvProspectus.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["MasterRaceID"] = dataKey.Value;
                    //rdbtnStatus.Items.FindByText(hdnval.Value).Selected = true;
                    //txtbxMemoirType.Text = hdnval.Value;
                    drpdwnMomenttoType.Items.FindByText(hdnval.Value).Selected = true;
                    drpdwnFromYear.Items.FindByText(row.Cells[1].Text).Selected = true;
                    if (row.Cells[2].Text.Contains("&nbsp;"))
                    {
                        drpdwnTillYear.Items.FindByText("-- Please select --").Selected = true;
                    }
                    else
                    {
                        drpdwnTillYear.Items.FindByText(row.Cells[2].Text).Selected = true;
                    }
                    if (row.Cells[3].Text.Contains("&quot;"))
                    {
                        txtbxComment.Text = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&quot;") ? row.Cells[3].Text.Replace("&quot;", "\"") : row.Cells[3].Text;
                    }
                    else
                    {
                        txtbxComment.Text = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&#39;") ? row.Cells[3].Text.Replace("&#39;", "'") : row.Cells[3].Text;
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Prospectus_Master_Momentto"))
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                    var dtErrorResult = new ProspectusBL().Import30(dt, "MomenttoType", _masterraceId);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("MomenttoType");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_Master_Momentto.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                            
                        }
                    }
                    else
                    {
                        BindData();
                        var message = "All Record has been added successfully.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
                else
                {
                    var message = "Issue found in record.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                var message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable dt = Bl.GetMasterData("Year");
                using (DataSet ds = new ProspectusBL().GetExport(_masterraceId, "MomenttoType"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //dt.Columns.Remove("MasterSponcerID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("MomenttoType");
                            int rowstart = 1;
                            int colstart = 1;
                            int rowend = rowstart;
                            int colend = colstart + (dt.Columns.Count - 1);
                            //  int colend = colstart;
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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_Master_Momentto.xlsx");
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }


        }
    }
}