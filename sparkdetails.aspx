<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="sparkdetails.aspx.cs" Inherits="spark" %>

<%@ Register TagPrefix="msb" TagName="MenuSideBar" Src="~/MenuSideBar.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>VIEW PREMIUM DELICACIES</title>

    <script>
        $(document).ready(function () {
            $("#xweight").on("change paste keyup", function () {
                multy();
            });
            checkgrp();
        });

        function checkgrp() {
            var sp = $.cookie('xgroup');
            if (sp !== "Cakes") {
                $("#x1").hide();
                $("#x2").hide();
                $("#x3").hide();
            }
        }

        function successalert() {
            swal({
                title: 'OOPS !! BELOW MINIMUM WEIGHT',
                text: 'The cake must be more than minimum weight',
                type: 'errors'
            });
        }

        function multy() {
            var minwt = $.cookie('xminweight');
            var wt = document.getElementById('xweight').value;

            if (wt > minwt) {
                var sp = $.cookie('xsellprice');
                var amt = Math.ceil(wt * sp);
                document.getElementById("xrate").innerHTML = "Rs " + amt + " (Rs " + sp + "/Kg)";
            } else {
                if (wt === minwt) {
                    var sp = $.cookie('xsellprice');
                    var amt = Math.ceil(wt * sp);
                    document.getElementById("xrate").innerHTML = "Rs " + amt + " (Rs " + sp + "/Kg)";
                } else {
                    if (wt !== "") {
                        var sp = $.cookie('xsellprice');
                        var textbox = document.getElementById('xweight');
                        textbox.value = minwt;
                        var amt = Math.ceil(minwt * sp);
                        document.getElementById("xrate").innerHTML = "Rs " + amt + " (Rs " + sp + "/Kg)";

                        swal("Minimum Weight : " + minwt + " Kg", "Please check the weight of the item!", "error");
                    }
                }

            }

        }
        function smallimgclick(obj) {
            var newSrc = obj.querySelector("img").getAttribute("src");
            var bigImg = document.getElementById("bigimg");
            var zoomResult = document.getElementById("zoomResult");

            if (bigImg) {
                bigImg.setAttribute("src", newSrc);
            }

            if (zoomResult) {
                zoomResult.style.backgroundImage = `url('${newSrc}')`;
            }

        }

        window.addEventListener("load", function () {
            const img = document.getElementById("bigimg");
            const zoomResult = document.getElementById("zoomResult");

            if (!zoomResult) return;

            // Set the zoom image source
            zoomResult.style.backgroundImage = `url('${img.src}')`;

            img.addEventListener("load", () => {
                zoomResult.style.backgroundSize = `${img.naturalWidth * 1.5}px ${img.naturalHeight * 1.5}px`;
            });

            function moveZoom(e) {
                const bounds = img.getBoundingClientRect();
                const x = e.clientX - bounds.left;
                const y = e.clientY - bounds.top;

                const xPercent = (x / bounds.width) * 100;
                const yPercent = (y / bounds.height) * 100;

                zoomResult.style.backgroundPosition = `${xPercent}% ${yPercent}%`;
            }

            img.addEventListener("mousemove", moveZoom);
            img.addEventListener("mouseenter", () => zoomResult.style.display = "block");
            img.addEventListener("mouseleave", () => zoomResult.style.display = "none");
        });
    </script>
    <style>


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
        <div itemscope="" itemtype="https://schema.org/Product" id="xdata" class="col-md-12 product-details-container">
            <meta itemprop="brand" content="Paris Bakery">
            <meta itemprop="url" content="<%=ItemUrl.ToString()%>">
            <div class="col-md-6 login-container">
                <meta itemprop="image" content="<%=ImageUrl.ToString()%>">
                <div class="imgBox" id="ximgshowser" runat="server">
                    <img alt="Placeholder" id="zoomImage"  class="imgBoxImageSize" src="images/svg-icons/img-placeholder.svg" data-origin="menupic/big/1b.png" />
                    <div id="lens"></div>
                    <div class="veg-symbol"></div>
                </div>

                <div class="row smallimgBox" runat="server">
                    <ul id="prodImages" runat="server">
                        <li id="ProductThumbnail_1" runat="server">
                            <div class="smallimgBoxImageSize" id="ximgsmall1" runat="server" onclick="smallimgclick(1)">
                                <img alt="Thumbnail" src="images/svg-icons/img-placeholder.svg" data-origin="menupic/big/1b.png" class="smallimgBoxImageSize" />
                            </div>
                        </li>
                        <li id="ProductThumbnail_2" runat="server">
                            <div class="smallimgBoxImageSize" id="ximgsmall2" runat="server" onclick="smallimgclick(2)">
                                <img alt="Thumbnail" src="images/svg-icons/img-placeholder.svg" data-origin="menupic/big/1b.png" class="smallimgBoxImageSize" />
                            </div>
                        </li>
                        <li id="ProductThumbnail_3" runat="server">
                            <div class="smallimgBoxImageSize" id="ximgsmall3" runat="server" onclick="smallimgclick(3)">
                                <img alt="Thumbnail" src="images/svg-icons/img-placeholder.svg" data-origin="menupic/big/1b.png" class="smallimgBoxImageSize" />
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-md-6">
                <div class="sectionContent">
                    <h1 id="xmenumane" runat="server" class="xmenuname" itemprop="name">
                        Cakes 
                    </h1>
                    <div class="rating-box">
                         <div>
                              <span class="star">★</span>
                              <span class="rating-value">4.<%= new Random().Next(1, 9) %></span>                           
                         </div>
                    </div>
                    <div id="xgroupsubgroup" runat="server" class="xgroupsubgroup">
                        Cakes
                    </div>
                    <asp:ImageButton ID="ImageButton2" Width="180" runat="server" ImageUrl="~/images/out.png" CssClass="nooutline" />
                    <div id="xrate" runat="server" class="xrate" itemprop="offers" itemscope="" itemtype="https://schema.org/Offer">
                        Rs 250
                    </div>
                    <div id="x2" runat="server" class="row">
                        <div class="col-md-12 product-prop-heading">
                            Weight
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="weightradiobtns" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Weight_Changed">
                                <asp:ListItem Text="Select Weight" Value="" />
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div id="x3" runat="server" class="row">
                        <div class="col-md-12 product-prop-heading">
                            Flavour
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="timerx form-control" AutoPostBack="True" CausesValidation="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="x1" runat="server" class="row">
                        <div class="col-md-12 product-prop-heading">
                            Cake Message
                        </div>
                        <div class="col-md-6">
                            <input runat="server" id="xwish" type="text" class="form-control" placeholder="Enter message on cake" />
                        </div>
                    </div>
                    <div id="x4" runat="server" class="row">
                        <div class="col-md-12 product-prop-heading">
                            Quantity
                        </div>
                        <div class="col-md-6">
                            <input runat="server" id="xqty" type="text" class="form-control numeric" maxlength="3" value="1" />
                        </div>
                    </div>
                    <div id="x5" runat="server" class="row">
                        <div class="col-md-6">
                            <asp:Button runat="server" ID="ImageButton1" class="rounded-btn pink-btn" Text="Add To Cart" OnClick="ImageButton1_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12 product-details-description">
        <h2>Cake Description</h2>
        <p>Turn your celebration into a sizzling party by ordering this adorable cake. This delicious cake will drivel the taste buds of all the party attendees and most impotantly an ideal delicacy to make someone special feel on any special occasion by gifting this delicious cake.</p>
    </div>

    <div class="col-md-12 time-smile-content">
        <div class="time-smile">
            <div class="thumb-time">
                <div class="thumb-image">
                    <img src="data:image/png;base64,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" alt="100% On Time Delivery" width="100%" height="100%">
                </div>
                <div class="time-content"><span class="percent-bold">100%</span>On Time Delivery</div>
            </div>
            <div class="thumb-time">
                <div class="thumb-image">
                    <img src="data:image/png;base64,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" alt="100% Payment Protection" width="100%" height="100%">
                </div>
                <div class="time-content"><span class="percent-bold">100%</span>Payment Protection</div>
            </div>
            <div class="thumb-time">
                <div class="thumb-image">
                    <img src="data:image/png;base64,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" alt="100% Smiles Delivered" width="100%" height="100%">
                </div>
                <div class="time-content"><span class="percent-bold">100%</span>Smiles Delivered</div>
            </div>
        </div>
    </div>

    <div class="col-md-12 corona-background-title" style="display:none;">
        <div class="big-small-title">
            <div class="corona-title-big">Your Safety is our Priority!</div>
            <div class="corona-title-small">Steps we are taking to keep you safe</div>
        </div>
        <div class="corona-content">
            <div class="corona-image-title">
                <div class="corona-image">
                    <img src="images/CoronaImages/mask-group1.svg" alt="Sanitization" width="100%" height="100%">
                </div>
                <div class="corona-title-image">
                    <img src="images/CoronaImages/group-title1.svg" alt="Sanitization" width="100%" height="100%">
                </div>
                <div class="corona-mobile-image">
                    <img src="images/CoronaImages/group-mobile1.svg" alt="Sanitization" width="100%" height="100%">
                </div>
            </div>
            <div class="corona-image-title">
                <div class="corona-image">
                    <img src="images/CoronaImages/mask-group2.svg" alt="Contact Less delivery" width="100%" height="100%">
                </div>
                <div class="corona-title-image">
                    <img src="images/CoronaImages/group-title2.svg" alt="Contact Less delivery" width="100%" height="100%">
                </div>
                <div class="corona-mobile-image">
                    <img src="images/CoronaImages/group-mobile2.svg" alt="Contact Less delivery" width="100%" height="100%">
                </div>
            </div>
            <div class="corona-image-title">
                <div class="corona-image">
                    <img src="images/CoronaImages/mask-group3.svg" alt="Temperature Checks" width="100%" height="100%">
                </div>
                <div class="corona-title-image">
                    <img src="images/CoronaImages/group-title3.svg" alt="Temperature Checks" width="100%" height="100%">
                </div>
                <div class="corona-mobile-image">
                    <img src="images/CoronaImages/group-mobile3.svg" alt="Temperature Checks" width="100%" height="100%">
                </div>
            </div>
            <div class="corona-image-title">
                <div class="corona-image">
                    <img src="images/CoronaImages/mask-group4.svg" alt="Compulsory Mask and Gloves" width="100%" height="100%">
                </div>
                <div class="corona-title-image">
                    <img src="images/CoronaImages/group-title4.svg" alt="Compulsory Mask and Gloves" width="100%" height="100%">
                </div>
                <div class="corona-mobile-image">
                    <img src="images/CoronaImages/group-mobile4.svg" alt="Compulsory Mask and Gloves" width="100%" height="100%">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
