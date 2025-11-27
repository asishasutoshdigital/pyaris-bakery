<%@ Page Language="C#" AutoEventWireup="true" CodeFile="whatsapp.aspx.cs" Inherits="whatsapp" %>


<!DOCTYPE html>

<html>
<head>
    <title>WhatsApp Paris Bakery</title>
</head>
<body>
   
<form method='post' name="redirect" action="https://api.whatsapp.com/send?phone=918018114444&text=Hi,%20I%20would%20like%20to%20Place%20an%20Order%20at%20Paris%20Bakery" id="whatsAppLink">
</form>

<script>
    var form = document.getElementById("whatsAppLink");
    var url = form.action;
    window.location.href = url;
</script>

</body>
</html>
