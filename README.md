# UniTechCollegeSystem

## Project Overview:
The **UniTech College System** is a dynamic web-based student enrolment platform developed using **ASP.NET Web Forms** and a **MySQL** backend. It enables students to register, log in securely, enrol in courses and modules, and manage their academic profile. The system is designed with a focus on user experience, functionality, and clean, professional UI styling.

## Features
- **User Registration & Secure Login**: Students can create accounts and log in securely with session-based authentication.
- **Course Listing**: View a list of available courses along with detailed descriptions.
- **Module Selection**: Explore and enrol in modules associated with selected courses.
- **Enrolment Management**: Automatically saves course and module enrolments to the database.
- **Personal Profile Management**: View and update personal information directly through the dashboard.
- **Access Control**: Pages are protected using session checks to restrict access to authenticated users only.
- **Responsive UI**: Clean and professional interface built with Bootstrap and a consistent Site.Master layout.

## Technologies Used

- **ASP.NET Web Forms**: For building dynamic server-rendered web pages using `.aspx` files and C# code-behind.
- **.NET Framework 4.8**: The development platform that supports ASP.NET Web Forms.
- **C#**: Used for backend logic and handling user interactions.
- **HTML5**: For structuring page content across the application.
- **CSS3**: For styling pages and creating a responsive design.
- **JavaScript**: Used for basic frontend interactions and Bootstrap component functionality.
- **Bootstrap**: Frontend framework for responsive layout, forms, and UI components.
- **MySQL**: Relational database to store and manage users, courses, modules, and enrolments.
- **MySQL Workbench**: Tool used to create and manage the database schema.
- **Visual Studio 2022**: Primary IDE used for development, debugging, and UI design.
- **Session Management**: Used to handle user authentication and page access control.

## How to Run the Project

To get started with the UniTechCollegeSystem, follow these steps:

1. **Clone the repository:**
    ```
    git clone https://github.com/ShyammD/UniTechCollegeSystem.git
    ```

2. **Open the solution** in Visual Studio 2022.

3. **Set up the MySQL database:**
   - Open **MySQL Workbench**.
   - Create a new database (e.g., `unitech`).
   - Run the following SQL scripts located in the `/SQL Scripts/` folder:
     - `Create-tables.sql` – Creates all required tables.
     - `insert_courses_modules.sql` – Inserts sample course and module data.

4. **Update the `web.config`** with your MySQL credentials:
    ```xml
    <connectionStrings>
      <add name="MySqlConnection" 
           connectionString="Server=localhost;Database=unitech;Uid=root;Pwd=yourpassword;" 
           providerName="MySql.Data.MySqlClient"/>
    </connectionStrings>
    ```

5. **Restore NuGet packages:**
   - Visual Studio should handle this automatically.
   - If not, right-click the solution and select **Restore NuGet Packages**.

6. **Run the application:**
   - Press `Ctrl + F5` to build and run without debugging.
   - The system will open in your browser using IIS Express.

> You can now register, log in, and explore the full student enrolment experience!
    
## Folder Structure
```
UniTechCollegeSystem
│
├── UniTechCollegeSystem
│ ├── Pages
│ │ ├── AboutUs.aspx
│ │ ├── courses.aspx
│ │ ├── dashboard.aspx
│ │ ├── Home.aspx
│ │ ├── login.aspx
│ │ ├── PersonalDetails.aspx
│ │ └── Register.aspx
│ ├── Site.Master
│ ├── web.config
│ └── Global.asax
│
├── SQL Scripts
│ ├── Create-tables.sql
│ └── insert_courses_modules.sql
│
├── Project Report
│ └── Design and Development of an Integrated University IT College System - An Agile Approach Report.pdf
│
├── LICENSE
├── README.md
└── UniTechCollegeSystem.sln
```

## Explanation of Key Folders and Files

### Pages:
- **AboutUs.aspx**: Static information page about the university/college.
- **courses.aspx**: Displays all available courses and allows selection.
- **dashboard.aspx**: The main landing page after login, showing personalised info.
- **Home.aspx**: Welcome page for unauthenticated users.
- **login.aspx**: Allows users to securely log in to their account.
- **PersonalDetails.aspx**: Enables users to view and update their personal information.
- **Register.aspx**: New user registration page.

> *Note: Each `.aspx` page has a corresponding `.aspx.cs` code-behind file that handles server-side logic, user interactions, and database operations.*

### Site.Master:
- Provides consistent navigation and layout across all pages using a master page template.

### web.config:
- Contains database connection strings and global ASP.NET configuration settings.

### Global.asax:
- Handles application-level events such as session start and end.

### SQL Scripts:
- **Create-tables.sql**: Creates all necessary tables in the MySQL database (e.g., Users, Courses, Modules, Enrollments).
- **insert_courses_modules.sql**: Inserts sample course and module data into the database for testing/demo purposes.

### Project Report:
- **Design and Development of an Integrated University IT College System - An Agile Approach Report.pdf**: The final report describing the system's planning, development, and evaluation.

### Other Files:
- **LICENSE**: MIT License for open-source use.
- **README.md**: This documentation file describing the project.
- **UniTechCollegeSystem.sln**: The Visual Studio solution file to open the entire project.

## Contributing
Contributions are welcome!

If you spot a bug, have an idea for improvement, or want to contribute a new feature, feel free to open an issue or submit a pull request.

Please ensure your code is clean, consistent with the existing structure, and well-tested before submitting.

Thank you for helping improve the UniTech College System!
