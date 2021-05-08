using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllList_MasterPage : System.Web.UI.MasterPage
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            if (Session["DisplayName"] != null)
                lblDisplayName.Text = Session["DisplayName"].ToString().Trim();
            if (Session["PhotoPath"] != null)
                imgProfile.ImageUrl = Session["PhotoPath"].ToString().Trim();
        }
    }
    #endregion Page Load Event

    #region Logout Button Event
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/AllList/AdminPanel/Login.aspx");
    }
    #endregion Logout Button Event
}