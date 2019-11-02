using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ViewServiceRents : System.Web.UI.Page
{
   
    public Boolean flag = false;
    CloudFuns obj = new CloudFuns();

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

            if (userid == "undefined" && (utype != "admin" || utype != "user"))
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
            DataSet ds = new DataSet();
            ds = obj.GetData("select * from servicecharges");

            if (ds.Tables[0].Rows.Count == 0)
            {
                flag = false;
            }
            else
                flag = true;

            datarents.DataSource = ds;
            datarents.DataBind();
        }
        catch (Exception ex) { }
    }
}