using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IncidentDetailsPage : System.Web.UI.Page
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

            // this query is for binding of the dropdown list for CMO officers
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
            // end of query for binding of the dropdown list for CMO officers


           
            // taking the incident id from the ViewIncidentListPage
            if (Request.QueryString["IncidentId"] != null)
            {
                String incidentId = Request.QueryString["IncidentId"];
                lbIncidentId.Text = incidentId;
                DataTable dt1 = new DataTable();

                using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
                {

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = con1;
                    cmd1.CommandText = "SELECT * FROM Incidents where IncidentID ='" + incidentId + "'";

                    cmd1.Parameters.AddWithValue("@IncidentID", incidentId);

                    con1.Open();

                    SqlDataAdapter da1 = new SqlDataAdapter();

                    da1.SelectCommand = cmd1;
                    da1.Fill(dt1);

                    // Retrieve data successfully
                    if (dt1.Rows.Count > 0)
                    {


                        tbCallOperator.Text = dt1.Rows[0][12].ToString();
                        
                       
                        tbcallername.Text = dt1.Rows[0][1].ToString();
                        tbcallernric.Text = dt1.Rows[0][2].ToString();
                        tbcallbackno.Text = dt1.Rows[0][3].ToString();
                        ddlNature.SelectedValue = dt1.Rows[0][21].ToString();
                        if (ddlNature.SelectedValue == "Select a nature")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a nature first"));
                        }
                        if (ddlNature.SelectedValue == "National")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
                            ddlNameOfEmergency.Items.Insert(1, new ListItem("Invasion"));
                            ddlNameOfEmergency.Items.Insert(2, new ListItem("Hostile Attack"));
                            ddlNameOfEmergency.Items.Insert(3, new ListItem("Terrorist Attack"));
                        }
                        else if (ddlNature.SelectedValue == "Individual")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
                            ddlNameOfEmergency.Items.Insert(1, new ListItem("Murder"));
                            ddlNameOfEmergency.Items.Insert(2, new ListItem("Robbery"));
                            ddlNameOfEmergency.Items.Insert(3, new ListItem("Assault"));
                            ddlNameOfEmergency.Items.Insert(4, new ListItem("Rape"));
                            ddlNameOfEmergency.Items.Insert(5, new ListItem("Burglary"));
                            ddlNameOfEmergency.Items.Insert(6, new ListItem("Medical Emergency"));
                            ddlNameOfEmergency.Items.Insert(7, new ListItem("Fire"));
                        }

                        else if (ddlNature.SelectedValue == "Natural Disaster")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
                            ddlNameOfEmergency.Items.Insert(1, new ListItem("Flood"));
                            ddlNameOfEmergency.Items.Insert(2, new ListItem("Earthquake"));
                            ddlNameOfEmergency.Items.Insert(3, new ListItem("Wild Fire"));
                            ddlNameOfEmergency.Items.Insert(4, new ListItem("Hurricane"));
                            ddlNameOfEmergency.Items.Insert(5, new ListItem("Landslide"));
                            ddlNameOfEmergency.Items.Insert(6, new ListItem("Tsunami"));
                            ddlNameOfEmergency.Items.Insert(7, new ListItem("Thunderstorm"));
                            ddlNameOfEmergency.Items.Insert(8, new ListItem("Volcano"));
                        }
                        else if (ddlNature.SelectedValue == "Virus Outbreak")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
                            ddlNameOfEmergency.Items.Insert(1, new ListItem("Bird Flu"));
                            ddlNameOfEmergency.Items.Insert(2, new ListItem("SARS"));
                            ddlNameOfEmergency.Items.Insert(3, new ListItem("Influenza"));
                            ddlNameOfEmergency.Items.Insert(4, new ListItem("Dengue"));
                            ddlNameOfEmergency.Items.Insert(5, new ListItem("H1N1 Swine Flu"));
                            ddlNameOfEmergency.Items.Insert(6, new ListItem("Ebola"));

                        }

                        else if (ddlNature.SelectedValue == "Supernatural")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
                            ddlNameOfEmergency.Items.Insert(1, new ListItem("Zombie Attack"));
                            ddlNameOfEmergency.Items.Insert(2, new ListItem("Alien Attack"));
                            ddlNameOfEmergency.Items.Insert(3, new ListItem("Vampire Attack"));
                            ddlNameOfEmergency.Items.Insert(4, new ListItem("Monster Attack"));

                        }
                        if (ddlNature.SelectedValue != "Supernatural")
                        {
                            Zombie.Visible = false;
                            ddlZombieType.Visible = false;
                            NoOfZombies.Visible = false;
                            tbNoOfZombies.Visible = false;
                            Zombie.Enabled = false;
                            ddlZombieType.Enabled = false;
                            NoOfZombies.Enabled = false;
                            tbNoOfZombies.Enabled = false;
                        }
                        ddlNameOfEmergency.SelectedValue = dt1.Rows[0][22].ToString();
                        tbemergencytime.Text = dt1.Rows[0][5].ToString();
                        tbemergencylocation.Text = dt1.Rows[0][4].ToString();
                        tbnoofcasualties.Text = dt1.Rows[0][7].ToString();
                        tbnoofdeaths.Text = dt1.Rows[0][8].ToString();
                        descriptionofemergency.Text = dt1.Rows[0][9].ToString();
                        if (dt1.Rows[0][10].ToString() == "True")
                        {
                            ddlPotentialCrisis.SelectedValue = "Yes";
                        }
                        if (dt1.Rows[0][10].ToString() == "False")
                        {
                            ddlPotentialCrisis.SelectedValue = "No";
                        }

                        if (dt1.Rows[0][14].ToString() != "")
                        {
                            ddlCMOName.Items.FindByText(dt1.Rows[0][14].ToString()).Selected = true;
                        }
                        ddlStatus.Items.FindByText(dt1.Rows[0][15].ToString()).Selected = true;
                        tbLastUpdated.Text = dt1.Rows[0][23].ToString();
                        tbUpdatedBy.Text = dt1.Rows[0][24].ToString();

                        tbCallOperator.ReadOnly = true;
                        tbcallername.ReadOnly = true;
                        tbcallernric.ReadOnly = true;
                        tbcallbackno.ReadOnly = true;
                        ddlNature.Enabled = false;
                        ddlNameOfEmergency.Enabled = false;
                        tbemergencytime.ReadOnly = true;
                        tbemergencylocation.ReadOnly = true;
                        tbnoofcasualties.ReadOnly = true;
                        tbnoofdeaths.ReadOnly = true;
                        descriptionofemergency.ReadOnly = true;
                        ddlPotentialCrisis.Enabled = false;
                        ddlCMOName.Enabled = false;
                        ddlStatus.Enabled = false;
                        tbLastUpdated.ReadOnly = true;
                        tbUpdatedBy.ReadOnly = true;
                        Session.Timeout = 30;

                    }

                    else
                    {
                        // No data retrieved
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('No Data has been found');", true);

                    }

                }

            }
        }

        else
        {


            
        }
    }

    protected void Edit_Click(object sender, EventArgs e)
    {
        
        ddlPotentialCrisis.Enabled = true;
        ddlCMOName.Enabled = true;
        ddlStatus.Enabled = true;
        Edit.Visible = false;
        Edit.Enabled = false;
        Cancel.Enabled = true;
        Cancel.Visible = true;
        Update.Enabled = true;
        Update.Visible = true;
    }

    protected void ddlNature_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlNature.SelectedValue == "Select a nature")
        {
            ddlNameOfEmergency.Items.Clear();
            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a nature first"));
        }
        if (ddlNature.SelectedValue == "National")
        {
            ddlNameOfEmergency.Items.Clear();
            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
            ddlNameOfEmergency.Items.Insert(1, new ListItem("Invasion"));
            ddlNameOfEmergency.Items.Insert(2, new ListItem("Hostile Attack"));
            ddlNameOfEmergency.Items.Insert(3, new ListItem("Terrorist Attack"));
        }
        else if (ddlNature.SelectedValue == "Individual")
        {
            ddlNameOfEmergency.Items.Clear();
            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
            ddlNameOfEmergency.Items.Insert(1, new ListItem("Murder"));
            ddlNameOfEmergency.Items.Insert(2, new ListItem("Robbery"));
            ddlNameOfEmergency.Items.Insert(3, new ListItem("Assault"));
            ddlNameOfEmergency.Items.Insert(4, new ListItem("Rape"));
            ddlNameOfEmergency.Items.Insert(5, new ListItem("Burglary"));
            ddlNameOfEmergency.Items.Insert(6, new ListItem("Medical Emergency"));
            ddlNameOfEmergency.Items.Insert(7, new ListItem("Fire"));
        }

        else if (ddlNature.SelectedValue == "Natural Disaster")
        {
            ddlNameOfEmergency.Items.Clear();
            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
            ddlNameOfEmergency.Items.Insert(1, new ListItem("Flood"));
            ddlNameOfEmergency.Items.Insert(2, new ListItem("Earthquake"));
            ddlNameOfEmergency.Items.Insert(3, new ListItem("Wild Fire"));
            ddlNameOfEmergency.Items.Insert(4, new ListItem("Hurricane"));
            ddlNameOfEmergency.Items.Insert(5, new ListItem("Landslide"));
            ddlNameOfEmergency.Items.Insert(6, new ListItem("Tsunami"));
            ddlNameOfEmergency.Items.Insert(7, new ListItem("Thunderstorm"));
            ddlNameOfEmergency.Items.Insert(8, new ListItem("Volcano"));
        }
        else if (ddlNature.SelectedValue == "Virus Outbreak")
        {
            ddlNameOfEmergency.Items.Clear();
            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
            ddlNameOfEmergency.Items.Insert(1, new ListItem("Bird Flu"));
            ddlNameOfEmergency.Items.Insert(2, new ListItem("SARS"));
            ddlNameOfEmergency.Items.Insert(3, new ListItem("Influenza"));
            ddlNameOfEmergency.Items.Insert(4, new ListItem("Dengue"));
            ddlNameOfEmergency.Items.Insert(5, new ListItem("H1N1 Swine Flu"));
            ddlNameOfEmergency.Items.Insert(6, new ListItem("Ebola"));

        }

        else if (ddlNature.SelectedValue == "Supernatural")
        {
            ddlNameOfEmergency.Items.Clear();
            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
            ddlNameOfEmergency.Items.Insert(1, new ListItem("Zombie Attack"));
            ddlNameOfEmergency.Items.Insert(2, new ListItem("Alien Attack"));
            ddlNameOfEmergency.Items.Insert(3, new ListItem("Vampire Attack"));
            ddlNameOfEmergency.Items.Insert(4, new ListItem("Monster Attack"));

        }
        if (ddlNature.SelectedValue != "Supernatural")
        {
            Zombie.Visible = false;
            ddlZombieType.Visible = false;
            NoOfZombies.Visible = false;
            tbNoOfZombies.Visible = false;
            Zombie.Enabled = false;
            ddlZombieType.Enabled = false;
            NoOfZombies.Enabled = false;
            tbNoOfZombies.Enabled = false;
        }
    }

    protected void ddlNameOfEmergency_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlNameOfEmergency.SelectedValue == "Select a name")
        {
            Zombie.Visible = false;
            ddlZombieType.Visible = false;
            NoOfZombies.Visible = false;
            tbNoOfZombies.Visible = false;
            Zombie.Enabled = false;
            ddlZombieType.Enabled = false;
            NoOfZombies.Enabled = false;
            tbNoOfZombies.Enabled = false;

        }
        if (ddlNameOfEmergency.SelectedValue == "Zombie Attack")
        {
            Zombie.Visible = true;
            ddlZombieType.Visible = true;
            NoOfZombies.Visible = true;
            tbNoOfZombies.Visible = true;
            Zombie.Enabled = true;
            ddlZombieType.Enabled = true;
            NoOfZombies.Enabled = true;
            tbNoOfZombies.Enabled = true;
            Zombie.Text = "Type of Zombie(s)";
            NoOfZombies.Text = "Estimated Number of Zombie(s)";

        }
        if (ddlNameOfEmergency.SelectedValue == "Alien Attack")
        {
            Zombie.Visible = true;
            ddlZombieType.Visible = true;
            NoOfZombies.Visible = true;
            tbNoOfZombies.Visible = true;
            Zombie.Enabled = true;
            ddlZombieType.Enabled = true;
            NoOfZombies.Enabled = true;
            tbNoOfZombies.Enabled = true;
            Zombie.Text = "Type of Alien(s)";
            NoOfZombies.Text = "Estimated Number of Alien(s)";

        }
        if (ddlNameOfEmergency.SelectedValue == "Vampire Attack")
        {
            Zombie.Visible = true;
            ddlZombieType.Visible = true;
            NoOfZombies.Visible = true;
            tbNoOfZombies.Visible = true;
            Zombie.Enabled = true;
            ddlZombieType.Enabled = true;
            NoOfZombies.Enabled = true;
            tbNoOfZombies.Enabled = true;
            Zombie.Text = "Type of Vampire(s)";
            NoOfZombies.Text = "Estimated Number of Vampire(s)";

        }
        if (ddlNameOfEmergency.SelectedValue == "Monster Attack")
        {
            Zombie.Visible = true;
            ddlZombieType.Visible = true;
            NoOfZombies.Visible = true;
            tbNoOfZombies.Visible = true;
            Zombie.Enabled = true;
            ddlZombieType.Enabled = true;
            NoOfZombies.Enabled = true;
            tbNoOfZombies.Enabled = true;
            Zombie.Text = "Type of Monster(s)";
            NoOfZombies.Text = "Estimated Number of Monster(s)";

        }

    }





    protected void Update_Click(object sender, EventArgs e)
    {
        String superBeings = "";
        if (ddlNameOfEmergency.SelectedValue == "Supernatural")
        {
            if (ddlNameOfEmergency.SelectedValue == "Zombie Attack")
            {
                superBeings = "Zombie";
            }
            else if (ddlNameOfEmergency.SelectedValue == "Alien Attack")
            {
                superBeings = "Alien";
            }
            else if (ddlNameOfEmergency.SelectedValue == "Vampire Attack")
            {
                superBeings = "Vampire";
            }
            else
            {
                superBeings = "Monster";
            }
        }
        DateTime dt2 = DateTime.Now;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
        {
            String incidentId = Request.QueryString["IncidentId"];
            String nameOfCaller = tbcallername.Text;
            String callerNric = tbcallernric.Text;
            int callBackNo;
            int.TryParse(tbcallbackno.Text, out callBackNo);
            String emergencyLocation = tbemergencylocation.Text;
            DateTime emergencytime = DateTime.Parse(tbemergencytime.Text);
            String time = emergencytime.ToString("hh:mm:ss tt");
            int estimatedNoOfInjuries = 0;
            int estimatedNoOfDeath = 0;
            String descOfEmergency = descriptionofemergency.Text;
            Boolean potentialCrisis = false;
            String staffId = lbStaffId.Text;
            String staffName = StaffName.Text;
            String alertedCMOId = "";
            String alertedCMOName = "";
            String status = ddlStatus.SelectedValue;
            int estimatedNoOfBeings = 0;
            DateTime incidentCreatedTime1 = DateTime.Now;
            String incidentCreatedTime = incidentCreatedTime1.ToString("hh:mm:ss tt");
            DateTime incidentCreatedDate = DateTime.Now.Date;
            String natureOfEmergency = ddlNature.SelectedValue;
            String nameOfEmergency = ddlNameOfEmergency.SelectedValue;
            DateTime updatedTime = DateTime.Now;
            String updatedTime1 = updatedTime.ToString("MM/dd/yyyy hh:mm:ss tt");

            if (tbnoofcasualties.Text != null)
            {
                int.TryParse(tbnoofcasualties.Text, out estimatedNoOfInjuries);
            }
            if (tbnoofdeaths.Text != null)
            {
                int.TryParse(tbnoofdeaths.Text, out estimatedNoOfDeath);
            }

            if (ddlPotentialCrisis.SelectedValue == "Yes")
            {
                potentialCrisis = true;
            }
            if (ddlPotentialCrisis.SelectedValue == "No")
            {
                potentialCrisis = false;
            }


            if (ddlCMOName.SelectedValue != "Select one")
            {
                alertedCMOId = ddlCMOName.SelectedValue.ToString();
                alertedCMOName = ddlCMOName.SelectedItem.ToString();
            }

            if (tbNoOfZombies.Text != null)
            {
                int.TryParse(tbNoOfZombies.Text, out estimatedNoOfBeings);
            }


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //descriptionofemergency.Text = staffId;

            string sql = "UPDATE Incidents SET CallerName ='" + nameOfCaller + "', CallerNric ='" + callerNric
               + "', CallBackNo ='" + callBackNo + "', LocationOfEmergency ='" + emergencyLocation +
               "', TimeOfEmergency = '" + time + "', EstimatedNoOfInjuries = '" + estimatedNoOfInjuries +
               "', EstimatedNoOfDeath = '" + estimatedNoOfDeath + "', DescOfEmergency ='" + descOfEmergency +
               "', potentialCrisis = '" + potentialCrisis + "', AlertedCMOId = '" + alertedCMOId + "', AlertedCMOName ='" + alertedCMOName +
               "', status='" + status + "', TypeOfSuperBeing ='" + superBeings + "', EstimatedNoOfBeings ='" +
               "' , NatureOfEmergency ='" + natureOfEmergency + "', NameOfEmergency ='" + nameOfEmergency +
               "', LastUpdated = '" + updatedTime1 + "', UpdatedByStaffName ='" + StaffName.Text +
               "', UpdatedByStaffId = '" + lbStaffId.Text + "'" +
               " WHERE IncidentId = @IncidentId";
            //cmd.Parameters.AddWithValue("@StaffID", staffId);
            cmd.Parameters.AddWithValue("@IncidentId", incidentId);
            cmd.CommandText = sql;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            // taking the incident id from the ViewIncidentListPage
            if (Request.QueryString["IncidentId"] != null)
            {
                
                DataTable dt1 = new DataTable();

                using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
                {

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = con1;
                    cmd1.CommandText = "SELECT * FROM Incidents where IncidentID ='" + incidentId + "'";

                    cmd1.Parameters.AddWithValue("@IncidentID", incidentId);

                    con1.Open();

                    SqlDataAdapter da1 = new SqlDataAdapter();

                    da1.SelectCommand = cmd1;
                    da1.Fill(dt1);

                    // Retrieve data successfully
                    if (dt1.Rows.Count > 0)
                    {


                        tbCallOperator.Text = dt1.Rows[0][11].ToString();


                        tbcallername.Text = dt1.Rows[0][1].ToString();
                        tbcallernric.Text = dt1.Rows[0][2].ToString();
                        tbcallbackno.Text = dt1.Rows[0][3].ToString();
                        ddlNature.SelectedValue = dt1.Rows[0][21].ToString();
                        if (ddlNature.SelectedValue == "Select a nature")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a nature first"));
                        }
                        if (ddlNature.SelectedValue == "National")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
                            ddlNameOfEmergency.Items.Insert(1, new ListItem("Invasion"));
                            ddlNameOfEmergency.Items.Insert(2, new ListItem("Hostile Attack"));
                            ddlNameOfEmergency.Items.Insert(3, new ListItem("Terrorist Attack"));
                        }
                        else if (ddlNature.SelectedValue == "Individual")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
                            ddlNameOfEmergency.Items.Insert(1, new ListItem("Murder"));
                            ddlNameOfEmergency.Items.Insert(2, new ListItem("Robbery"));
                            ddlNameOfEmergency.Items.Insert(3, new ListItem("Assault"));
                            ddlNameOfEmergency.Items.Insert(4, new ListItem("Rape"));
                            ddlNameOfEmergency.Items.Insert(5, new ListItem("Burglary"));
                            ddlNameOfEmergency.Items.Insert(6, new ListItem("Medical Emergency"));
                            ddlNameOfEmergency.Items.Insert(7, new ListItem("Fire"));
                        }

                        else if (ddlNature.SelectedValue == "Natural Disaster")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
                            ddlNameOfEmergency.Items.Insert(1, new ListItem("Flood"));
                            ddlNameOfEmergency.Items.Insert(2, new ListItem("Earthquake"));
                            ddlNameOfEmergency.Items.Insert(3, new ListItem("Wild Fire"));
                            ddlNameOfEmergency.Items.Insert(4, new ListItem("Hurricane"));
                            ddlNameOfEmergency.Items.Insert(5, new ListItem("Landslide"));
                            ddlNameOfEmergency.Items.Insert(6, new ListItem("Tsunami"));
                            ddlNameOfEmergency.Items.Insert(7, new ListItem("Thunderstorm"));
                            ddlNameOfEmergency.Items.Insert(8, new ListItem("Volcano"));
                        }
                        else if (ddlNature.SelectedValue == "Virus Outbreak")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
                            ddlNameOfEmergency.Items.Insert(1, new ListItem("Bird Flu"));
                            ddlNameOfEmergency.Items.Insert(2, new ListItem("SARS"));
                            ddlNameOfEmergency.Items.Insert(3, new ListItem("Influenza"));
                            ddlNameOfEmergency.Items.Insert(4, new ListItem("Dengue"));
                            ddlNameOfEmergency.Items.Insert(5, new ListItem("H1N1 Swine Flu"));
                            ddlNameOfEmergency.Items.Insert(6, new ListItem("Ebola"));

                        }

                        else if (ddlNature.SelectedValue == "Supernatural")
                        {
                            ddlNameOfEmergency.Items.Clear();
                            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
                            ddlNameOfEmergency.Items.Insert(1, new ListItem("Zombie Attack"));
                            ddlNameOfEmergency.Items.Insert(2, new ListItem("Alien Attack"));
                            ddlNameOfEmergency.Items.Insert(3, new ListItem("Vampire Attack"));
                            ddlNameOfEmergency.Items.Insert(4, new ListItem("Monster Attack"));

                        }
                        if (ddlNature.SelectedValue != "Supernatural")
                        {
                            Zombie.Visible = false;
                            ddlZombieType.Visible = false;
                            NoOfZombies.Visible = false;
                            tbNoOfZombies.Visible = false;
                            Zombie.Enabled = false;
                            ddlZombieType.Enabled = false;
                            NoOfZombies.Enabled = false;
                            tbNoOfZombies.Enabled = false;
                        }
                        ddlNameOfEmergency.SelectedValue = dt1.Rows[0][22].ToString();
                        tbemergencytime.Text = dt1.Rows[0][5].ToString();
                        tbemergencylocation.Text = dt1.Rows[0][4].ToString();
                        tbnoofcasualties.Text = dt1.Rows[0][7].ToString();
                        tbnoofdeaths.Text = dt1.Rows[0][8].ToString();
                        descriptionofemergency.Text = dt1.Rows[0][9].ToString();
                        if (dt1.Rows[0][10].ToString() == "True")
                        {
                            ddlPotentialCrisis.SelectedValue = "Yes";
                        }
                        if (dt1.Rows[0][10].ToString() == "False")
                        {
                            ddlPotentialCrisis.SelectedValue = "No";
                        }

                        if (dt1.Rows[0][14].ToString() != "")
                        {
                            ddlCMOName.Items.FindByText(dt1.Rows[0][14].ToString()).Selected = true;
                        }
                        ddlStatus.Items.FindByText(dt1.Rows[0][15].ToString()).Selected = true;
                        tbLastUpdated.Text = dt1.Rows[0][23].ToString();
                        tbUpdatedBy.Text = dt1.Rows[0][24].ToString();

                        tbCallOperator.ReadOnly = true;
                        tbcallername.ReadOnly = true;
                        tbcallernric.ReadOnly = true;
                        tbcallbackno.ReadOnly = true;
                        ddlNature.Enabled = false;
                        ddlNameOfEmergency.Enabled = false;
                        tbemergencytime.ReadOnly = true;
                        tbemergencylocation.ReadOnly = true;
                        tbnoofcasualties.ReadOnly = true;
                        tbnoofdeaths.ReadOnly = true;
                        descriptionofemergency.ReadOnly = true;
                        ddlPotentialCrisis.Enabled = false;
                        ddlCMOName.Enabled = false;
                        ddlStatus.Enabled = false;
                        tbLastUpdated.ReadOnly = true;
                        tbUpdatedBy.ReadOnly = true;
                        Session.Timeout = 30;

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Data Updated!');", true);
           
        }

        Edit.Visible = true;
        Edit.Enabled = true;
        Cancel.Enabled = false;
        Cancel.Visible = false;
        Update.Enabled = false;
        Update.Visible = false;
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
     
        Edit.Visible = true;
        Edit.Enabled = true;
        Cancel.Enabled = false;
        Cancel.Visible = false;
        Update.Enabled = false;
        Update.Visible = false;
        String incidentId;
        incidentId = lbIncidentId.Text;
        
        string Message = "No Changes Has Been Made";
        string Redirect_URL = "IncidentDetailsPage.aspx?IncidentId=" + incidentId;

        string alertMessage = "<script language=\"javascript\" type=\"text/javascript\">";

        alertMessage += "alert('" + Message + "');";
        alertMessage += "window.location.href=\"";
        alertMessage += Redirect_URL;
        alertMessage += "\";";
        alertMessage += "</script>";

        ClientScript.RegisterClientScriptBlock(GetType(), "alertMessage ", alertMessage);


    }

    
   
}