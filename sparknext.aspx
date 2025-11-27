<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="sparknext.aspx.cs" Inherits="spark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $("#AvailabilityText").hide();
            deliveryDateChanged();
        });

        function deliveryDateChanged() {
            if ($('input[id*=RadioButton1]').is(":checked")) {
                outletChanged();
            }
            else {
                pincodeChanged();
            }
        }

        function deliveryTimeChange() {
            var value = $('#DropDownList1').val();
            if (value != 'CHOOSE YOUR TIME') {
                $('#xdlcharge').html('Delivery Charge : Rs 49. (If Home Delivery Chosen)');
            }
            else {
                $('#xdlcharge').html('');
            }
        }

        function outletChanged() {
            if ($('input[id*=RadioButton1]').is(":checked")) {
                var outlet = $("#ddloutlet").val();
                var deliveryDate = $("#txtdate").val();
                var deliverytime = $("#DropDownList1").val();
                $.ajax({
                    url: "android/checkoutletavailabilityWeb.aspx?outlet=" + outlet + "&deliverydate=" + deliveryDate + "&deliverytime=" + deliverytime,
                    success: function (result) {
                        var availabilityResponseDetails = result.split('|');
                        if (availabilityResponseDetails[0] == 'true') {
                            $("#AvailabilityText").hide();
                        }
                        else {
                            $("#AvailabilityText").text(availabilityResponseDetails[1]).show();
                        }
                    }
                });
            }
        }

        $(document).ready(function () {
            const savedPincode = sessionStorage.getItem('selectedPincode');
            if (savedPincode) {
                $('#txtpincode').val(savedPincode);
                if ($('input[id*=RadioButton2]').is(":checked")) {
                    pincodeChanged();
                }
            }
        });

        function pincodeChanged() {
            if ($('input[id*=RadioButton2]').is(":checked")) {
                var pincode = $("#txtpincode").val();
                var deliveryDate = $("#txtdate").val();
                var deliverytime = $("#DropDownList1").val();

                $.ajax({
                    url: "android/checkhomedeliveryavailabilityWeb.aspx?pincode=" + pincode + "&deliverydate=" + deliveryDate + "&deliverytime=" + deliverytime,
                    success: function (result) {
                        var availabilityResponseDetails = result.split('|');
                        if (availabilityResponseDetails[0] === 'true') {
                            $("#AvailabilityText").hide();
                        } else {
                            $("#AvailabilityText").text(availabilityResponseDetails[1]).show();
                        }
                    }
                });
            }
        }
    </script>

    <style>
        html, body, form, .footer-main {
            background-color: #fbf3f3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart" class="container justify-center">
        <div class="col-md-12 checkout-container">
            <div class="snSectionTitle">
                1. Choose Delivery Type
            </div>

            <div class="col-md-12 checkout-section-container">
                <div class="form-group col-md-12 row">
                    <asp:RadioButton ID="RadioButton1" runat="server" ClientIDMode="Static" Font-Bold="True" Font-Italic="False" GroupName="xdelivery" Text="Pickup From Outlet" Checked="True" AutoPostBack="True" OnCheckedChanged="RadioButton1_CheckedChanged" />
                </div>
                <div class="col-md-12 row">
                    <div class="col-md-3 form-group checkout-details-title">Choose Outlet <span class="requiredStar">*</span></div>
                    <div class="form-group col-md-5">
                        <select id="ddloutlet" runat="server" clientidmode="Static" class="form-control" onchange="outletChanged()">
                            <option value="SELECT OUTLET">SELECT OUTLET</option>
                            <option value="BBSR-BARAMUNDA">BBSR-BARAMUNDA</option>
                            <option value="BBSR-SUM HOSPITAL">BBSR-SUM HOSPITAL</option>
                            <option value="BBSR-VIVEKANANDA MARG">BBSR-VIVEKANANDA MARG</option>
                            <option value="BBSR-BOMIKHAL">BBSR-BOMIKHAL</option>
                            <option value="BBSR-SAILASHREE VIHAR">BBSR-SAILASHREE VIHAR</option>
                            <option value="BBSR-SAHEED NAGAR">BBSR-SAHEED NAGAR</option>
                            <option value="BBSR-JAGAMARA">BBSR-JAGAMARA</option>
                            <option value="BBSR-JANPATH">BBSR-JANPATH</option>
                            <option value="BBSR-JHARPADA">BBSR-JHARPADA</option>
                            <option value="CTC-CDA">CTC-CDA</option>
                            <option value="CTC-NAYASARAK">CTC-NAYASARAK</option>
                            <option value="CTC-COLLEGESQUARE">CTC-COLLEGESQUARE</option>
                            <option value="CTC-SEC6 CDA">CTC-SEC6 CDA</option>
                            <option value="BAM-GANDHI NAGAR">BAM-GANDHI NAGAR</option>
                            <option value="BALESWAR-STATION ROAD">BALESWAR-STATION ROAD</option>
                            <option value="JAJPUR-SURYANSH">JAJPUR-SURYANSH</option>
                            <option value="BAM-ASKAROAD">BAM-ASKAROAD</option>
                        </select>
                    </div>
                </div>

                <div class="form-group col-md-12 row">
                    <asp:RadioButton ID="RadioButton2" runat="server" ClientIDMode="Static" Font-Bold="True" Font-Italic="False" GroupName="xdelivery" Text="Home Delivery/Edit Address" AutoPostBack="True" OnCheckedChanged="RadioButton2_CheckedChanged" />
                </div>

                <div class="col-md-12 row">
                    <div class="col-md-3 form-group checkout-details-title">Receiver Name <span class="requiredStar">*</span></div>
                    <div class="form-group col-md-5">
                        <input runat="server" id="txtname" type="text" class="form-control" placeholder="Your Name" />
                    </div>
                </div>

<div class="col-md-12 row">
    <div class="col-md-3 form-group checkout-details-title">Address Line 1 <span class="requiredStar">*</span></div>
    <div class="form-group col-md-5">
        <asp:TextBox ID="txtaddress1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="AddressFields_TextChanged" />
    </div>
</div>

<div class="col-md-12 row">
    <div class="col-md-3 form-group checkout-details-title">Address Line 2</div>
    <div class="form-group col-md-5">
        <asp:TextBox ID="txtaddess2" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="AddressFields_TextChanged" />
    </div>
</div>

<div class="col-md-12 row">
    <div class="col-md-3 form-group checkout-details-title">City <span class="requiredStar">*</span></div>
    <div class="form-group col-md-5">
        <asp:TextBox ID="txtcity" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="AddressFields_TextChanged" />
    </div>
</div>

                <div class="col-md-12 row">
                    <div class="col-md-3 form-group checkout-details-title">Pincode <span class="requiredStar">*</span></div>
                    <div class="form-group col-md-5">
                        <input runat="server" maxlength="6" id="txtpincode" type="text" class="form-control numeric" clientidmode="Static" onblur="pincodeChanged()" />
                    </div>
                </div>
            </div>

            <div class="snSectionTitle">
                2. Receiver Contact Detail
            </div>

            <div class="col-md-12 checkout-section-container">
                <div class="form-group">
                    <asp:RadioButton ID="ExistingContactRadioBtn" runat="server" Font-Bold="True" Font-Italic="False" GroupName="xdeliverycontact" Text="Use Registered Mobile Number" Checked="True" AutoPostBack="True" OnCheckedChanged="ExistingContact_Clicked" />
                </div>
                <div class="form-group">
                    <asp:RadioButton ID="NewContactRadioBtn" runat="server" Font-Bold="True" Font-Italic="False" GroupName="xdeliverycontact" Text="New Contact Number" AutoPostBack="True" OnCheckedChanged="NewContact_Clicked" />
                </div>
                <div class="form-group col-md-10">
                    <input class="numeric form-control" runat="server" id="txtmob" type="text" maxlength="10" placeholder="Your Mobile No" />
                </div>
            </div>

            <div class="snSectionTitle">
                3. Payment Review
            </div>

            <div class="col-md-12 checkout-section-container">
                <div class="col-md-4 cartTotalBox" runat="server" id="xcount">TOTAL CART ITEMS : 7</div>
                <div class="col-md-4 cartTotalBox" runat="server" id="xsubtotal">SUB TOTAL : Rs 6700</div>
                <div class="col-md-4 cartTotalBox" runat="server" id="xtotal" clientidmode="Static">TOTAL : Rs 6700</div>
            </div>

            <div class="snSectionTitle">
                4. Choose Date &amp; Payment Mode
            </div>

            <div class="col-md-12 checkout-section-container">
                <div class="col-md-12 row">
                    <div class="col-md-3 form-group checkout-details-title">Choose Date <span class="requiredStar">*</span></div>
                    <div class="form-group col-md-6">
                        <input autocomplete="off" runat="server" maxlength="6" id="txtdate" type="text" onclick=" javascript: NewCssCal('txtdate', 'ddmmmyyyy', '', '', '', '', 'future');" class="form-control" clientidmode="Static" onchange="deliveryDateChanged()" />
                    </div>
                </div>
                <div class="col-md-12 row">
                    <div class="col-md-3 form-group checkout-details-title">Choose Time <span class="requiredStar">*</span></div>
                    <div class="form-group col-md-6">
                       <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                           <asp:ListItem>CHOOSE YOUR TIME</asp:ListItem>
                           <asp:ListItem>9AM-12PM</asp:ListItem>
                           <asp:ListItem>12PM-3PM</asp:ListItem>
                           <asp:ListItem>3PM-6PM</asp:ListItem>
                       </asp:DropDownList>
                    </div>
                </div>

                <div id="xdlcharge" clientidmode="Static" runat="server" class="col-md-12 error-text"></div>

                <div id="AvailabilityText" runat="server" clientidmode="Static" class="availabilityText form-group col-md-12"></div>

                <div class="col-md-12 cart-inner-container justify-center">
                    <%--<asp:ImageButton CssClass="nooutline snButton" ID="ImageButton1" runat="server" ImageUrl="~/images/pay1x.png" OnClick="ImageButton1_Click1" />
                    <asp:ImageButton CssClass="nooutline snButton" ID="ImageButton2" runat="server" ImageUrl="~/images/pay2x.png" OnClick="ImageButton3_Click1" />--%>
                    <div class="col-md-5 col-sm-12 form-group">
                        <asp:Button runat="server" ID="ImageButton1" class="rounded-btn red-btn" Text="CASH ON DELIVERY" OnClick="ImageButton1_Click1" />
                    </div>
                    <div class="col-md-5 col-sm-12 form-group">
                        <asp:Button runat="server" ID="ImageButton2" class="rounded-btn red-btn" Text="MAKE ONLINE PAYMENT" OnClick="ImageButton3_Click1" />
                    </div>
                </div>
                <%--<div class="col-md-6" style="padding-top: 20px;">
                    <div class="snrow" style="padding-bottom: 63px;">
                        <div class="snImageDiv">
                            <img class="snImage" src="images/cup.png" title="Enter Promo Code" />
                        </div>
                        <div class="snTxtBoxDiv">
                            <input runat="server" placeholder="Enter Promo Code" maxlength="10" id="txtPromo" type="text" class="snTxtBox" />
                        </div>
                    </div>
                    <div id="AvailabilityText" runat="server" clientidmode="Static" class="availabilityText form-group col-md-12">
                    </div>
                    <div class="buttonSection">
                        <asp:ImageButton CssClass="nooutline snButton" ID="ImageButton2" runat="server" ImageUrl="~/images/pay2x.png" OnClick="ImageButton3_Click1" />
                    </div>
                </div>--%>
                <%-- <div style="height: 100px; width: 500px; margin: 0 auto;">
                <asp:ImageButton CssClass="nooutline" ID="ImageButton3" runat="server" ImageUrl="~/images/pay3x.png" OnClick="ImageButton3_Click1" style="width: 269px;margin-left: 113px;"/>
            </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
