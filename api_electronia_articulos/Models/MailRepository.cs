using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace api_electronia_articulos.Models
{
    public class MailRepository:IMail
    {
        public MailRepository()
        {
        }
        public string Get(string data)
        {
            return "ok"+data; 
        }

        public HttpResponseMessage  Add(Mail item)
        {
            string destinationAddress = item.emailDestino;
            string subject = item.subject;
            string mailAddress = ConfigurationSettings.AppSettings.Get("mailCredentialesMail").ToString(); //"mail.electronia@gmail.com";
            string mailPassword = ConfigurationSettings.AppSettings.Get("mailCredentialesPass").ToString(); //"electronia.com.mx";
            string mailHost = ConfigurationSettings.AppSettings.Get("mailHost").ToString(); //"smtp.gmail.com";
            int mailPort = int.Parse(ConfigurationSettings.AppSettings.Get("mailPort").ToString()); //587;
            // van a existir muchas cosas que dependan del servicio enviado

            string body = item.body;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(destinationAddress);
            msg.From = new MailAddress(mailAddress, mailPassword, System.Text.Encoding.UTF8);
            msg.Subject = subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;

            // Configuración SMTP
            SmtpClient mailSmtp = new SmtpClient();
            mailSmtp.Host = mailHost;
            mailSmtp.Credentials = new System.Net.NetworkCredential(mailAddress, mailPassword);
            mailSmtp.EnableSsl = true;
            mailSmtp.Port = mailPort;
            var response = new HttpResponseMessage(HttpStatusCode.Created); ;
            try
            {
                mailSmtp.Send(msg);
            }
            catch
            {
                response  = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
           
           return response;
        }
        
    }
}