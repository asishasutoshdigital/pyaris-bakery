<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="spark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        html, body, form, .footer-main {
            background-color: #fbf3f3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
        <div class="cupcake-candle">
            <img src="images/new-images/cupcake-candle.jpg" alt="cupcake-background" width="70%" height="70%" />
        </div>
        <div class="container login-container">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="sectionTitle">
                    Login
                </div>
                <div class="sectionContent">
                    <div class="form-group">
                        Username
                    </div>
                    <div class="form-group">
                        <input runat="server" id="Username" class="form-control numeric" type="text" maxlength="10" placeholder="Your Mobile No" />
                        <div runat="server" id="UserNameErrorText" class="error-text"></div>
                    </div>
                    <div id="PasswordSection" runat="server">
                        <div class="form-group">
                            Password
                        </div>
                        <div class="form-group">
                            <input runat="server" id="Password" class="form-control" type="password" placeholder="Your Password" />
                            <div runat="server" id="ErrorText" class="error-text"></div>
                        </div>
                    </div>
                </div>
                <div id="ContinueButtonSection" class="buttonSection" runat="server">
                    <asp:Button runat="server" ID="ContinueBtn" class="primary-btn" Text="CONTINUE" OnClick="ContinueBtnClicked" />
                </div>
                <div id="LoginButtonSection" runat="server">
                    <div class="buttonSection">
                        <asp:Button runat="server" ID="LoginBtn" class="primary-btn" Text="LOG IN" OnClick="LoginBtnClicked" />
                    </div>
                    <div class="buttonSection forgot-password-link"><a href="ForgotPassword.aspx">Forgot Password?</a></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
