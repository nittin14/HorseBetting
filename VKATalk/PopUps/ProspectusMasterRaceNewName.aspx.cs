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

    public partial class ProspectusMasterRaceNewName : System.Web.UI.Page
    {
        //MasterHorseBL Bl = new MasterHorseBL();
        int _userId = 1;
        private int _value = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
            if (!Request.QueryString["ProspectusID"].Equals(""))
            {
                _value = Convert.ToInt32(Request.QueryString["ProspectusID"]);
                hdnfldprospectusid.Value = Convert.ToString(_value);
            }
            if (!IsPostBack)
            {
                try
                {
                    if (_value != 0)
                    {
                        GetGridviewData();
                    }
                    else
                    {
                        btnSave.Text = "Add";
                    }
                }
                catch (Exception ex)
                {
                    object[] now = new object[] { "Page_Load (HorsePopup):", DateTime.Now, ", Issue Detail:", ex.Message + ex.StackTrace };
                    ErrorHandling.CheckEachSteps(string.Concat(now));
                    string message = "Incorrect Information.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);

                }
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
        public static List<string> AddCommentList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("RaceMasterCommentList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
            }
            return horseList;
        }


        private void GetGridviewData()
        {
            try
            {
                DataSet ds = new ProspectusBL().GetProspectusNameWithCombination(_value, "MasterRaceName");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvProspectus.DataSource = ds.Tables[0];
                    GvProspectus.DataBind();
                }
                else
                {
                    GvProspectus.DataSource = new DataTable();
                    GvProspectus.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        /// <summary>
        /// Bind Horse Name already exist in out Database
        /// </summary>
        private void BindData(DataTable dt)
        {
            try
            {
                DataSet ds = null;

                if (btnSave.Text.Equals("Add"))
                {
                    ds = new ProspectusBL().GetProspectusNameWithCombination(Convert.ToInt32(dt.Rows[0][1]), "MasterRaceNameCurrentInsert");
                }
                else
                {
                    ds = new ProspectusBL().GetProspectusNameWithCombination(Convert.ToInt32(dt.Rows[0][0]), "MasterRaceNameCurrentInsert");
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvProspectus.DataSource = ds.Tables[0];
                    GvProspectus.DataBind();
                }
                else
                {
                    GvProspectus.DataSource = new DataTable();
                    GvProspectus.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                int newprospectusId = 0;
                DataTable dt;
                if (btnSave.Text.Equals("Add"))
                {
                    //dt = this.Bl.HorseName(
                    //    0,
                    //    this.txtbxHorseName.Text,
                    //    this.txtbxHorseShortName.Text,
                    //    this.txtbxHorseAlias.Text,
                    //    this.txtbxDateofBirth.Text,
                    //    this.txtbxComment.Text,
                    //    this._userId,
                    //    "Insert");
                    //hdnfldhorseId.Value = Convert.ToString(dt.Rows[0][1]);

                    dt = new ProspectusBL().MasterRaceNameNew(
                        _value,
                        txtbxRaceName.Text,
                        txtbxRaceNameAlias.Text,
                        txtbxDateofNameChange.Text,
                        txtbxComment.Text,
                        1,
                        "Insert");
                }
                else
                {
                    dt = new ProspectusBL().MasterRaceNameNew(
                        (int)ViewState["GridViewRowID"],
                        txtbxRaceName.Text,
                        txtbxRaceNameAlias.Text,
                        txtbxDateofNameChange.Text,
                        txtbxComment.Text,
                        _userId,
                        "Update");

                }
                newprospectusId = Convert.ToInt32(dt.Rows[0][2]);
                if (newprospectusId == 4)
                {
                    message = "This Name Already Taken By Another Prospectus Master Race with Same Center, Sesaon.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearAllSelection(this);
                }
                else
                {
                    ClearAllSelection(this);
                    this.Page.ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "Popup",
                        this.btnSave.Text.Equals("Update")
                            ? "ShowPopup('Record Updated Successfully');"
                            : "ShowPopup('Record Added Successfully.');",
                        true);
                    if (btnSave.Text.Equals("Update"))
                        btnSave.Text = "Add";
                    BindData(dt);

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
                if (hdnfldprospectusid.Value != "")
                {
                    Session["ProspectusID"] = hdnfldprospectusid.Value;
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
        protected void GvProspectus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = GvProspectus.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                HiddenField hdnfieldparentid = (HiddenField)row.FindControl("hdnfieldParentID");
                var dataKey = GvProspectus.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
                    string strracename = hdnval.Value.Equals("&nbsp;") ? "" : hdnval.Value.Contains("&#39;") ? hdnval.Value.Replace("&#39;", "'") : hdnval.Value;
                    txtbxRaceName.Text = strracename.Replace("&amp;", "&");
                    string strracenamealias = row.Cells[2].Text.Equals("&nbsp;") ? "" : row.Cells[2].Text.Contains("&#39;") ? row.Cells[2].Text.Replace("&#39;", "'") : row.Cells[2].Text;
                    txtbxRaceNameAlias.Text = strracenamealias.Replace("&amp;", "&");
                    DataSet ds = new ProspectusBL().GetTillDateGridview(Convert.ToInt32(ViewState["GridViewRowID"]));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtbxDateofNameChange.Text = ds.Tables[0].Rows[0][0].ToString();
                    }

                    if (row.Cells[4].Text.Contains("&quot;"))
                    {
                        string strcomments = row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&quot;") ? row.Cells[4].Text.Replace("&quot;", "\"") : row.Cells[4].Text;
                        txtbxComment.Text = strcomments.Replace("&amp;", "&");
                    }
                    else
                    {
                        string strcomments1 = row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&#39;") ? row.Cells[4].Text.Replace("&#39;", "'") : row.Cells[4].Text;
                        txtbxComment.Text = strcomments1.Replace("&amp;", "&");
                    }

                    if (row.Cells[0].Text.Equals("Cr"))
                    {
                        //if (GvProspectus.Rows.Count == 1)
                        //{
                        //    string message = "Please add new name.";
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        //    btnSave.Text = "Add";
                        //    ClearAllSelection(this);
                        //}

                        if (hdnfieldparentid.Value.Equals("0"))
                        {
                            string message = "Please add new name.";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            btnSave.Text = "Add";
                            ClearAllSelection(this);
                        }
                    }
                    else
                    {
                        if (hdnfieldparentid.Value.Equals("0"))
                        {
                            string message = "Please select new name.";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            btnSave.Text = "Add";
                            ClearAllSelection(this);
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ViewState["GridViewRowID"].Equals(""))
                {
                    var dt = new ProspectusBL().MasterRaceNameNew(
                        (int)ViewState["GridViewRowID"],
                        txtbxRaceName.Text,
                        txtbxRaceNameAlias.Text,
                        txtbxDateofNameChange.Text,
                        txtbxComment.Text,
                        _userId,
                        "Delete");

                    ClearAllSelection(this);
                    BindData(dt);
                    btnSave.Text = "Add";
                    var message = "Record Deleted Successfully.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["GridViewRowID"] = "";
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
        protected void txtbxRaceName_OnTextChanged(object sender, EventArgs e)
        {
            if (txtbxRaceName.Text != "")
            {
                txtbxRaceNameAlias.Text = txtbxRaceName.Text;
            }
        }
    }
}