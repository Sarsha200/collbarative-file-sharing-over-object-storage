<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllotFilePermissions.aspx.cs" Inherits="AllotFilePermissions" %>

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
        <br /><h3 class="page-title">My Documents List</h3><br />
      
            <%if (flag)
        { %>
        <table align="center" cellpadding="5" cellspacing="8" class="form-group"> 
            <tr>
                <td>
                    <asp:Button ID="CheckAll" runat="server" Text="Check All" onclick="CheckAll_Click" CssClass="btn btn-info" />
                    &nbsp;
                    <asp:Button ID="UncheckAll" runat="server" Text="Uncheck All" onclick="UncheckAll_Click" CssClass="btn btn-info"/> 
                </td>
            </tr>
            <tr><td>
       
                <asp:GridView ID="grdusers" runat="server" CssClass="table table-striped table-bordered"
                    EnableViewState="false" AutoGenerateColumns="false" DataKeyNames="userid">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="selectact"  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>                              
                        <asp:BoundField  DataField="name" HeaderText="Member Name" />
                        
                    </Columns>
                </asp:GridView>
            </td></tr>
            <tr><td> <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Submit" CssClass="btn btn-info" /></td></tr>
        </table>
            <%}
        else
        {
            %>
            <h3>No Records Found</h3>
            <%
        }
        %>
        </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
