# UniTechCollegeSystem

## Project Overview
The **UniTech College System** is a dynamic web-based student enrolment platform developed using **ASP.NET Web Forms** and a **MySQL** backend. It enables students to register, log in securely, enrol in courses and modules, and manage their academic profile. The system is designed with a focus on user experience, functionality, and clean, professional UI styling.

## Features
- **User Registration & Secure Login**: Students can create accounts and log in securely with session-based authentication.
- **Course Listing**: View a list of available courses along with detailed descriptions.
- **Module Selection**: Explore and enrol in modules associated with selected courses.
- **Enrolment Management**: Automatically saves course and module enrolments to the database.
- **Personal Profile Management**: View and update personal information directly through the dashboard.
- **Access Control**: Pages are protected using session checks to restrict access to authenticated users only.
- **Responsive UI**: Clean and professional interface built with Bootstrap and a consistent Site.Master layout.

## 🛠️ Technologies Used

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
To get started with the BookingConsoleApp, follow these steps:

1. **Clone the repository:**
    ```
    git clone https://github.com/Shyam-Dattani/BookingConsoleApp.git
    ```
2. **Open the solution** in your preferred IDE, ideally Visual Studio Installer.

3. **Restore the NuGet packages:**
    ```
    dotnet restore
    ```
    
4. **Update the `appsettings.json` and `App.config`** with your Booking.com API credentials and other necessary configuration settings.

5. **Run the application:**
    ```
    dotnet run
    ```
    
## Folder Structure
```
BookingConsoleApp
│
├── Web API Integration Project Design
│   └── Web API Integration Project Design.pdf
├── Web API Integration Project Development
│   ├── Project Source Code
│   │   └── BookingConsoleApp
│   │       ├── Models
│   │       │   ├── HotelData.cs
│   │       │   ├── HotelDetails.cs
│   │       │   ├── HotelPhoto.cs
│   │       │   ├── HotelReview.cs
│   │       │   ├── HotelSearch.cs
│   │       │   ├── RoomAvailability.cs
│   │       │   ├── SearchFlights.cs
│   │       │   └── SearchTaxi.cs
│   │       ├── Properties
│   │       │   └── AssemblyInfo.cs
│   │       ├── Services
│   │       ├── App.config
│   │       ├── BookingConsoleApp.csproj
│   │       ├── Program.cs
│   │       ├── appsettings.json
│   │       ├── packages.config
│   │       ├── Recorded Data
│   │       ├── .gitattributes
│   │       ├── .gitignore
│   │       └── BookingConsoleApp.sln
│   └── Web API Integration Project Development.pdf
├── Web API Integration Project Presentation
│   ├── Demo of AceBooking System.pdf
│   └── Web Integration Project Presentation for Booking.com.pptx
```
## Explanation of Key Folders and Files

### Models:
- **HotelData.cs**: Contains the data structure for hotel-related information.
- **HotelDetails.cs**: Holds detailed information about specific hotels.
- **HotelPhoto.cs**: Represents the structure for hotel images.
- **HotelReview.cs**: Contains customer review data for hotels.
- **HotelSearch.cs**: Allows the user to search for hotels.
- **RoomAvailability.cs**: Represents room availability information for hotels.
- **SearchFlights.cs**: Defines the structure for flight search results.
- **SearchTaxi.cs**: Defines the structure for taxi search results.

### Properties:
- **AssemblyInfo.cs**: Contains metadata about the assembly.

### Services:
- Contains the business logic for interacting with the Booking.com API, handling searches for hotels, taxis, and flights.

### Configuration Files:
- **App.config**: Contains application-level configuration settings.
- **appsettings.json**: Holds JSON formatted configuration settings for the application.
- **packages.config**: Lists the NuGet packages used in the project.

### Program.cs:
The entry point of the application, where the main logic and user interface flow are defined.

### Recorded Data:
Contains recorded payment information and error logs.

### Other Files:
- **.gitattributes**: Defines attributes for pathnames.
- **.gitignore**: Specifies intentionally untracked files to ignore.
- **BookingConsoleApp.csproj**: The project file that defines the project configuration and dependencies.
- **BookingConsoleApp.sln**: The solution file that contains project configurations and build settings.

## Contributing
Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.
