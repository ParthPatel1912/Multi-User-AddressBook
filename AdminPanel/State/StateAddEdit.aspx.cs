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

public partial class AllList_AdminPanel_State_StateAddEdit : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
            Response.Redirect("~/AllList/AdminPanel/Login.aspx");

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["StateID"] == null)
            {
                lblTitle.Text = "State &nbsp; List &nbsp; Add";
                FillgridView();
            }
            else
            {
                lblTitle.Text = "State &nbsp; List &nbsp; Edit";
                FillgridView();
                FillStateData(Convert.ToInt32(Request.QueryString["StateID"].ToString().Trim()));

            }
        }
    }
    #endregion Page Load Event

    #region Fill DropDown List of Country
    private void FillgridView()
    {
        #region Open Connection
        using (SqlConnection ObjConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookCoonectionString"].ConnectionString))
        {
            try
            {
                if (ObjConn.State != System.Data.ConnectionState.Open)
                    ObjConn.Open();

                using (SqlCommand ObjCmd = ObjConn.CreateCommand())
                {
                    ObjCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    ObjCmd.CommandText = "PR_Country_SelectAllByUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    using (SqlDataReader ObjSdr = ObjCmd.ExecuteReader())
                    {
                        if (ObjSdr.HasRows)
                        {
                            ddlCountry.DataValueField = "CountryID";
                            ddlCountry.DataTextField = "CountryName";

                            ddlCountry.DataSource = ObjSdr;
                            ddlCountry.DataBind();

                            ddlCountry.Items.Insert(0, new ListItem(" -- Select Country --", "-1"));
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
                if (ObjConn.State == System.Data.ConnectionState.Open)
                    ObjConn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Fill DropDown List of Country

    #region Save Button Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        String error = "";
        SqlString StateName = SqlString.Null;
        SqlInt32 CountryId = SqlInt32.Null;
        #endregion Local Variable

        #region Check for Error
        if (txtStateName.Text.Trim() == "")
            error += "Enter State Name";
        if (error != "")
        {
            lblError.Text = error;
            return;
        }
        #endregion Check for Error

        #region Assign value
        if (txtStateName.Text.Trim() != "")
            StateName = txtStateName.Text.Trim();
        if (ddlCountry.SelectedIndex > 0)
            CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
        #endregion Assign value

        #region Open Connection
        using (SqlConnection ObjConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookCoonectionString"].ConnectionString))
        {
            try
            {
                if (ObjConn.State != System.Data.ConnectionState.Open)
                    ObjConn.Open();

                using (SqlCommand ObjCmd = ObjConn.CreateCommand())
                {
                    ObjCmd.CommandType = System.Data.CommandType.StoredProcedure;


                    if (Request.QueryString["StateID"] == null)
                    {
                        ObjCmd.CommandText = "PR_State_InsertByUserID";
                    }
                    else
                    {
                        ObjCmd.CommandText = "PR_State_UpdateByPKByUserID";
                        ObjCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = Request.QueryString["StateID"].ToString().Trim();
                    }

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    ObjCmd.Parameters.Add("@StateName", SqlDbType.VarChar, 50).Value = StateName;
                    ObjCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryId;

                    ObjCmd.ExecuteNonQuery();



                    if (Request.QueryString["StateID"] == null)
                    {
                        //lblError.Text = "Data Added Successfully.......";
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "save()", true);
                        txtStateName.Text = "";
                        ddlCountry.SelectedIndex = 0;
                    }
                    else
                        Response.Redirect("~/AllList/AdminPanel/State/StateGridList.aspx");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                if (ObjConn.State == System.Data.ConnectionState.Open)
                    ObjConn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Save Button Event

    #region Cancel Button Event
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AllList/AdminPanel/State/StateGridList.aspx");
    }
    #endregion Cancel Button Event

    #region Fill form data from database
    private void FillStateData(SqlInt32 StateID)
    {
        #region Open Connection
        using (SqlConnection ObjConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookCoonectionString"].ConnectionString))
        {
            try
            {
                if (ObjConn.State != System.Data.ConnectionState.Open)
                    ObjConn.Open();

                using (SqlCommand ObjCmd = ObjConn.CreateCommand())
                {
                    ObjCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    ObjCmd.CommandText = "PR_State_SelectByPKByUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    ObjCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;

                    using (SqlDataReader Objsdr = ObjCmd.ExecuteReader())
                    {
                        if (Objsdr.HasRows)
                        {
                            while (Objsdr.Read())
                            {
                                if (!Objsdr["StateName"].Equals(DBNull.Value))
                                {
                                    txtStateName.Text = Objsdr["StateName"].ToString().Trim();
                                }

                                if (!Objsdr["CountryID"].Equals(DBNull.Value))
                                {
                                    ddlCountry.SelectedValue = Objsdr["CountryID"].ToString().Trim();
                                }
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
                if (ObjConn.State == System.Data.ConnectionState.Open)
                    ObjConn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Fill form data from database
}