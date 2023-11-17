using System.Text;
using System.Security.Cryptography;


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
            ASCIIEncoding MyASCIIEncoding = new ASCIIEncoding();
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

    }
}
