using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CloudUsageReport : System.Web.UI.Page
{
    DataSet ds;
    int upload = 0, download = 0, encryption = 0, decryption = 0, login = 0, recovery = 0, permission = 0, askkey = 0;
    double spacebytes = 0, spacekb = 0, spacemb = 0;
    double spacech=0, uploadch = 0, downloadch = 0, encryptionch = 0, decryptionch = 0, loginch = 0, recoverych = 0, permissionch = 0, askkeych = 0;
    CloudFuns obj = new CloudFuns();
    string qr = "";
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

            if (userid == "undefined" && (utype != "admin" || utype != "user"))
                Response.Redirect("Invalid.aspx");

            DisableAuto(Page.Controls);
        }
        catch (Exception ex) { }
    }

   
    public void FillGrid()
    {
        string username = "", userid = "";
        int totallogins = 0;
        try
        {
            string utype = Session["utype"].ToString();

            string[] date = txtmonth.Text.Split('/');

            int mn = Convert.ToInt32(date[0]);
            int yr = Convert.ToInt32(date[1]);

            ds = new DataSet();
            ds.Clear();
            if (utype == "admin")
                qr = "select userid,usernm from users where usertype='user' and userstatus='active'";
            else
                qr = "select userid,usernm from users where userid='" + Session["userid"].ToString() + "'";
            ds = obj.GetData(qr);

            ds.Tables[0].Columns[0].ColumnName = "User_Name";
            ds.Tables[0].Columns[1].ColumnName = "Logins";
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns[2].ColumnName = "Uploads";
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns[3].ColumnName = "Downloads";
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns[4].ColumnName = "Encryption";
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns[5].ColumnName = "Decryption";
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns[6].ColumnName = "Permission";
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns[7].ColumnName = "Askkey";
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns[8].ColumnName = "Recovery";
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns[9].ColumnName = "Space";
            
            int rows = ds.Tables[0].Rows.Count;

            int cnt = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cnt++;
                userid = ds.Tables[0].Rows[i][0].ToString();
                username = ds.Tables[0].Rows[i][1].ToString();

                LoginLog ul = new LoginLog();
                totallogins = ul.getLoginCount(userid, mn, yr);

                ServiceTracker sl = new ServiceTracker(userid, mn, yr);
                
                upload = sl.Upload;
                download = sl.Download;
                encryption = sl.Encryption;
                decryption = sl.Decryption;
                spacebytes = sl.SpaceBytes;
                spacekb = sl.SpaceKB;
                spacemb = sl.SpaceMB;
                permission = sl.Permission;
                askkey = sl.Askkey;
                recovery = sl.Recovery;


                ServiceDetails charge = new ServiceDetails();
                uploadch = charge.UploadCharge;
                downloadch = charge.DownloadCharge;
                askkeych = charge.AskkeyCharge;
                permissionch = charge.PermissionCharge;
                loginch = charge.LoginCharge;
                spacech = charge.SpaceCharge;
                encryptionch = charge.EncryptionCharge;
                decryptionch = charge.DecryptionCharge;
                recoverych = charge.RecoveryCharge;

                double usagerent = 0.0;
                usagerent = (totallogins * loginch) + (upload * uploadch) + (download * downloadch)+ (permission * permissionch)*(spacemb*spacech);
                usagerent += (askkey * askkeych)+(encryption*encryptionch)+(decryption*decryptionch)+(recovery*recoverych);

                ds.Tables[0].Rows[i][0] = username;
                ds.Tables[0].Rows[i][1] = totallogins;

                ds.Tables[0].Rows[i][2] = upload;

                ds.Tables[0].Rows[i][3] = download;
                ds.Tables[0].Rows[i][4] = encryption;
                ds.Tables[0].Rows[i][5] = decryption;
                ds.Tables[0].Rows[i][6] = permission;
                ds.Tables[0].Rows[i][7] = askkey;
                ds.Tables[0].Rows[i][8] = recovery;
                ds.Tables[0].Rows[i][9] = spacekb + " KB   " + spacemb + " MB";

                ManagePayments objpay = new ManagePayments(userid, Math.Round(usagerent));
            }

            int count = ds.Tables[0].Rows.Count;

            if (count == 0)
            {
                flag = false;
                LinkButton2.Visible = false;
                LinkButton3.Visible = false;
            }
            else
                flag = true;

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

            datausage.DataSource = pgd;
            datausage.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void Submit(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex) { }
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