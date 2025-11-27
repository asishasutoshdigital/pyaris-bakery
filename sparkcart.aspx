<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="sparkcart.aspx.cs" Inherits="spark" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function dxr() {
            var fg = document.getElementById("xtotal").innerText;
            var ft = fg.replace('TOTAL : Rs ', '');
            var minOrderAmt = <%=Program.minimumOrderAmt%>;
            if (ft >= minOrderAmt) {
                return true;

            } else {
                swal('Minimum Order Amount : Rs ' + minOrderAmt + '', 'Please Check Your Cart Total !!', 'error');
                return false;
            }

        }
        function myfunction() {
            var g = document.getElementById("ContentPlaceHolder1_xavailablepoints").innerText;
            if (g.includes("LOGIN")) {
                window.location.href = "login.aspx";
            }
            else {
                return false;
            }
        }

        function redirect() {
            location.href = 'Default.aspx';
        }

        function toggleRedeemSection() {
            var redeemSection = document.getElementById("redeemSection");
            var redeemButton = document.getElementById('<%= btnToggleRedeem.ClientID %>');
    
    if (redeemSection.style.display === "none") {
        redeemSection.style.display = "flex";
    } else {
        redeemSection.style.display = "none";
    }
    return false;
}

        function toggleRedeemSection() {
            var redeemSection = document.getElementById("redeemSection");
            var redeemButton = document.getElementById('<%= btnToggleRedeem.ClientID %>');

            if (redeemSection.style.display === "none") {
                redeemSection.style.display = "flex";
                redeemButton.style.display = "none"; // Hide the button when section is shown
            } else {
                redeemSection.style.display = "none";
                redeemButton.style.display = "block"; // Show the button when section is hidden
            }
            return false;
        }

        function toggleGiftCardSection() {
            var giftCardSection = document.getElementById("giftCardSection");
            var giftCardButton = document.getElementById('<%= btnAddGiftCard.ClientID %>');

            if (giftCardSection.style.display === "none") {
                giftCardSection.style.display = "flex";
                giftCardButton.style.display = "none"; // Hide the button when section is shown
            } else {
                giftCardSection.style.display = "none";
                giftCardButton.style.display = "block"; // Show the button when section is hidden
            }
            return false;
        }

        function showActiveSection() {
            var redeemSection = document.getElementById("redeemSection");
            var giftCardSection = document.getElementById("giftCardSection");
            var redeemButton = document.getElementById('<%= btnToggleRedeem.ClientID %>');
            var giftCardButton = document.getElementById('<%= btnAddGiftCard.ClientID %>');
            var discountSource = '<%= Session["DiscountSource"] != null ? Session["DiscountSource"].ToString() : "" %>';

            if (discountSource === "Redeem") {
                redeemSection.style.display = "flex";
                redeemButton.style.display = "none"; // Hide redeem button
            } else if (discountSource === "GiftCard") {
                giftCardSection.style.display = "flex";
                giftCardButton.style.display = "none"; // Hide gift card button
            }
        }

        if (window.addEventListener) {
            window.addEventListener('load', showActiveSection);
        } else if (window.attachEvent) {
            window.attachEvent('onload', showActiveSection);
        }

        window.onload = function () {
            var msg = document.getElementById('<%= litMessage.ClientID %>');
               if (msg && msg.innerText.trim() !== "") {
                   setTimeout(function () {
                       msg.style.display = 'none';
                   }, 5000);
               }
        };
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <style>
        html, body, form, .footer-main {
            background-color: #fbf3f3;
        }
.cart-card {
    display: flex;
    background: rgba(255, 255, 255, 0.6);
    border-radius: 20px;
    margin-bottom: 25px;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
    overflow: hidden;
    align-items: center;
    transition: transform 0.3s ease, box-shadow 0.3s ease;
    position: relative;
    backdrop-filter: blur(10px);
    border: 1px solid rgba(255, 255, 255, 0.3);
}

.cart-card:hover {
    transform: translateX(-6px);
    box-shadow: 0 14px 40px rgba(0, 0, 0, 0.15);
}

.cart-card-image {
    width: 240px;
    height: 100%;
    display: flex;
    align-items: center; /* Vertical center */
    background: rgba(243, 246, 250, 0.8);
    padding: 30px;
    border-right: 1px solid #e0e0e0;
}

.cart-card-icon {
    max-width: 100%;
    height: auto;
    max-height: 160px;
    object-fit: contain;
    border-radius: 18px;
    box-shadow: 0 6px 18px rgba(0, 0, 0, 0.08);
}

.cart-card-body {
    color:#000;
    padding: 28px;
    flex-grow: 1;
    font-family: 'Segoe UI', sans-serif;
}

.cart-card-title {
    font-size: 1.5rem;
    font-weight: bold;
    color: #1e1e1e;
    margin-bottom: 12px;
}

.cart-card-text {
    font-size: 1.1rem;
    color: #000;
    margin-bottom: 16px;
}

.cart-card-quantity {
    display: flex;
    align-items: center;
    gap: 12px;
}

.quantity-button {
    padding: 8px 18px;
    background: #ffffff;
    border: 1px solid #ddd;
    border-radius: 10px;
    font-weight: 600;
    font-size: 1.1rem;
    color: #333;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
    transition: all 0.2s ease;
    cursor: pointer;
}

.quantity-button:hover {
    background: #f0f2f5;
    transform: scale(1.08);
}

.cart-card-footer {
    position: absolute;
    top: 16px;
    right: 16px;
    opacity: 0;
    transition: opacity 0.3s ease;
}

.cart-card:hover .cart-card-footer {
    opacity: 1;
}

.remove-item {
    width: 64px;
    height: 64px;
    background: linear-gradient(135deg, #ff4e4e, #ff7676);
    border: none;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    color: #fff;
    font-size: 24px;
    box-shadow: 0 6px 18px rgba(255, 60, 60, 0.4);
    cursor: pointer;
    transition: all 0.3s ease;
}

.remove-item:hover {
    transform: scale(1.15) rotate(-10deg);
    box-shadow: 0 12px 28px rgba(255, 0, 0, 0.4);
}
.redeem-btn {
    font-family: Poppins;
    font-size: 15px;
    font-weight: 400;
    height: 30px;
    width: 100%;
    padding: 0 20px;
    border-radius: 3px;
    margin-top: 15px;
    color: #fff;
}
.remove-discount-link {
    color: red;
    margin-left: 10px;
    text-decoration: none;
    font-weight: bold;
    cursor: pointer;
}

.redeemed-giftcard-entry {
    display: flex;
    align-items: center;
    margin: 5px 0;
    font-size: 12px;
    color: #333;
}

.giftcard-code {
    font-weight: bold;
    margin-right: 8px;
}

.remove-giftcard-link {
    color: red;
    text-decoration: none;
    cursor: pointer;
    font-size: 18px;
    margin-left: 5px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
        <div id="emptycartcontainer" class="row spark-over-container" runat="server">
            <div class="category-card-heading">Empty Cart !!</div>
            <div class="cartTitle">Add some items to cart to checkout</div>
            <div class="col-md-4">
                <input type="button" onclick="javascript: redirect();" value="CONTINUE SHOPPING" class="rounded-btn red-btn">
            </div>
        </div>
        <div id="cartcontainer" class="cart-container" runat="server">
            <div class="col-md-12 col-xs-12 no-padding cart-inner-container">
                <div class="col-md-8 col-sm-7 cart-item-container">
                    <h1 class="cartTitle">
                        Shopping cart                  
                    </h1>
                    <div runat="server" id="xdata">
                        <div class="row">
                            <div class="col-md-2">
                                <img alt="" src="menupic/small/181s.png" class="cartItemIcon">
                            </div>
                            <div class="col-md-7">
                                <div id="xmenumane" class="xmenuname">Chicken Biriyani</div>
                                <div id="xqty">
                                    QTY : 
                                    <a href="sparkcart.aspx?minus=261" class="quantity-button">-</a> 
                                    5 
                                    <a href="sparkcart.aspx?plus=261" class="quantity-button">+</a>

                                    <div class="col-md-4 itemDetailCard">PRICE : Rs 450</div>
                                    <div class="col-md-5 itemDetailCard">AMOUNT : Rs 5800</div>
                                </div>
                            </div>
                            <a href="sparkcart.aspx?del=261" class="removeItemImage">
                                <img src="images/remove.png" alt="Remove">
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4 cart-button-container">

                    <div class="col-md-12">
                        <asp:Label ID="litMessage" runat="server" CssClass="message-label" EnableViewState="false"></asp:Label>
                        <div class="cartTotalBox" runat="server" id="xsubtotal">SUB TOTAL : Rs 6700</div>
                        <div class="cartTotalBox" runat="server" id="xdiscountBox" visible="false">
                           <asp:Label ID="lblDiscountReason" runat="server" Text="Discount: Rs 0"></asp:Label>
                           <asp:LinkButton 
                               ID="lnkRemoveDiscount" 
                               runat="server" 
                               OnClick="RemoveDiscount_Click" 
                               CssClass="remove-discount-link">[Remove]</asp:LinkButton>
                           </div>
                        <div class="cartTotalBox" runat="server" id="xtotal" clientidmode="Static">TOTAL : Rs 6700</div>
                    </div>
                   <div id="payBackContainer" runat="server">
                       <div class="col-md-12">
                           <div class="subTotalBox availablePointsBox" id="xavailablepoints" runat="server">
                               AVAILABLE POINTS TO REDEEM: 20
                           </div>
                           <!-- Redeem Button -->
                           <div style="text-align:center;">
                               <asp:Button runat="server" 
                                   ID="btnToggleRedeem" 
                                   CssClass="btn pink-btn" 
                                   Text="Redeem Points" 
                                   OnClientClick="return toggleRedeemSection();" 
                                   Style="margin-top: 10px; color:white;" />
                           </div>


                          <div id="redeemSection" style="display: none; margin-top: 15px;" class="row">
                           <div class="col-md-8">
                               <input class="redeemTextBox form-control numeric" runat="server" id="txtpayback" type="text" placeholder="Points to Redeem" />
                           </div>
                           <div class="col-md-4">
                               <asp:Button runat="server" ID="ImageButton2" CssClass="redeem-btn pink-btn" OnClick="ImageButton2_Click" Text="Redeem" />
                           </div>
                       </div>
                           <div style="text-align:center;">
                               <asp:Button runat="server" 
                                   ID="btnAddGiftCard" 
                                   CssClass="btn btn-link" 
                                   Text="Do you have a gift card? Click here" 
                                   OnClientClick="return toggleGiftCardSection();" 
                                   Style="margin-top: 10px; font-size: 16px;" />
                           </div>
                          <div id="giftCardSection" style="display: none; margin-top: 15px;" class="row">
                           <div class="col-md-8">
                               <input class="redeemTextBox form-control numeric" runat="server" id="txtGift" type="text" placeholder="Gift Card" />
                           </div>
                           <div class="col-md-4">
                               <asp:Button runat="server" ID="Button1" CssClass="redeem-btn pink-btn" OnClick="CheckGiftCard_Click" Text="Apply" />
                           </div>

                       </div> 
                           <!-- Gift Card Display Section -->
                           <div id="giftCardDisplaySection" runat="server" visible="false" style="margin-top: 10px;">
                               <h4 class="redeemed-giftcard-title">Redeemed Gift Cards</h4>
                               <hr />
                               <asp:Repeater ID="rptGiftCards" runat="server">
                                   <ItemTemplate>
                                       <div class="redeemed-giftcard-entry">
                                           <div class="giftcard-code-container">
                                               <span class="giftcard-code"><%# Eval("CardNumber") %></span>
                                           </div>
                                           <asp:LinkButton 
                                               ID="lnkRemoveGiftCard" 
                                               runat="server" 
                                               CommandArgument='<%# Eval("CardNumber") %>' 
                                               OnClick="RemoveGiftCard_Click" 
                                               CssClass="remove-giftcard-link"
                                               ToolTip="Remove Gift Card">✖</asp:LinkButton>
                                       </div>
                                   </ItemTemplate>
                               </asp:Repeater>
                           </div>
                       </div>
                   
                       <!-- Hidden Redeem Inputs Section -->
                   </div>
                    <div>
                        <asp:Button ID="ImageButton1" runat="server" class="rounded-btn green-btn" Text="PLACE ORDER" OnClick="ImageButton1_Click3" OnClientClick="if(dxr() === false) return false;" />
                    </div>
                    <div>
                        <asp:Button runat="server" class="rounded-btn transparent-btn" OnClick="RedirectShopping_Click" Text="CONTINUE SHOPPING"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
