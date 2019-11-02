using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Collections;
using System.Data;

public partial class UploadDoc : System.Web.UI.Page
{
    string message = "", title = "";
    public Boolean flag = false;
    int value = 0;

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

            DisableAuto(Page.Controls);
        }
        catch (Exception ex) { }
    }

    protected void Submit(object sender, EventArgs e)
    {
        CloudFuns obj = new CloudFuns();
        int docid = obj.FetchMax("uploads", "uploadid");
        string userid = Convert.ToString(Session["userid"]);
        
        string newname = "NA", encfilename = "", filePath = "",metaxml="";
        string input = "", output = "", seckey = "", ext = "";
        
        long sizebytes = 0;

        int dd = DateTime.Now.Day;
        int mm = DateTime.Now.Month;
        int yy = DateTime.Now.Year;
        string today = dd + "-" + mm + "-" + yy;

        int hr = DateTime.Now.Hour;
        string now = hr + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        try
        { 

            if (filedocument.HasFile)
            {
                string filename = filedocument.FileName;
                ext = System.IO.Path.GetExtension(filename);
                filePath = Server.MapPath("UserDocs/") + userid + "/";

                metaxml=docid+"_xml.xml";

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                newname = "doc" + docid + ext;

                //---------------------------------
                input = filePath + "/" + newname;
                encfilename = "doc" + docid + "_enc" + ext;
                output = filePath + "/" + encfilename;

                if (File.Exists(input))
                    File.Delete(input);
                // save file for encryption
                filedocument.SaveAs(input);

                // Calculate SHA Hash of the original document
                SHATracker track = new SHATracker();
                string shavalue = track.GetSHA1Hash(input);

                FileInfo f = new FileInfo(input);
                sizebytes = f.Length;

                // Generate Keys

                Random r = new Random();
                seckey = "SEC" + docid + "@" + r.Next(111, 999);

                string dockey = docid + newname + "|D@C_" + now.Replace(":", "");

                // read all bytes from uploaded file
                byte[] file = System.IO.File.ReadAllBytes(input);

                // reverse and store file bytes into another byte array
                int filelength = file.Length;

                int index = filelength - 1;
                byte[] rev_file = new byte[filelength];
                foreach(byte b in file)
                {
                    rev_file[index] = b;
                    index--;                    
                }

                // create a file from reversed bytes and store on local server 
                string rev_file_path = filePath + "/rev_file"+docid + ext;

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

                // encrypt the file

                if (File.Exists(output))
                    File.Delete(output);

                Cryptography cobj = new Cryptography();
                cobj.EncryptAES(rev_file_path, output, dockey);
                
                // Store data in DB
                ArrayList data = new ArrayList();
                data.Clear();
                data.Add(docid);
                data.Add(txttitle.Text);
                data.Add(sizebytes);
                data.Add(txtdesc.Text);
                data.Add(userid);
                data.Add(metaxml);
                data.Add(dockey);
                data.Add(seckey);
                data.Add(today);
                data.Add(now);
                data.Add(encfilename);


                ManageDocuments dobj = new ManageDocuments();

                if (dobj.NewDoc(data))
                { 
                    // create metadata XML file and store on local server
                    DataSet dsxml = new DataSet();
                    dsxml.Tables.Add();

                    dsxml.Tables[0].Columns.Add();
                    dsxml.Tables[0].Columns[0].ColumnName = "dochash";
                    dsxml.Tables[0].Columns.Add();
                    dsxml.Tables[0].Columns[1].ColumnName = "uploaddt";
                    dsxml.Tables[0].Columns.Add();
                    dsxml.Tables[0].Columns[2].ColumnName = "uploadtime";
                    dsxml.Tables[0].Columns.Add();
                    dsxml.Tables[0].Columns[3].ColumnName = "docid";

                    dsxml.Tables[0].Rows.Add();
                    dsxml.Tables[0].Rows[0][0] = shavalue;
                    dsxml.Tables[0].Rows[0][1] = today;
                    dsxml.Tables[0].Rows[0][2] = now;
                    dsxml.Tables[0].Rows[0][3] = docid;

                    if (File.Exists(filePath + metaxml))
                        File.Delete(filePath + metaxml);

                    dsxml.WriteXml(filePath + metaxml);

                    // store file on storage and backup server
                    BackupHost info = new BackupHost();
                    WebClient oclient = new WebClient();
                    byte[] responseArray = oclient.UploadFile("http://" + info.StorageHostName + ":" + info.StorageHostPort + "/Receiver.aspx?path=" + userid, "POST", output);
                    byte[] responseArray1 = oclient.UploadFile("http://" + info.BackupHostName + ":" + info.BackupHostPort + "/Receiver.aspx?path=" + userid, "POST", output);

                    

                    value = 1;

                    if (File.Exists(input))
                        File.Delete(input);

                    if (File.Exists(output))
                        File.Delete(output);

                    try
                    {                        
                        if (File.Exists(rev_file_path))
                        {
                            FileStream fs = null;
                            try
                            {                            
                                fs = new FileStream(rev_file_path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                                fs.Close();
                            }
                            catch (Exception ex) { }
                            File.Delete(rev_file_path);
                        }
                    }
                    catch (Exception ex) { }


                    GetClientDetails dtl = new GetClientDetails(userid);
                    string name = dtl.ClientName;
                    string email = dtl.ClientEmailId;

                    EmailService mail = new EmailService();
                    mail.sendSecKey(email, seckey, name, txttitle.Text);

                    RecordUsage rec = new RecordUsage();
                    rec.record(userid, "upload");                    
                    rec.record(userid, "encryption");
                }
                else
                    value = 0;
            }
        }
        catch (Exception ex) { value = 0; }

        if (value == 1)
        {
            message = "File uploaded successfully..";
            title = "Success Report";
        }
        else if (value == 0)
        {
            message = "Sorry.!! File upload failed.. Try again..";
            title = "Failure Report";
        }
        
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), title, script, true);
        ClearInputs(Page.Controls);
    }

    private void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();

            ClearInputs(ctrl.Controls);
        }
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