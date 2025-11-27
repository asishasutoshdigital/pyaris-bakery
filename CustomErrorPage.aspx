<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="CustomErrorPage.aspx.cs" Inherits="CustomErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function redirect() {
            location.href = 'Default.aspx';
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart" class="container">
        <div class="row spark-over-container">
            <div class="category-card-heading">Oops !! Something Went Wrong</div>
            <div class="cartTitle">The page you are looking for is currently not available</div>
            <div class="col-md-4">
                <input type="button" onclick="javascript: redirect();" value="CONTINUE SHOPPING" class="rounded-btn red-btn">
            </div>
        </div>
    </div>
</asp:Content>