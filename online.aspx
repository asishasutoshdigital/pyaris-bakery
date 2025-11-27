<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="online.aspx.cs" Inherits="spark" %>

<%@ Register TagPrefix="msb" TagName="MenuSideBar" Src="~/MenuSideBar.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
        <div class="col-md-12">
            <msb:MenuSideBar ID="Menu" runat="server" />

            <div class="col-md-9 menu-right-content" id="xdata">
                <div class="menuPosition">
                    <img src="images/CFG.png" alt="background" />
                    <div class="pinCodeVerifyImg">
                        <div class="pinCodeVerifyContainer">
                            <a href="javascript:tyyy();" class="pinCodeVerifyLink"></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>

        function getCookie(name) {
            var dc = document.cookie;
            var prefix = name + "=";
            var begin = dc.indexOf("; " + prefix);
            if (begin == -1) {
                begin = dc.indexOf(prefix);
                if (begin != 0) return null;
            }
            else {
                begin += 2;
                var end = document.cookie.indexOf(";", begin);
                if (end == -1) {
                    end = dc.length;
                }
            }
            return decodeURI(dc.substring(begin + prefix.length, end));
        }

        $(document).ready(function () {

            var myCookie = getCookie("mypinverified");
            if (myCookie == null) {
                swal({
                    title: "Verify Your PINCODE !",
                    text: "Check PINCODE if You want Home Delivery !!!",
                    type: "input",
                    showCancelButton: true,
                    closeOnConfirm: false,
                    confirmButtonColor: "#a30f89",
                    animation: "slide-from-top",
                    confirmButtonText: "Verify My Pincode",
                    cancelButtonText: "Cancel",
                    inputPlaceholder: "Enter Pincode",
                    customClass: "numeric"
                },
                    function (inputValue) {
                        if (inputValue === false) return false;

                        if (inputValue === "" || inputValue.length < 6 || inputValue.length > 6) {
                            swal.showInputError("Please Enter Correct PinCode !!");
                            return false;
                        }

                        var string = "751001, 751022, 751009, 751010, 751005, 751007, 751020, 751006, 751002, 751011, 760001, 761005, 761001, 760003, 760002, 760004, 760005, 760008, 760010, 758002, 758001, 758014, 769001, 769004, 769015, 769007, 769012, 751018, 751014, 751025, 751017, 751015, 751004, 751003, 751030, 751012, 751008, 756001, 756002, 756003, 755019, 755020, 752002, 752001, 752003, 751024, 754132, 754005, 751021, 753001, 751013, 751023, 753006, 753003, 753008, 753004, 753015, 753009, 754004, 753002, 753010, 753014, 754006, 753013, 753012, 753007, 756100, 756101,711106, 711105, 711104, 711107, 711108, 711109,756001,756002,756003",
                            substring = inputValue;
                        var vali = string.includes(substring);

                        if (vali === true) {
                            document.cookie = "mypinverified=yes";
                            swal("Great !!!", " We can  deliver at this Pincode : " + inputValue + ".\n\nYou can also Pickup From Our Outlet", "success");
                        }
                        else {
                            swal.showInputError("OOPS !!! We cannot deliver to pincode ", "error");
                            return false;

                        }

                    });
            }
        });

        function tyyy() {
            swal({
                title: "Verify Your PINCODE !",
                text: "Check PINCODE if You want Home Delivery !!!",
                type: "input",
                showCancelButton: true,
                closeOnConfirm: false,
                confirmButtonColor: "#a30f89",
                animation: "slide-from-top",
                confirmButtonText: "Verify My Pincode",
                cancelButtonText: "Cancel",
                inputPlaceholder: "Enter Pincode",
                customClass: "numeric"
            },
                function (inputValue) {
                    if (inputValue === false) return false;

                    if (inputValue === "" || inputValue.length < 6 || inputValue.length > 6) {
                        swal.showInputError("Please Enter Correct PinCode !!");
                        return false;
                    }

                    var string = "751001, 751022, 751009, 751010, 751005, 751007, 751020, 751006, 751002, 751011, 760001, 761005, 761001,  760003, 760002, 760004, 760005, 760008, 760010, 758002, 758001, 758014, 769001, 769004, 769015, 769007, 769012, 751018, 751014, 751025, 751017, 751015, 751004, 751003, 751030, 751012, 751008, 756001, 756002, 756003, 755019, 755020, 752002, 752001, 752003, 751024, 754132, 754005, 751021, 753001, 751013, 751023, 753006, 753003, 753008, 753004, 753015, 753009, 754004, 753002, 753010, 753014, 754006, 753013, 753012, 753007, 756100, 756101, 700074, 700076, 700073, 711106, 711105, 711104, 711107, 711108, 711109, 756001, 756002,756003",
                        substring = inputValue;
                    var vali = string.includes(substring);

                    if (vali === true) {
                        document.cookie = "mypinverified=yes";
                        swal("Great !!!", " We can  deliver at this Pincode : " + inputValue + ".\n\nYou can also Pickup From Our Outlet", "success");
                    }
                    else {
                        swal.showInputError("OOPS !!! We cannot deliver to pincode ", "error");
                        return false;

                    }

                });
        }
    </script>
</asp:Content>