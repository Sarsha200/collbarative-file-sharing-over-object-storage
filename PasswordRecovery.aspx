<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="PasswordRecovery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <%Server.Execute("Header.aspx"); %>
    <%Server.Execute("Navigate.aspx"); %> 
    <form id="form1" runat="server" class="form-horizontal col-sm-12 text-center">        
    <div class="form-group text-center">       
        <center>
        <br /><h3 class="page-title">Pasword Recovery</h3><br />
        

        <table align="center" cellpadding="5" cellspacing="8" class="form-group">   
        <tr>
        <td>UserID</td>
        <td>
            <asp:TextBox ID="txtuserid" runat="server" CssClass="form-control"></asp:TextBox>
            </td><td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="*" ControlToValidate="txtuserid" Font-Size="Large" 
                ForeColor="#CC0000"></asp:RequiredFieldValidator>
        </td>
        </tr>                
        <tr>
            <td>Security Question</td>
            <td>
                <asp:DropDownList ID="cmbquestion" runat="server" CssClass="form-control">
                    <asp:ListItem Value="<----- Select ----->"><----- Select -----></asp:ListItem>                            
                    <asp:ListItem>What is your pet name?</asp:ListItem>
                    <asp:ListItem>What is your favorite  teachers name?</asp:ListItem>
                    <asp:ListItem>What is your hobby?</asp:ListItem>
                    <asp:ListItem>What is your favorite color?</asp:ListItem>
                    <asp:ListItem>What is your mothers name?</asp:ListItem>
                    <asp:ListItem>What is your fathers middle name?</asp:ListItem>
                    <asp:ListItem>What is your favorite food to eat?</asp:ListItem>
                    <asp:ListItem>What is your primary school name?</asp:ListItem>
                    <asp:ListItem>What is your favorite place to visit?</asp:ListItem>
                    <asp:ListItem>What is your favorite animal?</asp:ListItem>
                    <asp:ListItem>What is your zodiac sign?</asp:ListItem>
                    <asp:ListItem>What is your high school name?</asp:ListItem>
                    <asp:ListItem>What is your junior college name?</asp:ListItem>
                    <asp:ListItem>What is your college name?</asp:ListItem>
                </asp:DropDownList>
                </td><td>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*" ForeColor="#CC0000" Font-Size="Large" 
                Type="String" ControlToValidate="cmbquestion" ValueToCompare="<----- Select ----->" Operator="NotEqual"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>Answer</td>
            <td><asp:TextBox ID="txtans" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </td><td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                ControlToValidate="txtans" ErrorMessage="*" Font-Size="Large" 
                ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </td>
        </tr>                
        <tr>
            <td colspan="2">
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
