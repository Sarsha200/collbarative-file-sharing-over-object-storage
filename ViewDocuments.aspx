<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDocuments.aspx.cs" Inherits="ViewDocuments" %>

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
        <br /><h3 class="page-title">Documents List</h3><br />
      
        <table align="center" cellpadding="5" cellspacing="8" class="form-group"> <tr><td>
        <%if (flag)
        { %>
            <asp:DataList ID="dataclients" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" 
                CellSpacing="7" CellPadding="4" OnItemCommand="Process">
            <ItemTemplate>
            <div class="DataList-Style">
                <table class="table table-striped table-bordered" style="padding:3px">
                    <tr><th colspan="3" style="color:mediumvioletred;font-size:16px;padding-left:10px;padding-top:10px;"><%#Eval("docname") %></th></tr>
                                                           
                    <tr>
                        <td>Upload Date : <%#Eval("uploaddt") %></td>
                        <td>Time : <%#Eval("uploadtime") %></td>
                        <td>Size : <%#Eval("filesize") %> bytes</td>
                    </tr>                    
                    <tr><td colspan="3"><%#Eval("description") %></td></tr>
                    <tr><td colspan="3"> 
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" 
                        CommandArgument='<%#Eval("uploadid") %>' CommandName="download">Download</asp:LinkButton>   
                        &nbsp;|&nbsp;
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                        CommandArgument='<%#Eval("uploadid") %>' CommandName="askkey">Ask for Key</asp:LinkButton>  
                    </td></tr>
                </table>                    
            </div>                                           
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
