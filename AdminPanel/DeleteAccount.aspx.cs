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

public partial class AllList_AdminPanel_DeleteAccount : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Page Load Event

    #region Delete Button Event
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString UserName = SqlString.Null;
        SqlString Password = SqlString.Null;
        SqlString MobileNo = SqlString.Null;
        string Error = "";
        int Check = 0;
        #endregion Local Variable

        #region Check For Error
        if (txtUserName.Text.Trim() == "")
            Error += "Enter User Name<br/>";
        if (txtPassword.Text.Trim() == "")
            Error += "Enter Password";
        if (txtMobileNo.Text.Trim() == "")
            Error += "Enter Mobile No";
        if (Error != "")
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
        if (txtMobileNo.Text.Trim() != "")
            MobileNo = txtMobileNo.Text.Trim();
        #endregion Assign Value

        Check = CheckUserName(UserName,Password,MobileNo);

        if (Check == -1)
        {
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
                        ObjCmd.CommandText = "PR_User_DeleteByUserNamePasswordMobileNo";
                        ObjCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = UserName;
                        ObjCmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = Password;
                        ObjCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 50).Value = MobileNo;

                        ObjCmd.ExecuteNonQuery();
                        lblErrorMessage.Text = "Data Deleted.......";

                        txtMobileNo.Text = "";
                        txtPassword.Text = "";
                        txtUserName.Text = "";

                        Response.Redirect("~/AllList/AdminPanel/CreateAccount.aspx");
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
    }
    #endregion Delete Button Event

    #region For Check User is valid or not
    private int CheckUserName(SqlString Username, SqlString Password, SqlString MobileNo)
    {
        //String ConnectionStr = ConfigurationManager.ConnectionStrings["MultiUserAddressBookCoonectionString"].ConnectionString;

        #region Open Connection
        using (SqlConnection Objconn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookCoonectionString"].ConnectionString))
        {
            try
            {
                if (Objconn.State != ConnectionState.Open)
                    Objconn.Open();

                using (SqlCommand ObjCmd = Objconn.CreateCommand())
                {
                    ObjCmd.CommandType = CommandType.StoredProcedure;
                    ObjCmd.CommandText = "PR_User_SelectByUserNameMobileNoPassword";

                    ObjCmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = Username;
                    ObjCmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;
                    ObjCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = MobileNo;

                    using (SqlDataReader objSDR = ObjCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            return -1;
                        }
                        else
                        {
                            lblErrorMessage.Text = "User Name and Password does not match!!!!";
                            return 1;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }
            finally
            {
                if (Objconn.State == ConnectionState.Open)
                    Objconn.Close();
            }
        }
        #endregion Open Connection
        return 1;
    }
    #endregion For Check User is valid or not
}