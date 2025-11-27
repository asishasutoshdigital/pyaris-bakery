<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function myFunction() {
            var x1 = document.getElementById("inputName").value;
            var x2 = document.getElementById("inputPhone").value;
            var x3 = document.getElementById("inputEmail").value;
            var x4 = document.getElementById("inputMessage").value;

            if (x1 != "" && x2 != "" && x3 != "" && x4 != "") {
                var tttt = "NAME : " + x1 + ", PHONE : " + x2 + ", EMAIL : " + x3 + ", MESSAGE : " + x4;
                $.ajax({
                    type: "POST",
                    url: "sendf.asmx/SendMessage",
                    data: "{message:'" + tttt + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        swal({
                            title: "Thank You !",
                            text: "We will contact you soon.",
                            showCancelButton: true,
                            showConfirmButton: false,
                            animation: "slide-from-top",
                            cancelButtonText: "OK"
                        });
                    }
                });
            }
            else {
                swal({
                    title: "Alert !",
                    text: "Please fill required fields to submit.",
                    showCancelButton: true,
                    showConfirmButton: false,
                    animation: "slide-from-top",
                    cancelButtonText: "OK"
                });
            }
        }

        //window.onload = function () {
        //    if (!sessionStorage.getItem("selectedPincode")) {
        //        openPopup();
        //    }
        //};
    </script>

    <style>
                .sweet-alert button.cancel {
            background-color: #ff5f5d;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--LOADER--%>
    <%--<div id="site-loader" class="load-complete">
        <div class="loader">
            <div class="loader-inner ball-clip-rotate">
                <div></div>
            </div>
        </div>
    </div>--%>
    <h1 class="home-title">Paris Bakery | Home</h1>
    <%------------CAROUSEL------------%>
    <div class="slick-center">
            <asp:Repeater ID="rptHomepageSlider" runat="server">
                <ItemTemplate>
                    <div class="photo-slider">
                        <a href='spark.aspx?xdt=<%# Server.UrlEncode(Eval("data").ToString()) %>'
                           title='<%# Eval("data") %>'>
                            <img src='<%# Eval("options") %>' alt='<%# Eval("data") %>' />
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
    </div>
  <%-- <div class="desktop-safety-banner-container" style="font-size:20px">
	<a href="https://www.parisbakery.in">You can also place your orders in parisbakery.in</a>
    </div>--%>    

    <%------------SAFETY------------%>
    <div class="desktop-safety-banner-container" style="display:none">
        <div class="desktop-safety-banner">
            <img title="safety" src="images/Safety/desktopsafetybanner.png" alt="Safety Banner" class="img-fluid v-align-bottom" />
        </div>
    </div>
    <div class="mobile-safety-banner" style="display:none">
        <div class="safety-header-container">
            <div class="safety-header-heading">Your Safety is Our Priority</div>
            <div class="safety-header-text">Steps we are taking</div>
        </div>
        <div class="mobile-safety-image">
            <img src="images/Safety/safety-slider1.svg" alt="0" />
            <img src="images/Safety/safety-slider2.svg" alt="1" />
            <img src="images/Safety/safety-slider3.svg" alt="2" />
            <img src="images/Safety/safety-slider4.svg" alt="3" />
        </div>
    </div>

    <%------------EXPERIENCE FLAVOURS------------%>
    <div class="category-card-container flavour-card-container">
        <div class="category-card-heading">Experience Flavours</div>
<div class="category-card-content">
    <a class="category-card" href="spark.aspx?xdt=Chocolate Cakes&isFlavour=true" title="Buy Chocolate Cakes Online | Paris Bakery">
        <img src="images/ExperienceFlavours/chocolate.jpg" alt="Delicious Chocolate Cakes Online" loading="lazy" />
        <div class="category-card-title">Chocolate</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Fruit Cakes&isFlavour=true" title="Order Fresh Fruit Cakes | Paris Bakery">
        <img src="images/ExperienceFlavours/mixed_fruits.jpg" alt="Mixed Fruit Cakes - Freshly Baked" loading="lazy" />
        <div class="category-card-title">Fruit</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Mango Cakes&isFlavour=true" title="Mango Cakes for Summer | Paris Bakery">
        <img src="images/ExperienceFlavours/exotics_pastry.png" alt="Exotic Mango Cakes - Order Now" loading="lazy" />
        <div class="category-card-title">Mango</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Black Forest Cakes&isFlavour=true" title="Classic Black Forest Cakes | Paris Bakery">
        <img src="images/ExperienceFlavours/black_forest.jpg" alt="Black Forest Cakes with Cherries" loading="lazy" />
        <div class="category-card-title">Black Forest</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Butterscotch Cakes&isFlavour=true" title="Butterscotch Cakes Online Delivery">
        <img src="images/ExperienceFlavours/butterscotch.jpg" alt="Butterscotch Cakes with Caramel Topping" loading="lazy" />
        <div class="category-card-title">Butterscotch</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Vanilla Cakes&isFlavour=true" title="Classic Vanilla Cakes | Paris Bakery">
        <img src="images/ExperienceFlavours/Vanilla.jpg" alt="Vanilla Cakes for All Occasions" loading="lazy" />
        <div class="category-card-title">Vanilla</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Coffee Cakes&isFlavour=true" title="Buy Coffee Cakes Online | Paris Bakery">
        <img src="images/ExperienceFlavours/coffee.png" alt="Coffee Flavored Cakes for Coffee Lovers" loading="lazy" />
        <div class="category-card-title">Coffee</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Strawberry Cakes&isFlavour=true" title="Strawberry Cakes Delivery | Paris Bakery">
        <img src="images/ExperienceFlavours/strawberry.jpg" alt="Fresh Strawberry Cakes with Cream" loading="lazy" />
        <div class="category-card-title">Strawberry</div>
    </a>
</div>
    </div>

    <%------------DESIGNER CAKES------------%>
    <div class="category-card-container">
        <div class="category-card-heading">Designer Cakes</div>
<div class="category-card-content">

    <a class="category-card" href="spark.aspx?xdt=Spiderman Cakes" title="Order Spiderman Theme Cakes Online | Paris Bakery">
        <img src="images/DesignerCakes/spiderman.jpg" alt="Spiderman Theme Birthday Cake for Kids" loading="lazy" />
        <div class="category-card-title">Spiderman Cakes</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Rainbow Cakes" title="Colorful Rainbow Cakes for Celebrations">
        <img src="images/DesignerCakes/rainbow.jpg" alt="Vibrant Rainbow Cakes for Birthdays and Parties" loading="lazy" />
        <div class="category-card-title">Rainbow Cakes</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Baby shower Cakes" title="Baby Shower Cake Designs | Paris Bakery">
        <img src="images/DesignerCakes/babyshower.jpg" alt="Cute Baby Shower Cakes for Boys and Girls" loading="lazy" />
        <div class="category-card-title">Baby Shower Cakes</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Mickey Mouse Cakes" title="Mickey Mouse Birthday Cakes for Kids">
        <img src="images/DesignerCakes/mickeymouse.jpg" alt="Mickey Mouse Themed Cakes for Children" loading="lazy" />
        <div class="category-card-title">Mickey Mouse Cakes</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=PUBG Cakes" title="PUBG Gamer Birthday Cakes | Paris Bakery">
        <img src="images/DesignerCakes/pubg.jpg" alt="PUBG Game Inspired Designer Cakes" loading="lazy" />
        <div class="category-card-title">PUBG Cakes</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Minion Cakes" title="Minion Themed Birthday Cakes for Kids">
        <img src="images/DesignerCakes/minion.jpg" alt="Fun Minion Cakes - Custom Designs Available" loading="lazy" />
        <div class="category-card-title">Minion Cakes</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Chota Bheem Cakes" title="Chhota Bheem Birthday Cakes Online | Paris Bakery">
        <img src="images/DesignerCakes/bheem.jpg" alt="Chhota Bheem Character Cakes for Children" loading="lazy" />
        <div class="category-card-title">Chhota Bheem Cakes</div>
    </a>

    <a class="category-card" href="spark.aspx?xdt=Cartoon Cakes" title="Cartoon Character Birthday Cakes | Paris Bakery">
        <img src="images/DesignerCakes/cartoon.jpg" alt="Cartoon Character Themed Cakes for Kids" loading="lazy" />
        <div class="category-card-title">Cartoon Cakes</div>
    </a>
</div>
    </div>

    <%------------SOMETHING SPECIAL------------%>
    <div class="cup-cakes-container">
        <div class="category-card-heading">Looking for Something Else</div>
<div class="about-us-card-description">

    <a href="spark.aspx?xdt=Cup%20Cakes" class="cup-cakes" title="Order Fresh Cup Cakes Online | Paris Bakery">
        <div class="cup-cakes-desktop-image">
            <img 
                src="images/SpecialCakes/cup_cakes_desktop.png" 
                alt="Delicious Cup Cakes - Available in Various Flavors" 
                title="Order Cup Cakes Online" 
                class="img-fluid v-align-bottom" 
                loading="lazy" />
        </div>
        <div class="cup-cakes-mobile-image">
            <img 
                src="images/SpecialCakes/cup_cakes_mobile.png" 
                alt="Mobile View - Cup Cakes Selection" 
                title="Cup Cakes Mobile View" 
                loading="lazy" />
        </div>
    </a>

    <a href="spark.aspx?xdt=Jar%20Cakes" class="cup-cakes" title="Buy Jar Cakes Online | Paris Bakery">
        <div class="cup-cakes-desktop-image">
            <img 
                src="images/SpecialCakes/jar_cakes_desktop.png" 
                alt="Delicious Jar Cakes with Creamy Layers" 
                title="Order Jar Cakes Online" 
                class="img-fluid v-align-bottom" 
                loading="lazy" />
        </div>
        <div class="cup-cakes-mobile-image">
            <img 
                src="images/SpecialCakes/jar_cakes_mobile.png" 
                alt="Jar Cakes Mobile Display" 
                title="Jar Cakes Mobile View" 
                loading="lazy" />
        </div>
    </a>

    <a href="spark.aspx?xdt=Pastry" class="cup-cakes" title="Pastries Online Delivery | Paris Bakery">
        <div class="cup-cakes-desktop-image">
            <img 
                src="images/SpecialCakes/pastry_cakes_desktop.png" 
                alt="Tasty Pastries for Every Occasion" 
                title="Order Pastries Online" 
                class="img-fluid v-align-bottom" 
                loading="lazy" />
        </div>
        <div class="cup-cakes-mobile-image">
            <img 
                src="images/SpecialCakes/pastry_cakes_mobile.png" 
                alt="Pastry Cakes Mobile View" 
                title="Pastry Cakes Mobile View" 
                loading="lazy" />
        </div>
    </a>
</div>
    </div>

    <%------------CONTACT US------------%>
    <div class="about-section-container">
        <div class="about-us-card-description">
            <div class="image-form">
                <div class="image-plate-spoon">
                    <img alt="About" title="about" src="images/new-images/contact_form.png" class="img-fluid v-align-bottom" width="100%" height="100%">
                </div>
                <div class="contact-form-submit">
                    <div class="contact-header-form">..Seeking for something special and customized ?</div>
                    <form>
                        <div class="form-row">
                            <div class="form-group col-md-12 position">
                                <label>Your Name <span class="requiredStar">*</span></label>
                                <input type="text" class="form-control form-control-sm sizeFixed" id="inputName">
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-12 position">
                                <label>Phone Number <span class="requiredStar">*</span></label>
                                <input type="text" maxlength="10" class="form-control form-control-sm sizeFixed" id="inputPhone">
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-12 position">
                                <label>Email <span class="requiredStar">*</span></label>
                                <input type="email" class="form-control form-control-sm sizeFixed" id="inputEmail">
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-12 position">
                                <label>Message <span class="requiredStar">*</span></label>
                                <textarea maxlength="250" class="form-control message-sizeFixed" id="inputMessage" rows="3"></textarea>
                            </div>
                        </div>
                        <div class="contact-submit-cont">
                            <input type="button" onclick="javascript: myFunction();" value="Submit form" class="contact-submit-form">
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <%------------ABOUT US------------%>
    <div class="about-us-container about-section-container">
        <div class="about-us-card-description">
            <div id="aboutus-container" class="aboutus-title-description-image">
                <div class="about-us-mobile">About us</div>
                <div class="aboutus-image-data">
                    <div class="aboutus-description">
                        <div class="about-us">About us</div>
                        <div class="about-description">
                            We are exactly what you are looking for. Yes, we are an FSSAI certified online cake and Bakery Company that specializes in delivering absolutely lip-smacking delicacies. Currently, we are delivering cakes in Bhuvaneshwar.
                          We are not just a bakery, we are not just bakers. In fact, we are just like you, a bunch of food lovers fascinated with sweet indulgence, who dreamt of creating an appetizing fairy land of divine delicacies that relishes the utmost desires.
                        </div>
                        <a href="about.aspx">
                            <div class="seeMore-container1">
                                <span class="seeMoreText">Read</span>
                            </div>
                        </a>
                    </div>
                    <div class="image-plate">
                        <img src="images/new-images/aboutusimage.png" alt="About" class="image-about">
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
