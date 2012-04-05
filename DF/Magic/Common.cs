using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Data;
using Magic.Extensions;
namespace Magic
{
    public class Magically
    {
        public static void CopyPropertyValues<T>(ref T e1, T e2)
        {
            PropertyInfo[] properties = e1.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                    continue;

                //if (property.PropertyType.IsGenericType)
                //    continue;

                if (property.PropertyType.BaseType.Name.Contains("Entity") || property.PropertyType.Name.Contains("Entity"))
                    continue;

                e1.GetType().GetProperty(property.Name).SetValue(e1, e2.GetType().GetProperty(property.Name).GetValue(e2, null),null);
            }
        }


        /// <summary>
        /// Identifies and returns a string listing all changes between the two entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldE"></param>
        /// <param name="newE"></param>
        /// <param name="item"></param>
        /// <param name="itemChild"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string DetectChanges<T>(T oldE, T newE, string item, string itemChild, string property)
        {
            List<string> res = DetectChanges(oldE, newE,item,itemChild, new string[] {property});

            if (res != null && res.Count > 0)
                return res[0];
            else
                return string.Empty;
        }

        public static List<string> DetectChanges<T>(T oldE, T newE, string item, string itemChild, params string[] properties) 
        {
            List<string> result = new List<string>();
            if (!string.IsNullOrWhiteSpace(itemChild))
                itemChild = " " + itemChild;

            foreach (string p in properties)
            {
                if (newE.GetType().GetProperty(p).GetValue(newE, null).ToSafeString() != oldE.GetType().GetProperty(p).GetValue(oldE, null).ToSafeString())
                {
                    result.Add(string.Format("{0}{1} - {2} Updated From {3} to {4}",item,itemChild,p,oldE.GetType().GetProperty(p).GetValue(oldE, null),newE.GetType().GetProperty(p).GetValue(newE, null)));
                }
            }
            return result;
        }

        public static List<string> DetectChanges<T>(T oldE, T newE)
        {
            List<string> result = new List<string>();

            PropertyInfo[] properties = oldE.GetType().GetProperties();

            foreach (PropertyInfo p in properties)
            {
                if (p.PropertyType.Name.Contains("Entity") || p.PropertyType.BaseType.Name.Contains("Entity"))
                    continue;

                if (newE.GetType().GetProperty(p.Name).GetValue(newE, null).ToSafeString() != oldE.GetType().GetProperty(p.Name).GetValue(oldE, null).ToSafeString())
                {
                    result.Add(string.Format("{0} Updated From {1} To {2}", p.Name, oldE.GetType().GetProperty(p.Name).GetValue(oldE, null), newE.GetType().GetProperty(p.Name).GetValue(newE, null)));
                }
            }

            return result;
        }

        public static DataTable BuildTableFromEntity<T>(T entity)
        {
            string value = string.Empty;
            PropertyInfo[] properties = entity.GetType().GetProperties();

            List<string> cols = new List<string>();
            List<string> vals = new List<string>();


            //get columns and values
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                    continue;

                if (property.PropertyType.BaseType.Name.Contains("Entity") || property.PropertyType.Name.Contains("Entity"))
                    continue;

                cols.Add(typeof(T).Name + "." + property.Name);

                value = entity.GetType().GetProperty(property.Name).GetValue(entity, null).ToSafeString();
                value = value.Replace(" 12:00:00 AM", "");

                if (string.IsNullOrWhiteSpace(value))
                    vals.Add(string.Empty);
                else 
                    vals.Add(value);
            }

            //build and populate table
            DataTable dt =  BuildTable(cols, vals);
            dt.TableName = typeof(T).Name;

            return dt;
        }


        public static DataTable BuildTable(List<string> columns, List<string> values)
        {
            DataTable dt = new DataTable();
            foreach (string col in columns)
            {
                dt.Columns.Add(col);
            }

            DataRow row = dt.NewRow();

            for (int i = 0; i < values.Count; i++)
            {
                row[i] = values[i];
            }

            dt.Rows.Add(row);
            return dt;
        }

        public static string GetSafeString(object obj)
        {
            try
            {
                return obj.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        //I'm slow, don't use.
        public static T GetSafeT<T>(object obj)
        {
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }


        //http://stackoverflow.com/questions/321370/convert-hex-string-to-byte-array
        public static byte[] HexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => 0 == x % 2).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        //http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa-in-c
        public static string ByteArrayToHex(byte[] bytes)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                result.AppendFormat("{0:x2}", b);

            return result.ToString();
        }

        public static Guid GetGuidFromQueryString(string name, bool required)
        {
            string value = HttpContext.Current.Request.QueryString[name];
            if (value == null || value.Length == 0)
            {
                if (required)
                {
                    return Guid.Empty;
                    //throw new InvalidQueryStringException(name, "Value must be specified.");
                }
                else
                {
                    return Guid.Empty;
                }
            }
            else // value specified
            {
                try
                {
                    return new Guid(value);
                }
                catch
                {
                    return Guid.Empty;
                    //throw new InvalidQueryStringException(name, value, "Failed to parse value.");
                }
            }
        }

        public enum DocTypes
        {
            PDF,
            DOC,
            DOCX
        }

        public static void OpenDocument(string path, DocTypes type, string displayName)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                HttpResponse response = System.Web.HttpContext.Current.Response;

                response.AddHeader("Content-Type", "binary/octet-stream");
                response.AddHeader("Content-Disposition",
                           string.Format("attachment; filename={0};",displayName));
                response.WriteFile(path);
            }
        }

        public static void OpenDocument(System.IO.Stream stream, string mimeType, string displayName)
        {
            byte[] content = new byte[stream.Length];

            stream.Position = 0;
            stream.Read(content, 0, content.Length);
         
            OpenDocument(content, mimeType, displayName);
        }

        public static void OpenDocument(byte[] content, string mimeType, string displayName)
        {
            if (content != null)
            {
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ContentType = mimeType;
                response.AppendHeader("Content-Disposition",
                           string.Format("attachment; filename={0}; size={1}", displayName, content.Length));
                response.BinaryWrite(content);
                //response.End();
            }
        }

        public static System.IO.Stream GetFileStream(string path)
        {
            return null;
        }

        public static string SetRelativePath(string path)
        {
            if (path.LastIndexOf('/') != path.Length - 1)
                path = path + '/';

            if (path.IndexOf('/') > 0)
                path = '/' + path;

            if (path.IndexOf('~') < 0)
                path = '~' + path;

            //HttpServerUtility server = System.Web.HttpContext.Current.Server;
            path = System.Web.Hosting.HostingEnvironment.MapPath(path);

            return path;
        }

        public static void SaveDocument(string path, string fileName, HttpPostedFile file)
        {
            path = SetRelativePath(path);

            if (fileName.IndexOf('/') == 0)
                fileName = fileName.Substring(1);

            string fullPath = string.Format("{0}{1}", path, fileName);

            file.SaveAs(fullPath);
        }

        public static void SaveDocument(string path, string fileName, HttpPostedFileBase file)
        {
            path = SetRelativePath(path);

            if (fileName.IndexOf('/') == 0)
                fileName = fileName.Substring(1);

            string fullPath = string.Format("{0}{1}", path, fileName);

            file.SaveAs(fullPath);
        }

        public static string GetFileExtension(string fileName)
        {
            return System.IO.Path.GetExtension(fileName);
        }

        public static string GetTextFromFile(string path, string fileName)
        {
            string result = string.Empty;
            path = SetRelativePath(path);
            if (fileName.IndexOf('/') == 0)
                fileName = fileName.Substring(1);

            string fullPath = string.Format("{0}{1}", path, fileName);

            if (!System.IO.File.Exists(fullPath))
                return string.Empty;

            using (System.IO.StreamReader reader = new System.IO.StreamReader(fullPath))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        public static bool FileExists(string path)
        {
            path = SetRelativePath(path).TrimEnd('\\');

            return System.IO.File.Exists(path);
        }

        public static bool FileExists(string path, string fileName)
        {
            path = SetRelativePath(path);
            if (fileName.IndexOf('/') == 0)
                fileName = fileName.Substring(1);

            return System.IO.File.Exists(path + fileName);
        }

        public static void SendEmailMessage(string to, string from, string subject, string body, System.Net.Mail.MailPriority priority)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from, to, subject, body);
            msg.Priority = priority;
            SendEmailMessage(msg);
        }

        public static void SendEmailMessage(string to, string from, string subject, string body)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from, to, subject, body);
            SendEmailMessage(msg);
        }

        public static void SendEmailMessage(string to, string from, string subject, string body, System.Net.Mail.MailPriority priority,System.IO.Stream attachment)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from, to, subject, body);
            msg.Attachments.Add(new System.Net.Mail.Attachment(attachment, string.Empty));
            msg.Priority = priority;
            SendEmailMessage(msg);
        }

        public static void SendEmailMessage(System.Net.Mail.MailMessage msg)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.wrberkley.com");
            msg.IsBodyHtml = true;
            client.Send(msg);
        }

        public static void SendEmailMessage(System.Net.Mail.MailMessage msg, bool isHtml)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.wrberkley.com");
            msg.IsBodyHtml = isHtml;
            client.Send(msg);
        }

        public static void SendEmailMessageWithLogo(string to, string from, string subject, string body, string filePath)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from, to, subject, body);

            System.Net.Mail.AlternateView content = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body,null, System.Net.Mime.MediaTypeNames.Text.Html);
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(filePath);

            System.Net.Mail.LinkedResource logo = new System.Net.Mail.LinkedResource(imagePath, System.Net.Mime.MediaTypeNames.Image.Gif);
            logo.ContentId = "logo";
            content.LinkedResources.Add(logo);

            imagePath = System.Web.HttpContext.Current.Server.MapPath("/library/images/wrberkley_logo.png");
            System.Net.Mail.LinkedResource wrb = new System.Net.Mail.LinkedResource(imagePath);
            wrb.ContentId = "wrberkley";
            content.LinkedResources.Add(wrb);

            msg.AlternateViews.Add(content);
            
            SendEmailMessage(msg);
        }
    }
}
