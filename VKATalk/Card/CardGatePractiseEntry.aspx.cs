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

    public partial class CardGatePractiseEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxHandicapEnterDate.Text = CommonMethods.CurrentDate();
                BindDropDown(drpdwnCenterName, "Center", "CenterName", "ID");
                drpdwnCenterName.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnDistance, "Distance", "Distance", "DistanceID");
                drpdwnDistance.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnTrack, "MasterTrack", "TrackName", "MasterTrackID");
                drpdwnTrack.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                
                
                BindDropDown(drpdwnVerdictMargin, "VerdictMargin", "VerdictMargin", "VerdictMarginID");
                drpdwnVerdictMargin.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                BindDropDown(drpdwnDistaceBreakup, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup.Items.Insert(0, new ListItem("-- Please select --", "-1"));
            }
        }


        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = new CardsBL().GetDropdownBind(TableName_);
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
        public static List<string> AddSourceNameList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CommentatorList", prefixText);

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
        public static List<string> AddHorseNameList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CardHorseName", prefixText);

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
        public static List<string> AddHorseNameList1(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CardHorseNameEquipment", prefixText);

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
        public static List<string> AddRiderList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CardRiderList", prefixText);

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
        public static List<string> AddCommentsList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CardMockRaceCommentList", prefixText);

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
        public static List<string> AddCommentsList1(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CardMockRaceCommentList1", prefixText);

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
        public static List<string> AddCommentsList2(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CardMockRaceCommentList1", prefixText);

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
            try
            {

                DataTable dt = null;
                int entrytype = 0;

                if (btnMockRaceAdd.Text.Equals("Add"))
                {
                    dt = new CardsBL().GatePracticeEntry(0, txtbxHandicapEnterDate.Text, Convert.ToInt32(hdnfieldProfessionalnameid.Value),txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),  
                        Convert.ToInt32(drpdwnLotNo.SelectedItem.Value),Convert.ToInt32(drpdwnDistance.SelectedItem.Value),Convert.ToInt32(drpdwnTrack.SelectedItem.Value),1, "Insert");
                }
                else
                {
                    dt = new CardsBL().GatePracticeEntry((int)ViewState["MockRaceCID"], txtbxHandicapEnterDate.Text, Convert.ToInt32(hdnfieldProfessionalnameid.Value),txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),  
                        Convert.ToInt32(drpdwnLotNo.SelectedItem.Value),Convert.ToInt32(drpdwnDistance.SelectedItem.Value),Convert.ToInt32(drpdwnTrack.SelectedItem.Value),1, "Update");

                    btnMockRaceAdd.Text = "Add";
                }
                ClearAll("MockRaceEntry");
                if (dt.Rows.Count > 0)
                {
                    GvMockRace.DataSource = dt;
                    GvMockRace.DataBind();
                }
                else
                {
                    GvMockRace.DataSource = new DataTable();
                    GvMockRace.DataBind();
                }
                //ClearAll();
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        protected void btnHorseAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                int entrytype = 0;

                if (btnHorseAdd.Text.Equals("Add"))
                {
                    dt = new CardsBL().GatePracticeHorseEntry(0, Convert.ToInt32(hdnfieldMockRaceID.Value), txtbxHandicapEnterDate.Text, Convert.ToInt32(txtbxPlacing.Text),
                          0,Convert.ToInt32(hdnfieldHorseNameID.Value),Convert.ToInt32(hdnfieldRiderNameID.Value),
                         0,(txtbxMM.Text.Equals("")) ? "" : (Convert.ToInt32(txtbxMM.Text) + ":" + Convert.ToInt32(txtbxSS.Text) + ":" + +Convert.ToInt32(txtbxPPP.Text)),
                         Convert.ToInt32(drpdwnVerdictMargin.SelectedItem.Value),txtbxComment.Text,1,"Insert");
                }
                else
                {
                    dt = new CardsBL().GatePracticeHorseEntry((int)ViewState["MRParticipatingHorsesCID"], Convert.ToInt32(hdnfieldMockRaceID.Value), txtbxHandicapEnterDate.Text, Convert.ToInt32(txtbxPlacing.Text),
                          0, Convert.ToInt32(hdnfieldHorseNameID.Value), Convert.ToInt32(hdnfieldRiderNameID.Value),
                         0,(txtbxMM.Text.Equals("")) ? "" : (Convert.ToInt32(txtbxMM.Text) + ":" + Convert.ToInt32(txtbxSS.Text) + ":" + +Convert.ToInt32(txtbxPPP.Text)),
                         Convert.ToInt32(drpdwnVerdictMargin.SelectedItem.Value), txtbxComment.Text, 1, "Update");
                    btnHorseAdd.Text = "Add";
                }
                ClearAll("MockRaceHorse");
                if (dt.Rows.Count > 0)
                {
                    GrdViewHorseEntry.DataSource = dt;
                    GrdViewHorseEntry.DataBind();
                    txtbxPlacing.Focus();
                }
                else
                {
                    GrdViewHorseEntry.DataSource = new DataTable();
                    GrdViewHorseEntry.DataBind();
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



        protected void btnDeleteHorse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                int entrytype = 0;

                dt = new CardsBL().GatePracticeHorseEntry((int)ViewState["MRParticipatingHorsesCID"], Convert.ToInt32(hdnfieldMockRaceID.Value), txtbxHandicapEnterDate.Text, Convert.ToInt32(txtbxPlacing.Text),
                          0,Convert.ToInt32(hdnfieldHorseNameID.Value), Convert.ToInt32(hdnfieldRiderNameID.Value),
                         0,(txtbxMM.Text.Equals("")) ? "" : (Convert.ToInt32(txtbxMM.Text) + ":" + Convert.ToInt32(txtbxSS.Text) + ":" + +Convert.ToInt32(txtbxPPP.Text)),
                         Convert.ToInt32(drpdwnVerdictMargin.SelectedItem.Value), txtbxComment.Text, 1, "Delete");
                btnHorseAdd.Text = "Add";
                ClearAll("MockRaceHorse");
                if (dt.Rows.Count > 0)
                {
                    GrdViewHorseEntry.DataSource = dt;
                    GrdViewHorseEntry.DataBind();
                    txtbxPlacing.Focus();
                }
                else
                {
                    GrdViewHorseEntry.DataSource = new DataTable();
                    GrdViewHorseEntry.DataBind();
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
        

        protected void btnMockRaceEntryDelete_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = null;
                int entrytype = 0;
                //dt = new CardsBL().MockRaceEntry(0, txtbxHandicapEnterDate.Text, txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldProfessionalnameid.Value), Convert.ToInt32(txtbxMockRaceNo.Text), Convert.ToInt32(drpdwnDistance.SelectedItem.Value), 1, "Delete");
                dt = new CardsBL().GatePracticeEntry((int)ViewState["MockRaceCID"], txtbxHandicapEnterDate.Text, Convert.ToInt32(hdnfieldProfessionalnameid.Value),txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),  
                        Convert.ToInt32(drpdwnLotNo.SelectedItem.Value),Convert.ToInt32(drpdwnDistance.SelectedItem.Value),Convert.ToInt32(drpdwnTrack.SelectedItem.Value),1, "Delete");
                if (dt.Rows.Count > 0)
                {
                    GvMockRace.DataSource = dt;
                    GvMockRace.DataBind();
                }
                else
                {
                    GvMockRace.DataSource = new DataTable();
                    GvMockRace.DataBind();
                }
                //ClearAll();
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }
        
        protected void btnMockRaceEntryClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll("MockRaceEntry");
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        private void ClearAll(string section)
        {
            if (section.Equals("MockRaceEntry"))
            {
                txtbxHandicapEnterDate.Text = string.Empty;
                txtbxRaceDate.Text = string.Empty;
                drpdwnCenterName.ClearSelection();
                drpdwnDistance.ClearSelection();
                drpdwnTrack.ClearSelection();
                drpdwnLotNo.ClearSelection();
                txtbxSourceName.Text = string.Empty;
                //txtbxMockRaceNo.Text = string.Empty;
            }
            else if (section.Equals("MockRaceHorse"))
            {
                txtbxPlacing.Text = string.Empty;
                txtbxHorseName.Text = string.Empty;
                hdnfieldHorseNameID.Value = string.Empty;
                txtbxRiderName.Text = string.Empty;
                hdnfieldRiderNameID.Value = string.Empty;
                txtbxMM.Text = string.Empty;
                txtbxSS.Text = string.Empty;
                txtbxPPP.Text = string.Empty;
                drpdwnVerdictMargin.ClearSelection();
                txtbxComment.Text = string.Empty;
            }
            else if (section.Equals("MockRaceBreakUp"))
            {
                txtbxSourceName2.Text = string.Empty;
                hdnfieldSourceName2.Value = string.Empty;
                txtbxMM1.Text = string.Empty;
                txtbxSS1.Text = string.Empty;
                drpdwnDistaceBreakup.ClearSelection();
                txtbxComments2.Text = string.Empty;
            }
            else if (section.Equals("MockRaceIndividual"))
            {
                txtbxIndividualSource.Text = string.Empty;
                hdnfieldSourceIndividualID.Value = string.Empty;
                txtbxHorseNameIndividual.Text = string.Empty;
                hdnfieldHorseNameIDIndividual.Value = string.Empty;
                txtbxIndividualcomment.Text = string.Empty;
            }
        }

        protected void drpdwnDistance_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                int entrytype = 0;
                //dt = new CardsBL().MockRaceEntry(0, txtbxHandicapEnterDate.Text, txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldProfessionalnameid.Value), Convert.ToInt32(txtbxMockRaceNo.Text), Convert.ToInt32(drpdwnDistance.SelectedItem.Value), 1, "Select");
                dt = new CardsBL().GatePracticeEntry(0, txtbxHandicapEnterDate.Text, Convert.ToInt32(hdnfieldProfessionalnameid.Value),txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),  
                        Convert.ToInt32(drpdwnLotNo.SelectedItem.Value),Convert.ToInt32(drpdwnDistance.SelectedItem.Value),Convert.ToInt32(drpdwnTrack.SelectedItem.Value),1, "Select");
                if (dt.Rows.Count > 0)
                {
                    GvMockRace.DataSource = dt;
                    GvMockRace.DataBind();
                }
                else
                {
                    GvMockRace.DataSource = new DataTable();
                    GvMockRace.DataBind();
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

        protected void GvMockRace_OnSelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "MockRaceNo")
                {
                    int index = Convert.ToInt32(e.CommandArgument.ToString());
                    GridViewRow row = GvMockRace.Rows[index];
                    var dataKey = GvMockRace.DataKeys[row.RowIndex];
                    var rownumber = string.Empty;
                    if (dataKey != null)
                    {
                        ViewState["MockRaceCID"] = dataKey.Value;

                        HiddenField hdnfieldProfessionalNameidG = (HiddenField)row.FindControl("hdnfieldProfessionalNameidG");
                        hdnfieldProfessionalnameid.Value = hdnfieldProfessionalNameidG.Value;
                        txtbxSourceName.Text = row.Cells[0].Text;
                        txtbxRaceDate.Text = row.Cells[1].Text;
                        drpdwnCenterName.ClearSelection();
                        drpdwnCenterName.Items.FindByText(row.Cells[2].Text).Selected = true;
                        drpdwnLotNo.ClearSelection();
                        HiddenField hdnfieldGatePracticeLotNo = (HiddenField)row.FindControl("hdnfieldGatePracticeLotNo");
                        drpdwnLotNo.Items.FindByText(hdnfieldGatePracticeLotNo.Value).Selected = true;

                        drpdwnDistance.ClearSelection();
                        HiddenField hdnfieldDistance = (HiddenField)row.FindControl("hdnfieldDistance");
                        drpdwnDistance.Items.FindByText(hdnfieldDistance.Value).Selected = true;
                        drpdwnTrack.ClearSelection();
                        drpdwnTrack.Items.FindByText(row.Cells[5].Text).Selected = true;

                        txtbxHandicapEnterDate.Text = DateTime.Now.ToString();
                        btnMockRaceAdd.Text = "Update";

                    }
                }
                else if (e.CommandName == "Distance")
                {
                    int index = Convert.ToInt32(e.CommandArgument.ToString());
                    GridViewRow row = GvMockRace.Rows[index];
                    var dataKey = GvMockRace.DataKeys[row.RowIndex];
                    var rownumber = string.Empty;
                    if (dataKey != null)
                    {
                        HiddenField hdnfieldMockRaceNo = (HiddenField)row.FindControl("hdnfieldGatePracticeLotNo");
                        HiddenField hdnfieldDistance = (HiddenField)row.FindControl("hdnfieldDistance");
                        lblHorseParticipatingEntry.Text = "Gate Practice Lot No- " + hdnfieldMockRaceNo.Value + " (" + hdnfieldDistance.Value + ")";
                        lblDistanceBreakUpEntry.Text = "Gate Practice Lot No- " + hdnfieldMockRaceNo.Value + " (" + hdnfieldDistance.Value + ")";
                        //lblMockRaceEquipment.Text = "Gate Practice Lot No- " + hdnfieldMockRaceNo.Value + " (" + hdnfieldDistance.Value + ")";
                        lblIndividualHorse.Text = "Gate Practice Lot No- " + hdnfieldMockRaceNo.Value + " (" + hdnfieldDistance.Value + ")";
                        hdnfieldMockRaceID.Value = dataKey.Value.ToString();
                        ShowAllGridview();
                    }
                }
              
            }
            catch (Exception ex)
            {
                btnMockRaceAdd.Text = "Add";
                //hdnfieldhorseid.Value = "";
                //txtbxHorseName.Text = "";
                //txtbxTrainer.Text = "";
                //txtbxOwner.Text = "";
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        private void ShowAllGridview()
        {
            try
            {
                DataTable dt = null;
                DataTable dt1 = null;
                DataTable dt2 = null;
                DataTable dt3 = null;
                var time = string.Empty;

                dt = new CardsBL().GatePracticeHorseEntry(0, Convert.ToInt32(hdnfieldMockRaceID.Value), txtbxHandicapEnterDate.Text, 0,
                         0,0, 0,0,
                        (txtbxMM.Text.Equals("")) ? "" : (Convert.ToInt32(txtbxMM.Text) + ":" + Convert.ToInt32(txtbxSS.Text) + ":" + +Convert.ToInt32(txtbxPPP.Text)),
                        0, "", 1, "Select");
                if (dt.Rows.Count > 0)
                {
                    GrdViewHorseEntry.DataSource = dt;
                    GrdViewHorseEntry.DataBind();
                    txtbxPlacing.Focus();
                }
                else
                {
                    GrdViewHorseEntry.DataSource = new DataTable();
                    GrdViewHorseEntry.DataBind();
                }

               
                if (!(txtbxMM1.Text.Equals("") && txtbxSS1.Text.Equals("")))
                {
                    time = txtbxMM1.Text + ":" + txtbxSS1.Text;
                }
                dt1 = new CardsBL().GatePracticeDistanceBreakUp(0, Convert.ToInt32(hdnfieldMockRaceID.Value), (hdnfieldSourceName2.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldSourceName2.Value),
                         Convert.ToInt32(drpdwnDistaceBreakup.SelectedItem.Value),
                         time, txtbxComments2.Text, 1, "Select");
                if (dt1.Rows.Count > 0)
                {
                    GvDistanceBreakUp.DataSource = dt1;
                    GvDistanceBreakUp.DataBind();
                    txtbxSourceName2.Focus();
                }
                else
                {
                    GvDistanceBreakUp.DataSource = new DataTable();
                    GvDistanceBreakUp.DataBind();
                }



                dt2 = new CardsBL().GetPracticeIndividual(0, Convert.ToInt32(hdnfieldMockRaceID.Value),
                    (hdnfieldSourceIndividualID.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldSourceIndividualID.Value),
                    (hdnfieldHorseNameIDIndividual.Value.Equals(""))? 0 : Convert.ToInt32(hdnfieldHorseNameIDIndividual.Value),
                         txtbxIndividualcomment.Text,
                         1, "Select");

                if (dt2.Rows.Count > 0)
                {
                    GvIndividual.DataSource = dt2;
                    GvIndividual.DataBind();
                    txtbxIndividualSource.Focus();
                }
                else
                {
                    GvIndividual.DataSource = new DataTable();
                    GvIndividual.DataBind();
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
        protected void GrdViewHorseEntry_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GrdViewHorseEntry.SelectedRow;
                var dataKey = GrdViewHorseEntry.DataKeys[row.RowIndex];
                var rownumber = string.Empty;
                if (dataKey != null)
                {
                    ViewState["MRParticipatingHorsesCID"] = dataKey.Value;
                    HiddenField hdnfieldPlacing = (HiddenField)row.FindControl("hdnfieldPlacing");
                    txtbxPlacing.Text = hdnfieldPlacing.Value;

                    HiddenField hdnfieldHorseNameID2 = (HiddenField)row.FindControl("hdnfieldHorseNameID2");
                    hdnfieldHorseNameID.Value = hdnfieldHorseNameID2.Value;
                    txtbxHorseName.Text = row.Cells[1].Text;

                    HiddenField hdnfieldRiderNameID1 = (HiddenField)row.FindControl("hdnfieldRiderNameID");
                    hdnfieldRiderNameID.Value = hdnfieldRiderNameID1.Value;
                    
                    txtbxRiderName.Text = row.Cells[2].Text;

                    if (!row.Cells[3].Text.Equals("&nbsp;"))
                    {
                        string[] time = row.Cells[3].Text.Split(':');
                        if (time.Length == 3)
                        {
                            txtbxMM.Text = time[0];
                            txtbxSS.Text = time[1];
                            txtbxPPP.Text = time[2];
                        }
                        else if (time.Length == 2)
                        {
                            txtbxMM.Text = time[0];
                            txtbxSS.Text = time[1];
                        }
                        else if (time.Length == 1)
                        {
                            txtbxMM.Text = time[0];
                        }
                    }

                    drpdwnVerdictMargin.ClearSelection();
                    HiddenField hdnfieldVerdictMarginID = (HiddenField)row.FindControl("hdnfieldVerdictMarginID");
                    drpdwnVerdictMargin.Items.FindByText(row.Cells[4].Text).Selected = true;

                    if (!row.Cells[5].Text.Equals("&nbsp;"))
                    {
                        txtbxComment.Text = row.Cells[5].Text;
                    }
                    btnHorseAdd.Text = "Update";

                }
            }
            catch (Exception ex)
            {
                btnMockRaceAdd.Text = "Add";
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }



        protected void btnClearHorse_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll("MockRaceHorse");
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void GvDistanceBreakUp_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GvDistanceBreakUp.SelectedRow;
                var dataKey = GvDistanceBreakUp.DataKeys[row.RowIndex];
                var rownumber = string.Empty;
                if (dataKey != null)
                {
                    ViewState["MRDistanceBreakUpCID"] = dataKey.Value;
                    HiddenField hdnfieldSourceName = (HiddenField)row.FindControl("hdnfieldSourceName");
                    txtbxSourceName2.Text = hdnfieldSourceName.Value;

                    drpdwnDistaceBreakup.ClearSelection();
                    drpdwnDistaceBreakup.Items.FindByText(row.Cells[1].Text).Selected = true;

                    if (!row.Cells[2].Text.Equals("&nbsp;"))
                    {
                        string[] time = row.Cells[2].Text.Split(':');
                        if (time.Length == 2)
                        {
                            txtbxMM1.Text = time[0];
                            txtbxSS1.Text = time[1];
                        }
                        else if (time.Length == 1)
                        {
                            txtbxMM1.Text = time[0];
                        }
                    }

                    if (!row.Cells[3].Text.Equals("&nbsp;"))
                    {
                        txtbxComments2.Text = row.Cells[3].Text;
                    }
                    btnHorseAdd.Text = "Update";

                }
            }
            catch (Exception ex)
            {
                btnMockRaceAdd.Text = "Add";
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnBreakUpClear_Click(object sender, EventArgs e)
        {
            ClearAll("MockRaceBreakUp");
         }
        //protected void btnDistanceBreakupShow_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dt = null;
        //        var time = string.Empty;
        //        if (!(txtbxMM1.Text.Equals("") && txtbxSS1.Text.Equals("")))
        //        {
        //            time=txtbxMM1.Text + ":" + txtbxSS1.Text;
        //        }
        //        dt = new CardsBL().MockRaceDistanceBreakUp(0, Convert.ToInt32(hdnfieldMockRaceID.Value),(hdnfieldSourceName2.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldSourceName2.Value),
        //                 Convert.ToInt32(drpdwnDistaceBreakup.SelectedItem.Value),
        //                 time , txtbxComments2.Text, 1, "Select");
        //        if (dt.Rows.Count > 0)
        //        {
        //            GvDistanceBreakUp.DataSource = dt;
        //            GvDistanceBreakUp.DataBind();
        //            txtbxSourceName2.Focus();
        //        }
        //        else
        //        {
        //            GvDistanceBreakUp.DataSource = new DataTable();
        //            GvDistanceBreakUp.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //listPlacement.Visible = false;
        //        ErrorHandling.SendErrorToText(ex);
        //        var message = "Incorrect Information.";
        //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        //    }
        //}

        protected void btnDistanceBreakupAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                int entrytype = 0;

                if (btnHorseAdd.Text.Equals("Add"))
                {
                    dt = new CardsBL().GatePracticeDistanceBreakUp(0, Convert.ToInt32(hdnfieldMockRaceID.Value), Convert.ToInt32(hdnfieldSourceName2.Value),
                         Convert.ToInt32(drpdwnDistaceBreakup.SelectedItem.Value),
                         (txtbxMM1.Text + ":" + txtbxSS1.Text), txtbxComments2.Text, 1, "Insert");
                }
                else
                {
                    dt = new CardsBL().GatePracticeDistanceBreakUp((int)ViewState["MRDistanceBreakUpCID"], Convert.ToInt32(hdnfieldMockRaceID.Value), Convert.ToInt32(hdnfieldSourceName2.Value),
                         Convert.ToInt32(drpdwnDistaceBreakup.SelectedItem.Value),
                         (txtbxMM1.Text + ":" + txtbxSS1.Text), txtbxComments2.Text, 1, "Update");
                    btnHorseAdd.Text = "Add";
                }
                ClearAll("MockRaceBreakUp");
                if (dt.Rows.Count > 0)
                {
                    GvDistanceBreakUp.DataSource = dt;
                    GvDistanceBreakUp.DataBind();
                    txtbxSourceName2.Focus();
                }
                else
                {
                    GvDistanceBreakUp.DataSource = new DataTable();
                    GvDistanceBreakUp.DataBind();
                }
                //ClearAll();
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        protected void btnBreakUpDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                int entrytype = 0;

                dt = new CardsBL().GatePracticeDistanceBreakUp((int)ViewState["MRDistanceBreakUpCID"], Convert.ToInt32(hdnfieldMockRaceID.Value), Convert.ToInt32(hdnfieldSourceName2.Value),
                         Convert.ToInt32(drpdwnDistaceBreakup.SelectedItem.Value),
                         (txtbxMM1.Text + ":" + txtbxSS1.Text), txtbxComments2.Text, 1, "Delete");
                ClearAll("MockRaceBreakUp");
                if (dt.Rows.Count > 0)
                {
                    GvDistanceBreakUp.DataSource = dt;
                    GvDistanceBreakUp.DataBind();
                    txtbxSourceName2.Focus();
                }
                else
                {
                    GvDistanceBreakUp.DataSource = new DataTable();
                    GvDistanceBreakUp.DataBind();
                }
                //ClearAll();
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void btnAddIndividual_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                int entrytype = 0;

                if (btnAddIndividual.Text.Equals("Add"))
                {
                    dt = new CardsBL().GetPracticeIndividual(0, Convert.ToInt32(hdnfieldMockRaceID.Value), Convert.ToInt32(hdnfieldSourceIndividualID.Value),
                         Convert.ToInt32(hdnfieldHorseNameIDIndividual.Value),
                         txtbxIndividualcomment.Text,
                         1, "Insert");


                    txtbxIndividualcomment.Text = string.Empty;
                    txtbxIndividualSource.Text = string.Empty;
                    txtbxHorseNameIndividual.Text = string.Empty;
                    hdnfieldHorseNameIDIndividual.Value = string.Empty;
                    hdnfieldSourceIndividualID.Value = string.Empty;
                }
                else
                {
                    dt = new CardsBL().GetPracticeIndividual((int)ViewState["MRIHCommentCID"], Convert.ToInt32(hdnfieldMockRaceID.Value), Convert.ToInt32(hdnfieldSourceIndividualID.Value),
                         Convert.ToInt32(hdnfieldHorseNameIDIndividual.Value),
                         txtbxIndividualcomment.Text,
                         1, "Update");

                    txtbxIndividualcomment.Text = string.Empty;
                    txtbxIndividualSource.Text = string.Empty;
                    txtbxHorseNameIndividual.Text = string.Empty;
                    hdnfieldHorseNameIDIndividual.Value = string.Empty;
                    hdnfieldSourceIndividualID.Value = string.Empty;
                    btnAddIndividual.Text = "Add";
                }
                if (dt.Rows.Count > 0)
                {
                    GvIndividual.DataSource = dt;
                    GvIndividual.DataBind();
                    txtbxIndividualSource.Focus();
                }
                else
                {
                    GvIndividual.DataSource = new DataTable();
                    GvIndividual.DataBind();
                }
                //ClearAll();
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void btnIndividualDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                int entrytype = 0;

                dt = new CardsBL().GetPracticeIndividual((int)ViewState["MRIHCommentCID"], Convert.ToInt32(hdnfieldMockRaceID.Value), Convert.ToInt32(hdnfieldSourceIndividualID.Value),
                          Convert.ToInt32(hdnfieldHorseNameIDIndividual.Value),
                          txtbxIndividualcomment.Text,
                          1, "Delete");
                ClearAll("MockRaceIndividual");
                if (dt.Rows.Count > 0)
                {
                    GvIndividual.DataSource = dt;
                    GvIndividual.DataBind();
                    txtbxIndividualSource.Focus();
                }
                else
                {
                    GvIndividual.DataSource = new DataTable();
                    GvIndividual.DataBind();
                }
                //ClearAll();
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void btnIndividualClear_Click(object sender, EventArgs e)
        {
            ClearAll("MockRaceIndividual");
        }	

        //protected void btnShowIndividual_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dt = null;
        //        dt = new CardsBL().MockRaceIndividual(0, Convert.ToInt32(hdnfieldMockRaceID.Value), Convert.ToInt32(hdnfieldSourceIndividualID.Value),
        //                 Convert.ToInt32(hdnfieldHorseNameIDIndividual.Value),
        //                 txtbxIndividualcomment.Text,
        //                 1, "Show");

        //        txtbxIndividualcomment.Text = string.Empty;
        //            txtbxIndividualSource.Text = string.Empty;
        //            txtbxHorseNameIndividual.Text = string.Empty;
        //            hdnfieldHorseNameIDIndividual.Value = string.Empty;
        //            hdnfieldSourceIndividualID.Value = string.Empty;
        //            btnAddIndividual.Text = "Add";
        //        if (dt.Rows.Count > 0)
        //        {
        //            GvIndividual.DataSource = dt;
        //            GvIndividual.DataBind();
        //            txtbxIndividualSource.Focus();
        //        }
        //        else
        //        {
        //            GvIndividual.DataSource = new DataTable();
        //            GvIndividual.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //listPlacement.Visible = false;
        //        ErrorHandling.SendErrorToText(ex);
        //        var message = "Incorrect Information.";
        //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        //    }
        //}


        protected void GvIndividual_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GvIndividual.SelectedRow;
                var dataKey = GvIndividual.DataKeys[row.RowIndex];
                var rownumber = string.Empty;
                if (dataKey != null)
                {
                    ViewState["MRIHCommentCID"] = dataKey.Value;

                    HiddenField hdnfieldIndividualSourcePNameIDG = (HiddenField)row.FindControl("hdnfieldIndividualSourcePNameIDG");
                    HiddenField hdnfieldSourceName = (HiddenField)row.FindControl("hdnfieldIndividualSourceName");
                    hdnfieldSourceIndividualID.Value = hdnfieldIndividualSourcePNameIDG.Value;
                    txtbxIndividualSource.Text = hdnfieldSourceName.Value;


                    HiddenField hdnfieldIndividualHorseNameIDG = (HiddenField)row.FindControl("hdnfieldIndividualHorseNameIDG");
                    hdnfieldHorseNameIDIndividual.Value = hdnfieldIndividualHorseNameIDG.Value;
                    txtbxHorseNameIndividual.Text = row.Cells[1].Text;

                    txtbxIndividualcomment.Text = row.Cells[2].Text;


                    btnAddIndividual.Text = "Update";

                }
            }
            catch (Exception ex)
            {
                btnAddIndividual.Text = "Add";
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

    }
}