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

    public partial class ResultRaceCommentary : System.Web.UI.Page
    {
        MasterHorseBL Bl = new MasterHorseBL();
        int _horseId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;

                if (!IsPostBack)
                {
                    hdnfieldDivisionRaceDate.Value = Request.QueryString["DivisionRaceDate"];
                    hdnfieldCenterID.Value = Request.QueryString["CenterMID"];
                   
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
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
        public static List<string> AddProfessionalName(string prefixText, int count)
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
        public static List<string> AddCommentaryCommentsList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("AddCommentaryList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
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
                var highlight = string.Empty;
                if(chkbxHighlight.Checked.Equals(true))
                {
                    highlight = "true";
                }
                else
                {
                    highlight = "false";
                }

                    if (btnSave.Text.Equals("Add"))
                    {
                    status = new CardsBL().ResultRaceCommentary(
                        0,
                        Convert.ToInt32(hdnfieldOwnerStudID.Value),
                        Convert.ToInt32(hdnfieldProfessionalNameCommentator.Value),
                        Convert.ToInt32(drpdwnHorseName.SelectedItem.Value),
                        txtbxCommentary.Text,
                        highlight,1
                        ,"Insert");
                    }
                    else if (btnSave.Text.Equals("Update"))
                    {
                        status = new CardsBL().ResultRaceCommentary(
                            (int)ViewState["GlobalID"],
                            Convert.ToInt32(hdnfieldOwnerStudID.Value),
                            Convert.ToInt32(hdnfieldProfessionalNameCommentator.Value),
                            Convert.ToInt32(drpdwnHorseName.SelectedItem.Value),
                            txtbxCommentary.Text,
                            highlight,1,
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
                //}
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
            DataTable dt = new CardsBL().GetToteDividentDetail(Convert.ToInt32(hdnfieldOwnerStudID.Value), "RaceCommentary", hdnfieldDivisionRaceDate.Value, hdnfieldCenterID.Value);
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
            //if (!hdnfieldOwnerStudID.Value.Equals(""))
            //{
            //    DataTable dt = new CardsBL().GetToteDividentDetail(Convert.ToInt32(hdnfieldOwnerStudID.Value), "RaceCommentary", hdnfieldDivisionRaceDate.Value, hdnfieldCenterID.Value);
            //    if (dt.Rows.Count > 0)
            //    {
            //        GvHorseGlobal.DataSource = dt;
            //        GvHorseGlobal.DataBind();
            //    }
            //    else
            //    {
            //        GvHorseGlobal.DataSource = new DataTable();
            //        GvHorseGlobal.DataBind();
            //    }
            //}
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
                    var status = new CardsBL().ResultRaceCommentary(
                            (int)ViewState["GlobalID"],0,0,0,"","",1,
                            "Delete");


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
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldDivisionRaceName");
                HiddenField hdndivisionid = (HiddenField)row.FindControl("hdnfieldStatus");
                HiddenField hdnfdProfessionalNameID = (HiddenField)row.FindControl("hdnfdProfessionalNameID");
                HiddenField hdnflHorseNameID = (HiddenField)row.FindControl("hdnflHorseNameID");
                var dataKey = GvHorseGlobal.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GlobalID"] = dataKey.Value;
                    if (!hdnval.Value.Contains("&nbsp;"))
                    {
                        txtbxOwnerStud.Text = hdnval.Value;
                        hdnfieldOwnerStudID.Value = hdndivisionid.Value;
                    }
                    if (row.Cells[1].Text.Contains("&quot;"))
                    {
                        txtbxCommentator.Text = row.Cells[1].Text.Equals("&nbsp;") ? "" : row.Cells[1].Text.Contains("&quot;") ? row.Cells[1].Text.Replace("&quot;", "\"") : row.Cells[1].Text;
                        hdnfieldProfessionalNameCommentator.Value = hdnfdProfessionalNameID.Value;
                    }
                    else
                    {
                        txtbxCommentator.Text = row.Cells[1].Text.Equals("&nbsp;") ? "" : row.Cells[1].Text.Contains("&#39;") ? row.Cells[1].Text.Replace("&#39;", "'") : row.Cells[1].Text;
                        hdnfieldProfessionalNameCommentator.Value = hdnfdProfessionalNameID.Value;
                    }
                    if (row.Cells[2].Text.Contains("&#39;")) {
                        var horsename = row.Cells[2].Text.Replace("&#39;", "'");
                        drpdwnHorseName.Items.FindByText(horsename).Selected = true;
                    }
                    else
                    {
                        drpdwnHorseName.Items.FindByText(row.Cells[2].Text).Selected = true;
                    }
                    
                    if (row.Cells[3].Text.Contains("&quot;"))
                    {
                        txtbxCommentary.Text = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&quot;") ? row.Cells[3].Text.Replace("&quot;", "\"") : row.Cells[3].Text;
                    }
                    else
                    {
                        txtbxCommentary.Text = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&#39;") ? row.Cells[3].Text.Replace("&#39;", "'") : row.Cells[3].Text;
                    }
                    if (row.Cells[4].Text.Contains("Highlight"))
                    {
                        chkbxHighlight.Checked = true;
                    }
                    else
                    {
                        chkbxHighlight.Checked = false;
                    }
                }
                BindData();
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
            
            if (CheckBox2.Checked.Equals(true))
            {
                drpdwnHorseName.ClearSelection();
                txtbxCommentary.Text = string.Empty;
               // hdnfieldcommentary.Value = string.Empty;
                chkbxHighlight.Checked = false;
                GvHorseGlobal.DataSource = new DataTable();
                GvHorseGlobal.DataBind();
            }
            else if (chkboxfix1.Checked.Equals(true))
            {
                txtbxCommentator.Text = string.Empty;
                hdnfieldProfessionalNameCommentator.Value = string.Empty;
                drpdwnHorseName.ClearSelection();
                txtbxCommentary.Text = string.Empty;
                //  hdnfieldcommentary.Value = string.Empty;
                chkbxHighlight.Checked = false;
                GvHorseGlobal.DataSource = new DataTable();
                GvHorseGlobal.DataBind();
            }
            else if (btnSave.Text.Equals("Add"))
            {
                txtbxCommentator.Text = string.Empty;
                hdnfieldProfessionalNameCommentator.Value = string.Empty;
                drpdwnHorseName.ClearSelection();
                txtbxCommentary.Text = string.Empty;
                //  hdnfieldcommentary.Value = string.Empty;
                chkbxHighlight.Checked = false;
                //GvHorseGlobal.DataSource = new DataTable();
                //GvHorseGlobal.DataBind();
            }
            
            else
            {
                txtbxOwnerStud.Text = string.Empty;
                hdnfieldOwnerStudID.Value = string.Empty;
                txtbxCommentator.Text = string.Empty;
                hdnfieldProfessionalNameCommentator.Value = string.Empty;
                drpdwnHorseName.ClearSelection();
                txtbxCommentary.Text = string.Empty;
               // hdnfieldcommentary.Value = string.Empty;
                chkbxHighlight.Checked = false;
                GvHorseGlobal.DataSource = new DataTable();
                GvHorseGlobal.DataBind();
            }

            chkbxHighlight.Checked = false;
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_Result_Commentary"))
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
                    var dtErrorResult = Bl.Import30(dt, "Card_Result_Commentary");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Result_Commentary");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Result_Commentary.xlsx");
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
                using (DataSet ds = new MasterHorseBL().GetExport(_horseId, "Card_Result_Commentary"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
 //                       dt.Columns.Remove("HorseOwnerStudID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Result_Commentary");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Result_Commentary.xlsx");
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
                var dt = new CardsBL().GetHorseDetail(Convert.ToInt32(hdnfieldOwnerStudID.Value), 1);
                if (dt.Rows.Count > 0)
                {
                    drpdwnHorseName.DataSource = dt;
                    drpdwnHorseName.DataTextField = "HorseName";
                    drpdwnHorseName.DataValueField = "HorseNameID";
                    drpdwnHorseName.DataBind();
                    drpdwnHorseName.Items.Insert(0, new ListItem("--", "-1"));
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