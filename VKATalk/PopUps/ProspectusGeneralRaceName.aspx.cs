using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VKATalkBusinessLayer;
using System.Globalization;
using System.Data;

namespace VKATalk.PopUps
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;

    using OfficeOpenXml;

    public partial class ProspectusGeneralRaceName : System.Web.UI.Page
    {
        //MasterHorseBL Bl = new MasterHorseBL();
        int _userId = 1;
        private int _value = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
            if (!Request.QueryString["ProspectusGeneralID"].Equals(""))
            {
                _value = Convert.ToInt32(Request.QueryString["ProspectusGeneralID"]);
                hdnfldprospectusid.Value = Convert.ToString(_value);
            }
            if (!IsPostBack)
            {
                try
                {
                    BindDropDown(drpdwnYear, "Year", "YearName", "YearID");
                    drpdwnYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                    drpdwnYear.Items.FindByText("2020-2021").Selected = true;
                    if (_value != 0)
                    {
                        GetGridviewData();
                        btnSave.Text = "Add";
                    }
                }
                catch (Exception ex)
                {
                    object[] now = new object[] { "Page_Load (HorsePopup):", DateTime.Now, ", Issue Detail:", ex.Message + ex.StackTrace };
                    ErrorHandling.CheckEachSteps(string.Concat(now));
                    var message = "Incorrect Information.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
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
        public static List<string> AddCommentList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("RaceGeneralCommentList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
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
        public static List<string> AddProspectusList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusName", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0])));
            }
            return horseList;
        }
        private void GetGridviewData()
        {
            try
            {
                DataSet ds = new ProspectusBL().GetProspectusNameWithCombinationGeneral(_value, "GeneralRaceName");

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
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        /// <summary>
        /// Bind Horse Name already exist in out Database
        /// </summary>
        private void BindData(DataTable dt)
        {
            try
            {
                DataSet ds = null;

                if (btnSave.Text.Equals("Add"))
                {
                    ds = new ProspectusBL().GetProspectusNameWithCombinationGeneral(Convert.ToInt32(dt.Rows[0][1]), "GeneralRaceNameCurrentInsert");
                }
                else
                {
                    ds = new ProspectusBL().GetProspectusNameWithCombinationGeneral(Convert.ToInt32(dt.Rows[0][0]), "GeneralRaceNameCurrentUpdate");
                }
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
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
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
                string message = string.Empty;
                int newprospectusId = 0;
                DataTable dt;
                if (btnSave.Text.Equals("Add") && GvProspectus.Rows.Count > 0)
                {
                    message = "General Race Name Already Created.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        dt = new ProspectusBL().GeneralRaceName(
                            0,
                            Convert.ToInt32(hdnfieldMasterProsectusId.Value),
                            txtbxRaceName.Text,
                            txtbxRaceNameAlias.Text,
                            0,
                            0,
                            Convert.ToInt32(drpdwnYear.SelectedItem.Value),
                            "__-__-____",
                            txtbxComment.Text,
                            1,
                            "Insert",
                            hdfieldCurrentEx.Value);
                        hdnfldprospectusid.Value = Convert.ToString(dt.Rows[0][1]);
						
					}
                    else
                    {
                        lblParameters.Text = string.Empty;
                        lblParameters.Text = ViewState["GridViewRowID"] + "/" + hdnfieldMasterProsectusId.Value + '/' +
                                txtbxRaceName.Text + '/' +
                                txtbxRaceNameAlias.Text + '/' +
                                0 + '/' + 0 + '/' + Convert.ToInt32(drpdwnYear.SelectedItem.Value) + '/' +
                                "__-__-____" + '/' + _userId + '/' + 
                                "Update" + '/' + hdfieldCurrentEx.Value;
                        dt = new ProspectusBL().GeneralRaceName(
                            (int)ViewState["GridViewRowID"],
                            Convert.ToInt32(hdnfieldMasterProsectusId.Value),
                            txtbxRaceName.Text,
                            txtbxRaceNameAlias.Text,
                            0,
                            0,
                            Convert.ToInt32(drpdwnYear.SelectedItem.Value),
                            "__-__-____",
                            txtbxComment.Text,
                            _userId,
                            "Update",
                            hdfieldCurrentEx.Value);

                    }
                    newprospectusId = Convert.ToInt32(dt.Rows[0][2]);
                    if (newprospectusId == 2)
                    {
                        message = "Record Updated Successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ClearAllSelection(this);
                        BindData(dt);
                        btnSave.Text = "Add";
                    }
                    else if (newprospectusId == 4)
                    {
                        message = "General Race already created  with  Same Master Race, Center, Sesaon & Year.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ClearAllSelection(this);
                        hdnfldprospectusid.Value = "";
                    }
					else if (newprospectusId == 5)
					{
						message = "Record Activated successfully.";
						ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
						ClearAllSelection(this);
						BindData(dt);
					}
					else
                    {
                        ClearAllSelection(this);
                        this.Page.ClientScript.RegisterStartupScript(
                            this.GetType(),
                            "Popup",
                            this.btnSave.Text.Equals("Update")
                                ? "ShowPopup('Record Updated Successfully');"
                                : "ShowPopup('Record Added Successfully.');",
                            true);
                        BindData(dt);

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
        /// Close the window and pass the value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnfldprospectusid.Value != "")
                {
                    Session["ProspectusGeneralID"] = hdnfldprospectusid.Value;
                    //if (!Request.QueryString["MasterID"].Equals(""))
                    //{
                    //    Session["MasterRaceID"] = Request.QueryString["MasterID"];
                    //}
                    //if (!Session["ProspectusGeneralMasterRaceName"].Equals(""))
                    //{
                    //    Session["ProspectusMasterName"] = Session["ProspectusGeneralMasterRaceName"];
                    //}
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                }


            }
            catch (Exception ex)
            {
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
            hdfieldCurrentEx.Value = "";
            hdnfieldMasterProsectusId.Value = string.Empty;
            lblCenter.Text = string.Empty;
            lblSeason.Text = string.Empty;

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

            drpdwnYear.Items.FindByText("2020-2021").Selected = true;
        }

        protected void txtbxProspectusMasterName_OnTextChanged(object sender, EventArgs e)
        {
            //ClearAllSelection(this);
            lblCenter.Text = string.Empty;
            lblSeason.Text = string.Empty;
            string[] masterracename = txtbxProspectusMasterName.Text.Split('{');
            txtbxRaceName.Text = masterracename[0].ToString();
            var dt = new ProspectusBL().GetMasterRaceDetail(hdnfieldMasterProsectusId.Value);
            if (dt.Rows.Count > 0)
            {

                lblCenter.Text = dt.Rows[0][0].ToString();
                lblSeason.Text = dt.Rows[0][1].ToString();
            }
        }
        /// <summary>
        /// on select index change
        /// </summary>
        /// <param name="sender">value</param>
        /// <param name="e">event</param>
        protected void GvProspectus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = GvProspectus.SelectedRow;
                var dataKey = GvProspectus.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    HiddenField hdnfieldmasterracename = (HiddenField)row.FindControl("hdnfieldStatus");
					HiddenField hdnfieldGeneralRaceName = (HiddenField)row.FindControl("hdnfieldGeneralRaceName");
					HiddenField hndfieldmasterracenameid = (HiddenField)row.FindControl("hdnfieldMasterRaceNameID");
                    HiddenField hdnfieldCENTERNAME = (HiddenField)row.FindControl("hdnfieldCENTERNAME");
                    HiddenField hdnfieldSEASONNAME = (HiddenField)row.FindControl("hdnfieldSEASONNAME");
                    ViewState["GridViewRowID"] = dataKey.Value;
                    lblgeneralraceidnamegridviewvalue.Text = string.Empty;
                    lblgeneralraceidnamegridviewvalue.Text = "GeneralRaceNameID: " + ViewState["GridViewRowID"] + "/" + hndfieldmasterracenameid.Value;
                    hdnfieldMasterProsectusId.Value = hndfieldmasterracenameid.Value;
                    lblCenter.Text = string.Empty;
                    lblSeason.Text = string.Empty;
                    lblCenter.Text = hdnfieldCENTERNAME.Value;
                    lblSeason.Text = hdnfieldSEASONNAME.Value;
                    string stringmasterracename = hdnfieldmasterracename.Value.Equals("&nbsp;") ? "" : hdnfieldmasterracename.Value.Contains("&#39;") ? hdnfieldmasterracename.Value.Replace("&#39;", "'") : hdnfieldmasterracename.Value;
                    txtbxProspectusMasterName.Text = stringmasterracename.Replace("&amp;", "&");

                    string strracename = hdnfieldGeneralRaceName.Value.Equals("&nbsp;") ? "" : hdnfieldGeneralRaceName.Value.Contains("&#39;") ? hdnfieldGeneralRaceName.Value.Replace("&#39;", "'") : hdnfieldGeneralRaceName.Value;
                    txtbxRaceName.Text = strracename.Replace("&amp;", "&");
                    
                    
                    string strracenamealias = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&#39;") ? row.Cells[3].Text.Replace("&#39;", "'") : row.Cells[3].Text;
                    txtbxRaceNameAlias.Text = strracenamealias.Replace("&amp;", "&");

                    drpdwnYear.ClearSelection();
                    drpdwnYear.Items.FindByText(row.Cells[4].Text).Selected = true;
                    
                    if (row.Cells[6].Text.Contains("&quot;"))
                    {
                        string strcomments = row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&quot;") ? row.Cells[6].Text.Replace("&quot;", "\"") : row.Cells[6].Text;
                        txtbxComment.Text = strcomments.Replace("&amp;", "&");
                    }
                    else
                    {
                        string strcomments1 = row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&#39;") ? row.Cells[6].Text.Replace("&#39;", "'") : row.Cells[6].Text;
                        txtbxComment.Text = strcomments1.Replace("&amp;", "&");
                    }

                    if (row.Cells[0].Text.Equals("Cr"))
                    {
                        if (GvProspectus.Rows.Count == 2)
                        {
                            var message = "Please select Old Name.";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            ClearAllSelection(this);
                            btnSave.Text = "Add";
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



        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Prospectus_General_RaceName"))
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
                    var dtErrorResult = new ProspectusBL().Import30(dt, "ProspectusGeneralRaceName", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Race Name");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_General_RaceName.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
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
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }


        }

        //protected void btnClear_Click(object sender, EventArgs e)
        //{
        //    ClearAllSelection(this);
        //    btnSave.Text = "Add";
        //}

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                //using (DataSet ds = new ProspectusBL().GetProspectusNameWithCombination(0, "MasterMillion"))
                using (DataSet ds = new ProspectusBL().GetExport(0, "ProspectusGeneralRaceName"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("GeneralRaceID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Race Name");
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
                                if (dc.DataType == typeof(decimal)) ws.Column(i).Style.Numberformat.Format = "#0.00";
                            }
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                                    ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                                        ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style =
                                            OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_General_RaceName.xlsx");
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


        protected void txtbxRaceName_OnTextChanged(object sender, EventArgs e)
        {
            if (txtbxRaceName.Text != "")
            {
                txtbxRaceNameAlias.Text = txtbxRaceName.Text;
            }
        }


        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Verifies that the control is rendered */
        //}

    }
}