using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace UniTechCollegeSystem.Pages
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserEmail"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                LoadUserInfo();
                LoadEnrolledCourses();
            }
        }

        // Loads user's name and email
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
                            string fullName = reader["FullName"].ToString();
                            lblFullName.Text = fullName;
                            lblEmail.Text = reader["Email"].ToString();
                            lblUserName.Text = fullName;
                        }
                    }
                }
            }
        }

        // Loads enrolled courses and modules
        private void LoadEnrolledCourses()
        {
            string userEmail = Session["UserEmail"].ToString();
            string query = @"
                SELECT c.CourseID, c.CourseName, c.CourseDescription
                FROM Courses c
                JOIN Enrollments e ON c.CourseID = e.CourseID
                JOIN Users u ON e.UserID = u.UserID
                WHERE u.Email = @Email";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rptCourses.DataSource = dt;
                    rptCourses.DataBind();
                    // Show message if no courses are enrolled
                    lblNoCourses.Visible = dt.Rows.Count == 0;
                }
            }
        }

        // Event handler to bind modules for each enrolled course
        protected void rptCourses_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                if (drv != null && drv.DataView.Table.Columns.Contains("CourseID"))
                {
                    int courseID = Convert.ToInt32(drv["CourseID"]);
                    Repeater rptModules = e.Item.FindControl("rptModules") as Repeater;
                    if (rptModules != null)
                    {
                        DataTable modules = GetModulesForCourse(courseID);
                        rptModules.DataSource = modules;
                        rptModules.DataBind();
                    }
                }
            }
        }

        // Retrieve selected modules for a given course
        protected DataTable GetModulesForCourse(int courseID)
        {
            string userEmail = Session["UserEmail"].ToString();
            string query = @"
                SELECT m.ModuleTitle
                FROM Modules m
                JOIN UserModules um ON m.ModuleID = um.ModuleID
                JOIN Users u ON um.UserID = u.UserID
                WHERE u.Email = @Email AND m.CourseID = @CourseID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.Parameters.AddWithValue("@CourseID", courseID);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // Redirects to edit details page
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("PersonalDetails.aspx");
        }

        // Start editing enrollment
        protected void btnEditEnrollment_Click(object sender, EventArgs e)
        {
            pnlEditEnrollment.Visible = true;
            LoadCoursesForEditing();
        }

        // Load courses for editing
        private void LoadCoursesForEditing()
        {
            string query = "SELECT CourseID, CourseName FROM Courses";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlCourses.DataSource = dt;
                    ddlCourses.DataTextField = "CourseName";
                    ddlCourses.DataValueField = "CourseID";
                    ddlCourses.DataBind();
                }
            }

            string userEmail = Session["UserEmail"].ToString();
            if (!string.IsNullOrEmpty(userEmail))
            {
                string enrollmentQuery = @"
                    SELECT CourseID
                    FROM Enrollments e
                    JOIN Users u ON e.UserID = u.UserID
                    WHERE u.Email = @Email";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(enrollmentQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", userEmail);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            ddlCourses.SelectedValue = result.ToString();
                            LoadModulesForEditing(Convert.ToInt32(result));
                        }
                        else if (ddlCourses.Items.Count > 0)
                        {
                            LoadModulesForEditing(Convert.ToInt32(ddlCourses.SelectedValue));
                        }
                    }
                }
            }
        }

        // Load modules for the selected course during editing
        private void LoadModulesForEditing(int courseID)
        {
            string userEmail = Session["UserEmail"].ToString();
            string query = "SELECT ModuleID, ModuleTitle FROM Modules WHERE CourseID = @CourseID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseID);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    chkEditModules.DataSource = dt;
                    chkEditModules.DataTextField = "ModuleTitle";
                    chkEditModules.DataValueField = "ModuleID";
                    chkEditModules.DataBind();
                }

                if (!string.IsNullOrEmpty(userEmail))
                {
                    string moduleQuery = @"
                        SELECT m.ModuleID
                        FROM UserModules um
                        JOIN Users u ON um.UserID = u.UserID
                        JOIN Modules m ON um.ModuleID = m.ModuleID
                        WHERE u.Email = @Email AND m.CourseID = @CourseID";
                    using (MySqlCommand cmd = new MySqlCommand(moduleQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", userEmail);
                        cmd.Parameters.AddWithValue("@CourseID", courseID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string moduleID = reader["ModuleID"].ToString();
                                ListItem item = chkEditModules.Items.FindByValue(moduleID);
                                if (item != null)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        // Handle course selection change during editing
        protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            int courseID = Convert.ToInt32(ddlCourses.SelectedValue);
            LoadModulesForEditing(courseID);
        }

        // Save edited enrollment
        protected void btnSaveEnrollment_Click(object sender, EventArgs e)
        {
            string userEmail = Session["UserEmail"].ToString();
            if (string.IsNullOrEmpty(userEmail))
            {
                lblEditError.Text = "Please log in to edit your enrollment.";
                lblEditError.Visible = true;
                return;
            }

            int courseID = Convert.ToInt32(ddlCourses.SelectedValue);

            int selectedModuleCount = 0;
            foreach (ListItem item in chkEditModules.Items)
            {
                if (item.Selected)
                {
                    selectedModuleCount++;
                }
            }

            if (selectedModuleCount != 3)
            {
                lblEditError.Text = "Please select exactly three modules.";
                lblEditError.Visible = true;
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string deleteEnrollmentQuery = "DELETE e FROM Enrollments e JOIN Users u ON e.UserID = u.UserID WHERE u.Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(deleteEnrollmentQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.ExecuteNonQuery();
                }

                string deleteModulesQuery = "DELETE um FROM UserModules um JOIN Users u ON um.UserID = u.UserID WHERE u.Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(deleteModulesQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.ExecuteNonQuery();
                }

                string enrollQuery = "INSERT INTO Enrollments (UserID, CourseID) " +
                                     "SELECT u.UserID, @CourseID FROM Users u WHERE u.Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(enrollQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.Parameters.AddWithValue("@CourseID", courseID);
                    cmd.ExecuteNonQuery();
                }

                foreach (ListItem item in chkEditModules.Items)
                {
                    if (item.Selected)
                    {
                        int moduleID = Convert.ToInt32(item.Value);
                        string moduleQuery = "INSERT INTO UserModules (UserID, ModuleID) " +
                                             "SELECT u.UserID, @ModuleID FROM Users u WHERE u.Email = @Email";
                        using (MySqlCommand moduleCmd = new MySqlCommand(moduleQuery, conn))
                        {
                            moduleCmd.Parameters.AddWithValue("@Email", userEmail);
                            moduleCmd.Parameters.AddWithValue("@ModuleID", moduleID);
                            moduleCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            pnlEditEnrollment.Visible = false;
            lblEditError.Visible = false;
            LoadEnrolledCourses();
        }

        // Drop the enrolled course
        protected void btnDropCourse_Click(object sender, EventArgs e)
        {
            string userEmail = Session["UserEmail"].ToString();
            if (string.IsNullOrEmpty(userEmail))
            {
                lblEditError.Text = "Please log in to drop your enrollment.";
                lblEditError.Visible = true;
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string deleteEnrollmentQuery = "DELETE e FROM Enrollments e JOIN Users u ON e.UserID = u.UserID WHERE u.Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(deleteEnrollmentQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.ExecuteNonQuery();
                }

                string deleteModulesQuery = "DELETE um FROM UserModules um JOIN Users u ON um.UserID = u.UserID WHERE u.Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(deleteModulesQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.ExecuteNonQuery();
                }
            }

            pnlEditEnrollment.Visible = false;
            lblEditError.Visible = false;
            LoadEnrolledCourses();
        }

        // Cancel editing
        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            pnlEditEnrollment.Visible = false;
            lblEditError.Visible = false;
        }

        // Course and Module classes
        public class Course
        {
            public int CourseID { get; set; }
            public string CourseName { get; set; }
            public string CourseDescription { get; set; }
            public List<Module> Modules { get; set; }
        }

        public class Module
        {
            public string ModuleCode { get; set; }
            public string ModuleTitle { get; set; }
            public string ModuleDescription { get; set; }
        }
    }
}