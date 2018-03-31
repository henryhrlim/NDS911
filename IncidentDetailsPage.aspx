<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="IncidentDetailsPage.aspx.cs" Inherits="IncidentDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <style type="text/css">
       /*styling of the textboxes*/
    .txtbox {
    border-top-left-radius: 3px;
    border-top-right-radius: 3px;
    border-bottom-left-radius: 3px;
    border-bottom-right-radius: 3px;
    border-style: double;
    border-color: #CCCCCC;
    }
   </style>
   

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="Welcome" runat="server" Text="Welcome Back," ForeColor="black"></asp:Label>
    <asp:Label ID="StaffName" runat="server" Text="" ForeColor="black"></asp:Label> |
    <asp:Label ID="StaffRole" runat="server" Text="" ForeColor="black"></asp:Label> |
    <asp:Label ID="StaffEmail" runat="server" Text="" ForeColor="black" style="margin-right:-58%;"></asp:Label>
    <asp:Label ID="lbStaffId" runat="server" Text="" Enabled="True" Visible="False"></asp:Label>
    <asp:Label ID="lbIncidentId" runat="server" Text="" Enabled="True" Visible="False"></asp:Label>
    <br />
    <br />
        
 
    <h2>Incident Details Page</h2>
     
      <table id="calldetails" align="center"> 
          <tr>
          <td>Call Operator Name</td>
          <td><asp:UpdatePanel ID="UpdatePanel6" runat="server">
              <ContentTemplate>
              <asp:TextBox ID="tbCallOperator" runat="server" Width="80%" >
                 
               </asp:TextBox>
              </ContentTemplate>
              </asp:UpdatePanel>
          </td>
          </tr>

          <tr>
          <td>Name of Caller</td>
          <td><asp:TextBox ID="tbcallername" runat="server" Width="80%" cssclass="txtbox" validationGroup="login"></asp:TextBox>
          </td>
          </tr>

          <tr>
          <td>NRIC/FIN No. of Caller</td>
          <td><asp:TextBox ID="tbcallernric" runat="server" Width="80%" cssclass="txtbox"></asp:TextBox>
              
          </td>
          </tr>

          <tr>
          <td>Callback Number</td>
          <td><asp:TextBox ID="tbcallbackno" runat="server" Width="80%" cssclass="txtbox"></asp:TextBox>
          </td>
          </tr>

          <tr>
          <td>Nature Of Emergency</td>
          <td><asp:UpdatePanel ID="UpdatePanel2" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="ddlNature" runat="server" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="ddlNature_SelectedIndexChanged1">
              <asp:ListItem>Select a nature</asp:ListItem>
              <asp:ListItem>National</asp:ListItem>
              <asp:ListItem>Individual</asp:ListItem>
              <asp:ListItem>Natural Disaster</asp:ListItem>
              <asp:ListItem>Virus Outbreak</asp:ListItem>
              <asp:ListItem>Supernatural</asp:ListItem>
              </asp:DropDownList>
              </ContentTemplate>
              </asp:UpdatePanel>

          </td>
          </tr>


          <tr>
          <td>Name Of Emergency</td>
          <td>
              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="ddlNameOfEmergency" runat="server" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="ddlNameOfEmergency_SelectedIndexChanged1">
              <asp:ListItem>Select a nature first</asp:ListItem>
              </asp:DropDownList>
              
              
              <asp:Label ID="Zombie" runat="server" Text="" Visible="False" Width="39%" Font-Size="Small" Enabled="False"></asp:Label>
              <asp:DropDownList ID="ddlZombieType" runat="server" Width="19%" AutoPostBack="true" Visible="False" Enabled="False">
                  <asp:ListItem>Land</asp:ListItem>
                  <asp:ListItem>Flying</asp:ListItem>
                  <asp:ListItem>Sea</asp:ListItem>
               </asp:DropDownList>
               <br />
               <asp:Label ID="NoOfZombies" runat="server" Text="" Visible="False" Width="38%" Font-Size="Small"></asp:Label>
                  <asp:TextBox ID="tbNoOfZombies" runat="server" Visible="false" Width="20%" cssclass="txtbox" Enabled="False"></asp:TextBox>

                  </ContentTemplate>
              </asp:UpdatePanel>
          </td>
          </tr>

          <tr>
          <td>Time of Emergency</td>
          <td><asp:TextBox ID="tbemergencytime" runat="server" Width="80%" cssclass="txtbox"></asp:TextBox></td>
          </tr>

          <tr>
          <td>Location of Emergency</td>
          <td><asp:TextBox ID="tbemergencylocation" runat="server" Width="80%" cssclass="txtbox"></asp:TextBox>
             
             
          </td>
          </tr>

          <tr>
          <td>Estimated Number of Casualties(s)</td>
          <td><asp:TextBox ID="tbnoofcasualties" runat="server" Width="80%" cssclass="txtbox"></asp:TextBox></td>
          </tr>

          <tr>
          <td>Estimated Number of Death(s)</td>
          <td><asp:TextBox ID="tbnoofdeaths" runat="server" Width="80%" cssclass="txtbox"></asp:TextBox></td>
          </tr>

          <tr>
          <td>Description of Emergency</td>
          <td><asp:TextBox ID="descriptionofemergency" runat="server" Width="80%" TextMode="MultiLine" Columns="50" Rows="6" cssclass="txtbox"></asp:TextBox></td>
          </tr>

          <tr>
          <td>Potential Crisis*</td>
          <td><asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="ddlPotentialCrisis" runat="server" Width="80%" AutoPostBack="true">
                  <asp:ListItem>Select one</asp:ListItem>
                  <asp:ListItem>Yes</asp:ListItem>
                  <asp:ListItem>No</asp:ListItem>
               </asp:DropDownList>
              </ContentTemplate>
              </asp:UpdatePanel>

          </td>
          </tr>

          <tr>
          <td>Alerted CMO Name</td>
          <td>
              <asp:UpdatePanel ID="UpdatePanel4" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="ddlCMOName" runat="server" Width="80%" AutoPostBack="true">
                 <asp:ListItem>Select one</asp:ListItem>
               </asp:DropDownList>
              </ContentTemplate>
              </asp:UpdatePanel>

          </td>
          </tr>

           <tr>
          <td>Status Of Emergency</td>
          <td><asp:UpdatePanel ID="UpdatePanel5" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="ddlStatus" runat="server" Width="80%" AutoPostBack="true">
                  <asp:ListItem>Select one</asp:ListItem>
                  <asp:ListItem>New</asp:ListItem>
                  <asp:ListItem>Notified CMO</asp:ListItem>
                  <asp:ListItem>Closed</asp:ListItem>
               </asp:DropDownList>
              </ContentTemplate>
              </asp:UpdatePanel>

          </td>
          </tr>

          <tr>
          <td>Last Updated</td>
          <td><asp:TextBox ID="tbLastUpdated" runat="server" Width="80%" cssclass="txtbox"></asp:TextBox></td>
          </tr>

          <tr>
          <td>Updated By</td>
          <td><asp:TextBox ID="tbUpdatedBy" runat="server" Width="80%" cssclass="txtbox"></asp:TextBox></td>
          </tr>

           <tr>
              <td colspan="2">
                  <asp:Button ID="Edit" runat="server" Text="Enable Edit" cssclass="txtbox"  Width="80%" ForeColor="Black" OnClick="Edit_Click" />
                  <asp:Button ID="Cancel" runat="server" Text="Cancel" cssclass="txtbox"  Width="40%" ForeColor="Black" Visible="false" Enabled="false" OnClick="Cancel_Click" />
                  <asp:Button ID="Update" runat="server" Text="Update" cssclass="txtbox" Width="40%" ForeColor="Black" OnClick="Update_Click" Visible="false" Enabled="false"/>
                  
          </tr>
          


      </table>
</asp:Content>

