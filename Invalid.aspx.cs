using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Invalid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["userid"] = "undefined";
        Session["utype"] = "undefined";
        Session["username"] = "undefined";
        Session["userphoto"] = "undefined";
        Session["seckey"] = "undefined";

        Session.Abandon();
    }
}