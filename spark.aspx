<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="spark.aspx.cs" Inherits="spark" %>

<%@ Register TagPrefix="msb" TagName="MenuSideBar" Src="~/MenuSideBar.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="xdata">
        <div class="product-category-title" id="xdisplayheader" runat="server"></div>
        <div class="product-listing-container row" id="xfdata" runat="server"></div>
    </div>
</asp:Content>
