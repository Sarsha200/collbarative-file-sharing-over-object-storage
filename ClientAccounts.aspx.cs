using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ClientAccounts : System.Web.UI.Page
{
    public string act = "";
    int value = 0;
    ManageClients objclient = new ManageClients();
    string message = "", title = "";
    DataSet ds;
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
            act = Request.QueryString["act"];
            FillGrid();
        }
        catch (Exception) { }
        
    }

    protected void FillGrid()
    {
        try
        {
            act = Request.QueryString["act"];

            ds = new DataSet();
            ds = objclient.GetClientsList(act);

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
            string commandname = e.CommandName.ToString();
            string userid = Convert.ToString(e.CommandArgument);

            ManageClients obj = new ManageClients();

            if (obj.ProcessClient(userid, commandname))
                value = 1;
            else
                value = 0;

            FillGrid();

        }
        catch (Exception ex)
        {
            value = 0;
        }

        if (value == 1)
        {
            message = "Client processed successfully..";
            title = "Success Report";
        }
        else if (value == 0)
        {
            message = "Sorry.!! Client account processing failed.. Try again..";
            title = "Failure Report";
        }

        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), title, script, true);

    }
}