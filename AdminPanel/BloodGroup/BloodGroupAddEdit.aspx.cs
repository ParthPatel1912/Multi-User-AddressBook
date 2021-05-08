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

public partial class AllList_AdminPanel_BloodGroup_BloodGroupAddEdit : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/AllList/AdminPanel/Login.aspx");

            if (Request.QueryString["BloodGroupID"] == null)
            {
                lblTitle.Text = "Blood &nbsp; Group &nbsp; List &nbsp; Add";
            }
            else
            {
                lblTitle.Text = "Blood &nbsp; Group &nbsp; List &nbsp; Edit";
                FillBlooDGroupBox(Convert.ToInt32(Request.QueryString["BloodGroupID"].ToString().Trim()));
            }
        }
    }
    #endregion Page Load Event

    #region Save Button Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString BloodGroupName = SqlString.Null;
        String error = "";
        #endregion Local Variable

        #region Check For Error
        if (txtBloodGroupName.Text.Trim() == "")
        {
            error += "Enter Blood Group Name";
        }
        if (error != "")
        {
            lblError.Text = error;
            return;
        }
        #endregion Check For Error

        #region Assign Value
        if (txtBloodGroupName.Text.Trim() != "")
        {
            BloodGroupName = txtBloodGroupName.Text.Trim();
        }
        #endregion Assign Value

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


                    if (Request.QueryString["BloodGroupID"] == null)
                        ObjCmd.CommandText = "PR_BloodGroup_InsertByUserID";
                   
                    else
                    {
                        ObjCmd.CommandText = "PR_BloodGroup_UpdateByPKUserID";
                        
                        
                        ObjCmd.Parameters.Add("@BloodGroupID", SqlDbType.Int).Value = Request.QueryString["BloodGroupID"].ToString().Trim();

                        
                    }

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();
                    ObjCmd.Parameters.Add("@BloodGroupName", SqlDbType.VarChar, 10).Value = BloodGroupName;

                    ObjCmd.ExecuteNonQuery();

                    if (Request.QueryString["BloodGroupID"] == null)
                    {
                        //lblError.Text = "Data Added Successfully.......";
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "save()", true);
                        txtBloodGroupName.Text = "";
                    }
                    else
                        Response.Redirect("~/AllList/AdminPanel/BloodGroup/BloodGroupGridList.aspx");
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
        Response.Redirect("~/AllList/AdminPanel/BloodGroup/BloodGroupGridList.aspx");
    }
    #endregion Cancel Button Event

    #region Fill form data From Database
    private void FillBlooDGroupBox(SqlInt32 BlooDGroupID)
    {
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
                    ObjCmd.CommandText = "PR_BloodGroup_SelectByPKUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();
                    ObjCmd.Parameters.Add("@BloodGroupID", SqlDbType.Int).Value = BlooDGroupID;

                    using (SqlDataReader Objsdr = ObjCmd.ExecuteReader())
                    {
                        if (Objsdr.HasRows)
                        {
                            while (Objsdr.Read())
                            {
                                if (!Objsdr["BloodGroupName"].Equals(DBNull.Value))
                                    txtBloodGroupName.Text = Objsdr["BloodGroupName"].ToString().Trim();
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