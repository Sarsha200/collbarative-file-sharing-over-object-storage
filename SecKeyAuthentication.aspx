﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecKeyAuthentication.aspx.cs" Inherits="SecKeyAuthentication" %>

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
        <br /><h3 class="page-title">Secrete Key Authentication</h3><br />
       

        <table align="center" cellpadding="5" cellspacing="8" class="form-group"> 
            <tr>
            <td>Secrete Key</td>
            <td>
                <asp:TextBox ID="txtseckey" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="*" ControlToValidate="txtseckey" Font-Size="Large" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </td>
            </tr>
            <tr>
            <td colspan="3">
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Submit" CssClass="btn btn-info" />       
            </td>
            </tr>   
        </table>

        </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
