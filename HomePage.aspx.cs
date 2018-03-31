using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;


public partial class HomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.RemoveAll();
    }

    protected void login_Click(object sender, EventArgs e)
    {


        if ((staffId.Text != "") && (password.Text != ""))
        {
            String staffId1 = staffId.Text;
            String password1 = password.Text;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection911"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT StaffName, Role, Email, StaffId FROM Staff where StaffId ='" + staffId1 + "' and Password ='" + password1 + "'";
                //cmd1.CommandText = "SELECT Acc_No FROM InsuranceAccount WHERE Cust_Id='" + strUserID + "'";
                cmd.Parameters.AddWithValue("@StaffId", staffId1);
                cmd.Parameters.AddWithValue("@Password", password1);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(dt);

                //da1.SelectCommand = cmd1;
                //da1.Fill(dt1);

                //Count would be 1 if the userID and password matches
                if (dt.Rows.Count > 0)
                {
                    // Login Successful
                    Session["StaffName"] = dt.Rows[0][0].ToString();
                    Session["StaffRole"] = dt.Rows[0][1].ToString();
                    Session["Email"] = dt.Rows[0][2].ToString();
                    Session["StaffId"] = dt.Rows[0][3].ToString();
                    Session.Timeout = 30;


                    Response.Redirect("LogIncident.aspx");

                }

                else
                {
                    // Login fail
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Invalid username or password!');", true);
                    staffId.Text = "";
                    password.Text = "";
                }

            }
            
            


        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter Staff and Password');", true);
        }
    }


}