<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmCloudPayment.aspx.cs" Inherits="ConfirmCloudPayment" %>

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
        <br /><h3 class="page-title">Confirm Cloud Payment</h3><br />
    <%-------------------------------------------------------------------------------%>

        <table align="center" cellpadding="7" cellspacing="10" class="form-group">
        <tr>
            <td>Client Member</td>
            <td>
                <asp:DropDownList ID="cmbmember" runat="server" CssClass="form-control">
                </asp:DropDownList></td><td>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Select Client User"
                ControlToValidate="cmbmember" ValueToCompare="<----- Select ----->" Operator="NotEqual" ForeColor="#CC0000"></asp:CompareValidator>
            </td>
        </tr>
            <tr>
            <td>Payment Date</td>
            <td><asp:TextBox ID="txtpatdt" runat="server" CssClass="form-control"></asp:TextBox></td><td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtpatdt" ErrorMessage="*" Font-Bold="True" Font-Size="Large" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </td>
        </tr>
            <tr>
            <td>Rent For Month</td>
            <td>
                <asp:DropDownList ID="cmbmonth" runat="server" AutoPostBack="true" CssClass="form-control"
                onselectedindexchanged="cmbmonth_SelectedIndexChanged">
                <asp:ListItem Value="<---Select Month--->"><---Select Month---></asp:ListItem>
                <asp:ListItem Value="01">January</asp:ListItem>
                <asp:ListItem Value="02">February</asp:ListItem>
                <asp:ListItem Value="03">March</asp:ListItem>
                <asp:ListItem Value="04">April</asp:ListItem>
                <asp:ListItem Value="05">May</asp:ListItem>
                <asp:ListItem Value="06">June</asp:ListItem>
                <asp:ListItem Value="07">July</asp:ListItem>
                <asp:ListItem Value="08">August</asp:ListItem>
                <asp:ListItem Value="09">September</asp:ListItem>
                <asp:ListItem Value="10">October</asp:ListItem>
                <asp:ListItem Value="11">November</asp:ListItem>
                <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList></td><td>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Select Month"
                ControlToValidate="cmbmonth" ValueToCompare="<---Select Month--->" Operator="NotEqual" ForeColor="#CC0000"></asp:CompareValidator>
            </td>
        </tr>
            <tr>
            <td>Rent For Year</td>
            <td><asp:TextBox ID="txtrentyear" runat="server" CssClass="form-control"></asp:TextBox></td><td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtrentyear" ErrorMessage="*" Font-Bold="True" Font-Size="Large" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </td>
        </tr>
            <tr>
            <td>Amount</td>
            <td><asp:TextBox ID="txtamt" runat="server" CssClass="form-control"></asp:TextBox></td><td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtamt" ErrorMessage="*" Font-Bold="True" Font-Size="Large" 
                    ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </td>
        </tr>
            <tr>
            <td>Payment Mode</td>
            <td >
                <asp:DropDownList ID="cmbpaymode" runat="server" CssClass="form-control">
                <asp:ListItem Value="Online">Online</asp:ListItem>
                <asp:ListItem Value="DD / Cheque">DD / Cheque</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center"><asp:Button ID="Button1" runat="server" CssClass="btn btn-info" Text="Confirm Payment" OnClick="Submit" /></td>
        </tr>
        </table>
        <br /><br />    
 </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
