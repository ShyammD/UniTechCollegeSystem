using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace UniTechCollegeSystem.Pages
{
    public partial class Courses : System.Web.UI.Page
    {
        // Database connection string
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;

        // Handles page load, initializes courses and user state
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourseBoxes();
                pnlSelectedCourse.Visible = false;
            }

            if (Session["UserEmail"] != null)
            {
                lblLoginStatus.Visible = false;
                pnlWelcome.Visible = true;
                LoadUserName();
            }
            else
            {
                pnlWelcome.Visible = false;
                pnlCourses.Visible = true;
                pnlModules.Visible = false;
            }
        }

        // Loads user's full name from database
        private void LoadUserName()
        {
            string userEmail = Session["UserEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail))
            {
                lblLoginStatus.Visible = true;
                lblLoginStatus.Text = "Please log in to select a course and its modules.";
                return;
            }

            string query = "SELECT FullName FROM Users WHERE Email = @Email";

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
                            lblUserName.Text = reader["FullName"].ToString();
                        }
                    }
                }
            }
        }

        // Populates course repeater with course data
        private void BindCourseBoxes()
        {
            string query = "SELECT CourseID, CourseName, CourseDescription FROM Courses";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    rptCourses.DataSource = reader;
                    rptCourses.DataBind();
                }
            }
        }

        // Handles course selection from repeater
        protected void rptCourses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectCourse")
            {
                int courseID = Convert.ToInt32(e.CommandArgument);
                Session["SelectedCourseID"] = courseID;

                pnlCourses.Visible = false;
                BindSelectedCourse(courseID);

                if (Session["UserEmail"] != null)
                {
                    string userEmail = Session["UserEmail"].ToString();
                    if (IsAlreadyEnrolled(userEmail))
                    {
                        lblLoginStatus.Text = "You are already enrolled in a course. Edit your enrollment on the dashboard.";
                        lblLoginStatus.Visible = true;
                        pnlModules.Visible = false;
                    }
                    else
                    {
                        BindModules(courseID);
                    }
                }
            }
        }

        // Loads details of selected course and its modules
        private void BindSelectedCourse(int courseID)
        {
            string query = "SELECT c.CourseID, c.CourseName, c.CourseDescription, " +
                           "m.ModuleCode, m.ModuleTitle, m.ModuleDescription " +
                           "FROM Courses c " +
                           "LEFT JOIN Modules m ON c.CourseID = m.CourseID " +
                           "WHERE c.CourseID = @CourseID";

            List<Course> courses = new List<Course>();
            Dictionary<int, Course> courseDict = new Dictionary<int, Course>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int dbCourseID = reader.GetInt32("CourseID");
                    string courseName = reader.GetString("CourseName");
                    string courseDescription = reader.GetString("CourseDescription");

                    if (!courseDict.ContainsKey(dbCourseID))
                    {
                        courseDict[dbCourseID] = new Course
                        {
                            CourseID = dbCourseID,
                            CourseName = courseName,
                            CourseDescription = courseDescription,
                            Modules = new List<Module>()
                        };
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("ModuleCode")))
                    {
                        courseDict[dbCourseID].Modules.Add(new Module
                        {
                            ModuleCode = reader.GetString("ModuleCode"),
                            ModuleTitle = reader.GetString("ModuleTitle"),
                            ModuleDescription = reader.GetString("ModuleDescription")
                        });
                    }
                }

                courses.AddRange(courseDict.Values);
            }

            CourseRepeater.DataSource = courses;
            CourseRepeater.DataBind();
            pnlSelectedCourse.Visible = true;
        }

        // Checks if user is already enrolled in a course
        private bool IsAlreadyEnrolled(string userEmail)
        {
            string query = "SELECT COUNT(*) FROM Enrollments e " +
                           "JOIN Users u ON e.UserID = u.UserID " +
                           "WHERE u.Email = @Email";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        // Saves user's course and module selections
        private void BindModules(int courseID)
        {
            string query = "SELECT ModuleID, ModuleTitle FROM Modules WHERE CourseID = @CourseID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseID);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    chkModules.DataSource = reader;
                    chkModules.DataTextField = "ModuleTitle";
                    chkModules.DataValueField = "ModuleID";
                    chkModules.DataBind();
                    pnlModules.Visible = true;
                }
            }
        }

        // Saves user's course and module selections
        protected void btnSaveSelection_Click(object sender, EventArgs e)
        {
            if (Session["UserEmail"] != null && Session["SelectedCourseID"] != null)
            {
                string userEmail = Session["UserEmail"].ToString();
                int courseID = Convert.ToInt32(Session["SelectedCourseID"]);

                int selectedModuleCount = 0;
                foreach (ListItem item in chkModules.Items)
                {
                    if (item.Selected)
                    {
                        selectedModuleCount++;
                    }
                }

                if (selectedModuleCount != 3)
                {
                    lblLoginStatus.Text = "Please select exactly three modules.";
                    lblLoginStatus.Visible = true;
                    return;
                }

                if (IsAlreadyEnrolled(userEmail))
                {
                    lblLoginStatus.Text = "You are already enrolled in a course. Edit your enrollment on the dashboard.";
                    lblLoginStatus.Visible = true;
                    return;
                }

                string enrollQuery = "INSERT INTO Enrollments (UserID, CourseID) " +
                                     "SELECT u.UserID, @CourseID FROM Users u WHERE u.Email = @Email";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(enrollQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", userEmail);
                        cmd.Parameters.AddWithValue("@CourseID", courseID);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (ListItem item in chkModules.Items)
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

                Session["SelectedCourseID"] = null;
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                lblLoginStatus.Text = "Please log in to select a course and its modules.";
                lblLoginStatus.Visible = true;
            }
        }

        // Returns to main courses view
        protected void btnBackToCourses_Click(object sender, EventArgs e)
        {
            pnlSelectedCourse.Visible = false;
            pnlCourses.Visible = true;
            pnlModules.Visible = false;
            lblLoginStatus.Visible = false;
        }

        // Course data model
        public class Course
        {
            public int CourseID { get; set; }
            public string CourseName { get; set; }
            public string CourseDescription { get; set; }
            public List<Module> Modules { get; set; }
        }

        // Module data model
        public class Module
        {
            public string ModuleCode { get; set; }
            public string ModuleTitle { get; set; }
            public string ModuleDescription { get; set; }
        }
    }
}
