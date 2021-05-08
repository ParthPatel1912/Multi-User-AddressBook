<%@ Page Title="" Language="C#" MasterPageFile="~/AllList/Content/MasterPage.master" AutoEventWireup="true" CodeFile="CountryAddEdit.aspx.cs" Inherits="AllList_AdminPanel_Country_CountryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <script src="../../Content/JavaScript.js"></script>
    <script>
        function save() {
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Your work has been saved',
                showConfirmButton: false,
                timer: 1500
            })
        }
    </script>

    <br />
    <div class="row">
        <h1>
            <asp:Label ID="lblTitle" runat="server" Text="Country &nbsp; List &nbsp; Add &nbsp; and &nbsp; Edit" />
        </h1>
    </div>

    <div class="row" style="padding-top: 10px">
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
    </div>

    <div class="row text-left" style="padding-top: 60px; padding-left: 250px;">
        <div class="col-md-3 font-weight-bold">
            Country &nbsp; Name
        </div>
        <div class="col-md-4 text-center">
            <asp:TextBox runat="server" ID="txtCountryName" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvCountryName" runat="server" ErrorMessage="Enter Your City Name" ControlToValidate="txtCountryName" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="Save" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="row text-left" style="padding-top: 10px; padding-left: 250px;">
        <div class="col-md-3 font-weight-bold">
            Country &nbsp; Code
        </div>
        <div class="col-md-4 text-center">
            <asp:TextBox runat="server" ID="txtCountryCode" CssClass="form-control" TextMode="Number"/>
            <asp:RequiredFieldValidator ID="rfvCountryCode" runat="server" ErrorMessage="Enter City Code" ControlToValidate="txtCountryCode" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="Save" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="row text-center" style="padding-top: 50px; padding-left: 435px;">
        <div class="col-md-3 font-weight-bold">
            <asp:Button ID="btnSave" runat="server" Text="    Save    " CssClass="btn btn-info" OnClick="btnSave_Click" EnableViewState="false" ValidationGroup="Save" SetFocusOnError="true" />
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnCancel" runat="server" Text="    Cancel    " CssClass="btn btn-danger" OnClick="btnCancel_Click" />
        </div>
    </div>

</asp:Content>

