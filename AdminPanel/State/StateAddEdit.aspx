<%@ Page Title="" Language="C#" MasterPageFile="~/AllList/Content/MasterPage.master" AutoEventWireup="true" CodeFile="StateAddEdit.aspx.cs" Inherits="AllList_AdminPanel_State_StateAddEdit" %>

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
            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
        </h1>
    </div>

    <div class="row" style="padding-top: 10px">
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
    </div>

    <div class="row text-left" style="padding-top: 60px; padding-left: 250px;">
        <div class="col-md-2 font-weight-bold">
            State Name
        </div>
        <div class="col-md-4 text-center">
            <asp:TextBox runat="server" ID="txtStateName" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvStateName" runat="server" ErrorMessage="Enter State Name" ControlToValidate="txtStateName" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="Save" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="row text-left" style="padding-top: 15px; padding-left: 250px;">
        <div class="col-md-2 font-weight-bold">
            Country Name
        </div>
        <div class="col-md-4 text-center text-center">
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control text-center"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCountyName" runat="server" ErrorMessage="Select Country" ControlToValidate="ddlCountry" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="Save" SetFocusOnError="true" InitialValue="-1"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="row text-center" style="padding-top: 50px; padding-left: 350px;">
        <div class="col-md-3 font-weight-bold">
            <asp:Button ID="btnSave" runat="server" Text="   Save   " CssClass="btn btn-info" OnClick="btnSave_Click" EnableViewState="false" ValidationGroup="Save" SetFocusOnError="true" />
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnCancel" runat="server" Text="    Cancel    " CssClass="btn btn-danger" OnClick="btnCancel_Click" />
        </div>
    </div>

</asp:Content>

