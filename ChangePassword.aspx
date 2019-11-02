<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <%Server.Execute("Header.aspx"); %>
    <%Server.Execute("Navigate.aspx"); %> 
    <form id="form1" runat="server" class="form-horizontal col-sm-12 text-center">  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="form-group text-center">       
        <center>
        <br /><h3 class="page-title">Change Pasword</h3><br />
       

        <table align="center" cellpadding="5" cellspacing="8" class="form-group"> 
            <tr>
            <td>UserID</td>
            <td>
                <asp:TextBox ID="usid" runat="server" CssClass="form-control"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="*" ControlToValidate="usid" Font-Size="Large" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </td>
            </tr>
    
            <tr>
            <td>Current Password</td>
            <td>
                <asp:TextBox ID="opass" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="*" ControlToValidate="opass" Font-Size="Large" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </td>
            </tr>
    
            <tr>
            <td>New Password</td>
            <td>
                <asp:TextBox ID="npass" runat="server" TextMode="Password" CssClass="form-control" ></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ErrorMessage="*" ControlToValidate="npass" Font-Size="Large" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>                   
            </td>
            </tr>
            <tr>
                <td colspan="3"><asp:Label ID="TextBox2_HelpLabel" runat="server" />
                    <asp:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="npass"
                    DisplayPosition="RightSide" 
                    StrengthIndicatorType="BarIndicator"
                    PreferredPasswordLength="8"
                    HelpStatusLabelID="TextBox2_HelpLabel"            
                    StrengthStyles="BarIndicator_TextBox2_weak;BarIndicator_TextBox2_average;BarIndicator_TextBox2_good"            
                    BarBorderCssClass="BarBorder_TextBox2"
                    MinimumNumericCharacters="1"
                    MinimumSymbolCharacters="1"
                    TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
                    RequiresUpperAndLowerCaseCharacters="true" /></td>
            </tr>
            <tr>
            <td>Retype Password</td>
            <td>
                <asp:TextBox ID="rpass" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="*" ControlToValidate="rpass" Font-Size="Large" 
                ForeColor="#CC0000"></asp:RequiredFieldValidator>
       
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="Passwords mismatch" ControlToCompare="npass" 
                ControlToValidate="rpass" ForeColor="#CC0000"></asp:CompareValidator></td></tr>    
            <tr>
            <td colspan="3">
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Change" CssClass="btn btn-info" />       
            </td>
            </tr>   
        </table>

        </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
