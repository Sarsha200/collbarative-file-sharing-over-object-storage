<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClientProfile.aspx.cs" Inherits="ClientProfile" %>
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
        <br /><h3 class="page-title">Client Profile</h3><br />
        
        <table cellpadding="5" cellspacing="8" class="form-group"> 
            <tr>
                <td>Client Name</td>
                <td><asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                    </td><td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtname" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </td>
            </tr>                       
            <tr>
                <td>Contact No</td>
                <td><asp:TextBox ID="txtmobile" runat="server" CssClass="form-control"></asp:TextBox></td><td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtmobile" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" 
                        ForeColor="#CC0000"></asp:RequiredFieldValidator>
                
                    <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Double" 
                        ControlToValidate="txtmobile" ErrorMessage="Incorrect Mobile No" 
                        ForeColor="#CC0000" MaximumValue="9999999999" MinimumValue="7000000000"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>Email Id</td>
                <td><asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                    </td><td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtemail" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" 
                        ForeColor="#CC0000"></asp:RequiredFieldValidator>               
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ErrorMessage="Invalid email id" ForeColor="#CC0000"  ControlToValidate="txtemail"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                </td>
            </tr>                        
            <tr>
                <td>Date of Birth</td>            
                <td>
                    <asp:TextBox ID="txtdob" runat="server" CssClass="form-control"></asp:TextBox>
                                           
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdob" PopupButtonID="txtdob" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" 
                    TargetControlID="txtdob" FilterType="Custom, Numbers" ValidChars="/_" />     
                    
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                        ControlToValidate="txtdob" ErrorMessage="*" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#CC0000"></asp:RequiredFieldValidator> 
                </td>
            </tr>
            <tr>
                <td>Security Question</td>
                <td>
                    <asp:DropDownList ID="cmbques" runat="server" CssClass="form-control">
                    <asp:ListItem><--Select--></asp:ListItem>
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
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*" 
                        ControlToValidate="cmbques" ForeColor="Red" ValueToCompare="<--Select-->" Operator="NotEqual" 
                        Type="String"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>Answer</td>
                <td><asp:TextBox ID="txtans" runat="server" CssClass="form-control"></asp:TextBox>
                    </td><td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                        ControlToValidate="txtans" ErrorMessage="*" Font-Bold="True" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>                
            
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
