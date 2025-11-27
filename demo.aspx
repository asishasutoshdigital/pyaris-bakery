<%@ Page Language="C#" AutoEventWireup="true" CodeFile="demo.aspx.cs" Inherits="demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery.imgzoom.js"></script>
    <style>
        .imgBox {
            width: 200px;
            height: 200px;
        }
    </style>
</head>
<body>



    <form id="form1" runat="server">
        <div class="imgBox">
            <img alt="" src="http://parisbakery.in/menupic/small/1s.png" data-origin="menupic/big/1b.png" style="display: block; width: 200px; height: 200px" />
        </div>
        <script type="text/javascript">
            $('.imgBox').imgZoom({
                boxWidth: 400,
                boxHeight: 400,
                marginLeft: 5,
                origin: 'data-origin'
            });
        </script>
    </form>
</body>
</html>
