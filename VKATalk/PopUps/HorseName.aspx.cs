using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VKATalkBusinessLayer;
using System.Globalization;
using System.Data;

namespace VKATalk.PopUps
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;

    using OfficeOpenXml;

    public partial class HorseName : System.Web.UI.Page
    {
        MasterHorseBL Bl = new MasterHorseBL();
        int _userId = 1;
        private int _value = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
            if (!Request.QueryString["HorseNameID"].Equals(""))
            {
                _value = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                hdnfldhorseId.Value = Convert.ToString(_value);
            }
            if (!IsPostBack)
            {
                try
                {
                    if (_value != 0)
                    {
                        GetGridviewData();
                       // btnSave.Text = "Update";
                    }
                    //else
                    //{
                    //    btnSave.Text = "Add";
                    //}
					// BindDropDown(this.drpdwnProfiler, "ProfessionalProfileType", "ProfileType", "ProfileTypeID");
					BindDropDown(drpdwnProfile, "MasterHorseProfile", "HorseProfile", "ProfileHMID");
					//drpdwnProfile.Items.Insert(0, new ListItem("-- Please select --", "-1"));
				}
				catch (Exception ex)
                {
                    object[] now = new object[] { "Page_Load (HorsePopup):", DateTime.Now, ", Issue Detail:", ex.Message + ex.StackTrace };
                    ErrorHandling.CheckEachSteps(string.Concat(now));
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
                }
            }
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
		/// Fill current Mission
		/// </summary>
		/// <param name="prefixText"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddCommentList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("CommentList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
            }
            return horseList;
        }


        private void GetGridviewData()
        {
                DataSet ds = Bl.GetHorseNameWithCombination(_value, "HorseName");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvHorseName.DataSource = ds.Tables[0];
                    GvHorseName.DataBind();
                }
                else
                {
                    GvHorseName.DataSource = new DataTable();
                    GvHorseName.DataBind();
                }
        }

        /// <summary>
        /// Bind Horse Name already exist in out Database
        /// </summary>
        private void BindData(DataTable dt)
        {
                DataSet ds = null;
                if (btnSave.Text.Equals("Add"))
                {
                    ds = Bl.GetHorseNameWithCombination(Convert.ToInt32(dt.Rows[0][1]), "HorseNameCurrentInsert");
                }
                else
                {
                    ds = Bl.GetHorseNameWithCombination(Convert.ToInt32(dt.Rows[0][0]), "HorseNameCurrentUpdate");
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvHorseName.DataSource = ds.Tables[0];
                    GvHorseName.DataBind();
                }
                else
                {
                    GvHorseName.DataSource = new DataTable();
                    GvHorseName.DataBind();
                }
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
                string message = string.Empty;
                int newHorseId = 0;
                DataTable dt;
				if (btnSave.Text.Equals("Add") && GvHorseName.Rows.Count > 0)
				{
					message = "Horse Name Already Created.";
					ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
				}
				else
				{
					if (btnSave.Text.Equals("Add"))
					{
						dt = this.Bl.HorseName(
							0,
							this.txtbxHorseName.Text,
							this.txtbxHorseShortName.Text,
							this.txtbxHorseAlias.Text,
							this.txtbxDateofBirth.Text,
							this.txtbxComment.Text,
							this._userId,
							"Insert",
							drpdwnProfile.SelectedItem.Value);

					}
					else
					{
						dt = this.Bl.HorseName(
						   (int)ViewState["GridViewRowID"],
						   this.txtbxHorseName.Text,
						   this.txtbxHorseShortName.Text,
						   this.txtbxHorseAlias.Text,
						   this.txtbxDateofBirth.Text,
						   this.txtbxComment.Text,
						   this._userId,
						   "Update",
						   drpdwnProfile.SelectedItem.Value);
					}
					newHorseId = Convert.ToInt32(dt.Rows[0][2]);
					if (newHorseId == 2)
					{
						Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Record already exist');", true);
					}
					else if (newHorseId == 3)
					{
						Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('This Name Already Taken By Another Horse With Same Year of Birth');", true);
					}
					else if (newHorseId == 4)
					{
						Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('This Name Already Taken By Another Horse With Same Date of Birth');", true);
					}
                    else if (newHorseId == 5)
                    {
                        if (this.btnSave.Text.Equals("Add"))
                        {
                            hdnfldhorseId.Value = Convert.ToString(dt.Rows[0][1]);
                        }
                        ClearAllSelection(this);
                        BindData(dt);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Record Activated sucessfully.');", true);
                    }
                    else
					{
						if (this.btnSave.Text.Equals("Add"))
						{
							hdnfldhorseId.Value = Convert.ToString(dt.Rows[0][1]);
						}
						ClearAllSelection(this);
						this.Page.ClientScript.RegisterStartupScript(
							this.GetType(),
							"Popup",
							this.btnSave.Text.Equals("Update")
								? "ShowPopup('Record Updated Successfully');"
								: "ShowPopup('Record Added Successfully.');",
							true);
						BindData(dt);

					}
				}
            }
            catch (Exception ex)
            {
                // listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
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
                if (hdnfldhorseId.Value != "")
                {
                    Session["HorseID"] = hdnfldhorseId.Value;
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                }


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
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
        }

        /// <summary>
        /// on select index change
        /// </summary>
        /// <param name="sender">value</param>
        /// <param name="e">event</param>
        protected void GvHorseName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = GvHorseName.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvHorseName.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
                    txtbxHorseName.Text = hdnval.Value.Equals("&nbsp;") ? "" : hdnval.Value.Contains("&#39;") ? hdnval.Value.Replace("&#39;", "'") : hdnval.Value;
                    txtbxHorseShortName.Text = row.Cells[2].Text.Equals("&nbsp;") ? "" : row.Cells[2].Text.Contains("&#39;") ? row.Cells[2].Text.Replace("&#39;", "'") : row.Cells[2].Text;
                    txtbxHorseAlias.Text = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&#39;") ? row.Cells[3].Text.Replace("&#39;", "'") : row.Cells[3].Text;
                    txtbxDateofBirth.Text = row.Cells[4].Text;
                    if (row.Cells[6].Text.Contains("&quot;"))
                    {
                        txtbxComment.Text = row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&quot;") ? row.Cells[6].Text.Replace("&quot;", "\"") : row.Cells[6].Text;
                    }
                    else
                    {
                        txtbxComment.Text = row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&#39;") ? row.Cells[6].Text.Replace("&#39;", "'") : row.Cells[6].Text;
                    }

                    if (row.Cells[0].Text.Equals("Cr"))
                    {
                        if (GvHorseName.Rows.Count == 2)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please select Old Name');", true);
                            ClearAllSelection(this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
            }
        }

        protected void txtbxHorseName_OnTextChanged(object sender, EventArgs e)
        {
            var txtvalue = sender as TextBox;
            if (txtvalue != null)
            {
                string value = txtvalue.Text;
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                this.txtbxHorseName.Text = textInfo.ToTitleCase(value.ToLower());
                this.txtbxHorseShortName.Text = this.txtbxHorseName.Text.Replace(" ", "");
                this.txtbxHorseAlias.Text = this.txtbxHorseShortName.Text.Substring(0, Math.Min(this.txtbxHorseShortName.Text.Length, 10));
            }
        }

       protected void txtbxAge_OnTextChanged(object sender, EventArgs e)
        {
            var txtvalue = sender as TextBox;
            if (txtvalue != null)
            {
                var value = txtvalue.Text;
                var getYear = (Convert.ToInt32(DateTime.Now.Year.ToString()) - Convert.ToInt32(value));
                txtbxDateofBirth.Text = "30" + "11" + getYear;
            }
        }

       protected void txtbxDateofBirth_OnTextChanged(object sender, EventArgs e)
       {
           var txtvalue = sender as TextBox;
           if  (txtvalue != null && txtvalue.Text != "__-__-____")
           {
               var firstdateofcurrentyear = new DateTime(DateTime.Now.Year, 1, 1);
               //var year = firstdateofcurrentyear.Year;
               var currentyear = DateTime.Now.Year;
               var userdate = Convert.ToDateTime(txtvalue.Text).Year;
               if (userdate.Equals(currentyear))
               {
                   txtbxAge.Text = "0";
               }
               else
               {
                   var year = new DateTime(firstdateofcurrentyear.Subtract(Convert.ToDateTime(txtvalue.Text)).Ticks).Year;
                   txtbxAge.Text = Convert.ToString(currentyear- userdate);
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Horse_Name"))
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
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
                    var dtErrorResult = Bl.Import30(dt, "HorseName");
                    GetGridviewData();
                    var message1 = "All Record has been added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message1 + "');", true);
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
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }


        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                using (DataSet ds = new ProspectusBL().GetExport(0, "HorseName"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("HorseID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse Name");
                            int rowstart = 1;
                            int colstart = 1;
                            int rowend = rowstart;
                            int colend = colstart + (dt.Columns.Count - 1);
                            //  int colend = colstart;
                            rowend = rowstart + dt.Rows.Count;
                            ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);
                            int i = 1;
                            foreach (DataColumn dc in dt.Columns)
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_Name.xlsx");
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
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "Popup",
                    "ShowPopup('Incorrect Information');",
                    true);
            }

        }

    }
}