using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SecKeyAuthentication : System.Web.UI.Page
{
    int value = 0;
    string message = "", title = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string userid = "", utype = "";
        try
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            userid = Convert.ToString(Session["userid"]);
            utype = Convert.ToString(Session["utype"]);

            if (userid == "undefined" && utype != "user")
                Response.Redirect("Invalid.aspx");

            DisableAuto(Page.Controls);
        }
        catch (Exception ex) { }
    }

    protected void Submit(object sender, EventArgs e)
    {
        int docid = 0;
        try
        {
            docid = Convert.ToInt32(Request.QueryString["docid"]);
            string key = txtseckey.Text;

            DocKeysGenerator gen = new DocKeysGenerator();
            gen.GetDocUserKey(docid);
            string secretekey = gen.DocUserKey;

            if (key == secretekey)
            {
                value = 1;
                Session["seckey"] = secretekey;
            }
            else
                value = 0;
        }
        catch (Exception ex) { value = 0; }

        if (value == 1)
        {
            Response.Redirect("Downloading.aspx?docid="+docid);            
        }
        else if (value == 0)
        {
            message = "Sorry.!! Secrete Key Authentication Failed.. Try again..";
            title = "Failure Report";
        }

        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
        ClearInputs(Page.Controls);
    }

    private void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();

            ClearInputs(ctrl.Controls);
        }
    }

    private void DisableAuto(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Attributes.Add("autocomplete", "off");

            DisableAuto(ctrl.Controls);
        }

    }
}