<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewServiceRents.aspx.cs" Inherits="ViewServiceRents" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/CloudStyles.css" />
</head>
<body>
   <%Server.Execute("Header.aspx"); %>
    <%Server.Execute("Navigate.aspx"); %> 
    <form id="form1" runat="server" class="form-horizontal col-sm-12 text-center">  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="form-group text-center">       
        <center>
        <br /><h3 class="page-title">Service Rents</h3><br />
        
        <table align="center" cellpadding="5" cellspacing="8" class="form-group">             
            <tr>
            <td>
        
                <asp:DataList ID="datarents"  runat="server" RepeatColumns="3" RepeatDirection="Horizontal" CellSpacing="0" CellPadding="7">
                    <ItemTemplate>
                    <table class="table table-striped table-bordered " cellpadding="5px">
                        <tr><td>Service: <%#Eval("service") %></td>
                        <td>Charge: Rs. <%#Eval("rent") %> /-</td></tr>
                    </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
            </tr>   
        </table>

        </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
