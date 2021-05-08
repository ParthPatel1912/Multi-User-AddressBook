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

public partial class AllList_AdminPanel_Cascade_Cascade : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDownListCountry();
        }
    }
    #endregion Page Load Event

    #region Fill DropDown List of City
    private void FillDropDownListCityByStateID(SqlInt32 StateID)
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
                    ObjCmd.CommandText = "PR_City_SelectAllByStateID";
                    ObjCmd.Parameters.Add("StateID", SqlDbType.Int).Value = StateID;

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
                        else
                        {
                            ddlCity.Items.Clear();
                            ddlCity.Items.Insert(0, new ListItem("-- Select City --", "-1"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
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
                    ObjCmd.CommandText = "PR_Country_SelectAll";

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

    #region Fill DropDown List of state
    private void FillDropDownListStateByCountryID(SqlInt32 CountryID)
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
                    ObjCmd.CommandText = "PR_State_SelectAllByCountryID";
                    ObjCmd.Parameters.Add("CountryID", SqlDbType.Int).Value = CountryID;

                    using (SqlDataReader ObjSdr = ObjCmd.ExecuteReader())
                    {
                        if (ObjSdr.HasRows)
                        {
                            ddlState.DataSource = ObjSdr;

                            ddlState.DataValueField = "StateID";
                            ddlState.DataTextField = "StateName";

                            ddlState.DataBind();

                            ddlState.Items.Insert(0, new ListItem("-- Select State --", "-1"));
                        }
                        else
                        {
                            ddlState.Items.Clear();
                            ddlState.Items.Insert(0, new ListItem("-- Select State --", "-1"));

                            ddlCity.Items.Clear();
                            ddlCity.Items.Insert(0, new ListItem("-- Select City --", "-1"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
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

    #region ddlCountry Load Event
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCountry.SelectedIndex > 0)
        {
            FillDropDownListStateByCountryID(Convert.ToInt32(ddlCountry.SelectedValue));
        }
        else
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("-- Select State --", "-1"));

            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("-- Select City --", "-1"));
        }
    }
    #endregion ddlCountry Load Event

    #region ddlState Load Event
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlState.SelectedIndex > 0)
        {
            FillDropDownListCityByStateID(Convert.ToInt32(ddlState.SelectedValue));
        }
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("-- Select City --", "-1"));
        }
    }
    #endregion ddlState Load Event

    #region ddlCity Load Event
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion ddlCity Load Event

    #region Print Event
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if(ddlCountry.SelectedIndex > 0)
        {
            lblAnswer.Text = "Your selected Country is " + ddlCountry.SelectedItem.Text + " and <br/>";
        }

        if (ddlState.SelectedIndex > 0)
        {
            lblAnswer.Text = lblAnswer.Text + "Your selected State is " + ddlState.SelectedItem.Text + " and <br/>";
        }

        if (ddlCity.SelectedIndex > 0)
        {
            lblAnswer.Text = lblAnswer.Text +  "Your selected City is " + ddlCity.SelectedItem.Text + ".";
        }

       
    }
    #endregion Print Event
}