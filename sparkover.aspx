<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="sparkover.aspx.cs" Inherits="spark" %>

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
            <div class="category-card-heading">Yay !! Order Placed Successfully !!</div>
            <div class="cartTitle">You will soon receive the details via Email and SMS</div>
            <div class="col-md-4">
                <input type="button" onclick="javascript: redirect();" value="CONTINUE SHOPPING" class="rounded-btn red-btn">
            </div>
        </div>
    </div>
</asp:Content>
