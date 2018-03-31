using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewIncidentListPage : System.Web.UI.Page
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

    }
}