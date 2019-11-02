using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;

public partial class ClientProfile : System.Web.UI.Page
{
    int value = 0;
    CloudFuns cfun = new CloudFuns();
    ManageClients obj = new ManageClients();
    string message = "", title = "";
    public Boolean flag = false;
    
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

        try
        {
            userid = Convert.ToString(Session["userid"]);

            if(!IsPostBack)
            {
                GetClientDetails dtl = new GetClientDetails(userid);

                
                txtans.Text = dtl.ClientAnswer;
                txtemail.Text = dtl.ClientEmailId;
                txtdob.Text = dtl.ClientDOB;
                txtmobile.Text = dtl.ClientMobile;
                txtname.Text = dtl.ClientName;

                cmbques.SelectedItem.Text = dtl.ClientSecQues;
                cmbques.SelectedItem.Value = dtl.ClientSecQues;
            }

        }
        catch (Exception) { }
        try
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            DisableAuto(Page.Controls);
        }
        catch (Exception) { }
    }

    protected void Submit(object sender, EventArgs e)
    {
        try
        {            
            string name = txtname.Text;
            string mobile = txtmobile.Text;

            string userid = Convert.ToString(Session["userid"]);
            
            ArrayList data = new ArrayList();
            data.Clear();
            data.Add(name);            
            data.Add(mobile);
            data.Add(txtemail.Text);            
            data.Add(txtdob.Text);            
            data.Add(cmbques.SelectedItem.Text);
            data.Add(txtans.Text);
            data.Add(userid);

            if (obj.UpdateClient(data))
            {
                value = 1;
            }
            else
                value = 0;

        }
        catch (Exception ex) { value = 0; }
        if (value == 1)
        {
            message = "Profile updated  successfully..";
            title = "Success Report";
        }
        else if (value == 0)
        {
            message = "Sorry.!! Profile update failed.. Try again..";
            title = "Failure Report";
        }

        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
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