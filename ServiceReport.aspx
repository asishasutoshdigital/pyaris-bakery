<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="ServiceReport.aspx.cs" Inherits="ServiceReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
        <div class="col-md-12 form-group ExportContainer" runat="server">
            <h3>Total messages to process : <span id="NewCount" runat="server">0</span></h3> 
        </div>
        <div class="col-md-12" id="ServiceReportContainer" runat="server" clientidmode="Static">
        </div>
    </div>
</asp:Content>
