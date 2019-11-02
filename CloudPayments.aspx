<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CloudPayments.aspx.cs" Inherits="CloudPayments" %>
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
        <br /><h3 class="page-title">Cloud Payments</h3><br />
    <%-------------------------------------------------%>
        <table align="center" cellpadding="5" cellspacing="5" align="center">
            <tr>
                <td>Select Month</td>            
                <td>
                    <asp:TextBox ID="txtdob" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                        ControlToValidate="txtdob" ErrorMessage="*" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#CC0000"></asp:RequiredFieldValidator>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdob" PopupButtonID="txtdob" Format="MM/yyyy"></asp:CalendarExtender>                    
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Generate" CssClass="btn btn-info" OnClick="Submit"/>
                </td>
            </tr>
        </table>
        <br /><br />
        <table cellpadding="5" cellspacing="5" align="center">
        <tr>
        <td>
            <%if (flag)
            { %>
            <asp:DataList ID="dataresult"  runat="server" RepeatColumns="2" RepeatDirection="Horizontal" CellSpacing="0" 
                CellPadding="7">
                <ItemTemplate>
                <table class="table table-striped table-bordered" cellpadding="5" cellspacing="5">
                    <tr><th style="color:green;font-size:16px;" colspan="3"><%#Eval("usernm") %></th>                        
                    </tr>                    
                    <tr><td>Rent Month : <%#Eval("rentmonth") %></td><td>Rent Year : <%#Eval("rentyear") %></td>
                    <td>Amount : <%#Eval("amount") %></td></tr>
                    <tr><td>[ <%#Eval("paystatus") %> ]</td>
                    <td>Payment Date : <%#Eval("paydate") %></td><td>Payment Mode : <%#Eval("paymode") %></td></tr>
                                       
                </table>
                </ItemTemplate>
            </asp:DataList>

            <br /><br />
            <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click" CssClass="btn btn-info">Previous</asp:LinkButton>
            &nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click" CssClass="btn btn-info">Next</asp:LinkButton>
            <br />
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


    <br /><br />
             </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
