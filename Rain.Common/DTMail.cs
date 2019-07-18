using System.Net;
using System.Net.Mail;
using System.Text;

namespace Rain.Common
{
  public class DTMail
  {
    public static void sendMail(
      string smtpserver,
      int enablessl,
      string userName,
      string pwd,
      string nickName,
      string strfrom,
      string strto,
      string subj,
      string bodys)
    {
      SmtpClient smtpClient = new SmtpClient();
      smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
      smtpClient.Host = smtpserver;
      smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential(userName, pwd);
      if (enablessl == 1)
        smtpClient.EnableSsl = true;
      smtpClient.Send(new MailMessage(new MailAddress(strfrom, nickName), new MailAddress(strto))
      {
        Subject = subj,
        Body = bodys,
        BodyEncoding = Encoding.Default,
        IsBodyHtml = true,
        Priority = MailPriority.Normal
      });
    }
  }
}
