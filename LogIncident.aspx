<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="LogIncident.aspx.cs" Inherits="LogIncident" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <head runat="server">
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
   
    </head>

    

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="Welcome" runat="server" Text="Welcome Back," ForeColor="black"></asp:Label>
    <asp:Label ID="StaffName" runat="server" Text="" ForeColor="black"></asp:Label> |
    <asp:Label ID="StaffRole" runat="server" Text="" ForeColor="black"></asp:Label> |
    <asp:Label ID="StaffEmail" runat="server" Text="" ForeColor="black" style="margin-right:-58%;"></asp:Label>
    <asp:Label ID="lbStaffId" runat="server" Text="" Enabled="True" Visible="False"></asp:Label>
    <br />
    <br />
       <asp:TextBox ID="tbIncidentNumber" runat="server" Width="80%" Visible="false"></asp:TextBox> 
 
    <h2>New Incident Log</h2>
     <p>Call Operators are required to submit an emergency with the required fields.</p>
      <table id="calldetails" align="center"> 
          <tr>
          <td>Name of Caller*</td>
          <td><asp:TextBox ID="tbcallername" runat="server" Width="80%" cssclass="txtbox" Text="" validationGroup="login"></asp:TextBox>
              <br/>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="tbcallername" ErrorMessage="Please enter a valid name !" 
                Font-Italic="True" Font-Size="Small" ForeColor="Red" 
                ValidationExpression="^[a-zA-Z'-'.,\s]{1,30}$" Display="Dynamic"></asp:RegularExpressionValidator>
             
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="tbcallername" ErrorMessage="This field is required !" 
                Font-Italic="True" Font-Size="Small" ForeColor="Red" 
                ValidationGroup="login" Display="Dynamic"></asp:RequiredFieldValidator>
              
          </td>
          </tr>

          <tr>
          <td>NRIC/FIN No. of Caller*</td>
          <td><asp:TextBox ID="tbcallernric" runat="server" Text="" Width="80%" cssclass="txtbox"></asp:TextBox>
              <br />
              <asp:RegularExpressionValidator ID="RegularExpressionValidator11" 
                runat="server" ControlToValidate="tbcallernric" 
                ErrorMessage="Please enter a valid ic !" Font-Italic="True" Font-Size="Small" 
                ForeColor="Red" ValidationExpression="^[a-zA-Z]{1}[0-9]{7,12}[a-zA-Z]{1}$" Display="Dynamic"></asp:RegularExpressionValidator>

             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                ControlToValidate="tbcallernric" ErrorMessage="This field is required !" 
                Font-Italic="True" Font-Size="Small" ForeColor="Red" 
                ValidationGroup="login" Display="Dynamic"></asp:RequiredFieldValidator>
          </td>
          </tr>

          <tr>
          <td>Callback Number*</td>
          <td><asp:TextBox ID="tbcallbackno" runat="server" Text="" Width="80%" cssclass="txtbox"></asp:TextBox>
              <br />
              
               <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                ControlToValidate="tbcallbackno" 
                ErrorMessage="Please enter a valid mobile number !" Font-Italic="True" 
                Font-Size="Small" ForeColor="Red" ValidationExpression="^^[0-9]{8}" Display="Dynamic"></asp:RegularExpressionValidator>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="tbcallbackno" ErrorMessage="This field is required !" 
                Font-Italic="True" Font-Size="Small" ForeColor="Red" 
                ValidationGroup="login" Display="Dynamic"></asp:RequiredFieldValidator>

          </td>
          </tr>

          <tr>
          <td>Nature Of Emergency*</td>
          <td><asp:UpdatePanel ID="UpdatePanel2" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="ddlNature" runat="server" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="Nature_SelectedIndexChanged">
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
          <td>Name Of Emergency*</td>
          <td>
              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="ddlNameOfEmergency" runat="server" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="ddlNameOfEmergency_SelectedIndexChanged">
              <asp:ListItem>Select a nature first</asp:ListItem>
              </asp:DropDownList>
              
              
              <asp:Label ID="Zombie" runat="server" Text="" Visible="False" Width="39%" Font-Size="Small" Enabled="False"></asp:Label>
              <asp:DropDownList ID="ddlZombieType" runat="server" Width="19%" AutoPostBack="true" Visible="False" Enabled="False">
                  <asp:ListItem>Select One</asp:ListItem>
                  <asp:ListItem>Land</asp:ListItem>
                  <asp:ListItem>Flying</asp:ListItem>
                  <asp:ListItem>Sea</asp:ListItem>
               </asp:DropDownList>
               <br />
               <asp:Label ID="NoOfZombies" runat="server" Text="" Visible="False" Width="38%" Font-Size="Small" Enabled="False"></asp:Label>
                  <asp:TextBox ID="tbNoOfZombies" runat="server" Visible="false" Width="20%" cssclass="txtbox" Enabled="False"></asp:TextBox>

                  </ContentTemplate>
              </asp:UpdatePanel>
          </td>
          </tr>

          <tr>
          <td>Time of Emergency*</td>
          <td><asp:TextBox ID="tbemergencytime" runat="server" Text="" Width="80%" cssclass="txtbox" TextMode="Time"></asp:TextBox>
               
          </td>
          </tr>

          <tr>
          <td>Location of Emergency*</td>
          <td><asp:TextBox ID="tbemergencylocation" runat="server" Text="" Width="80%" cssclass="txtbox"></asp:TextBox>
             
             
          </td>
          </tr>

          <tr>
          <td>Estimated Number of Casualties(s)</td>
          <td><asp:TextBox ID="tbnoofcasualties" runat="server" Text="" Width="80%" cssclass="txtbox"></asp:TextBox></td>
          </tr>

          <tr>
          <td>Estimated Number of Death(s)</td>
          <td><asp:TextBox ID="tbnoofdeaths" runat="server" Text="" Width="80%" cssclass="txtbox"></asp:TextBox></td>
          </tr>

          <tr>
          <td>Description of Emergency*</td>
          <td><asp:TextBox ID="descriptionofemergency" runat="server" Text="" Width="80%" TextMode="MultiLine" Columns="50" Rows="6" cssclass="txtbox"></asp:TextBox></td>
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
          <td>Alerted CMO Name*</td>
          <td><asp:UpdatePanel ID="UpdatePanel4" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="ddlCMOName" runat="server" Width="80%" AutoPostBack="true">
                 <asp:ListItem>Select one</asp:ListItem>
               </asp:DropDownList>
              </ContentTemplate>
              </asp:UpdatePanel>

          </td>
          </tr>

           <tr>
          <td>Status Of Emergency*</td>
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
              <td colspan="2">
                  <asp:Button ID="Submit" runat="server" Text="Log Emergency" cssclass="txtbox" Width="80%" ForeColor="Black" onclick="CreateNew_Click" validationGroup="login"/>
                 
                  <asp:Button ID="Notify" runat="server" Text="Notify CMO" cssclass="txtbox" Width="80%" ForeColor="Black" OnClick="NotifyCMO_Click" Visible ="false"/>

                   <asp:Button ID="LogNewIncident" runat="server" Text="Log Another New Incident" cssclass="txtbox" Width="80%" ForeColor="Black" Visible ="false" OnClick="LogNewIncident_Click"/>
              </td>
          </tr>


      </table>
</asp:Content>

