using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllList_AdminPanel_Login : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Page Load Event

    #region Login Button Event
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString UserName = SqlString.Null;
        SqlString Password = SqlString.Null;
        string Error = "";
        #endregion Local Variable

        #region Check For Error
        if (txtUserName.Text.Trim() == "")
            Error += "Enter User Name<br/>";
        if (txtPassword.Text.Trim() == "")
            Error += "Enter Password";
        if(Error != "")
        {
            lblErrorMessage.Text = Error.ToString().Trim();
            return;
        }
        #endregion Check For Error

        #region Assign Value
        if (txtUserName.Text.Trim() != "")
            UserName = txtUserName.Text.Trim();
        if (txtPassword.Text.Trim() != "")
            Password = txtPassword.Text.Trim();
        #endregion Assign Value

        #region Open Connection
        using (SqlConnection Objconn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookCoonectionString"].ConnectionString))
        {
            try
            {
                if (Objconn.State != System.Data.ConnectionState.Open)
                    Objconn.Open();

                using (SqlCommand ObjCmd = Objconn.CreateCommand())
                {
                    ObjCmd.CommandType = CommandType.StoredProcedure;
                    ObjCmd.CommandText = "PR_User_SelectByUserNamePassword";
                    ObjCmd.Parameters.Add("@UserName",SqlDbType.VarChar,100).Value = UserName;
                    ObjCmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = Password;

                    using (SqlDataReader objSdr = ObjCmd.ExecuteReader())
                    {
                        if (objSdr.HasRows)
                        {
                            while (objSdr.Read())
                            {
                                if (!objSdr["UserID"].Equals(DBNull.Value))
                                    Session["UserID"] = objSdr["UserID"].ToString().Trim();
                                if (!objSdr["DisplayName"].Equals(DBNull.Value))
                                    Session["DisplayName"] = objSdr["DisplayName"].ToString().Trim();
                                if (!objSdr["PhotoPath"].Equals(DBNull.Value))
                                    Session["PhotoPath"] = objSdr["PhotoPath"];
                            }
                            Response.Redirect("~/AllList/AdminPanel/AboutDIET/AboutDIET.aspx");
                        }
                        else
                            lblErrorMessage.Text = "User Name or Password not mathched....";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }
            finally
            {
                if (Objconn.State == System.Data.ConnectionState.Open)
                    Objconn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Login Button Event

    #region Sign Up Button Event
    protected void btnSignUP_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AllList/AdminPanel/CreateAccount.aspx");
    }
    #endregion Sign Up Button Event
}