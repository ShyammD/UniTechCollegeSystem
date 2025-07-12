<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UniTechCollegeSystem.Pages.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <!-- Page Heading for Login -->
        <h2 class="mb-4 text-center text-primary">Login to Your Account</h2>

        <!-- Label for displaying error/success messages -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mb-3 d-block" />

        <!-- New feature: Label for displaying remaining attempts -->
        <asp:Label ID="lblAttempts" runat="server" CssClass="text-warning mb-3 d-block" />
        <!-- End of new feature: Label for displaying remaining attempts -->

        <!-- Input field for email -->
        <div class="mb-3">
            <asp:Label ID="lblEmail" runat="server" Text="Email" />
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" />
        </div>

        <!-- Input field for password -->
        <div class="mb-3">
            <asp:Label ID="lblPassword" runat="server" Text="Password" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
        </div>

        <!-- Login button which triggers the authentication logic -->
        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
    </div>
</asp:Content>