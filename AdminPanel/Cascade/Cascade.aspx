<%@ Page Title="" Language="C#" MasterPageFile="~/AllList/Content/MasterPage.master" AutoEventWireup="true" CodeFile="Cascade.aspx.cs" Inherits="AllList_AdminPanel_Cascade_Cascade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <div class="row text-left" style="padding-top: 50px; padding-left: 250px;">
        <div class="col-md-2 font-weight-bold">
            Country Name
        </div>
        <div class="col-md-4 text-center text-center">
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control text-center" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCountyName" runat="server" ErrorMessage="Select Country" ControlToValidate="ddlCountry" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="Save" InitialValue="-1"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="row text-left" style="padding-top: 15px; padding-left: 250px;">
        <div class="col-md-2 font-weight-bold">
            State Name
        </div>
        <div class="col-md-4 text-center text-center">
            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control text-center" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="Select State" ControlToValidate="ddlCountry" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="Save" InitialValue="-1"></asp:RequiredFieldValidator>
        </div>
    </div>

     <div class="row text-left" style="padding-top: 20px; padding-left: 250px;">
        <div class="col-md-2 font-weight-bold">
            City
        </div>
        <div class="col-md-4 text-center">
            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control text-center" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCityName" runat="server" ErrorMessage="Select City" ControlToValidate="ddlCity" CssClass="alert-danger" ForeColor="Red" Display="Dynamic" ValidationGroup="Save" InitialValue="-1"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="row text-right" style="padding-top: 20px; padding-left: 350px;">
        <div class="col-md-4 text-center">
            <asp:Button ID="btnPrint" runat="server" Text="Answer" CssClass="btn btn-success" OnClick="btnPrint_Click" />
        </div>
    </div>

    <div class="row text-right" style="padding-top: 20px;">
        <div class="col-md-12 text-center">
            <asp:Label ID="lblAnswer" runat="server" EnableViewState="false"></asp:Label>
        </div>
    </div>

</asp:Content>

