using System.Text;
using System.Security.Cryptography;

namespace WEB.MVCUI.Hashing
{
    namespace VektorelMvcApp.Hashing
    {
        public static class EncryptionUtility
        {
            private static string key = "vektorelmvcapp";

            // Hashlemek için:
            public static string Encrypt(string toEncrypt, bool useHashing)
            {
                byte[] keyArray;
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

                if (useHashing)
                {
                    using (MD5 md5 = MD5.Create())
                    {
                        keyArray = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                    }
                }
                else
                {
                    keyArray = Encoding.UTF8.GetBytes(key);
                }

                using (TripleDES tdes = TripleDES.Create())
                {
                    tdes.Key = keyArray;
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = tdes.CreateEncryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length).Replace("+", "-").Replace("/", "_");
                }
            }
        
    






            //public static int Decrypt(string cipherString, bool useHashing)
            //{
            //    if (cipherString == "" || cipherString == null)
            //    {
            //        return 0;
            //    }
            //    cipherString = cipherString.Replace("-", "+").Replace("_", "/");
            //    byte[] keyArray;
            //    byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            //    if (useHashing)
            //    {
            //        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //        hashmd5.Clear();
            //    }
            //    else
            //        keyArray = UTF8Encoding.UTF8.GetBytes(key);
            //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //    tdes.Key = keyArray;
            //    tdes.Mode = CipherMode.ECB;
            //    tdes.Padding = PaddingMode.PKCS7;
            //    ICryptoTransform cTransform = tdes.CreateDecryptor();
            //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //    tdes.Clear();
            //    return Convert.ToInt32(UTF8Encoding.UTF8.GetString(resultArray));
            //}
            //public static string DecryptString(string cipherString, bool useHashing)
            //{
            //    if (cipherString == "" || cipherString == null)
            //    {
            //        return "";
            //    }
            //    cipherString = cipherString.Replace("-", "+").Replace("_", "/");
            //    byte[] keyArray;
            //    byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            //    if (useHashing)
            //    {
            //        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //        hashmd5.Clear();
            //    }
            //    else
            //        keyArray = UTF8Encoding.UTF8.GetBytes(key);
            //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //    tdes.Key = keyArray;
            //    tdes.Mode = CipherMode.ECB;
            //    tdes.Padding = PaddingMode.PKCS7;
            //    ICryptoTransform cTransform = tdes.CreateDecryptor();
            //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //    tdes.Clear();
            //    return Convert.ToString(UTF8Encoding.UTF8.GetString(resultArray));
            //}
            //public static string Base64Decode(string base64EncodedData)
            //{
            //    try
            //    {
            //        var modAl = base64EncodedData.Length % 4;
            //        if (modAl > 0) base64EncodedData += new string('=', 4 - modAl);
            //        var data = Convert.FromBase64String(base64EncodedData);
            //        var decodedString = Encoding.UTF8.GetString(data);
            //        return decodedString;
            //    }
            //    catch (Exception ex)
            //    {
            //        return ex.ToString();
            //    }
            //}
            //public static string Base64Encode(string plainText)
            //{
            //    var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            //    return Convert.ToBase64String(plainTextBytes);
            //}
        }
    }

}
