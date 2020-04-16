using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Timers;
using System.Net.Mail;
using Cartelux1.Models;
using System.Net;

namespace Cartelux1.Global_Objects
{
    public static class AlarmsThread
    {
        public static void checkForTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            SendAlarma_Email();
            //if (timeIsReady())
            {
                //SendEmail();
            }
        }

        private static void SendAlarma_Email()
        {
            // SOURCE: https://www.smarterasp.net/support/kb/a179/how-to-send-email-in-asp_net.aspx
            //create the mail message 
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;

            string email_emisor = "notificaciones@cartelux.uy";
            string password_emisor = "Cartelux1234$";
            List<string> email_receptor = new List<string>();
            List<string> email_receptor_entrega = new List<string>();
            using (CarteluxDB context = new CarteluxDB())
            {
                List<config_emails> lista_config_emails = (List<config_emails>)context.config_emails.ToList();
                if (lista_config_emails != null && lista_config_emails.Count > 0)
                {
                    foreach (config_emails _config_emails in lista_config_emails)
                    {
                        if (_config_emails.EsEmisor)
                        {
                            email_emisor = _config_emails.Email;
                            password_emisor = _config_emails.Clave;
                        }
                        else
                        {
                            if (_config_emails.EstaActivo && _config_emails.AlarmasOK)
                            {
                                //email_receptor_entrega.Add(_config_emails.Email);
                                mail.CC.Add(_config_emails.Email);
                            }
                        }
                    }
                }
            }

            //set the addresses 
            mail.From = new MailAddress(email_emisor); //IMPORTANT: This must be same as your smtp authentication address.
            foreach (string email in email_receptor)
            {
                int index = email_receptor.IndexOf(email);
                if (index == 0)
                {
                    mail.To.Add(email);
                }
                else
                {
                    mail.CC.Add(email);
                }
            }


            //set the content 
            mail.Subject = "CX-ALARMA: Alarma diaria";
            mail.Body += "<div>Este es un email auto-generado, por favor no lo responda.</div>";
            mail.Body += "<div>Fecha creación: " + GlobalVariables.GetCurrentTime().ToString(GlobalVariables.ShortDateTime_format1_long) + "</div>";
            /*
            mail.Subject = "CX-ALARMA: Alarma diaria" + nombre + " > " + day_name.Substring(0, 3) + " " + fecha_entrega;
            mail.Body = "<div><strong>Información básica del pedido nuevo.</strong></div>";
            mail.Body += "<br/><div><strong>Nombre:</strong> " + nombre + "</div>";
            mail.Body += "<div><strong>Teléfono:</strong> " + telefono + "</div>";
            mail.Body += "<div><strong>Tipo:</strong> " + tipo + "</div>";
            mail.Body += "<div><strong>Temática:</strong> " + tematica + "</div>";
            mail.Body += "<div><strong>Entrega:</strong> " + fecha_entrega + "</div>";
            mail.Body += "<br/><br/>";
            mail.Body += "<div><font size='2'><strong><span style='color:#e15211'>------------------------------<wbr>------------------------------<wbr>-------------</span></strong></font></div>";
            mail.Body += "<div><strong><span style='font-size:12pt;color:#e15211'>Accesos</span></strong></div>";
            mail.Body += "<div><strong><a href='" + url + "' title='' target='_blank'>Formulario</a></strong></div>";
            mail.Body += "<div><strong><a href='www.pedidos.cartelux.uy/Dashboard' title='' target='_blank'>Dashboard</a></strong></div>";
            mail.Body += "<div><strong><a href='" + wpp_url + "' title='' target='_blank'>WhatsApp URL</a></strong></div>";
            mail.Body += "<br/><br/>";
            mail.Body += "<div>Este es un email auto-generado, por favor no lo responda.</div>";
            mail.Body += "<div>Fecha creación: " + GetCurrentTime().ToString(GlobalVariables.ShortDateTime_format1_long) + "</div>";
            */
            string sign_1 = "<br/><div><font size='2'><strong><span style='color:#e15211'>------------------------------<wbr>------------------------------<wbr>-------------</span></strong></font></div>";
            string sign_2 = "<div><strong><span style='font-size:8pt;color:#e15211'>Cartelux Publicidad 2018</span></strong><strong><span style='color:#4a442a'> | </span></strong><strong><span style='FONT-SIZE:8pt;'>www.cartelux.uy</span></strong></div>";
            string sign_3 = "<div><font size='2'><strong><span style='color:#e15211'>------------------------------<wbr>------------------------------<wbr>-------------</span></strong></font></div>";
            mail.Body += sign_1 + sign_2 + sign_3;

            //send the message 
            SmtpClient smtp = new SmtpClient("mail.cartelux.uy");

            //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
            NetworkCredential Credentials = new NetworkCredential(email_emisor, password_emisor);
            smtp.Credentials = Credentials;
            smtp.Send(mail);
        }


    }
}