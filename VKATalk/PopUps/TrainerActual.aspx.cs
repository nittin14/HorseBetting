﻿using System;
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

    public partial class TrainerActual : System.Web.UI.Page
    {
        int _horseId = 0;
        int _userId = 1;
        private int _value = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
                BindHorseName();

                if (!IsPostBack)
                {
                    var dob = Request.QueryString["HorseDOB"].Substring(0, 10);
                    if (dob.Contains("%2F"))
                    {
                        txtbxFromDate.Text = Convert.ToString(dob.Replace("/", "%2F"));
                    }
                    else
                    {
                        txtbxFromDate.Text = Convert.ToString(dob.Replace("/", "-"));
                    }
                    BindData();
                }
                //if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
                //if (!Request.QueryString["HorseNameID"].Equals(""))
                //{
                //    _value = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                //    horseId.Value = Request.QueryString["HorseNameID"];
                //    _horseId = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                //}
                //if (!IsPostBack)
                //{
                //    if (_horseId != 0)
                //    {
                //        var reqQueryValue = string.Empty;
                //        if (!Request.QueryString["HorseName"].Equals(""))
                //        {
                //            reqQueryValue = Request.QueryString["HorseName"];
                //        }
                //        string[] horseName = reqQueryValue.Split(',');
                //        lblHorseNameFirst.Text = horseName[1];
                //        lblHorseNameSecond.Text = horseName[2];
                //        BindData();
                //    }
                //}
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        public void BindHorseName()
        {
            if (!Request.QueryString["HorseNameID"].Equals(""))
            {
                _value = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                horseId.Value = Request.QueryString["HorseNameID"];
                _horseId = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                if (_horseId != 0)
                {
                    var reqQueryValue = string.Empty;
                    if (!Request.QueryString["HorseName"].Equals(""))
                    {
                        reqQueryValue = Request.QueryString["HorseName"];
                    }
                    string[] horseName = reqQueryValue.Split(',');
                    lblHorseNameFirst.Text = horseName[1];
                    lblHorseNameSecond.Text = horseName[2];
                }
            }
        }

        /// <summary>
        /// AddStudOwnerList
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddProfessionalNameList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("ProfessionalName", prefixText);
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
        /// bind drop down 
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="TableName_"></param>
        /// <param name="TextField"></param>
        /// <param name="ValueField"></param>
        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = new MasterHorseBL().GetHorseName(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
        }

        /// <summary>
        /// This is use for bind the gridview
        /// </summary>
        public void BindData()
        {
                MasterHorseBL Bl = new MasterHorseBL();
                DataTable dt = Bl.GetHorseDetail(Convert.ToString(_horseId), "TrainerActual");
                if (dt.Rows.Count > 0)
                {
                    this.GvGlobal.DataSource = dt;
                    this.GvGlobal.DataBind();
                }
                else
                {
                    this.GvGlobal.DataSource = new DataTable();
                    this.GvGlobal.DataBind();
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
        public static List<string> AddCommentsList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("TrainerActualComment", prefixText);
            List<string> currentMissionComments = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currentMissionComments.Add(dt.Rows[i][0].ToString());
            }
            return currentMissionComments;
        }

        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddReasonofChange(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("TrainerActualReasonofChange", prefixText);
            List<string> currentMissionComments = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currentMissionComments.Add(dt.Rows[i][0].ToString());
            }
            return currentMissionComments;
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
                var conditioncheck = 0;
				if (!(txtbxTillDate.Text.Equals("__-__-____") || txtbxReasonofChange.Text.Equals("") || Convert.ToInt32(drpdwnStatus.SelectedItem.Value) == -1))
				{
					conditioncheck = 1;

				}
				else if ((txtbxTillDate.Text.Equals("__-__-____") && txtbxReasonofChange.Text.Equals("") && Convert.ToInt32(drpdwnStatus.SelectedItem.Value) == -1))
				{
					conditioncheck = 1;

				}
				if (conditioncheck == 0)
                {
                    string message = "Please Enter Change Status & Reason Of Change.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        ds = new MasterHorseBL().GetHorseTillDateValidation(_horseId, "HorseTrainerActual", txtbxFromDate.Text, btnSave.Text);
                    }
                    else
                    {
                        ds = new MasterHorseBL().GetHorseTillDateValidation((int)ViewState["horseStatusID"], "HorseTrainerActual", txtbxFromDate.Text, btnSave.Text);
                    }
                    Int64 checkTillDate = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                    if (_horseId == 0)
                    {
                        string message = "Please select Horse name.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else if (checkTillDate == 2)
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Check the existing record TillDate.');", true);
                        string message = "Please Check the existing record TillDate.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
						var drbtnstatus = string.Empty;
						if (!(Convert.ToInt32(drpdwnStatus.SelectedItem.Value) == -1))
							drbtnstatus = drpdwnStatus.SelectedItem.Text;

						if (btnSave.Text.Equals("Add"))
                        {
                            status = new MasterHorseBL().HorseTrainerActual(
                                _horseId,
                                Convert.ToInt32(hdnprofessionalid.Value),
                                txtbxFromDate.Text,
                                txtbxTillDate.Text,
								drbtnstatus,
                                txtbxReasonofChange.Text,
                                txtbxComment.Text,
                                1,
                                "Insert");
                        }
                        else if (btnSave.Text.Equals("Update"))
                        {
                            status = new MasterHorseBL().HorseTrainerActual(
                                (int)ViewState["horseStatusID"],
                                Convert.ToInt32(hdnprofessionalid.Value),
                                txtbxFromDate.Text,
                                txtbxTillDate.Text,
								drbtnstatus,
                                txtbxReasonofChange.Text,
                                txtbxComment.Text,
                                1,
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
        /// Close the window and pass the value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (horseId.Value != "")
                {
                    Session["HorseID"] = horseId.Value;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
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
                if (!ViewState["horseStatusID"].Equals(""))
                {
                    new MasterHorseBL().HorseTrainerActual((int)ViewState["horseStatusID"], 0, txtbxFromDate.Text, txtbxTillDate.Text,
                     "","",txtbxComment.Text, 1, "Delete");
                    ClearAllSelection(this);
                    BindData();
                    btnSave.Text = "Add";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Record Deleted Successfully.');", true); 
                    ViewState["horseStatusID"] = "";
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('No Record Found.');", true); 
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
            }

        }

        protected void GvGlobal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = this.GvGlobal.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvGlobal.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["horseStatusID"] = dataKey.Value;
                    //this.drpdwnGender.Items.FindByText(hdnval.Value).Selected = true;
                    HiddenField hdnval1 = (HiddenField)row.FindControl("hdnfielGridviewProfessionalNameID");
                    hdnprofessionalid.Value = hdnval1.Value;
                    txtbxProfessionalName.Text = hdnval.Value;
                    txtbxFromDate.Text = row.Cells[1].Text;
					if (!row.Cells[2].Text.Contains("&nbsp;"))
						txtbxTillDate.Text = row.Cells[2].Text;
					if (!row.Cells[3].Text.Contains("&nbsp;"))
						this.drpdwnStatus.Items.FindByText(row.Cells[3].Text).Selected = true;
					if (row.Cells[4].Text.Contains("&quot;"))
                    {
                        txtbxReasonofChange.Text = row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&quot;") ? row.Cells[4].Text.Replace("&quot;", "\"") : row.Cells[4].Text;
                    }
                    else
                    {
                        txtbxReasonofChange.Text = row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&#39;") ? row.Cells[4].Text.Replace("&#39;", "'") : row.Cells[4].Text;
                    }

                    if (row.Cells[5].Text.Contains("&quot;"))
                    {
                        txtbxComment.Text = row.Cells[5].Text.Equals("&nbsp;") ? "" : row.Cells[5].Text.Contains("&quot;") ? row.Cells[5].Text.Replace("&quot;", "\"") : row.Cells[5].Text;
                    }
                    else
                    {
                        txtbxComment.Text = row.Cells[5].Text.Equals("&nbsp;") ? "" : row.Cells[5].Text.Contains("&#39;") ? row.Cells[5].Text.Replace("&#39;", "'") : row.Cells[5].Text;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
            }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            btnSave.Text = "Add";
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

            var dob = Request.QueryString["HorseDOB"].Substring(0, 10);
            if (dob.Contains("%2F"))
            {
                txtbxFromDate.Text = Convert.ToString(dob.Replace("/", "%2F"));
            }
            else
            {
                txtbxFromDate.Text = Convert.ToString(dob.Replace("/", "-"));
            }
            //  rdbtnStatus.ClearSelection();
            //rdbtnStatus.Items.FindByText("Not Specified").Selected = true;
        }


        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Horse_TrainerHidden"))
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Incorrect Information');", true);
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
                    var dtErrorResult = new MasterHorseBL().Import30(dt, "HorseTrainerActual");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse_TrainerHidden");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_TrainerHidden.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        BindData();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('All Record has been added successfully.');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('No Record Found.');", true);
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
                //using (DataSet ds = new MasterHorseBL().GetHorseNameWithCombination(_horseId, "HorseTrainerActual"))
                using (DataSet ds = new MasterHorseBL().GetExport(_horseId, "HorseTrainerActual"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                      //  dt.Columns.Remove("TrainerActualID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse_TrainerHidden");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_TrainerHidden.xlsx");
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