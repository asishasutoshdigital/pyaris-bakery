<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart">
   
        <div class="col-sm-12 ExportContainer" runat="server">
            <div class="col-sm-4 form-group">
                
                <asp:FileUpload CssClass="ExportBtn col-sm-12 form-control" ID="FileUploadBtn" runat="server" />
            </div>
            <div class="col-sm-2 form-group">
                <asp:Button CssClass="ExportBtn col-sm-12 form-control" ID="ImportBtn" runat="server" Text="Upload" OnClick="ImportBtn_Clicked" />
            </div>
            <div class="col-sm-2 form-group">
                <asp:Button CssClass="ExportBtn col-sm-12 form-control" ID="ExportBtn" runat="server" Text="Export" OnClick="ExportBtn_Clicked" />
            </div>
        </div>


        <div class="col-md-12" id="AllProducts" runat="server">
        </div>
    </div>

</asp:Content>