<%@ Page Title="" Language="C#" MasterPageFile="~/AllList/Content/MasterPage.master" AutoEventWireup="true" CodeFile="BloodGroupAddEdit.aspx.cs" Inherits="AllList_AdminPanel_BloodGroup_BloodGroupAddEdit" %>

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
            <asp:Label ID="lblTitle" runat="server" Text="" />
        </h1>
    </div>

    <div class="row" style="padding-top: 10px">
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
    </div>

    <div class="row text-left" style="padding-top: 60px; padding-left: 250px;">
        <div class="col-md-3 font-weight-bold">
            Blood &nbsp; Group &nbsp; Name
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtBloodGroupName" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvBloodGroupName" runat="server" ErrorMessage="Enter Blood Group Name" ControlToValidate="txtBloodGroupName" CssClass="alert-danger" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="row text-center" style="padding-top: 50px; padding-left: 440px;">
        <div class="col-md-2 font-weight-bold">
            <asp:Button ID="btnSave" runat="server" Text=" Save " CssClass="btn btn-info" EnableViewState="false" OnClick="btnSave_Click" ValidationGroup="Save" />
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnCancel" runat="server" Text="  Cancel  " CssClass="btn btn-danger" OnClick="btnCancel_Click" />
        </div>
    </div>
</asp:Content>

