<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="PendingOrders.aspx.cs" Inherits="PendingOrders" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <script>
           function decisionRecord(OrderId, Decision) {
               $("#pageLoader").css("visibility", "visible");
               if (Decision == "Rejected") {
                   if (!confirm("Please click ok to Reject and Refund the order - " + OrderId)) {
                       $("#pageLoader").css("visibility", "hidden");
                       return
                   }
                    }
                       var Data = JSON.stringify({ Decision: Decision, OrderNumber: OrderId });
                   $.ajax({
                       type: "POST",
                       url: "PendingOrders.aspx/ActionOrder",
                       contentType: "application/json; charset=utf-8",
                       data: Data,
                       dataType: "json",
                       success: function (result) {
                           updateOrders();
                           alert(result.d);
                       },
                       failure: function (error) {
                           updateOrders();
                           alert(error.d);
                           
                       }
                   });
         
           }

           function updateOrders() {
               
               $.ajax({
                   url: "PendingOrders.aspx",
                   success: function (result) {
                       $("#pageLoader").css("visibility", "hidden");
                       location.reload(true);
                   }
               });
           }

       </script>
    <div id="midpart">
           <div id="pageLoader"></div>
        <div class="col-md-12" id="PendingOdr" runat="server" clientidmode="Static">
        </div>
    </div>
</asp:Content>