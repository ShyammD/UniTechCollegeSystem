using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace UniTechCollegeSystem.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
        // Constants for login attempt tracking and lockout
        private const int MaxAttempts = 3;
        private const int LockoutMinutes = 15;

        // Page load event
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Handle login button click
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                lblMessage.CssClass = "text-danger";
                lblMessage.Text = "Please enter both email and password.";
                lblAttempts.Text = "";
                return;
            }

            // Check lockout status and track failed attempts
            int failedAttempts = 0;
            bool isLockedOut = false;
            DateTime? lastFailedLogin = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT FailedLoginAttempts, LastFailedLogin FROM Users WHERE Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            failedAttempts = reader["FailedLoginAttempts"] != DBNull.Value ? Convert.ToInt32(reader["FailedLoginAttempts"]) : 0;
                            lastFailedLogin = reader["LastFailedLogin"] != DBNull.Value ? (DateTime?)reader["LastFailedLogin"] : null;
                        }
                    }
                }

                if (failedAttempts >= MaxAttempts && lastFailedLogin.HasValue)
                {
                    TimeSpan timeSinceLastFailure = DateTime.Now - lastFailedLogin.Value;
                    if (timeSinceLastFailure.TotalMinutes < LockoutMinutes)
                    {
                        isLockedOut = true;
                    }
                    else
                    {
                        ResetFailedAttempts(email, conn);
                        failedAttempts = 0;
                    }
                }

                if (isLockedOut)
                {
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Text = "Too many failed attempts. Please try again later.";
                    lblAttempts.Text = "";
                    return;
                }
            }

            // Attempt to login and retrieve user info
            if (TryLoginUser(email, password, out string userName))
            {
                // Reset failed attempts on successful login
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    ResetFailedAttempts(email, conn);
                }

                // Store in session
                Session["UserEmail"] = email;
                Session["UserName"] = userName;

                // Dynamic redirection based on enrollment status
                try
                {
                    string checkEnrollmentsQuery = "SELECT COUNT(*) FROM Enrollments e " +
                                                  "JOIN Users u ON e.UserID = u.UserID " +
                                                  "WHERE u.Email = @Email";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(checkEnrollmentsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Email", email);
                            int enrollmentCount = Convert.ToInt32(cmd.ExecuteScalar());
                            if (enrollmentCount == 0)
                            {
                                Response.Redirect("~/Pages/Courses.aspx", false);
                            }
                            else
                            {
                                Response.Redirect("~/Pages/Dashboard.aspx", false);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Text = "Error checking enrollments: " + ex.Message;
                    return;
                }
            }
            else
            {
                // Update failed attempts and display remaining attempts
                failedAttempts++;
                int attemptsRemaining = MaxAttempts - failedAttempts;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE Users SET FailedLoginAttempts = @Attempts, LastFailedLogin = @LastFailedLogin WHERE Email = @Email";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Attempts", failedAttempts);
                        cmd.Parameters.AddWithValue("@LastFailedLogin", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.ExecuteNonQuery();
                    }
                }

                lblMessage.CssClass = "text-danger";
                lblMessage.Text = "Invalid email or password. Please try again.";
                lblAttempts.CssClass = "text-warning";
                if (attemptsRemaining > 0)
                {
                    lblAttempts.Text = $"{attemptsRemaining} Attempt{(attemptsRemaining == 1 ? "" : "s")} Remaining";
                }
                else
                {
                    lblMessage.Text = "Too many failed attempts. Please try again later.";
                    lblAttempts.Text = "";
                }
            }
        }

        // Attempt to login user and return user name if successful
        private bool TryLoginUser(string email, string password, out string userName)
        {
            bool isValid = false;
            userName = string.Empty;

            string query = "SELECT FullName, Password FROM Users WHERE Email = @Email";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                string storedHashedPassword = reader["Password"].ToString();
                                string fullName = reader["FullName"].ToString();

                                if (VerifyPasswordHash(password, storedHashedPassword))
                                {
                                    userName = fullName;
                                    isValid = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.CssClass = "text-danger";
                lblMessage.Text = "Login error: " + ex.Message;
                lblAttempts.Text = "";
            }

            return isValid;
        }

        // Reset failed login attempts
        private void ResetFailedAttempts(string email, MySqlConnection conn)
        {
            string resetQuery = "UPDATE Users SET FailedLoginAttempts = 0, LastFailedLogin = NULL WHERE Email = @Email";
            using (MySqlCommand cmd = new MySqlCommand(resetQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.ExecuteNonQuery();
            }
        }

        // Verify password hash
        private bool VerifyPasswordHash(string enteredPassword, string storedHashedPassword)
        {
            string enteredHashedPassword = HashPassword(enteredPassword);
            return enteredHashedPassword == storedHashedPassword;
        }

        // Hash password using SHA256
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