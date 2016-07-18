<%@ WebHandler Language="C#" Class="hnd_image" %>

using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using LibraryDAL;


public class hnd_image : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter  usr = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter ();
            
            string id = context.Request.QueryString["id"];
            MemoryStream memoryStream = new MemoryStream();
            dsLinkoffice.tblUserInfoRow  dr;
            byte[] image;

            dr = usr.GetUserByCode(id)[0];

            if (dr.IsUserImageNull())
            {

                string ImageFilePath = System.Web.HttpContext.Current.Server.MapPath("~/images/forum_old_2.gif");
                FileInfo _fileInfo = new FileInfo(ImageFilePath);
                long _NumBytes = _fileInfo.Length;
                FileStream _FStream = new FileStream(ImageFilePath, FileMode.Open, FileAccess.Read);

                BinaryReader _BinaryReader = new BinaryReader(_FStream);
                image = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes));

            }
            else
            {
                //image = dr.user_image;

                //added for no image use begin
                string ImageFilePath = System.Web.HttpContext.Current.Server.MapPath("~/images/forum_old_2.gif");
                FileInfo _fileInfo = new FileInfo(ImageFilePath);
                long _NumBytes = _fileInfo.Length;
                FileStream _FStream = new FileStream(ImageFilePath, FileMode.Open, FileAccess.Read);

                BinaryReader _BinaryReader = new BinaryReader(_FStream);
                image = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes));

                //added for no image use end
            }

            memoryStream.Write(image, 0, image.Length);
            context.Response.Buffer = true;
            context.Response.BinaryWrite(image);
            memoryStream.Dispose();
        }
        catch {
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}