<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CloudAdmin.aspx.cs" Inherits="CloudAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <%Server.Execute("Header.aspx"); %>
    <%Server.Execute("Navigate.aspx"); %>
    <form id="form1" runat="server">
        <center>
        <br /><h3 class="page-title">Cloud Administrator</h3>

            <br /><br />

            <img src="images/adminhome.jpg" />
            <br /><br />
        </center>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
