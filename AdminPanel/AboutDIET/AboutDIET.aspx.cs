 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllList_AdminPanel_AboutDIET : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if(!Page.IsPostBack)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/AllList/AdminPanel/Login.aspx");
        }
    }
}