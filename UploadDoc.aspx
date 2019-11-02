<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadDoc.aspx.cs" Inherits="UploadDoc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <%Server.Execute("Header.aspx"); %>
    <%Server.Execute("Navigate.aspx"); %> 
    <form id="form1" runat="server" class="form-horizontal col-sm-12 text-center">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="form-group text-center">       
        <center>
        <br /><h3 class="page-title">Upload Document</h3><br />
        
        <table class="form-group" cellspacing="10" cellpadding="10">
            <tr>
                <td>Document Title</td>
                <td><asp:TextBox ID="txttitle" runat="server" CssClass="form-control"></asp:TextBox>
                    </td><td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txttitle" ErrorMessage="*" Font-Size="Medium" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </td>
            </tr> 
            <tr>
                <td>
                    Document
                </td>
                <td>
                    <asp:FileUpload ID="filedocument" runat="server" />    
                    </td><td>                                                          
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                    ControlToValidate="filedocument" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>  
                </td>
            </tr> 
            <tr>
                <td>Description</td>
                <td><asp:TextBox ID="txtdesc" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                    </td><td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtdesc" ErrorMessage="*" Font-Size="Medium" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>
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
