<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="StoreOrders.aspx.cs" Inherits="StoreOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            updateOrders();
            setInterval(updateOrders, 300000);
        });

        function updateOrders() {
            $("#pageLoader").css("visibility", "visible");
            $.ajax({
                url: "android/OrderHistory.aspx",
                success: function (result) {
                    $("#AllOrders").empty();
                    $("#AllOrders").append(result);
                    $("#pageLoader").css("visibility", "hidden");
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
        <div id="pageLoader"></div>
        <div class="col-md-2 form-group ExportContainer" runat="server">
            <asp:Button CssClass="ExportBtn form-control" ID="ExportBtn" runat="server" Text="Export" OnClick="ExportBtn_Clicked" />
        </div>
        <div class="col-md-12" id="AllOrders" runat="server" clientidmode="Static">
        </div>
    </div>
</asp:Content>
