<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="UniTechCollegeSystem.Pages.AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Container with padding -->
    <div class="container py-5">
        <!-- Centered main heading -->
        <h1 class="display-4 text-center mb-4">About Us</h1>

        <!-- Centered image section -->
        <p class="lead text-center">
            Based in the city of Leicester, UniTech College has a mission to equip the next generation with the skills, knowledge, and mindset to thrive in a tech-driven world.
        </p>

        <!-- University image -->
        <div class="text-center my-4">
            <img src='<%= ResolveUrl("~/Images/uni.jpg") %>' alt="UniTech College Campus" class="img-fluid rounded shadow-sm" style="max-height: 400px;" />
        </div>

        <!-- Academic focus -->
        <p class="text-center">
            At UniTech, we specialise in technology, engineering, digital innovation, and applied sciences, offering industry-relevant courses that blend academic excellence with real-world experience.
        </p>

        <!-- Facilities and partnerships -->
        <p class="text-center">
            Our state-of-the-art facilities, supportive learning environment, and strong ties with local and global industry partners make us a launchpad for ambitious students from all backgrounds.
        </p>
    </div>
</asp:Content>