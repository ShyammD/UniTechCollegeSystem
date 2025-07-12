<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="UniTechCollegeSystem.Pages.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Hero section with a background gradient and padding -->
    <div class="bg-light py-5">
        <div class="container text-center">
            <!-- Headline -->
            <h1 class="display-3 fw-bold text-primary mb-4">Welcome to <span class="text-dark">UniTech College</span></h1>

            <!-- Subheading -->
            <p class="lead text-secondary mb-4">Explore our wide variety of courses and start your journey to success!</p>

            <!-- CTA Button -->
            <a href='<%= ResolveUrl("~/Pages/Courses.aspx") %>' class="btn btn-outline-primary btn-lg px-4 py-2 shadow-sm rounded-pill">
                🚀 Explore Courses
            </a>

            <!-- Homepage Image: Students on Laptops -->
            <div class="mt-5">
                <img src='<%= ResolveUrl("~/Images/UniTech College Picture.png") %>' alt="Students on Laptops" class="img-fluid rounded shadow" style="max-height: 450px;" />
            </div>
        </div>
    </div>

    <!-- Feature highlights section -->
    <div class="container py-5">
        <div class="row g-4">
            <div class="col-md-4">
                <div class="card h-100 shadow-sm border-0">
                    <div class="card-body">
                        <h5 class="card-title text-primary">📚 Diverse Courses</h5>
                        <p class="card-text">From Computer Science to AI, choose from 5 specialisations tailored to today’s tech-driven world.</p>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card h-100 shadow-sm border-0">
                    <div class="card-body">
                        <h5 class="card-title text-success">💡 Expert Instructors</h5>
                        <p class="card-text">Learn from industry professionals and academic experts committed to your success.</p>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card h-100 shadow-sm border-0">
                    <div class="card-body">
                        <h5 class="card-title text-danger">🏆 Career-Focused</h5>
                        <p class="card-text">Our curriculum is designed with employability in mind, equipping you with real-world skills.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
