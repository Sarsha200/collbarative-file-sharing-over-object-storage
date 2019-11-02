using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AllotFilePermissions : System.Web.UI.Page
{
    int value = 0;
    
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
            string userid = Convert.ToString(Session["userid"]);
            int uploadid = Convert.ToInt32(Request.QueryString["docid"]);

            FriendsForUploads objf = new FriendsForUploads();
            ds = new DataSet();
            ds = objf.FillMembers(userid, uploadid);

            int count = ds.Tables[0].Rows.Count;

            if (count > 0)
                flag = true;
            else
            {
                flag = false;                
            }

            grdusers.DataSource = ds;
            grdusers.DataBind();
        }
        catch (Exception)
        {
            flag = false;            
        }
    }

    protected void Submit(object sender, EventArgs e)
    {
        try
        {            
            int uploadid = Convert.ToInt32(Request.QueryString["docid"]);
            ManageDocPermissions objperm = new ManageDocPermissions();


            foreach (GridViewRow row in grdusers.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("selectact");
                if (cb != null && cb.Checked)
                {
                    value = 0;
                    string memberid = grdusers.DataKeys[row.RowIndex].Value.ToString();

                    if (objperm.NewPermission(uploadid,memberid))
                        value = 1;

                    string userid = Convert.ToString(Session["userid"]);
                    RecordUsage rec = new RecordUsage();
                    rec.record(userid, "permission");
                    
                }
            }

            

        }
        catch (Exception ex) { value = 0; }
        if (value == 1)
        {
            message = "Document access permission allotted to members successfully..";
            title = "Success Report";
            FillGrid();
        }
        else if (value == 0)
        {
            message = "Permission allottment failed.. Try again..";
            title = "Failure Report";
        }

        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
    }

    protected void CheckAll_Click(object sender, EventArgs e)
    {
        ToggleCheckState(true);
    }
    protected void UncheckAll_Click(object sender, EventArgs e)
    {
        ToggleCheckState(false);
    }

    private void ToggleCheckState(bool checkState)
    {
        foreach (GridViewRow row in grdusers.Rows)
        {
            CheckBox cb = (CheckBox)row.FindControl("selectact");
            if (cb != null)
                cb.Checked = checkState;
        }
    }
}