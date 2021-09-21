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

    public partial class CardOdds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxHandicapEnterDate.Text = CommonMethods.CurrentDate();
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
            }
        }

        protected void txtbxRaceDate_OnTextChanged(object sender, EventArgs e)
        {
            //ClearSelection();
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

        protected void GvShowALL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }

        } 
        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                //GetRaceGeneralRaceDetail
               

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
            var dt = new CardsBL().GetAcceptanceDivisionDetail(
                    txtbxRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "CardsOdd");
            if (dt.Rows.Count > 0)
            {
                // dvgridview.Visible = true;
                //grdvwRaceDetail.Columns[0].Visible = true;
                lblSeason.Text = dt.Rows[0][15].ToString();
                lblYear.Text = dt.Rows[0][16].ToString();
                GvShowALL.DataSource = dt;
                GvShowALL.DataBind();
            }
            else
            {
                // dvgridview.Visible = false;
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
            }
        }

        protected void GvShowALL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GvShowALL.SelectedRow;
                var dataKey = GvShowALL.DataKeys[row.RowIndex];

                if (dataKey != null)
                {
                    ViewState["GlobalID"] = dataKey.Value;

                    TextBox txtbxNowG = (TextBox)row.FindControl("txtbxNowG");
                    TextBox txtbxMOWG = (TextBox)row.FindControl("txtbxMOWG");
                    TextBox txtbxLOOWG = (TextBox)row.FindControl("txtbxLOOWG");
                    TextBox txtbxOOWG = (TextBox)row.FindControl("txtbxOOWG");
                    TextBox txtbxOOPG = (TextBox)row.FindControl("txtbxOOPG");
                    TextBox txtbxMDWG = (TextBox)row.FindControl("txtbxMDWG");
                    TextBox txtbxMOPG = (TextBox)row.FindControl("txtbxMOPG");
                    TextBox txtbxCOWG = (TextBox)row.FindControl("txtbxCOWG");
                    TextBox txtbxCOPG = (TextBox)row.FindControl("txtbxCOPG");
                    double? now=null;
                    if (!txtbxNowG.Text.Equals(""))
                        now = Convert.ToDouble(txtbxNowG.Text);
                    double? mow = null;
                    if (!txtbxMOWG.Text.Equals(""))
                        mow = Convert.ToDouble(txtbxMOWG.Text);
                    double? loow = null;
                    if (!txtbxLOOWG.Text.Equals(""))
                        loow = Convert.ToDouble(txtbxLOOWG.Text);
                    double? oow = null;
                    if (!txtbxOOWG.Text.Equals(""))
                        oow = Convert.ToDouble(txtbxOOWG.Text); 
                    double? oop = null;
                    if (!txtbxOOPG.Text.Equals(""))
                        oop = Convert.ToDouble(txtbxOOPG.Text);
                    double? mdw = null;
                    if (!txtbxMDWG.Text.Equals(""))
                        mdw = Convert.ToDouble(txtbxMDWG.Text);
                    double? mop = null;
                    if (!txtbxMOPG.Text.Equals(""))
                        mop = Convert.ToDouble(txtbxMOPG.Text); 
                    double? cow = null;
                    if (!txtbxCOWG.Text.Equals(""))
                        cow = Convert.ToDouble(txtbxCOWG.Text);
                    double? cop = null;
                    if (!txtbxCOPG.Text.Equals(""))
                        cop = Convert.ToDouble(txtbxCOPG.Text);
                    

                    var result = new CardsBL().CardOdd(Convert.ToInt32(ViewState["GlobalID"])
                          , now
                          , mow
                          , loow
                          , oow
                          , oop
                          , mdw
                          , mop
                          , cow
                          , cop
                          , 1, "Update");
                    
                    if (result == 2)
                    {
                        var message = "Record updated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        var message = "Issue in Record.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var divisionraceid = 0;
            DataTable dtDeclaration = new DataTable("Declaration");
            try
            {
                dtDeclaration.Columns.Add("DivisionRaceID_Fk", typeof(int));
                dtDeclaration.Columns.Add("DayRaceNo", typeof(int));
                dtDeclaration.Columns.Add("HorseNo", typeof(int));
                dtDeclaration.Columns.Add("HorseID", typeof(int));
                dtDeclaration.Columns.Add("NOW", typeof(Double));
                dtDeclaration.Columns.Add("MOW", typeof(Double));
                dtDeclaration.Columns.Add("LOOW", typeof(Double));
                dtDeclaration.Columns.Add("OOW", typeof(Double));
                dtDeclaration.Columns.Add("OOP", typeof(Double));
                dtDeclaration.Columns.Add("MDW", typeof(Double));
                dtDeclaration.Columns.Add("MOP", typeof(Double));
                dtDeclaration.Columns.Add("COW", typeof(Double));
                dtDeclaration.Columns.Add("COP", typeof(Double));
                dtDeclaration.Columns.Add("CreatedDate", typeof(DateTime));
                dtDeclaration.Columns.Add("CreatedUserID", typeof(int));
                dtDeclaration.Columns.Add("IsActive", typeof(int));

                int rowcount = 0;
                foreach (GridViewRow row in GvShowALL.Rows)
                {
                    dtDeclaration.Rows.Add();
                    divisionraceid = Convert.ToInt32((row.FindControl("hdnfieldGlobalID") as HiddenField).Value);
                    dtDeclaration.Rows[rowcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGlobalID") as HiddenField).Value);
                    dtDeclaration.Rows[rowcount][1] = row.Cells[1].Text;
                    if (!row.Cells[2].Text.Equals("&nbsp;"))
                    {
                        dtDeclaration.Rows[rowcount][2] = row.Cells[2].Text;
                    }
                    else
                    {
                        dtDeclaration.Rows[rowcount][2] = 0;
                    }
                    if ((row.FindControl("hdnfielHorseID") as HiddenField).Value.Equals(""))
                    {
                        dtDeclaration.Rows[rowcount][3] = 0;
                    }
                    else
                    {
                        dtDeclaration.Rows[rowcount][3] = Convert.ToInt32((row.FindControl("hdnfielHorseID") as HiddenField).Value);
                    }
                    dtDeclaration.Rows[rowcount][4]=
                         string.IsNullOrWhiteSpace((row.FindControl("txtbxNowG") as TextBox).Text) ? DBNull.Value : (object)(row.FindControl("txtbxNowG") as TextBox).Text;
                    dtDeclaration.Rows[rowcount][5] =
                        string.IsNullOrWhiteSpace((row.FindControl("txtbxMOWG") as TextBox).Text) ? DBNull.Value : (object)(row.FindControl("txtbxMOWG") as TextBox).Text;
                    dtDeclaration.Rows[rowcount][6] =
                        string.IsNullOrWhiteSpace((row.FindControl("txtbxLOOWG") as TextBox).Text) ? DBNull.Value : (object)(row.FindControl("txtbxLOOWG") as TextBox).Text;
                    dtDeclaration.Rows[rowcount][7] =
                         string.IsNullOrWhiteSpace((row.FindControl("txtbxOOWG") as TextBox).Text) ? DBNull.Value : (object)(row.FindControl("txtbxOOWG") as TextBox).Text;
                    dtDeclaration.Rows[rowcount][8] =
                        string.IsNullOrWhiteSpace((row.FindControl("txtbxOOPG") as TextBox).Text) ? DBNull.Value : (object)(row.FindControl("txtbxOOPG") as TextBox).Text;
                    dtDeclaration.Rows[rowcount][9] =
                       string.IsNullOrWhiteSpace((row.FindControl("txtbxMDWG") as TextBox).Text) ? DBNull.Value : (object)(row.FindControl("txtbxMDWG") as TextBox).Text;
                    dtDeclaration.Rows[rowcount][10] =
                       string.IsNullOrWhiteSpace((row.FindControl("txtbxMOPG") as TextBox).Text) ? DBNull.Value : (object)(row.FindControl("txtbxMOPG") as TextBox).Text;
                    dtDeclaration.Rows[rowcount][11] =
                       string.IsNullOrWhiteSpace((row.FindControl("txtbxCOWG") as TextBox).Text) ? DBNull.Value : (object)(row.FindControl("txtbxCOWG") as TextBox).Text;
                    dtDeclaration.Rows[rowcount][12] =
                       string.IsNullOrWhiteSpace((row.FindControl("txtbxCOPG") as TextBox).Text) ? DBNull.Value : (object)(row.FindControl("txtbxCOPG") as TextBox).Text;
                    dtDeclaration.Rows[rowcount][13] = DateTime.Now;
                    dtDeclaration.Rows[rowcount][14] = 1;
                    dtDeclaration.Rows[rowcount][15] = 1;
                    rowcount++;
                }

                var result = new CardsBL().AddCardOdds(dtDeclaration, divisionraceid);
                if (result == 1)
                {
                    AcceptanceShow();
                    var message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    var message = "Issue in Record.";
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtbxHandicapEnterDate.Text = string.Empty;
            txtbxRaceDate.Text = string.Empty;
            drpdwnCenterName.ClearSelection();
            lblSeason.Text = string.Empty;
            lblYear.Text = string.Empty;
            GvShowALL.DataSource = new DataTable();
            GvShowALL.DataBind();
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_RaceCard"))
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
                    var dtErrorResult = new CardsBL().Import30(dt, "Card_RaceCard", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_RaceCard");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_RaceCard.xlsx");
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
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "Card_RaceCard"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_RaceCard");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_RaceCard.xlsx");
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


        protected void btnClose_Click(object sender, EventArgs e)
        {
            //ClearAll();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }
    }
}