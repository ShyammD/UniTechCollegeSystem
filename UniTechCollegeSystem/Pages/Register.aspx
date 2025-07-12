<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UniTechCollegeSystem.Pages.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4 text-center text-primary">Create Your Account</h2>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mb-3 d-block" />

        <!-- Full Name -->
        <div class="mb-3">
            <asp:Label ID="lblFullName" runat="server" Text="Full Name" />
            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
        </div>

        <!-- Email -->
        <div class="mb-3">
            <asp:Label ID="lblEmail" runat="server" Text="Email" />
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" />
        </div>

        <!-- Password -->
        <div class="mb-3">
            <asp:Label ID="lblPassword" runat="server" Text="Password" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" ClientIDMode="Static" 
                onfocus="showPasswordHint()" onblur="hidePasswordHint()" oninput="validatePassword()" />

            <!-- New feature: Password strength indicator -->
            <asp:Label ID="lblPasswordStrength" runat="server" CssClass="mt-2 d-block" />
            <!-- End of new feature -->

            <!-- Password hint (original) enhanced with requirements guide -->
            <span id="passwordHint" style="display:none; color:blue; font-size: 0.9em;">
                Password must be at least 8 characters, include 1 capital letter, 1 special character (!@#$%^&*(),.?":{}|<>), and 1 number.
            </span>
            <!-- New feature: Password requirements guide -->
            <div class="mt-2" id="passwordRequirements" style="display:none;">
                <p><strong>Password Requirements:</strong></p>
                <ul style="list-style-type: none; padding-left: 0;">
                    <li id="reqLength">At least 8 characters</li>
                    <li id="reqCapital">At least 1 capital letter</li>
                    <li id="reqSpecial">At least 1 special character (!@#$%^&*(),.?":{}|<>)</li>
                    <li id="reqNumber">At least 1 number</li>
                </ul>
            </div>
            <!-- End of new feature -->
        </div>

        <!-- Register button -->
        <asp:Button ID="btnRegister" runat="server" Text="Register" 
            CssClass="btn btn-primary w-100" OnClick="btnRegister_Click" />
    </div>

    <!-- JavaScript for hint toggle and password validation -->
    <script type="text/javascript">
        function showPasswordHint() {
            document.getElementById("passwordHint").style.display = "inline";
            document.getElementById("passwordRequirements").style.display = "block";
        }

        function hidePasswordHint() {
            document.getElementById("passwordHint").style.display = "none";
            document.getElementById("passwordRequirements").style.display = "none";
        }

        function validatePassword() {
            const password = document.getElementById('txtPassword').value;
            const strengthLabel = document.getElementById('<%= lblPasswordStrength.ClientID %>');

            // Regex for requirements
            const lengthCheck = password.length >= 8;
            const capitalCheck = /[A-Z]/.test(password);
            const specialCheck = /[!@#$%^&*(),.?":{}|<>]/.test(password);
            const numberCheck = /[0-9]/.test(password);

            // Update requirements guide
            document.getElementById('reqLength').style.color = lengthCheck ? 'green' : 'red';
            document.getElementById('reqLength').innerHTML = lengthCheck ? '✔ At least 8 characters' : '✖ At least 8 characters';
            document.getElementById('reqCapital').style.color = capitalCheck ? 'green' : 'red';
            document.getElementById('reqCapital').innerHTML = capitalCheck ? '✔ At least 1 capital letter' : '✖ At least 1 capital letter';
            document.getElementById('reqSpecial').style.color = specialCheck ? 'green' : 'red';
            document.getElementById('reqSpecial').innerHTML = specialCheck ? '✔ At least 1 special character' : '✖ At least 1 special character (!@#$%^&*(),.?":{}|<>)';
            document.getElementById('reqNumber').style.color = numberCheck ? 'green' : 'red';
            document.getElementById('reqNumber').innerHTML = numberCheck ? '✔ At least 1 number' : '✖ At least 1 number';

            // Calculate strength
            let criteriaMet = [lengthCheck, capitalCheck, specialCheck, numberCheck].filter(Boolean).length;
            if (password.length < 8 || criteriaMet <= 2) {
                strengthLabel.style.color = 'red';
                strengthLabel.textContent = 'Weak';
            } else if (criteriaMet >= 3 && password.length < 12) {
                strengthLabel.style.color = 'orange';
                strengthLabel.textContent = 'Medium';
            } else {
                strengthLabel.style.color = 'green';
                strengthLabel.textContent = 'Strong';
            }
        }
    </script>
</asp:Content>