<%@ WebHandler Language="C#" Class="hndImage" %>

using System;
using System.IO;
using System.Web;
using LibraryDAL;

public class hndImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context)
    {
        try
        {
            clsSystem usr = new clsSystem();
            string id = context.Request.QueryString["id"];
            dsLinkoffice.tblUserInfoDataTable dt = new dsLinkoffice.tblUserInfoDataTable();
            dt = usr.GetUserById(id, current.CompanyCode);
            byte[] image;
            MemoryStream memoryStream = new MemoryStream();
            
            if(dt.Count>0)
            {
                if (!dt[0].IsUserImageNull())
                {
                    image = dt[0].UserImage;                    
                }
                else
                {
                    //image = dr.user_image;

                    //added for no image use begin
                    string ImageFilePath = System.Web.HttpContext.Current.Server.MapPath("~/images/forum_old.gif");
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


            
        }
        catch (Exception ex)
        {
        	
	        
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}