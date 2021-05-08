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

public partial class AllList_City_CityAddList : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/AllList/AdminPanel/Login.aspx");

            if (Request.QueryString["CityID"] == null)
            {
                lblTitle.Text = "City &nbsp; List &nbsp; Add";
                FillDropDownListState();
            }
            else
            {
                lblTitle.Text = "City &nbsp; List &nbsp; Edit";
                FillDropDownListState();
                FillCityData(Convert.ToInt32(Request.QueryString["CityID"].ToString().Trim()));
            }
        }
    }
    #endregion Page Load Event

    #region Fill DropDown List of state
    private void FillDropDownListState()
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
                    ObjCmd.CommandText = "PR_State_SelectAllByUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    using (SqlDataReader ObjSdr = ObjCmd.ExecuteReader())
                    {
                        if (ObjSdr.HasRows == true)
                        {
                            ddlState.DataValueField = "StateID";
                            ddlState.DataTextField = "StateName";

                            ddlState.DataSource = ObjSdr;
                            ddlState.DataBind();

                            ddlState.Items.Insert(0, new ListItem("-- Select State --", "-1"));
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
    #endregion Fill DropDown List of state

    #region Save Button Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        String strError = "";
        SqlInt32 StateId = SqlInt32.Null;
        //SqlInt32 CountryID = SqlInt32.Null;
        SqlString CityName = SqlString.Null;
        SqlString Pincode = SqlString.Null;
        SqlString STDCode = SqlString.Null;
        #endregion Local Variable

        #region Check for Error (Server side Validation)
        if (txtCityName.Text.Trim() == "")
        {
            strError += "Enter City Name<br/>";
        }
        if (txtPincode.Text.Trim() == "")
        {
            strError += "Enter Pincode<br/>";
        }
        if (txtSTDCode.Text.Trim() == "")
        {
            strError += "Enter STD Code<br/>";
        }
        if (ddlState.SelectedIndex == 0)
        {
            strError += "Select State   <br/>";
        }
        if (strError.Trim() != "")
        {
            lblError.Text = strError.ToString();
            return;
        }
        #endregion Check for Error (Server side Validation)

        #region Assign Value
        if (ddlState.SelectedIndex > 0)
        {
            StateId = Convert.ToInt32(ddlState.SelectedValue);
        }
        if (txtCityName.Text.Trim() != "")
        {
            CityName = txtCityName.Text.Trim();
        }
        if (txtPincode.Text.Trim() != "")
        {
            Pincode = txtPincode.Text.Trim();
        }
        if (txtSTDCode.Text.Trim() != "")
        {
            STDCode = txtSTDCode.Text.Trim();
        }
        #endregion Assign Value

        #region Open Connection
        using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookCoonectionString"].ConnectionString))
        {
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                using (SqlCommand ObjCmd = objConn.CreateCommand())
                {
                    ObjCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    if (Request.QueryString["CityID"] == null)
                    {
                        ObjCmd.CommandText = "PR_City_InsertByUserID";
                    }

                    else
                    {
                        ObjCmd.CommandText = "PR_City_UpdateByPKUserID";
                        ObjCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = Request.QueryString["CityID"].ToString().Trim();
                    }

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    ObjCmd.Parameters.Add("@CityName", SqlDbType.VarChar, 50).Value = CityName;
                    ObjCmd.Parameters.Add("@Pincode", SqlDbType.VarChar, 10).Value = Pincode;
                    ObjCmd.Parameters.Add("@STPCode", SqlDbType.VarChar, 10).Value = STDCode;
                    ObjCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateId;

                    ObjCmd.ExecuteNonQuery();

                    if (Request.QueryString["CityID"] == null)
                    {
                        //lblError.Text = "Data Added Successfully......";
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "save()", true);
                        ddlState.SelectedIndex = 0;
                        txtCityName.Text = "";
                        txtPincode.Text = "";
                        txtSTDCode.Text = "";
                    }
                    else
                    {
                        Response.Redirect("~/AllList/AdminPanel/City/CityGridList.aspx");
                    }


                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                if (objConn.State == System.Data.ConnectionState.Open)
                    objConn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Save Button Event

    #region Cancel Button Event
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AllList/AdminPanel/City/CityGridList.aspx");
    }
    #endregion Cancel Button Event

    #region Fill form data From Database
    private void FillCityData(SqlInt32 CityID)
    {
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
                    ObjCmd.CommandText = "PR_City_SelectByPKUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"];

                    ObjCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;

                    using (SqlDataReader ObjSdr = ObjCmd.ExecuteReader())
                    {
                        if (ObjSdr.HasRows)
                        {
                            while (ObjSdr.Read())
                            {
                                if (!ObjSdr["CityName"].Equals(DBNull.Value))
                                    txtCityName.Text = ObjSdr["CityName"].ToString().Trim();
                                if (!ObjSdr["Pincode"].Equals(DBNull.Value))
                                    txtPincode.Text = ObjSdr["Pincode"].ToString().Trim();
                                if (!ObjSdr["STDCode"].Equals(DBNull.Value))
                                    txtSTDCode.Text = ObjSdr["STDCode"].ToString().Trim();
                                if (!ObjSdr["StateID"].Equals(DBNull.Value))
                                    ddlState.SelectedValue = ObjSdr["StateID"].ToString().Trim();
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
                if (Objconn.State == ConnectionState.Open)
                    Objconn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Fill form data From Database
}