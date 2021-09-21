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

    public partial class ProspectusGeneralSeasonalCondition : System.Web.UI.Page
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
                    //if (Request.QueryString["PageName"].Equals("1"))
                    //{
                    //    txtbxGeneralRaceName.Enabled = false;
                    //    if (!Request.QueryString["GeneralRaceName"].Equals(""))
                    //    {
                    //        var masterracename = Session["ProspectusGeneralRaceName"].ToString();//Request.QueryString["GeneralRaceName"];
                    //        string[] masterrcename = masterracename.Split('{');
                    //        txtbxGeneralRaceName.Text = masterrcename[0];
                            
                    //    }

                    if (_value != 0)
                    {
                        BindData();
                    }

                    //}
                    //else
                    //{
                    //    txtbxGeneralRaceName.Enabled = true;
                    //    hdnfieldProspectusGeneralRaceNameID.Value = Request.QueryString["GeneralRaceNameID"].ToString();
                    //}
                    MainBinding();
                    BindDropDown(drpdwnSeasonalCondition, "SeasonalConditional", "SeasonalCondition", "SeasonalConditionID");
                    drpdwnSeasonalCondition.Items.Insert(0, new ListItem("-- Please select --", "-1"));
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


        private void MainBinding()
        {
            if (Request.QueryString["PageName"].Equals("1"))
            {
                
                if (!Request.QueryString["GeneralRaceName"].Equals(""))
                {
                    var masterracename = Session["ProspectusGeneralRaceName"].ToString();//Request.QueryString["GeneralRaceName"];
                    string[] masterrcename = masterracename.Split('{');
                    txtbxGeneralRaceName.Text = masterrcename[0];

                }
                txtbxGeneralRaceName.Enabled = false;
            }
            else
            {
                txtbxGeneralRaceName.Enabled = true;
                hdnfieldProspectusGeneralRaceNameID.Value = Request.QueryString["GeneralRaceNameID"].ToString();
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
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddGeneralNameList(string prefixText, int count, string contextKey)
        {
            // DataTable dt = new ProspectusBL().GetprospectusAutoFiller("GeneralRaceList", prefixText, contextKey);
            DataTable dt = new ProspectusBL().GetprospectusAutoFillerWithParameters("GeneralRaceList", prefixText, contextKey);
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
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddCommentsList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("SeasonalConditionComments", prefixText);
            List<string> currentMissionComments = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currentMissionComments.Add(dt.Rows[i][0].ToString());
            }
            return currentMissionComments;
        }


        private void BindData()
        {
            try
            {
                DataSet ds = new ProspectusBL().GetProspectusNameWithCombinationGeneral(_value, "GeneralSeasonalCondition");

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
                if (Request.QueryString["PageName"].Equals("2"))
                {
                    _value = Convert.ToInt32(hdnfieldGeneralRaceNameID.Value);
                }
                int newprospectusId = 0;
               // DataTable dt;
                int status = 0;
                DataSet ds = null;
				//if (btnSave.Text.Equals("Add"))
				//{
				//    ds = new ProspectusBL().GetProspectusTillDateValidation(_value, "ProspectusSeasonalCondition", txtbxFromDate.Text, btnSave.Text);
				//}
				//else
				//{
				//    ds = new ProspectusBL().GetProspectusTillDateValidation((int)ViewState["GridViewRowID"], "ProspectusSeasonalCondition", txtbxFromDate.Text, btnSave.Text);
				//}
				//Int64 checkTillDate = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
				Int64 checkTillDate = -0;

				if (_value == 0)
                {
                    var message = "Please select General Race Name.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else if (checkTillDate == 2)
                {
                    var message = "Please Check the existing record TillYear.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        status = new ProspectusBL().GeneralSeasonalCondition(
                            _value,
                            Convert.ToInt32(drpdwnSeasonalCondition.SelectedItem.Value),
                            txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            txtbxComment.Text,
                            1,
                            "Insert");
                        //hdnfldprospectusid.Value = Convert.ToString(dt.Rows[0][1]);

                    }
                    else
                    {
                        status = new ProspectusBL().GeneralSeasonalCondition(
                            (int)ViewState["GridViewRowID"],
                            Convert.ToInt32(drpdwnSeasonalCondition.SelectedItem.Value),
                            txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            txtbxComment.Text,
                            1,
                            "Update");

                    }
                    //newprospectusId = Convert.ToInt32(dt.Rows[0][2]);
                    if (status == 1 || status == 2)
                    {
                        var message = status == 1 ? "Record added successfully." : "Record update successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        if (status == 2)
                        {
                            btnSave.Text = "Add";
                        }
                        MainBinding();
                    }
                    else if (status == 5)
                    {
                        var message = "Record activated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                        MainBinding();
                    }
                    else if (status == 4)
                    {
                        var message = "Record already exist.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                        MainBinding();
                    }
                    else
                    {
                        MainBinding();
                        ErrorHandling.CheckEachSteps(Convert.ToString(status));
                        var message = "Issue in Record. (Status) : " + status;
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
        /// Delete record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ViewState["GridViewRowID"].Equals(""))
                {
                   var status = new ProspectusBL().GeneralSeasonalCondition(
                            (int)ViewState["GridViewRowID"],
                            0,
                            "__-__-____",
							"__-__-____",
                            "",
                            1,
                            "Delete");
                    ClearAllSelection(this);
                    BindData();
                    btnSave.Text = "Add";
                    var message = "Record Deleted Successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["GridViewRowID"] = "";
                    MainBinding();
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
                    if (!Session["ProspectusGeneralRaceName"].Equals(""))
                    {
                        Session["ProspectusGeneralName"] = Session["ProspectusGeneralRaceName"];
                    }
                    //if (!Request.QueryString["MasterRaceName"].Equals(""))
                    //{
                    //    //Session["ProspectusMasterRaceName"]
                    //    //Session.Abandon("ProspectusMasterRaceName");
                    //   //   Session.Remove("ProspectusMasterRaceName");
                    //   // Session["ProspectusMasterName"] = Request.QueryString["MasterRaceName"];
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
            MainBinding();
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
                if ((x.GetType() == typeof(RadioButtonList)))
                {
                    ((RadioButtonList)(x)).SelectedValue = "-1";
                }

                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
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
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldtypeofdate");
                var dataKey = GvProspectus.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    MainBinding();
                    ViewState["GridViewRowID"] = dataKey.Value;
                    //txtbxObservation.Text = hdnval.Value.Equals("&nbsp;") ? "" : hdnval.Value.Contains("&#39;") ? hdnval.Value.Replace("&#39;", "'") : hdnval.Value;
                    drpdwnSeasonalCondition.Items.FindByText(hdnval.Value).Selected = true;
                    //string strcomments = string.Empty;
                    if (row.Cells[1].Text.Contains("&quot;"))
                    {
                        txtbxFromDate.Text = "";
                    }
                    else
                    {
                        txtbxFromDate.Text = row.Cells[1].Text;
                    }
                    if (row.Cells[2].Text.Contains("&quot;"))
                    {
                        txtbxTillDate.Text = "";
                    }
                    else
                    {
                        txtbxTillDate.Text = row.Cells[2].Text;
                    }
                    if (row.Cells[3].Text.Contains("&quot;"))
                    {
                        txtbxComment.Text = "";
                    }
                    else
                    {
                        string strcomments1 = row.Cells[3].Text;
                        strcomments1 = strcomments1.Replace("&nbsp;", "");
                        txtbxComment.Text = strcomments1.Replace("&amp;", "&");
                    }
                }
            }
            catch (Exception ex)
            {
                btnSave.Text = "Add";
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Prospectus_General_SeasonalCondition"))
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
                    var dtErrorResult = new ProspectusBL().Import30(dt, "ProspectusGeneralSeasonalCondition", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Prospectus_General_SeasonalCondition");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_General_SeasonalCondition.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        BindData();
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
                using (DataSet ds = new ProspectusBL().GetExport(0, "ProspectusGeneralSeasonalCondition"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("General_SeasonalCondition");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_General_SeasonalCondition.xlsx");
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

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Verifies that the control is rendered */
        //}

    }
}