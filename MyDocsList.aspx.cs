using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class MyDocsList : System.Web.UI.Page
{
    CloudFuns cfun = new CloudFuns();
    ManageDocuments obj = new ManageDocuments();
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
            userid = Convert.ToString(Session["userid"]);

            ds = new DataSet();
            ds = obj.GetMyDocsList(userid);

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
            else if (commandname == "permission")
            {
                value = 3;
            }
            else if (commandname == "viewpermissions")
            {
                value = 4;
            }
            else if (commandname == "delete")
            {
                
                GetDocumentDetails dtl = new GetDocumentDetails(docid);
                    
                // delete meta file
                string metafilePath = Server.MapPath("UserDocs/") + userid + "/"+ dtl.DocHashFile; ;
                
                try
                {
                    if (File.Exists(metafilePath))
                    {
                        FileStream fs = new FileStream(metafilePath, FileMode.Open, FileAccess.Read, FileShare.None);
                        fs.Close();
                        File.Delete(metafilePath);
                    }
                }
                catch (Exception ex) { }

                if (obj.DeleteDoc(docid))
                {
                    value = 1;
                }
                else
                    value = 0;
            }

        }
        catch(Exception ex)
        {
            value = 0;
        }


        if (value == 1)
        {
            message = "File removed successfully..";
            title = "Success Report";

            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
        }
        else if (value == 0)
        {
            message = "Sorry.!! File processing failed.. Try again..";
            title = "Failure Report";

            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
        }
        else if (value == 2)
        {
            Response.Redirect("SecKeyAuthentication.aspx?docid=" + docid);
        }
        else if (value == 3)
        {
            Response.Redirect("AllotFilePermissions.aspx?docid=" + docid);
        }
        else if (value == 4)
        {
            Response.Redirect("ViewPermissions.aspx?docid=" + docid);
        }
    }

}