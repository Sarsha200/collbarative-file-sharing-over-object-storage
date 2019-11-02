<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Invalid.aspx.cs" Inherits="Invalid" %>

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
        <br /><h3 class="page-title">Access Denied</h3><br />
       
        <table align="center" cellpadding="5" cellspacing="8" class="form-group"> <tr><td>
                <h2 style="color:orangered">
                    
                    Sorry.. Your session expired.. Login Again..

                </h2>
                 <h3><a href="Default.aspx">Back Home</a></h3><br />
                </td></tr>
            </table>

        </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
