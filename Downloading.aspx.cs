using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Data;

public partial class Downloading : System.Web.UI.Page
{
    string userid = "", utype = "";
    public Global gobj = new Global();
    
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
            int docid = Convert.ToInt32(Request.QueryString["docid"]);
            

            GetDocumentDetails dtl = new GetDocumentDetails(docid);

            string docname = dtl.DocFileName;
            string docowner = dtl.DocOwner;
            
            string filePath = Server.MapPath("UserDocs/") + docowner + "/";
            

            string newpath = "/UserDocs/" + docowner;

            BackupHost info = new BackupHost();
            string remotepath = "http://" + info.StorageHostName + ":" + info.StorageHostPort + newpath;
            string backuppath= "http://" + info.BackupHostName + ":" + info.BackupHostPort + newpath;

            if (FileToDownload(remotepath, docname, filePath, docowner))
            {
                Global.DocDownloadPath = "UserDocs/" + docowner + "/" + docname;
               // Response.Write(Global.DocDownloadPath);
                Panel1.Visible = true;
                Panel2.Visible = false;

                string userid = Convert.ToString(Session["userid"]);
                RecordUsage rec = new RecordUsage();
                rec.record(userid, "download");
                rec.record(userid, "decryption");
                
            }
            else if (FileToDownload(backuppath, docname, filePath, docowner))
            {
                //Global.DocDownloadPath = filePath  + docname;
                Global.DocDownloadPath = "UserDocs/" + docowner + "/" + docname;
               // Response.Write(Global.DocDownloadPath);
                Panel1.Visible = true;
                Panel2.Visible = false;

                string userid = Convert.ToString(Session["userid"]);
                RecordUsage rec = new RecordUsage();
                rec.record(userid, "download");
                rec.record(userid, "decryption");
                rec.record(userid, "recovery");
            }
            else
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
            }
            
            // else recover the file from backup server
            // calculate its hash after decryption and allow it to download
        }
        catch (Exception ex) { }
    }

    protected Boolean FileToDownload(string remotepath,string docname,string filePath,string docowner)
    {
        Boolean flag = false;
        try
        {
            int docid = Convert.ToInt32(Request.QueryString["docid"]);
            string userkey = Convert.ToString(Session["seckey"]);

            DocKeysGenerator gen = new DocKeysGenerator();
            gen.GetDocSecreteKey(docid, userkey);
            string docseckey = gen.DocSecreteKey;

            string outfilepath = Server.MapPath("UserDocs/") + docowner + "/temp/";
            if (!System.IO.Directory.Exists(outfilepath))
            {
                System.IO.Directory.CreateDirectory(outfilepath);
            }

            WebClient oclient = new WebClient();
            oclient.DownloadFile(remotepath + "/" + docname, outfilepath + "/" + docname);

            string decfile = outfilepath + "dec_" + docname;

            Cryptography cobj = new Cryptography();
            cobj.DecryptAES(outfilepath + "/" + docname, decfile, docseckey);

            byte[] myfile = System.IO.File.ReadAllBytes(decfile);
            // reverse and store file bytes into another byte array
            int filelength = myfile.Length;

            int index = filelength - 1;
            byte[] rev_file = new byte[filelength];
            foreach (byte b in myfile)
            {
                rev_file[index] = b;
                index--;
            }

            // create a file from reversed bytes and store on local server 
            string rev_file_path = filePath + "/" + docname;

            try
            {
                if (File.Exists(rev_file_path))
                {
                    FileStream fs = new FileStream(rev_file_path, FileMode.Open, FileAccess.Read, FileShare.None);
                    fs.Close();
                    File.Delete(rev_file_path);
                }
            }
            catch (Exception ex) { }

            File.WriteAllBytes(rev_file_path, rev_file);
            // file is saved on server

            // check its hash value
            SHATracker track = new SHATracker();
            string shavalue = track.GetSHA1Hash(rev_file_path);

            // compare with hash value from xml file
            string metaxml = filePath + "/" + docid + "_xml.xml";
            DataSet dsxml = new DataSet();
            dsxml.ReadXml(metaxml);

            string original_hash = Convert.ToString(dsxml.Tables[0].Rows[0]["dochash"]);

            // if matches let it get downloaded

            if (shavalue == original_hash)
            {
                flag = true;
              //  Global.DocDownloadPath = rev_file_path;
            }

        }
        catch (Exception ex) { }
        return flag;
    }

    
}