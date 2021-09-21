using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VKATalkBusinessLayer;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using OfficeOpenXml;

namespace VKATalk.Master
{
    using System.Collections.Generic;

    public partial class MasterDisease : System.Web.UI.Page
    {
        MasterBL Bl = new MasterBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
            
        }


        /// <summary>
        /// Fill MyComments
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> DiseaseCommentList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("DiseaseCommentList", prefixText);
            List<string> ImpactList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ImpactList.Add(dt.Rows[i][0].ToString());
            }
            return ImpactList;
        }


        /// <summary>
        /// Fill MyComments
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> PrecautionsList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("PrecautionsList", prefixText);
            List<string> ImpactList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ImpactList.Add(dt.Rows[i][0].ToString());
            }
            return ImpactList;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int status;
                int checkcondition = 0;
                if (BtnSubmit.Text.Equals("Add"))
                {
                    status = Bl.MasterDisease(0, txtbxDisease.Text, txtbxAlias.Text, txtbxDiseaseInDetail.Text, txtbxmedicalname.Text, txtbxPerformanceImpact.Text,txtbxtreatment.Text,txtbxPrecautions.Text,txtbxComments.Text, 1,"Insert");
                    checkcondition = 1;
                }
                else
                {
                    status = Bl.MasterDisease((int)ViewState["GridViewRowID"], txtbxDisease.Text, txtbxAlias.Text, txtbxDiseaseInDetail.Text, txtbxmedicalname.Text, txtbxPerformanceImpact.Text, txtbxtreatment.Text,txtbxPrecautions.Text, txtbxComments.Text, 1, "Update");
                    //BtnSubmit.Text = "Add";
                    checkcondition = 2;
                }
                if (status == 1 || status == 2)
                {
                    var message = checkcondition == 1 ? "Record added successfully." : "Record update successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    BindData();
                    ClearAllSelection(this);
                    if (checkcondition == 2)
                    {
                        BtnSubmit.Text = "Add";
                    }
                }
                else if (status == 5)
                {
                    var message = "Record activated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    BindData();
                    ClearAllSelection(this);
                    BtnSubmit.Text = "Add";
                }
                else if (status == 4)
                {
                    string message = "Record already exist.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearAllSelection(this);
                    BtnSubmit.Text = "Add";
                    //BtnSubmit.Text = "Update";
                }
                else
                {
                    ErrorHandling.CheckEachSteps(Convert.ToString(status));
                    string message = "Issue in Record. (Status) : " + status;
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        
        protected void btnDownload_Click(object sender, EventArgs e)
        {
         
        }

        private void BindData()
        {
             DataTable dt = Bl.GetMasterData("MasterDisease");
            if (dt.Rows.Count > 0)
            {
                GvDisease.DataSource = dt;
                GvDisease.DataBind();
            }
            else
            {
                DataTable dt1 = new DataTable();
                GvDisease.DataSource = dt1;
                GvDisease.DataBind();
            }
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

        protected void GvDisease_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Text = "Update";
                GridViewRow row = GvDisease.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvDisease.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
                    if (!hdnval.Value.Equals("&nbsp;"))
                    {
                        string disease = hdnval.Value;
                        txtbxDisease.Text = disease.Replace("&amp;", "&");
                    }
                    if (!row.Cells[2].Text.Equals("&nbsp;"))
                    {
                        string alias = row.Cells[2].Text;
                        txtbxAlias.Text = alias.Replace("&amp;", "&");
                    }
                    if (!row.Cells[3].Text.Equals("&nbsp;"))
                    {
                        string diseaseindetail = row.Cells[3].Text;
                        diseaseindetail= diseaseindetail.Replace("&amp;", "&");
                        diseaseindetail = diseaseindetail.Replace("&quot;", "\"");
                        txtbxDiseaseInDetail.Text = diseaseindetail.Replace("&#39;", "'");
                    }
                    if (!row.Cells[4].Text.Equals("&nbsp;"))
                    {
                        string medicalname = row.Cells[4].Text;
                        medicalname= medicalname.Replace("&amp;", "&");
                        medicalname= medicalname.Replace("&quot;", "\"");
                        txtbxmedicalname.Text = medicalname.Replace("&#39;", "'");
                    }
                    if (!row.Cells[5].Text.Equals("&nbsp;"))
                    {
                        string perfomanceimpact = row.Cells[5].Text;
                        perfomanceimpact = perfomanceimpact.Replace("&amp;", "&");
                        perfomanceimpact = perfomanceimpact.Replace("&quot;", "\"");
                        txtbxPerformanceImpact.Text = perfomanceimpact.Replace("&#39;", "'");
                    }
                    if (!row.Cells[6].Text.Equals("&nbsp;"))
                    {
                        string treatment = row.Cells[6].Text;
                        treatment=treatment.Replace("&amp;", "&");
                        treatment=treatment.Replace("&quot;", "\"");
                        
                        txtbxtreatment.Text = treatment.Replace("&#39;", "'");
                    }
                    if (!row.Cells[7].Text.Equals("&nbsp;"))
                    {
                        string precaution = row.Cells[7].Text;
                        precaution=precaution.Replace("&amp;", "&");
                        precaution=precaution.Replace("&quot;", "\"");
                        txtbxPrecautions.Text = precaution.Replace("&#39;", "'");
                    }
                    if (!row.Cells[8].Text.Equals("&nbsp;"))
                    {
                        string comments = row.Cells[8].Text;
                        comments=comments.Replace("&amp;", "&");
                        comments=comments.Replace("&quot;", "\"");
                        txtbxComments.Text = comments.Replace("&#39;", "'");

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            BtnSubmit.Text = "Add";
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
                    var status = Bl.MasterDisease((int)ViewState["GridViewRowID"], "", "","","","","","","", 1, "Delete");
                    ClearAllSelection(this);
                    BindData();
                    BtnSubmit.Text = "Add";
                    var message = "Record deleted successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["GridViewRowID"] = "";
                }
                else
                {
                    var message = "No record Found.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception exception)
            {
                string message = "Issue in Record.";
                ErrorHandling.SendErrorToText(exception);
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
                using (DataTable dt = Bl.GetMasterData("MasterDisease"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("RowCount");
                        dt.Columns.Remove("DiseaseID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Disease");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Disease.xlsx");
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
                var message = "Incorrect Information";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message  + "');", true);
            }


        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Disease"))
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
            }
        }


        private void Import_To_Grid(string FilePath, string Extension)
        {
            try
            {
                string strConn;
                bool hasHeaders = false;
                string HDR = hasHeaders ? "Yes" : "No";
                if (FilePath.Substring(FilePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
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
                    var dtErrorResult = new MasterBL().ImportExcel30(dt, "Disease");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        //                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                        BindData();
                        var message = "Issue in few record. Please check the XL sheet.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Disease");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Disease.xlsx");
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
                    var message = "No Record found.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

    }
}