using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PendingClients : System.Web.UI.Page
{
    int value = 0;
    ManageClients objclient = new ManageClients();
    string message = "", title = "";
    DataSet ds;
    public string utype = "";
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
        }
        catch (Exception ex) { }

        try
        {
            FillGrid();
        }
        catch (Exception) { }
    }

    protected void FillGrid()
    {
        try
        {
            ds = new DataSet();
            ds = objclient.GetClientsDetails();

            int count = ds.Tables[0].Rows.Count;

            if (count > 0)
                flag = true;
            else
            {
                flag = false;
                LinkButton2.Visible = false;
                LinkButton3.Visible = false;
            }

            PagedDataSource pgd = new PagedDataSource();
            pgd.DataSource = ds.Tables[0].DefaultView;
            pgd.CurrentPageIndex = CurrentPageIndex;
            pgd.AllowPaging = true;

            if (count < 8)
                pgd.PageSize = count;
            else
                pgd.PageSize = 8;

            LinkButton2.Enabled = !(pgd.IsLastPage);
            LinkButton3.Enabled = !(pgd.IsFirstPage);

            dataclients.DataSource = pgd;
            dataclients.DataBind();
        }
        catch (Exception)
        {
            flag = false;
            LinkButton2.Visible = false;
            LinkButton3.Visible = false;
        }
    }

    public int CurrentPageIndex
    {
        get
        {
            if (ViewState["pg"] == null)
                return 0;
            else
                return Convert.ToInt16(ViewState["pg"]);
        }
        set
        {
            ViewState["pg"] = value;
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        CurrentPageIndex++;
        FillGrid();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        CurrentPageIndex--;
        FillGrid();
    }

    protected void Process(object source, DataListCommandEventArgs e)
    {
        try
        {
            string name = "", email = "", pass = "";
            string commandname = e.CommandName.ToString();
            string clientuserid = Convert.ToString(e.CommandArgument);

            GetClientDetails dtl = new GetClientDetails(clientuserid);
            name = dtl.ClientName;
            email = dtl.ClientEmailId;
            
            if(commandname=="approved")
            {
                pass = dtl.ClientPass;

                if(objclient.ProcessRequest(clientuserid,commandname))
                {
                    value = 1;

                    EmailService mail = new EmailService();
                    mail.sendAccDetails(email, clientuserid, pass, name);
                }                
            }
            else if (commandname == "rejected")
            {
                if (objclient.ProcessRequest(clientuserid, commandname))
                {
                    value = 1;

                    EmailService mail = new EmailService();
                    mail.sendRejectStatus(email, name);
                }
            }
            
            else
                value = 0;

        }
        catch (Exception) { value = 0; }

        if (value == 1)
        {
            message = "Client request processed successfully..";
            title = "Success Report";
        }
        else if (value == 0)
        {
            message = "Client request processing failed..";
            title = "Failure Report";
        }
        FillGrid();
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
    }
}