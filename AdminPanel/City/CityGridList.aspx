<%@ Page Title="" Language="C#" MasterPageFile="~/AllList/Content/MasterPage.master" AutoEventWireup="true" CodeFile="CityGridList.aspx.cs" Inherits="AllList_City_CityGridList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <script src="../../Content/JavaScript.js"></script>
    <script>
        function alert() {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                }
            })
        }
    </script>

    <div>
        <br />
        <h1>City  &nbsp; List</h1>
        <div class="row" style="padding-top: 10px">
            <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
        </div>
        <div class="text-right col-md-10">
            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnAdd_Click" />
        </div>
        <br />
        <br />


        <asp:GridView ID="gvCity" runat="server" CssClass="table table-bordered table-striped" OnRowCommand="gvCity_RowCommand" AutoGenerateColumns="false">
            <AlternatingRowStyle BackColor="#F7F7F7" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <SortedAscendingCellStyle BackColor="#F4F4FD" />
            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
            <SortedDescendingCellStyle BackColor="#D8D8F0" />
            <SortedDescendingHeaderStyle BackColor="#3E3277" />

            <Columns>
                <asp:BoundField DataField="CityID" HeaderText="ID" />
                <asp:BoundField DataField="CityName" HeaderText="Name" />
                <asp:BoundField DataField="Pincode" HeaderText="Pincode" />
                <asp:BoundField DataField="STDCode" HeaderText="STD Code" />
                <asp:BoundField DataField="StateName" HeaderText="State Name" />
                <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                <asp:TemplateField ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" CommandName="DeleteID" CommandArgument='<%# Eval("CityID") %>' Text="Delete" CssClass="btn btn-danger" />&nbsp;&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-info" Text="Edit" NavigateUrl='<%# "~/AllList/AdminPanel/City/CityAddEdit.aspx?CityID=" +Eval("CityID").ToString().Trim() %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>

