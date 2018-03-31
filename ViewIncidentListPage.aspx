<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="ViewIncidentListPage.aspx.cs" Inherits="ViewIncidentListPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <asp:Label ID="Welcome" runat="server" Text="Welcome Back," ForeColor="black"></asp:Label>
    <asp:Label ID="StaffName" runat="server" Text="" ForeColor="black"></asp:Label> |
    <asp:Label ID="StaffRole" runat="server" Text="" ForeColor="black"></asp:Label> |
    <asp:Label ID="StaffEmail" runat="server" Text="" ForeColor="black" style="margin-right:-58%;"></asp:Label>
    <asp:Label ID="lbStaffId" runat="server" Text="" Enabled="True" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:SqlDataSource ID="IncidentListDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Connection911 %>" SelectCommand="SELECT [IncidentID], [NatureOfEmergency], [NameOfEmergency], [CallerName], [CallBackNo], [LocationOfEmergency], [TimeOfEmergency], [IncidentCreatedTime], [IncidentCreatedDate], [StaffName], [AlertedCMOName], [Status], [PotentialCrisis] FROM [Incidents]"></asp:SqlDataSource>
    <h2>View Incident List</h2>
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IncidentID" DataSourceID="IncidentListDataSource" ForeColor="Black" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" PageSize="20">
        <Columns>
            <asp:HyperLinkField Text="View Details" DataNavigateUrlFields="IncidentId" DataNavigateUrlFormatString="IncidentDetailsPage.aspx?IncidentId={0}" ItemStyle-Width="10%" />
            
            <asp:BoundField DataField="IncidentId" HeaderText="Incident ID " SortExpression="IncidentId" ItemStyle-Width="10%" >
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="NatureOfEmergency" HeaderText="Nature Of Emergency " SortExpression="NatureOfEmergency" ItemStyle-Width="10%" >
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="CallerName" HeaderText="Caller Name " SortExpression="CallerName" ItemStyle-Width="10%">
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="CallBackNo" HeaderText="Call Back Number " SortExpression="CallBackNo" ItemStyle-Width="10%">
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="LocationOfEmergency" HeaderText="Location Of Emergency " SortExpression="LocationOfEmergency" ItemStyle-Width="10%">
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="IncidentCreatedDate" HeaderText="Date Of Emergency" SortExpression="IncidentCreatedDate" ItemStyle-Width="10%" DataFormatString="{0:d}" />
            <asp:BoundField DataField="TimeOfEmergency" HeaderText="Time Of Emergency " SortExpression="TimeOfEmergency" ItemStyle-Width="10%">
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="StaffName" HeaderText="Staff Name" SortExpression="StaffName" ItemStyle-Width="10%">
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"  ItemStyle-Width="10%">
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:CheckBoxField DataField="PotentialCrisis" HeaderText="Potential Crisis" SortExpression="PotentialCrisis" ItemStyle-Width="10%">
<ItemStyle Width="10%"></ItemStyle>
            </asp:CheckBoxField>
        </Columns>
        <EditRowStyle Font-Size="Small" />
        <FooterStyle BackColor="#CCCCCC" Font-Size="Small" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Font-Size="Small" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" Font-Size="Small" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" Font-Size="Small" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" Font-Size="Small" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>

</asp:Content>

