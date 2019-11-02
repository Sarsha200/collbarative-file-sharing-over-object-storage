<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CloudRentReport.aspx.cs" Inherits="CloudRentReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
        <br /><h3 class="page-title">Cloud Usage Report</h3><br />
    <%-------------------------------------------------%>
        <table>
            <tr>
                <td>Select Month</td>            
                <td>
                    <asp:TextBox ID="txtmonth" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                        ControlToValidate="txtmonth" ErrorMessage="*" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#CC0000"></asp:RequiredFieldValidator>                        
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtmonth" 
                        PopupButtonID="txtmonth" Format="MM/yyyy"></asp:CalendarExtender>                        
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-info" OnClick="Submit"/>
                </td>
            </tr>
        </table>
        <br /><br />
        <%if (flag)
        { %>
            <asp:DataList ID="datausage" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" 
                CellSpacing="10" CellPadding="7">
            <ItemTemplate>
           
                <table class="table table-striped table-bordered" cellpadding="3">
                    <tr><th colspan="2" style="color:mediumvioletred;font-size:16px;padding-left:30px;padding-top:10px;">
                        <%#Eval("User_Name") %></th>
                    </tr>                    
                        <td>Uploads : <%#Eval("Uploads") %></td>
                        <td>Downloads : <%#Eval("Downloads") %></td>
                    </tr>                                        
                    </tr>                    
                        <td>Encryption : <%#Eval("Encryption") %></td>
                        <td>Decryption : <%#Eval("Decryption") %></td>
                    </tr>
                    </tr>                    
                        <td>Permissions : <%#Eval("Permission") %></td>
                        <td>Key Generation : <%#Eval("Askkey") %></td>
                    </tr>
                    <tr>
                        <td>Doc Recovery : <%#Eval("Recovery") %></td>
                        <td>Logins : <%#Eval("Logins") %></td>
                    </tr>
                    <tr>
                        <td>Space (KB-MB) : <%#Eval("Space") %></td>
                        <td><b>Total Rent : <%#Eval("TotalRent") %></b></td>
                    </tr>
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
        <br /><br /><br />

    <%-------------------------------------------------%>
     </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
