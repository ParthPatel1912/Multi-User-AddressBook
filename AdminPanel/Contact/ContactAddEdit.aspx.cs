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
using System.IO;

public partial class AllList_AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/AllList/AdminPanel/Login.aspx");

            if (Request.QueryString["ContactID"] == null)
            {
                lblTitle.Text = "Contact &nbsp; List &nbsp; Add";
                FillDropDownListState();
                FillDropDownListBloodGroup();
                FillDropDownListCity();
                FillDropDownListCountry();
                FillCheckBoxListContactCategory();
            }
            else
            {
                lblTitle.Text = "Contact &nbsp; List &nbsp; Edit";
                FillDropDownListState();
                FillDropDownListBloodGroup();
                FillDropDownListCity();
                FillDropDownListCountry();
                FillCheckBoxListContactCategory();
                FillContactData(Convert.ToInt32(Request.QueryString["ContactID"].ToString().Trim()));
            }
        }
    }
    #endregion Page Load Event

    #region Fill DropDown List of BloodGroup
    private void FillDropDownListBloodGroup()
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
                    ObjCmd.CommandText = "PR_BloodGroup_SelectAllByUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    using (SqlDataReader ObjSdr = ObjCmd.ExecuteReader())
                    {
                        if (ObjSdr.HasRows == true)
                        {
                            ddlBloodGroup.DataValueField = "BloodGroupID";
                            ddlBloodGroup.DataTextField = "BloodGroupName";

                            ddlBloodGroup.DataSource = ObjSdr;
                            ddlBloodGroup.DataBind();

                            ddlBloodGroup.Items.Insert(0, new ListItem("-- Select BloodGroup --", "-1"));
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
    #endregion Fill DropDown List of BloodGroup

    #region Fill DropDown List of City
    private void FillDropDownListCity()
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
                    ObjCmd.CommandText = "PR_City_SelectAllByUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    using (SqlDataReader ObjSdr = ObjCmd.ExecuteReader())
                    {
                        if (ObjSdr.HasRows == true)
                        {
                            ddlCity.DataValueField = "CityID";
                            ddlCity.DataTextField = "CityName";

                            ddlCity.DataSource = ObjSdr;
                            ddlCity.DataBind();

                            ddlCity.Items.Insert(0, new ListItem("-- Select city --", "-1"));
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
    #endregion Fill DropDown List of City

    #region Fill DropDown List of Country
    private void FillDropDownListCountry()
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
                        if (ObjSdr.HasRows == true)
                        {
                            ddlCountry.DataValueField = "CountryID";
                            ddlCountry.DataTextField = "CountryName";

                            ddlCountry.DataSource = ObjSdr;
                            ddlCountry.DataBind();

                            ddlCountry.Items.Insert(0, new ListItem("-- Select Country --", "-1"));
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

    #region Fill Check Box List of ContactCategory
    private void FillCheckBoxListContactCategory()
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
                    ObjCmd.CommandText = "PR_ContactCategory_SelectAllByUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    using (SqlDataReader ObjSdr = ObjCmd.ExecuteReader())
                    {
                        if (ObjSdr.HasRows == true)
                        {
                            chkCategory.DataValueField = "ContactCategoryName";
                            chkCategory.DataTextField = "ContactCategoryName";

                            chkCategory.DataSource = ObjSdr;
                            chkCategory.DataBind();
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
    #endregion Fill Check Box List of ContactCategory

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
        SqlInt32 StateID = SqlInt32.Null;
        SqlInt32 CityID = SqlInt32.Null;
        SqlInt32 CountryID = SqlInt32.Null;
        SqlInt32 BloodGroupID = SqlInt32.Null;
        SqlString ContactCategoryName = SqlString.Null;
        SqlString Address = SqlString.Null;
        SqlInt32 Age = SqlInt32.Null;
        SqlString Email = SqlString.Null;
        SqlString Gender = SqlString.Null;
        SqlString MobileNo = SqlString.Null;
        SqlString ContactName = SqlString.Null;
        SqlString PhoneNo = SqlString.Null;
        SqlString Pincode = SqlString.Null;
        SqlString Profession = SqlString.Null;
        SqlString PhotoPath = SqlString.Null;
        string chkselect = "";
        #endregion Local Variable

        for (int i = 0; i < chkCategory.Items.Count; i++)
        {
            if (chkCategory.Items[i].Selected)
            {
                if (chkselect == "")
                {
                    chkselect = chkCategory.Items[i].Value;
                }
                else
                {
                    chkselect += "," + chkCategory.Items[i].Value;
                }
            }
        }

        #region Check for Error
        if (txtContactAddress.Text.Trim() == "")
        {
            strError += "Enter Address<br/>";
        }
        if (txtContactAge.Text.Trim() == "")
        {
            strError += "Enter Age<br/>";
        }
        if (txtContactEmail.Text.Trim() == "")
        {
            strError += "Enter E-mail<br/>";
        }
        if (txtContactMobile.Text.Trim() == "")
        {
            strError += "Enter Mobile No.<br/>";
        }
        if (txtContactName.Text.Trim() == "")
        {
            strError += "Enter Name<br/>";
        }
        if (txtContactPhone.Text.Trim() == "")
        {
            strError += "Enter Phone No.<br/>";
        }
        if (txtContactPincode.Text.Trim() == "")
        {
            strError += "Enter City Pincode<br/>";
        }
        if (txtContactProfession.Text.Trim() == "")
        {
            strError += "Enter Profeession<br/>";
        }
        if (ddlBloodGroup.SelectedIndex == 0)
        {
            strError += "Select Blood Group<br/>";
        }
        if (ddlCity.SelectedIndex == 0)
        {
            strError += "Select City<br/>";
        }

        if (chkselect.ToString().Trim() == "")
        {
            strError += "Select at least one Category";
            chkCategory.Focus();
        }

        if (ddlCountry.SelectedIndex == 0)
        {
            strError += "Select Country<br/>";
        }
        if (ddlState.SelectedIndex == 0)
        {
            strError += "Select State   <br/>";
        }
        if (fuPhoto.HasFile == false)
        {
            strError += "Select File of Photo";
        }
        if (strError.Trim() != "")
        {
            lblError.Text = strError;
            return;
        }
        #endregion Check for Error

        #region Assign value

        if (ddlBloodGroup.SelectedIndex > 0)
        {
            BloodGroupID = Convert.ToInt32(ddlBloodGroup.SelectedValue);
        }
        if (ddlCity.SelectedIndex > 0)
        {
            CityID = Convert.ToInt32(ddlCity.SelectedValue);
        }

        if (chkselect != "")
        {
            ContactCategoryName = chkselect.ToString().Trim();
        }

        if (ddlCountry.SelectedIndex > 0)
        {
            CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        }
        if (ddlState.SelectedIndex > 0)
        {
            StateID = Convert.ToInt32(ddlState.SelectedValue);
        }
        if (txtContactAddress.Text.Trim() != "")
        {
            Address = txtContactAddress.Text.Trim();
        }
        if (txtContactAge.Text.Trim() != "")
        {
            Age = Convert.ToInt32(txtContactAge.Text.Trim());
        }
        if (txtContactEmail.Text.Trim() != "")
        {
            Email = txtContactEmail.Text.Trim();
        }
        if (rbMale.Checked)
        {
            Gender = rbMale.Text;
        }
        if (rbFemale.Checked)
        {
            Gender = rbFemale.Text;
        }
        if (txtContactMobile.Text.Trim() != "")
        {
            MobileNo = txtContactMobile.Text.Trim();
        }
        if (txtContactName.Text.Trim() != "")
        {
            ContactName = txtContactName.Text.Trim();
        }
        if (txtContactPhone.Text.Trim() != "")
        {
            PhoneNo = txtContactPhone.Text.Trim();
        }
        if (txtContactPincode.Text.Trim() != "")
        {
            Pincode = txtContactPincode.Text.Trim();
        }
        if (txtContactProfession.Text.Trim() != "")
        {
            Profession = txtContactProfession.Text.Trim();
        }

        if (fuPhoto.HasFile)
        {
            String location = "~/AllList/AdminPanel/Photo/";
            location += fuPhoto.FileName;

            String locationPhysical = Server.MapPath(location);

            if (File.Exists(locationPhysical))
            {
                File.Delete(locationPhysical);
            }

            fuPhoto.SaveAs(locationPhysical);

            PhotoPath = location;
        }

        #endregion Assign value

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

                    if (Request.QueryString["ContactID"] == null)
                    {
                        ObjCmd.CommandText = "PR_Contact_InsertByUserID";
                    }

                    else
                    {
                        ObjCmd.CommandText = "PR_Contact_UpdateByPKByUserID";
                        ObjCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = Request.QueryString["ContactID"].ToString().Trim();
                    }


                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    ObjCmd.Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = Address;
                    ObjCmd.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
                    ObjCmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = Email;
                    ObjCmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = Gender;
                    ObjCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 50).Value = MobileNo;
                    ObjCmd.Parameters.Add("@ContactName", SqlDbType.VarChar, 100).Value = ContactName;
                    ObjCmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar, 50).Value = PhoneNo;
                    ObjCmd.Parameters.Add("@Pincode", SqlDbType.VarChar, 10).Value = Pincode;
                    ObjCmd.Parameters.Add("@Profession", SqlDbType.VarChar, 50).Value = Profession;
                    ObjCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                    ObjCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                    ObjCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                    ObjCmd.Parameters.Add("@BloodGroupID", SqlDbType.Int).Value = BloodGroupID;
                    ObjCmd.Parameters.Add("@ContactCategoryName", SqlDbType.VarChar, 100).Value = ContactCategoryName;
                    ObjCmd.Parameters.Add("@PhotoPath", SqlDbType.VarChar, 200).Value = PhotoPath;

                    ObjCmd.ExecuteNonQuery();

                    if (Request.QueryString["ContactID"] == null)
                    {
                        //lblError.Text = "Data Added Successfully......";
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "save()", true);

                        ddlBloodGroup.SelectedIndex = 0;
                        ddlCity.SelectedIndex = 0;
                        chkCategory.ClearSelection();
                        ddlCountry.SelectedIndex = 0;
                        ddlState.SelectedIndex = 0;

                        txtContactAddress.Text = "";
                        txtContactAge.Text = "";
                        txtContactEmail.Text = "";
                        rbMale.Checked = true;
                        txtContactMobile.Text = "";
                        txtContactName.Text = "";
                        txtContactPhone.Text = "";
                        txtContactPincode.Text = "";
                        txtContactProfession.Text = "";
                    }
                    else
                    {
                        Response.Redirect("~/AllList/AdminPanel/Contact/ContactGridList.aspx");
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
        Response.Redirect("~/AllList/AdminPanel/Contact/ContactGridList.aspx");
    }
    #endregion Cancel Button Event

    #region Fill form data From Database
    private void FillContactData(SqlInt32 ContactID)
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
                    ObjCmd.CommandText = "PR_Contact_SelectByPKByUserID";

                    if (Session["UserID"] != null)
                        ObjCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString().Trim();

                    ObjCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;

                    using (SqlDataReader ObjSdr = ObjCmd.ExecuteReader())
                    {
                        if (ObjSdr.HasRows)
                        {
                            while (ObjSdr.Read())
                            {
                                if (!ObjSdr["Address"].Equals(DBNull.Value))
                                    txtContactAddress.Text = ObjSdr["Address"].ToString().Trim();
                                if (!ObjSdr["Age"].Equals(DBNull.Value))
                                    txtContactAge.Text = ObjSdr["Age"].ToString();
                                if (!ObjSdr["Email"].Equals(DBNull.Value))
                                    txtContactEmail.Text = ObjSdr["Email"].ToString().Trim();
                                if (ObjSdr["Gender"] != null)
                                {
                                    if (ObjSdr["Gender"].ToString() == "Male")
                                        rbMale.Checked = true;
                                    if (ObjSdr["Gender"].ToString() == "Female")
                                        rbFemale.Checked = true;
                                }
                                if (!ObjSdr["MobileNo"].Equals(DBNull.Value))
                                    txtContactMobile.Text = ObjSdr["MobileNo"].ToString().Trim();
                                if (!ObjSdr["ContactName"].Equals(DBNull.Value))
                                    txtContactName.Text = ObjSdr["ContactName"].ToString().Trim();
                                if (!ObjSdr["PhoneNo"].Equals(DBNull.Value))
                                    txtContactPhone.Text = ObjSdr["PhoneNo"].ToString().Trim();
                                if (!ObjSdr["Pincode"].Equals(DBNull.Value))
                                    txtContactPincode.Text = ObjSdr["Pincode"].ToString().Trim();
                                if (!ObjSdr["Profession"].Equals(DBNull.Value))
                                    txtContactProfession.Text = ObjSdr["Profession"].ToString().Trim();

                                if (!ObjSdr["BloodGroupID"].Equals(DBNull.Value))
                                    ddlBloodGroup.SelectedValue = ObjSdr["BloodGroupID"].ToString().Trim();
                                if (!ObjSdr["CityID"].Equals(DBNull.Value))
                                    ddlCity.SelectedValue = ObjSdr["CityID"].ToString().Trim();
                                if (!ObjSdr["CountryID"].Equals(DBNull.Value))
                                    ddlCountry.SelectedValue = ObjSdr["CountryID"].ToString().Trim();
                                if (!ObjSdr["StateID"].Equals(DBNull.Value))
                                    ddlState.SelectedValue = ObjSdr["StateID"].ToString().Trim();


                                if (ObjSdr["ContactCategoryName"] != null)
                                {
                                    string word = ObjSdr["ContactCategoryName"].ToString().Trim();
                                    string[] words = word.Split(',');

                                    foreach (ListItem finalitem in chkCategory.Items)
                                    {
                                        foreach (var final in words)
                                        {
                                            if (finalitem.Value == final.ToString().Trim())
                                                finalitem.Selected = true;
                                        }
                                    }
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
                if (Objconn.State == ConnectionState.Open)
                    Objconn.Close();
            }
        }
        #endregion Open Connection
    }
    #endregion Fill form data From Database
}