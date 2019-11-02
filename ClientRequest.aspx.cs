using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;

public partial class ClientRequest : System.Web.UI.Page
{
    
    CloudFuns cfun = new CloudFuns();
    ManageClients obj = new ManageClients();
    string message = "", title = "";
    public Boolean flag = false;
    int value = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            DisableAuto(Page.Controls);
        }
        catch (Exception ex) { }
    }

    protected void Submit(object sender, EventArgs e)
    {
        try
        {
            string newname = "";
            string path = "";

            string name = txtname.Text;
            string mobile = txtmobile.Text;

            Random rnd = new Random();
            int no = rnd.Next(100, 999);

            if (!obj.CheckExists(name, mobile, txtemail.Text))
            {

                int reqid = cfun.FetchMax("clientpersonal", "id");

                GenerateCredits credit = new GenerateCredits();
                string userid = credit.GenUserid(name, reqid);
                string pass = credit.GenPassword(name, no);

                if (filephoto.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(filephoto.FileName);
                    path = Server.MapPath("Profile/");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    newname = userid + "_pht" + ext;

                    if (newname.Contains(" "))
                        newname = newname.Replace(' ', 'P');
                }

                ArrayList data = new ArrayList();
                data.Clear();
                data.Add(name);
                data.Add(newname);
                data.Add(mobile);
                data.Add(txtemail.Text);                
                data.Add(txtdob.Text);
                data.Add(userid);
                data.Add(cmbques.SelectedItem.Text);
                data.Add(txtans.Text);
                data.Add(pass);

                if (obj.InsertRequest(data))
                {
                    value = 1;

                    if (File.Exists(path + newname))
                        File.Delete(path + newname);
                    filephoto.SaveAs(path + newname);
                }
                else
                    value = 0;
            }
            else
                value = 3;

        }
        catch (Exception ex) { value = 0; }
        if (value == 1)
        {
            message = "Congrats.!! Your request submitted successfully..";
            title = "Success Report";
        }
        else if (value == 0)
        {
            message = "Sorry.!! Your registration reqest failed.. Try again..";
            title = "Failure Report";
        }
        else if (value == 3)
        {
            message = "Sorry.!! You are already registered with us.. Try again..";
            title = "Failure Report";
        }

        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
        ClearInputs(Page.Controls);
        DisableAuto(Page.Controls);
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