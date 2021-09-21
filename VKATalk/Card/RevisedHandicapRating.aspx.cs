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

    public partial class RevisedHandicapRating : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxDeclarationEnterDate.Text = CommonMethods.CurrentDate();
            }
        }

        protected void txtbxDivisionRaceDate_OnTextChanged(object sender, EventArgs e)
        {
            var dt = new CardsBL().GetRaceCenterName(txtbxDivisionRaceDate.Text);
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

        protected void drpdwnBasecenter_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                var dt = new CardsBL().GetRaceGeneralRaceDetailAcceptance(
                    txtbxDivisionRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
                if (dt.Rows.Count > 0)
                {
                    lblEntryDate.Text = string.Empty;
                    lblSeason.Text = dt.Rows[0][7].ToString();
                    lblYear.Text = dt.Rows[0][8].ToString();
                }
                ShowRevisedHandicapRating();
                GetRevisedRating();
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                var dt = new CardsBL().GetRaceGeneralRaceDetailAcceptance(
                    txtbxDivisionRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
                if (dt.Rows.Count > 0)
                {
                    lblEntryDate.Text = string.Empty;
                    lblSeason.Text = dt.Rows[0][7].ToString();
                    lblYear.Text = dt.Rows[0][8].ToString();
                }
                ShowRevisedHandicapRating();
                GetRevisedRating();
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        private void ShowRevisedHandicapRating()
        {
            var dt = new CardsBL().GetAcceptanceDivisionDetail(
                    txtbxDivisionRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "RevisedHandicapRating");
            if (dt.Rows.Count > 0)
            {
                GvShowALL.DataSource = dt;
                GvShowALL.DataBind();
            }
            else
            {
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
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

        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dtDeclaration = new DataTable("Declaration");
            try
            {
                var duplicatecheck = 0;
                dtDeclaration.Columns.Add("DataEntryDate", typeof(string));
                dtDeclaration.Columns.Add("DivisionRaceDate", typeof(string));
                dtDeclaration.Columns.Add("CenterMID", typeof(int));
                dtDeclaration.Columns.Add("GeneralRaceID", typeof(int));
                dtDeclaration.Columns.Add("GeneralRaceNameID", typeof(int));
                dtDeclaration.Columns.Add("DivisionRaceID", typeof(int));//5
                dtDeclaration.Columns.Add("HorseNo", typeof(int));
                dtDeclaration.Columns.Add("HorseID", typeof(int));
                dtDeclaration.Columns.Add("HorseNameID", typeof(int));
                dtDeclaration.Columns.Add("RevisedHandicapRating", typeof(string));//9
                dtDeclaration.Columns.Add("CreatedDate", typeof(DateTime));
                dtDeclaration.Columns.Add("CreatedUserID", typeof(int));
                dtDeclaration.Columns.Add("IsActive", typeof(int));
                dtDeclaration.Columns.Add("RevisedMyHandicapRating", typeof(string));//13
                var result = 0;
                int rowcount = 0;
                foreach (GridViewRow row in GvShowALL.Rows)
                {

                    duplicatecheck = new CardsBL().CheckDuplicateRecordRevisedRating(Convert.ToInt32((row.FindControl("hdnfieldDivisionRaceID") as HiddenField).Value),
                        Convert.ToInt32((row.FindControl("hdnfielHorseID") as HiddenField).Value));
                    if (duplicatecheck==1)
                    {
                        var handicapratingvalue = (row.FindControl("txtbxRevisedHandicapRating") as TextBox).Text;

                        if (!handicapratingvalue.Equals(""))
                        {
                            dtDeclaration.Rows.Add();
                            if (txtbxDeclarationEnterDate.Text.Equals("__-__-____"))
                            {
                                dtDeclaration.Rows[rowcount][0] = DBNull.Value;
                            }
                            else
                            {
                                string[] dateString = txtbxDeclarationEnterDate.Text.Split('-');
                                DateTime enterDate =
                                    Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                                dtDeclaration.Rows[rowcount][0] = enterDate.ToString("yyyy-MM-dd 00:00:00");
                            }

                            if (txtbxDivisionRaceDate.Text.Equals("__-__-____"))
                            {
                                dtDeclaration.Rows[rowcount][1] = DBNull.Value;
                            }
                            else
                            {
                                string[] dateString = txtbxDivisionRaceDate.Text.Split('-');
                                DateTime enterDate =
                                    Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                                dtDeclaration.Rows[rowcount][1] = enterDate.ToString("yyyy-MM-dd 00:00:00");
                            }

                            dtDeclaration.Rows[rowcount][2] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtDeclaration.Rows[rowcount][3] = Convert.ToInt32((row.FindControl("hdnfielGeneralRaceID") as HiddenField).Value);
                            dtDeclaration.Rows[rowcount][4] = Convert.ToInt32((row.FindControl("hdnfielGeneralRaceNameID") as HiddenField).Value);
                            dtDeclaration.Rows[rowcount][5] = Convert.ToInt32((row.FindControl("hdnfieldDivisionRaceID") as HiddenField).Value);
                            if (!row.Cells[3].Text.Equals(""))
                            {
                                dtDeclaration.Rows[rowcount][6] = Convert.ToInt32(row.Cells[3].Text);
                            }
                            else
                            {
                                dtDeclaration.Rows[rowcount][6] = null;
                            }
                            dtDeclaration.Rows[rowcount][7] = Convert.ToInt32((row.FindControl("hdnfielHorseID") as HiddenField).Value);
                            dtDeclaration.Rows[rowcount][8] = Convert.ToInt32((row.FindControl("hdnfieldHorseNameID") as HiddenField).Value);
                            dtDeclaration.Rows[rowcount][9] = (row.FindControl("txtbxRevisedHandicapRating") as TextBox).Text.Equals("") ? string.Empty : (row.FindControl("txtbxRevisedHandicapRating") as TextBox).Text;
                            dtDeclaration.Rows[rowcount][10] = DateTime.Now;
                            dtDeclaration.Rows[rowcount][11] = 1;
                            dtDeclaration.Rows[rowcount][12] = 1;
                            //dtDeclaration.Rows[rowcount][13] = (row.FindControl("txtbxRevisedMyHandicapRating") as TextBox).Text.Equals("") ? string.Empty : (row.FindControl("txtbxRevisedMyHandicapRating") as TextBox).Text;
                            dtDeclaration.Rows[rowcount][13] = string.Empty;
                            result = new CardsBL().AddCardRevisedHandicapRating(dtDeclaration);
                            dtDeclaration.Clear();
                            // rowcount++;
                        }
                    }
                    
                }

                if (duplicatecheck == 2)
                {
                    var message = "Record already exist.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    GetRevisedRating();
                    ShowRevisedHandicapRating();
                    var message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                    

                //if (result == 1)
                //{
                //    GetRevisedRating();
                //    ShowRevisedHandicapRating();
                //    var message = "Record added successfully.";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //}
                //else
                //{
                //    var message = "Issue in Record.";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //}
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        //protected void GvShowALL_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try{

        //        GridViewRow row = GvShowALL.SelectedRow;
        //        var dataKey = GvShowALL.DataKeys[row.RowIndex];
        //        int status = 0;

        //        if (dataKey != null)
        //        {
                    
        //            TextBox txtbxRevisedHandicapRatingG = (TextBox)row.FindControl("txtbxRevisedHandicapRating");
        //            TextBox txtbxRevisedMyHandicapRatingG = (TextBox)row.FindControl("txtbxRevisedMyHandicapRating");
        //            status = new CardsBL().RaceCardUpdate(Convert.ToInt32(dataKey.Value), 0, "", 0, "RevisedHandicapRating", 
        //                        (txtbxRevisedHandicapRatingG.Text.Equals(""))?string.Empty: txtbxRevisedHandicapRatingG.Text, 0,0, 
        //                            (txtbxRevisedMyHandicapRatingG.Text.Equals(""))? string.Empty: txtbxRevisedMyHandicapRatingG.Text);
        //        }

        //        if (status == 1)
        //        {
        //            GetRevisedRating();
        //            string message = "Record updated successfully.";
        //            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        //        }
        //        else
        //        {
        //            string message = "Issue in Record.";
        //            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandling.SendErrorToText(ex);
        //        string message = "Issue in Record.";
        //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        //    }
        //}


        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_RevisedHandicapRating"))
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
                    var dtErrorResult = new CardsBL().Import30(dt, "Card_RevisedHandicapRating", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_RevisedHandicapRating");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_RevisedHandicapRating.xlsx");
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
                using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "Card_RevisedHandicapRating"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_RevisedHandicapRating");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_RevisedHandicapRating.xlsx");
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

        protected void btnHandicapShow_Click(object sender, EventArgs e)
        {
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }

        

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {

                GetRevisedRating();
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        private void GetRevisedRating()
        {
            var dt = new CardsBL().GetRevisedRatingData(
                    txtbxDivisionRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "RevisedHandicapRating");
            if (dt.Rows.Count > 0)
            {
                GvRevisedRating.DataSource = dt;
                GvRevisedRating.DataBind();
            }
            else
            {
                GvRevisedRating.DataSource = new DataTable();
                GvRevisedRating.DataBind();
            }
        }


        protected void RowEdit(object sender, GridViewEditEventArgs e)
        {
            GvRevisedRating.EditIndex = e.NewEditIndex;
            GetRevisedRating();
        }

        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GvRevisedRating.EditIndex = -1;
            GetRevisedRating();
        }

        protected void RowUpdate(object sender, GridViewUpdateEventArgs e)
        {

            try
            {
                int id = (int)GvRevisedRating.DataKeys[e.RowIndex].Value;

                string handicaprating = ((TextBox)GvRevisedRating.Rows[e.RowIndex].FindControl("txtbxRevisedHandicapRatingG")).Text;
                //string handicapmyrating = ((TextBox)GvRevisedRating.Rows[e.RowIndex].FindControl("txtbxRevisedMyHandicapRatingG")).Text;



                int result = new CardsBL().RevisedRatingCardUpdate(id, handicaprating, string.Empty,"Update");
                if (result == 1)
                {
                    GvRevisedRating.EditIndex = -1;
                    GetRevisedRating();
                    var message = "Record Updated Successfully.";
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
                var message = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }


        }


        protected void GvRevisedRating_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           

        }

        protected void GvRevisedRating_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try {
                int id = (int)GvRevisedRating.DataKeys[e.RowIndex].Value;

                int result = new CardsBL().RevisedRatingCardUpdate(id, string.Empty, string.Empty, "Delete");
                if (result == 1)
                {
                    GvRevisedRating.EditIndex = -1;
                    GetRevisedRating();
                    var message = "Record Deleted Successfully.";
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
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        
    }
}