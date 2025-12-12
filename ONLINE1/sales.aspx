<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sales.aspx.cs" Inherits="ONLINE_sales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Paris Bakery |</title>
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	
	<script>
		addEventListener("load", function () {
			setTimeout(hideURLbar, 0);
		}, false);

		function hideURLbar() {
			window.scrollTo(0, 1);
		}
	</script>
	<!-- fonts -->
	<link href="//fonts.googleapis.com/css?family=Source+Sans+Pro:200,200i,300,300i,400,400i,600,600i,700,700i,900,900i" rel="stylesheet">
	<!-- /fonts -->
	<!-- css -->
	<link href="css/bootstrap.css" rel="stylesheet" type='text/css' media="all" />
	<link href="css/style.css" rel="stylesheet" type='text/css' media="all" />
     <script src="../datetimepicker_css.js"></script>

    <style type="text/css">
       .Grid {background-color: #fff; margin: 5px 0 10px 0; border: solid 1px #525252; border-collapse:collapse; font-family:Calibri; color: #474747;}

.Grid td {

      padding: 2px;

      border: solid 1px #c1c1c1; }

.Grid th  {

      padding : 4px 2px;

      color: #fff;

      background: #363670 url(Images/grid-header.png) repeat-x top;

      border-left: solid 1px #525252;

      font-size: 0.9em; }

.Grid .alt {

      background: #fcfcfc url(Images/grid-alt.png) repeat-x top; }

.Grid .pgr {background: #363670 url(Images/grid-pgr.png) repeat-x top; }

.Grid .pgr table { margin: 3px 0; }

.Grid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666; font-weight: bold; color: #fff; line-height: 12px; }  

.Grid .pgr a { color: Gray; text-decoration: none; }

.Grid .pgr a:hover { color: #000; text-decoration: none; }
    </style>
	<!-- /css -->
</head>
<body>
    <form id="form1" runat="server">
        
	<div class="content-agileits">
		<h1 class="title">Online Sales</h1>
		<div class="left">
			<form action="#" method="post" data-toggle="validator">
				<div class="form-group">
				 <asp:TextBox ID="txtdate" runat="server" class="form-control"></asp:TextBox>	
				</div>
				<div class="form-group">
					 <asp:TextBox ID="txtdate1" runat="server" class="form-control"></asp:TextBox>	
				</div>
				<div class="form-group">
					<asp:DropDownList ID="cmboutlet" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmboutlet_SelectedIndexChanged">
                        <asp:ListItem>Select Outlet</asp:ListItem>
                        <asp:ListItem>BBSR-NAYAPALLI</asp:ListItem>
                        <asp:ListItem>BBSR-BARAMUNDA</asp:ListItem>
                        <asp:ListItem>BBSR-SUM HOSPITAL</asp:ListItem>
                        <asp:ListItem>BBSR-VIVEKANANDA MARG</asp:ListItem>
                        <asp:ListItem>BBSR-BOMIKHAL</asp:ListItem>
                        <asp:ListItem>BBSR-SAILASHREE VIHAR</asp:ListItem>
                        <asp:ListItem>BBSR-PATIA</asp:ListItem>
                        <asp:ListItem>BBSR-SAHEED NAGAR</asp:ListItem>
                        <asp:ListItem>BBSR-JAGAMARA</asp:ListItem>
                        <asp:ListItem>BBSR-UNIT4</asp:ListItem>
                        <asp:ListItem>BBSR-JANPATH</asp:ListItem>
                        <asp:ListItem>BBSR-ZESTATE</asp:ListItem>
                        <asp:ListItem>BBSR-JAYDEV VIHAR</asp:ListItem>
                        <asp:ListItem>BBSR-JHARPADA</asp:ListItem>
                        <asp:ListItem>BBSR-LEWIS ROAD</asp:ListItem>
                        <asp:ListItem>BALESWAR-STATION ROAD</asp:ListItem>
                        <asp:ListItem>BERHAMPUR-SIBANI TOWER</asp:ListItem>
                        <asp:ListItem>BERHAMPUR-GANDHINAGAR</asp:ListItem>
                        <asp:ListItem>BERHAMPUR-ASKA ROAD</asp:ListItem>
                        <asp:ListItem>PURI-VIP ROAD</asp:ListItem>
                        <asp:ListItem>CTC-CDA</asp:ListItem>
                        <asp:ListItem>CTC-NAYASARAK</asp:ListItem>
                        <asp:ListItem>CTC-COLLEGESQUARE</asp:ListItem>
                        <asp:ListItem>CTC-SEC6 CDA</asp:ListItem>
                        <asp:ListItem>JAJPUR-SURYANSH</asp:ListItem>
                        <asp:ListItem>KEONJHAR-KASHIVI MALL</asp:ListItem>
                        <asp:ListItem>KEONJHAR-KASHIVI MALL</asp:ListItem>
                        <asp:ListItem>ROURKELA-UDITNAGAR</asp:ListItem>
                        <asp:ListItem>ROURKELA-CIVIL TOWNSHIP</asp:ListItem>
                        <asp:ListItem>BHADRAK-TOWN THANA</asp:ListItem>
                        <asp:ListItem>BARGARH-BHATLI SQUARE</asp:ListItem>
                        <asp:ListItem>SAMBALPUR-CHURCH</asp:ListItem>
                        <asp:ListItem>CTC-KANIKA CHHAK</asp:ListItem>
                        <asp:ListItem>KOL-HOWRAH</asp:ListItem>
                        <asp:ListItem>KOL-DUMDUM</asp:ListItem>
                        <asp:ListItem>KOL-VIPROAD</asp:ListItem>
                        <asp:ListItem>KOL-SHIBPUR</asp:ListItem>
                        <asp:ListItem>KOL-BUNGUR</asp:ListItem>
                        <asp:ListItem>KOL-JESSORE ROAD</asp:ListItem>
                        <asp:ListItem>KOL-BHOWANIPORE</asp:ListItem>
                        <asp:ListItem>KOL-SALT LAKE</asp:ListItem>


					</asp:DropDownList>
				</div>
                <div class="form-group">
				
                    <asp:Button ID="Button2" runat="server" class="btn btn-lg" Text="Search" OnClick="Button2_Click"/>
				</div>
				<div class="form-group">
				                    <asp:DropDownList ID="cmbzone" class="form-control" runat="server" OnSelectedIndexChanged="cmbzone_SelectedIndexChanged">
                                        <asp:ListItem>Select Prod Zone</asp:ListItem>
                                        <asp:ListItem>BBSR</asp:ListItem>
                                        <asp:ListItem>KOL</asp:ListItem>
                                    </asp:DropDownList>
				</div>
				
				<div class="form-group">
				
                    <asp:Button ID="Button1" runat="server" class="btn btn-lg" Text="Search" OnClick="Button1_Click" />
				</div>

                <div class="form-group">
					 <asp:Button ID="Button3" runat="server" class="btn btn-lg" Text="Get Plaza Sales" OnClick="Button3_Click"/>
				</div>
                <div class="form-group">
				
				</div>
                <div class="form-group">
                    <asp:GridView ID="dv" runat="server" Width="100%"  CssClass="Grid"                    

                      AlternatingRowStyle-CssClass="alt" OnRowDataBound="dv_RowDataBound"></asp:GridView>
				</div>
			</form>
		</div>
		<div class="right"></div>
		<div class="clear"></div>
	</div>
	<p class="copyright-w3ls">© 2017. All Rights Reserved 
	</p>
	<!-- js -->
	<script src="js/jquery-2.1.4.min.js"></script>
	<!-- //js -->

	<script src="js/bootstrap.min.js"></script>
	<script src="js/validator.min.js"></script>
    </form>
</body>
</html>
