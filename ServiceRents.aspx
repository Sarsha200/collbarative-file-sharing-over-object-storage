<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceRents.aspx.cs" Inherits="ServiceRents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Rents</title>
    <link rel="stylesheet" href="css/CloudStyles.css" />
</head>
<body>
   <%Server.Execute("Header.aspx"); %>
    <%Server.Execute("Navigate.aspx"); %> 
    <form id="form1" runat="server" class="form-horizontal col-sm-12 text-center">  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="form-group text-center">       
        <center>
        <br /><h3 class="page-title">Cloud Service Rents</h3><br />
        
            
                    
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>   

        <table align="center" cellpadding="5" cellspacing="8" class="form-group">      
            
            <tr>
                <td>Select Service</td>
                <td>
                    <asp:DropDownList ID="cmbservice" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="GetCharges">
                        <asp:ListItem Value="<----- Select ----->"><----- Select -----></asp:ListItem>                            
                        <asp:ListItem Value="upload">Documents Upload</asp:ListItem>
                        <asp:ListItem Value="downlod">Documents Download</asp:ListItem>                        
                        <asp:ListItem Value="encryption">Encryption</asp:ListItem>
                        <asp:ListItem Value="decryption">Decryption</asp:ListItem>
                        <asp:ListItem Value="space">Space per MB</asp:ListItem>
                        <asp:ListItem Value="permission">Permission</asp:ListItem>
                        <asp:ListItem Value="recovery">File Recovery</asp:ListItem>
                        <asp:ListItem Value="askkey">Generate Key</asp:ListItem>
                        <asp:ListItem Value="login">Logins</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*" ForeColor="#CC0000" Font-Size="Large" 
                    Type="String" ControlToValidate="cmbservice" ValueToCompare="<----- Select ----->" Operator="NotEqual"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>Service Charge</td>
                <td><asp:TextBox ID="txtcharge" runat="server" CssClass="form-control"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtcharge" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" 
                    TargetControlID="txtcharge" FilterType="Numbers ,custom" ValidChars="."  /> 
                </td>
            </tr>
            </table>
                    </ContentTemplate>
            </asp:UpdatePanel>
            <table align="center" cellpadding="5" cellspacing="8" class="form-group">  
            <tr>
                <td colspan="3">
                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-info" OnClick="Submit"/>                    
                </td>                
            </tr>
       </table>
                    
        </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
