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

    public partial class CardSelection : System.Web.UI.Page
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
            //drpdwnHorseNo.ClearSelection();
            //drpdwndayraceno.ClearSelection();
            lblSeason.Text = "";
            lblYear.Text = "";
            grdvwHorseDetail.DataSource = new DataTable();
            grdvwHorseDetail.DataBind();
            txtbxHotliner.Text = string.Empty;
            hdnfieldhotlinerid.Value = string.Empty;
           // hdnfieldHorseID.Value = string.Empty;
            //txtbxHorseName.Text = string.Empty;
            //txtbxHotline.Text = string.Empty;
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
            DataTable dt = new CardsBL().GetCardAutoFiller("AddSelectionList", prefixText);

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
                if (dataKey != null)
                {
                    ViewState["SelectionCID"] = dataKey.Value;

                    HiddenField hdnfieldSelectorPID = (HiddenField)row.FindControl("hdnfieldSelectorPID");
                    hdnfieldSelectorPID.Value = hdnfieldSelectorPID.Value;
                    HiddenField hdnfieldPRofessionalName = (HiddenField)row.FindControl("hdnfieldPRofessionalName");
                    txtbxHotliner.Text = hdnfieldPRofessionalName.Value;


                    var dr1 = row.Cells[2].Text;
                    string[] dr1break = dr1.Split(':');
                    if (dr1break.Length == 3)
                    {
                        txtbx11.Text = dr1break[0];
                        txtbx12.Text = dr1break[1];
                        txtbx13.Text = dr1break[2];
                    }
                    else if (dr1break.Length == 2)
                    {
                        txtbx11.Text = dr1break[0];
                        txtbx12.Text = dr1break[1];
                    }
                    else
                    {
                        txtbx11.Text = dr1break[0];
                    }


                    var dr2= row.Cells[3].Text;
                    string[] dr2break = dr2.Split(':');
                    if (dr2break.Length == 3)
                    {
                        txtbx21.Text = dr1break[0];
                        txtbx22.Text = dr1break[1];
                        txtbx23.Text = dr1break[2];
                    }
                    else if (dr2break.Length == 2)
                    {
                        txtbx21.Text = dr1break[0];
                        txtbx22.Text = dr1break[1];
                    }
                    else
                    {
                        txtbx21.Text = dr1break[0];
                    }

                    if (!row.Cells[4].Text.Equals(""))
                    {
                        var dr3 = row.Cells[4].Text;
                        string[] dr3break = dr3.Split(':');
                        if (dr3break.Length == 3)
                        {
                            txtbx31.Text = dr1break[0];
                            txtbx32.Text = dr1break[1];
                            txtbx33.Text = dr1break[2];
                        }
                        else if (dr3break.Length == 2)
                        {
                            txtbx31.Text = dr1break[0];
                            txtbx32.Text = dr1break[1];
                        }
                        else
                        {
                            txtbx31.Text = dr1break[0];
                        }
                    }

                    if (!row.Cells[5].Text.Equals(""))
                    {
                        var dr4 = row.Cells[5].Text;
                        string[] dr4break = dr4.Split(':');
                        if (dr4break.Length == 3)
                        {
                            txtbx41.Text = dr1break[0];
                            txtbx42.Text = dr1break[1];
                            txtbx43.Text = dr1break[2];
                        }
                        else if (dr4break.Length == 2)
                        {
                            txtbx41.Text = dr1break[0];
                            txtbx42.Text = dr1break[1];
                        }
                        else
                        {
                            txtbx41.Text = dr1break[0];
                        }
                    }

                    if (!row.Cells[6].Text.Equals(""))
                    {
                        var dr5 = row.Cells[6].Text;
                        string[] dr5break = dr5.Split(':');
                        if (dr5break.Length == 3)
                        {
                            txtbx51.Text = dr1break[0];
                            txtbx52.Text = dr1break[1];
                            txtbx53.Text = dr1break[2];
                        }
                        else if (dr5break.Length == 2)
                        {
                            txtbx51.Text = dr1break[0];
                            txtbx52.Text = dr1break[1];
                        }
                        else
                        {
                            txtbx51.Text = dr1break[0];
                        }
                    }

                    if (!row.Cells[7].Text.Equals(""))
                    {
                        var dr6 = row.Cells[7].Text;
                        string[] dr6break = dr6.Split(':');
                        if (dr6break.Length == 3)
                        {
                            txtbx61.Text = dr1break[0];
                            txtbx62.Text = dr1break[1];
                            txtbx63.Text = dr1break[2];
                        }
                        else if (dr6break.Length == 2)
                        {
                            txtbx61.Text = dr1break[0];
                            txtbx62.Text = dr1break[1];
                        }
                        else
                        {
                            txtbx61.Text = dr1break[0];
                        }
                    }

                    if (!row.Cells[8].Text.Equals(""))
                    {
                        var dr7 = row.Cells[8].Text;
                        string[] dr7break = dr7.Split(':');
                        if (dr7break.Length == 3)
                        {
                            txtbx71.Text = dr1break[0];
                            txtbx72.Text = dr1break[1];
                            txtbx73.Text = dr1break[2];
                        }
                        else if (dr7break.Length == 2)
                        {
                            txtbx71.Text = dr1break[0];
                            txtbx72.Text = dr1break[1];
                        }
                        else
                        {
                            txtbx71.Text = dr1break[0];
                        }
                    }

                    if (!row.Cells[9].Text.Equals(""))
                    {
                        var dr8 = row.Cells[9].Text;
                        string[] dr8break = dr8.Split(':');
                        if (dr8break.Length == 3)
                        {
                            txtbx81.Text = dr1break[0];
                            txtbx82.Text = dr1break[1];
                            txtbx83.Text = dr1break[2];
                        }
                        else if (dr8break.Length == 2)
                        {
                            txtbx81.Text = dr1break[0];
                            txtbx82.Text = dr1break[1];
                        }
                        else
                        {
                            txtbx81.Text = dr1break[0];
                        }
                    }

                    if (!row.Cells[10].Text.Equals(""))
                    {
                        var dr9 = row.Cells[10].Text;
                        string[] dr9break = dr9.Split(':');
                        if (dr9break.Length == 3)
                        {
                            txtbx91.Text = dr1break[0];
                            txtbx92.Text = dr1break[1];
                            txtbx93.Text = dr1break[2];
                        }
                        else if (dr9break.Length == 2)
                        {
                            txtbx91.Text = dr1break[0];
                            txtbx92.Text = dr1break[1];
                        }
                        else
                        {
                            txtbx91.Text = dr1break[0];
                        }
                    }

                    if (!row.Cells[11].Text.Equals(""))
                    {
                        var dr10 = row.Cells[11].Text;
                        string[] dr10break = dr10.Split(':');
                        if (dr10break.Length == 3)
                        {
                            txtbx101.Text = dr1break[0];
                            txtbx102.Text = dr1break[1];
                            txtbx103.Text = dr1break[2];
                        }
                        else if (dr10break.Length == 2)
                        {
                            txtbx101.Text = dr1break[0];
                            txtbx102.Text = dr1break[1];
                        }
                        else
                        {
                            txtbx101.Text = dr1break[0];
                        }
                    }
                    if (!row.Cells[12].Text.Equals(""))
                    {
                        var dr11 = row.Cells[12].Text;
                        string[] dr11break = dr11.Split(':');
                        if (dr11break.Length == 3)
                        {
                            txtbx111.Text = dr1break[0];
                            txtbx112.Text = dr1break[1];
                            txtbx113.Text = dr1break[2];
                        }
                        else if (dr11break.Length == 2)
                        {
                            txtbx111.Text = dr1break[0];
                            txtbx112.Text = dr1break[1];
                        }
                        else
                        {
                            txtbx111.Text = dr1break[0];
                        }
                    }

                    if (!row.Cells[13].Text.Equals(""))
                    {
                        txtbxDayBest.Text = row.Cells[13].Text;
                    }

                    if (!row.Cells[14].Text.Equals(""))
                    {
                        txtbxGoodDouble.Text = row.Cells[14].Text;
                    }

                    if (!row.Cells[15].Text.Equals(""))
                    {
                        txtbxgoodplace.Text = row.Cells[15].Text;
                    }

                    if (!row.Cells[16].Text.Equals(""))
                    {
                        txtbxRoller.Text = row.Cells[16].Text;
                    }

                    if (!row.Cells[17].Text.Equals(""))
                    {
                        txtbxUpsetter.Text = row.Cells[17].Text;
                    }

                    if (!row.Cells[18].Text.Equals(""))
                    {
                        txtbxeatablebet.Text = row.Cells[18].Text;
                    }

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
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "RaceDaySelection");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblSeason.Text = ds.Tables[0].Rows[0][0].ToString();
                lblYear.Text = ds.Tables[0].Rows[0][1].ToString();
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
            else
            {
                grdvwHorseDetail.DataSource = new DataTable();
                grdvwHorseDetail.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                VKATalkClassLayer.CardSelection cs =new VKATalkClassLayer.CardSelection();
                var status=0;
                cs.SelectionPID = Convert.ToInt32(hdnfieldhotlinerid.Value);
                cs.CenterID = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                if (txtbx12.Text.Equals(""))
                {
                    cs.DRNo1 = txtbx11.Text;
                }
                else if (txtbx13.Text.Equals(""))
                {
                    cs.DRNo1 = txtbx11.Text + ":" + txtbx12.Text;
                }
                else if (txtbx11.Text.Equals(""))
                {
                    cs.DRNo1 = string.Empty;
                }
                else
                {
                    cs.DRNo1 = txtbx11.Text + ":" + txtbx12.Text + ":" + txtbx13.Text;
                }

                if (txtbx22.Text.Equals(""))
                {
                    cs.DRNo2 = txtbx21.Text;
                }
                else if (txtbx23.Text.Equals(""))
                {
                    cs.DRNo2 = txtbx21.Text + ":" + txtbx22.Text;
                }
                else if (txtbx21.Text.Equals(""))
                {
                    cs.DRNo2 = string.Empty;
                }
                else
                {
                    cs.DRNo2 = txtbx21.Text + ":" + txtbx22.Text + ":" + txtbx23.Text;
                }

                if (txtbx32.Text.Equals(""))
                {
                    cs.DRNo3 = txtbx31.Text;
                }
                else if (txtbx33.Text.Equals(""))
                {
                    cs.DRNo3 = txtbx31.Text + ":" + txtbx32.Text;
                }
                else if (txtbx31.Text.Equals(""))
                {
                    cs.DRNo3 = string.Empty;
                }
                else
                {
                    cs.DRNo3 = txtbx31.Text + ":" + txtbx32.Text + ":" + txtbx33.Text;
                }

                if (txtbx42.Text.Equals(""))
                {
                    cs.DRNo4 = txtbx41.Text;
                }
                else if (txtbx43.Text.Equals(""))
                {
                    cs.DRNo4 = txtbx41.Text + ":" + txtbx42.Text;
                }
                else if (txtbx41.Text.Equals(""))
                {
                    cs.DRNo4 = string.Empty;
                }
                else
                {
                    cs.DRNo4 = txtbx41.Text + ":" + txtbx42.Text + ":" + txtbx43.Text;
                }

                if (txtbx52.Text.Equals(""))
                {
                    cs.DRNo5 = txtbx51.Text;
                }
                else if (txtbx53.Text.Equals(""))
                {
                    cs.DRNo5 = txtbx51.Text + ":" + txtbx52.Text;
                }
                else if (txtbx51.Text.Equals(""))
                {
                    cs.DRNo5 = string.Empty;
                }
                else
                {
                    cs.DRNo5 = txtbx51.Text + ":" + txtbx52.Text + ":" + txtbx53.Text;
                }

                if (txtbx62.Text.Equals(""))
                {
                    cs.DRNo6 = txtbx61.Text;
                }
                else if (txtbx63.Text.Equals(""))
                {
                    cs.DRNo6 = txtbx61.Text + ":" + txtbx62.Text;
                }
                else if (txtbx61.Text.Equals(""))
                {
                    cs.DRNo6 = string.Empty;
                }
                else
                {
                    cs.DRNo6 = txtbx61.Text + ":" + txtbx62.Text + ":" + txtbx63.Text;
                }

                if (txtbx72.Text.Equals(""))
                {
                    cs.DRNo7 = txtbx71.Text;
                }
                else if (txtbx73.Text.Equals(""))
                {
                    cs.DRNo7 = txtbx71.Text + ":" + txtbx72.Text;
                }
                else if (txtbx71.Text.Equals(""))
                {
                    cs.DRNo7 = string.Empty;
                }
                else
                {
                    cs.DRNo7 = txtbx71.Text + ":" + txtbx72.Text + ":" + txtbx73.Text;
                }

                if (txtbx82.Text.Equals(""))
                {
                    cs.DRNo8 = txtbx81.Text;
                }
                else if (txtbx83.Text.Equals(""))
                {
                    cs.DRNo8 = txtbx81.Text + ":" + txtbx82.Text;
                }
                else if (txtbx81.Text.Equals(""))
                {
                    cs.DRNo8 = string.Empty;
                }
                else
                {
                    cs.DRNo8 = txtbx81.Text + ":" + txtbx82.Text + ":" + txtbx83.Text;
                }

                if (txtbx92.Text.Equals(""))
                {
                    cs.DRNo9 = txtbx91.Text;
                }
                else if (txtbx93.Text.Equals(""))
                {
                    cs.DRNo9 = txtbx91.Text + ":" + txtbx92.Text;
                }
                else if (txtbx91.Text.Equals(""))
                {
                    cs.DRNo9 = string.Empty;
                }
                else
                {
                    cs.DRNo9 = txtbx91.Text + ":" + txtbx92.Text + ":" + txtbx93.Text;
                }

                if (txtbx102.Text.Equals(""))
                {
                    cs.DRNo10 = txtbx101.Text;
                }
                else if (txtbx103.Text.Equals(""))
                {
                    cs.DRNo10 = txtbx101.Text + ":" + txtbx102.Text;
                }
                else if (txtbx101.Text.Equals(""))
                {
                    cs.DRNo10 = string.Empty;
                }
                else
                {
                    cs.DRNo10 = txtbx101.Text + ":" + txtbx102.Text + ":" + txtbx103.Text;
                }

                if (txtbx112.Text.Equals(""))
                {
                    cs.DRNo11 = txtbx111.Text;
                }
                else if (txtbx113.Text.Equals(""))
                {
                    cs.DRNo11 = txtbx111.Text + ":" + txtbx112.Text;
                }
                else if (txtbx111.Text.Equals(""))
                {
                    cs.DRNo11 = string.Empty;
                }
                else
                {
                    cs.DRNo11 = txtbx111.Text + ":" + txtbx112.Text + ":" + txtbx113.Text;
                }
                cs.DayBest = txtbxDayBest.Text;
                cs.GoodDouble = txtbxGoodDouble.Text;
                cs.GoodPlace = txtbxgoodplace.Text;
                cs.Rolling = txtbxRoller.Text;
                cs.Upsetter = txtbxUpsetter.Text;
                cs.EatableBet = txtbxeatablebet.Text;
                cs.UserId = 1;

                if (btnAdd.Text.Equals("Add"))
                {
                    cs.SelectionCID = 0;
                    status =new CardsBL().RaceCardSelection(cs,txtbxRaceDate.Text,"Insert");
                }
                else if (btnAdd.Text.Equals("Update"))
                {
                    cs.SelectionCID = (int)ViewState["SelectionCID"];
                    status = new CardsBL().RaceCardSelection(cs, txtbxRaceDate.Text, "Update");
                    btnAdd.Text = "Add";
                }
                if (status == 1)
                {
                    AcceptanceShow();
                    var message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else if (status == 2)
                {
                    AcceptanceShow();
                    var message = "Record updated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_Selection"))
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
                    var dtErrorResult = new CardsBL().Import30(dt, "Card_Selection", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Selection");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Selection.xlsx");
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
                using (DataSet ds = new CardsBL().GetExport(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "Card_Selection"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Selection");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Selection.xlsx");
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