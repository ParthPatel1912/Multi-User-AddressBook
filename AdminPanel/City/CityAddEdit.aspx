<%@ Page Title="" Language="C#" MasterPageFile="~/AllList/Content/MasterPage.master" AutoEventWireup="true" CodeFile="CityAddEdit.aspx.cs" Inherits="AllList_City_CityAddList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

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

    <div class="row" style="padding-top: 20px">
            <asp:Label ID="lblError" runat="server" CssClass="text-danger" EnableViewState="false"/>
        </div>

    <div style="padding-left: 250px; padding-top:20px;">

        <div class="row text-left" style="">
            <div class="col-md-2 font-weight-bold">
                City Name
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="txtCityName" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvCityName" runat="server" ErrorMessage="Enter City Name" ControlToValidate="txtCityName" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="Save" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row text-left" style="padding-top: 10px;">
            <div class="col-md-2 font-weight-bold">
                Pincode
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="txtPincode" CssClass="form-control" TextMode="Number"/>
                <asp:RequiredFieldValidator ID="rfvPincode" runat="server" ErrorMessage="Enter Pincode" ControlToValidate="txtPincode" CssClass="alert-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revPincode" runat="server" ErrorMessage="Enter 6 digit Pincode" ControlToValidate="txtPincode" CssClass="alert-danger" Display="Dynamic" ForeColor="Red" ValidationExpression="[0-9]{6}" ValidationGroup="Save" SetFocusOnError="true"></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="row text-left" style="padding-top: 10px;">
            <div class="col-md-2 font-weight-bold">
                STD Code
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="txtSTDCode" CssClass="form-control" TextMode="Number"/>
                <asp:RequiredFieldValidator ID="rfvSTDCode" runat="server" ErrorMessage="Enter city STD code" ControlToValidate="txtSTDCode" CssClass="alert-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row text-left" style="padding-top: 10px;">
            <div class="col-md-2 font-weight-bold">
                State Name
            </div>
            <div class="col-md-4 text-center">
                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control text-center"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvSateName" runat="server" ErrorMessage="Select State" ControlToValidate="ddlState" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="Save" SetFocusOnError="true" InitialValue="-1"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row text-center" style="padding-top: 50px;padding-left:125px;">
            <div class="col-md-2 font-weight-bold">
                <asp:Button ID="btnSave" runat="server" Text="  Save  " CssClass="btn btn-info" OnClick="btnSave_Click" EnableViewState="false" ValidationGroup="Save" SetFocusOnError="true" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnCancel" runat="server" Text="  Cancel  " CssClass="btn btn-danger" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>

</asp:Content>

