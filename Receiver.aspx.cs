using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Receiver : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (string f in Request.Files.AllKeys)
        {
            HttpPostedFile file = Request.Files[f];
            string filepath = Server.MapPath("UserDocs/") + Request.QueryString["path"];
            Response.Write(filepath);
            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);

            file.SaveAs(filepath + "/" + file.FileName);
        }
    }
}