using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;


namespace PortalFornecedor.Noventa.Application.Services.Util
{
    public static class Utils
    {
        public static string Criptografar(string password)
        {
            TripleDESCryptoServiceProvider tripledescryptoserviceprovider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5cryptoserviceprovider = new MD5CryptoServiceProvider();

            string myKey = "1111111111111111";  //Aqui vc inclui uma chave qualquer para servir de base para cifrar, que deve ser a mesma no método Decodificar
            tripledescryptoserviceprovider.Key = md5cryptoserviceprovider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey));
            tripledescryptoserviceprovider.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = tripledescryptoserviceprovider.CreateEncryptor();
            byte[] buff = Encoding.ASCII.GetBytes(password);

            return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));

        }

        public static string Descriptografar(string entrada)
        {
            TripleDESCryptoServiceProvider tripledescryptoserviceprovider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5cryptoserviceprovider = new MD5CryptoServiceProvider();

            string myKey = "1111111111111111";  //Aqui vc inclui uma chave qualquer para servir de base para cifrar, que deve ser a mesma no método Codificar
            tripledescryptoserviceprovider.Key = md5cryptoserviceprovider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey));
            tripledescryptoserviceprovider.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = tripledescryptoserviceprovider.CreateDecryptor();
            byte[] buff = Convert.FromBase64String(entrada);

            return ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));

        }

        public static void EnviarEmail(string destinatarios, string assunto, string mensagem, bool formatoHtml = false, List<Tuple<byte[], string>> anexos = null, Dictionary<string, string> imagemCidCaminho = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(destinatarios))
                {
                    var emails = destinatarios.Split(',').ToList();

                    var smtpClient = new SmtpClient
                    {
                        Host = "smtp.task.com.br",
                        Port = 587,
                        EnableSsl = false,
                        Credentials = new NetworkCredential("portalfornecedor@noventa.com.br", "N0v3nt@$")
                    };

                    MailAddress from = new MailAddress("portalfornecedor@noventa.com.br");

                    var message = new MailMessage()
                    {
                        From = from,
                        IsBodyHtml = formatoHtml,
                        Subject = assunto,
                        Body = mensagem
                    };

                    if (anexos != null && anexos.Any())
                    {
                        foreach (var anexo in anexos)
                        {
                            MemoryStream memoryStream = new MemoryStream(anexo.Item1);
                            message.Attachments.Add(new Attachment(memoryStream, anexo.Item2));
                        }
                    }

                    if (imagemCidCaminho != null && formatoHtml)
                    {
                        foreach (KeyValuePair<string, string> cidCaminho in imagemCidCaminho)
                        {
                            string caminhaAbsoluto = AppDomain.CurrentDomain.BaseDirectory + cidCaminho.Value.Replace("/", "\\");
                            Attachment htmlView = new Attachment(caminhaAbsoluto);
                            htmlView.ContentId = cidCaminho.Key;
                            htmlView.ContentDisposition.Inline = true;
                            message.Attachments.Add(htmlView);
                        }
                    }

                    foreach (var email in emails)
                    {
                        message.To.Add(email)
            ;
                    }

                    smtpClient.Send(message);
                }
                
            }
            catch
            {
                throw new Exception("Erro ao enviar email");
            }
           
        }

    }
}
