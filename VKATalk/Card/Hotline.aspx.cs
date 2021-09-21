using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using VKATalkBusinessLayer;

namespace VKATalk.Card
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;
    using System.Web.WebSockets;

    using OfficeOpenXml;
    using VKATalk.Common;

    public partial class Hotline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxEntryEnterDate.Text = CommonMethods.CurrentDate();

            }
        }

        public void ClearSelection()
        {
            drpdwnCenterName.ClearSelection();
            drpdwnHorseNo.ClearSelection();
            drpdwndayraceno.ClearSelection();
            lblSeason.Text = "";
            lblYear.Text = "";
            grdvwHorseDetail.DataSource = new DataTable();
            grdvwHorseDetail.DataBind();
            txtbxHotliner.Text = string.Empty;
            hdnfieldhotlinerid.Value = string.Empty;
            hdnfieldHorseID.Value = string.Empty;
            txtbxHorseName.Text = string.Empty;
            txtbxHotline.Text = string.Empty;
        }

      
        protected void txtbxRaceDate_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                //txtbxRaceDate_OnTextChanged
                
                ClearSelection();
                var dt = new CardsBL().GetRaceCenterName(txtbxRaceDate.Text);
                if (dt.Rows.Count > 0)
                {
                    drpdwnCenterName.DataSource = dt;
                    drpdwnCenterName.DataTextField = "CenterName";
                    drpdwnCenterName.DataValueField = "ID";
                    drpdwnCenterName.DataBind();
                    drpdwnCenterName.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                    drpdwnCenterName.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        //protected void grdvwRaceDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow row = grdvwRaceDetail.SelectedRow;
        //        DropDownList drpdwnEntry = (DropDownList)row.FindControl("drpdwnEntryType");
        //        HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
        //        var dataKey = grdvwRaceDetail.DataKeys[row.RowIndex];
        //        if (dataKey != null)
        //        {
        //            tblHorseEntryForm.Visible = true;
        //            ViewState["GridViewRowID"] = dataKey.Value; //generalraceid
        //            ViewState["GridViewRaceName"] = hdnval.Value;//reneralracename
        //            lblRaceNameShow.Text = hdnval.Value;
        //            ViewState["SerialNumber"] = row.Cells[0].Text;
        //            hdnfieldGeneralRaceNameID.Value = Convert.ToString(dataKey.Value);
        //            //Session["GeneralRaceName"] = hdnfieldGeneralRaceNameID.Value 
        //            ViewState["EntryType"] = drpdwnEntry.SelectedItem.Value;
        //        }
        //        var dsGeneralDate = new CardsBL().GetEntryDateInformation(Convert.ToInt32(dataKey.Value),"Entry");
        //        if (dsGeneralDate.Tables[0].Rows.Count > 0)
        //        {
        //            if (dsGeneralDate.Tables[0].Rows.Count > 0)
        //            {
        //                lblEntryDate.Text = dsGeneralDate.Tables[0].Rows[0][0].ToString();
        //            }
        //            if (dsGeneralDate.Tables[1].Rows.Count > 0)
        //            {
        //                lbl1stSupplimentry.Text = dsGeneralDate.Tables[1].Rows[0][0].ToString();
        //            }
        //            if (dsGeneralDate.Tables[2].Rows.Count > 0)
        //            {
        //                lbl2ndSupplimentry.Text = dsGeneralDate.Tables[2].Rows[0][0].ToString();
        //            }
        //            if (dsGeneralDate.Tables[3].Rows.Count > 0)
        //            {
        //                lblFinalEntry.Text = dsGeneralDate.Tables[3].Rows[0][0].ToString();
        //            }
        //        }

        //        var dt = new CardsBL().HorseInformation(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0, 0, 0, 0, 0, 1, "Select", 0, 1,txtbxEntryEnterDate.Text);
        //        if (dt.Rows.Count > 0)
        //        {
        //            grdvwHorseDetail.DataSource = dt;
        //            grdvwHorseDetail.DataBind();
        //            var localrownumber = 0;
        //            for (int count = 0; count < dt.Rows.Count; count++)
        //            {
        //                localrownumber = Convert.ToInt32(dt.Rows[count][1]);
        //            }
        //            lblRowNumber.Text = Convert.ToString(localrownumber + 1);
        //        }
        //        else
        //        {
        //            grdvwHorseDetail.DataSource = new DataTable();
        //            grdvwHorseDetail.DataBind();
        //            lblRowNumber.Text = Convert.ToString(rownumber);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandling.SendErrorToText(ex);
        //        string message = "Issue in Record.";
        //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        //    }

        //}

        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddHotlinerList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("AddHotlinerList", prefixText);

            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //  horseList.Add(dt.Rows[i][1].ToString());
                horseList.Add(
                    AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
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
        public static List<string> AddHorseList(string prefixText, int count, string contextKey)
        {
            DataTable dt = new CardsBL().GetHorseNameAutoFillerHotliner("CardHorseList", prefixText, contextKey);

            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //  horseList.Add(dt.Rows[i][1].ToString());
                horseList.Add(
                    AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
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
        public static List<string> AddHotlineList(string prefixText, int count)
        {
            //DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("AddHotlineList", prefixText);
            DataTable dt = new CardsBL().GetCardAutoFiller("AddHotlinerList", prefixText);

            List<string> commentList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                commentList.Add(dt.Rows[i][0].ToString());
            }
            return commentList;
        }

        protected void dvgrdviewHorseDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = grdvwHorseDetail.SelectedRow;
                var dataKey = grdvwHorseDetail.DataKeys[row.RowIndex];
                //var rownumber = string.Empty;
                if (dataKey != null)
                {
                    HiddenField hdnfieldHotlinerID = (HiddenField)row.FindControl("hdnfieldHotlinerID");
                    hdnfieldhotlinerid.Value = hdnfieldHotlinerID.Value;
                    ViewState["HotlineCID"] = dataKey.Value;
                    HiddenField hdnfieldPRofessionalName = (HiddenField)row.FindControl("hdnfieldPRofessionalName");
                    txtbxHotliner.Text = hdnfieldPRofessionalName.Value;

                    HiddenField hdnfieldHorseID = (HiddenField)row.FindControl("hdnfieldHorseID");
                    hdnfieldHorseID.Value = hdnfieldHorseID.Value;
                    txtbxHorseName.Text = row.Cells[4].Text;
                    txtbxHotline.Text = row.Cells[5].Text;

                    btnAdd.Text = "Update";
                }

               
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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

        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                AcceptanceShow();
                
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        private void AcceptanceShow()
        {
            DataSet ds = new CardsBL().GetAcceptanceDivisionDetailMultipleReturn(
                    txtbxRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "CardHotLine");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblSeason.Text = ds.Tables[0].Rows[0][2].ToString();
                lblYear.Text = ds.Tables[0].Rows[0][3].ToString();
                drpdwndayraceno.DataSource = ds.Tables[0];
                drpdwndayraceno.DataTextField = "DRNo";
                drpdwndayraceno.DataValueField = "DRNo";
                drpdwndayraceno.DataBind();

                drpdwnHorseNo.DataSource = ds.Tables[0];
                drpdwnHorseNo.DataTextField = "HorseNo";
                drpdwnHorseNo.DataValueField = "HorseNo";
                drpdwnHorseNo.DataBind();

                drpdwndayraceno.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                drpdwnHorseNo.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                tblHorseEntryForm.Visible = true;
            }
            else
            {
                tblHorseEntryForm.Visible = false;
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                grdvwHorseDetail.DataSource = ds.Tables[1];
                grdvwHorseDetail.DataBind();
            }
            else{
                grdvwHorseDetail.DataSource = new DataTable();
                grdvwHorseDetail.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {



                DataTable dt = null;
                //int entrytype = 0;
                //foreach (GridViewRow row in grdvwRaceDetail.Rows)
                //{
                //    if (row.RowType == DataControlRowType.DataRow)
                //    {
                //        int id = Convert.ToInt32(grdvwRaceDetail.DataKeys[row.RowIndex]["GeneralRaceNameID"]);
                //        int raceid = Convert.ToInt32(hdnfieldGeneralRaceNameID.Value);
                //        if (raceid == id)
                //        {
                //            DropDownList drpdwnentrytype = (row.Cells[2].FindControl("drpdwnEntryType") as DropDownList);
                //            entrytype = Convert.ToInt32(drpdwnentrytype.SelectedItem.Value);
                //        }
                //    }
                //}
                if (btnAdd.Text.Equals("Add"))
                {
                    dt = new CardsBL().HotLine(0, txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldhotlinerid.Value), 
                        Convert.ToInt32(hdnfieldHorseID.Value),txtbxHotline.Text, 1, "Insert");
                }
                else
                {
                    dt = new CardsBL().HotLine(Convert.ToInt32(ViewState["HotlineCID"]), txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldhotlinerid.Value), 
                        Convert.ToInt32(hdnfieldHorseID.Value),txtbxHotline.Text, 1, "Update");
                    btnAdd.Text = "Add";
                }
                if (dt.Rows.Count > 0)
                {
                    grdvwHorseDetail.DataSource = dt;
                    grdvwHorseDetail.DataBind();
                }
                else
                {
                    grdvwHorseDetail.DataSource = new DataTable();
                    grdvwHorseDetail.DataBind();
                }
                ClearSelection();
             }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

       

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClearSelection();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }

       

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_Hotline"))
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
                    var dtErrorResult = new CardsBL().Import30(dt, "Card_Hotline", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Hotline");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Hotline.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        //BindData();
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


        /// <summary>
        /// Export Gridview Data in Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataSet ds = new CardsBL().GetExport(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "Card_Hotline"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Hotline");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Hotline.xlsx");
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