<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyDocsList.aspx.cs" Inherits="MyDocsList" %>

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
      
        <table align="center" cellpadding="5" cellspacing="8" class="form-group"> <tr><td>
        <%if (flag)
        { %>
            <asp:DataList ID="dataclients" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" 
                CellSpacing="7" CellPadding="4" OnItemCommand="Process">
            <ItemTemplate>
            
                <table class="table table-striped table-bordered" style="padding:3px">
                    <tr>
                        <th colspan="2" style="color:mediumvioletred;font-size:16px;padding-left:10px;padding-top:10px;"><%#Eval("docname") %></th>
                        <td>
                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" 
                            CommandArgument='<%#Eval("uploadid") %>' CommandName="download">Download</asp:LinkButton>
                        </td>   
                        </tr>
                    <tr>
                        <td>Upload Date : <%#Eval("uploaddt") %></td>
                        <td>Time : <%#Eval("uploadtime") %></td>
                        <td>Size : <%#Eval("filesize") %> bytes</td>
                    </tr>                    
                    <tr><td colspan="3"><%#Eval("description") %></td></tr>
                    <tr><td colspan="3">                                                 
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandArgument='<%#Eval("uploadid") %>' CommandName="permission" 
                        OnClientClick="return confirm('You will be allowing other users to access the document by sending them secrete key thru mail.??')">Allot Permissions</asp:LinkButton>
                        &nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="false" 
                        CommandArgument='<%#Eval("uploadid") %>' CommandName="viewpermissions">View Permissions</asp:LinkButton>
                        &nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="false" 
                        CommandArgument='<%#Eval("uploadid") %>' CommandName="delete"
                        OnClientClick="return confirm('Are you sure you want to delete document.??')">Delete</asp:LinkButton>
                    
                    </td></tr>
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
        

     </td></tr>
            </table>

        </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
