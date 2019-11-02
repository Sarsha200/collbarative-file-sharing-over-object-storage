<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Downloading.aspx.cs" Inherits="Downloading" %>

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
        <br /><h3 class="page-title">Secrete Key Authentication</h3><br />
            
        <br />
        <asp:Panel ID="Panel1" runat="server">    
            <h3>Congrats your file is ready to download..</h3>
            <a href="<%=Global.DocDownloadPath %>" target="_blank">Click Here to download file</a>                
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
            <h3 style="font-weight:normal;color:Red;font-size:20px">Something has gone wrong with your document download.. Please try again..</h3>
        </asp:Panel>
        <br /><br />
        
        <a href="ClientHome.aspx" class="button">Back Home</a>
      
        <br /><br />


        </center>
    </div>
    </form>

    <%Server.Execute("Footer.html"); %>
</body>
</html>
