<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="mya.aspx.cs" Inherits="spark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CancelOrderClicked(orderId) {
            debugger;
            swal({
                title: "Cancel Order",
                text: "Do you want to cancel your order?",
                showCancelButton: true,
                closeOnConfirm: false,
                confirmButtonColor: "#c60505",
                animation: "slide-from-top",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
            },
                function () {
                    $.ajax({
                        type: "POST",
                        url: 'mya.aspx?cancel=' + orderId,
                        data: "",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (e) {
                            swal({
                                title: "Cancellation Success",
                                text: "Your order has been successfully cancelled.",
                                showCancelButton: true,
                                closeOnConfirm: false,
                                confirmButtonColor: "#c60505",
                                animation: "slide-from-top",
                                confirmButtonText: "Yes",
                                cancelButtonText: "No",
                            })
                        },
                        error: function (e) {
                            swal({
                                title: "Cancellation Failure",
                                text: "Unable to cancel your order. Please try again.",
                                showCancelButton: true,
                                closeOnConfirm: false,
                                confirmButtonColor: "#c60505",
                                animation: "slide-from-top",
                                confirmButtonText: "Yes",
                                cancelButtonText: "No",
                            })
                        }
                    });
                }
            )

        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
        <div class="col-md-12">
            <div class="col-md-12 menu-right-content" id="xfdata" runat="server">
                <div>
                    <div class="orderContainer">
                        <div class="row orderDetailsBox">
                            <div class="col-md-8">
                                <div class="OrderNoText">Order No: 33</div>
                                <div class="OrderDetailsText">Order Date : 24-03-2020 , Delivery/Pickup Date : 24-03-2020 , Delivery/Pickup Time : 6PM-9PM</div>
                            </div>
                        </div>
                        <img src="images/statuspiccancel.png" alt="Cancel">
                        <div>
                            <table class="table table-condensed table-responsive tableContainer">
                                <thead>
                                    <tr>
                                        <th class="col-md-4">Description</th>
                                        <th class="col-md-2">Quantity</th>
                                        <th class="col-md-2">Rate</th>
                                        <th class="col-md-2">Amount</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="col-md-4">PB013 </td>
                                        <td class="col-md-2">1 </td>
                                        <td class="col-md-2">800 </td>
                                        <td class="col-md-3">800 </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="row orderFooterBox">
                            <div class="col-md-8">
                                <div class="OrderNoText">PICKUP FROM OUTLET&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;&nbsp;(PAYMODE : CASH-ON-DELIVERY) </div>
                            </div>
                            <div class="col-md-4">
                                <div class="smallText">SUBTOTAL : Rs 830 </div>
                                <div class="smallText">PAYBACK REDEEM : Rs 0 (0 POINTS) </div>
                                <div class="totalText">TOTAL: Rs 830 (Delivery: Rs 0) </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
