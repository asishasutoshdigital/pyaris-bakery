<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stock.aspx.cs" Inherits="ONLINE_sales" %>

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
	<link href="/../fonts.googleapis.com/css?family=Source+Sans+Pro:200,200i,300,300i,400,400i,600,600i,700,700i,900,900i" rel="stylesheet">
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

    <script type="text/javascript">
        $(function () {
            $("[id$=txtSearch]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/stock.aspx/GetCustomers") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=hfCustomerId]").val(i.item.val);
            },
            minLength: 1
        });
    });
</script>
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
					<asp:DropDownList ID="cmboutlet" runat="server" class="form-control" AutoPostBack="True" >


					</asp:DropDownList>
				</div>
                <div class="form-group">
				<asp:TextBox ID="txtmenu" runat="server" class="form-control"></asp:TextBox>	
                    
				</div>
				<div class="form-group">
                    <asp:Button ID="Button2" runat="server" class="btn btn-lg" Text="Search" OnClick="Button2_Click"/>
				</div>
				
				<div class="form-group">
				<asp:HiddenField ID="hfCustomerId" runat="server" />
				</div>

                <div class="form-group">
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
	p>
	<!-- js -->
	<script src="js/jquery-2.1.4.min.js"></script>
	<!-- //js -->

	<script src="js/bootstrap.min.js"></script>
	<script src="js/validator.min.js"></script>
    </form>
</body>
</html>
