using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchIncident : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["StaffName"] != null)
        {
            StaffName.Text = Session["StaffName"].ToString();
            StaffRole.Text = Session["StaffRole"].ToString();
            StaffEmail.Text = Session["Email"].ToString();
            lbStaffId.Text = Session["StaffId"].ToString();
        }
        if (Session["StaffName"] == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Session has been logged out, please login again'); window.location = 'HomePage.aspx';", true);

        }

        if (!IsPostBack)
        {
            // this code will execute only first time when the page loads
            // loading of CMO officers list
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
            {
                String role = "CMO Officer";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT StaffId, StaffName FROM Staff where Role ='" + role + "'";
                //cmd1.CommandText = "SELECT Acc_No FROM InsuranceAccount WHERE Cust_Id='" + strUserID + "'";
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(dt);

                //da1.SelectCommand = cmd1;
                //da1.Fill(dt1);


                //Count would be 1 if the userID and password matches
                if (dt.Rows.Count > 0)
                {
                    ddlCMOName.DataSource = dt;
                    ddlCMOName.DataTextField = "StaffName";
                    ddlCMOName.DataValueField = "StaffId";

                    // bind the control to the datasource
                    ddlCMOName.DataBind();
                    ddlCMOName.Items.Insert(0, "Select One");
                    con.Close();

                }
            }


            // loading of Operators list
            DataTable dt1 = new DataTable();

            using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
            {
                String role = "Call Operator";
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con1;
                cmd1.CommandText = "SELECT StaffId, StaffName FROM Staff where Role ='" + role + "'";
                //cmd1.CommandText = "SELECT Acc_No FROM InsuranceAccount WHERE Cust_Id='" + strUserID + "'";
                con1.Open();

                SqlDataAdapter da1 = new SqlDataAdapter();

                da1.SelectCommand = cmd1;
                da1.Fill(dt1);

                //da1.SelectCommand = cmd1;
                //da1.Fill(dt1);


                //Count would be 1 if the userID and password matches
                if (dt1.Rows.Count > 0)
                {
                    ddlOperator.DataSource = dt1;
                    ddlOperator.DataTextField = "StaffName";
                    ddlOperator.DataValueField = "StaffId";

                    // bind the control to the datasource
                    ddlOperator.DataBind();
                    ddlOperator.Items.Insert(0, "Select One");
                    con1.Close();

                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(BindGrid() != null)
        {
            
            GVSearchResults.DataSource = BindGrid();
            GVSearchResults.DataBind();
            ViewState["dt"] = BindGrid();
            ViewState["sort"] = "ASC";

        }
        else{
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('No Records Found!');", true);
            GVSearchResults.DataSource = BindGrid();
            GVSearchResults.DataBind();
        }
        

    }

    private DataTable BindGrid()
    {
        // for a range of dates only
        if (tbEndDate.Text != "" && tbStartDate.Text != "" && ddlCMOName.SelectedValue == "Select One" && ddlOperator.SelectedValue == "Select One")
        {
            DateTime endDate = DateTime.Parse(tbEndDate.Text);
            DateTime startDate = DateTime.Parse(tbStartDate.Text);
            // when start date is smaller than end date
            if (endDate.Date >= startDate.Date)
            {
                String startDateStr = startDate.ToString("yyyy/MM/dd");
                String endDateStr = endDate.ToString("yyyy/MM/dd");
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT IncidentID, NatureOfEmergency, NameOfEmergency, CallerName, CallBackNo, " +
                                "LocationOfEmergency, TimeOfEmergency, IncidentCreatedTime, IncidentCreatedDate, StaffName," +
                                "AlertedCMOName, Status, PotentialCrisis FROM Incidents where IncidentCreatedDate >= '" + startDateStr + "' AND IncidentCreatedDate <='" + endDateStr + "'";

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    //da1.SelectCommand = cmd1;
                    //da1.Fill(dt1);

                    //Count would be 1 if the userID and password matches
                    if (dt.Rows.Count > 0)
                    {
                        return dt;

                    }
                    else
                    {
                        return null;
                        //GVSearchResults.DataSource = null;
                        //GVSearchResults.DataBind();
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('No Records Found!');", true);
                    }

                }
            }

            // when start date is bigger than end date
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter a Start Date that is smaller than End Date');", true);
                return null;
            }
        }

        // by CMO officers only
        else if (tbEndDate.Text == "" && tbStartDate.Text == "" && ddlCMOName.SelectedValue != "Select One" && ddlOperator.SelectedValue == "Select One")
        {
            String CMOName = ddlCMOName.SelectedItem.ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT IncidentID, NatureOfEmergency, NameOfEmergency, CallerName, CallBackNo, " +
                            "LocationOfEmergency, TimeOfEmergency, IncidentCreatedTime, IncidentCreatedDate, StaffName," +
                            "AlertedCMOName, Status, PotentialCrisis FROM Incidents where AlertedCMOName = '" + CMOName + "'";

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(dt);

                //da1.SelectCommand = cmd1;
                //da1.Fill(dt1);

                //Count would be 1 if the userID and password matches
                if (dt.Rows.Count > 0)
                {
                    return dt;

                }
                else
                {
                    return null;
                }

            }

        }

        // by Operators only
        else if (tbEndDate.Text == "" && tbStartDate.Text == "" && ddlCMOName.SelectedValue == "Select One" && ddlOperator.SelectedValue != "Select One")
        {
            String OperatorName = ddlOperator.SelectedItem.ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT IncidentID, NatureOfEmergency, NameOfEmergency, CallerName, CallBackNo, " +
                            "LocationOfEmergency, TimeOfEmergency, IncidentCreatedTime, IncidentCreatedDate, StaffName," +
                            "AlertedCMOName, Status, PotentialCrisis FROM Incidents where StaffName = '" + OperatorName + "'";

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(dt);

                //da1.SelectCommand = cmd1;
                //da1.Fill(dt1);

                //Count would be 1 if the userID and password matches
                if (dt.Rows.Count > 0)
                {
                    return dt;

                }
                else
                {
                    return null;
                }

            }

        }
        // by Operators and CMO officers
        else if (tbEndDate.Text == "" && tbStartDate.Text == "" && ddlCMOName.SelectedValue != "Select One" && ddlOperator.SelectedValue != "Select One")
        {
            String OperatorName = ddlOperator.SelectedItem.ToString();
            String CMOName = ddlCMOName.SelectedItem.ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT IncidentID, NatureOfEmergency, NameOfEmergency, CallerName, CallBackNo, " +
                            "LocationOfEmergency, TimeOfEmergency, IncidentCreatedTime, IncidentCreatedDate, StaffName," +
                            "AlertedCMOName, Status, PotentialCrisis FROM Incidents where StaffName = '" + OperatorName + "' AND AlertedCMOName = '" + CMOName + "'";

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(dt);

                //da1.SelectCommand = cmd1;
                //da1.Fill(dt1);

                //Count would be 1 if the userID and password matches
                if (dt.Rows.Count > 0)
                {
                    return dt;

                }
                else
                {
                    return null;
                }

            }

        }

        // for a range of dates and by CMO officers
        else if (tbEndDate.Text != "" && tbStartDate.Text != "" && ddlCMOName.SelectedValue != "Select One" && ddlOperator.SelectedValue == "Select One")
        {
            DateTime endDate = DateTime.Parse(tbEndDate.Text);
            DateTime startDate = DateTime.Parse(tbStartDate.Text);
            String CMOName = ddlCMOName.SelectedItem.ToString();
            // when start date is smaller than end date
            if (endDate.Date >= startDate.Date)
            {
                String startDateStr = startDate.ToString("yyyy/MM/dd");
                String endDateStr = endDate.ToString("yyyy/MM/dd");
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT IncidentID, NatureOfEmergency, NameOfEmergency, CallerName, CallBackNo, " +
                                "LocationOfEmergency, TimeOfEmergency, IncidentCreatedTime, IncidentCreatedDate, StaffName," +
                                "AlertedCMOName, Status, PotentialCrisis FROM Incidents where IncidentCreatedDate >= '" + startDateStr + "' AND IncidentCreatedDate <='" + endDateStr + "' AND AlertedCMOName ='" + CMOName + "'";

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    //da1.SelectCommand = cmd1;
                    //da1.Fill(dt1);

                    //Count would be 1 if the userID and password matches
                    if (dt.Rows.Count > 0)
                    {
                        return dt;

                    }
                    else
                    {
                        return null;
                    }

                }
            }

            // when start date is bigger than end date
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter a Start Date that is smaller than End Date');", true);
                return null;
            }

        }

        // for a range of dates and by Call Operators
        else if (tbEndDate.Text != "" && tbStartDate.Text != "" && ddlOperator.SelectedValue != "Select One" && ddlCMOName.SelectedValue == "Select One")
        {
            DateTime endDate = DateTime.Parse(tbEndDate.Text);
            DateTime startDate = DateTime.Parse(tbStartDate.Text);
            String OperatorName = ddlOperator.SelectedItem.ToString();
            // when start date is smaller than end date
            if (endDate.Date >= startDate.Date)
            {
                String startDateStr = startDate.ToString("yyyy/MM/dd");
                String endDateStr = endDate.ToString("yyyy/MM/dd");
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT IncidentID, NatureOfEmergency, NameOfEmergency, CallerName, CallBackNo, " +
                                "LocationOfEmergency, TimeOfEmergency, IncidentCreatedTime, IncidentCreatedDate, StaffName," +
                                "AlertedCMOName, Status, PotentialCrisis FROM Incidents where IncidentCreatedDate >= '" + startDateStr + "' AND IncidentCreatedDate <='" + endDateStr + "' AND StaffName ='" + OperatorName + "'";

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    //da1.SelectCommand = cmd1;
                    //da1.Fill(dt1);

                    //Count would be 1 if the userID and password matches
                    if (dt.Rows.Count > 0)
                    {
                        return dt;

                    }
                    else
                    {
                        return null;
                    }

                }
            }

            // when start date is bigger than end date
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter a Start Date that is smaller than End Date');", true);
                return null;
            }

        }



        // for a range of dates and by CMO officers by operators
        else if (tbEndDate.Text != "" && tbStartDate.Text != "" && ddlCMOName.SelectedValue != "Select One" && ddlOperator.SelectedValue != "Select One")
        {
            DateTime endDate = DateTime.Parse(tbEndDate.Text);
            DateTime startDate = DateTime.Parse(tbStartDate.Text);
            String CMOName = ddlCMOName.SelectedItem.ToString();
            String Operator = ddlOperator.SelectedItem.ToString();
            // when start date is smaller than end date
            if (endDate.Date >= startDate.Date)
            {
                String startDateStr = startDate.ToString("yyyy/MM/dd");
                String endDateStr = endDate.ToString("yyyy/MM/dd");
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT IncidentID, NatureOfEmergency, NameOfEmergency, CallerName, CallBackNo, " +
                                "LocationOfEmergency, TimeOfEmergency, IncidentCreatedTime, IncidentCreatedDate, StaffName," +
                                "AlertedCMOName, Status, PotentialCrisis FROM Incidents where IncidentCreatedDate >= '" + startDateStr +
                                "' AND IncidentCreatedDate <='" + endDateStr + "' AND AlertedCMOName ='" + CMOName +
                                "' AND StaffName = '" + Operator + "'";

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    //da1.SelectCommand = cmd1;
                    //da1.Fill(dt1);

                    //Count would be 1 if the userID and password matches
                    if (dt.Rows.Count > 0)
                    {
                        return dt;

                    }
                    else
                    {
                        return null;
                    }

                }
            }

            // when start date is bigger than end date
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter a Start Date that is smaller than End Date');", true);
                return null;
            }

        }

        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter at least one of the search criteria');", true);
            return null;
        }
    }


    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GVSearchResults.PageIndex = e.NewPageIndex;
        if (Session["SortedView"] != null)
        {
            GVSearchResults.DataSource = Session["SortedView"];
            GVSearchResults.DataBind();
        }
        else
        {
            GVSearchResults.DataSource = BindGrid();
            GVSearchResults.DataBind();
        }

        
    }

   
    protected void SortRecords(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";

        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";

        }
        DataView sortedView = new DataView(BindGrid());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        GVSearchResults.DataSource = sortedView;
        GVSearchResults.DataBind();

    }

    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }

   



}