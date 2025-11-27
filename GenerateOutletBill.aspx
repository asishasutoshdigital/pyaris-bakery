<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateOutletBill.aspx.cs" Inherits="GenerateOutletBill" %>
<!DOCTYPE html>
<html lang="en">
   <head runat="server">
      <link href="Content/GlobalContent?v=1" rel="stylesheet"/>
   </head>
   <body>
      <!-- Main Header -->
      <header class="header-main container-fluid no-padding">
         <div class="menu-block container-fluid no-padding">
            <div class="container-fluid navbarContainer">
               <nav class="navbar ow-navigation">
                  <div class="navbar-header">
                     <a class="navbar-brand">
                     <img src="images/logo-navbar.png" alt="Paris Bakery" title="Paris Bakery" />
                     </a>
                  </div>
               </nav>
            </div>
         </div>
      </header>
      <!-- Main Header -->
      <!-- Mobile Header -->
      <div class="mobile-header">
         <div class="mobile-header-content">
            <div class="mobile-icon">
               <div class="mobile-content-section">
                  <div>
                     <a>
                        <div class="mobile-logo-container">
                           <img src="images/logo-navbar.png" alt="Paris Bakery" />
                        </div>
                     </a>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <!-- Mobile Header -->
      <!-- Main Body -->
      <form id="form1" runat="server">
         <div id="mainBody">
            <div id="midpart" class="container">
               <div class="row spark-over-container">
                  <div id="cardText"  runat="server"  class="card-heading"> </div>
               </div>
            </div>
         </div>
      </form>
   </body>
</html>
<style>
   .card-heading {
   color: #2c263a;
   font-family: Georgia, serif;
   font-size: 35px;
   font-weight: 400;
   margin: 40px 0px;
   }
</style>