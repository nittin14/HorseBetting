using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VKATalkBusinessLayer;
using System.Drawing;
using VKATalkClassLayer;
using VKATalk.Common;

namespace VKATalk.Master
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;

    using OfficeOpenXml;

    public partial class AddRaceTimings : System.Web.UI.Page
    {
        MasterBL Bl = new MasterBL();
        CustomValidator err = new CustomValidator();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDown(DrpdwnCenter, "Center", "CenterName", "ID");
                DrpdwnCenter.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnFromYear, "Year", "YearName", "YearID");
                drpdwnFromYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnTillYear, "Year", "YearName", "YearID");
                drpdwnTillYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnFromSeason, "Season", "SeasonName", "SeasonID");
                drpdwnFromSeason.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnTillSeason, "Season", "SeasonName", "SeasonID");
                drpdwnTillSeason.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                BindDropDown(drpdwnClass, "ProfessionalBunchGroup", "BunchGroup", "BunchGroupMID");
                drpdwnClass.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                
                BindDropDown(drpdwnDistance, "Distance", "Distance", "DistanceID");
                drpdwnDistance.Items.Insert(0, new ListItem("-- Please select --", "-1"));


                //BindDropDown(drpdwnHorseName, "RaceHorseName", "HorseName", "HorseNameID");
                //drpdwnHorseName.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                BindDropDown(drpdwnTrack, "MasterTrack", "TrackName", "MasterTrackID");
                drpdwnTrack.Items.Insert(0, new ListItem("-- Please select --", "-1"));

				BindDropDown(drpdwnRaceTimings, "MasterRaceTimingType", "RaceTimingType", "MRTID");
				drpdwnRaceTimings.Items.Insert(0, new ListItem("-- Please select --", "-1"));

				BindData();

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
        public static List<string> AddHorseList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseName", prefixText);
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
        public static List<string> TrackDetail(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("TrackInformation", prefixText);
            List<string> trackinformation = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                trackinformation.Add(dt.Rows[i][0].ToString());
            }
            return trackinformation;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            BtnSubmit.Text = "Add";
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

                    ((RadioButtonList)(x)).SelectedValue="-1";
                }
                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }

                rdbtnRaceStatus.ClearSelection();
                rdbtnFalseRails.ClearSelection();
                rdbtnRaceType.ClearSelection();
            }
        }

        /// <summary>
        /// Bind Dropdown at the time of page load
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="TableName_"></param>
        /// <param name="TextField"></param>
        /// <param name="ValueField"></param>
        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = Bl.GetDropdownBind(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }

        private void BindData()
        {
            try
            {
                DataTable dt = Bl.GetMasterData("RaceTiming");
                if (dt.Rows.Count > 0)
                {
                    grdviewRaceTiming.DataSource = dt;
                    grdviewRaceTiming.DataBind();
                }
                else
                {
                    grdviewRaceTiming.DataSource = new DataTable();
                    grdviewRaceTiming.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
            }
        }
        
        /// <summary>
        /// Submit Detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                VKATalkClassLayer.RaceTimings clsRaceTiming = new VKATalkClassLayer.RaceTimings();
                clsRaceTiming.RaceTimingType = drpdwnRaceTimings.SelectedItem.Text;
                clsRaceTiming.CenterID = Convert.ToInt32(DrpdwnCenter.SelectedItem.Value);
                clsRaceTiming.FromYearID = Convert.ToInt32(drpdwnFromYear.SelectedItem.Value);
                clsRaceTiming.TillYearID= Convert.ToInt32(drpdwnTillYear.SelectedItem.Value);
                clsRaceTiming.FromSeasonID = Convert.ToInt32(drpdwnFromSeason.SelectedItem.Value);
                clsRaceTiming.TillSeasonID = Convert.ToInt32(drpdwnTillSeason.SelectedItem.Value);
                clsRaceTiming.TrackID = Convert.ToInt32(drpdwnTrack.SelectedItem.Value);
                clsRaceTiming.DistanceID = Convert.ToInt32(drpdwnDistance.SelectedItem.Value);
                if (rdbtnRaceType.SelectedIndex == -1) { clsRaceTiming.RaceType = string.Empty; }
                else { clsRaceTiming.RaceType = rdbtnRaceType.SelectedItem.Text; }
                
                clsRaceTiming.ClassID = Convert.ToInt32(drpdwnClass.SelectedItem.Value);
                if (rdbtnRaceStatus.SelectedIndex == -1)
                {
                    clsRaceTiming.RaceStatus = "";
                }
                else
                {
                    clsRaceTiming.RaceStatus = rdbtnRaceStatus.SelectedItem.Text;
                }
                
                
                clsRaceTiming.RaceDate = txtbxRaceDate.Text;
                if (!hdnfieldhorseid.Value.Equals(""))
                {
                    clsRaceTiming.HorseNameID = Convert.ToInt32(hdnfieldhorseid.Value);
                }
                else
                {
                    clsRaceTiming.HorseNameID = 0;
                }
                clsRaceTiming.CarriedWeight = txtbxCarriedWeight.Text;
                clsRaceTiming.PenetrometerReading = txtbxPReading.Text;

                if (rdbtnFalseRails.SelectedIndex == -1)
                {
                    clsRaceTiming.FalseRails = "";
                }
                else
                {
                    clsRaceTiming.FalseRails = rdbtnFalseRails.SelectedItem.Text;
                }
                
                clsRaceTiming.Timing = txtbxMin.Text + ":" + txtbxSec.Text + ":" + txtbxPulse.Text; 
                int status;
                if (BtnSubmit.Text.Equals("Add"))
                {
                    status = Bl.InsertRaceTimings(clsRaceTiming, 0, 1, "Insert");
                }
                else
                {
                    status = Bl.InsertRaceTimings(clsRaceTiming, Convert.ToInt32(ViewState["GridViewRowID"]), 1, "Update");
                    
                }
                if (status == 1)
                {
                    string message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearAllSelection(this);
                    BindData();
                }
                else if (status == 2)
                {
                    var message = "Record updated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearAllSelection(this);
                    BindData();
                    BtnSubmit.Text = "Add";
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
                var message = "Issue in record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void GridView_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Text = "Update";
                GridViewRow row = grdviewRaceTiming.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = grdviewRaceTiming.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
                    drpdwnRaceTimings.Items.FindByText(hdnval.Value).Selected = true;
                    DrpdwnCenter.Items.FindByText(row.Cells[2].Text).Selected = true;
                    if (!row.Cells[3].Text.Equals("&nbsp;"))
                    {
                        drpdwnFromYear.Items.FindByText(row.Cells[3].Text).Selected = true;
                    }
                    if (!row.Cells[4].Text.Equals("&nbsp;"))
                    {
                        drpdwnTillYear.Items.FindByText(row.Cells[4].Text).Selected = true;
                    }
                    
                    if (!(row.Cells[5].Text.Equals("&nbsp;") || row.Cells[5].Text.Equals("-1")))
                    {
                        drpdwnFromSeason.Items.FindByText(row.Cells[5].Text).Selected = true;
                    }
                    if (!(row.Cells[6].Text.Equals("&nbsp;") || row.Cells[6].Text.Equals("-1")))
                    {
                        drpdwnTillSeason.Items.FindByText(row.Cells[6].Text).Selected = true;
                    }
                    if (!(row.Cells[7].Text.Equals("&nbsp;") || row.Cells[7].Text.Equals("-1")))
                    {
                        drpdwnTrack.Items.FindByText(row.Cells[7].Text).Selected = true;
                    }
                    //txtbxTrack.Text* = row.Cells[7].Text;
                    drpdwnDistance.Items.FindByText(row.Cells[8].Text).Selected = true;
                    

                    if ((row.Cells[9].Text.Equals("&nbsp;") || row.Cells[9].Text.Equals("")))
                    {
                        rdbtnRaceType.ClearSelection();
                    }
                    else
                    {
                        rdbtnRaceType.Items.FindByText(row.Cells[9].Text).Selected = true;
                    }

                    drpdwnClass.Items.FindByText(row.Cells[10].Text).Selected = true;

                    if (!(row.Cells[11].Text.Equals("&nbsp;") || row.Cells[11].Text.Equals("-1") || row.Cells[11].Text.Equals("")))
                    {
                        rdbtnRaceStatus.Items.FindByText(row.Cells[11].Text).Selected = true;
                    }
                    else
                    {
                        rdbtnRaceStatus.ClearSelection();
                    }
                    if (!(row.Cells[12].Text.Equals("&nbsp;") || row.Cells[12].Text.Equals("-1")))
                    {
                        txtbxRaceDate.Text = row.Cells[12].Text;
                    }
                    if (!(row.Cells[13].Text.Equals("&nbsp;") || row.Cells[13].Text.Equals("-1")))
                    {
                        //drpdwnHorseName.Items.FindByText(row.Cells[13].Text).Selected = true;
                        txtbxHorseName.Text = row.Cells[13].Text;
                    }
                    if (!(row.Cells[14].Text.Equals("&nbsp;") || row.Cells[14].Text.Equals("-1")))
                    {
                        txtbxCarriedWeight.Text = row.Cells[14].Text;
                    }
                    if (!(row.Cells[15].Text.Equals("&nbsp;") || row.Cells[15].Text.Equals("-1")))
                    {
                        txtbxPReading.Text = row.Cells[15].Text;
                    }
                    if (!(row.Cells[16].Text.Equals("&nbsp;") || row.Cells[16].Text.Equals("-1") || row.Cells[16].Text.Equals("")))
                    {
                        rdbtnFalseRails.Items.FindByText(row.Cells[16].Text).Selected = true;
                    }
                    else
                    {
                        rdbtnFalseRails.ClearSelection();
                    }

                    
                    var timings = row.Cells[17].Text;
                    string[] timingsplit = timings.Split(':');

                    txtbxMin.Text = timingsplit[0].ToString();
                    txtbxSec.Text = timingsplit[1].ToString();
                    txtbxPulse.Text = timingsplit[2].ToString();
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
                    VKATalkClassLayer.RaceTimings clsRaceTiming = new VKATalkClassLayer.RaceTimings();
                    clsRaceTiming.RaceTimingType = "";
                    clsRaceTiming.CenterID = 0;
                    clsRaceTiming.FromYearID = 0;
                    clsRaceTiming.TillYearID = 0;
                    clsRaceTiming.FromSeasonID = 0;
                    clsRaceTiming.TillSeasonID = 0;
                    clsRaceTiming.TrackID = 0;
                    clsRaceTiming.DistanceID = 0;
                    clsRaceTiming.RaceType = "";
                    clsRaceTiming.ClassID = 0;
                    clsRaceTiming.RaceStatus = "";
                    clsRaceTiming.RaceDate = "__-__-____";
                    clsRaceTiming.HorseNameID = 0;
                    clsRaceTiming.CarriedWeight = "";
                    clsRaceTiming.PenetrometerReading = "";
                    clsRaceTiming.FalseRails = "";
                    clsRaceTiming.Timing = "";

                    var status = Bl.InsertRaceTimings(clsRaceTiming,Convert.ToInt32(ViewState["GridViewRowID"]), 1, "Delete");
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
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Issue found in record.";
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
                //  DataTable dt = Bl.GetMasterData("Year");
                using (DataTable dt = Bl.GetMasterData("RaceTiming"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("RowCount");
                        dt.Columns.Remove("RaceTimingsID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("RaceTiming");

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
                            Response.AddHeader("content-disposition", "attachment;filename=RaceTiming.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        var message = "No Record found.";
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("RaceTiming"))
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
                    var dtErrorResult = new MasterBL().ImportExcel30(dt, "RaceTiming");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        BindData();
                        var message = "Issue in few record. Please check the XL sheet.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("RaceTiming");
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
                            Response.AddHeader("content-disposition", "attachment;filename=RaceTiming.xlsx");
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
                var message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
    }
}