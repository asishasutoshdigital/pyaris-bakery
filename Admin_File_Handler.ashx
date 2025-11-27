<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string fileName = string.Empty;
        int fileIncrement = 0;
        try
        {
            if (context.Request.Form["action"].ToString() == "Delete")
            {
                string FolderPath = context.Server.MapPath(Program.ImagePath);
                fileName = Path.GetFileName(context.Request.Form["File_Name"].ToString().Trim());

                FileInfo file = new FileInfo(FolderPath + fileName);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                    Responsemessage(context, "File deleted Successfully.");
                }
                else
                {
                    Responsemessage(context, "File does not exist");
                }
            }
            else
            {
                //Check if Request is to Upload the File.
                if (context.Request.Files.Count > 0)
                {
                    //Fetch the Uploaded File.
                    HttpPostedFile postedFile = context.Request.Files[0];
                    var fileinfo = new FileInfo(postedFile.FileName);
                    var fileExtention = fileinfo.Extension.ToLower();
                    if (fileExtention == ".jpg")
                    {
                        //Set the Folder Path.
                        string folderPath = context.Server.MapPath(Program.ImagePath);

                        //Set the File Name.
                        if (context.Request.Form["action"].ToString() == "update")
                        {
                            fileName = Path.GetFileName(context.Request.Form["File_Name"].ToString().Trim());
                        }
                        else
                        {
                            string[] filelist = Directory.GetFiles(context.Server.MapPath(Program.ImagePath), context.Request.Form["File_Name"].ToString().Trim() + "*.jpg");
                            if (filelist.Length > 0 && filelist.Length <= (int.Parse(Program.Display_Image_Max)-1))
                            {
                                fileName = Path.GetFileName(filelist[filelist.Length - 1]);
                                Regex regex = new Regex(@"\d+");
                                var num = regex.Match(fileName);
                                fileIncrement = Convert.ToInt16(num.Value) + 1;
                            }
                            else if(filelist.Length>=int.Parse(Program.Display_Image_Max))
                            {
                                Responsemessage(context, "Maximum of "+Program.Display_Image_Max+ " files can be uploaded ");
                            }
                            else
                            {
                                fileIncrement = 1;
                            }

                            fileName = context.Request.Form["File_Name"].ToString().Trim() + "-" + fileIncrement + ".jpg";
                        }
                        //Save the File in Folder.
                        postedFile.SaveAs(folderPath + fileName);

                        Responsemessage(context, "File uploaded Successfully.");
                    }
                    else
                    {
                        Responsemessage(context, "Please upload a valid jpg image.");
                    }
                }
                else
                {
                    Responsemessage(context, "Please upload a file.");
                }
            }
        }
        catch( System.Threading.ThreadAbortException )
        {

        }
        catch (Exception ex)
        {
            Log.Error("Issue with Product load : " + ex.Message, ex);
        }

    }
    public void Responsemessage(HttpContext context, string message)
    {
        string json = new JavaScriptSerializer().Serialize(new { Message = message });
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        context.Response.ContentType = "text/json";
        context.Response.Write(json);
        context.Response.Flush();
        context.Response.SuppressContent = true;
        context.Response.End();
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}