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

    public partial class ProspectusMasterHandicapWeight : System.Web.UI.Page
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
                        try
                        {
                            lblMasterRaceNameFirst.Text = RaceName[0];
                            lblMasterRaceNameSecond.Text = RaceName[1];
                        }
                        catch
                        { }

                        BindDropDown(drpdwnFromYear, "Year", "YearName", "YearID");
                        drpdwnFromYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                        BindDropDown(drpdwnTillYear, "Year", "YearName", "YearID");
                        drpdwnTillYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                        BindDropDown(drpdwnGender, "HorseSex", "HorseSex", "HorseSexID");
                        drpdwnGender.Items.Insert(0, new ListItem("-- Please select --", "-1"));
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



        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            //dt = new MasterHorseBL().GetDropdownBind(TableName_);
            dt = new ProspectusBL().GetDropdownBind(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
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
                DataSet ds = new ProspectusBL().GetProspectusNameWithCombination(_masterraceId, "ProspectusMasterHandicapWeight");
                var checkstatus = 0;
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    for (int count = 0; count <= ds.Tables[0].Rows.Count - 1; count++)
                //    {
                //        if (ds.Tables[0].Rows[count][1].ToString().Equals(drpdwnGender.SelectedItem.Text) &&
                //              ds.Tables[0].Rows[count][4].ToString().Equals(""))
                //        {
                //            if ((hdnfieldWeightID.Value.ToString() != null) || (hdnfieldWeightID.Value.ToString() != ""))
                //            {
                //                if (ds.Tables[0].Rows[count][0].ToString().Equals(hdnfieldWeightID.Value.ToString()))
                //                {
                //                    checkstatus = 0;
                //                }
                //                else
                //                {
                //                    checkstatus = 1;
                //                }
                //            }
                //        }
                //    }
                //}
                //ErrorHandling.CheckEachSteps("btnSave_Click" + DateTime.Now);
                //if (btnSave.Text.Equals("Add"))
                //{
                //    ds = new ProspectusBL().GetProspectusTillDateValidation(_masterraceId, "HandicapWeight", drpdwnFromYear.SelectedItem.Text, btnSave.Text);
                //}
                //else
                //{
                //    ds = new ProspectusBL().GetProspectusTillDateValidation((int)ViewState["MasterRaceID"], "HandicapWeight", drpdwnFromYear.SelectedItem.Text, btnSave.Text);
                //}
                ////ErrorHandling.CheckEachSteps("btnSave_Click2" + Convert.ToInt64(ds.Tables[0].Rows[0][0]));
                //Int64 checkTillDate = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                if (checkstatus == 1)
                {
                    var message = "Please Check the existing record TillYear.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        status = new ProspectusBL().MasterHandicapWeight(
                            _masterraceId,
                            Convert.ToInt32(drpdwnGender.SelectedItem.Value),
                            Convert.ToDecimal(txtbxHandicapWeight.Text),
                            Convert.ToInt32(drpdwnFromYear.SelectedItem.Value),
                            Convert.ToInt32(drpdwnTillYear.SelectedItem.Value),
                            txtbxComment.Text,
                            1,
                            "Insert");
                    }
                    else if (btnSave.Text.Equals("Update"))
                    {
                        status = new ProspectusBL().MasterHandicapWeight(
                            Convert.ToInt32(hdnfieldWeightID.Value),
                            Convert.ToInt32(drpdwnGender.SelectedItem.Value),
                            Convert.ToDecimal(txtbxHandicapWeight.Text),
                            Convert.ToInt32(drpdwnFromYear.SelectedItem.Value),
                            Convert.ToInt32(drpdwnTillYear.SelectedItem.Value),
                            txtbxComment.Text,
                            1,
                            "Update");
                      //  btnSave.Text = "Add";txt
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
                        hdnfieldWeightID.Value = null;
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
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusMasterHandicapWeightComments", prefixText);
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
                DataSet ds = new ProspectusBL().GetProspectusNameWithCombination(_masterraceId, "ProspectusMasterHandicapWeight");
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
                if (!hdnfieldWeightID.Value.Equals(""))
                {
                    var status = new ProspectusBL().MasterHandicapWeight(
                           Convert.ToInt32(hdnfieldWeightID.Value),
                            0,
                           Convert.ToDecimal(txtbxHandicapWeight.Text),
                           0,0,"",
                           1,
                           "Delete");
                    btnSave.Text = "Add";
                    ClearAllSelection(this);
                    BindData();
                    var message = "Record Deleted Successfully.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    hdnfieldWeightID.Value = "";
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
                    hdnfieldWeightID.Value = dataKey.Value.ToString();
                    //rdbtnStatus.Items.FindByText(hdnval.Value).Selected = true;
                    //rdbtnRaceType.Items.FindByText(hdnval.Value).Selected = true;
                    drpdwnGender.Items.FindByText(hdnval.Value).Selected = true;
                    txtbxHandicapWeight.Text = row.Cells[1].Text;
                    drpdwnFromYear.Items.FindByText(row.Cells[2].Text).Selected = true;
                    if (row.Cells[3].Text.Contains("&nbsp;"))
                    {
                        drpdwnTillYear.Items.FindByText("-- Please select --").Selected = true;
                    }
                    else
                    {
                        drpdwnTillYear.Items.FindByText(row.Cells[3].Text).Selected = true;
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Prospectus_Master_HWAPGender"))
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
                    var dtErrorResult = new ProspectusBL().Import30(dt, "ProspectusMasterHandicapWeight", _masterraceId);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Prospectus_Master_HWAPGender");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_Master_HWAPGender.xlsx");
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
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                //using (DataSet ds = new ProspectusBL().GetProspectusNameWithCombination(_masterraceId, "MasterAgeCondition"))
                using (DataSet ds = new ProspectusBL().GetExport(_masterraceId, "ProspectusMasterHandicapWeight"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                      //  dt.Columns.Remove("MACID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("ProspectusMasterHandicapWeight");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_Master_HWAPGender.xlsx");
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
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + ex.StackTrace + "');", true);
            }


        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}