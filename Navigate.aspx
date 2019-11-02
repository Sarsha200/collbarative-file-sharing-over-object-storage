<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Navigate.aspx.cs" Inherits="Navigate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <nav class="navbar navbar-expand-sm bg-dark navbar-dark">
      
    <%
        string userid = Convert.ToString(Session["userid"]);
        string utype = Convert.ToString(Session["utype"]);

        try
        {
            if (utype.ToLower() == "admin".ToLower())
            {%>
            <!-- Brand -->
            <a class="navbar-brand" href="#">Welcome <%=Convert.ToString(Session["username"]) %> | </a>   
            <!-- Links -->
            <ul class="navbar-nav">
            <li class="nav-item"><a class="nav-link" href="CloudAdmin.aspx">Home</a></li>
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">
                Client Users
              </a>
              <div class="dropdown-menu">
                <a class="dropdown-item" href="PendingClients.aspx">Pending Requests</a>
                <a class="dropdown-item" href="ClientsList.aspx">Client Users List</a>                
              </div>
            </li>    
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">
                Cloud Services
              </a>
              <div class="dropdown-menu">
                <a class="dropdown-item" href="ServiceRents.aspx">Service Rents</a>
                <a class="dropdown-item" href="ViewServiceRents.aspx">View Cloud Rents</a>                
              </div>
            </li>    
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">
                Cloud Usage
              </a>
              <div class="dropdown-menu">
                <a class="dropdown-item" href="CloudUsageReport.aspx">Cloud Usage</a>
                <a class="dropdown-item" href="CloudRentReport.aspx">Cloud Rent</a>                
              </div>
            </li>  
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">
                Cloud Payments
              </a>
              <div class="dropdown-menu">
                <a class="dropdown-item" href="ConfirmCloudPayment.aspx">Confirm Cloud Payment</a>
                <a class="dropdown-item" href="CloudPayments.aspx?status=pending">Pending Payments</a>
                <a class="dropdown-item" href="CloudPayments.aspx?status=paid">Paid Payments</a>                        
              </div>
              <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">
                Client Accounts
              </a>
              <div class="dropdown-menu">
                <a class="dropdown-item" href="ClientAccounts.aspx?act=activate">Activate Client</a>
                <a class="dropdown-item" href="ClientAccounts.aspx?act=deactivate">Deactivate Client</a>                
              </div>
            </li> 
            </li>                          
	        <li class="nav-item"><a class="nav-link" href="Logout.aspx">Logout</a></li>
            <%                
            }
            else if (utype == "user")
            {%>
            <!-- Brand -->
            <a class="navbar-brand" href="#">
                <img src="Profile/<%=Session["userphoto"].ToString() %>" class="img-fluid" style="border-radius:50%;max-height:70px;"/>
                Welcome <%=Convert.ToString(Session["username"]) %> | </a>      
            <!-- Links -->
            <ul class="navbar-nav">
            <li class="nav-item"><a class="nav-link" href="ClientHome.aspx">Home</a></li>
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">
                My Profile
              </a>
              <div class="dropdown-menu">
                <a class="dropdown-item" href="ClientProfile.aspx">My Profile</a>
                <a class="dropdown-item" href="ChangePassword.aspx">Change Password</a>                             
              </div>
            </li>                 
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">
                Manage Documents
              </a>
              <div class="dropdown-menu">
                <a class="dropdown-item" href="UploadDoc.aspx">Upload Document</a>
                <a class="dropdown-item" href="MyDocsList.aspx">My Documents</a>
                  <a class="dropdown-item" href="ViewDocuments.aspx">Download Documents</a>
              </div>
            </li> 
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">
                Cloud Services
              </a>
              <div class="dropdown-menu">               
                <a class="dropdown-item" href="ViewServiceRents.aspx">View Cloud Rents</a>                
              </div>
            </li>    
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">
                Cloud Usage
              </a>
              <div class="dropdown-menu">
                <a class="dropdown-item" href="CloudUsageReport.aspx">Cloud Usage</a>
                <a class="dropdown-item" href="CloudRentReport.aspx">Cloud Rent</a>                
              </div>
            </li>  
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">
                Cloud Payments
              </a>
              <div class="dropdown-menu">                
                <a class="dropdown-item" href="CloudPayments.aspx?status=pending">Pending Payments</a>
                <a class="dropdown-item" href="CloudPayments.aspx?status=paid">Paid Payments</a>                
              </div>
            </li>                          
	        <li class="nav-item"><a class="nav-link" href="Logout.aspx">Logout</a></li>
            <%                             
            }
            
            else
            {%>
            <ul class="navbar-nav">
            <li class="nav-item"><a class="nav-link" href="Default.aspx">Home</a></li>            
            <li class="nav-item"><a class="nav-link" href="ClientRequest.aspx">New Client Request</a></li>            
            <li class="nav-item"><a class="nav-link" href="PasswordRecovery.aspx">Password Recovery</a></li>
            <%                
            }
        }
        catch (Exception ex)
        {
        %>
        <ul class="navbar-nav">
        <li class="nav-item"><a class="nav-link" href="Default.aspx">Home</a></li>            
        <li class="nav-item"><a class="nav-link" href="ClientRequest.aspx">New Client Request</a></li>            
        <li class="nav-item"><a class="nav-link" href="PasswordRecovery.aspx">Password Recovery</a></li>

         <%                             
        }
        %>
          </ul>
    </nav>
</body>
</html>
