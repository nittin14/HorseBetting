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

    public partial class ResultLapTiming : System.Web.UI.Page
    {
        MasterHorseBL Bl = new MasterHorseBL();
        int _horseId = 0;
        static int show=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;

                if (!IsPostBack)
                {
                    hdnfielddate.Value = Request.QueryString["DivisionRaceDate"];
                    hdnfieldcenterid.Value = Request.QueryString["CenterMID"];
                    BindDropDown(drpdwnsectiondistance, "Master_DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                    drpdwnsectiondistance.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                    
                   
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void drpdwnHorseNo_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                var dt = new CardsBL().GetHorseDetail(Convert.ToInt32(hdnfieldOwnerStudID.Value), Convert.ToInt32(drpdwnHorseNo.SelectedItem.Value),0);
                if (dt.Rows.Count > 0)
                {
                    hdnfieldHorseID.Value=dt.Rows[0][0].ToString();
                    lblHorseName.Text = dt.Rows[0][1].ToString();
                    hdnfieldHorseNameID.Value = dt.Rows[0][2].ToString();
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

        private void BindDropDown(DropDownList ddl, String tablename, string textfield, String valuefield)
        {
            DataTable dt = new CardsBL().GetDropdownBind(tablename);
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
        public static List<string> AddStudOwnerList(string prefixText, int count, string contextKey)
        {
            //DataTable dt = new CardsBL().GetCardAutoFiller("ProspectusDivision", prefixText);
            // contextKey= 
            DataTable dt = new CardsBL().GetHorseNameAutoFillerMultiplewithoutsplit("ProspectusDivisionTote", prefixText, contextKey);
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
        public static List<string> AddProfessionalList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("ResultSectionalTiming", prefixText);
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
        public static List<string> AddCommentaryList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("AddCommentaryList", prefixText);
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
            DataTable dtDeclaration = new DataTable("Declaration");
            try
            {
                int status = 0;
                    if (btnSave.Text.Equals("Add"))
                    {
                        dtDeclaration.Columns.Add("DataEntryDate", typeof(string));
                        dtDeclaration.Columns.Add("DivisionRaceID_FK", typeof(int));
                        dtDeclaration.Columns.Add("LapTimingProviderID", typeof(int));
                        dtDeclaration.Columns.Add("HorseID", typeof(int));//3
                        dtDeclaration.Columns.Add("LapDistanceBreakUpMID", typeof(int));
                        dtDeclaration.Columns.Add("LapTiming", typeof(string));
                        dtDeclaration.Columns.Add("CreatedUserID", typeof(int));
                        dtDeclaration.Columns.Add("CreatedDate", typeof(DateTime));//7
                        dtDeclaration.Columns.Add("IsActive", typeof(int));
                        dtDeclaration.Columns.Add("LapTimingProviderNamePID", typeof(int));
                        dtDeclaration.Columns.Add("HorseNameID", typeof(int));

                    int rowcount = 0;
                       
                            dtDeclaration.Rows.Add();
                            if (Request.QueryString["DataEntryDate"].ToString().Equals("__-__-____"))
                            {
                                dtDeclaration.Rows[rowcount][0] = DBNull.Value;
                            }
                            else
                            {
                                string[] dateString = Request.QueryString["DataEntryDate"].ToString().Split('-');
                                DateTime enterDate =
                                    Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                                dtDeclaration.Rows[rowcount][0] = enterDate.ToString("yyyy-MM-dd 00:00:00");
                            }
                            dtDeclaration.Rows[rowcount][1] = Convert.ToInt32(hdnfieldOwnerStudID.Value);
                            if (hdnfieldProfessionalNameLapProviderID.Value.Equals(""))
                            {

                                dtDeclaration.Rows[rowcount][2] = DBNull.Value;
                                dtDeclaration.Rows[rowcount][9] = DBNull.Value;
                            }
                            else
                            {
                                dtDeclaration.Rows[rowcount][2] = new CardsBL().GetProfessionalID(Convert.ToInt32(hdnfieldProfessionalNameLapProviderID.Value));
                                dtDeclaration.Rows[rowcount][9] = Convert.ToInt32(Convert.ToInt32(hdnfieldProfessionalNameLapProviderID.Value));
                            }
                            dtDeclaration.Rows[rowcount][3] = Convert.ToInt32(hdnfieldHorseID.Value);//HorseID
                            dtDeclaration.Rows[rowcount][4] = Convert.ToInt32(drpdwnsectiondistance.SelectedItem.Value);
                            dtDeclaration.Rows[rowcount][5] = txtbxss1.Text + ":" + txtbxpulse1.Text;
                            dtDeclaration.Rows[rowcount][6] = 1;
                            dtDeclaration.Rows[rowcount][7] = DateTime.Now;
                            dtDeclaration.Rows[rowcount][8] = 1;
                            dtDeclaration.Rows[rowcount][10] = Convert.ToInt32(hdnfieldHorseNameID.Value);//HorseNameID



                        status = new CardsBL().CheckDuplicateLapTiming(Convert.ToInt32(hdnfieldOwnerStudID.Value), hdnfieldProfessionalNameLapProviderID.Value, hdnfieldHorseNameID.Value,
                                                drpdwnsectiondistance.SelectedItem.Value,
                                                txtbxss1.Text + ":" + txtbxpulse1.Text);
                }
                    else if (btnSave.Text.Equals("Update"))
                    {
                        var timing = txtbxss1.Text + ":" + txtbxpulse1.Text;
                        status = new CardsBL().ResultLapTiming((int)ViewState["GlobalID"], Convert.ToInt32(hdnfieldOwnerStudID.Value), 
                                                Convert.ToInt32(hdnfieldProfessionalNameLapProviderID.Value), timing, 1, "Update", Convert.ToInt32(drpdwnsectiondistance.SelectedItem.Value),
                                                hdnfieldHorseNameID.Value);
                        btnSave.Text = "Add";
                    }

                    if (status == 1)
                    {
                        status = new CardsBL().AddLapTiming(dtDeclaration);
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
                //}
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtbxOwnerStud.Text.Equals(""))
                {
                    BindData();
                }
                else
                {
                    var message = "Please select Division Race Name.";
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
        /// BindData with Gridview
        /// </summary>
        private void BindData()
        {
            if (hdnfieldOwnerStudID.Value.Equals(""))
            {
                GvHorseGlobal.DataSource = new DataTable();
                GvHorseGlobal.DataBind();
            }
            else
            {
                DataTable dt = new CardsBL().GetToteDividentDetail(Convert.ToInt32(hdnfieldOwnerStudID.Value), "ResultLapTiming", "", "");
                if (dt.Rows.Count > 0)
                {
                    GvHorseGlobal.DataSource = dt;
                    GvHorseGlobal.DataBind();
                }
                else
                {
                    GvHorseGlobal.DataSource = new DataTable();
                    GvHorseGlobal.DataBind();
                }
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
                if (!ViewState["GlobalID"].Equals(""))
                {
                    var status = new CardsBL().ResultLapTiming(
                            (int)ViewState["GlobalID"],0,0,"",
                            1,
                            "Delete",0,string.Empty);


                    ClearAllSelection(this);
                    BindData();
                    btnSave.Text = "Add";
                    var message = "Record Deleted Successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["GlobalID"] = "";
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
                HiddenField hdndivisionid = (HiddenField)row.FindControl("hdnfieldDivisionRaceID");
                HiddenField hdnfieldDivisionRaceName = (HiddenField)row.FindControl("hdnfieldDivisionRaceName");
                HiddenField hdnfieldProfessionalNameID = (HiddenField)row.FindControl("hdnfieldProfessionalNameID");
                HiddenField hdnfieldProfessionalName = (HiddenField)row.FindControl("hdnfieldProfessionalName");
                HiddenField hdnfieldHorseNameIDG = (HiddenField)row.FindControl("hdnfieldHorseNameIDG");
                var dataKey = GvHorseGlobal.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    //ClearAllSelection(this);
                    hdnfieldHorseNameID.Value = hdnfieldHorseNameIDG.Value;
                       rqv.Enabled = false;
                       ViewState["GlobalID"] = dataKey.Value;
                       if (!hdndivisionid.Value.Contains("&nbsp;"))
                    {
                        txtbxOwnerStud.Text = hdnfieldDivisionRaceName.Value;
                        //hdnfieldDivisionRaceMID.Value = hdndivisionid.Value;
                        hdnfieldOwnerStudID.Value = hdndivisionid.Value;
                    }

                    //hdnfieldProfessionalNameMID.Value=hdnfieldProfessionalNameID.Value;
                    hdnfieldProfessionalNameLapProviderID.Value = hdnfieldProfessionalNameID.Value;
                    txtbxProfessionalName.Text= hdnfieldProfessionalName.Value;
                    drpdwnHorseNo.ClearSelection();
                    drpdwnHorseNo.Items.FindByText(row.Cells[2].Text).Selected = true;
                    drpdwnsectiondistance.ClearSelection();
                    drpdwnsectiondistance.Items.FindByText(row.Cells[3].Text).Selected = true;
                    if (!row.Cells[4].Text.Contains("&quot;"))
                    {
                        string[] timing = row.Cells[4].Text.Split(':');
                        txtbxss1.Text = timing[0];
                        txtbxpulse1.Text = timing[1];
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


        protected void btnClear_Click(object sender, EventArgs e)
        {
            GvHorseGlobal.DataSource = new DataTable();
            GvHorseGlobal.DataBind();
            ClearAllSelection(this);
            btnSave.Text = "Add";
        }


        public void ClearAllSelection(Control parent)
        {
            rqv.Enabled = true;

            if (chkbxfix.Checked.ToString().Equals("True"))
            {
                if (chkbxfix2.Checked.ToString().Equals("True"))
                {
                    drpdwnsectiondistance.ClearSelection();
                    txtbxss1.Text = string.Empty;
                    txtbxpulse1.Text = string.Empty;
                }
                else
                {
                    drpdwnHorseNo.ClearSelection();
                    hdnfieldHorseID.Value = string.Empty;
                    hdnfieldHorseNameID.Value = string.Empty;
                    lblHorseName.Text = string.Empty;
                    drpdwnsectiondistance.ClearSelection();
                    txtbxss1.Text = string.Empty;
                    txtbxpulse1.Text = string.Empty;
                }
            }
          
            if (chkbxfix2.Checked.ToString().Equals("False") && chkbxfix.Checked.ToString().Equals("False"))
            {
                hdnfieldOwnerStudID.Value = string.Empty;
                txtbxOwnerStud.Text = string.Empty;
                hdnfieldProfessionalNameLapProviderID.Value = string.Empty;
                txtbxProfessionalName.Text = string.Empty;
                drpdwnHorseNo.ClearSelection();
                hdnfieldHorseID.Value = string.Empty;
                hdnfieldHorseNameID.Value = string.Empty;
                lblHorseName.Text = string.Empty;
                drpdwnsectiondistance.ClearSelection();
                txtbxss1.Text = string.Empty;
                txtbxpulse1.Text = string.Empty;
            }
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Result_LapTiming"))
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
                    var dtErrorResult = Bl.Import30(dt, "Result_LapTiming");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                       // Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("LapTiming");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Result_LapTiming.xlsx");
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
                using (DataSet ds = new MasterHorseBL().GetExport(_horseId, "Result_LapTiming"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
 //                       dt.Columns.Remove("HorseOwnerStudID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("LapTiming");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Result_LapTiming.xlsx");
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

        protected void txtbxOwnerStud_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var dt = new CardsBL().GetHorseDetail(Convert.ToInt32(hdnfieldOwnerStudID.Value), 0);
                if (dt.Rows.Count > 0)
                {
                    drpdwnHorseNo.DataSource = dt;
                    drpdwnHorseNo.DataTextField = "HorseNo";
                    drpdwnHorseNo.DataValueField = "HorseNo";
                    drpdwnHorseNo.DataBind();
                    drpdwnHorseNo.Items.Insert(0, new ListItem("--", "-1"));
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