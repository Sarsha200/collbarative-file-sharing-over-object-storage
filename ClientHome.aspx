<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClientHome.aspx.cs" Inherits="ClientHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <%Server.Execute("Header.aspx"); %>
    <%Server.Execute("Navigate.aspx"); %>
    <form id="form1" runat="server">
        <div>
            <center>
        <br /><h3 class="page-title">Client Home</h3>
                <br /><br />

            <img src="images/userhome.png" />
            <br /><br />

                </center>
        </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
