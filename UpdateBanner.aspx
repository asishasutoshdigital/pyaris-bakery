<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="UpdateBanner.aspx.cs" Inherits="UpdateBanner" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <script type="text/javascript">
        window.onload = function () {
            var msg = document.getElementById('<%= litMessage.ClientID %>');
               if (msg && msg.innerText.trim() !== "") {
                   setTimeout(function () {
                       msg.style.display = 'none';
                   }, 10000);
               }
        };
    
        function toggleEdit(editBtnId, deleteBtnId, cancelBtnId, sectionId, activeBtnId) {
            document.getElementById(sectionId).style.display = 'block'; // Show the edit section
            document.getElementById(editBtnId).style.display = 'none';   // Hide the Edit button
            document.getElementById(deleteBtnId).style.display = 'none'; // Hide the Delete button
            document.getElementById(cancelBtnId).style.display = 'inline-block'; // Show the "Cancel" button
            document.getElementById(activeBtnId).style.display = 'none'; // Hide the Activate/Inactivate button
            return false;
        }

        function cancelEdit(editBtnId, deleteBtnId, cancelBtnId, sectionId, activeBtnId) {
            document.getElementById(sectionId).style.display = 'none';   // Hide the edit section
            document.getElementById(editBtnId).style.display = 'inline-block'; // Show the Edit button
            document.getElementById(deleteBtnId).style.display = 'inline-block'; // Show the Delete button
            document.getElementById(activeBtnId).style.display = 'inline-block'; // Show the Activate/Inactivate button
            document.getElementById(cancelBtnId).style.display = 'none'; // Hide the Cancel button
            return false;
        }

        function showAddBannerFields(e) {
            e.preventDefault();
            var showBanner = e.target;
            document.getElementById("addBannerField").style.display = "block";
            showBanner.style.display = "none";
        }

        function showAddButton(e) {
            e.preventDefault();
            document.getElementById("addBannerField").style.display = "none";
            document.getElementById('<%= btnShowAddBanner.ClientID %>').style.display = "inline-block";
     }

 </script>

    <div class="container py-5">
        <h2 class="mb-4 text-center">📢 Manage Banners</h2>
        <asp:Label ID="litMessage" runat="server" CssClass="message-label" EnableViewState="false"></asp:Label>

        <div>
            <div class="card mb-5">
                <div class="card-header bg-dark text-white ">
                    <h5 class="mb-0 ">✨ Add New Banner</h5>
                </div>
                <div class="text-center mb-3" style="padding:10px 10px ">
                    <asp:Button ID="btnShowAddBanner" runat="server" Text="+ Add New Banner" CssClass="btn btn-primary" OnClientClick="showAddBannerFields(event); return false;" />
                </div>
                <div id="addBannerField" style="display:none;" class="card-body">
                    <div class="row g-3 mb-4">
                        <div class="col-md-6">
                            <label for="<%= fuNewBannerImage.ClientID %>" class="form-label">Upload New Image</label>
                            <asp:FileUpload ID="fuNewBannerImage" runat="server" CssClass="form-control" />
                            <div class="form-text">Allowed file format:JPG,PNG,GIF and Max size: 2MB.</div>
                        </div>

                        <div class="col-md-6">
                            <label for="<%= ddlNewBannerGroup.ClientID %>" class="form-label">Assign to Group</label>
                            <div>
                                <asp:DropDownList ID="ddlNewBannerGroup" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="text-center" style="margin-top:10px">
                        <asp:Button ID="btnAddNewBanner" runat="server" Text="Upload" CssClass="btn btn-success px-5" OnClick="btnAddNewBanner_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClientClick="showAddButton(event); return false;" />
                    </div>
                </div>
            </div>
        </div>
        <div class="card shadow-lg" style="margin-top:35px;">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0">🎨 Current Banners</h5>
            </div>
            <div class="card-body">
                <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-4">
                    <asp:Repeater ID="rptCurrentBanners" runat="server" OnItemDataBound="rptCurrentBanners_ItemData" OnItemCommand="rptCurrentBanners_ItemCommand">
                        <ItemTemplate>
                            <div class="col">
                                <div class="card banner-card h-100">
                                    <h3 class="text-bold text-center">
                                        <%# System.IO.Path.GetFileNameWithoutExtension(Eval("FileName").ToString().ToUpper()) %>
                                    </h3>
                                    <p style="margin-bottom:10px; font-size: 17px;
                                        background-color: <%# ((bool)Eval("ActiveStatus")) ? "#28a745" : "#dc3545" %>; color: #fff;" class="badgestyle rounded-pill">
                                        <%# ((bool)Eval("ActiveStatus")) ? "Status: Active" : "Status: Inactive" %>
                                    </p>

                                    <img src='<%# "images/slider/" + Eval("FileName") %>' class="card-img-top img-fluid banner-preview" />
                                    <div class="card-body d-flex flex-column">
                                        <p class="text-bold">Group: <%# Eval("Group") %></p>
                                        <div class="text-center">
                                            <asp:Button ID="btnEditBanner" runat="server" Text="Edit" CssClass="btn btn-primary btn-sm mb-2"
                                                OnClientClick='<%# "return toggleEdit(\"" + ((Button)Container.FindControl("btnEditBanner")).ClientID + "\", \"" + ((Button)Container.FindControl("btnDeleteSingleBanner")).ClientID + "\", \"" + ((Button)Container.FindControl("btnCancelEdit")).ClientID + "\", \"edit-section-" + Container.ItemIndex + "\", \"" + ((Button)Container.FindControl("chkIsActive")).ClientID + "\");" %>' />

                                            <asp:Button ID="btnDeleteSingleBanner" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="DeleteBanner" CommandArgument='<%# Eval("FileName") %>' OnClientClick="return confirm('Are you sure you want to delete this banner?');" />

                                            <asp:Button
                                                ID="chkIsActive"
                                                runat="server"
                                                Text='<%# ((bool)Eval("ActiveStatus")) ? "Activate" : "Inactivate" %>'
                                                CssClass='<%# ((bool)Eval("ActiveStatus") ) ? "btn btn-success" : "btn btn-warning" %>'
                                                CommandName="ActiveStatus"
                                                CommandArgument='<%# Eval("FileName") %>'
                                            />
                                        </div>
                                        <hr class="solid">
                                        <div id='<%# "edit-section-" + Container.ItemIndex %>' style="display:none;" class="mt-3">
                                            <div class="col-md-4">
                                                <div>
                                                    <label class="form-label">Assign Group</label>
                                                </div>
                                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-select"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-8">
                                                <label class="form-label">Replace Image</label>
                                                <asp:FileUpload ID="fuBannerImage" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-check text-center" style="margin-top:20px">
                                                <div>
                                                    <asp:Button ID="btnUpdateSingleBanner" runat="server" Text="Upload" style="margin-top:8px" CssClass="btn btn-success btn-sm w-100 mt-2" CommandName="UpdateBanner" CommandArgument='<%# Eval("FileName") %>' />
                                                    <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" CssClass="btn btn-warning btn-sm w-100 mt-2"
                                                        OnClientClick='<%# "return cancelEdit(\"" + ((Button)Container.FindControl("btnEditBanner")).ClientID + "\", \"" + ((Button)Container.FindControl("btnDeleteSingleBanner")).ClientID + "\", \"" + ((Button)Container.FindControl("btnCancelEdit")).ClientID + "\", \"edit-section-" + Container.ItemIndex + "\", \"" + ((Button)Container.FindControl("chkIsActive")).ClientID + "\");" %>' Style="margin-top:8px;" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                    </div>
                    </FooterTemplate>
                    </asp:Repeater>
                    <asp:Label ID="lblNoBanners" runat="server" Text="No banners found in the slider directory." CssClass="alert alert-info d-block mt-3 mx-3" Visible="false"></asp:Label>
                </div>
            </div>
        </div>

<style>
    body {
        background-color: #f8f9fa; 
        font-family: 'Segoe UI', Roboto, Arial, sans-serif; 
        color: #343a40;
    }

    .container {
        max-width: 1200px; 
        padding-top: 3.5rem; 
        padding-bottom: 3.5rem;
    }

 
    h2.text-primary {
        color: #2c3e50 !important;
        font-size: 2.5rem; 
        margin-bottom: 1rem !important; 
    }

    p.lead {
        font-size: 1.15rem;
        color: #6c757d;
        margin-bottom: 3rem !important; 
    }


    .card {
        border-radius: 12px; 
        border: none; 
        box-shadow: 0 6px 20px rgba(0, 0, 0, 0.08); 
        transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
    }

    .card-header {
        background-color: #e9ecef;
        color: #343a40; 
        padding: 1.25rem 1.75rem; 
        border-bottom: 1px solid rgba(0, 0, 0, 0.08);
        font-weight: 600;
        font-size: 1.15rem;
        display: flex;
        align-items: center;
        gap: 0.75rem;
    }

    .card-header.bg-success {
        background-color: #28a745 !important; 
        color: white;
    }

    .card-header.bg-dark {
        background-color: #343a40 !important;
        color: white;
    }

    .card-body {
        padding: 2rem;
    }


    .form-label {
        font-weight: 600;
        margin-bottom: 0.5rem; 
        color: #495057; 
    }

    .form-control,
    .form-select {
        border-radius: 0.5rem;
        border: 1px solid #ced4da;
        padding: 0.75rem 1rem;
        font-size: 1rem; 
    }

    .form-control-sm,
    .form-select-sm {
        padding: 0.5rem 0.75rem; 
        font-size: 0.9rem; 
    }

    .form-text {
        font-size: 0.875rem;
        color: #6c757d;
        margin-top: 0.25rem;
    }

    .btn {
        font-weight: 1000; 
        border: none; 
        border-radius: 0.5rem;
        padding: 0.75rem 1.5rem; 
        transition: all 0.2s ease-in-out; 
    }

    .btn-primary.btn-lg {
        background: linear-gradient(to right, #007bff, #0056b3); 
        box-shadow: 0 4px 12px rgba(0, 123, 255, 0.25); 
    }

    .btn-primary.btn-lg:hover {
        background: linear-gradient(to right, #0056b3, #004085);
        transform: translateY(-2px);
        box-shadow: 0 6px 16px rgba(0, 123, 255, 0.35);
    }

    .btn-primary.btn-sm {
        background: #007bff;
        box-shadow: 0 2px 6px rgba(0, 123, 255, 0.15);
    }

    .btn-primary.btn-sm:hover {
        background: #0056b3;
        transform: translateY(-1px);
        box-shadow: 0 3px 8px rgba(0, 123, 255, 0.25);
    }

    .btn-danger.btn-sm {
        background: #dc3545;
        box-shadow: 0 2px 6px rgba(220, 53, 69, 0.15);
    }

    .btn-danger.btn-sm:hover {
        background: #c82333;
        transform: translateY(-1px);
        box-shadow: 0 3px 8px rgba(220, 53, 69, 0.25);
    }

    .banner-item-card {
        border-radius: 10px;
        box-shadow: 0 3px 10px rgba(0, 0, 0, 0.08);
        transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
        cursor: pointer;
    }

    .banner-item-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 8px 25px rgba(0, 0, 0, 0.15); 
    }

    .banner-preview {
        height: 180px;
        object-fit: cover;
        width: 100%;
        border-top-left-radius: 10px; 
        border-top-right-radius: 10px;
    }

    .card-body.d-flex.flex-column.p-3 {
        padding: 1.25rem !important;
    }

    .card-title.fw-bold {
        font-size: 1rem;
        margin-bottom: 0.75rem !important;
        color: #343a40;
    }

    .card-footer {
        background-color: #fefefe !important;
        border-top: 1px solid rgba(0, 0, 0, 0.05); 
        padding: 1rem 1.25rem;
    }

    .toast-container {
        bottom: 2rem !important;
        right: 2rem !important;
        z-index: 1080; 
    }

    .toast {
        border-radius: 8px;
        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        border: none;
    }

    .toast-header {
        background-color: transparent;
        border-bottom: none;
        font-weight: 600;
        color: #343a40;
        padding-bottom: 0.5rem;
    }

    .toast-body {
        padding-top: 0.5rem; 
        padding-bottom: 0.75rem;
        font-size: 0.95rem;
        line-height: 1.4;
    }

    .badgestyle {
    display: inline-block;
    min-width: 10px;
    padding: 3px 7px;
    font-size: 12px;
    font-weight: 700;
    line-height: 1;
    text-align: center;
    white-space: nowrap;
    vertical-align: middle;
    border-radius: 10px;
}
</style>
</asp:Content>