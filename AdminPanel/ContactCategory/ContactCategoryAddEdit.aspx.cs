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

public partial class AllList_AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/AllList/AdminPanel/Login.aspx");

            if (Request.QueryString["ContactCategoryID"] == null)
            {
                lblTitle.Text = "Contact &nbsp; Category &nbsp; List &nbsp; Add";
            }
            else
            {
                lblTitle.Text = "Contact &nbsp; Category &nbsp; List &nbsp; Edit";
                FillContactcategoryForm(Convert.ToInt32(Request.QueryString["ContactCategoryID"].ToString().Trim()));
            }
        }
    }
    #endregion Page Load Event

    #region Save Button Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString ContactCategoryName = SqlString.Null;
        String error = "";
        #endregion Local Variable

        #region Check for Error
        if (txtContactCategoryName.Text.Trim() == "")
        {
            error += "Enter Contact Category Name";
        }
        if (error != "")
        {
            lblError.Text = error;
            return;
        }
        #endregion Check for Error

        #region Assign value
        if (txtContactCategoryName.Text.Trim() != "")
        {
            ContactCategoryName = txtContactCategoryName.Text.Trim();
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

                    if (Request.QueryString["ContactCategoryID"] == null)
                    {
                        ObjCmd.CommandText = "PR_ContactCategory_InsertByUserID";
                    }

                    else
                    {

                        ObjCmd.CommandText = "PR_ContactCategory_UpdateByPKByUserID";

                        ObjCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = Request.QueryString["ContactCategoryID"].ToString().Trim();
                    }

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    ObjCmd.Parameters.Add("@ContactCategoryName", SqlDbType.VarChar, 50).Value = ContactCategoryName;

                    ObjCmd.ExecuteNonQuery();


                    if (Request.QueryString["ContactCategoryID"] == null)
                    {
                        //lblError.Text = "Data Added Successfully.......";
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "save()", true);
                        txtContactCategoryName.Text = "";
                    }
                    else
                        Response.Redirect("~/AllList/AdminPanel/ContactCategory/ContactCategoryGridList.aspx");


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
        Response.Redirect("~/AllList/AdminPanel/ContactCategory/ContactCategoryGridList.aspx");
    }
    #endregion Cancel Button Event

    #region Fill form data From Database
    private void FillContactcategoryForm(SqlInt32 ContactCategoryID)
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
                    ObjCmd.CommandText = "PR_ContactCategory_SelectByPByUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    ObjCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;

                    using (SqlDataReader ObjSdr = ObjCmd.ExecuteReader())
                    {
                        if (ObjSdr.HasRows)
                        {
                            while (ObjSdr.Read())
                            {
                                if (!ObjSdr["ContactCategoryName"].Equals(DBNull.Value))
                                    txtContactCategoryName.Text = ObjSdr["ContactCategoryName"].ToString().Trim();
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