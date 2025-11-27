<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function myFunction() {
            var x1 = document.getElementById("input_fname").value;
            var x2 = document.getElementById("input_lname").value;
            var x3 = document.getElementById("input_email").value;
            var x4 = document.getElementById("input_phone").value;
            var x5 = document.getElementById("input_subject").value;
            var x6 = document.getElementById("textarea_message").value;

            var tttt = "FNAME : " + x1 + ", LNAME : " + x2 + ", EMAIL : " + x3 + ", PHONE : " + x4 + ", SUBJECT : " + x5 + ", TEXTAREA MESSAGE : " + x6;

            $.ajax({


                type: "POST",
                url: "sendf.asmx/SendMessage",
                data: "{message:'" + tttt + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    alert("\n\nThankyou.\n\n\n We will contact you soon.\n\n\n\n");
                }
            });
        }

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-36251023-1']);
        _gaq.push(['_setDomainName', 'jqueryscript.net']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-title-banner">
        <div class="page-title-text">
            <h1 class="page-title-heading">Contact Us</h1>
        </div>
    </div>
    <main class="site-main page-spacing">
        <div class="contact-us-1 container-fluid no-padding">
            <div class="container ContentSpacing">
                <div class="col-md-7">
                    <div class="section-header left-header">
                        <h3>General Inquiry & Feedback</h3>
                        <h5>We value your opinion</h5>
                        <img src="images/section-seprator-1.png" alt="section-seprator-1" width="70" height="3">
                        <p>Please fill out the below form. Send us your inquiry and feedback. We will come back to you as quickly as possible.</p>
                    </div>
                    <form class="row">
                        <div class="form-group col-md-6">
                            <input type="text" id="input_fname" name="contact-fname" required="" placeholder="First Name" class="form-control">
                        </div>
                        <div class="form-group col-md-6">
                            <input type="text" id="input_lname" name="contact-lname" required="" placeholder="Last Name" class="form-control">
                        </div>
                        <div class="form-group col-md-6">
                            <input type="text" id="input_email" name="contact-email" required="" placeholder="Your Email" class="form-control">
                        </div>
                        <div class="form-group col-md-6">
                            <input type="text" id="input_phone" required="" name="contact-phone" placeholder="Your Phone Number" class="form-control">
                        </div>
                        <div class="form-group col-md-12">
                            <input type="text" id="input_subject" name="contact-subject" required="" placeholder="Subject" class="form-control">
                        </div>
                        <div class="form-group col-md-12">
                            <textarea id="textarea_message" name="contact-message" placeholder="Your Message" rows="6" class="form-control"></textarea>
                            <div runat="server" id="ErrorText" class="error-text"></div>
                        </div>
                        <input type="submit" title="Send Now" value="Send Now" id="btn_submit" name="Send Now" onclick="myFunction();">
                        <div id="alert-msg" class="alert-msg"></div>
                    </form>
                </div>

                <div class="col-md-5 contact-detail">
                    <div class="contact-content">
                        <div class="contact-info">
                            <h3>Paris Bakery(HO)</h3>
                            <p>F/3, Chandaka IE,</p>
                            <p>Chandrasekharpur,</p>
                            <p>Bhubaneswar-751024</p>
                        </div>
                    </div>
                    <!-- Map Section -->
                    <div class="map">
                        <div class="map-canvas">
                            <iframe class="col-lg-12 col-md-12 col-sm-12 col-xs-12" height="500" id="gmap_canvas" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3741.0183768422926!2d85.80606745321566!3d20.34085949057096!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3a1909864d29d01b%3A0xe6a33f56802740eb!2sParis%20Bakery%20Corporate%20office!5e0!3m2!1sen!2sus!4v1686142532061!5m2!1sen!2sus" width="600" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"></iframe>
                        </div>
                    </div>
                    <!--  Map Section /- -->
                </div>
            </div>
            <div class="section-padding"></div>
        </div>

        <!-- Newsletter Box -->
        <%--<div class="subscribe-section container-fluid" id="subscribe-section">
            <div class="container">
                <h4 class="subscribe-title col-md-5 col-sm-4 col-xs-12">subscribe to our newsletter</h4>
                <form class="col-md-7 col-sm-8 col-xs-12">
                    <div class="col-md-9 col-sm-8 col-xs-8">
                        <input type="text" placeholder="Your Email Here" name="email"><i class="fa fa-envelope-o" aria-hidden="true"></i>
                    </div>
                    <div class="col-md-3 col-sm-4 col-xs-4 no-left-padding">
                        <input type="submit" value="subscribe" class="btn-default">
                    </div>
                </form>
            </div>
        </div>--%>
        <!-- Newsletter Box /- -->
    </main>
</asp:Content>
