<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        html, body, form, .footer-main {
            background-color: #fbf3f3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
        <div class="container login-container">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="sectionTitle">
                    Forgot Password
                </div>
                <div class="sectionContent">
                    <input class="form-control numeric" runat="server" id="UserName" type="text" maxlength="10" placeholder="Enter Mobile Number" />
                    <div runat="server" id="xdrf" class="error-text"></div>
                </div>
                <div class="buttonSection">
                    <asp:Button runat="server" ID="ForgotPasswordBtn" class="primary-btn" Text="GET PASSWORD" OnClick="ForgotPasswordBtnClicked" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
