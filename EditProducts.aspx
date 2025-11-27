<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="EditProducts.aspx.cs" Inherits="EditProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var File_Name = ""
        $(document).ready(function () {
            $('.weight').keypress(function (event) {
                if (((event.which != 46 || (event.which == 46 && $(this).val() == '')) ||
                    $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            }).on('paste', function (event) {
                event.preventDefault();
            });
            $('.active').keypress(function (event) {
                if (event.which != 48 && event.which != 49) {
                    event.preventDefault();
                }
            }).on('paste', function (event) {
                event.preventDefault();
            });
        });

        function readURL(input, id) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#' + id)
                        .attr('src', e.target.result)
                        .width(300)
                        .height(300);
                };

                reader.readAsDataURL(input.files[0]);
            }
        }
        $(document).on('click', "#uploadbtn", function (e) {
            $(this).val("");
        });
        $(document).on('change', '#uploadbtn', function (e) {
            saveImage("update", File_Name)
        });
        $(document).on('click', "#addbtn", function () {
            $("#addbtn").val("");
        });
        $(document).on('change', "#addbtn", function () {
            File_Name = "";
            File_Name = $("#ContentPlaceHolder1_ProductTitle").text();
            saveImage("add", File_Name)
        });
        function callImport(name) {
            File_Name = "";
            File_Name = name;
            document.querySelector('#uploadbtn').click()
        }
        function saveImage(action, File_Name) {
            var img_data = new FormData();


            if (action == "update") {
                var uploadedImage = document.getElementById("uploadbtn").files[0];
                img_data.append('Image_file', uploadedImage);
                img_data.append('File_Name', File_Name);
            }
            else {
                var uploadedImage = document.getElementById("addbtn").files[0];
                img_data.append('Image_file', uploadedImage);
                img_data.append('File_Name', File_Name);
            }
            img_data.append('action', action);
            $.ajax({
                url: 'Admin_File_Handler.ashx',
                type: 'POST',
                data: img_data,
                cache: false,
                contentType: false,
                processData: false,
                success: function (response) {
                    alert(response.Message);
                    location.reload(true);
                },
                error: function (response) {
                    alert(response.Message);
                }
            });
        };

        function DeleteImage(File_Name) {
            if (confirm('Are you sure you want to delete the image ?')) {
                var img_data = new FormData();
                img_data.append('File_Name', File_Name);
                img_data.append('action', "Delete");
                $.ajax({
                    url: 'Admin_File_Handler.ashx',
                    type: 'POST',
                    data: img_data,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        alert(response.Message);
                        location.reload(true);
                    },
                    error: function (response) {
                        alert(response.Message);
                    }
                });
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="midpart" class="container" style="background-color: white">
        <div class="col-md-12 EditProductTitle" id="ProductTitle" runat="server"></div>

        <%--<div class="col-md-12">
            <div class="col-md-6 form-group">
                <div>BIG IMAGE</div>
                <div style="display: flex; justify-content: center;">
                    <div style="border: 2px solid; padding: 10px; margin: 10px">
                        <img id="BigImg" src="menupic/big/481b.png" alt="your image" height="300" width="300" />
                    </div>
                </div>
                <input type="file" id="BigImgUploadBtn" accept="image/jpeg, image/png" onchange="readURL(this, 'BigImg')" class="ExportBtn col-sm-12 form-control" />
            </div>
            <div class="col-md-6 form-group">
                <div>SMALL IMAGE</div>
                <div style="display: flex; justify-content: center;">
                    <div style="border: 2px solid; padding: 10px; margin: 10px">
                        <img id="SmallImg" src="menupic/big/481b.png" alt="your image" height="300" width="300" />
                    </div>
                </div>
                <input type="file" id="SmallImgUploadBtn" accept="image/jpeg, image/png" onchange="readURL(this, 'SmallImg')" class="ExportBtn col-sm-12 form-control" />
            </div>--%>

        <%--<div class="col-sm-2 form-group">
                <asp:Button CssClass="ExportBtn col-sm-12 form-control" ID="Button1" runat="server" Text="Upload" OnClick="ImportBtn_Clicked" />
            </div>
            <div class="col-sm-2 form-group">
                <asp:Button CssClass="ExportBtn col-sm-12 form-control" ID="Button2" runat="server" Text="Export" OnClick="ExportBtn_Clicked" />
            </div>--%>

        <%--</div>--%>

        <div class="col-md-12">
            <div class="col-md-4">
                <div class="row">
                    <div class="form-group col-md-4">
                        ID
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="ProductId" type="text" class="form-control" readonly />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Menu Name
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="MenuName" type="text" class="form-control" readonly/>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Barcode
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="Barcode" type="text" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Group
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="Group" type="text" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Sub Group
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="SubGroup" type="text" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Feature 1
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="Feature1" type="text" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Feature 2
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="Feature2" type="text" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Feature 3
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="Feature3" type="text" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Feature 4
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="Feature4" type="text" class="form-control" />
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="row">
                    <div class="form-group col-md-4">
                        Cost Price
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="CostPrice" type="text" class="form-control weight" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Sell Price
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="SellPrice" type="text" class="form-control weight" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Tax
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="Tax" type="text" class="form-control weight" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Stock
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="Stock" type="text" class="form-control weight" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Discount Type
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="DiscountType" type="text" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Discount Value
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="DiscountValue" type="text" class="form-control weight" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Min Weight
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="MinWeight" type="text" class="form-control weight" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Flavour
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="Flavour" type="text" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        Active
                    </div>
                    <div class="form-group col-md-8">
                        <input runat="server" id="Active" type="text" class="form-control active" maxlength="1" />
                    </div>
                </div>
            </div>
            
            <div class ="col-md-4">
            <div class="product-listing-container row col-md-12 form-group div-image-container" id="xfdata" runat="server">
            </div>
        </div>

        </div>
        
        <div class="col-md-12">
            <div class="col-md-2 form-group">
                <asp:Button CssClass="ExportBtn col-sm-12 form-control" ID="ImportBtn" runat="server" Text="Save" CausesValidation="false" OnClick="SaveBtn_Clicked" />
            </div>
            <div class="col-md-2 form-group">
                <asp:Button CssClass="ExportBtn col-sm-12 form-control" ID="ExportBtn" runat="server" Text="Close" OnClick="CloseBtn_Clicked" />
            </div>
            <div class="col-md-2 form-group">
                <asp:Button CssClass="ExportBtn col-sm-12 form-control" ID="ExportImg" runat="server" Text="Export Image" OnClick="Exportbtn_Clicked"  />
            </div>
            <div class="col-md-2 form-group">
                <input type="file" id="addbtn" accept=".jpg" style="display:none" />
                <button type="button" Class ="ExportBtn col-sm-12 form-control" ID="AddImagbtn" OnClick ="document.querySelector('#addbtn').click()" >Add Image</button>
            </div>
            
        </div>       
        </div>
</asp:Content>
