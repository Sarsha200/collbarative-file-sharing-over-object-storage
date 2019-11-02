<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClientsList.aspx.cs" Inherits="ClientsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Companies</title>
    <link rel="stylesheet" href="css/CloudStyles.css" />
</head>
<body>
   <%Server.Execute("Header.aspx"); %>
    <%Server.Execute("Navigate.aspx"); %> 
    <form id="form1" runat="server" class="form-horizontal col-sm-12 text-center">  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="form-group text-center">       
        <center>
        <br /><h3 class="page-title">Clients List</h3><br />

        <table align="center" cellpadding="5" cellspacing="8" class="form-group"> 
            <tr>
                <td>
                <%if (flag)
                { %>
                    <asp:DataList ID="dataclients" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" 
                        CellSpacing="10" CellPadding="7">
                    <ItemTemplate>
                    <div class="DataList-Style">
                       <table class="table table-striped table-bordered " cellpadding="5px">
                            <tr><th colspan="2" style="color:mediumvioletred;font-size:16px;padding-left:30px;padding-top:10px;"><%#Eval("name") %></th></tr>
                            <tr><td rowspan="6"><img src='Profile/<%#Eval("photo") %>' class="img-fluid rounded-circle" style="max-height:130px;max-width:120px;" /></td>
                                <td>Mobile : <%#Eval("mobile") %></td>
                            </tr>                                        
                            <tr><td>Email Id : <%#Eval("emailid") %></td></tr>                            
                            <tr><td>DOB : <%#Eval("dob") %></td></tr>                    
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
        

            </td>                    
            </tr> 

        </table>

        </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
