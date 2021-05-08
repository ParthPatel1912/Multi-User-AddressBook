using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllList_AdminPanel_CreateAccount : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Page Load Event

    #region Create Button Event
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString UserName = SqlString.Null;
        SqlString Password = SqlString.Null;
        SqlString ConfirmPassword = SqlString.Null;
        SqlString DisplayName = SqlString.Null;
        SqlString MobileNo = SqlString.Null;
        SqlString PhotoPath = SqlString.Null;
        string Error = "";
        int Check = 0;
        #endregion Local Variable

        #region Check For Error
        if (txtUserName.Text.Trim() == "")
            Error += "Enter User Name<br/>";
        if (txtPassword.Text.Trim() == "")
            Error += "Enter Password";
        if (txtConfirmPassword.Text.Trim() == "")
            Error += "Enter Password";
        if (txtDisplayName.Text.Trim() == "")
            Error += "Enter Display Name<br/>";
        if (txtMobileNo.Text.Trim() == "")
            Error += "Enter Mobile No";
        if (fuProfile.HasFile == false)
        {
            Error += "Select File of Photo";
        }
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
        if (txtDisplayName.Text.Trim() != "")
            DisplayName = txtDisplayName.Text.Trim();
        if (txtMobileNo.Text.Trim() != "")
            MobileNo = txtMobileNo.Text.Trim();

        if (fuProfile.HasFile)
        {
            String location = "~/AllList/AdminPanel/Photo/";
            location += fuProfile.FileName;

            String locationPhysical = Server.MapPath(location);

            if (File.Exists(locationPhysical))
            {
                File.Delete(locationPhysical);
            }

            fuProfile.SaveAs(locationPhysical);

            PhotoPath = location;
        }

        #endregion Assign Value

        Check = CheckUserName(UserName);

        if (Check == 1)
        {
            if (txtPassword.Text.Trim() == txtConfirmPassword.Text.Trim())
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
                            ObjCmd.CommandText = "PR_User_CreateAccout";
                            ObjCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = UserName;
                            ObjCmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = Password;
                            ObjCmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 100).Value = DisplayName;
                            ObjCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 50).Value = MobileNo;
                            ObjCmd.Parameters.Add("@PhotoPath", SqlDbType.VarChar, 200).Value = PhotoPath;

                            ObjCmd.ExecuteNonQuery();
                            lblErrorMessage.Text = "Data successfully added.......";

                            txtDisplayName.Text = "";
                            txtMobileNo.Text = "";
                            txtPassword.Text = "";
                            txtUserName.Text = "";

                            Response.Redirect("~/AllList/AdminPanel/Login.aspx");
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
            else
                lblErrorMessage.Text = "Confirm password is not matched with password..?";
        }
        else
            txtUserName.Text = "";
    }
    #endregion Save Button Event

    #region For Check Duplicate User Name
    private int CheckUserName(SqlString Username)
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
                    ObjCmd.CommandText = "PR_User_SelectByUserName";

                    ObjCmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = Username;

                    using (SqlDataReader objSDR = ObjCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            lblErrorMessage.Text = "User Name already Exits!!!!";
                            return -1;
                        }
                        else
                        {
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
        return -1;
    }
    #endregion For Check Duplicate User Name
}