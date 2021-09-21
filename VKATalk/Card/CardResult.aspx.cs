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
    using System.Drawing;
    using System.IO;
    using System.Web;
    using System.Web.WebSockets;

    using OfficeOpenXml;
    using VKATalk.Common;

    public partial class CardResult : System.Web.UI.Page
    {
        private DataTable dtCount;
        public static int placingcount = 0;
        public static int horsenocheck = 0;
        public static int count = 1;
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

        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                var dt = new CardsBL().GetAcceptanceDivisionDetail(
                    txtbxDivisionRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "CardResult");
                if (dt.Rows.Count > 0)
                {
                    lblSeason.Text = dt.Rows[0][7].ToString();
                    lblYear.Text = dt.Rows[0][8].ToString();
                    dvgridview.Visible = true;
                    grdvwRaceDetail.DataSource = dt;
                    grdvwRaceDetail.DataBind();
                }
                else
                {
                    dvgridview.Visible = false;
                    grdvwRaceDetail.DataSource = new DataTable();
                    grdvwRaceDetail.DataBind();
                    GvShowALL.DataSource = new DataTable();
                    GvShowALL.DataBind();
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


        protected void grdvwRaceDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //ClearALL();
                //BtnSubmit.Text = "Update";
                placingcount = 0;
                horsenocheck = 0;
                count = 1;
                GridViewRow row = grdvwRaceDetail.SelectedRow;
                HiddenField hdnfielddivisionracename = (HiddenField)row.FindControl("hdnfielddivisionracename");
                HiddenField hdnfieldGeneralRaceNameIDG = (HiddenField)row.FindControl("hdnfieldGeneralRaceNameIDG");
                hdnfieldGeneralRaceNameID.Value = hdnfieldGeneralRaceNameIDG.Value;
                HiddenField hdnfieldGeneralRaceID = (HiddenField)row.FindControl("hdnfieldGeneralRaceID");
                HiddenField hdnfieldHorseNameID = (HiddenField)row.FindControl("hdnfieldHorseNameID");
                var dataKey = grdvwRaceDetail.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ViewState["DivisionRaceID"] = dataKey.Value; //generalraceid
                    Session["DivisionRaceID"] = dataKey.Value;
                    hdnfielddivisionraceidpopup.Value = dataKey.Value.ToString();
                    ViewState["DivisionRaceName"] = hdnfielddivisionracename.Value;//reneralracename
                    ViewState["GeneralRaceNameID"] = hdnfieldGeneralRaceNameIDG.Value;
                    ViewState["GeneralRaceID"] = hdnfieldGeneralRaceID.Value;
                    hdnfieldGeneralRaceIDM.Value = hdnfieldGeneralRaceID.Value;
                   // hdnfieldHorseNameIDM.Value = hdnfieldHorseNameID.Value;
                   // hdnfieldDivisionRaceID.Value = dataKey.Value.ToString();
                    lblGeneralRaceName.Text = hdnfielddivisionracename.Value;
                }

                ShowDeclaration();
                
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        private void ShowDeclaration()
        {
            var dt = new CardsBL().GetDeclaration(Convert.ToInt32(ViewState["DivisionRaceID"]), Convert.ToInt32(ViewState["GeneralRaceNameID"]), 
                                ViewState["DivisionRaceName"].ToString(), "CardResult", txtbxDivisionRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
            dtCount = dt.Tables[0];
            if (dt.Tables[0].Rows.Count > 0)
            {
                GvShowALL.DataSource = dt;
                GvShowALL.DataBind();

            }
            else
            {
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
            }

            var dt1 = new CardsBL().GetCardResultDisplayData(Convert.ToInt32(ViewState["DivisionRaceID"]), Convert.ToInt32(ViewState["GeneralRaceNameID"]),
                                ViewState["DivisionRaceName"].ToString(), "CardResult", txtbxDivisionRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
            //dtCount1 = dt1.Tables[0];
            if (dt1.Tables[0].Rows.Count > 0)
            {
                GvCardResult.DataSource = dt1;
                GvCardResult.DataBind();

            }
            else
            {
                GvCardResult.DataSource = new DataTable();
                GvCardResult.DataBind();
            }
        }

        protected void txtbxJockeyNameG_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(sender as TextBox).NamingContainer;
                HiddenField hdnfieldProfessionalNameid = row.FindControl("hdnfieldProfessionalNameid") as HiddenField;
                HiddenField hdnfieldDivisionRaceID = row.FindControl("hdnfieldDivisionRaceID") as HiddenField;
                HiddenField hdnfieldHorseNameIDG = row.FindControl("hdnfieldHorseNameIDG") as HiddenField;
                TextBox txtbxCarriedWeightG = row.FindControl("txtbxCarriedWeightG") as TextBox;
                if (!hdnfieldProfessionalNameid.Value.Equals(""))
                {
                    Label lblJA = row.FindControl("lblJA") as Label;
                    Label lblACCEPTANCEWEIGHTGBC = row.FindControl("lblACCEPTANCEWEIGHTGBC") as Label;


                    Label lblDeclareWeight = row.FindControl("lblDeclareWeight") as Label;
                    DataTable dt = new CardsBL().GetCardDJA(Convert.ToInt32(hdnfieldProfessionalNameid.Value),
                                       txtbxDivisionRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldDivisionRaceID.Value));
                    //DataTable dt = new CardsBL().GetCardResultInformation1(Convert.ToInt32(hdnfieldProfessionalNameid.Value), Convert.ToInt32(hdnfieldDivisionRaceID.Value));
                    if (dt.Rows.Count > 0 && !dt.Rows[0][0].ToString().Equals(""))
                    {
                        lblJA.Text = dt.Rows[0]["JA"].ToString();
                        lblDeclareWeight.Text = Convert.ToString(Convert.ToDouble(lblACCEPTANCEWEIGHTGBC.Text) - Convert.ToDouble(dt.Rows[0][""]));
                    }
                    else
                    {
                        lblJA.Text = string.Empty;
                        lblDeclareWeight.Text = lblACCEPTANCEWEIGHTGBC.Text;
                    }
                   // lblDeclareWeight.Focus();
                }

                txtbxCarriedWeightG.Focus();

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void txtbxHorseNameG_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                //placingcount

                GridViewRow row = (GridViewRow)(sender as TextBox).NamingContainer;
                HiddenField hdnfieldHorseNameIDG = row.FindControl("hdnfieldHorseNameIDG") as HiddenField;
                HiddenField hdnfieldDivisionRaceID = row.FindControl("hdnfieldDivisionRaceID") as HiddenField;
                Label lblHORSENO = row.FindControl("lblHORSENO") as Label;
                Label lblACCEPTANCEWEIGHTGBC = row.FindControl("lblACCEPTANCEWEIGHTGBC") as Label;
                TextBox txtbxJockeyNameG = row.FindControl("txtbxJockeyNameG") as TextBox;
                HiddenField hdnfieldProfessionalnameid = row.FindControl("hdnfieldProfessionalnameid") as HiddenField;
                Label lblJA = row.FindControl("lblJA") as Label;
                Label lblDeclareWeight = row.FindControl("lblDeclareWeight") as Label;
                TextBox txtbxCarriedWeightG = row.FindControl("txtbxCarriedWeightG") as TextBox;
                TextBox txtbxPlacing = row.FindControl("txtbxPlacing") as TextBox;
                Label lblJkyWgts = row.FindControl("lblJkyWgts") as Label;

                TextBox txtbxHorseBodyWeightG = row.FindControl("txtbxHorseBodyWeightG") as TextBox;
                TextBox txtbxRevisedRatingG = row.FindControl("txtbxRevisedRatingG") as TextBox;

                //HiddenField hdnfieldProfessionalnameid = row.FindControl("hdnfieldProfessionalnameid") as HiddenField;

                DropDownList drpdwnVerdictMarginG = row.FindControl("drpdwnVerdictMarginG") as DropDownList;
                HiddenField hdnfielVerdictMargin = row.FindControl("hdnfielVerdictMargin") as HiddenField;
                BindDropDown(drpdwnVerdictMarginG, "VerdictMargin", "VerdictMargin", "VerdictMarginID");
                drpdwnVerdictMarginG.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                
                TextBox txtbxFinishTimeMMG = row.FindControl("txtbxFinishTimeMMG") as TextBox;
                TextBox txtbxFinishTimeSSG = row.FindControl("txtbxFinishTimeSSG") as TextBox;
                TextBox txtbxFinishTimeMSG = row.FindControl("txtbxFinishTimeMSG") as TextBox;

                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                {
                    DataTable dt = new CardsBL().GetCardResultInformation(Convert.ToInt32(hdnfieldHorseNameIDG.Value),Convert.ToInt32(hdnfieldDivisionRaceID.Value));
                    if (dt.Rows.Count > 0 && !dt.Rows[0][0].ToString().Equals(""))
                    {
                        txtbxHorseBodyWeightG.Text= dt.Rows[0]["HorseBodyWeight"].ToString();
                        txtbxRevisedRatingG.Text = dt.Rows[0]["RevisedRating"].ToString();
                        lblHORSENO.Text = dt.Rows[0]["HorseNo"].ToString();
                        lblACCEPTANCEWEIGHTGBC.Text = dt.Rows[0]["ACCEPTANCEWEIGHTGBC"].ToString();
                        txtbxJockeyNameG.Text = dt.Rows[0]["JockeyName"].ToString();
                        hdnfieldProfessionalnameid.Value = dt.Rows[0]["JockeyNameID"].ToString();
                        lblJA.Text = dt.Rows[0]["JA"].ToString();
                        lblDeclareWeight.Text = dt.Rows[0]["DeclaredWeight"].ToString();
                        txtbxCarriedWeightG.Text = dt.Rows[0]["CarriedWeight"].ToString();
                        txtbxJockeyNameG.Text = dt.Rows[0]["JockeyName"].ToString();
                        lblJkyWgts.Text = dt.Rows[0]["JkyWgts"].ToString();
                        if(dt.Rows[0]["VerdictMargin"].ToString() != "")
                        drpdwnVerdictMarginG.Items.FindByText(dt.Rows[0]["VerdictMargin"].ToString()).Selected = true;

                        txtbxFinishTimeMMG.Text = dt.Rows[0]["MM"].ToString();
                        txtbxFinishTimeSSG.Text = dt.Rows[0]["SS"].ToString();
                        txtbxFinishTimeMSG.Text = dt.Rows[0]["MS"].ToString();
                    }
                    else
                    {
                        txtbxFinishTimeMMG.Text =string.Empty;
                        txtbxFinishTimeSSG.Text =string.Empty;
                        txtbxFinishTimeMSG.Text = string.Empty;
                        txtbxHorseBodyWeightG.Text = string.Empty;
                        txtbxRevisedRatingG.Text = string.Empty;
                        txtbxCarriedWeightG.Text = string.Empty;
                        lblDeclareWeight.Text = string.Empty;
                        lblHORSENO.Text = string.Empty;
                        lblACCEPTANCEWEIGHTGBC.Text = string.Empty;
                        txtbxJockeyNameG.Text = string.Empty;
                        hdnfieldProfessionalnameid.Value = string.Empty;
                        lblJA.Text = string.Empty;
                        txtbxJockeyNameG.Text = string.Empty;
                        lblJkyWgts.Text = string.Empty;
                    }
                }

                if (horsenocheck != Convert.ToInt32(lblHORSENO.Text))
                {
                    placingcount = placingcount + 1;
                    
                }
                txtbxPlacing.Text = placingcount.ToString();
                horsenocheck = Convert.ToInt32(lblHORSENO.Text);

                txtbxPlacing.Focus();

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void GvShowALL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drpdwnVerdictMarginG = (e.Row.FindControl("drpdwnVerdictMarginG") as DropDownList);
                BindDropDown(drpdwnVerdictMarginG, "VerdictMargin", "VerdictMargin", "VerdictMarginID");
                drpdwnVerdictMarginG.Items.Insert(0, new ListItem("-- Please select --", "-1"));
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

        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddJockeyList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CardDeclarationJockeyList", prefixText);

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
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> AddHorseNameList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetHorseNameAutoFillerMultiple("CardResultHorseNameList", prefixText, HttpContext.Current.Session["DivisionRaceID"].ToString());
            //var test = HttpContext.Current.Session["Checkvalue"];
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            DataTable dtDeclaration = new DataTable("Declaration");
            try
            {
                var result = 0;
                var totalcount = 0;
                dtDeclaration.Columns.Add("DataEntryDate", typeof(string));
                dtDeclaration.Columns.Add("DivisionRaceDate", typeof(string));
                dtDeclaration.Columns.Add("CenterMID", typeof(int));
                dtDeclaration.Columns.Add("GeneralRaceID", typeof(int));
                dtDeclaration.Columns.Add("GeneralRaceNameID", typeof(int));
                dtDeclaration.Columns.Add("DivisionRaceID", typeof(int));
                dtDeclaration.Columns.Add("HorseID", typeof(int));
                dtDeclaration.Columns.Add("HorseNameID", typeof(int));
                dtDeclaration.Columns.Add("Placing", typeof(string));
                dtDeclaration.Columns.Add("JockeyID", typeof(int));//9
                dtDeclaration.Columns.Add("CarriedWeight", typeof(string));
                dtDeclaration.Columns.Add("FinishTime", typeof(String));
                dtDeclaration.Columns.Add("VerdictMarginMID", typeof(int));
                dtDeclaration.Columns.Add("HorseBodyWeight", typeof(string));//13
                dtDeclaration.Columns.Add("RevisedRating", typeof(string));
                dtDeclaration.Columns.Add("CreatedDate", typeof(DateTime));//15
                dtDeclaration.Columns.Add("CreatedUserID", typeof(int));
                dtDeclaration.Columns.Add("IsActive", typeof(int));
                dtDeclaration.Columns.Add("JockeyNamePID", typeof(int));
                dtDeclaration.Columns.Add("JockeyAllowance", typeof(string));//19
                dtDeclaration.Columns.Add("FinalDeclareWeight", typeof(string));
                dtDeclaration.Columns.Add("Action", typeof(string));

                int rowcount = 0;
               
                foreach (GridViewRow row in GvShowALL.Rows)
                {
                    DataTable status = new CardsBL().GetDeclareOldRecordStatus(Convert.ToInt32(ViewState["GeneralRaceNameID"]), txtbxDivisionRaceDate.Text,
                                          Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), lblSeason.Text, lblYear.Text, "CardResult",
                                          Convert.ToInt32(ViewState["DivisionRaceID"]), Convert.ToInt32((row.FindControl("hdnfieldHorseNameIDG") as HiddenField).Value));
                    if (btnAdd.Text.Equals("Update"))
                    {
                        status = new DataTable();
                    }
                    if (status.Rows.Count <= 0)
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
                            //dtDeclaration.Rows[rowcount][0] = Convert.ToDateTime(dtformat);
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
                            //dtDeclaration.Rows[rowcount][1] = Convert.ToDateTime(dtformat);
                        }

                        dtDeclaration.Rows[rowcount][2] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                        dtDeclaration.Rows[rowcount][3] = Convert.ToInt32(ViewState["GeneralRaceID"]);
                        dtDeclaration.Rows[rowcount][4] = Convert.ToInt32(ViewState["GeneralRaceNameID"]);
                        dtDeclaration.Rows[rowcount][5] = Convert.ToInt32(ViewState["DivisionRaceID"]);
                        //dtDeclaration.Rows[rowcount][6] = Convert.ToInt32((row.FindControl("hdnfielHorseID") as HiddenField).Value);
                        dtDeclaration.Rows[rowcount][6] = 0;
                        dtDeclaration.Rows[rowcount][7] = Convert.ToInt32((row.FindControl("hdnfieldHorseNameIDG") as HiddenField).Value);
                        var placing = (row.FindControl("txtbxPlacing") as TextBox).Text;
                        if (placing.Equals(""))
                        {
                            goto Outer;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][8] = placing;
                        }
                        

                        if ((row.FindControl("hdnfieldProfessionalnameid") as HiddenField).Value.Equals(""))
                        {

                            dtDeclaration.Rows[rowcount][9] = DBNull.Value;
                            dtDeclaration.Rows[rowcount][18] = DBNull.Value;
                            goto Outer;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][9] = new CardsBL().GetProfessionalID(Convert.ToInt32((row.FindControl("hdnfieldProfessionalnameid") as HiddenField).Value));
                            dtDeclaration.Rows[rowcount][18] = Convert.ToInt32((row.FindControl("hdnfieldProfessionalnameid") as HiddenField).Value);
                        }


                        if ((row.FindControl("txtbxCarriedWeightG") as TextBox).Text.Equals(""))
                        {
                            // dtDeclaration.Rows[rowcount][10] = DBNull.Value;
                            goto Outer;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][10] = (row.FindControl("txtbxCarriedWeightG") as TextBox).Text;
                        }

                        if ((row.FindControl("txtbxFinishTimeMMG") as TextBox).Text.Equals(""))
                        {
                            //dtDeclaration.Rows[rowcount][11] = DBNull.Value;
                            goto Outer;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][11] = (row.FindControl("txtbxFinishTimeMMG") as TextBox).Text + ":" + (row.FindControl("txtbxFinishTimeSSG") as TextBox).Text + ":" + (row.FindControl("txtbxFinishTimeMSG") as TextBox).Text;
                        }

                        if ((row.FindControl("drpdwnVerdictMarginG") as DropDownList).SelectedItem.Value.Equals("-1"))
                        {
                            //dtDeclaration.Rows[rowcount][12] = DBNull.Value;
                            goto Outer;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][12] = Convert.ToInt32((row.FindControl("drpdwnVerdictMarginG") as DropDownList).SelectedItem.Value);
                        }

                        if ((row.FindControl("txtbxHorseBodyWeightG") as TextBox).Text.Equals(""))
                        {
                            //dtDeclaration.Rows[rowcount][13] = DBNull.Value;
                            goto Outer;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][13] = (row.FindControl("txtbxHorseBodyWeightG") as TextBox).Text.ToUpper();
                        }
                        if ((row.FindControl("txtbxRevisedRatingG") as TextBox).Text.Equals(""))
                        {
                            dtDeclaration.Rows[rowcount][14] = DBNull.Value;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][14] = (row.FindControl("txtbxRevisedRatingG") as TextBox).Text;
                        }

                        dtDeclaration.Rows[rowcount][15] = DateTime.Now;
                        dtDeclaration.Rows[rowcount][16] = 1;
                        dtDeclaration.Rows[rowcount][17] = 1;
                        if ((row.FindControl("lblJA") as Label).Text.Equals(""))
                        {
                            dtDeclaration.Rows[rowcount][19] = DBNull.Value;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][19] = (row.FindControl("lblJA") as Label).Text;
                        }

                        if ((row.FindControl("lblDeclareWeight") as Label).Text.Equals(""))
                        {
                            dtDeclaration.Rows[rowcount][20] = DBNull.Value;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][20] = (row.FindControl("lblDeclareWeight") as Label).Text;
                        }


                        if (btnAdd.Text.Equals("Add"))
                        {
                            dtDeclaration.Rows[rowcount][21] = "Insert";
                        }
                        else if (btnAdd.Text.Equals("Update"))
                        {
                            dtDeclaration.Rows[rowcount][21] = "Update";
                        }

                        
                        result = new CardsBL().AddCardResult(dtDeclaration);
                        if (btnAdd.Text.Equals("Add"))
                        {
                            result = 1;
                        }
                        else if (btnAdd.Text.Equals("Update"))
                        {
                            result = 2;
                        }
                        totalcount = rowcount;
                        dtDeclaration.Clear();
                    }
                    else
                    {
                        var message = "Record already exist.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    
                }

                

                if (result == 1)
                {
                    ClearALL();
                    ShowDeclaration();
                    var message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else if (result == 2)
                {
                    ClearALL();
                    ShowDeclaration();
                    var message = "Record updated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    var message = "Issue in Record";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                Outer:
                dtDeclaration.Clear();
                var message1 = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message1 + "');", true);
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        public void ClearALL()
        {
            btnAdd.Text = "Add";
            GvCardResult.DataSource = new DataTable();
            GvCardResult.DataBind();

            GvShowALL.DataSource = new DataTable();
            GvShowALL.DataBind();

            placingcount = 0;
            horsenocheck = 0;
            count = 1;
        }
        protected void GvCardResult_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnAdd.Text = "Update";
                GridViewRow row = GvCardResult.SelectedRow;
                var dataKey = GvCardResult.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    HiddenField hdnfieldDivisionRaceIDG1 = (HiddenField)row.FindControl("hdnfieldDivisionRaceIDG1");
                    HiddenField hdnfieldHorseNameIDG1 = (HiddenField)row.FindControl("hdnfieldHorseNameIDG1");
                    var ds = new CardsBL().GetResultselectedRowforUpdate(Convert.ToInt32(dataKey.Value), 
                                            Convert.ToInt32(hdnfieldDivisionRaceIDG1.Value), Convert.ToInt32(hdnfieldHorseNameIDG1.Value));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GvShowALL.DataSource = ds.Tables[0];
                        GvShowALL.DataBind();
                    }
                    else
                    {
                        GvShowALL.DataSource = new DataTable();
                        GvShowALL.DataBind();
                    }
                }

                }
            catch(Exception ex)
            {
                btnAdd.Text = "Add";
            }
        }
        protected void GvShowALL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try{

                GridViewRow row = GvShowALL.SelectedRow;
                var dataKey = GvShowALL.DataKeys[row.RowIndex];
                var rownumber = string.Empty;
                var professionalnameid = 0;
                var drawno = string.Empty;
                int status = 0;
                var emgercencycolor = string.Empty;
                var errorcheck= 0;

                if (dataKey != null)
                {

                    TextBox txtbxPlacing = (TextBox)row.FindControl("txtbxPlacing");
                    if (txtbxPlacing.Text.Equals(""))
                    {
                        errorcheck = 1;
                        goto Outer1;

                    }

                    HiddenField hdnfieldProfessionalnameid = (HiddenField)row.FindControl("hdnfieldProfessionalnameid");
                    if (hdnfieldProfessionalnameid.Value.Equals(""))
                    {
                        errorcheck = 1;
                        goto Outer1;
                    }
                    else
                    {
                        professionalnameid = Convert.ToInt32(hdnfieldProfessionalnameid.Value);
                    }
                    TextBox txtbxCarriedWeightG = (TextBox)row.FindControl("txtbxCarriedWeightG");
                    var carriedweight = string.Empty;
                    if (txtbxCarriedWeightG.Text.Equals(""))
                    {
                        errorcheck = 1;
                        goto Outer1;
                    }
                    else
                    {
                        carriedweight = txtbxCarriedWeightG.Text;
                    }

                    
                    TextBox txtbxMM = (TextBox)row.FindControl("txtbxFinishTimeMMG");
                    TextBox txtbxSS = (TextBox)row.FindControl("txtbxFinishTimeSSG");
                    TextBox txtbxMS = (TextBox)row.FindControl("txtbxFinishTimeMSG");

                    var timeformat = txtbxMM.Text + ":" + txtbxSS.Text + ":" + txtbxMS.Text;
                    //var min=string.Empty;
                    //if (txtbxMM.Text.Length == 1)
                    //{
                    //    min = "0" + txtbxMM.Text;
                    //}
                    //else
                    //{
                    //    min = txtbxMM.Text;
                    //}
                    //var sec = string.Empty;
                    //if (txtbxSS.Text.Length == 1)
                    //{
                    //    sec = "0" + txtbxSS.Text;
                    //}
                    //else
                    //{
                    //    sec = txtbxSS.Text;
                    //}
                    //var pulse = string.Empty;
                    //if (txtbxMS.Text.Length == 1)
                    //{
                    //    pulse = txtbxMS.Text + "00";
                    //}
                    //else if (txtbxMS.Text.Length == 2)
                    //{
                    //    pulse = txtbxMS.Text + "0";
                    //}
                    //else
                    //{
                    //    pulse = txtbxMS.Text;
                    //}
                    // var timeformat = sec + ":" + pulse;
                    DropDownList drpdwnVerdictMarginG = (DropDownList)row.FindControl("drpdwnVerdictMarginG");
                    var drpdwnVerdictMargin = 0;
                    if (drpdwnVerdictMarginG.SelectedItem.Value.Equals("-1"))
                    {
                        errorcheck = 1;
                        goto Outer1;
                    }
                    else
                    {
                        drpdwnVerdictMargin = Convert.ToInt32(drpdwnVerdictMarginG.SelectedItem.Value);
                    }
                    TextBox txtbxHorseBodyWeightG = (TextBox)row.FindControl("txtbxHorseBodyWeightG");
                    Label lblJAG = (Label)row.FindControl("lblJA");
                    Label lblDeclareWeightG = (Label)row.FindControl("lblDeclareWeight");
                    var horsebodyweight = string.Empty;
                    if (txtbxHorseBodyWeightG.Text.Equals(""))
                    {
                        errorcheck = 1;
                        goto Outer1;
                       
                       
                    }
                    else
                    {
                        horsebodyweight = txtbxHorseBodyWeightG.Text.ToUpper();
                    }

                    TextBox txtbxRevisedRatingG = (TextBox)row.FindControl("txtbxRevisedRatingG");
                    status = new CardsBL().CardResultUpdate(Convert.ToInt32(dataKey.Value), txtbxPlacing.Text,
                                    Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                                carriedweight, timeformat,
                                                drpdwnVerdictMargin, horsebodyweight,
                                                txtbxRevisedRatingG.Text, lblJAG.Text, lblDeclareWeightG.Text);

                Outer1:
                    if (errorcheck == 1)
                    {
                        string message = "Issue in Record.";
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }

                if (status == 2)
                {
                    ShowDeclaration();
                    string message = "Record updated successfully.";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                //Outer1:
                    string message = "Issue in Record.";
                    //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

               
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearALL();
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_Result"))
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

        protected void GvCardResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                Label lblCarriedWeight = (Label)e.Row.FindControl("lblCarriedWeight");
                Label lblDeclaredWeight = (Label)e.Row.FindControl("lblDeclaredWeight");

                if (Convert.ToDecimal(lblCarriedWeight.Text) > Convert.ToDecimal(lblDeclaredWeight.Text))
                {
                    lblCarriedWeight.ForeColor = Color.Red;
                    lblCarriedWeight.Font.Bold = true;
                }
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

                var dtresult = new DataTable();
                var id = string.Empty;
                var rowcount = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int count = 0; count < dt.Rows.Count; count++)
                    {
                        rowcount = rowcount + 1;
                        id = dt.Rows[count][0].ToString();
                        if (!(string.IsNullOrEmpty(id)))
                        {


                            dtresult = new CardsBL().ImportCardFiles(dt.Rows[count][1].ToString(), dt.Rows[count][2].ToString(), dt.Rows[count][3].ToString(), dt.Rows[count][4].ToString(),
                                 dt.Rows[count][5].ToString(), dt.Rows[count][6].ToString(), dt.Rows[count][7].ToString(), dt.Rows[count][8].ToString(), dt.Rows[count][9].ToString(),
                                 dt.Rows[count][10].ToString(), dt.Rows[count][11].ToString(), dt.Rows[count][12].ToString(), dt.Rows[count][13].ToString(), dt.Rows[count][14].ToString(),
                                 dt.Rows[count][15].ToString(), dt.Rows[count][16].ToString(), dt.Rows[count][17].ToString(), dt.Rows[count][18].ToString(), dt.Rows[count][19].ToString(),
                                 dt.Rows[count][20].ToString(), dt.Rows[count][21].ToString(), dt.Rows[count][22].ToString(), dt.Rows[count][23].ToString(), dt.Rows[count][24].ToString(),
                                 dt.Rows[count][25].ToString(), dt.Rows[count][26].ToString(), dt.Rows[count][27].ToString(), dt.Rows[count][28].ToString(), dt.Rows[count][29].ToString(),
                                 dt.Rows[count][30].ToString(), dt.Rows[count][31].ToString(), "Card_Result");


                            if (dtresult.Rows.Count > 0)
                            {
                                if (dtresult.Rows[0][0].Equals("9"))
                                {
                                    var message = "Issue in Row No: " + id + "<br />" + "Column Detail: " + dtresult.Rows[0][1]
                                        + "<br />" + "Table Name: " + dtresult.Rows[0][2];
                                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                                    break;
                                }
                            }
                        }
                    }

                    if (rowcount.Equals(dt.Rows.Count))
                    {
                        var message1 = "All Record has been added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message1 + "');", true);
                    }

                    //if (dt.Rows.Count > 0)
                    //{
                    //    //var dtErrorResult = new CardsBL().Import30(dt, "Card_RaceCard", 0);
                    //    var dtErrorResult = new CardsBL().Import30(dt, "Card_Result", 0);
                    //    if (dtErrorResult.Rows.Count > 0)
                    //    {
                    //        using (ExcelPackage xp = new ExcelPackage())
                    //        {

                    //            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Result");

                    //            int rowstart = 1;
                    //            int colstart = 1;
                    //            int rowend = rowstart;
                    //            int colend = colstart + (dtErrorResult.Columns.Count - 1);
                    //            //  int colend = colstart;
                    //            rowend = rowstart + dtErrorResult.Rows.Count;
                    //            ws.Cells[rowstart, colstart].LoadFromDataTable(dtErrorResult, true);
                    //            int i = 1;
                    //            foreach (DataColumn dc in dtErrorResult.Columns)
                    //            {
                    //                i++;
                    //                if (dc.DataType == typeof(decimal)) ws.Column(i).Style.Numberformat.Format = "#0.00";
                    //            }
                    //            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                    //            ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                    //                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                    //                    ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                    //                        ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style =
                    //                            OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    //            Response.AddHeader("content-disposition", "attachment;filename=Card_Result.xlsx");
                    //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //            Response.BinaryWrite(xp.GetAsByteArray());
                    //            Response.End();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //BindData();
                    //        var message1 = "All Record has been added successfully.";
                    //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message1 + "');", true);
                    //    }
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
                using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "Card_Result"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Result");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Result.xlsx");
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