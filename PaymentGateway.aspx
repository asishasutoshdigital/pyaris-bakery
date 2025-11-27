<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentGateway.aspx.cs" Inherits="PaymentGateway" %>

<!DOCTYPE html>

<html>
<head>
    <title>Merchant Check Out Page</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script>

</head>
<body>
    <h1>Please do not refresh this page...</h1>
    <form method='post' name="redirect" action="https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction" id="nonseamless">
        <input type="hidden" id="encRequest" name="encRequest" runat="server" />
        <input type="hidden" id="access_code" name="access_code" runat="server" />
        <button type="submit" hidden>submit</button>
    </form>

    <input type="hidden" id="pgName" name="pgName" runat="server" />


    <script type='text/javascript'>

        $(document).ready(function () {            
            var pg = $("#pgName").val();
            if (pg == 'CCAvenue') {
                document.getElementById("nonseamless").submit();
            }
            else if (pg == 'PhonePe') {
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("~/PaymentGateway.aspx/GetPhonePeUrl") %>',
                    data: "",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d.success) {
                            window.location.href = response.d.data.instrumentResponse.redirectInfo.url;
                        }
                        else {
                            alert('Unable to initiate Payment, please try again');
                        }
                    },
                    error: function (response) {
                        alert(response);
                    },
                    failure: function (response) {
                        alert(response);
                    }
                });
            }
        });
    </script>
</body>
</html>
