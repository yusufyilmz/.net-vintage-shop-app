using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YSVintageShop.Models;

namespace YSVintageShop.Utils
{
    public class FileUploader
    {
        public static FileModel Upload(HttpPostedFileBase upload)
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new FileModel
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    return avatar;
                }

                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
          
        }
    }
}