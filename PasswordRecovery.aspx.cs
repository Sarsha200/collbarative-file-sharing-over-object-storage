using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PasswordRecovery : System.Web.UI.Page
{
    int value = 0;
    CloudFuns obj = new CloudFuns();
    string message = "", title = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        DisableAuto(Page.Controls);
    }

    protected void Submit(object sender, EventArgs e)
    {
        try
        {
            string email = "", mobile = "", name = "", ques = "", ans = "";
            string uid = txtuserid.Text.Trim();
            ManageUsers objuser = new ManageUsers();
            string utype = objuser.GetUserType(uid);

            if (utype == "user")
            {
                GetClientDetails objc = new GetClientDetails(uid);
                email = objc.ClientEmailId;
                mobile = objc.ClientMobile;
                name = objc.ClientName;
                ques = objc.ClientSecQues;
                ans = objc.ClientAnswer;
            }
            

            if (ques == cmbquestion.SelectedItem.Text && ans == txtans.Text)
            {
                string npass = email.Substring(1, 3) + "!" + name.Substring(1, 3) + "*" + mobile.Substring(3, 2);

                if (objuser.UpdatePass(uid, npass))
                {
                    value = 1;

                    EmailService mail = new EmailService();
                    mail.sendRecovery(email, npass, name);
                }
                else
                {
                    value = 0;
                }
            }
            else
            {
                value = 0;
            }
        }
        catch (Exception) { value = 0; }

        if (value == 1)
        {
            message = "Password recovered successfully.. Details are sent on your registered emailid..";
            title = "Success Report";
        }
        else if (value == 0)
        {
            message = "Password recovery failed..";
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