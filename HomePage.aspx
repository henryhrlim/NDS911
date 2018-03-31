<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Login.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <head>
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

    .btnstyle{
    border-top-left-radius: 3px;
    border-top-right-radius: 3px;
    border-bottom-left-radius: 3px;
    border-bottom-right-radius: 3px;
    }
   </style>
   
    </head>
  
    <br />
     <h2>OUR VISION-MISSION</h2>
    Vision: <br />
    "As Singapore's Emergency Response Unit, we will assist to maintain peace and 
    harmony in the country and continue to strive to provide a better place for everyone".
    <br /><br />

    Mission: <br />
    "It is our mission to provide excellent emergency responses to the civilans of Singapore. 
    We will always be prepared for any kind of emergency to assist those in need by providing
    quality assistance in Medical, Rescue, Fire, Police and K-9 Services."
    <br /><br />

    <h2>ABOUT US</h2>
    The three-digit telephone number "9-1-1" has been designated as the "Universal Emergency Number," 
    for citizens throughout the Singapore to request emergency assistance. 
    It is intended as a nationwide telephone number and gives the public fast and easy access 
    to a Public Safety Answering Point (PSAP).
    

</asp:Content>

<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <h2>LOGIN</h2>
    <br />
    <table id="logintable" align="center">
        <tr>
            <td>Staff Id: &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="staffId" runat="server" Width="65%" class="txtbox"></asp:TextBox></td>
            

        <tr>
            <td style="width: 313px">Password: <asp:TextBox ID="password" runat="server" Width="65%" TextMode="Password" class="txtbox"></asp:TextBox></td>
        </tr>

        <tr>
            <td style="width: 313px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="login" runat="server" Text="Login" Width="65%" OnClick="login_Click" CssClass="txtbox" /></td>
        </tr>
    </table>
    </asp:Content>

