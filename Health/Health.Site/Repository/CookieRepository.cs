using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml.Serialization;
using Health.Core.API.Repository;
using Health.Core.Entities;

namespace Health.Site.Repository
{
    //TODO: ƒобавить поведение не случай если не разрешены cookie
    public class CookieRepository : IPermanentCredentialRepository
    {
        protected HttpResponse Response
        {
            get { return HttpContext.Current.Response; }
        }

        protected HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }

        #region IPermanentCredentialRepository Members

        public void Write(string identifier, UserCredential credential)
        {
            HttpCookie http_cookie = Request.Cookies[identifier];
            if (http_cookie != null && !http_cookie.Equals(null))
            {
                Response.Cookies.Add(new HttpCookie(identifier, Encrypt(Serialize(credential), credential.Login))
                                         {
                                             Expires = DateTime.Now.AddDays(14)
                                         });
            }
            else
            {
                Response.Cookies.Set(new HttpCookie(identifier, Encrypt(Serialize(credential), credential.Login))
                                         {
                                             Expires = DateTime.Now.AddDays(14)
                                         });
            }
        }

        public UserCredential Read(string identifier)
        {
            HttpCookie http_cookie = Request.Cookies[identifier];
            if (http_cookie != null)
            {
                return Deserialize(Decrypt(http_cookie.Value));
            }
            return null;
        }

        public void Clear()
        {
            string[] keys = Request.Cookies.AllKeys;
            foreach (string key in keys)
            {
                HttpCookie http_cookie = Response.Cookies[key];
                if (http_cookie != null)
                {
                    http_cookie.Value = null;
                    http_cookie.Expires = DateTime.Now.AddYears(-2);
                }
            }
        }

        #endregion

        protected string Encrypt(string data, string token)
        {
            var ticket = new FormsAuthenticationTicket(1, token, DateTime.Now, DateTime.Now.AddDays(14), true, data);
            string encrypt_ticket = FormsAuthentication.Encrypt(ticket);
            return encrypt_ticket;
        }

        protected string Decrypt(string data)
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(data);
            string user_data = ticket.UserData;
            return user_data;
        }

        protected string Serialize(UserCredential credential)
        {
            var memory_stream = new MemoryStream();
            var xml_serializer = new XmlSerializer(credential.GetType());
            xml_serializer.Serialize(memory_stream, credential);
            return Encoding.UTF8.GetString(memory_stream.ToArray());
        }

        protected UserCredential Deserialize(string data)
        {
            byte[] byte_data = Encoding.Default.GetBytes(data);
            var xml_serializer = new XmlSerializer(typeof (UserCredential));
            var memory_stream = new MemoryStream(byte_data);
            return (UserCredential) xml_serializer.Deserialize(memory_stream);
        }
    }
}