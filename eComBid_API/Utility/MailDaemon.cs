using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eComBid_API.Utility
{
    public class MailDaemon
    {
        private static string siteName = ConfigurationManager.AppSettings["SiteName"];
        private static bool isEmailDisabled = Convert.ToInt32(ConfigurationManager.AppSettings["EmailDisabled"]) == 1;
        private static string emailDomain = ConfigurationManager.AppSettings["EmailDomain"];
        private static string domain = ConfigurationManager.AppSettings["Domain"];
        private static string https = ConfigurationManager.AppSettings["https"];

        private static SmtpClient MailClient = new SmtpClient();

        private static void setMailClientInfo()
        {
            MailClient.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
        }

        private static string GetDomain()
        {
            string aDomain = MailDaemon.domain.Trim();
            string aHttps = MailDaemon.https;
            if (string.IsNullOrEmpty(aDomain))
            {
                throw new ArgumentNullException("Domain entries in the web.config is null.  Please make sure they are not empty.");
            }
            if (aHttps == "on")
                aDomain = "https://" + domain;
            else
                aDomain = "http://" + domain;
            return aDomain;
        }

        private static void GetAndCheckEmailDomainAndSiteName(out string aEmailDomain, out string aSiteName)
        {
            string testEmailDomain = MailDaemon.emailDomain.Trim();
            string testSiteName = MailDaemon.siteName.Trim();

            if (string.IsNullOrEmpty(testEmailDomain) || string.IsNullOrEmpty(testSiteName))
            {
                aEmailDomain = null;
                aSiteName = null;
                throw new ArgumentNullException("Either the emailDomain or the siteName entries in the web.config is null.  Please make sure they are not empty.");
            }

            aEmailDomain = testEmailDomain;
            aSiteName = testSiteName;
        }

        private static MailMessage GetNewMailMessage(string emailDomain, string siteName, string custEmail, string custName)
        {
            MailAddress source = new MailAddress("support@" + emailDomain, siteName);
            MailAddress recipient = new MailAddress(custEmail, custName);

            MailMessage msg = new MailMessage();

            msg.From = source;
            msg.To.Add(recipient);
            msg.IsBodyHtml = true;
            return msg;
        }

        public static void SendTemporaryPasswordEmail(string custName, string custEmail, string custUsername, string custPassword)
        {
            MailDaemon.setMailClientInfo();

            bool isEmailDisabled = MailDaemon.isEmailDisabled;
            if (isEmailDisabled)
                return;

            string siteName;
            string emailDomain;

            GetAndCheckEmailDomainAndSiteName(out emailDomain, out siteName);

            string aDomain = MailDaemon.GetDomain();

            MailMessage msg = MailDaemon.GetNewMailMessage(emailDomain, siteName, custEmail, custName);

            msg.Subject = "Your Temporary Password";
            msg.Body = "<html><body>" + siteName +
                "<br><br>Dear " + custName + ",<br><br>" +

                "Your temporary password is: " + custPassword +
                "<br><br>Click on the following link to reset your password: <a href='" + aDomain + "/AdminVSPTempPassword.aspx?username=" + custUsername + "' >" + aDomain + "/AdminVSPTempPassword.aspx?username=" + custUsername + "</a>" +
                "<br>When logging in with your temporary password, you will be prompted " +
                "to change your password.<br>" +
                "***Important: This temporary password will expire in 24 hours.***<br>" +
                "If you did not request this temporary password, please contact us<br>" +
                "immediately at  855-999-otto (6886) so that a representative can<br>" +
                "assist you further. A request not made by you could indicate<br>" +
                "attempted identity theft or other suspicious activity.<br><br>" +
                "Please do not respond to this automated message. Emails sent to<br>" +
                "this address are not monitored.<br><br>" +
                "Thank you,<br>" +
                "otto support team</body></html>";

            MailDaemon.MailClient.Send(msg);
        }

        public static void SendUserNameEmail(string custName, string custEmail, string custUserName)
        {
            MailDaemon.setMailClientInfo();

            bool isEmailDisabled = MailDaemon.isEmailDisabled;
            if (isEmailDisabled)
                return;

            string siteName;
            string emailDomain;

            GetAndCheckEmailDomainAndSiteName(out emailDomain, out siteName);

            string aDomain = MailDaemon.GetDomain();

            MailMessage msg = MailDaemon.GetNewMailMessage(emailDomain, siteName, custEmail, custName);

            msg.Subject = "Your user name";
            msg.Body = "<html><body>" + siteName +
                "<br><br>Dear " + custName + ",<br><br>" +
                "Your user name is " + custUserName +
                "<br>Click on the following link to log in: <a href='" + aDomain + "/ManageOtto.aspx'>" + aDomain + "/ManageOtto.aspx" + "</a>" +
                "<br><br><br>If you did not request your user ID, please contact us immediately at" +
                "<br>855-999-otto (6886) so that a representative can assist you further. A" +
                "<br>request not made by you could indicate an attempted identity theft or" +
                "<br>other suspicious activity." +
                "<br><br>Please do not respond to this automated message. Emails sent to" +
                "<br>this address are not monitored.<br>" +
                "Thank you," +
                "<br>otto support team</body></html>";

            MailDaemon.MailClient.Send(msg);
        }
    }
}
