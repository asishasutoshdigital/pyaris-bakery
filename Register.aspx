<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript">
           window.onload = function () {
               var inputs = document.querySelectorAll("input");
               for (var i = 0; i < inputs.length; i++) {
                   inputs[i].setAttribute("autocomplete", "off");
               }
           };
        </script>
    <style>
        html, body, form, .footer-main {
            background-color: #fbf3f3;
        }
    </style>
    <div id="midpart">
        <div class="container login-container">
            <div class="col-md-8 col-xs-12 no-padding">
                <div class="sectionTitle">
                    Register Yourself
                </div>
                <div class="sectionContent">
                    <div class="col-md-6 col-sm-6">
                        <div class="form-group">Mobile No&nbsp;<span class="requiredStar">*</span></div>
                        <input class="form-control numeric" runat="server" id="txtmob" type="text" maxlength="10" placeholder="Your Mobile No" autocomplete="off" />
                        <div class="form-group">Password&nbsp;<span class="requiredStar">*</span></div>
                        <input class="form-control" runat="server" id="txtpwd" type="password" placeholder="Your Password" autocomplete="off" />
                        <div class="form-group">Your Name&nbsp;<span class="requiredStar">*</span></div>
                        <input runat="server" id="txtname" type="text" class="form-control" placeholder="Your Name" autocomplete="off" />
                        <div class="form-group">Email Id&nbsp;<span class="requiredStar">*</span></div>
                        <input runat="server" id="txtemailid" type="text" class="form-control" placeholder="Your Email" autocomplete="off" />
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <div class="form-group">Address Line1</div>
                        <input runat="server" id="txtaddress1" type="text" class="form-control" placeholder="Your Address" autocomplete="off" />
                        <div class="form-group">Address Line2</div>
                        <input runat="server" id="txtaddress2" type="text" class="form-control" placeholder="Your Address" autocomplete="off" />
                        <div class="form-group">City</div>
                        <input runat="server" id="txtcity" type="text" class="form-control" placeholder="Your City" autocomplete="off" />
                        <div class="form-group">Pin Code</div>
                        <input runat="server" id="txtpin" maxlength="6" type="text" class="form-control numeric" placeholder="Your Pincode" autocomplete="off" />
                    </div>
                </div>
            </div>
            <div runat="server" id="ErrorText" class="error-text"></div>
            <div class="buttonSection col-md-4 col-sm-6 col-xs-12">
                <asp:Button runat="server" ID="RegisterBtn" class="primary-btn" Text="REGISTER" OnClick="RegisterBtnClicked" />
            </div>
        </div>
    </div>
</asp:Content>
