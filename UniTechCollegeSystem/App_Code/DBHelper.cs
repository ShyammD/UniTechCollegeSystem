namespace UniTechCollegeSystem
{
    using MySql.Data.MySqlClient;

    public static class DBHelper
    {
        private static string connectionString = "Server=localhost;Database=unitech_college_db;Uid=root;Pwd=admin;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}