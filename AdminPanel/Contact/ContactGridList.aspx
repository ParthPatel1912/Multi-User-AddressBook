<%@ Page Title="" Language="C#" MasterPageFile="~/AllList/Content/MasterPage.master" AutoEventWireup="true" CodeFile="ContactGridList.aspx.cs" Inherits="AllList_AdminPanel_Contact_ContactGridList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

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
        <h1>Contact &nbsp;   List</h1>
        <div class="row" style="padding-top: 10px">
            <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
        </div>
        <div class="text-right col-md-10">
            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnAdd_Click" />
        </div>
        <br />
        <br />
        <asp:GridView ID="gvContact" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" OnRowCommand="gvState_RowCommand">
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
                <asp:TemplateField ItemStyle-CssClass="text-center">
                     <ItemTemplate>
                        <asp:Image ID="photo" runat="server" ImageUrl='<%# Eval("PhotoPath") %>' Height="50" BackColor="RosyBrown" BorderColor="#ff00ff"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="ContactName" HeaderText="Name"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="Address" HeaderText="Address"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="Pincode" HeaderText="Pincode"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="CityName" HeaderText="City Name"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="StateName" HeaderText="State Name"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="CountryName" HeaderText="Country Name"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="MobileNo" HeaderText="Mobile"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="PhoneNo" HeaderText="Phone"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="Email" HeaderText="Email"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="Gender" HeaderText="Gender"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                 <asp:BoundField DataField="Age" HeaderText="Age"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="Profession" HeaderText="Profession"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="BloodgroupName" HeaderText="BloodgroupName"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />
                <asp:BoundField DataField="ContactcategoryName" HeaderText="ContactcategoryName"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Italic="true" />

                <asp:TemplateField ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="DeleteID" CommandArgument='<%# Eval("ContactID") %>' />&nbsp;&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:HyperLink ID="hledit" runat="server" Text="Edit" CssClass="btn btn-info" NavigateUrl='<%# "~/AllList/AdminPanel/Contact/ContactAddEdit.aspx?ContactID=" + Eval("ContactId").ToString().Trim() %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </div>

</asp:Content>

