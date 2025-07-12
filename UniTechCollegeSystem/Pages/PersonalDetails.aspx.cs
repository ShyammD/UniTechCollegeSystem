using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace UniTechCollegeSystem.Pages
{
    public partial class PersonalDetails : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;

        // Page Load: Load user's current information to populate the fields
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ensure the user is logged in
                if (Session["UserEmail"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                LoadUserInfo();
            }
        }

        // Method to load user details (full name and email)
        private void LoadUserInfo()
        {
            string userEmail = Session["UserEmail"].ToString();
            string query = "SELECT FullName, Email FROM Users WHERE Email = @Email";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            txtFullName.Text = reader["FullName"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                        }
                    }
                }
            }
        }

        // Method to save updated user information to the database
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string userEmail = Session["UserEmail"].ToString();
            string newFullName = txtFullName.Text;
            string newEmail = txtEmail.Text;

            string query = "UPDATE Users SET FullName = @FullName, Email = @Email WHERE Email = @OldEmail";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", newFullName);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@OldEmail", userEmail);
                    cmd.ExecuteNonQuery();  // Execute the update query
                }
            }

            // Update the session email in case the email is changed
            Session["UserEmail"] = newEmail;

            // Redirect back to the dashboard
            Response.Redirect("Dashboard.aspx");
        }
    }
}
