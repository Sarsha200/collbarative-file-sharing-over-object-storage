using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ChangePassword : System.Web.UI.Page
{
    string uid = "", ops = "", nps = "", rps = "";
    int value = 0;
    CloudFuns obj = new CloudFuns();
    ManageUsers objuser = new ManageUsers();
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

    protected void Change(object sender, EventArgs e)
    {
        try
        {
            string id = Session["userid"].ToString().Trim();
            uid = usid.Text.Trim();
            ops = opass.Text.Trim();
            nps = npass.Text.Trim();
            rps = rpass.Text.Trim();

            if (uid.Trim().Equals(id))
            {
                if (objuser.IsUser(uid, ops))
                {
                    if (objuser.UpdatePass(uid, nps))
                        value = 1;
                    else
                        value = 0;
                }
                else
                    value = 2;
            }
            else
            {
                value = 0;
            }
        }
        catch (Exception)
        {
            value = 0;
        }

        if (value == 1)
        {
            message = "Password changed successfully..";
            title = "Success Report";
        }
        else if (value == 0)
        {
            message = "Password change failed..";
            title = "Failure Report";
        }
        else if (value == 2)
        {
            message = "You are and unauthorised user..";
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