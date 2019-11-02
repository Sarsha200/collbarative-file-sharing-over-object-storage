using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ViewPermissions : System.Web.UI.Page
{
    public Boolean flag = false;
    CloudFuns obj = new CloudFuns();    
    string message = "", title = "";
    int value = 0;
    DataSet ds;
    string userid = "", utype = "";
    ManageDocPermissions perm = new ManageDocPermissions();

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
        }
        catch (Exception ex) { }

        try
        {
            FillGrid();
        }
        catch (Exception) { }
    }

    public void FillGrid()
    {
        try
        {
            string userid = Convert.ToString(Session["userid"]);
            int docid = Convert.ToInt32(Request.QueryString["docid"]);
            
            DataSet ds = new DataSet();            
            ds = perm.GetPermittedDocuments(userid,docid);

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

            datapermissions.DataSource = pgd;
            datapermissions.DataBind();
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
        int id = 0;
        try
        {
            string commandname = e.CommandName.ToString();
            id = Convert.ToInt32(Convert.ToString(e.CommandArgument));

            if (perm.RevokePermission(id))
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
            message = "File permissions revoked successfully..";
            title = "Success Report";

            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
        }
        else if (value == 0)
        {
            message = "Sorry.!! File permissions revoke failed.. Try again..";
            title = "Failure Report";

            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
        }

    }
}