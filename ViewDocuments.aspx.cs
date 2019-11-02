using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class ViewDocuments : System.Web.UI.Page
{
    CloudFuns cfun = new CloudFuns();    
    string message = "", title = "";
    public Boolean flag = false;
    int value = 0;
    DataSet ds;
    string userid = "", utype = "";

    protected void Page_Load(object sender, EventArgs e)
    {
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

    protected void FillGrid()
    {
        try
        {
            ManageDocPermissions obj = new ManageDocPermissions();
            userid = Convert.ToString(Session["userid"]);

            ds = new DataSet();
            ds = obj.GetPermittedDocuments(userid);

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
        int docid = 0;
        try
        {
            string commandname = e.CommandName.ToString();
            docid = Convert.ToInt32(Convert.ToString(e.CommandArgument));

            if (commandname == "download")
            {
                value = 2;
            }
            if (commandname == "askkey")
            {
                string userid = Convert.ToString(Session["userid"]);
                GetClientDetails cdtl = new GetClientDetails(userid);
                string name = cdtl.ClientName;
                string email = cdtl.ClientEmailId;

                DocKeysGenerator gen = new DocKeysGenerator();
                gen.GetDocUserKey(docid);
                string seckey = gen.DocUserKey;

                GetDocumentDetails dtl = new GetDocumentDetails(docid);
                string doctitle = dtl.DocTitle;
                string docowner = dtl.DocOwner;
                string size = dtl.DocSize+" bytes";

                EmailService mail = new EmailService();
                mail.sendSecKey(email, seckey, name, doctitle, docowner, size);

                value = 1;

                RecordUsage rec = new RecordUsage();
                rec.record(userid, "askkey");                
            }
        }
        catch (Exception ex)
        {
            value = 0;
        }
        
        if (value == 2)
        {
            Response.Redirect("SecKeyAuthentication.aspx?docid=" + docid);
        }

        else if (value == 1)
        {
            message = "Secrete key generated successfully. Key is sent on your registered emailid.";
            title = "Success Report";
        }
        else if (value == 0)
        {
            message = "Sorry.!! Secrete key generation failed.. Try again..";
            title = "Failure Report";
        }

        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
        
        
    }
}