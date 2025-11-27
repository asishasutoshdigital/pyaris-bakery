<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="myapayback.aspx.cs" Inherits="spark" %>

<%@ Register TagPrefix="psb" TagName="ProfileSideBar" Src="~/ProfileSideBar.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart" class="container-fluid">
        <div class="col-md-12">
            <psb:ProfileSideBar ID="Menu" runat="server" />

            <div class="col-md-9 menu-right-content" id="xdata" runat="server">
                <div class="pbImageWrapper">
                    <img src="images/payback.png" alt="Payback"/>
                    <div id="point" runat="server" class="pbImageTxtContainer">
                        250
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="pbTableContainer" runat="server" id="xdfg">
                        <table class="table table-condensed table-responsive tableContainer">
                            <thead>
                                <tr>
                                    <th class="col-md-2">Points </th>
                                    <th class="col-md-2">Date </th>
                                    <th class="col-md-3">Description / Order </th>
                                    <th class="col-md-2">Type </th>
                                    <th class="col-md-2">Location </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="col-md-2">4 </td>
                                    <td class="col-md-2">12-03-2020  </td>
                                    <td class="col-md-3">REGISTRATION  </td>
                                    <td class="col-md-2">GAIN  </td>
                                    <td class="col-md-2">BBSR-KANAHDSFJDSFSDFSDFSDF </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="pbRuleContainer">
                        <h3>Loyalty Points Redemption Rule</h3>
                        1) On Registration, Customer will get 50 Points
                            <br />
                        2) On every Purchase of Rs 10/- customer will get 1 point<br />
                        3) 1 Points is equivalent to Rs 0.50Paise<br />
                        4) Customer will eligible for redemption on minimum accumulation of 300points<br />
                        5) Customer can redeem 50% of the accumulated points i.e a minimum of 150points will always be there in the data base.<br />
                        6) Customer can redeem loyalty points from the Invoice subject to up to 50% of the accumulated points on a single Invoice.<br />
                        (e.g If the customer has 500 points, and Invoice Value is Rs 125, he can redeem up to 50% of the accumulated points<br />
                        i.e 250 points which value is Rs 125  from the entire invoice amount)<br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
