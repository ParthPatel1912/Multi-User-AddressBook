<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteAccount.aspx.cs" Inherits="AllList_AdminPanel_DeleteAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/bootstrap-4.5.2-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/bootstrap-4.5.2-dist/css/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="~/bootstrap-4.5.2-dist/js/bootstrap.min.js"></script>

    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/AllList/Content/images/icons/favicon.ico")%>" />

    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/AllList/Content/vendor/bootstrap/css/bootstrap.min.css")%>" />

    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/AllList/Content/fonts/font-awesome-4.7.0/css/font-awesome.min.css")%>" />

    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/AllList/Content/fonts/Linearicons-Free-v1.0.0/icon-font.min.css")%>" />

    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/AllList/Content/vendor/animate/animate.css")%>" />

    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/AllList/Content/vendor/css-hamburgers/hamburgers.min.css")%>" />

    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/AllList/Content/vendor/animsition/css/animsition.min.css")%>" />

    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/AllList/Content/vendor/select2/select2.min.css")%>" />

    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/AllList/Content/vendor/daterangepicker/daterangepicker.css")%>" />

    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/AllList/Content/css/util.css")%>" />
    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/AllList/Content/css/main.css")%>" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="limiter">
            <div class="container-login100">
                <div class="wrap-login100">
                    <div class="login100-form-title" style="background-image: url(<%=ResolveClientUrl("~/AllList/Content/images/bg-01.jpg); ")%>">
                        <span class="login100-form-title-1">Delete Account
                        </span>
                    </div>
                    <div class="login100-form validate-form">
                        <div class="wrap-input100 validate-input m-b-26" data-validate="Username is required">
                            <span class="label-input100">Username</span>
                            <asp:TextBox runat="server" ID="txtUserName" CssClass="input100" placeholder="Enter username"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Enter User Name" ControlToValidate="txtUserName" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="delete" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <%--<input class="input100" type="text" name="username" placeholder="Enter username" />--%>
                            <span class="focus-input100"></span>
                        </div>
                        <div class="wrap-input100 validate-input m-b-18" data-validate="Password is required">
                            <span class="label-input100">Password</span>
                            <asp:TextBox runat="server" ID="txtPassword" CssClass="input100" TextMode="Password" placeholder="Enter password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Enter Password" ControlToValidate="txtPassword" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="delete" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <%--<input class="input100" type="password" name="pass" placeholder="Enter password" />--%>
                            <span class="focus-input100"></span>
                        </div>
                        <div class="wrap-input100 validate-input m-b-26" data-validate="Username is required">
                            <span class="label-input100">Mobile No</span>
                            <asp:TextBox runat="server" ID="txtMobileNo" TextMode="Number" CssClass="input100" placeholder="Enter mobile number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ErrorMessage="Enter Mobile No" ControlToValidate="txtMobileNo" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="delete" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMobile" runat="server" ErrorMessage="Enter 10 digit mobile no." ControlToValidate="txtMobileNo" CssClass="alert-dark" Display="Dynamic" ForeColor="Red" ValidationExpression="[0-9]{10}" SetFocusOnError="true" ValidationGroup="delete"></asp:RegularExpressionValidator>
                            <%--<input class="input100" type="text" name="username" placeholder="Enter username" />--%>
                            <span class="focus-input100"></span>
                        </div>
                        <div class="flex-sb-m w-full p-b-30">
                            <div class="contact100-form-checkbox">
                                <asp:Label runat="server" ID="lblErrorMessage" EnableViewState="false" ForeColor="#999999"></asp:Label>
                            </div>
                            <div>
                                <asp:HyperLink runat="server" ID="hlForgetPassword" Text="Forget Password?" NavigateUrl="~/AllList/AdminPanel/ForgetPassword.aspx"></asp:HyperLink>
                            </div>
                        </div>
                        <div class="container-login100-form-btn">
                            <asp:Button runat="server" ID="btnDelete" Text="  Delete  " Style="background-color: #ff0000;" CssClass="login100-form-btn" OnClick="btnDelete_Click" ValidationGroup="delete" />
                            <%--<button class="login100-form-btn">
                                Login
                            </button>--%>
                            <%--<button class="login100-form-btn" style="background-color:#4649b8;">
                                Sign Up
                            </button>--%>
                        </div>
                        <div style="padding-top: 20px;">
                            <asp:HyperLink runat="server" ID="hlCancelDeleteAccount" Text="Cancel Delete Account" NavigateUrl="~/AllList/AdminPanel/Login.aspx"></asp:HyperLink><br />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script src="<%=ResolveClientUrl("~/AllList/Content/vendor/jquery/jquery-3.2.1.min.js")%>"></script>

        <script src="<%=ResolveClientUrl("~/AllList/Content/vendor/animsition/js/animsition.min.js")%>"></script>

        <script src="<%=ResolveClientUrl("~/AllList/Content/vendor/bootstrap/js/popper.js")%>"></script>
        <script src="<%=ResolveClientUrl("~/AllList/Content/vendor/bootstrap/js/bootstrap.min.js")%>"></script>

        <script src="<%=ResolveClientUrl("~/AllList/Content/vendor/select2/select2.min.js")%>"></script>

        <script src="<%=ResolveClientUrl("~/AllList/Content/vendor/daterangepicker/moment.min.js")%>"></script>
        <script src="<%=ResolveClientUrl("~/AllList/Content/vendor/daterangepicker/daterangepicker.js")%>"></script>

        <script src="<%=ResolveClientUrl("~/AllList/Content/vendor/countdowntime/countdowntime.js")%>"></script>

        <script src="<%=ResolveClientUrl("~/AllList/Content/js/main.js")%>"></script>

        <script async src="https://www.googletagmanager.com/gtag/js?id=UA-23581568-13"></script>
        <script>
            window.dataLayer = window.dataLayer || [];
            function gtag() { dataLayer.push(arguments); }
            gtag('js', new Date());

            gtag('config', 'UA-23581568-13');
        </script>
    </form>
</body>
</html>
