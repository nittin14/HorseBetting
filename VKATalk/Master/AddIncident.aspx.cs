using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VKATalkBusinessLayer;

namespace VKATalk.Master
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;

    using OfficeOpenXml;

    public partial class AddIncident : System.Web.UI.Page
    {
        MasterBL Bl = new MasterBL();
        CustomValidator err = new CustomValidator();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        //BindIncidentImpactData();
                        //DeSelectAllCheckBoxes();
                        BindData();
                    }
            }
            catch (Exception ex)
            {
                var message = "Issue in record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
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
        public static List<string> IncidentImpactList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("IncidentImpactList", prefixText);
            List<string> ImpactList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ImpactList.Add(dt.Rows[i][0].ToString());
            }
            return ImpactList;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            int checkcondition = 0, status = 0;
            try
            {
                
               // string impact = GetSelectedItems();
                if (BtnSubmit.Text.Equals("Add"))
                {
                    checkcondition = 1;
                    status = Bl.InsertMasterPagesDataIncident("Insert", txtbxIncidentName.Text, txtbxAlias.Text, txtbxImpact.Text, "", 1,0);
                    
                }
                else
                {
                    checkcondition = 2;
                    status = Bl.InsertMasterPagesDataIncident("Update", txtbxIncidentName.Text, txtbxAlias.Text, txtbxImpact.Text, "", 1, (int)ViewState["GridViewRowID"]);
                    //BtnSubmit.Text = "Add";
                    
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


        private void BindData()
        {
                DataTable dt;
                dt = Bl.GetMasterData("Incident");
                if (dt.Rows.Count > 0)
                {
                    Gv_Incident.DataSource = dt;
                    Gv_Incident.DataBind();
                }
                else
                {
                    Gv_Incident.DataSource = new DataTable();
                    Gv_Incident.DataBind();
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
                if ((x.GetType() == typeof(CheckBoxList)))
                {

                    ((CheckBoxList)(x)).ClearSelection();
                }
                if ((x.GetType() == typeof(RadioButtonList)))
                {

                    ((RadioButtonList)(x)).SelectedIndex = 0;
                }
                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }
        }

        protected void Gv_Incident_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Text = "Update";
                GridViewRow row = Gv_Incident.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = Gv_Incident.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
                    txtbxIncidentName.Text = hdnval.Value;
                    txtbxAlias.Text = row.Cells[2].Text;
                    if (!row.Cells[3].Text.Equals("&nbsp;"))
                    {
                        txtbxImpact.Text = row.Cells[3].Text;
                    }
                    
                    //GetSelectedItems();
                    //SetSelectedItems(row.Cells[4].Text);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ViewState["GridViewRowID"].Equals(""))
                {
            //        var status = Bl.UpdateMasterPagesData("Hotliner", (int)ViewState["GridViewRowID"], "", "", 1, "Delete");
                    var status = Bl.InsertMasterPagesDataIncident("Delete", "", "", "", "", 1, (int)ViewState["GridViewRowID"]);
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            BtnSubmit.Text = "Add";
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
                //  DataTable dt = Bl.GetMasterData("Year");
                using (DataTable dt = Bl.GetMasterData("Incident"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("RowCount");
                        dt.Columns.Remove("IncidentID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Incident");

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

                            //  }
                            Response.AddHeader("content-disposition", "attachment;filename=Incident.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string message = "Issue in Record.";
                ErrorHandling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Incident"))
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
                string message = "Issue in Record.";
                ErrorHandling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                if (dt.Rows.Count > 0)
                {
                    var dtErrorResult = new MasterBL().ImportExcel30(dt, "Incident");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        BindData();
                        var message = "Issue in few record. Please check the XL sheet.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Incident");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Incident.xlsx");
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
            }
            catch (Exception ex)
            {
                string message = "Issue in Record.";
                ErrorHandling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
    }
}