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


public partial class AllList_AdminPanel_ForgetPassword : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Page Load Event

    #region Submit Button Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString UserName = SqlString.Null;
        SqlString MobileNo = SqlString.Null;
        string Error = "";
        #endregion Local Variable

        #region Check For Error
        if (txtUserName.Text.Trim() == "")
            Error += "Enter User Name";
        if (txtMobileNo.Text.Trim() == "")
            Error += "Enter Mobile No";
        if (Error != "")
        {
            lblErrorMessage.Text = Error.ToString().Trim();
            return;
        }
        #endregion Check For Error

        #region Assign Value
        if (txtMobileNo.Text.Trim() != "")
            MobileNo = txtMobileNo.Text.Trim();
        if (txtUserName.Text.Trim() != "")
            UserName = txtUserName.Text.Trim();
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
                    ObjCmd.CommandText = "PR_User_SelectByUserNameMobileNo";

                    ObjCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 50).Value = MobileNo;
                    ObjCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = UserName;

                    using (SqlDataReader objSdr = ObjCmd.ExecuteReader())
                    {
                        if (objSdr.HasRows)
                        {
                            while(objSdr.Read())
                            {
                                if (!objSdr["UserName"].Equals(DBNull.Value))
                                    txtNewUserName.Text = objSdr["UserName"].ToString().Trim();
                                if (!objSdr["Password"].Equals(DBNull.Value))
                                    txtNewPassword.Text = objSdr["Password"].ToString().Trim();
                                if (!objSdr["DisplayName"].Equals(DBNull.Value))
                                    txtNewDisplayName.Text = objSdr["DisplayName"].ToString().Trim();
                                if (!objSdr["MobileNo"].Equals(DBNull.Value))
                                    txtNewMobileNo.Text = objSdr["MobileNo"].ToString().Trim();
                                if(!objSdr["UserID"].Equals(DBNull.Value))
                                    Session["UserID"] = objSdr["UserID"].ToString().Trim();
                            }
                            lblErrorMessage.Text = "Data found successfully.......";
                        }
                        else
                            lblErrorMessage.Text = "Data not found.......";
                    }
                    txtUserName.Text = "";
                    txtMobileNo.Text = "";
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
    #endregion Submit Button Event

    #region Change Button Event
    protected void btnChange_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString UserName = SqlString.Null;
        SqlString MobileNo = SqlString.Null;
        SqlString Password = SqlString.Null;
        SqlString DisplayName = SqlString.Null;
        SqlString PhotoPath = SqlString.Null;
        string Error = "";
        #endregion Local Variable

        #region Check For Error
        if (txtNewUserName.Text.Trim() == "")
            Error += "Enter User Name";
        if (txtNewMobileNo.Text.Trim() == "")
            Error += "Enter Mobile No";
        if (txtNewDisplayName.Text.Trim() == "")
            Error += "Enter Display Name";
        if (txtNewPassword.Text.Trim() == "")
            Error += "Enter Password";
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
        if (txtNewMobileNo.Text.Trim() != "")
            MobileNo = txtNewMobileNo.Text.Trim();
        if (txtNewUserName.Text.Trim() != "")
            UserName = txtNewUserName.Text.Trim();
        if (txtNewDisplayName.Text.Trim() != "")
            DisplayName = txtNewDisplayName.Text.Trim();
        if (txtNewPassword.Text.Trim() != "")
            Password = txtNewPassword.Text.Trim();

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
                    ObjCmd.CommandText = "PR_User_UpdateByPK";

                    ObjCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 50).Value = MobileNo;
                    ObjCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = UserName;
                    ObjCmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = Password;
                    ObjCmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 100).Value = DisplayName;
                    ObjCmd.Parameters.Add("@PhotoPath", SqlDbType.VarChar, 200).Value = PhotoPath;
                    ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"];

                    ObjCmd.ExecuteNonQuery();
                    lblErrorMessage.Text = "Data successfully Updated.......";

                    txtNewDisplayName.Text = "";
                    txtNewMobileNo.Text = "";
                    txtNewPassword.Text = "";
                    txtNewUserName.Text = "";

                    Response.Redirect("~/AllList/AdminPanel/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }
            finally
            {
                Session.Clear();
                if (Objconn.State == System.Data.ConnectionState.Open)
                    Objconn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Change Button Event
}