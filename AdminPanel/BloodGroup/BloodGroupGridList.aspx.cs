using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class AllList_AdminPanel_BloodGroup_BloodGroupGridList : System.Web.UI.Page
{
    #region Page Load method
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/AllList/AdminPanel/Login.aspx");
            else
                FillGridViewList();
        }
    }
    #endregion Page Load method

    #region Fill Grid View From Database
    private void FillGridViewList()
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
                    ObjCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    ObjCmd.CommandText = "PR_BloodGroup_DeleteByPKUserID";

                    ObjCmd.CommandText = "PR_BloodGroup_SelectAllByUserID";
                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"];


                    SqlDataReader ObjSdr = ObjCmd.ExecuteReader();

                    if (ObjSdr.HasRows == true)
                    {
                        gvBloodGroup.DataSource = ObjSdr;
                        gvBloodGroup.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                if (Objconn.State == System.Data.ConnectionState.Open)
                    Objconn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Fill Grid View From Database

    #region Add Button Event
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AllList/AdminPanel/BloodGroup/BloodGroupAddEdit.aspx");
    }
    #endregion Add Button Event

    #region Delete Button Event
    protected void gvBloodGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteID")
        {
            if (e.CommandArgument != null)
            {
                DeleteID(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
    }
    #endregion Delete Button Event

    #region Delete By ID function
    private void DeleteID(Int32 BloodGroupID)
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
                    ObjCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    ObjCmd.CommandText = "PR_BloodGroup_DeleteByPKUserID";

                    ObjCmd.Parameters.Add("@BloodGroupID", SqlDbType.Int).Value = BloodGroupID;

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"];

                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alert()", true);

                    ObjCmd.ExecuteNonQuery();

                    FillGridViewList();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                if (Objconn.State == System.Data.ConnectionState.Open)
                    Objconn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Delete By ID function
}