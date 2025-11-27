<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="myprofile.aspx.cs" Inherits="spark" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
        <div class="col-md-12">
            <div class="col-md-12 menu-right-content" id="xdata" runat="server">
                <div class="col-md-6">
                    <div class="sectionTitle">
                        Change Password !!
                    </div>
                    <div class="horizontalLine"></div>
                    <div class="sectionContent">
                        <div class="row">
                            <div class="form-group col-md-4">
                                New Password
                            </div>
                            <div class="form-group col-md-8">
                                <input runat="server" id="Text1" type="password" maxlength="10" class="form-control" placeholder="Your Password" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                Confirm Password
                            </div>
                            <div class="form-group col-md-8">
                                <input runat="server" id="Text2" type="password" class="form-control" placeholder="Confirm Password" />
                            </div>
                        </div>
                    </div>
                    <div class="buttonSection">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/RESET.png" OnClick="ImageButton1_Click" />
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="sectionTitle">
                        Update Profile !!
                    </div>
                    <div class="horizontalLine"></div>
                    <div class="sectionContent">
                        <div class="row">
                            <div class="form-group col-md-4">
                                Mobile No
                            </div>

                            <div class="form-group col-md-8">
                                <input readonly="readonly" runat="server" id="txtmob" type="text" maxlength="10" class="form-control numeric" placeholder="Your Mobile No" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                Your Name
                            </div>
                            <div class="form-group col-md-8">
                                <input runat="server" id="txtname" type="text" class="form-control" placeholder="Your Name" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                Email Id
                            </div>
                            <div class="form-group col-md-8">
                                <input runat="server" id="txtemail" type="text" class="form-control" placeholder="Your Email" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                Address Line1
                            </div>
                            <div class="form-group col-md-8">
                                <input runat="server" id="txtaddress1" type="text" class="form-control" placeholder="Your Address" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                Address Line2
                            </div>
                            <div class="form-group col-md-8">
                                <input runat="server" id="txtaddess2" type="text" class="form-control" placeholder="Your Address" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                City
                            </div>
                            <div class="form-group col-md-8">
                                <input runat="server" id="txtcity" type="text" class="form-control" placeholder="Your City" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                Pin Code
                            </div>
                            <div class="form-group col-md-8">
                                <input runat="server" id="txtpin" maxlength="6" type="text" class="form-control" placeholder="Your PinCode" />
                            </div>
                        </div>
                    </div>
                    <div class="buttonSection">
                        <asp:ImageButton runat="server" ImageUrl="~/images/update.png" OnClick="Unnamed1_Click" ID="but2" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
