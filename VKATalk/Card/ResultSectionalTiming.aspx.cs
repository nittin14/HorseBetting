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

    public partial class ResultSectionalTiming : System.Web.UI.Page
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

        ///// <summary>
        ///// AddStudOwnerList
        ///// </summary>
        ///// <param name="prefixText"></param>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod]
        //public static List<string> AddCommentatorList(string prefixText, int count)
        //{
        //    DataTable dt = new CardsBL().GetCardAutoFiller("CommentatorList", prefixText);
        //    List<string> horseList = new List<string>();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
        //                dt.Rows[i][1].ToString(),
        //                Convert.ToString(dt.Rows[i][0])));
        //    }
        //    return horseList;
        //}

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
                        dtDeclaration.Columns.Add("SectionalTimingProviderID", typeof(int));
                        dtDeclaration.Columns.Add("SectionDistanceBreakUpMID", typeof(int));
                        dtDeclaration.Columns.Add("SectionalTiming", typeof(string));
                        dtDeclaration.Columns.Add("CreatedUserID", typeof(int));
                        dtDeclaration.Columns.Add("CreatedDate", typeof(DateTime));
                        dtDeclaration.Columns.Add("IsActive", typeof(int));
                        dtDeclaration.Columns.Add("SectionTimingProviderNamePID", typeof(int));
                    
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
                        if (hdnfieldCommentator.Value.Equals(""))
                        {

                            dtDeclaration.Rows[rowcount][2] = DBNull.Value;
                            dtDeclaration.Rows[rowcount][8] = DBNull.Value;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][2] = new CardsBL().GetProfessionalID(Convert.ToInt32(hdnfieldCommentator.Value));
                            dtDeclaration.Rows[rowcount][8] = Convert.ToInt32(Convert.ToInt32(hdnfieldCommentator.Value));
                        }

                        dtDeclaration.Rows[rowcount][3] = Convert.ToInt32(drpdwnsectiondistance.SelectedItem.Value);
                            dtDeclaration.Rows[rowcount][4] = txtbxmm1.Text + ":"  + txtbxss1.Text + ":" + txtbxpulse1.Text;
                            
                            dtDeclaration.Rows[rowcount][5] = 1;
                            dtDeclaration.Rows[rowcount][6] = DateTime.Now;
                            dtDeclaration.Rows[rowcount][7] = 1;

                    status= new CardsBL().CheckDuplicateSectionalTiming(Convert.ToInt32(hdnfieldOwnerStudID.Value), hdnfieldCommentator.Value, drpdwnsectiondistance.SelectedItem.Value, txtbxmm1.Text + ":" + txtbxss1.Text + ":" + txtbxpulse1.Text);
                    }
                    else if (btnSave.Text.Equals("Update"))
                    {
                        var timing = txtbxmm1.Text + ":" + txtbxss1.Text + ":" + txtbxpulse1.Text;
                        status = new CardsBL().ResultSectionalTiming((int)ViewState["GlobalID"], Convert.ToInt32(hdnfieldOwnerStudID.Value), Convert.ToInt32(hdnfieldCommentator.Value), timing, 1, "Update", Convert.ToInt32(drpdwnsectiondistance.SelectedItem.Value));
                       
                        btnSave.Text = "Add";
                    }

                    if (status == 1)
                    {
                        status = new CardsBL().AddSectionalTiming(dtDeclaration);
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
            DataTable dt = new CardsBL().GetToteDividentDetail(Convert.ToInt32(hdnfieldOwnerStudID.Value), "ResultSectionalTiming", hdnfielddate.Value, hdnfieldcenterid.Value);
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
                    var status = new CardsBL().ResultSectionalTiming(
                            (int)ViewState["GlobalID"],0,0,"",
                            1,
                            "Delete",0);


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
                //DropDownList drpdwnsectiondistance = (DropDownList)row.FindControl("drpdwnsectiondistance");
                var dataKey = GvHorseGlobal.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                       ClearAllSelection(this);
                       rqv.Enabled = false;
                       ViewState["GlobalID"] = dataKey.Value;
                       if (!hdndivisionid.Value.Contains("&nbsp;"))
                    {
                        txtbxOwnerStud.Text = hdnfieldDivisionRaceName.Value;
                        hdnfieldDivisionRaceMID.Value = hdndivisionid.Value;
                    }
                    
                    hdnfieldProfessionalNameMID.Value=hdnfieldProfessionalNameID.Value;
                    txtbxCommentator.Text= hdnfieldProfessionalName.Value;
                    drpdwnsectiondistance.Items.FindByText(row.Cells[2].Text).Selected = true;
                    if (!row.Cells[3].Text.Contains("&quot;"))
                    {
                        string[] timing = row.Cells[3].Text.Split(':');
                        txtbxmm1.Text = timing[0];
                        txtbxss1.Text = timing[1];
                        txtbxpulse1.Text = timing[2];
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
            ClearAllSelection(this);
            btnSave.Text = "Add";
        }


        public void ClearAllSelection(Control parent)
        {
            if (chkboxfix.Checked.ToString().Equals("True"))
            {
                drpdwnsectiondistance.ClearSelection();
                txtbxmm1.Text = string.Empty;
                txtbxss1.Text = string.Empty;
                txtbxpulse1.Text = string.Empty;
            }
            else
            {
                rqv.Enabled = true;
                hdnfieldCommentator.Value = string.Empty;
               // hdnfieldOwnerStudID.Value = string.Empty;
                GvHorseGlobal.DataSource = new DataTable();
                GvHorseGlobal.DataBind();
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
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Result_SectionalTiming"))
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
                    var dtErrorResult = Bl.Import30(dt, "SectionalTiming");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                       // Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("SectionalTiming");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Result_SectionalTiming.xlsx");
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
                using (DataSet ds = new MasterHorseBL().GetExport(_horseId, "SectionalTiming"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
 //                       dt.Columns.Remove("HorseOwnerStudID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("SectionalTiming");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Result_SectionalTiming.xlsx");
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