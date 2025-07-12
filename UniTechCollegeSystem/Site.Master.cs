using System;

namespace UniTechCollegeSystem
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Only run this logic once per page load
            if (!IsPostBack)
            {
                bool isLoggedIn = Session["UserEmail"] != null;

                navLogin.Visible = !isLoggedIn;
                navRegister.Visible = !isLoggedIn;
                navProfile.Visible = isLoggedIn;
                navLogout.Visible = isLoggedIn;
            }
        }

        // Logout handler
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Pages/Home.aspx");
        }
    }
}
