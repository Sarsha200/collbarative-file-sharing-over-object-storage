using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class ConfirmCloudPayment : System.Web.UI.Page
{
    string uid = "", ops = "", nps = "", rps = "";
    int value = 0;
    CloudFuns obj = new CloudFuns();
    string message = "", title = "";
    ManageClients objc = new ManageClients();

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

            if (userid == "undefined" && (utype != "admin"))
                Response.Redirect("Invalid.aspx");
        }
        catch (Exception ex) { }

        try
        {
            if (!IsPostBack)
                objc.FillClientsList(cmbmember);

            String tday = DateTime.Today.ToShortDateString() + "   " + DateTime.Now.ToShortTimeString();
            txtpatdt.Text = tday;

            txtrentyear.Text = DateTime.Today.Year.ToString();

            DisableAuto(Page.Controls);
        }
        catch (Exception ex) { }
    }

    protected void Submit(object sender, EventArgs e)
    {
        try
        {
            string qr = "update cloudpayments set paydate='" + txtpatdt.Text.Trim() + "', paymode ='" + cmbpaymode.Text.Trim() + "', paystatus='paid' where";
            qr += " rentmonth ='" + cmbmonth.SelectedItem.Value.Trim() + "' and userid='" + cmbmember.SelectedItem.Value.Trim() + "' and rentyear='" + DateTime.Today.Year.ToString() + "'";

            if (obj.Execute(qr))
            {
                value = 1;
            }
            else
            {
                value = 0;
            }

        }
        catch (Exception ex)
        {
            value = 0;
        }
        if (value == 1)
        {
            message = "Payment done successfully..";
            title = "Success Report";
        }
        else if (value == 0)
        {
            message = "Payment failed..";
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

    protected void cmbmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int month = Convert.ToInt32(cmbmonth.SelectedItem.Value);
            int year = DateTime.Now.Year;
            string userid = cmbmember.SelectedItem.Value;

            ManageCloudPays objpay = new ManageCloudPays();
            txtamt.Text = objpay.GetCloudRent(month, year, userid);

        }
        catch (Exception ex) { }
    }
}