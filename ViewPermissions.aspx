<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPermissions.aspx.cs" Inherits="ViewPermissions" %>

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
        <br /><h3 class="page-title">Permitted Members List</h3><br />
        
        <table align="center" cellpadding="5" cellspacing="8" class="form-group">             
            <tr>
            <td>
         <%if (flag)
        { %>
                <asp:DataList ID="datapermissions"  runat="server" RepeatColumns="3" RepeatDirection="Horizontal" CellSpacing="0" 
                    CellPadding="7" OnItemCommand="Process">
                    <ItemTemplate>
                    <table class="table table-striped table-bordered " cellpadding="5px">
                        <tr><td>Document: <%#Eval("docname") %></td>
                        <td>Member: <%#Eval("usernm") %></td></tr>
                        <tr><td>Status: <%#Eval("status") %></td>
                        <td>
                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" 
                            CommandArgument='<%#Eval("id") %>' CommandName="revoke">Revoke Permission</asp:LinkButton>   
                        </td>
                        </tr>
                    </table>
                    </ItemTemplate>
                </asp:DataList>

                <br />
                <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click" CssClass="btn btn-info">Previous</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click" CssClass="btn btn-info">Next</asp:LinkButton>
                <%}
        else
        {
            %>
            <h3>No Records Found</h3>
            <%
        }
        %>
            </td>
            </tr>   
        </table>

        </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
