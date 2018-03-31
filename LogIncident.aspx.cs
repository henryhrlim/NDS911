using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;


public partial class LogIncident : System.Web.UI.Page
{
    int IncidentIDNumber=0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["StaffName"] != null)
        {
            StaffName.Text = Session["StaffName"].ToString();
            StaffRole.Text = Session["StaffRole"].ToString();
            StaffEmail.Text = Session["Email"].ToString();
            lbStaffId.Text = Session["StaffId"].ToString();
        }
        if(Session["StaffName"] == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Session has been logged out, please login again'); window.location = 'HomePage.aspx';", true);
                      
        }
        if (!IsPostBack)
        {
            // this code will execute only first time when the page loads
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
            {
                String role = "CMO Officer";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT StaffId, StaffName FROM Staff where Role ='" + role +"'";
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
        }
        else
        {
            // this code will execute after the page postbacks
            
            
        }

    }

    protected void CreateNew_Click(object sender, EventArgs e)
    {
        
        // start of query to get the last incident id number from database
        DataTable dt3 = new DataTable();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT MAX(IncidentId) FROM Incidents";
            //cmd1.CommandText = "SELECT Acc_No FROM InsuranceAccount WHERE Cust_Id='" + strUserID + "'";
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = cmd;
            da.Fill(dt3);

            //da1.SelectCommand = cmd1;
            //da1.Fill(dt1);


            //Count would be 1 if the userID and password matches
            if (dt3.Rows.Count > 0)
            {
                String IncidentIDNumberString = dt3.Rows[0][0].ToString();
                int.TryParse(IncidentIDNumberString, out IncidentIDNumber);
                IncidentIDNumber += 1;
                tbIncidentNumber.Text = IncidentIDNumber.ToString();
            }
        }

        // end of query to get the last incident id number from database


        DateTime dt2 = DateTime.Now;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
        {
            
            String nameOfCaller = tbcallername.Text; 
            String callerNric = tbcallernric.Text;
            int callBackNo;
            int.TryParse(tbcallbackno.Text, out callBackNo);
            String emergencyLocation = tbemergencylocation.Text;
            DateTime emergencytime = DateTime.Parse(tbemergencytime.Text);
            String time = emergencytime.ToString("hh:mm:ss tt");
            DateTime emergencyDate = DateTime.Now.Date;
            String datetimeofemergency = emergencyDate.ToString("yyyy/MM/dd") + " " + time;
            DateTime datetimeofemergency1 = DateTime.Parse(datetimeofemergency);
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
            String datetimeofcreated = incidentCreatedDate.ToString("yyyy/MM/dd") + " " + incidentCreatedTime;
            DateTime datetimeofcreated1 = DateTime.Parse(datetimeofcreated);
            String natureOfEmergency = ddlNature.SelectedValue;
            String nameOfEmergency = ddlNameOfEmergency.SelectedValue;
            String modeOfBeings= "";
            String superBeings = "";
            if (ddlNature.SelectedValue.ToString() == "Supernatural")
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
            if (tbnoofcasualties.Text != null)
            {
                int.TryParse(tbnoofcasualties.Text, out estimatedNoOfInjuries);
            }
            if(tbnoofdeaths.Text != null)
            {
                int.TryParse(tbnoofdeaths.Text, out estimatedNoOfDeath);
            }

            if(ddlPotentialCrisis.SelectedValue == "Yes")
            {
                potentialCrisis = true;
            }
            if(ddlPotentialCrisis.SelectedValue == "No")
            {
                potentialCrisis = false;
            }
            if(ddlCMOName.SelectedValue != "Select one")
            {
                alertedCMOId = ddlCMOName.SelectedValue.ToString();
                alertedCMOName = ddlCMOName.SelectedItem.ToString();
            }
            if(superBeings != null)
            {
                modeOfBeings = ddlZombieType.SelectedValue.ToString();
            }
            if (tbNoOfZombies.Text != null)
            {
                int.TryParse(tbNoOfZombies.Text, out estimatedNoOfBeings);
            }

 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //descriptionofemergency.Text = staffId;

            string sql = "INSERT INTO Incidents(CallerName, CallerNric, CallBackNo, " +
                "LocationOfEmergency, TimeOfEmergency, DateTimeOfEmergency, EstimatedNoOfInjuries, EstimatedNoOfDeath, " +
                "DescOfEmergency,PotentialCrisis, StaffId, StaffName, AlertedCMOId, " +
                "AlertedCMOName, Status, TypeOfSuperBeing, EstimatedNoOfBeings, " +
                "IncidentCreatedTime, IncidentCreatedDate, DateTimeOfCreated, NatureOfEmergency, NameOfEmergency, ModeOfBeings) ";
            sql += "VALUES(@NameOfCaller, @CallerNric, @CallBackNo, @Location, " +
                "@EmergencyTime, @DateTimeOfEmergency, @EstimatedInjuries, @EstimatedDeath, @DescOfEmergency, " +
                "@PotentialCrisis, @StaffId, @StaffName, @AlertedCMOId, @AlertedCMOName, @Status," +
                "@TypeOfSuperBeing, @EstimatedNoOfBeings, @IncidentCreatedTime, @IncidentCreatedDate, @DateTimeOfCreated," +
                "@NatureOfEmergency, @NameOfEmergency, @ModeOfBeings);";
            //cmd.Parameters.AddWithValue("@StaffID", staffId);
            cmd.Parameters.AddWithValue("@NameOfCaller", nameOfCaller);
            cmd.Parameters.AddWithValue("@CallerNric", callerNric);
            cmd.Parameters.AddWithValue("@CallBackNo", callBackNo);
            cmd.Parameters.AddWithValue("@Location", emergencyLocation);
            cmd.Parameters.AddWithValue("@EmergencyTime", time);
            cmd.Parameters.AddWithValue("@DateTimeOfEmergency", datetimeofemergency1);
            cmd.Parameters.AddWithValue("@EstimatedInjuries", estimatedNoOfInjuries);
            cmd.Parameters.AddWithValue("@EstimatedDeath", estimatedNoOfDeath);
            cmd.Parameters.AddWithValue("@DescOfEmergency", descOfEmergency);
            cmd.Parameters.AddWithValue("@PotentialCrisis", potentialCrisis);
            cmd.Parameters.AddWithValue("@StaffId", staffId);
            cmd.Parameters.AddWithValue("@StaffName", staffName);
            cmd.Parameters.AddWithValue("@AlertedCMOId", alertedCMOId);
            cmd.Parameters.AddWithValue("@AlertedCMOName", alertedCMOName);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@TypeOfSuperBeing", superBeings);
            cmd.Parameters.AddWithValue("@EstimatedNoOfBeings", estimatedNoOfBeings);
            cmd.Parameters.AddWithValue("@IncidentCreatedTime", incidentCreatedTime);
            cmd.Parameters.AddWithValue("@IncidentCreatedDate", incidentCreatedDate);
            cmd.Parameters.AddWithValue("@DateTimeOfCreated", datetimeofcreated1);
            cmd.Parameters.AddWithValue("@NatureOfEmergency", natureOfEmergency);
            cmd.Parameters.AddWithValue("@NameOfEmergency", nameOfEmergency);
            cmd.Parameters.AddWithValue("@ModeOfBeings", modeOfBeings);
            cmd.CommandText = sql;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Incident Has Been Created!'); ", true);

            // after inserting into the database
            // for reviewing purpose or notifying CMO
            ddlNature.SelectedValue = natureOfEmergency;
            ddlNameOfEmergency.SelectedValue = nameOfEmergency;
            if(potentialCrisis == true)
            {
                ddlPotentialCrisis.SelectedValue = "Yes";
                Submit.Enabled = false;
                Submit.Visible = false;
                Notify.Enabled = true;
                Notify.Visible = true;
                ddlStatus.SelectedValue = "Notified CMO";

            }
            else
            {
                ddlPotentialCrisis.SelectedValue = "No";
                Submit.Visible = false;
                Submit.Enabled = false;
                LogNewIncident.Visible = true;
                LogNewIncident.Enabled = true;
            }
            if(superBeings != null)
            {
                Zombie.Enabled = true;
                ddlZombieType.Enabled = true;
                NoOfZombies.Enabled = true;
                tbNoOfZombies.Enabled = true;
                ddlZombieType.SelectedValue = modeOfBeings;
                tbNoOfZombies.Text = estimatedNoOfBeings.ToString();

            }
            ddlCMOName.SelectedValue = alertedCMOId;
            //ddlStatus.SelectedValue = status;



            //int incidentNumber;
            //int.TryParse(tbIncidentNumber.Text, out incidentNumber);
            //Session["NewIncidentId"] = tbIncidentNumber.Text;
            //string pageurl = "IncidentSummaryPage.aspx?incidentId" + incidentNumber.ToString() + ";";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Incident Has Been Created!'); window.location = 'IncidentSummaryPage.aspx';", true);

            // reset all the fields 
            //tbcallername.Text = "";
            //tbcallernric.Text = "";
            //tbcallbackno.Text = "";

            //Zombie.Visible = false;
            //ddlZombieType.Visible = false;
            //NoOfZombies.Visible = false;
            //tbNoOfZombies.Visible = false;
            //Zombie.Enabled = false;
            //ddlZombieType.Enabled = false;
            //NoOfZombies.Enabled = false;
            //tbNoOfZombies.Enabled = false;

            //ddlNature.SelectedIndex = 0;
            //ddlNameOfEmergency.SelectedIndex = 0;
            //ddlPotentialCrisis.SelectedIndex = 0;
            //ddlNameOfEmergency.SelectedIndex = 0;
            //ddlStatus.SelectedIndex = 0;
            //ddlCMOName.SelectedIndex = 0;

            //tbemergencytime.Text = "";
            //tbemergencylocation.Text = "";
            //tbnoofcasualties.Text = "";
            //tbnoofdeaths.Text = "";
            //tbNoOfZombies.Text = "";
            //descriptionofemergency.Text = "";

        }
    }

    protected void Nature_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlNature.SelectedValue == "Select a nature")
        {
            ddlNameOfEmergency.Items.Clear();
            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a nature first"));
        }
        if(ddlNature.SelectedValue == "National")
        {
            ddlNameOfEmergency.Items.Clear();
            ddlNameOfEmergency.Items.Insert(0, new ListItem("Select a name"));
            ddlNameOfEmergency.Items.Insert(1, new ListItem("Invasion"));
            ddlNameOfEmergency.Items.Insert(2, new ListItem("Hostile Attack"));
            ddlNameOfEmergency.Items.Insert(3, new ListItem("Terrorist Attack"));
        }
        else if(ddlNature.SelectedValue == "Individual")
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
        if(ddlNature.SelectedValue != "Supernatural")
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

    protected void ddlNameOfEmergency_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void NotifyCMO_Click(object sender, EventArgs e)
    {
        // updating status to notified
        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
        {
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con1;
            string sql = "UPDATE Incidents SET Status = 'Notified CMO' WHERE IncidentID = '" + tbIncidentNumber.Text + "'";

            cmd1.CommandText = sql;
            con1.Open();
            cmd1.ExecuteNonQuery();
            con1.Close();


        }

            DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT CallerName, CallerNRIC, CallBackNo, LocationofEmergency, DateTimeOfEmergency, TypeOfSuperBeing, EstimatedNoOfBeings, " +
                "EstimatedNoOfInjuries, EstimatedNoOfDeath, DescOfEmergency, StaffId, AlertedCMOId, " +
                "DateTimeOfCreated, NatureOfEmergency, NameOfEmergency FROM Incidents WHERE IncidentId ='" + tbIncidentNumber.Text +"'" ;



            con.Open();

            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = cmd;
            da.Fill(dt);

            //da1.SelectCommand = cmd1;
            //da1.Fill(dt1);

            //Count would be 1 if the userID and password matches
            if (dt.Rows.Count > 0)
            {
                //String jsonfile = DataTableToJSONWithStringBuilder(dt);
                Report911insert obj = new Report911insert
                {
                    CallerName = dt.Rows[0][0].ToString(),
                    CallerNRIC = dt.Rows[0][1].ToString(),
                    CallBackNo = int.Parse(dt.Rows[0][2].ToString()),
                    LocationofEmergency = dt.Rows[0][3].ToString(),
                    DatetimeOfEmergency = DateTime.Parse(dt.Rows[0][4].ToString()),
                    TypeOfSuperBeing = dt.Rows[0][5].ToString(),
                    EstimatedNoOfBeings = int.Parse(dt.Rows[0][6].ToString()),
                    EstimatedNoOfInjuries = int.Parse(dt.Rows[0][7].ToString()),
                    EstimatedNoOfDeath = int.Parse(dt.Rows[0][8].ToString()),
                    DescOfEmergency = dt.Rows[0][9].ToString(),
                    StaffId = dt.Rows[0][10].ToString(),
                    AlertedCMOId = dt.Rows[0][11].ToString(),
                    IncidentCreatedDatetime = DateTime.Parse(dt.Rows[0][12].ToString()),
                    NatureOfEmergency = dt.Rows[0][13].ToString(),
                    NameOfEmergency = dt.Rows[0][14].ToString()
                };

                String json = JsonConvert.SerializeObject(obj, Formatting.Indented);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://ssadapi.azurewebsites.net/api/Report911");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "*/*";
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();

                }

                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
                catch (WebException ex)
                {
                    string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                }



                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Crisis has been notified'); ", true);


            }
            con.Close();
            LogNewIncident.Visible = true;
            LogNewIncident.Enabled = true;
            Notify.Visible = false;
            Notify.Enabled = false;




            //var http = (HttpWebRequest)WebRequest.Create(new Uri("http://ssadapi.azurewebsites.net/api/Report911"));
            //http.Accept = "application/json";
            //http.ContentType = "application/json";
            //http.Accept = "*/*";
            //http.Method = "POST";

            //ASCIIEncoding encoding = new ASCIIEncoding();
            //Byte[] bytes = encoding.GetBytes(jsonfile);

            //Stream newStream = http.GetRequestStream();
            //newStream.Write(bytes, 0, bytes.Length);
            //newStream.Close();

            //var response = http.GetResponse();

            //var stream = response.GetResponseStream();
            //var sr = new StreamReader(stream);
            //var content = sr.ReadToEnd();




            //string path = Server.MapPath("~/App_Data/");
            //System.IO.File.WriteAllText(path + "IncidentReport.json", json);
            //Response.Redirect("IncidentReport.aspx?MyJson=" + json);




        }

            
            
    }

    public string DataTableToJSONWithStringBuilder(DataTable table)
    {
        var JSONString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            JSONString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                JSONString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("},");
                }
            }
            JSONString.Append("]");
        }
        return JSONString.ToString();
    }


    public string ConvertDataTabletoString(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                
                
                    row.Add(col.ColumnName, dr[col]);
     
                
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }



    protected void LogNewIncident_Click(object sender, EventArgs e)
    {
        // reset all the fields 
        tbcallername.Text = "";
        tbcallernric.Text = "";
        tbcallbackno.Text = "";

        Zombie.Visible = false;
        ddlZombieType.Visible = false;
        NoOfZombies.Visible = false;
        tbNoOfZombies.Visible = false;
        Zombie.Enabled = false;
        ddlZombieType.Enabled = false;
        NoOfZombies.Enabled = false;
        tbNoOfZombies.Enabled = false;

        ddlNature.SelectedIndex = 0;
        ddlNameOfEmergency.SelectedIndex = 0;
        ddlPotentialCrisis.SelectedIndex = 0;
        ddlNameOfEmergency.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlCMOName.SelectedIndex = 0;

        tbemergencytime.Text = "";
        tbemergencylocation.Text = "";
        tbnoofcasualties.Text = "";
        tbnoofdeaths.Text = "";
        tbNoOfZombies.Text = "";
        descriptionofemergency.Text = "";
        LogNewIncident.Enabled = false;
        LogNewIncident.Visible = false;
        Submit.Enabled = true;
        Submit.Visible = true;



    }
}