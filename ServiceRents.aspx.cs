using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class ServiceRents : System.Web.UI.Page
{
 
    int value = 0;
    CloudFuns obj = new CloudFuns();
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

            if (userid == "undefined" && utype != "admin")
                Response.Redirect("Invalid.aspx");

            DisableAuto(Page.Controls);
        }
        catch (Exception ex) { }
    }

    protected void Submit(object sender, EventArgs e)
    {
        try
        {
            ManageServices serv = new ManageServices();

            string service = cmbservice.SelectedItem.Value;
            double rent = Convert.ToDouble(txtcharge.Text);

            if (serv.AddService(service, rent))
                value = 1;
            else
                value = 0;
        }
        catch (Exception) { value = 0; }

        if (value == 1)
        {
            message = "Service charges stored successfully..";
            title = "Success Report";
        }
        else if (value == 0)
        {
            message = "Service charges storage failed..";
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

    protected void GetCharges(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            string service = cmbservice.SelectedItem.Value;
            ds = obj.GetData("select rent from servicecharges where service='" + service + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtcharge.Text = Convert.ToString(ds.Tables[0].Rows[0]["rent"]);
            }
            else
                txtcharge.Text = "0.0";
        }
        catch (Exception) { txtcharge.Text = "0.0"; }
    }
}