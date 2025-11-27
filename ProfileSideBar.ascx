<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProfileSideBar.ascx.cs" Inherits="ProfileSideBar" %>

<div class="onlinesidemenu col-md-3">
    <div class="menuCardTitle">OPERATIONS</div>
    <div class="menuCardSeperator"></div>
    <div id='flyout_menu'>
        <ul>
            <%--<li><a href='online.aspx'><span>VIEW ONLINE MENU</span></a>
            </li>--%>
            <li><a href='mya.aspx'><span>MY ORDERS</span></a>

            </li>
            <li id="payBackLink" runat="server"><a href='myapayback.aspx'><span>MY PAYBACK POINTS</span></a>

            </li>
            <li><a href='myprofile.aspx'><span>MY PROFILE</span></a>
            </li>
            <li><a href='mya.aspx?logout=yes'><span>LOGOUT</span></a>
            </li>
        </ul>
    </div>
</div>
