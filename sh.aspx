<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sh.aspx.cs" Inherits="sh" %>
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
                     
                     </a>
                  </div>
               </nav>
            </div>
         </div>
      </header>

       <div id="loader" runat="server">
         <div class="loader__balls">
           <div class="loader__balls__group">
             <div class="ball item1"></div>
             <div class="ball item1"></div>
             <div class="ball item1"></div>
           </div>
           <div class="loader__balls__group">
             <div class="ball item2"></div>
             <div class="ball item2"></div>
             <div class="ball item2"></div>
           </div>
           <div class="loader__balls__group">
             <div class="ball item3"></div>
             <div class="ball item3"></div>
             <div class="ball item3"></div>
           </div>
         </div>
       </div>

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
                  <div id="cardtext"  runat="server"  class="card-heading"> </div>
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
/* Loading css */
#loader {
    display: flex;                  
    justify-content: center;        
    align-items: center;            
    height: 100vh;                 
    width: 100vw;                  
    position: fixed;                
    top: 0;                        
    left: 0;                       
    background-color: rgba(255, 255, 255, 0.8); 
    z-index: 9999;               
}
  .loader__balls {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  gap: 5px;
}
.loader__balls__group {
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  align-items: center;
  position: relative;
  height: 50px;
  width: 30px;
}
.ball {
  height: 20px;
  width: 20px;
  border-radius: 15px;
  position: absolute;
  transform-origin: bottom;
}
 
/* ANIMATION BALL 1*/
.loader__balls__group :nth-child(1) {
  background-color: #99e2d0;
  animation-name: jumpinBallAnimation1;
  animation-duration: 1000ms;
  animation-iteration-count: infinite;
}
@keyframes jumpinBallAnimation1 {
  0% {
    transform: translateY(0) scale(1, 1);
  }
  10% {
    transform: translateY(0) scale(1.3, 0.8);
  }
  11% {
    transform: translateY(0) scale(0.7, 1.2);
    animation-timing-function: cubic-bezier(0, 0, 0.5, 1);
  }
  39% {
    transform: translateY(-75px) scale(1);
    animation-timing-function: cubic-bezier(0, 0, 0.5, 1);
  }
  40% {
    transform: translateY(-75px) scale(1);
  }
  41% {
    transform: translateY(-75px) scale(1);
    animation-timing-function: cubic-bezier(1, 0, 1, 0);
  }
  69% {
    transform: translateY(0px) scale(1, 1);
    animation-timing-function: cubic-bezier(1, 0, 1, 0);
  }
  70% {
    transform: translateY(0) scale(1.5, 0.4);
  }
  80% {
    transform: translateY(0) scale(0.8, 1.2);
  }
  90% {
    transform: translateY(0) scale(1.1, 0.8);
  }
  100% {
    transform: translateY(0) scale(1, 1);
  }
}
 
/* ANIMATION BALL 2*/
.loader__balls__group :nth-child(2) {
  background-color: #ff79da;
  animation-name: jumpinBallAnimation2;
  animation-duration: 1000ms;
  animation-iteration-count: infinite;
}
@keyframes jumpinBallAnimation2 {
  0% {
    transform: translateY(0) scale(1, 1);
  }
  10% {
    transform: translateY(0) scale(1.3, 0.8);
  }
  11% {
    transform: translateY(0) scale(0.7, 1.2);
    animation-timing-function: cubic-bezier(0, 0.5, 0.5, 1);
  }
  39% {
    transform: translateY(-75px) scale(1);
    animation-timing-function: cubic-bezier(0, 0.5, 0.5, 1);
  }
  40% {
    transform: translateY(-75px) scale(1);
  }
  41% {
    transform: translateY(-75px) scale(1);
    animation-timing-function: cubic-bezier(1, 0, 1, 0.5);
  }
  69% {
    transform: translateY(0px) scale(1, 1);
    animation-timing-function: cubic-bezier(1, 0, 1, 0.5);
  }
  70% {
    transform: translateY(0) scale(1.5, 0.4);
  }
  80% {
    transform: translateY(0) scale(0.8, 1.2);
  }
  90% {
    transform: translateY(0) scale(1.1, 0.8);
  }
  100% {
    transform: translateY(0) scale(1, 1);
  }
}
 
/* ANIMATION BALL 3*/
.loader__balls__group :nth-child(3) {
  background-color: #9b7ad5;
  animation-name: jumpinBallAnimation3;
  animation-duration: 1000ms;
  animation-iteration-count: infinite;
}
@keyframes jumpinBallAnimation3 {
  0% {
    transform: translateY(0) scale(1, 1);
  }
  10% {
    transform: translateY(0) scale(1.3, 0.8);
  }
  11% {
    transform: translateY(0) scale(0.7, 1.2);
    animation-timing-function: cubic-bezier(0, 1, 0.5, 1);
  }
  39% {
    transform: translateY(-75px) scale(1);
    animation-timing-function: cubic-bezier(0, 1, 0.5, 1);
  }
  40% {
    transform: translateY(-75px) scale(1);
  }
  41% {
    transform: translateY(-75px) scale(1);
    animation-timing-function: cubic-bezier(1, 0, 1, 1);
  }
  69% {
    transform: translateY(0px) scale(1, 1);
    animation-timing-function: cubic-bezier(1, 0, 1, 1);
  }
  70% {
    transform: translateY(0) scale(1.5, 0.4);
  }
  80% {
    transform: translateY(0) scale(0.8, 1.2);
  }
  90% {
    transform: translateY(0) scale(1.1, 0.8);
  }
  100% {
    transform: translateY(0) scale(1, 1);
  }
}
.loader__balls__group .item1 {
  animation-delay: 0ms;
}
.loader__balls__group .item2 {
  animation-delay: 100ms;
}
.loader__balls__group .item3 {
  animation-delay: 200ms;
}
</style>