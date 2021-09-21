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
    public partial class ProfessionalNewName : System.Web.UI.Page
    {
        ProfessionalBL Bl = new ProfessionalBL();
        int _userId = 1;
        private int _value = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.FindControl("NavigationMenu").Visible = false;
            if (!Request.QueryString["ProfessionalValue"].Equals(""))
            {
                _value = Convert.ToInt32(Request.QueryString["ProfessionalValue"]);
                hdnfldProfessionalId.Value = Convert.ToString(_value);
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
                    object[] now = new object[] { "Page_Load (Professional New Name):", DateTime.Now, ", Issue Detail:", ex.Message + ex.StackTrace };
                    ErrorHandling.CheckEachSteps(string.Concat(now));
                    string message = "Incorrect Information.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            btnSave.Text = "Add";
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
            DataTable dt = new ProfessionalBL().GetProfessionalNameAutoFiller("CommentList", prefixText);
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
                DataSet ds = Bl.GetProfessionalNameWithCombination(_value, "ProfessionalName");

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
                    ds = Bl.GetProfessionalNameWithCombination(Convert.ToInt32(dt.Rows[0][1]), "ProfessionalNewNameInsert");
                }
                else
                {
                    ds = Bl.GetProfessionalNameWithCombination(Convert.ToInt32(dt.Rows[0][0]), "ProfessionalNewNameUpdate");
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
                int newHorseId = 0;
                DataTable dt;
                if (btnSave.Text.Equals("Add"))
                {
                    dt = this.Bl.ProfessionalNewName(
                        _value,
                        this.txtbxProfessionalName.Text,
                        txtbxPNWithoutSolution.Text,
                        this.txtbxProfessionalShortName.Text,
                        this.txtbxProfessionalAlias.Text,
                        txtbxDateofNameChange.Text,
                        this.txtbxComment.Text,
                        this._userId,
                        "Insert");
                   
                }
                else
                {
                    dt = this.Bl.ProfessionalNewName(
                       (int)ViewState["GridViewRowID"],
                        this.txtbxProfessionalName.Text,
                        txtbxPNWithoutSolution.Text,
                        this.txtbxProfessionalShortName.Text,
                        this.txtbxProfessionalAlias.Text,
                        txtbxDateofNameChange.Text,
                       this.txtbxComment.Text,
                       this._userId,
                       "Update");
                }
                newHorseId = Convert.ToInt32(dt.Rows[0][2]);
                if (newHorseId == 2)
                {
                    message = "Record Already Exist.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else if (newHorseId == 3)
                {
                    message = "This Name Already Taken By Another Professional with Same Profile & Center.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        hdnfldProfessionalId.Value = Convert.ToString(dt.Rows[0][1]);
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
            catch (Exception ex)
            {
                // listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ViewState["GridViewRowID"].Equals(""))
                {
                    var dt = this.Bl.ProfessionalNewName(
                      (int)ViewState["GridViewRowID"],
                       this.txtbxProfessionalName.Text,
                       txtbxPNWithoutSolution.Text,
                       this.txtbxProfessionalShortName.Text,
                       this.txtbxProfessionalAlias.Text,
                       txtbxDateofNameChange.Text,
                      this.txtbxComment.Text,
                      this._userId,
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

        /// <summary>
        /// Close the window and pass the value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnfldProfessionalId.Value != "")
                {
                    Session["ProfessionalID"] = hdnfldProfessionalId.Value;
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
                    //txtbxProfessionalName.Text = hdnval.Value.Equals("&nbsp;") ? "" : hdnval.Value.Contains("&#39;") ? hdnval.Value.Replace("&#39;", "'") : hdnval.Value;
                    //txtbxPNWithoutSolution.Text = row.Cells[2].Text.Equals("&nbsp;") ? "" : row.Cells[2].Text.Contains("&#39;") ? row.Cells[2].Text.Replace("&#39;", "'") : row.Cells[2].Text;
                    //txtbxProfessionalShortName.Text = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&#39;") ? row.Cells[3].Text.Replace("&#39;", "'") : row.Cells[3].Text;
                    //txtbxProfessionalAlias.Text = row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&#39;") ? row.Cells[4].Text.Replace("&#39;", "'") : row.Cells[4].Text;
                    string strprofessionalname = hdnval.Value.Equals("&nbsp;") ? "" : hdnval.Value.Contains("&#39;") ? hdnval.Value.Replace("&#39;", "'") : hdnval.Value;
                    txtbxProfessionalName.Text = strprofessionalname.Replace("&amp;", "&");
                    string strsolutation = row.Cells[2].Text.Equals("&nbsp;") ? "" : row.Cells[2].Text.Contains("&#39;") ? row.Cells[2].Text.Replace("&#39;", "'") : row.Cells[2].Text;
                    txtbxPNWithoutSolution.Text = strsolutation.Replace("&amp;", "&");
                    string strprofessionalshortname = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&#39;") ? row.Cells[3].Text.Replace("&#39;", "'") : row.Cells[3].Text;
                    txtbxProfessionalShortName.Text = strprofessionalshortname.Replace("&amp;", "&");
                    string strprofessionalshortnamealias = row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&#39;") ? row.Cells[4].Text.Replace("&#39;", "'") : row.Cells[4].Text;
                    txtbxProfessionalAlias.Text = strprofessionalshortnamealias.Replace("&amp;", "&");

                    txtbxDateofNameChange.Text = GvHorseName.Rows[0].Cells[5].Text;
                     string strcomments= row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&#39;") ? row.Cells[6].Text.Replace("&#39;", "'") : row.Cells[6].Text;
                     txtbxComment.Text = strcomments.Replace("&amp;", "&");

                    if (row.Cells[0].Text.Equals("Cr"))
                    {
                        if (GvHorseName.Rows.Count == 1)
                        {
                            string message = "Please add new name.";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            btnSave.Text = "Add";
                            ClearAllSelection(this);
                        }
                    }
                    else
                    {
                        string message = "Please select new name.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        btnSave.Text = "Add";
                        ClearAllSelection(this);

                        //if (GvHorseName.Rows.Count == 1)
                        //{
                        //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please add new name.');", true);
                        //    string message = "Please add new name.";
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        //    btnSave.Text = "Add";
                        //    ClearAllSelection(this);
                        //}
                        //else
                        //{
                        //    string message = "Please add new name.";
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);

                        //    btnSave.Text = "Update";
                        //    ClearAllSelection(this);
                        //}

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
                this.txtbxProfessionalName.Text = textInfo.ToTitleCase(value.ToLower());
                this.txtbxProfessionalShortName.Text = this.txtbxProfessionalName.Text.Replace(" ", "");
                txtbxProfessionalAlias.Text = this.txtbxProfessionalName.Text;
				txtbxPNWithoutSolution.Text= this.txtbxProfessionalName.Text;
			}
        }

    }
}