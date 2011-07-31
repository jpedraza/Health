using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml.Serialization;
using Health.API.Entities;
using Health.API.Repository;

namespace Health.Site.Repository
{
    //TODO: ƒобавить поведение не случай если не разрешены cookie
    public class CookieDataAccessor : IPermanentCredentialRepository
    {
        protected HttpResponse Response
        {
            get { return HttpContext.Current.Response; }
        }

        protected HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }
        
        public void Write(string identifier, IUserCredential credential)
        {
            if (!Request.Cookies[identifier].Equals(null))
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

        public IUserCredential Read(string identifier)
        {
            if (Request.Cookies[identifier] != null)
            {
                return Deserialize(Decrypt(Request.Cookies[identifier].Value));
            }
            return null;
        }

        public void Clear()
        {
            string[] keys = Request.Cookies.AllKeys;
            foreach (string key in keys)
            {
                Response.Cookies[key].Value = null;
                Response.Cookies[key].Expires = DateTime.Now.AddYears(-2);
            }
        }

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

        protected string Serialize(IUserCredential credential)
        {
            var memory_stream = new MemoryStream();
            var xml_serializer = new XmlSerializer(credential.GetType());
            xml_serializer.Serialize(memory_stream, credential);
            return Encoding.UTF8.GetString(memory_stream.ToArray());
        }

        protected IUserCredential Deserialize(string data)
        {
            byte[] byte_data = Encoding.Default.GetBytes(data);
            var xml_serializer = new XmlSerializer(typeof(IUserCredential));
            var memory_stream = new MemoryStream(byte_data);
            return (IUserCredential) xml_serializer.Deserialize(memory_stream);
        }
    }
}