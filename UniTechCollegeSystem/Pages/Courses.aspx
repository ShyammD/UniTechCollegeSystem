<%@ Page Title="Courses" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="UniTechCollegeSystem.Pages.Courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <!-- Welcome message for logged-in users -->
        <asp:Panel ID="pnlWelcome" runat="server" Visible="false">
            <h2 class="mb-4 text-center text-primary">Welcome, <asp:Label ID="lblUserName" runat="server"></asp:Label>!</h2>
            <p class="text-center">Please select a course to view its modules.</p>
        </asp:Panel>

        <!-- Login status or validation message -->
        <asp:Label ID="lblLoginStatus" runat="server" CssClass="alert alert-warning d-block text-center" Visible="false" Text="Please log in to select a course and its modules." />

        <!-- Page Title and Description -->
        <div class="text-center mb-5">
            <h1 class="display-4">Explore Our Courses</h1>
            <p class="lead">Discover a wide range of courses designed to help you succeed in your academic and career goals!</p>
        </div>

        <!-- Display all courses as clickable boxes -->
        <asp:Panel ID="pnlCourses" runat="server">
            <div class="row">
                <asp:Repeater ID="rptCourses" runat="server" OnItemCommand="rptCourses_ItemCommand">
                    <ItemTemplate>
                        <div class="col-md-4 mb-4">
                            <div class="card h-100 shadow-sm">
                                <div class="card-body">
                                    <h3 class="card-title"><%# Eval("CourseName") %></h3>
                                    <p class="card-text"><%# Eval("CourseDescription") %></p>
                                    <asp:Button ID="btnSelectCourse" runat="server" Text="View Course" CommandName="SelectCourse" CommandArgument='<%# Eval("CourseID") %>' CssClass="btn btn-primary" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>

        <!-- Panel to display selected course and its modules -->
        <asp:Panel ID="pnlSelectedCourse" runat="server" Visible="false">
            <!-- Back to Courses Button -->
            <div class="mb-3">
                <asp:Button ID="btnBackToCourses" runat="server" Text="← Back to All Courses"
                    CssClass="btn btn-outline-secondary"
                    OnClick="btnBackToCourses_Click" />
            </div>

            <div class="row">
                <asp:Repeater ID="CourseRepeater" runat="server">
                    <ItemTemplate>
                        <!-- Selected Course Card -->
                        <div class="col-12 mb-4">
                            <div class="card shadow-sm">
                                <div class="card-body">
                                    <h3 class="card-title"><%# Eval("CourseName") %></h3>
                                    <p class="card-text"><%# Eval("CourseDescription") %></p>

                                    <h5>Modules:</h5>
                                    <ul class="list-unstyled">
                                        <asp:Repeater ID="ModuleRepeater" runat="server" DataSource='<%# Eval("Modules") %>'>
                                            <ItemTemplate>
                                                <li class="mb-2">
                                                    <a class="d-flex justify-content-between align-items-center text-decoration-none"
                                                       data-bs-toggle="collapse"
                                                       href='#module_<%# Eval("ModuleCode") %>'
                                                       role="button"
                                                       aria-expanded="false"
                                                       aria-controls='module_<%# Eval("ModuleCode") %>'
                                                       onclick="toggleIcon(this)">
                                                        <span><strong><%# Eval("ModuleCode") %>:</strong> <%# Eval("ModuleTitle") %></span>
                                                        <i class="bi bi-plus-circle toggle-icon"></i>
                                                    </a>
                                                    <div class="collapse mt-1" id='module_<%# Eval("ModuleCode") %>'>
                                                        <div class="card card-body bg-light">
                                                            <%# Eval("ModuleDescription") %>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>

        <!-- Panel to display modules for selection (logged-in users only) -->
        <asp:Panel ID="pnlModules" runat="server" Visible="false">
            <h3 class="mt-4">Select Exactly Three Modules for Enrollment</h3>
            <asp:CheckBoxList ID="chkModules" runat="server" DataTextField="ModuleTitle" DataValueField="ModuleID" CssClass="mb-3"></asp:CheckBoxList>
            <asp:Button ID="btnSaveSelection" runat="server" Text="Save Selection" OnClick="btnSaveSelection_Click" CssClass="btn btn-success" />
        </asp:Panel>

        <!-- Toggle icon script -->
        <script>
            function toggleIcon(element) {
                const icon = element.querySelector(".toggle-icon");
                const targetId = element.getAttribute("href");
                const target = document.querySelector(targetId);

                const allIcons = document.querySelectorAll(".toggle-icon");
                allIcons.forEach(i => i.classList.remove("bi-dash-circle"));
                allIcons.forEach(i => i.classList.add("bi-plus-circle"));

                if (target.classList.contains("show")) {
                    icon.classList.remove("bi-dash-circle");
                    icon.classList.add("bi-plus-circle");
                } else {
                    icon.classList.remove("bi-plus-circle");
                    icon.classList.add("bi-dash-circle");
                }
            }
        </script>
    </div>
</asp:Content>
