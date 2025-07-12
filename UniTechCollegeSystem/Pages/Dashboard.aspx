<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="UniTechCollegeSystem.Pages.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <!-- Welcome Header -->
        <h2 class="mb-4 text-center text-primary">
            Welcome, <asp:Label ID="lblUserName" runat="server" CssClass="text-primary"></asp:Label>
        </h2>

        <!-- User Information Section -->
        <h4>Your Information</h4>
        <p><strong>Name:</strong> <asp:Label ID="lblFullName" runat="server"></asp:Label></p>
        <p><strong>Email:</strong> <asp:Label ID="lblEmail" runat="server"></asp:Label></p>
        <!-- Edit Personal Information Button -->
        <asp:Button ID="btnEdit" runat="server" Text="Edit Details" CssClass="btn btn-primary mb-4" OnClick="btnEdit_Click" />

        <!-- Enrolled Courses Section -->
        <h4 class="mt-4">Your Enrolled Courses</h4>
        <!-- Repeater for course cards with edit enrollment option -->
        <asp:Repeater ID="rptCourses" runat="server" OnItemDataBound="rptCourses_ItemDataBound">
            <ItemTemplate>
                <div class="card mb-4 shadow-sm">
                    <div class="card-body">
                        <h3 class="card-title"><%# Eval("CourseName") %></h3>
                        <p class="card-text"><strong>Description:</strong> <%# Eval("CourseDescription") %></p>
                        <h5>Modules:</h5>
                        <ul class="list-unstyled">
                            <asp:Repeater ID="rptModules" runat="server">
                                <ItemTemplate>
                                    <li><%# Eval("ModuleTitle") %></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <!-- New feature: Button to edit enrollment -->
                        <asp:Button ID="btnEditEnrollment" runat="server" Text="Edit Enrollment" CommandArgument='<%# Eval("CourseID") %>' OnClick="btnEditEnrollment_Click" CssClass="btn btn-secondary" />
                        <!-- End of new feature: Button to edit enrollment -->
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <!-- Label for no enrolled courses -->
        <asp:Label ID="lblNoCourses" runat="server" Text="You are not enrolled in any courses." Visible="false" CssClass="text-muted" />

        <!-- Edit Enrollment Panel -->
        <asp:Panel ID="pnlEditEnrollment" runat="server" Visible="false">
            <div class="card mb-4 shadow-sm">
                <div class="card-header">
                    <h4 class="card-title">Edit Enrollment</h4>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label for="ddlCourses" class="form-label">Select Course:</label>
                        <asp:DropDownList ID="ddlCourses" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <h5>Select Exactly Three Modules:</h5>
                        <asp:CheckBoxList ID="chkEditModules" runat="server" DataTextField="ModuleTitle" DataValueField="ModuleID" CssClass="mb-3" ClientIDMode="Static"></asp:CheckBoxList>
                    </div>
                    <asp:Label ID="lblEditError" runat="server" CssClass="alert alert-danger d-block" Visible="false" Text="Please select exactly three modules." />
                    <asp:Button ID="btnSaveEnrollment" runat="server" Text="Save Enrollment" OnClick="btnSaveEnrollment_Click" CssClass="btn btn-success" />
                    <asp:Button ID="btnDropCourse" runat="server" Text="Drop Course" OnClick="btnDropCourse_Click" CssClass="btn btn-danger" />
                    <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" OnClick="btnCancelEdit_Click" CssClass="btn btn-secondary" />
                </div>
            </div>
        </asp:Panel>

        <!-- Calendar display -->
        <h4>Calendar</h4>
        <asp:Calendar ID="calDashboard" runat="server"></asp:Calendar>
    </div>

    <!-- Client-side validation for module selection -->
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const checkboxes = document.querySelectorAll("#chkEditModules input[type='checkbox']");
            const saveButton = document.querySelector("#<%= btnSaveEnrollment.ClientID %>");
            const errorLabel = document.querySelector("#<%= lblEditError.ClientID %>");

        checkboxes.forEach(checkbox => {
            checkbox.addEventListener("change", function () {
                const checkedCount = Array.from(checkboxes).filter(cb => cb.checked).length;
                if (checkedCount > 3) {
                    this.checked = false;
                    errorLabel.style.display = "block";
                    errorLabel.textContent = "You can only select up to three modules.";
                } else {
                    errorLabel.style.display = checkedCount === 3 ? "none" : "block";
                    errorLabel.textContent = "Please select exactly three modules.";
                }
            });
        });

        saveButton.addEventListener("click", function (event) {
            const checkedCount = Array.from(checkboxes).filter(cb => cb.checked).length;
            if (checkedCount !== 3) {
                event.preventDefault();
                errorLabel.style.display = "block";
                errorLabel.textContent = "Please select exactly three modules.";
            }
        });
    </script>
</asp:Content>