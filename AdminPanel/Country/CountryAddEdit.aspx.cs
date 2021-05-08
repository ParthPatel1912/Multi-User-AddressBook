using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlTypes;
using System.Data;

public partial class AllList_AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/AllList/AdminPanel/Login.aspx");

            if (Request.QueryString["CountryID"] == null)
            {
                lblTitle.Text = "Country &nbsp; List &nbsp; Add";
            }
            else
            {
                lblTitle.Text = "Contact &nbsp; List &nbsp; Edit";
                FillCountryForm(Convert.ToInt32(Request.QueryString["CountryID"].ToString().Trim()));
            }
        }
    }
    #endregion Page Load Event

    #region Save Button Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString CountryName = SqlString.Null;
        SqlString CountryCode = SqlString.Null;
        String error = "";
        #endregion Local Variable

        #region Check for Error
        if (txtCountryName.Text.Trim() == "")
        {
            error += "Enter Country Name<br/>";
        }
        if (txtCountryCode.Text.Trim() == "")
        {
            error += "Enter Country Code";
        }
        if (error != "")
        {
            lblError.Text = error;
            return;
        }
        #endregion Check for Error

        #region Assign value
        if (txtCountryName.Text.Trim() != "")
        {
            CountryName = txtCountryName.Text.Trim();
        }
        if (txtCountryCode.Text.Trim() != "")
        {
            CountryCode = txtCountryCode.Text.Trim();
        }
        #endregion Assign value

        #region Open Connection
        using (SqlConnection ObjConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookCoonectionString"].ConnectionString))
        {
            try
            {
                if (ObjConn.State != ConnectionState.Open)
                {
                    ObjConn.Open();
                }

                using (SqlCommand ObjCmd = ObjConn.CreateCommand())
                {
                    ObjCmd.CommandType = CommandType.StoredProcedure;

                    if (Request.QueryString["CountryID"] == null)
                    {
                        ObjCmd.CommandText = "PR_Country_InsertByUserID";
                    }

                    else
                    {
                        ObjCmd.CommandText = "PR_Country_UpdateByPKByUserID";

                        ObjCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = Request.QueryString["CountryID"].ToString().Trim();
                    }

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    ObjCmd.Parameters.Add("@CountryName", SqlDbType.VarChar, 50).Value = CountryName;
                    ObjCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 10).Value = CountryCode;

                    ObjCmd.ExecuteNonQuery();



                    if (Request.QueryString["CountryID"] == null)
                    {
                        //lblError.Text = "Data Added Successfully.......";
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "save()", true);
                        txtCountryName.Text = "";
                        txtCountryCode.Text = "";
                    }
                    else
                        Response.Redirect("~/AllList/AdminPanel/Country/CountyGridList.aspx");


                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                if (ObjConn.State == ConnectionState.Open)
                    ObjConn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Save Button Event

    #region Cancel Button Event
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AllList/AdminPanel/Country/CountyGridList.aspx");
    }
    #endregion Cancel Button Event

    #region Fill form data From Database
    private void FillCountryForm(SqlInt32 CountryID)
    {
        #region Open Connection
        using (SqlConnection ObjConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookCoonectionString"].ConnectionString))
        {
            try
            {
                if (ObjConn.State != ConnectionState.Open)
                    ObjConn.Open();

                using (SqlCommand ObjCmd = ObjConn.CreateCommand())
                {
                    ObjCmd.CommandType = CommandType.StoredProcedure;
                    ObjCmd.CommandText = "PR_Country_SelectByPKByUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    ObjCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;

                    using (SqlDataReader ObjSdr = ObjCmd.ExecuteReader())
                    {
                        if (ObjSdr.HasRows)
                        {
                            while (ObjSdr.Read())
                            {
                                if (!ObjSdr["CountryName"].Equals(DBNull.Value))
                                    txtCountryName.Text = ObjSdr["CountryName"].ToString().Trim();
                                if (!ObjSdr["CountryCode"].Equals(DBNull.Value))
                                    txtCountryCode.Text = ObjSdr["CountryCode"].ToString().Trim();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                if (ObjConn.State == ConnectionState.Open)
                    ObjConn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Fill form data From Database
   
}