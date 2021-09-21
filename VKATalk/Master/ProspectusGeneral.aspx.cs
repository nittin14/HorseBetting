using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Globalization;
using OfficeOpenXml;

namespace VKATalk.Master
{
    using System.Data;
    using VKATalk.Common;
    using VKATalkBusinessLayer;

    public partial class ProspectusGeneral : System.Web.UI.Page
    {
        private static int _prospectusgeneralid = 0;
        String strProspectusMasterRaceName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
				lblGeneralRacecount.Text = Convert.ToString(new ProspectusBL().GetProspectusGeneralLastId("General"));
				if (Session["ProspectusGeneralID"] != null)
                {
                    this.hdnfieldprospectusid.Value = Convert.ToString(Session["ProspectusGeneralID"]);
                    this.MainProspectusBind(hdnfieldprospectusid.Value);
                    this.ProspectusBindMastervalue(hdnfieldprospectusid.Value);
                    this.Session["ProspectusGeneralID"] = null;
                    _prospectusgeneralid = Convert.ToInt32(hdnfieldprospectusid.Value);
                    btnProspectusName.Text = "Update"; 
                }
				if(!IsPostBack)
				{
					
				}
             
            }
            catch (Exception ex)
            {
                var message = "Incorrect Information";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

       
        protected void ProspectusBindMastervalue(string prospectusId)
        {
            try
            {
           
            DataSet ds = null;
            ds = new ProspectusBL().sp_GetProspectusMasterInGeneral(Convert.ToInt32(prospectusId), "MainProspectus");
                this.lblRaceInMemoryOf.Text = ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0][0].ToString() : "No Record found.";
                dvsponcer.Visible = true;
                GvSponcer.DataSource = ds.Tables[3];
                GvSponcer.DataBind();
				if (ds.Tables[5].Rows.Count > 0)
				{
					dvMomenttoPresenter.Visible = true;
					GvMomenttoPresenter.DataSource = ds.Tables[5];
					GvMomenttoPresenter.DataBind();
				}
				else
				{
					dvMomenttoPresenter.Visible = true;
					GvMomenttoPresenter.DataSource = new DataTable();
					GvMomenttoPresenter.DataBind();
				}
				if (ds.Tables[6].Rows.Count > 0)
                {
                    dvdistance.Visible = true;
                    GvDistance.DataSource = ds.Tables[6];
                    GvDistance.DataBind();
                }
                else
                {
                    dvdistance.Visible = true;
                    GvDistance.DataSource = new DataTable();
                    GvDistance.DataBind();
                }
                //this.lblRaceType.Text = ds.Tables[7].Rows.Count > 0 ? ds.Tables[7].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[7].Rows.Count > 0)
                {
                    dvracetype.Visible = true;
                    gvracetype.DataSource = ds.Tables[7];
                    gvracetype.DataBind();
                }
                else
                {
                    dvracetype.Visible = true;
                    gvracetype.DataSource = new DataTable();
                    gvracetype.DataBind();
                }

                //this.lblHandicapRatingRange.Text = ds.Tables[8].Rows.Count > 0 ? ds.Tables[8].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[8].Rows.Count > 0)
                {
                    dvhandicapratingrange.Visible = true;
                    gvhandicapratingrange.DataSource = ds.Tables[8];
                    gvhandicapratingrange.DataBind();
                }
                else
                {
                    dvhandicapratingrange.Visible = true;
                    gvhandicapratingrange.DataSource = new DataTable();
                    gvhandicapratingrange.DataBind();
                }


                //this.lblEligbleHandicapRatingRange.Text = ds.Tables[9].Rows.Count > 0 ? ds.Tables[9].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[9].Rows.Count > 0)
                {
                    dvEligiblehandicapratingrange.Visible = true;
                    gveligiblehandicapratingrange.DataSource = ds.Tables[9];
                    gveligiblehandicapratingrange.DataBind();
                }
                else
                {
                    dvEligiblehandicapratingrange.Visible = true;
                    gveligiblehandicapratingrange.DataSource = new DataTable();
                    gveligiblehandicapratingrange.DataBind();
                }


                this.lblAgeCondition.Text = ds.Tables[10].Rows.Count > 0 ? ds.Tables[10].Rows[0][0].ToString() : "No Record found.";
                this.lblRaceStatus.Text = ds.Tables[11].Rows.Count > 0 ? ds.Tables[11].Rows[0][0].ToString() : "No Record found.";
                this.lblMillion.Text = ds.Tables[12].Rows.Count > 0 ? ds.Tables[12].Rows[0][0].ToString() : "No Record found.";
                this.lblSweepStake.Text = ds.Tables[13].Rows.Count > 0 ? ds.Tables[13].Rows[0][0].ToString() : "No Record found.";
                this.lblClassic.Text = ds.Tables[14].Rows.Count > 0 ? ds.Tables[14].Rows[0][0].ToString() : "No Record found.";
                this.lblGraded.Text = ds.Tables[15].Rows.Count > 0 ? ds.Tables[15].Rows[0][0].ToString() : "No Record found.";
                this.lblGradedNo.Text = ds.Tables[16].Rows.Count > 0 ? ds.Tables[16].Rows[0][0].ToString() : "No Record found.";

                
                this.lblAbbreviation.Text = ds.Tables[17].Rows.Count > 0 ? ds.Tables[17].Rows[0][0].ToString() : "No Record found.";
                //this.lblForeignHorseAllowed.Text = ds.Tables[17].Rows.Count > 0 ? ds.Tables[17].Rows[0][1].ToString() : "No Record found.";
                lblMomenttoType.Text = ds.Tables[18].Rows.Count > 0 ? ds.Tables[18].Rows[0][0].ToString() : "No Record found.";
                //lblpresenterbackground.Text = ds.Tables[19].Rows.Count > 0 ? ds.Tables[19].Rows[0][0].ToString() : "No Record found.";

                divInterestedprofessionBackground.Visible = true;
                GvInterestedProfessionBackground.DataSource = ds.Tables[19];
                GvInterestedProfessionBackground.DataBind();

                lblbunchcondition.Text = ds.Tables[20].Rows.Count > 0 ? ds.Tables[20].Rows[0][0].ToString() : "No Record found.";
				//lblPermanentCondition.Text = ds.Tables[21].Rows.Count > 0 ? ds.Tables[21].Rows[0][0].ToString() : "No Record found.";
				if (ds.Tables[21].Rows.Count > 0)
				{
					DvPermanent.Visible = true;
					GvPermanentCondition.DataSource = ds.Tables[21];
					GvPermanentCondition.DataBind();
				}
				else
				{
					DvPermanent.Visible = true;
					GvPermanentCondition.DataSource = new DataTable();
					GvPermanentCondition.DataBind();
				}

				dvhandicapweightaspergender.Visible = true;
                GvHandicapWeightAsperGender.DataSource = ds.Tables[22];
                GvHandicapWeightAsperGender.DataBind();

                dvHWPCondition.Visible = true;
                GVHWPCondition.DataSource = ds.Tables[23];
                GVHWPCondition.DataBind();

                lblMaidenHorseTerm.Text = ds.Tables[24].Rows.Count > 0 ? ds.Tables[24].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[25].Rows.Count > 0)
                {
                    dvAgeCondition.Visible = true;
                    GvAgeCondition.DataSource = ds.Tables[25];
                    GvAgeCondition.DataBind();
                }
                else
                {
                    dvAgeCondition.Visible = true;
                    GvAgeCondition.DataSource = new DataTable();
                    GvAgeCondition.DataBind();
                }

                if (ds.Tables[26].Rows.Count > 0)
                {
                    dvracehistory.Visible = true;
                    gvracehistory.DataSource = ds.Tables[26];
                    gvracehistory.DataBind();
                }
                else
                {
                    dvracehistory.Visible = true;
                    gvracehistory.DataSource = new DataTable();
                    gvracehistory.DataBind();
                }

                
                //this.lblClassGroup.Text = ds.Tables[27].Rows.Count > 0 ? ds.Tables[27].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[27].Rows.Count > 0)
                {
                    dvclassgroup.Visible = true;
                    gvclassgroup.DataSource = ds.Tables[27];
                    gvclassgroup.DataBind();
                }
                else
                {
                    dvclassgroup.Visible = true;
                    gvclassgroup.DataSource = new DataTable();
                    gvclassgroup.DataBind();
                }
				//lblOtherCondition.Text = ds.Tables[28].Rows.Count > 0 ? ds.Tables[28].Rows[0][0].ToString() : "No Record found.";

				if (ds.Tables[28].Rows.Count > 0)
				{
					DvOtherCondition.Visible = true;
					GvOtherCondition.DataSource = ds.Tables[28];
					GvOtherCondition.DataBind();
				}
				else
				{
					DvOtherCondition.Visible = true;
					GvOtherCondition.DataSource = new DataTable();
					GvOtherCondition.DataBind();
				}

			}
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }



        protected void MainProspectusBind(string prospectusId)
        {
            try
            {

                DataSet ds = null;
                ds = new ProspectusBL().GetProspectusCompleteInformationGeneral(Convert.ToInt32(prospectusId), "MainProspectus");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //this.hdnfdHorseDetail.Value = ds.Tables[0].Rows[0][1].ToString();
                    if (Session["ProspectusGeneralID"] != null)
                    {
                        this.txtbxGeneralRaceName.Text = ds.Tables[0].Rows[0][1].ToString();
                        Session["ProspectusGeneralRaceName"] = txtbxGeneralRaceName.Text;
                       // this.txtbxGeneralRaceName.Text = Session["ProspectusGeneralRaceName"].ToString();
                    }
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.lblProspectusCurrentName.Text = ds.Tables[1].Rows[0][0].ToString();
                    this.lblProspectusExName.Text = ds.Tables[1].Rows.Count > 1 ? ds.Tables[1].Rows[1][0].ToString() : "";
                    //this.hdnfieldRaceName.Value = hdnfieldRaceName.Value + "," + ds.Tables[1].Rows[0][0].ToString() + "," +
                    //                         lblProspectusExName.Text;
                    this.hdnfieldRaceName.Value = ds.Tables[1].Rows[0][0] + "," + lblProspectusExName.Text;
                    this.lblCenterName.Text = (ds.Tables[1].Rows[0][1].ToString().Replace("(", "")).Replace(")", "").Trim();
                    this.lblSeasonName.Text = (ds.Tables[1].Rows[0][2].ToString().Replace("(", "")).Replace(")", "").Trim();
                    this.lblYearName.Text = (ds.Tables[1].Rows[0][3].ToString().Replace("(", "")).Replace(")", "").Trim();
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        lblRaceDate.Text = ds.Tables[2].Rows[0]["RaceDate"].ToString();

                        if ((lblRaceDate.Text != "__-__-____") && ((lblRaceDate.Text != "NULL")) && (lblRaceDate.Text != ""))
                        {
                            string value = lblRaceDate.Text;
                            DateTime dt = Convert.ToDateTime(value);
                            lblWeekDay.Text = dt.DayOfWeek.ToString();
                        }

                        txtbxRaceDay.Text = ds.Tables[2].Rows[0]["RaceDay"].ToString();
                        txtbxSerialNumber.Text = ds.Tables[2].Rows[0]["SerialNumber"].ToString();
                        txtbxYearofBirth.Text = ds.Tables[2].Rows[0]["YearofBirth"].ToString();
                        if (ds.Tables[2].Rows[0]["TimeSlotofRace"].ToString().Equals(""))
                        {
                            rdbtnTimeSlot.Items.FindByText("After Noon").Selected = true;
                        }
                        else
                        {
                            rdbtnTimeSlot.ClearSelection();
                            rdbtnTimeSlot.Items.FindByText(ds.Tables[2].Rows[0]["TimeSlotofRace"].ToString()).Selected = true;
                        }
                        if (!(ds.Tables[2].Rows[0]["RaceDate"].ToString().Equals("")))
                        {
                            DateTime dt = Convert.ToDateTime(ds.Tables[2].Rows[0]["RaceDate"].ToString());
                            lblWeekDay.Text = dt.DayOfWeek.ToString();
                        }
                        if (ds.Tables[2].Rows[0]["MainRaceofDay"].ToString().Equals("1"))
                        {
                            chkbxMainRaceofDay.Checked = true;
                        }
                        
                     }
					
					if (ds.Tables[16].Rows.Count > 0)
					{
						DvSeasonalCondition.Visible = true;
						GvSeasonalCondition.DataSource = ds.Tables[16];
						GvSeasonalCondition.DataBind();
					}
					else
					{
						DvSeasonalCondition.Visible = true;
						GvSeasonalCondition.DataSource = new DataTable();
						GvSeasonalCondition.DataBind();
					}
					if (ds.Tables[17].Rows.Count > 0)
					{
						DvRaceCardCondition.Visible = true;
						GvRaceCardCondition.DataSource = ds.Tables[17];
						GvRaceCardCondition.DataBind();
					}
					else
					{
						DvRaceCardCondition.Visible = true;
						GvRaceCardCondition.DataSource = new DataTable();
						GvRaceCardCondition.DataBind();
					}

					dvRaceDate.Visible = true;
                    gridvwGeneralDates.DataSource = ds.Tables[18];
                    gridvwGeneralDates.DataBind();


                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        DvObservation.Visible = true;
                        GvObservation.DataSource = ds.Tables[3];
                        GvObservation.DataBind();
                    }
                    else
                    {
                        DvObservation.Visible = true;
                        GvObservation.DataSource = new DataTable();
                        GvObservation.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        private void BindDropDown(DropDownList ddl, String tablename, string textfield, String valuefield)
        {
            ddl.DataSource = new ProspectusBL().GetDropdownBind(tablename);
            ddl.DataTextField = textfield;
            ddl.DataValueField = valuefield;
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
        public static List<string> AddProspectusList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusName", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0])));
               // horseList.Add(dt.Rows[i][1].ToString());
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
		public static List<string> AddProspectusList(string prefixText, int count, string contextKey)
		{
			DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusName", prefixText, contextKey);
			List<string> horseList = new List<string>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
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
        public static List<string> AddProspectusGeneralList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetProspectusGeneralAutoFill("ProspectusGeneralName", prefixText,_prospectusgeneralid,"");
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][1].ToString());
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
        public static List<string> AddRaceDayList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusRaceDay", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
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
        public static List<string> AddSerialNoList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("SerialNoList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
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
        public static List<string> AddNameofRaceDayList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusNameofRaceDay", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
            }
            return horseList;
        }

        protected void btnShow_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtbxGeneralRaceName.Text.Equals(""))
                {
                    var message = "Please select Prospectus General Name.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    string[] prospectusName = txtbxGeneralRaceName.Text.Split('{');
                    Session["ProspectusGeneralRaceName"] = txtbxGeneralRaceName.Text;
                    if (prospectusName.Length == 1)
                    {
                        var message = "Prospectus Name not found.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        
                        //hdnfieldprospectusid.Value = Convert.ToString(new ProspectusBL().GetProspectusGeneralId(prospectusName[0], prospectusName[1].Remove(prospectusName[1].Length - 1), prospectusName[2].Remove(prospectusName[2].Length - 1), prospectusName[3].Remove(prospectusName[3].Length - 1)));

                        DataTable dt = new ProspectusBL().GetProspectusGeneralId(prospectusName[0], prospectusName[1].Remove(prospectusName[1].Length - 1), prospectusName[2].Remove(prospectusName[2].Length - 1), prospectusName[3].Remove(prospectusName[3].Length - 1));
                        if (dt.Rows.Count > 0)
                        {
                            hdnfieldprospectusid.Value = dt.Rows[0][1].ToString();
                            lblGeneralRaceIDNameID.Text = dt.Rows[0][0].ToString() + "-" + dt.Rows[0][1].ToString();
                        }


                        btnProspectusName.Text = "Update";
                        MainProspectusBind(hdnfieldprospectusid.Value);
                        this.ProspectusBindMastervalue(hdnfieldprospectusid.Value);
                        DisableAllField(this, "Disable");
                        DisableButton("Disable");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + ex.StackTrace + "');", true);
            }
        }

        //protected void btnMasterShow_OnClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtbxProspectusMasterName.Text.Equals(""))
        //        {
        //            var message = "Please select Prospectus Name.";
        //            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        //        }
        //        else
        //        {
        //            btnProspectusName.Enabled = true; Button8.Enabled = true;
        //            _prospectusgeneralid = Convert.ToInt32(hdnfieldMasterProsectusId.Value);
        //            string[] prospectusName = txtbxProspectusMasterName.Text.Split('{');

        //            if (prospectusName.Length == 1)
        //            {
        //                var message = "Prospectus Name not found.";
        //                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        //            }
        //            else
        //            {
        //                Session["ProspectusGeneralMasterRaceName"] = txtbxProspectusMasterName.Text;
        //                strProspectusMasterRaceName = txtbxProspectusMasterName.Text;
        //                ProspectusBindMastervalue(hdnfieldMasterProsectusId.Value);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandling.SendErrorToText(ex);
        //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + ex.StackTrace + "');", true);
        //    }
        //}

        public void DisableButton(string action)
        {
            if (action.Equals("Disable"))
            {
				btnProspectusName.Enabled = false;
				Button8.Enabled = false;
				btnSeasonalCondition.Enabled = false;
                btnRaceCard.Enabled = false;
                btnObservation.Enabled = false;
                Button1.Enabled = false;
                
            }
            else
            {
                btnSeasonalCondition.Enabled = true;
                btnRaceCard.Enabled = true;
                btnObservation.Enabled = true;
                Button1.Enabled = true;
				btnProspectusName.Enabled = true;
				Button8.Enabled = true;

			}
        }

        public void DisableAllField(Control parent, string action)
        {
            if (action.Equals("Disable"))
            {
                foreach (Control x in parent.Controls)
                {
                    if ((x.GetType() == typeof(TextBox)))
                    {

                        ((TextBox)(x)).Enabled = false;
                    }

                    if ((x.GetType() == typeof(DropDownList)))
                    {

                        ((DropDownList)(x)).Enabled = false;
                    }

                    if ((x.GetType() == typeof(Label)))
                    {

                        ((Label)(x)).Enabled = false;
                    }
                    if ((x.GetType() == typeof(CheckBox)))
                    {

                        ((CheckBox)(x)).Enabled = false;
                    }
                    if ((x.GetType() == typeof(CheckBoxList)))
                    {

                        ((CheckBoxList)(x)).Enabled = false;
                    }
                    if ((x.GetType() == typeof(RadioButtonList)))
                    {

                        ((RadioButtonList)(x)).Enabled = false;
                    }

                    if (x.HasControls())
                    {
                        DisableAllField(x, "Disable");
                    }
                }
            }
            else
            {
                foreach (Control x in parent.Controls)
                {
                    if ((x.GetType() == typeof(TextBox)))
                    {

                        ((TextBox)(x)).Enabled = true;
                    }

                    if ((x.GetType() == typeof(DropDownList)))
                    {

                        ((DropDownList)(x)).Enabled = true;
                    }

                    if ((x.GetType() == typeof(Label)))
                    {

                        ((Label)(x)).Enabled = true;
                    }
                    if ((x.GetType() == typeof(CheckBox)))
                    {

                        ((CheckBox)(x)).Enabled = true;
                    }
                    if ((x.GetType() == typeof(CheckBoxList)))
                    {

                        ((CheckBoxList)(x)).Enabled = true;
                    }
                    if ((x.GetType() == typeof(RadioButtonList)))
                    {

                        ((RadioButtonList)(x)).Enabled = true;
                    }

                    if (x.HasControls())
                    {
                        DisableAllField(x, "Enable");
                    }
                }
            }
        }
        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            lblGeneralRaceIDNameID.Text = string.Empty;
            ClearAllSelection(this);
            btnAdd.Text = "Add";
            btnProspectusName.Text = "Add";
            Session["ProspectusGeneralID"] = null;
            Session["ProspectusMasterName"] = null;
            Session.Remove("ProspectusGeneralMasterRaceName");
            Session.Remove("ProspectusGeneralRaceName");
			btnProspectusName.Enabled = true;
			Button8.Enabled = true;
			GvSponcer.DataSource = new DataTable();
            GvSponcer.DataBind();

            GvDistance.DataSource = new DataTable();
            GvDistance.DataBind();

            gvracetype.DataSource = new DataTable();
            gvracetype.DataBind();

            gvhandicapratingrange.DataSource = new DataTable();
            gvhandicapratingrange.DataBind();


			gridvwGeneralDates.DataSource = new DataTable();
			gridvwGeneralDates.DataBind();

			GvInterestedProfessionBackground.DataSource = new DataTable();
            GvInterestedProfessionBackground.DataBind();

            GVHWPCondition.DataSource = new DataTable();
            GVHWPCondition.DataBind();

            GvHandicapWeightAsperGender.DataSource = new DataTable();
            GvHandicapWeightAsperGender.DataBind();

            GvAgeCondition.DataSource = new DataTable();
            GvAgeCondition.DataBind();

			GvMomenttoPresenter.DataSource = new DataTable();
			GvMomenttoPresenter.DataBind();

			GvPermanentCondition.DataSource = new DataTable();
			GvPermanentCondition.DataBind();

			GvSeasonalCondition.DataSource = new DataTable();
			GvSeasonalCondition.DataBind();

			gveligiblehandicapratingrange.DataSource = new DataTable();
			gveligiblehandicapratingrange.DataBind();

			//gvclassgroup.DataBind();
			gvclassgroup.DataSource = new DataTable();
            gvclassgroup.DataBind();

            gvracehistory.DataSource = new DataTable();
            gvracehistory.DataBind();

			GvRaceCardCondition.DataSource = new DataTable();
			GvRaceCardCondition.DataBind();

			GvOtherCondition.DataSource = new DataTable();
			GvOtherCondition.DataBind();

			GvObservation.DataSource = new DataTable();
			GvObservation.DataBind();
			

			txtbxGeneralRaceName.Enabled = true;
        }

        public void ClearAllSelection(Control parent)
        {
            hdnfieldprospectusid.Value = "";
            _prospectusgeneralid = 0;
            //hdnfieldMasterProsectusId.Value = "";
            hdnfieldRaceName.Value = "";
            hdnfieldpropectusgeneralracenameid.Value = "";
            
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

                if ((x.GetType() == typeof(Label)))
                {

                    ((Label)(x)).Text = "";
                }
                if ((x.GetType() == typeof(CheckBox)))
                {
                    ((CheckBox)(x)).Checked=false;
                }
                //if ((x.GetType() == typeof(CheckBoxList)))
                //{
                //    ((CheckBoxList)(x)).Checked = false;
                //}
                if ((x.GetType() == typeof(RadioButtonList)))
                {

                    ((RadioButtonList)(x)).SelectedValue = "2";
                }

                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            DisableAllField(this, "Enable");
            DisableButton("Enable");
        }

        protected void BtnAddClick(object sender, EventArgs e)
        {
            SaveData();
            DisableAllField(this, "Disable");
            DisableButton("Disable");
        }

        protected void SaveData()
        {
            try
            {
                //CommonMethods sentencecase = new CommonMethods();
                var status = 0;
                status = new ProspectusBL().AddGeneralProspectus(
                           Convert.ToInt32(hdnfieldprospectusid.Value)
                          ,txtbxRaceDay.Text
                          ,string.Empty
                          ,rdbtnTimeSlot.SelectedItem.Text
                          ,Convert.ToInt32(chkbxMainRaceofDay.Checked)
                          ,txtbxSerialNumber.Text
                          , CommonMethods.ConvertInSentenceCase(txtbxYearofBirth.Text)
                           ,1,
                           "Update");

                if (status == 2)
                {
                    var message = "Duplicate Record.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else if (status > 0)
                {
                    var message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    //rdbtnTimeSlot.Items.FindByText("After Noon").Selected = true;
                    btnProspectusName.Text="Add";
                    DisableAllField(this, "Disable");
                    DisableButton("Disable");
                }
                else
                {
                    var message = "Incorrect Information.";
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


		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (hdnfieldprospectusid.Value != null)
				{
					var status = new ProspectusBL().AddGeneralProspectus(
						   Convert.ToInt32(hdnfieldprospectusid.Value)
						  , txtbxRaceDay.Text
						  , string.Empty
						  , rdbtnTimeSlot.SelectedItem.Text
						  , Convert.ToInt32(chkbxMainRaceofDay.Checked)
						  , txtbxSerialNumber.Text
						  , txtbxYearofBirth.Text
						   , 1,
						   "Delete");
					var message = "Record Deleted Successfully.";
					Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
					hdnfieldprospectusid.Value = string.Empty;
					ClearAllSelection(this);
					btnAdd.Text = "Add";
					btnProspectusName.Text = "Add";
					Session["ProspectusGeneralID"] = null;
					Session["ProspectusMasterName"] = null;
					Session.Remove("ProspectusGeneralMasterRaceName");
					Session.Remove("ProspectusGeneralRaceName");
					btnProspectusName.Enabled = true;
					Button8.Enabled = true;
					GvSponcer.DataSource = new DataTable();
					GvSponcer.DataBind();

					GvDistance.DataSource = new DataTable();
					GvDistance.DataBind();

					GvMomenttoPresenter.DataSource = new DataTable();
					GvMomenttoPresenter.DataBind();


					gvracetype.DataSource = new DataTable();
					gvracetype.DataBind();

					gvhandicapratingrange.DataSource = new DataTable();
					gvhandicapratingrange.DataBind();

					GvInterestedProfessionBackground.DataSource = new DataTable();
					GvInterestedProfessionBackground.DataBind();

					gridvwGeneralDates.DataSource = new DataTable();
					gridvwGeneralDates.DataBind();

					GVHWPCondition.DataSource = new DataTable();
					GVHWPCondition.DataBind();


					GvSeasonalCondition.DataSource = new DataTable();
					GvSeasonalCondition.DataBind();

					GvHandicapWeightAsperGender.DataSource = new DataTable();
					GvHandicapWeightAsperGender.DataBind();

					GvAgeCondition.DataSource = new DataTable();
					GvAgeCondition.DataBind();

					GvPermanentCondition.DataSource = new DataTable();
					GvPermanentCondition.DataBind();

					gveligiblehandicapratingrange.DataSource = new DataTable();
					gveligiblehandicapratingrange.DataBind();

					GvOtherCondition.DataSource = new DataTable();
					GvOtherCondition.DataBind();

					
					//gvclassgroup.DataBind();
					gvclassgroup.DataSource = new DataTable();
					gvclassgroup.DataBind();

					gvracehistory.DataSource = new DataTable();
					gvracehistory.DataBind();

					GvRaceCardCondition.DataSource = new DataTable();
					GvRaceCardCondition.DataBind();

					GvObservation.DataSource = new DataTable();
					GvObservation.DataBind();

					txtbxGeneralRaceName.Enabled = true;
				}
				else
				{
					var message = "Incorrect Information.";
					Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
				}

			}
			catch (Exception exception)
			{
				ErrorHandling.SendErrorToText(exception);
				var message = "Incorrect Information.";
				Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
			}

		}

		protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Prospectus_General"))
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
                    var dtErrorResult = new ProspectusBL().Import30(dt, "ProspectusGeneral", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Race Name");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_General.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
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
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }


        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                //using (DataSet ds = new ProspectusBL().GetProspectusNameWithCombination(0, "MasterMillion"))
                using (DataSet ds = new ProspectusBL().GetExport(0, "ProspectusGeneral"))
                {

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("GeneralRaceID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Prospectus General");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_General.xlsx");
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
            Session.Remove("ProspectusGeneralMasterRaceName");
            Session.Remove("ProspectusGeneralRaceName");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }

        //protected void txtbxRaceDate_OnTextChanged(object sender, EventArgs e)
        //{
        //    var txtvalue = sender as TextBox;
        //    if (txtvalue.Text != "__-__-____")
        //    {
        //        string value = txtvalue.Text;
        //        DateTime dt = Convert.ToDateTime(value);
        //        lblWeekDay.Text = dt.DayOfWeek.ToString();
        //    }
        //}
    }
}