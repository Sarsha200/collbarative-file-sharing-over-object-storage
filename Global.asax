<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
    ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        Session["userid"] = "undefined";
        Session["utype"] = "undefined";        
        Session["username"]= "undefined";
        Session["userphoto"]= "undefined";
        Session["seckey"]= "undefined";
        
    }

    void Session_End(object sender, EventArgs e) 
    {
        Session["userid"] = "undefined";
        Session["utype"] = "undefined";        
        Session["username"]= "undefined";
        Session["userphoto"]= "undefined";        
        Session["seckey"]= "undefined";

        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
