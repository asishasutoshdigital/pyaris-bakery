<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="sparkfail.aspx.cs" Inherits="spark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function redirect() {
            location.href = 'sparknext.aspx';
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart" class="container">
        <div class="row spark-over-container">
            <div class="category-card-heading">OOPS !! Payment Failed</div>
            <div class="cartTitle">You can retry your payment</div>
            <div class="col-md-4">
                <input type="button" onclick="javascript: redirect();" value="RETRY PAYMENT" class="rounded-btn red-btn">
            </div>
        </div>
    </div>
</asp:Content>
