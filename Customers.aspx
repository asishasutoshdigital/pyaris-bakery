<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="Customers.aspx.cs" Inherits="Customers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
        <div class="col-md-12 ExportContainer" runat="server">
            <div class="col-md-2 form-group">
                <asp:Button CssClass="ExportBtn col-md-12 form-control" ID="ExportBtn" runat="server" Text="Export" OnClick="ExportBtn_Clicked" />
            </div>
            <div class="col-md-2 col-md-offset-8 form-group">
                <asp:DropDownList ID="YearDropDown" runat="server" CssClass="col-md-12 form-control" AutoPostBack="True">
                    <asp:ListItem Value="12">1 Year</asp:ListItem>
                    <asp:ListItem Value="6">6 Months</asp:ListItem>
                    <asp:ListItem Value="3">3 Months</asp:ListItem>
                    <asp:ListItem Value="1">1 Months</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-12" id="CustomersTableContainer" runat="server">
        </div>
    </div>
</asp:Content>
