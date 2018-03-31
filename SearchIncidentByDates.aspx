﻿<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="SearchIncidentByDates.aspx.cs" Inherits="SearchIncident" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <head runat="server">

    </head>
    <!-- for the sake of using calender extender, i need to add this -->
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Label ID="Welcome" runat="server" Text="Welcome Back," ForeColor="black"></asp:Label>
    <asp:Label ID="StaffName" runat="server" Text="" ForeColor="black"></asp:Label> |
    <asp:Label ID="StaffRole" runat="server" Text="" ForeColor="black"></asp:Label> |
    <asp:Label ID="StaffEmail" runat="server" Text="" ForeColor="black" style="margin-right:-58%;"></asp:Label>
    <asp:Label ID="lbStaffId" runat="server" Text="" Enabled="True" Visible="False"></asp:Label>
    <br />
    <br />
    
    <h2>Search Incident</h2>
    <br />
    <table id="logintable" align="center">
        <tr>
            <td>CMO Officer Name:
              
            </td>
            <td> <asp:UpdatePanel ID="UpdatePanel4" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="ddlCMOName" runat="server" Width="80%" AutoPostBack="true">
                 <asp:ListItem>Select one</asp:ListItem>
               </asp:DropDownList>
              </ContentTemplate>
              </asp:UpdatePanel></td>
            
        </tr>

        <tr>
            <td>Call Operator Name: 
                               
            </td>

            <td> <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="ddlOperator" runat="server" Width="80%" AutoPostBack="true">
                 <asp:ListItem>Select one</asp:ListItem>
               </asp:DropDownList>
              </ContentTemplate>
              </asp:UpdatePanel></td>
            
        </tr>

        <tr>
            <td>Start Date:                    
            </td>

            <td> <asp:TextBox ID="tbStartDate" runat="server" Width="80%"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="tbStartDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>

            </td>
            
        </tr>

        <tr>
            <td>End Date:   
            </td>

            <td>
                 <asp:TextBox ID="tbEndDate" runat="server" Width="80%"></asp:TextBox>
                 <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="tbEndDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
            </td>
            
        </tr>

        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               </td>

            <td> 
                 <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80%" OnClick="btnSearch_Click"/>
            </td>
        </tr>
       
    </table>
    <br /><br />
    
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
              <ContentTemplate>
     <asp:GridView ID="GVSearchResults" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" AllowPaging="True" PageSize="20" OnPageIndexChanging="grdData_PageIndexChanging" OnSorting="SortRecords" AllowSorting="True">
         <Columns>
             <asp:HyperLinkField Text="View Details" DataNavigateUrlFields="IncidentID" DataNavigateUrlFormatString="IncidentDetailsPage.aspx?IncidentId={0}" ItemStyle-Width="10%" />
             <asp:BoundField DataField="IncidentID" HeaderText="Incident Id" SortExpression="IncidentID" ItemStyle-Width="10%" />
             <asp:BoundField DataField="NatureOfEmergency" HeaderText="Nature Of Emergency" SortExpression="NatureOfEmergency" ItemStyle-Width="10%" />
             <asp:BoundField DataField="CallerName" HeaderText="Caller Name" SortExpression="CallerName" ItemStyle-Width="10%" />
             <asp:BoundField DataField="CallBackNo" HeaderText="Call Back Number" SortExpression="CallBackNo" ItemStyle-Width="10%" />
             <asp:BoundField DataField="LocationOfEmergency" HeaderText="Location Of Emergency" SortExpression="LocationOfEmergency" ItemStyle-Width="10%" />
             <asp:BoundField DataField="IncidentCreatedDate" HeaderText="Date Of Emergency" SortExpression="IncidentCreatedDate" ItemStyle-Width="10%" DataFormatString="{0:d}" />
             <asp:BoundField DataField="TimeOfEmergency" HeaderText="Time Of Emergency" SortExpression="TimeOfEmergency" ItemStyle-Width="10%" />
             <asp:BoundField DataField="StaffName" HeaderText="Staff Name" SortExpression="StaffName" ItemStyle-Width="10%" />
             <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ItemStyle-Width="10%" />
             <asp:CheckBoxField DataField="PotentialCrisis" HeaderText="Potential Crisis" SortExpression="PotentialCrisis" ItemStyle-Width="10%">
            </asp:CheckBoxField>
         </Columns>
         <FooterStyle BackColor="#CCCCCC" Font-Size="Small" />
         <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Font-Size="Small" />
         <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" Font-Size="Small" />
         <RowStyle BackColor="White" Font-Size="Small" />
         <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" Font-Size="Small" />
         <SortedAscendingCellStyle BackColor="#F1F1F1" Font-Size="Small" />
         <SortedAscendingHeaderStyle BackColor="#808080" Font-Size="Small" />
         <SortedDescendingCellStyle BackColor="#CAC9C9" />
         <SortedDescendingHeaderStyle BackColor="#383838" Font-Size="Small" />
    </asp:GridView>
  
    </ContentTemplate>
              </asp:UpdatePanel>

    
</asp:Content>

