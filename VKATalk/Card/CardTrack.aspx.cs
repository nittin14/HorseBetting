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

    public partial class CardTrack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxHandicapEnterDate.Text = CommonMethods.CurrentDate();
                txtbxentrydatedisplay.Text= CommonMethods.CurrentDate();
                BindDropDown(drpdwnCenterName, "Center", "CenterName", "ID");
                drpdwnCenterName.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                BindDropDown(drpdwnCenterDisplay, "Center", "CenterName", "ID");
                drpdwnCenterDisplay.Items.Insert(0, new ListItem("-- Please select --", "-1"));


                BindDropDown(drpdwnTrack, "MasterTrack", "TrackAlias", "MasterTrackID");
                drpdwnTrack.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                BindDropDown(drpdwnVerdictMargin, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup.Items.Insert(0, new ListItem("----", "-1"));

                BindDropDown(drpdwnWorkoutQuality1, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality1.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR1, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR1.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM1, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM1.Items.Insert(0, new ListItem("----", "-1"));

                BindDropDown(drpdwnVerdictMargin1, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin1.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup1, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup1.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality2, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality2.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR2, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR2.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM2, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM2.Items.Insert(0, new ListItem("----", "-1"));

                BindDropDown(drpdwnVerdictMargin2, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin2.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup2, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup2.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality3, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality3.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR3, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR3.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM3, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM3.Items.Insert(0, new ListItem("----", "-1"));

                BindDropDown(drpdwnVerdictMargin3, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin3.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup3, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup3.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality4, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality4.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR4, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR4.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM4, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM4.Items.Insert(0, new ListItem("----", "-1"));

                BindDropDown(drpdwnVerdictMargin4, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin4.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup4, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup4.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality5, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality5.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR5, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR5.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM5, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM5.Items.Insert(0, new ListItem("----", "-1"));

                BindDropDown(drpdwnVerdictMargin5, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin5.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup5, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup5.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality6, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality6.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR6, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR6.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM6, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM6.Items.Insert(0, new ListItem("----", "-1"));

                BindDropDown(drpdwnVerdictMargin6, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin6.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup6, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup6.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality7, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality7.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR7, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR7.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM7, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM7.Items.Insert(0, new ListItem("----", "-1"));


                BindDropDown(drpdwnVerdictMargin7, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin7.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup7, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup7.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality8, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality8.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR8, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR8.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM8, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM8.Items.Insert(0, new ListItem("----", "-1"));


                BindDropDown(drpdwnVerdictMargin8, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin8.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup8, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup8.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality9, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality9.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR9, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR9.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM9, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM9.Items.Insert(0, new ListItem("----", "-1"));

                BindDropDown(drpdwnVerdictMargin9, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin9.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup9, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup9.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality10, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality10.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR10, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR10.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM10, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM10.Items.Insert(0, new ListItem("----", "-1"));

                BindDropDown(drpdwnVerdictMargin10, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin10.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup10, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup10.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality11, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality11.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR11, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR11.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM11, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM11.Items.Insert(0, new ListItem("----", "-1"));


                BindDropDown(drpdwnVerdictMargin11, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin11.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup11, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup11.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality12, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality12.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR12, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR12.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM12, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM12.Items.Insert(0, new ListItem("----", "-1"));


                BindDropDown(drpdwnVerdictMargin12, "VerdictMargin", "VerdictMarginAlias", "VerdictMarginID");
                drpdwnVerdictMargin12.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnDistaceBreakup12, "DistanceBreakUp", "DistanceBreakUp", "DistanceBreakUpMID");
                drpdwnDistaceBreakup12.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWorkoutQuality13, "Master_CardWorkoutQuality", "WorkoutQuality", "WorkoutQualityCMID");
                drpdwnWorkoutQuality13.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnWR13, "Master_CardRating", "Rating", "RatingCMID");
                drpdwnWR13.Items.Insert(0, new ListItem("----", "-1"));
                BindDropDown(drpdwnWIM13, "Master_CardImprovementMark", "Mark", "IMarkCMID");
                drpdwnWIM13.Items.Insert(0, new ListItem("----", "-1"));
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
            DataTable dt = new CardsBL().GetCardAutoFiller("ProfessionalSupplimentaryList", prefixText);

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
            //DataTable dt = new CardsBL().GetCardAutoFiller("CardHorseName", prefixText);
            //DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseName", prefixText);
            DataTable dt = new CardsBL().GetHorseNameAutoFiller("CardHorseListWithoutDate", prefixText, "");
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
            DataTable dt = new CardsBL().GetCardAutoFiller("CardRiderListTrack", prefixText);
           // DataTable dt = new ProfessionalBL().GetProfessionalNameAutoFiller("ProfessionalName", prefixText);
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
        public static List<string> AddIndividualComment(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("TrackIndividualComment", prefixText);

            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                  horseList.Add(dt.Rows[i][0].ToString());
                //horseList.Add(
                //    AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                //        dt.Rows[i][1].ToString(),
                //        Convert.ToString(dt.Rows[i][0])));
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
        public static List<string> AddCommonComment(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("TrackCommonComment", prefixText);

            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
                //horseList.Add(
                //    AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                //        dt.Rows[i][1].ToString(),
                //        Convert.ToString(dt.Rows[i][0])));
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
                int count= 0;
                var status = 0;
                var recordstatus = 0;
                if (btnMockRaceAdd.Text.Equals("Add"))
                {
                    var timetaken = string.Empty;
                    if (txtbxMM1.Text.Equals("") && !txtbxSS1.Text.Equals(""))
                    {
                        timetaken = "00:" + txtbxSS1.Text;
                    }
                    else if (!txtbxMM1.Text.Equals("") && txtbxSS1.Text.Equals(""))
                    {
                        timetaken = txtbxMM1.Text + ":00";
                    }
                    else if (txtbxMM1.Text.Equals("") && txtbxSS1.Text.Equals(""))
                    {
                        timetaken = "00:00";
                    }
                    else
                    {
                        timetaken = txtbxMM1.Text + ":" + txtbxSS1.Text;
                    }
                    status = new CardsBL().CardTrack(
                        count,
                        txtbxHandicapEnterDate.Text,
                        Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                        Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                        txtbxRaceDate.Text,
                        //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                        0,
                        Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                        Convert.ToInt32(hdnfieldHorseNameID.Value),
                        Convert.ToInt32(hdnfieldRiderNameID.Value),
                        Convert.ToInt32(drpdwnDistaceBreakup.SelectedItem.Value),
                        timetaken,
                        0,
                        0,
                        0,
                        Convert.ToInt32(drpdwnVerdictMargin.SelectedItem.Value),
                        txtbxComment.Text, txtbxIndividualcomment.Text, 1, "Insert",drpdwnWorkouttype.SelectedItem.Text,txtbxDraw1.Text,txtbxCW1.Text,
                        (chkbxDBC1.Checked.Equals(true))?1:0,
                        (chkbxIHCC1.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality1.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR1.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM1.SelectedItem.Value),
                         (chkbxIsShow1.Checked.Equals(true)) ? 1 : 0);

                    //count += 1;
                    count = status;
                    recordstatus = 1;

                    if (!(drpdwnDistaceBreakup1.SelectedItem.Value=="-1" && hdnfieldHorseNameID1.Value.Equals("")))
                    {
                        var timetaken2 = string.Empty;
                        if (txtbxMM2.Text.Equals("") && !txtbxSS2.Text.Equals(""))
                        {
                            timetaken2 = "00:" + txtbxSS2.Text;
                        }
                        else if (!txtbxMM2.Text.Equals("") && txtbxSS2.Text.Equals(""))
                        {
                            timetaken2 = txtbxMM2.Text + ":00";
                        }
                        else if (txtbxMM2.Text.Equals("") && txtbxSS2.Text.Equals(""))
                        {
                            timetaken2 = "00:00";
                        }
                        else
                        {
                            timetaken2 = txtbxMM2.Text + ":" + txtbxSS2.Text;
                        }

                        status = new CardsBL().CardTrack(
                        count,
                        txtbxHandicapEnterDate.Text,
                        Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                        Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                        txtbxRaceDate.Text,
                        //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                        0,
                        Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                        (hdnfieldHorseNameID1.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID1.Value),
                        (hdnfieldRiderNameID1.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID1.Value),
                        Convert.ToInt32(drpdwnDistaceBreakup1.SelectedItem.Value),
                        timetaken2,
                        0,0,0,
                        Convert.ToInt32(drpdwnVerdictMargin1.SelectedItem.Value),
                        txtbxComment1.Text, txtbxIndividualcomment1.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw2.Text, txtbxCW2.Text,
                        (chkbxDBC2.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC2.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality2.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR2.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM2.SelectedItem.Value),
                         (chkbxIsShow2.Checked.Equals(true)) ? 1 : 0);
                        count = status;
                        recordstatus = 1;
                        if (!(drpdwnDistaceBreakup2.SelectedItem.Value == "-1" && hdnfieldHorseNameID2.Value.Equals("")))
                        {
                            var timetaken3 = string.Empty;
                            if (txtbxMM3.Text.Equals("") && !txtbxSS3.Text.Equals(""))
                            {
                                timetaken3 = "00:" + txtbxSS3.Text;
                            }
                            else if (!txtbxMM3.Text.Equals("") && txtbxSS3.Text.Equals(""))
                            {
                                timetaken3 = txtbxMM3.Text + ":00";
                            }
                            else if (txtbxMM3.Text.Equals("") && txtbxSS3.Text.Equals(""))
                            {
                                timetaken3 = "00:00";
                            }
                            else
                            {
                                timetaken3 = txtbxMM3.Text + ":" + txtbxSS3.Text;
                            }

                            status = new CardsBL().CardTrack(
                            count,
                           txtbxHandicapEnterDate.Text,
                            Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                            Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                            txtbxRaceDate.Text,
                            //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                            0,
                            Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                            (hdnfieldHorseNameID2.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID2.Value),
                            (hdnfieldRiderNameID2.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID2.Value),
                            Convert.ToInt32(drpdwnDistaceBreakup2.SelectedItem.Value),
                            timetaken3,
                            0, 0, 0,
                            Convert.ToInt32(drpdwnVerdictMargin2.SelectedItem.Value),
                            txtbxComment2.Text, txtbxIndividualcomment2.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw3.Text, txtbxCW3.Text,
                            (chkbxDBC3.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC3.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality3.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR3.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM3.SelectedItem.Value),
                         (chkbxIsShow3.Checked.Equals(true)) ? 1 : 0);
                            count = status;
                            recordstatus = 1;
                            if (!(drpdwnDistaceBreakup3.SelectedItem.Value == "-1" && hdnfieldHorseNameID3.Value.Equals("")))
                            {
                                var timetaken4 = string.Empty;
                                if (txtbxMM4.Text.Equals("") && !txtbxSS4.Text.Equals(""))
                                {
                                    timetaken4= "00:" + txtbxSS4.Text;
                                }
                                else if (!txtbxMM4.Text.Equals("") && txtbxSS4.Text.Equals(""))
                                {
                                    timetaken4 = txtbxMM4.Text + ":00";
                                }
                                else if (txtbxMM4.Text.Equals("") && txtbxSS4.Text.Equals(""))
                                {
                                    timetaken4 = "00:00";
                                }
                                else
                                {
                                    timetaken4 = txtbxMM4.Text + ":" + txtbxSS4.Text;
                                }

                                status = new CardsBL().CardTrack(
                                count,
                              txtbxHandicapEnterDate.Text,
                                Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                                txtbxRaceDate.Text,
                                //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                                0,
                                Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                                (hdnfieldHorseNameID3.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID3.Value),
                                (hdnfieldRiderNameID3.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID3.Value),
                                Convert.ToInt32(drpdwnDistaceBreakup3.SelectedItem.Value),
                                timetaken4,
                                0, 0, 0,
                                Convert.ToInt32(drpdwnVerdictMargin3.SelectedItem.Value),
                                txtbxComment3.Text, txtbxIndividualcomment3.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw4.Text, txtbxCW4.Text,
                                (chkbxDBC4.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC4.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality4.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR4.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM4.SelectedItem.Value),
                         (chkbxIsShow4.Checked.Equals(true)) ? 1 : 0);
                                count = status;
                                recordstatus = 1;
                                if (!(drpdwnDistaceBreakup4.SelectedItem.Value == "-1" && hdnfieldHorseNameID4.Value.Equals("")))
                                {
                                    var timetaken5 = string.Empty;
                                    if (txtbxMM5.Text.Equals("") && !txtbxSS5.Text.Equals(""))
                                    {
                                        timetaken5 = "00:" + txtbxSS5.Text;
                                    }
                                    else if (!txtbxMM5.Text.Equals("") && txtbxSS5.Text.Equals(""))
                                    {
                                        timetaken5 = txtbxMM5.Text + ":00";
                                    }
                                    else if (txtbxMM5.Text.Equals("") && txtbxSS5.Text.Equals(""))
                                    {
                                        timetaken5 = "00:00";
                                    }
                                    else
                                    {
                                        timetaken5 = txtbxMM5.Text + ":" + txtbxSS5.Text;
                                    }

                                    status = new CardsBL().CardTrack(
                                    count,
                                   txtbxHandicapEnterDate.Text,
                                    Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                                    txtbxRaceDate.Text,
                                    //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                                    0,
                                    Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                                    (hdnfieldHorseNameID4.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID4.Value),
                                    (hdnfieldRiderNameID4.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID4.Value),
                                    Convert.ToInt32(drpdwnDistaceBreakup4.SelectedItem.Value),
                                    timetaken5,
                                    0, 0, 0,
                                    Convert.ToInt32(drpdwnVerdictMargin4.SelectedItem.Value),
                                    txtbxComment4.Text, txtbxIndividualcomment4.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw5.Text, txtbxCW5.Text,
                                    (chkbxDBC5.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC5.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality5.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR5.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM5.SelectedItem.Value),
                         (chkbxIsShow5.Checked.Equals(true)) ? 1 : 0);
                                    count = status;
                                    recordstatus = 1;
                                    if (!(drpdwnDistaceBreakup5.SelectedItem.Value == "-1" && hdnfieldHorseNameID5.Value.Equals("")))
                                    {
                                        var timetaken6 = string.Empty;
                                        if (txtbxMM6.Text.Equals("") && !txtbxSS6.Text.Equals(""))
                                        {
                                            timetaken6 = "00:" + txtbxSS6.Text;
                                        }
                                        else if (!txtbxMM6.Text.Equals("") && txtbxSS6.Text.Equals(""))
                                        {
                                            timetaken6 = txtbxMM6.Text + ":00";
                                        }
                                        else if (txtbxMM6.Text.Equals("") && txtbxSS6.Text.Equals(""))
                                        {
                                            timetaken6 = "00:00";
                                        }
                                        else
                                        {
                                            timetaken6 = txtbxMM6.Text + ":" + txtbxSS6.Text;
                                        }

                                        status = new CardsBL().CardTrack(
                                        count,
                                       txtbxHandicapEnterDate.Text,
                                        Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                        Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                                        txtbxRaceDate.Text,
                                        //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                                        0,
                                        Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                                        (hdnfieldHorseNameID5.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID5.Value),
                                        (hdnfieldRiderNameID5.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID5.Value),
                                        Convert.ToInt32(drpdwnDistaceBreakup5.SelectedItem.Value),
                                        timetaken6,
                                        0, 0, 0,
                                        Convert.ToInt32(drpdwnVerdictMargin5.SelectedItem.Value),
                                        txtbxComment5.Text, txtbxIndividualcomment5.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw6.Text, txtbxCW6.Text,
                                        (chkbxDBC6.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC6.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality6.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR6.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM6.SelectedItem.Value),
                         (chkbxIsShow6.Checked.Equals(true)) ? 1 : 0);
                                        count = status;
                                        recordstatus = 1;
                                        if (!(drpdwnDistaceBreakup6.SelectedItem.Value == "-1" && hdnfieldHorseNameID6.Value.Equals("")))
                                        {
                                            var timetaken7 = string.Empty;
                                            if (txtbxMM7.Text.Equals("") && !txtbxSS7.Text.Equals(""))
                                            {
                                                timetaken7 = "00:" + txtbxSS7.Text;
                                            }
                                            else if (!txtbxMM7.Text.Equals("") && txtbxSS7.Text.Equals(""))
                                            {
                                                timetaken7 = txtbxMM7.Text + ":00";
                                            }
                                            else if (txtbxMM7.Text.Equals("") && txtbxSS7.Text.Equals(""))
                                            {
                                                timetaken7 = "00:00";
                                            }
                                            else
                                            {
                                                timetaken7 = txtbxMM7.Text + ":" + txtbxSS7.Text;
                                            }

                                            status = new CardsBL().CardTrack(
                                            count,
                                            txtbxHandicapEnterDate.Text,
                                            Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                            Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                                            txtbxRaceDate.Text,
                                            //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                                            0,
                                            Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                                            (hdnfieldHorseNameID6.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID6.Value),
                                            (hdnfieldRiderNameID6.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID6.Value),
                                            Convert.ToInt32(drpdwnDistaceBreakup6.SelectedItem.Value),
                                            timetaken7,
                                            0, 0, 0,
                                            Convert.ToInt32(drpdwnVerdictMargin6.SelectedItem.Value),
                                            txtbxComment6.Text, txtbxIndividualcomment6.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw7.Text, txtbxCW7.Text,
                                            (chkbxDBC7.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC7.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality7.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR7.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM7.SelectedItem.Value),
                         (chkbxIsShow7.Checked.Equals(true)) ? 1 : 0);
                                            count = status;
                                            recordstatus = 1;

                                            if (!(drpdwnDistaceBreakup7.SelectedItem.Value == "-1" && hdnfieldHorseNameID7.Value.Equals("")))
                                            {
                                                var timetaken8 = string.Empty;
                                                if (txtbxMM8.Text.Equals("") && !txtbxSS8.Text.Equals(""))
                                                {
                                                    timetaken8 = "00:" + txtbxSS8.Text;
                                                }
                                                else if (!txtbxMM8.Text.Equals("") && txtbxSS8.Text.Equals(""))
                                                {
                                                    timetaken8 = txtbxMM8.Text + ":00";
                                                }
                                                else if (txtbxMM8.Text.Equals("") && txtbxSS8.Text.Equals(""))
                                                {
                                                    timetaken8 = "00:00";
                                                }
                                                else
                                                {
                                                    timetaken8 = txtbxMM8.Text + ":" + txtbxSS8.Text;
                                                }

                                                status = new CardsBL().CardTrack(
                                                count,
                                                txtbxHandicapEnterDate.Text,
                                                Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                                Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                                                txtbxRaceDate.Text,
                                                //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                                                0,
                                                Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                                                (hdnfieldHorseNameID7.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID7.Value),
                                                (hdnfieldRiderNameID7.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID7.Value),
                                                Convert.ToInt32(drpdwnDistaceBreakup7.SelectedItem.Value),
                                                timetaken8,
                                                0, 0, 0,
                                                Convert.ToInt32(drpdwnVerdictMargin7.SelectedItem.Value),
                                                txtbxComment7.Text, txtbxIndividualcomment7.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw8.Text, txtbxCW8.Text,
                                                (chkbxDBC8.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC8.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality8.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR8.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM8.SelectedItem.Value),
                         (chkbxIsShow8.Checked.Equals(true)) ? 1 : 0);
                                                count = status;
                                                recordstatus = 1;

                                                if (!(drpdwnDistaceBreakup8.SelectedItem.Value == "-1" && hdnfieldHorseNameID8.Value.Equals("")))
                                                {
                                                    var timetaken81 = string.Empty;
                                                    if (txtbxMM81.Text.Equals("") && !txtbxSS81.Text.Equals(""))
                                                    {
                                                        timetaken81 = "00:" + txtbxSS81.Text;
                                                    }
                                                    else if (!txtbxMM81.Text.Equals("") && txtbxSS81.Text.Equals(""))
                                                    {
                                                        timetaken81 = txtbxMM81.Text + ":00";
                                                    }
                                                    else if (txtbxMM81.Text.Equals("") && txtbxSS81.Text.Equals(""))
                                                    {
                                                        timetaken81 = "00:00";
                                                    }
                                                    else
                                                    {
                                                        timetaken81 = txtbxMM81.Text + ":" + txtbxSS81.Text;
                                                    }

                                                    status = new CardsBL().CardTrack(
                                                    count,
                                                    txtbxHandicapEnterDate.Text,
                                                    Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                                                    txtbxRaceDate.Text,
                                                    //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                                                    0,
                                                    Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                                                    (hdnfieldHorseNameID8.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID8.Value),
                                                    (hdnfieldRiderNameID8.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID8.Value),
                                                    Convert.ToInt32(drpdwnDistaceBreakup8.SelectedItem.Value),
                                                    timetaken81,
                                                    0, 0, 0,
                                                    Convert.ToInt32(drpdwnVerdictMargin8.SelectedItem.Value),
                                                    txtbxComment8.Text, txtbxIndividualcomment8.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw9.Text, txtbxCW9.Text,
                                                    (chkbxDBC9.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC9.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality9.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR9.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM9.SelectedItem.Value),
                         (chkbxIsShow9.Checked.Equals(true)) ? 1 : 0);
                                                    count = status;
                                                    recordstatus = 1;

                                                    if (!(drpdwnDistaceBreakup9.SelectedItem.Value == "-1" && hdnfieldHorseNameID9.Value.Equals("")))
                                                    {
                                                        var timetaken9 = string.Empty;
                                                        if (txtbxMM9.Text.Equals("") && !txtbxSS9.Text.Equals(""))
                                                        {
                                                            timetaken9 = "00:" + txtbxSS9.Text;
                                                        }
                                                        else if (!txtbxMM9.Text.Equals("") && txtbxSS9.Text.Equals(""))
                                                        {
                                                            timetaken9 = txtbxMM9.Text + ":00";
                                                        }
                                                        else if (txtbxMM9.Text.Equals("") && txtbxSS9.Text.Equals(""))
                                                        {
                                                            timetaken9 = "00:00";
                                                        }
                                                        else
                                                        {
                                                            timetaken9 = txtbxMM9.Text + ":" + txtbxSS9.Text;
                                                        }

                                                        status = new CardsBL().CardTrack(
                                                        count,
                                                        txtbxHandicapEnterDate.Text,
                                                        Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                                        Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                                                        txtbxRaceDate.Text,
                                                        //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                                                        0,
                                                        Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                                                        (hdnfieldHorseNameID9.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID9.Value),
                                                        (hdnfieldRiderNameID9.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID9.Value),
                                                        Convert.ToInt32(drpdwnDistaceBreakup9.SelectedItem.Value),
                                                        timetaken9,
                                                        0, 0, 0,
                                                        Convert.ToInt32(drpdwnVerdictMargin9.SelectedItem.Value),
                                                        txtbxComment9.Text, txtbxIndividualcomment9.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw10.Text, txtbxCW10.Text,
                                                        (chkbxDBC10.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC10.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality10.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR10.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM10.SelectedItem.Value),
                         (chkbxIsShow10.Checked.Equals(true)) ? 1 : 0);
                                                        count = status;
                                                        recordstatus = 1;

                                                        if (!(drpdwnDistaceBreakup10.SelectedItem.Value == "-1" && hdnfieldHorseNameID10.Value.Equals("")))
                                                        {
                                                            var timetaken10 = string.Empty;
                                                            if (txtbxMM10.Text.Equals("") && !txtbxSS10.Text.Equals(""))
                                                            {
                                                                timetaken10 = "00:" + txtbxSS10.Text;
                                                            }
                                                            else if (!txtbxMM10.Text.Equals("") && txtbxSS10.Text.Equals(""))
                                                            {
                                                                timetaken10 = txtbxMM10.Text + ":00";
                                                            }
                                                            else if (txtbxMM10.Text.Equals("") && txtbxSS10.Text.Equals(""))
                                                            {
                                                                timetaken10 = "00:00";
                                                            }
                                                            else
                                                            {
                                                                timetaken10 = txtbxMM10.Text + ":" + txtbxSS10.Text;
                                                            }

                                                            status = new CardsBL().CardTrack(
                                                            count,
                                                            txtbxHandicapEnterDate.Text,
                                                            Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                                            Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                                                            txtbxRaceDate.Text,
                                                            //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                                                            0,
                                                            Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                                                            (hdnfieldHorseNameID10.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID10.Value),
                                                            (hdnfieldRiderNameID10.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID10.Value),
                                                            Convert.ToInt32(drpdwnDistaceBreakup10.SelectedItem.Value),
                                                            timetaken10,
                                                            0, 0, 0,
                                                            Convert.ToInt32(drpdwnVerdictMargin10.SelectedItem.Value),
                                                            txtbxComment10.Text, txtbxIndividualcomment10.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw11.Text, txtbxCW11.Text,
                                                            (chkbxDBC11.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC11.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality11.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR11.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM11.SelectedItem.Value),
                         (chkbxIsShow11.Checked.Equals(true)) ? 1 : 0);
                                                            count = status;
                                                            recordstatus = 1;

                                                            if (!(drpdwnDistaceBreakup11.SelectedItem.Value == "-1" && hdnfieldHorseNameID11.Value.Equals("")))
                                                            {
                                                                var timetaken11 = string.Empty;
                                                                if (txtbxMM11.Text.Equals("") && !txtbxSS11.Text.Equals(""))
                                                                {
                                                                    timetaken11 = "00:" + txtbxSS11.Text;
                                                                }
                                                                else if (!txtbxMM11.Text.Equals("") && txtbxSS11.Text.Equals(""))
                                                                {
                                                                    timetaken11 = txtbxMM11.Text + ":00";
                                                                }
                                                                else if (txtbxMM11.Text.Equals("") && txtbxSS11.Text.Equals(""))
                                                                {
                                                                    timetaken11 = "00:00";
                                                                }
                                                                else
                                                                {
                                                                    timetaken11 = txtbxMM11.Text + ":" + txtbxSS11.Text;
                                                                }

                                                                status = new CardsBL().CardTrack(
                                                                count,
                                                                txtbxHandicapEnterDate.Text,
                                                                Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                                                Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                                                                txtbxRaceDate.Text,
                                                                //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                                                                0,
                                                                Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                                                                (hdnfieldHorseNameID11.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID11.Value),
                                                                (hdnfieldRiderNameID11.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID11.Value),
                                                                Convert.ToInt32(drpdwnDistaceBreakup11.SelectedItem.Value),
                                                                timetaken11,
                                                                0, 0, 0,
                                                                Convert.ToInt32(drpdwnVerdictMargin11.SelectedItem.Value),
                                                                txtbxComment11.Text, txtbxIndividualcomment11.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw12.Text, txtbxCW12.Text,
                                                                (chkbxDBC12.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC12.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality12.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR12.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM12.SelectedItem.Value),
                         (chkbxIsShow12.Checked.Equals(true)) ? 1 : 0);
                                                                count = status;
                                                                recordstatus = 1;

                                                                if (!(drpdwnDistaceBreakup12.SelectedItem.Value == "-1" && hdnfieldHorseNameID12.Value.Equals("")))
                                                                {
                                                                    var timetaken12 = string.Empty;
                                                                    if (txtbxMM12.Text.Equals("") && !txtbxSS12.Text.Equals(""))
                                                                    {
                                                                        timetaken12 = "00:" + txtbxSS12.Text;
                                                                    }
                                                                    else if (!txtbxMM12.Text.Equals("") && txtbxSS12.Text.Equals(""))
                                                                    {
                                                                        timetaken12 = txtbxMM12.Text + ":00";
                                                                    }
                                                                    else if (txtbxMM12.Text.Equals("") && txtbxSS12.Text.Equals(""))
                                                                    {
                                                                        timetaken12 = "00:00";
                                                                    }
                                                                    else
                                                                    {
                                                                        timetaken12 = txtbxMM12.Text + ":" + txtbxSS12.Text;
                                                                    }

                                                                    status = new CardsBL().CardTrack(
                                                                    count,
                                                                    txtbxHandicapEnterDate.Text,
                                                                    Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                                                                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                                                                    txtbxRaceDate.Text,
                                                                    //Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                                                                    0,
                                                                    Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                                                                    (hdnfieldHorseNameID12.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID12.Value),
                                                                    (hdnfieldRiderNameID12.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID12.Value),
                                                                    Convert.ToInt32(drpdwnDistaceBreakup12.SelectedItem.Value),
                                                                    timetaken12,
                                                                    0, 0, 0,
                                                                    Convert.ToInt32(drpdwnVerdictMargin12.SelectedItem.Value),
                                                                    txtbxComment12.Text, txtbxIndividualcomment12.Text, 1, "Insert", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw13.Text, txtbxCW13.Text,
                                                                    (chkbxDBC13.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC13.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality13.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR13.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM13.SelectedItem.Value),
                         (chkbxIsShow13.Checked.Equals(true)) ? 1 : 0);
                                                                    count = status;
                                                                    recordstatus = 1;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                        
                                    }
                                    
                                }
                                
                            }
                            
                        }
                        
                    }
                }
                else
                {
                    //dt = new CardsBL().GatePracticeEntry((int)ViewState["MockRaceCID"], txtbxHandicapEnterDate.Text, Convert.ToInt32(hdnfieldProfessionalnameid.Value),txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),  
                    //    Convert.ToInt32(drpdwnLotNo.SelectedItem.Value),Convert.ToInt32(drpdwnDistance.SelectedItem.Value),Convert.ToInt32(drpdwnTrack.SelectedItem.Value),1, "Update");
                    var timetaken = string.Empty;
                    if (txtbxMM1.Text.Equals("") && !txtbxSS1.Text.Equals(""))
                    {
                        timetaken = "00:" + txtbxSS1.Text;
                    }
                    else if (!txtbxMM1.Text.Equals("") && txtbxSS1.Text.Equals(""))
                    {
                        timetaken = txtbxMM1.Text + ":00";
                    }
                    else if (txtbxMM1.Text.Equals("") && txtbxSS1.Text.Equals(""))
                    {
                        timetaken = "00:00";
                    }
                    else
                    {
                        timetaken = txtbxMM1.Text + ":" + txtbxSS1.Text;
                    }

                    status = new CardsBL().CardTrack(
                        (int)ViewState["TrackCID"],
                        txtbxHandicapEnterDate.Text,
                        Convert.ToInt32(hdnfieldProfessionalnameid.Value),
                        Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
                        txtbxRaceDate.Text,
                        0,//Convert.ToInt32(drpdwnDistance.SelectedItem.Value),
                        Convert.ToInt32(drpdwnTrack.SelectedItem.Value),
                        (hdnfieldHorseNameID.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldHorseNameID.Value),
                        (hdnfieldRiderNameID.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldRiderNameID.Value),
                        Convert.ToInt32(drpdwnDistaceBreakup.SelectedItem.Value),
                        timetaken,
                        0,0,0,
                        Convert.ToInt32(drpdwnVerdictMargin.SelectedItem.Value),
                        txtbxComment.Text, txtbxIndividualcomment.Text, 1, "Update", drpdwnWorkouttype.SelectedItem.Text, txtbxDraw1.Text, txtbxCW1.Text,
                        (chkbxDBC1.Checked.Equals(true)) ? 1 : 0,
                        (chkbxIHCC1.Checked.Equals(true)) ? 1 : 0,
                         Convert.ToInt32(drpdwnWorkoutQuality1.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWR1.SelectedItem.Value),
                         Convert.ToInt32(drpdwnWIM1.SelectedItem.Value),
                         (chkbxIsShow1.Checked.Equals(true)) ? 1 : 0);
                    btnMockRaceAdd.Text = "Add";
                    recordstatus = status;
                }
                
                if (recordstatus == 1)
                {
                    //var message = "Total record added successfully are:" + count;
                    var message = "Record addes successfully";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ShowGridView();
                    ClearALL();
                    //BindData();
                    //ClearAllSelection(this);

                }
                else if (recordstatus == 2)
                {
                    var message = "Record updated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ShowGridView();
                    ClearALL();
                }
                else if (recordstatus == 5)
                {
                    var message = "Record activated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    
                }
                else if (recordstatus == 4)
                {
                    string message = "Record already exist.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    
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
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        private void ShowGridView()
        {
            DataTable dt = null;
            int entrytype = 0;
            var status = 0;
            dt = new CardsBL().CardTrackSelect(
                    0,
                    "__-__-____",
                    0,
                    0,
                    txtbxRaceDate.Text,
                    0,
                    0,
                    0,
                    0,
                    0,
                    "",
                    0,
                    0,
                    0, 0,
                    "", "", 1, "Select");

            //ClearAll("MockRaceEntry");
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
        protected void btnShowDisplay_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                if (txtbxHorseDisplay.Text.Equals(""))
                {
                    hdnfieldHorseNameID13.Value = string.Empty;
                }
                if (txtbxSourceNameDisplay.Text.Equals(""))
                {
                    hdnfieldsourcenamedisplay.Value = string.Empty;
                }
                dt = new CardsBL().GetTrackInformation(txtbxentrydatedisplay.Text,txtbxTrackDatedisplay.Text, 
                    (hdnfieldHorseNameID13.Value.Equals(""))? 0 : Convert.ToInt32(hdnfieldHorseNameID13.Value),
                    (hdnfieldsourcenamedisplay.Value.Equals("")) ? 0 : Convert.ToInt32(hdnfieldsourcenamedisplay.Value),
                    (drpdwnCenterDisplay.SelectedItem.Value.Equals("-1")) ? 0 : Convert.ToInt32(drpdwnCenterDisplay.SelectedItem.Value)
                    );

                //ClearAll("MockRaceEntry");
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
            catch(Exception ex)
            {

            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                ShowGridView();
                
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

        private void ClearALL()
        {
            btnMockRaceAdd.Text = "Add";
            chkbxDBC1.Checked = false;
            chkbxDBC2.Checked = false;
            chkbxDBC3.Checked = false;
            chkbxDBC4.Checked = false;
            chkbxDBC5.Checked = false;
            chkbxDBC6.Checked = false;
            chkbxDBC7.Checked = false;
            chkbxDBC8.Checked = false;
            chkbxDBC9.Checked = false;
            chkbxDBC10.Checked = false;
            chkbxDBC11.Checked = false;
            chkbxDBC12.Checked = false;
            chkbxDBC13.Checked = false;

            chkbxIHCC1.Checked = false;
            chkbxIHCC2.Checked = false;
            chkbxIHCC3.Checked = false;
            chkbxIHCC4.Checked = false;
            chkbxIHCC5.Checked = false;
            chkbxIHCC6.Checked = false;
            chkbxIHCC7.Checked = false;
            chkbxIHCC8.Checked = false;
            chkbxIHCC9.Checked = false;
            chkbxIHCC10.Checked = false;
            chkbxIHCC11.Checked = false;
            chkbxIHCC12.Checked = false;
            chkbxIHCC13.Checked = false;

            chkbxIsShow1.Checked = true;
            chkbxIsShow2.Checked = true;
            chkbxIsShow3.Checked = true;
            chkbxIsShow4.Checked = true;
            chkbxIsShow5.Checked = true;
            chkbxIsShow6.Checked = true;
            chkbxIsShow7.Checked = true;
            chkbxIsShow8.Checked = true;
            chkbxIsShow9.Checked = true;
            chkbxIsShow10.Checked = true;
            chkbxIsShow11.Checked = true;
            chkbxIsShow12.Checked = true;
            chkbxIsShow13.Checked = true;


            drpdwnWorkoutQuality1.ClearSelection();
            drpdwnWorkoutQuality2.ClearSelection();
            drpdwnWorkoutQuality3.ClearSelection();
            drpdwnWorkoutQuality4.ClearSelection();
            drpdwnWorkoutQuality5.ClearSelection();
            drpdwnWorkoutQuality6.ClearSelection();
            drpdwnWorkoutQuality7.ClearSelection();
            drpdwnWorkoutQuality8.ClearSelection();
            drpdwnWorkoutQuality9.ClearSelection();
            drpdwnWorkoutQuality10.ClearSelection();
            drpdwnWorkoutQuality11.ClearSelection();
            drpdwnWorkoutQuality12.ClearSelection();
            drpdwnWorkoutQuality13.ClearSelection();


            drpdwnWR1.ClearSelection();
            drpdwnWR2.ClearSelection();
            drpdwnWR3.ClearSelection();
            drpdwnWR4.ClearSelection();
            drpdwnWR5.ClearSelection();
            drpdwnWR6.ClearSelection();
            drpdwnWR7.ClearSelection();
            drpdwnWR8.ClearSelection();
            drpdwnWR9.ClearSelection();
            drpdwnWR10.ClearSelection();
            drpdwnWR11.ClearSelection();
            drpdwnWR12.ClearSelection();
            drpdwnWR13.ClearSelection();


            drpdwnWIM1.ClearSelection();
            drpdwnWIM2.ClearSelection();
            drpdwnWIM3.ClearSelection();
            drpdwnWIM4.ClearSelection();
            drpdwnWIM5.ClearSelection();
            drpdwnWIM6.ClearSelection();
            drpdwnWIM7.ClearSelection();
            drpdwnWIM8.ClearSelection();
            drpdwnWIM9.ClearSelection();
            drpdwnWIM10.ClearSelection();
            drpdwnWIM11.ClearSelection();
            drpdwnWIM12.ClearSelection();
            drpdwnWIM13.ClearSelection();

            if (!chkbxFix2.Checked.Equals(true))
            {
                txtbxRaceDate.Text = string.Empty;
                drpdwnWorkouttype.ClearSelection();
                drpdwnTrack.ClearSelection();
                drpdwnWorkouttype.ClearSelection();
            }
            if (!chkbxFix.Checked.Equals(true))
            {
                
                //txtbxHandicapEnterDate.Text = string.Empty;
                txtbxSourceName.Text = string.Empty;
                hdnfieldProfessionalnameid.Value = string.Empty;
                drpdwnCenterName.ClearSelection();

            }

            txtbxHorseName.Text = string.Empty;
            hdnfieldHorseNameID.Value = string.Empty;
            txtbxHorseName1.Text = string.Empty;
            hdnfieldHorseNameID1.Value = string.Empty;
            txtbxHorseName2.Text = string.Empty;
            hdnfieldHorseNameID2.Value = string.Empty;
            txtbxHorseName3.Text = string.Empty;
            hdnfieldHorseNameID3.Value = string.Empty;
            txtbxHorseName4.Text = string.Empty;
            hdnfieldHorseNameID4.Value = string.Empty;
            txtbxHorseName5.Text = string.Empty;
            hdnfieldHorseNameID5.Value = string.Empty;
            txtbxHorseName6.Text = string.Empty;
            hdnfieldHorseNameID6.Value = string.Empty;
            txtbxHorseName7.Text = string.Empty;
            hdnfieldHorseNameID7.Value = string.Empty;
            txtbxHorseName8.Text = string.Empty;
            hdnfieldHorseNameID8.Value = string.Empty;
            txtbxHorseName9.Text = string.Empty;
            hdnfieldHorseNameID9.Value = string.Empty;
            txtbxHorseName10.Text = string.Empty;
            hdnfieldHorseNameID10.Value = string.Empty;
            txtbxHorseName11.Text = string.Empty;
            hdnfieldHorseNameID11.Value = string.Empty;
            txtbxHorseName12.Text = string.Empty;
            hdnfieldHorseNameID12.Value = string.Empty;

            txtbxRiderName.Text = string.Empty;
            hdnfieldRiderNameID.Value = string.Empty;
            txtbxRiderName1.Text = string.Empty;
            hdnfieldRiderNameID1.Value = string.Empty;
            txtbxRiderName2.Text = string.Empty;
            hdnfieldRiderNameID2.Value = string.Empty;
            txtbxRiderName3.Text = string.Empty;
            hdnfieldRiderNameID3.Value = string.Empty;
            txtbxRiderName4.Text = string.Empty;
            hdnfieldRiderNameID4.Value = string.Empty;
            txtbxRiderName5.Text = string.Empty;
            hdnfieldRiderNameID5.Value = string.Empty;
            txtbxRiderName6.Text = string.Empty;
            hdnfieldRiderNameID6.Value = string.Empty;
            txtbxRiderName7.Text = string.Empty;
            hdnfieldRiderNameID7.Value = string.Empty;
            txtbxRiderName8.Text = string.Empty;
            hdnfieldRiderNameID8.Value = string.Empty;
            txtbxRiderName9.Text = string.Empty;
            hdnfieldRiderNameID9.Value = string.Empty;
            txtbxRiderName10.Text = string.Empty;
            hdnfieldRiderNameID10.Value = string.Empty;
            txtbxRiderName11.Text = string.Empty;
            hdnfieldRiderNameID11.Value = string.Empty;
            txtbxRiderName12.Text = string.Empty;
            hdnfieldRiderNameID12.Value = string.Empty;

            drpdwnDistaceBreakup.ClearSelection();
            drpdwnDistaceBreakup1.ClearSelection();
            drpdwnDistaceBreakup2.ClearSelection();
            drpdwnDistaceBreakup3.ClearSelection();
            drpdwnDistaceBreakup4.ClearSelection();
            drpdwnDistaceBreakup5.ClearSelection();
            drpdwnDistaceBreakup6.ClearSelection();
            drpdwnDistaceBreakup7.ClearSelection();
            drpdwnDistaceBreakup8.ClearSelection();
            drpdwnDistaceBreakup9.ClearSelection();
            drpdwnDistaceBreakup10.ClearSelection();
            drpdwnDistaceBreakup11.ClearSelection();
            drpdwnDistaceBreakup12.ClearSelection();


            txtbxMM1.Text = string.Empty;
            txtbxMM2.Text = string.Empty;
            txtbxMM3.Text = string.Empty;
            txtbxMM4.Text = string.Empty;
            txtbxMM5.Text = string.Empty;
            txtbxMM6.Text = string.Empty;
            txtbxMM7.Text = string.Empty;
            txtbxMM8.Text = string.Empty;
            txtbxMM81.Text = string.Empty;
            txtbxMM9.Text = string.Empty;
            txtbxMM10.Text = string.Empty;
            txtbxMM11.Text = string.Empty;
            txtbxMM12.Text = string.Empty;

            txtbxSS1.Text = string.Empty;
            txtbxSS2.Text = string.Empty;
            txtbxSS3.Text = string.Empty;
            txtbxSS4.Text = string.Empty;
            txtbxSS5.Text = string.Empty;
            txtbxSS6.Text = string.Empty;
            txtbxSS7.Text = string.Empty;
            txtbxSS8.Text = string.Empty;
            txtbxSS81.Text = string.Empty;
            txtbxSS9.Text = string.Empty;
            txtbxSS10.Text = string.Empty;
            txtbxSS11.Text = string.Empty;
            txtbxSS12.Text = string.Empty;


            drpdwnVerdictMargin.ClearSelection();
            drpdwnVerdictMargin1.ClearSelection();
            drpdwnVerdictMargin2.ClearSelection();
            drpdwnVerdictMargin3.ClearSelection();
            drpdwnVerdictMargin4.ClearSelection();
            drpdwnVerdictMargin5.ClearSelection();
            drpdwnVerdictMargin6.ClearSelection();
            drpdwnVerdictMargin7.ClearSelection();
            drpdwnVerdictMargin8.ClearSelection();
            drpdwnVerdictMargin9.ClearSelection();
            drpdwnVerdictMargin10.ClearSelection();
            drpdwnVerdictMargin11.ClearSelection();
            drpdwnVerdictMargin12.ClearSelection();

            txtbxComment.Text = string.Empty;
            txtbxComment1.Text = string.Empty;
            txtbxComment2.Text = string.Empty;
            txtbxComment3.Text = string.Empty;
            txtbxComment4.Text = string.Empty;
            txtbxComment5.Text = string.Empty;
            txtbxComment6.Text = string.Empty;
            txtbxComment7.Text = string.Empty;
            txtbxComment8.Text = string.Empty;
            txtbxComment9.Text = string.Empty;
            txtbxComment10.Text = string.Empty;
            txtbxComment11.Text = string.Empty;
            txtbxComment12.Text = string.Empty;

            txtbxIndividualcomment.Text = string.Empty;
            txtbxIndividualcomment1.Text = string.Empty;
            txtbxIndividualcomment2.Text = string.Empty;
            txtbxIndividualcomment3.Text = string.Empty;
            txtbxIndividualcomment4.Text = string.Empty;
            txtbxIndividualcomment5.Text = string.Empty;
            txtbxIndividualcomment6.Text = string.Empty;
            txtbxIndividualcomment7.Text = string.Empty;
            txtbxIndividualcomment8.Text = string.Empty;
            txtbxIndividualcomment9.Text = string.Empty;
            txtbxIndividualcomment10.Text = string.Empty;
            txtbxIndividualcomment11.Text = string.Empty;
            txtbxIndividualcomment12.Text = string.Empty;

            txtbxCW1.Text = string.Empty;
            txtbxCW2.Text = string.Empty;
            txtbxCW3.Text = string.Empty;
            txtbxCW4.Text = string.Empty;
            txtbxCW5.Text = string.Empty;
            txtbxCW6.Text = string.Empty;
            txtbxCW7.Text = string.Empty;
            txtbxCW8.Text = string.Empty;
            txtbxCW9.Text = string.Empty;
            txtbxCW10.Text = string.Empty;
            txtbxCW11.Text = string.Empty;
            txtbxCW12.Text = string.Empty;
            txtbxCW13.Text = string.Empty;

            txtbxDraw1.Text = string.Empty;
            txtbxDraw2.Text = string.Empty;
            txtbxDraw3.Text = string.Empty;
            txtbxDraw4.Text = string.Empty;
            txtbxDraw5.Text = string.Empty;
            txtbxDraw6.Text = string.Empty;
            txtbxDraw7.Text = string.Empty;
            txtbxDraw8.Text = string.Empty;
            txtbxDraw9.Text = string.Empty;
            txtbxDraw10.Text = string.Empty;
            txtbxDraw11.Text = string.Empty;
            txtbxDraw12.Text = string.Empty;
            txtbxDraw13.Text = string.Empty;


        }
        protected void btnMockRaceEntryDelete_Click(object sender, EventArgs e)
        {
            try
            {
                btnMockRaceAdd.Text = "Add";
                DataTable dt = null;
                int status = 0;
                status = new CardsBL().CardTrack(
                        (int)ViewState["TrackBunchID"],
                        "__-__-____",
                        0,0,"__-__-____",0,0,0,0,0,
                        "",0,0,0,0,
                        "","", 1, "Delete",string.Empty, string.Empty, string.Empty,0,0,0,0,0,0);
                
                if (status == 3)
                {
                    var message = "Record deleted successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    //ShowGridView();
                    //ClearALL();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }
        
        protected void btnMockRaceEntryClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearALL();
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void GvMockRace_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int status = 0;
            
                try
            {
                GridViewRow row = GvMockRace.SelectedRow;

                var dataKey = GvMockRace.DataKeys[row.RowIndex];
                var rownumber = string.Empty;
                if (dataKey != null)
                {
                    ViewState["TrackCID"] = dataKey.Value;

                    HiddenField hdnfieldProfessionalNameidG = (HiddenField)row.FindControl("hdnfieldProfessionalNameidG");
                    HiddenField hdnfieldProfessionalNameG = (HiddenField)row.FindControl("hdnfieldProfessionalNameG");

                    if (!row.Cells[1].Text.Equals("&nbsp;"))
                    {
                        hdnfieldProfessionalnameid.Value = hdnfieldProfessionalNameidG.Value;
                        txtbxSourceName.Text = hdnfieldProfessionalNameG.Value;
                    }


                    if (!row.Cells[2].Text.Equals("&nbsp;"))
                    {
                        drpdwnCenterName.ClearSelection();
                        drpdwnCenterName.Items.FindByText(row.Cells[2].Text).Selected = true;
                    }

                    if (!row.Cells[3].Text.Equals("&nbsp;"))
                        txtbxRaceDate.Text=row.Cells[3].Text;

                    if (!row.Cells[4].Text.Equals("&nbsp;"))
                    {
                        drpdwnTrack.ClearSelection();
                        drpdwnTrack.Items.FindByText(row.Cells[4].Text).Selected = true;
                    }

                    if (!row.Cells[5].Text.Equals("&nbsp;"))
                    {
                        drpdwnWorkouttype.ClearSelection();
                        drpdwnWorkouttype.Items.FindByText(row.Cells[5].Text).Selected = true;
                    }



                    
                    var previoustrackbunchid = 0;
                    //////// Upper Gridview Bind
                    for (int i = 0; i < GvMockRace.Rows.Count; i++)
                    {
                        HiddenField hdnfieldTrackBunchID = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldTrackBunchID");
                        if (i == 0)
                        {
                            ViewState["TrackBunchID"] = Convert.ToInt32(hdnfieldTrackBunchID.Value);
                            previoustrackbunchid = Convert.ToInt32(hdnfieldTrackBunchID.Value);
                        }
                        if (previoustrackbunchid == Convert.ToInt32(hdnfieldTrackBunchID.Value))
                        {
                            if (i == 0)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw1.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW1.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }



                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup.ClearSelection();
                                        drpdwnDistaceBreakup.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM1.Text = time[0];
                                            txtbxSS1.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC1.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin.ClearSelection();
                                        drpdwnVerdictMargin.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC1.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality1.ClearSelection();
                                        drpdwnWorkoutQuality1.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR1.ClearSelection();
                                        drpdwnWR1.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM1.ClearSelection();
                                        drpdwnWIM1.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow1.Checked = true;
                                    }
                                }

                            }
                            if (i == 1)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID1.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName1.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID1.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName1.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw2.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW2.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }



                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup1.ClearSelection();
                                        drpdwnDistaceBreakup1.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM2.Text = time[0];
                                            txtbxSS2.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC2.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin1.ClearSelection();
                                        drpdwnVerdictMargin1.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment1.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment1.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC2.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality2.ClearSelection();
                                        drpdwnWorkoutQuality2.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR2.ClearSelection();
                                        drpdwnWR2.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM2.ClearSelection();
                                        drpdwnWIM2.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow2.Checked = true;
                                    }
                                }

                            }
                            if (i == 2)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID2.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName2.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID2.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName2.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw3.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW3.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }
                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup2.ClearSelection();
                                        drpdwnDistaceBreakup2.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM3.Text = time[0];
                                            txtbxSS3.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC3.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin2.ClearSelection();
                                        drpdwnVerdictMargin2.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment2.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment2.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC3.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality3.ClearSelection();
                                        drpdwnWorkoutQuality3.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR3.ClearSelection();
                                        drpdwnWR3.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM3.ClearSelection();
                                        drpdwnWIM3.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow3.Checked = true;
                                    }
                                }

                            }
                            if (i == 3)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID3.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName3.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID3.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName3.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw4.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW4.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }
                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup3.ClearSelection();
                                        drpdwnDistaceBreakup3.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM4.Text = time[0];
                                            txtbxSS4.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC4.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin3.ClearSelection();
                                        drpdwnVerdictMargin3.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment3.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment3.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC4.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality4.ClearSelection();
                                        drpdwnWorkoutQuality4.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR4.ClearSelection();
                                        drpdwnWR4.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM4.ClearSelection();
                                        drpdwnWIM4.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow4.Checked = true;
                                    }
                                }

                            }
                            if (i == 4)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID4.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName4.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID4.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName4.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw5.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW5.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }
                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup5.ClearSelection();
                                        drpdwnDistaceBreakup5.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM5.Text = time[0];
                                            txtbxSS5.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC5.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin4.ClearSelection();
                                        drpdwnVerdictMargin4.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment4.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment4.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC5.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality5.ClearSelection();
                                        drpdwnWorkoutQuality5.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR5.ClearSelection();
                                        drpdwnWR5.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM5.ClearSelection();
                                        drpdwnWIM5.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow5.Checked = true;
                                    }
                                }

                            }
                            if (i == 5)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID5.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName5.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID5.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName5.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw6.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW6.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }
                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup6.ClearSelection();
                                        drpdwnDistaceBreakup6.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM6.Text = time[0];
                                            txtbxSS6.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC6.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin5.ClearSelection();
                                        drpdwnVerdictMargin5.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment5.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment5.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC6.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality6.ClearSelection();
                                        drpdwnWorkoutQuality6.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR6.ClearSelection();
                                        drpdwnWR6.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM6.ClearSelection();
                                        drpdwnWIM6.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow6.Checked = true;
                                    }
                                }

                            }
                            if (i == 6)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID6.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName6.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID6.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName6.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw7.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW7.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }
                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup7.ClearSelection();
                                        drpdwnDistaceBreakup7.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM7.Text = time[0];
                                            txtbxSS7.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC7.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin6.ClearSelection();
                                        drpdwnVerdictMargin6.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment6.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment6.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC7.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality7.ClearSelection();
                                        drpdwnWorkoutQuality7.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR7.ClearSelection();
                                        drpdwnWR7.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM7.ClearSelection();
                                        drpdwnWIM7.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow7.Checked = true;
                                    }
                                }
                            }
                            if (i == 7)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID7.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName7.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID7.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName7.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw8.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW8.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }
                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup8.ClearSelection();
                                        drpdwnDistaceBreakup8.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM8.Text = time[0];
                                            txtbxSS8.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC8.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin7.ClearSelection();
                                        drpdwnVerdictMargin7.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment7.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment7.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC8.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality8.ClearSelection();
                                        drpdwnWorkoutQuality8.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR8.ClearSelection();
                                        drpdwnWR8.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM8.ClearSelection();
                                        drpdwnWIM8.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow8.Checked = true;
                                    }
                                }
                            }
                            if (i == 8)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID8.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName8.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID8.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName8.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw9.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW9.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }
                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup9.ClearSelection();
                                        drpdwnDistaceBreakup9.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM9.Text = time[0];
                                            txtbxSS9.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC9.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin8.ClearSelection();
                                        drpdwnVerdictMargin8.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment8.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment8.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC9.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality9.ClearSelection();
                                        drpdwnWorkoutQuality9.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR9.ClearSelection();
                                        drpdwnWR9.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM9.ClearSelection();
                                        drpdwnWIM9.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow9.Checked = true;
                                    }
                                }
                            }
                            if (i == 9)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID9.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName9.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID9.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName9.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw10.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW10.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }
                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup10.ClearSelection();
                                        drpdwnDistaceBreakup10.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM10.Text = time[0];
                                            txtbxSS10.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC10.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin9.ClearSelection();
                                        drpdwnVerdictMargin9.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment9.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment9.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC10.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality10.ClearSelection();
                                        drpdwnWorkoutQuality10.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR10.ClearSelection();
                                        drpdwnWR10.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM10.ClearSelection();
                                        drpdwnWIM10.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow10.Checked = true;
                                    }
                                }
                            }
                            if (i == 10)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID10.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName10.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID10.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName10.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw11.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW11.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }
                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup11.ClearSelection();
                                        drpdwnDistaceBreakup11.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM11.Text = time[0];
                                            txtbxSS11.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC11.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin10.ClearSelection();
                                        drpdwnVerdictMargin10.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment10.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment10.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC11.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality11.ClearSelection();
                                        drpdwnWorkoutQuality11.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR11.ClearSelection();
                                        drpdwnWR11.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM11.ClearSelection();
                                        drpdwnWIM11.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow11.Checked = true;
                                    }
                                }
                            }
                            if (i == 11)
                            {
                                HiddenField hdnfieldHorseNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldHorseNameIDG");
                                if (!hdnfieldHorseNameIDG.Value.Equals(""))
                                {
                                    hdnfieldHorseNameID11.Value = hdnfieldHorseNameIDG.Value;
                                    txtbxHorseName11.Text = GvMockRace.Rows[i].Cells[6].Text.Equals("&nbsp;") ? "" : GvMockRace.Rows[i].Cells[6].Text.Contains("&#39;") ? GvMockRace.Rows[i].Cells[6].Text.Replace("&#39;", "'") : GvMockRace.Rows[i].Cells[6].Text;

                                    HiddenField hdnfieldRiderPNameIDG = (HiddenField)GvMockRace.Rows[i].FindControl("hdnfieldRiderPNameIDG");
                                    if (!hdnfieldRiderPNameIDG.Value.Equals(""))
                                    {
                                        hdnfieldRiderNameID11.Value = hdnfieldRiderPNameIDG.Value;
                                        txtbxRiderName11.Text = GvMockRace.Rows[i].Cells[7].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[8].Text.Equals("&nbsp;"))
                                    {
                                        txtbxDraw12.Text = GvMockRace.Rows[i].Cells[8].Text;
                                    }

                                    if (!GvMockRace.Rows[i].Cells[9].Text.Equals("&nbsp;"))
                                    {
                                        txtbxCW12.Text = GvMockRace.Rows[i].Cells[9].Text;
                                    }
                                    if (!GvMockRace.Rows[i].Cells[10].Text.Equals("&nbsp;"))
                                    {
                                        drpdwnDistaceBreakup12.ClearSelection();
                                        drpdwnDistaceBreakup12.Items.FindByText(GvMockRace.Rows[i].Cells[10].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                                    {
                                        string[] time = GvMockRace.Rows[i].Cells[11].Text.Split(':');
                                        if (time.Length == 2)
                                        {
                                            txtbxMM12.Text = time[0];
                                            txtbxSS12.Text = time[1];
                                        }
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[12].Text.Equals("&nbsp;")))
                                    {
                                        if (GvMockRace.Rows[i].Cells[12].Text.Equals("Yes"))
                                        {
                                            chkbxDBC12.Checked = true;
                                        }

                                    }

                                    if (!(GvMockRace.Rows[i].Cells[13].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnVerdictMargin11.ClearSelection();
                                        drpdwnVerdictMargin11.Items.FindByText(GvMockRace.Rows[i].Cells[13].Text).Selected = true;
                                    }

                                    if (!(GvMockRace.Rows[i].Cells[14].Text.Equals("&nbsp;")))
                                    {
                                        txtbxComment11.Text = GvMockRace.Rows[i].Cells[14].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[15].Text.Equals("&nbsp;")))
                                    {
                                        txtbxIndividualcomment11.Text = GvMockRace.Rows[i].Cells[15].Text;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[16].Text.Equals("&nbsp;")))
                                    {
                                        if ((GvMockRace.Rows[i].Cells[16].Text.Equals("Yes")))
                                        {
                                            chkbxIHCC12.Checked = true;
                                        }
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[17].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWorkoutQuality12.ClearSelection();
                                        drpdwnWorkoutQuality12.Items.FindByText(GvMockRace.Rows[i].Cells[17].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[18].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWR12.ClearSelection();
                                        drpdwnWR12.Items.FindByText(GvMockRace.Rows[i].Cells[18].Text).Selected = true;
                                    }
                                    if (!(GvMockRace.Rows[i].Cells[19].Text.Equals("&nbsp;")))
                                    {
                                        drpdwnWIM12.ClearSelection();
                                        drpdwnWIM12.Items.FindByText(GvMockRace.Rows[i].Cells[19].Text).Selected = true;
                                    }
                                    if ((GvMockRace.Rows[i].Cells[20].Text.Equals("Yes")))
                                    {
                                        chkbxIsShow12.Checked = true;
                                    }
                                }
                            }
                            previoustrackbunchid = Convert.ToInt32(hdnfieldTrackBunchID.Value);
                        }
                    }


                    
                }
                btnMockRaceAdd.Text = "Update";
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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