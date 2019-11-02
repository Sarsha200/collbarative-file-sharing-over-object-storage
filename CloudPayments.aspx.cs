using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CloudPayments : System.Web.UI.Page
{
    DataSet ds;
    string qr = "";
    ManageCloudPays objp = new ManageCloudPays();
    public Boolean flag = false;
    public string status = "";
    string message = "", title = "";    
    int value = 0;
    public string command = "";
    public string userid = "", utype = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            userid = Convert.ToString(Session["userid"]);
            utype = Convert.ToString(Session["utype"]);

            if (userid == "undefined" && (utype != "admin" || utype != "user"))
                Response.Redirect("Invalid.aspx");

            DisableAuto(Page.Controls);
        }
        catch (Exception ex) { }
    }

    public void FillGrid()
    {        
        try
        {
            string utype = Session["utype"].ToString();
            string userid = Session["userid"].ToString();
            status = Request.QueryString["status"];

            string[] date = txtdob.Text.Split('/');

            int mn = Convert.ToInt32(date[0]);
            int yr = Convert.ToInt32(date[1]);

            ds = new DataSet();
            ds.Clear();

            if (utype == "admin")
                ds = objp.GetPayments(mn, yr, status);
            else
                ds = objp.GetPayments(mn, yr, status, userid);

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

            dataresult.DataSource = pgd;
            dataresult.DataBind();

        }
        catch (Exception ex)
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

    protected void Submit(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex) { }
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