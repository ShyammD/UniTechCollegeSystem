using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace UniTechCollegeSystem.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        // Connection string from Web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Event handler for the Register button click
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Get and trim user input
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // 1. Validate that no fields are empty
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                lblMessage.CssClass = "text-danger";
                lblMessage.Text = "All fields are required.";
                return;
            }

            // 2. Validate email format
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblMessage.CssClass = "text-danger";
                lblMessage.Text = "Please enter a valid email address.";
                return;
            }

            // Validate password requirements (8+ chars, 1 capital, 1 special, 1 number)
            if (!IsValidPassword(password))
            {
                lblMessage.CssClass = "text-danger";
                lblMessage.Text = "Password must be at least 8 characters, include 1 capital letter, 1 special character (!@#$%^&*(),.?\":{}|<>), and 1 number.";
                return;
            }

            // 3. Hash the password using SHA256
            string hashedPassword = HashPassword(password);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL insert command using parameters
                    string query = "INSERT INTO Users (FullName, Email, Password) VALUES (@FullName, @Email, @Password)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);

                        cmd.ExecuteNonQuery();

                        // New feature: Auto-login and redirect to Courses.aspx
                        Session["UserEmail"] = email;
                        Session["UserName"] = fullName; // Added to match Login.aspx.cs
                        Response.Redirect("~/Pages/Courses.aspx", false);
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Duplicate email entry
                if (ex.Number == 1062)
                {
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Text = "This email is already registered.";
                }
                else
                {
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Text = "Database error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                lblMessage.CssClass = "text-danger";
                lblMessage.Text = "Unexpected error: " + ex.Message;
            }
        }

        // Validate password requirements
        private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;
            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;
            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]"))
                return false;
            if (!Regex.IsMatch(password, @"[0-9]"))
                return false;
            return true;
        }

        // Helper function to hash passwords using SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}