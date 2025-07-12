<%@ Page Title="Edit Personal Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PersonalDetails.aspx.cs" Inherits="UniTechCollegeSystem.Pages.PersonalDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4 text-center text-primary">Edit Your Personal Details</h2>

        <!-- Form for editing personal information -->
        <div class="mb-3">
            <asp:Label ID="lblFullName" runat="server" Text="Full Name" />
            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
        </div>
        <div class="mb-3">
            <asp:Label ID="lblEmail" runat="server" Text="Email" />
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" />
        </div>

        <!-- Save button to update the details -->
        <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
</asp:Content>