using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BasicsApi.Dto;
using System.Text.Json;

namespace BasicsApi.conmon
{
    public class RSAHelper
    {
        private readonly RSA _privateKeyRsaProvider;
        private readonly RSA _publicKeyRsaProvider;
        private readonly RSA _publicAppKeyRsaProvider;
        private readonly HashAlgorithmName _hashAlgorithmName;
        private readonly string _splitStr;
        private readonly Encoding _encoding;
        /// <summary>
        /// ʵ����RSAHelper
        /// </summary>
        /// <param name="rsaType">�����㷨���� RSA SHA1;RSA2 SHA256 ��Կ��������Ϊ2048</param>
        /// <param name="encoding">��������</param>
        /// <param name="privateKey">˽Կ</param>
        /// <param name="publicKey">��Կ</param>
        public RSAHelper(RSAType rsaType, Encoding encoding, string privateKey, string publicKey = null, string appKey = null, string splitStr = null)
        {
            _encoding = encoding;
            if (!string.IsNullOrWhiteSpace(privateKey))
            {
                _privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(privateKey);
            }
            if (!string.IsNullOrWhiteSpace(publicKey))
            {
                _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);
            }
            if (!string.IsNullOrWhiteSpace(appKey))
            {
                _publicAppKeyRsaProvider = CreateRsaProviderFromPublicKey(appKey);
            }
            _hashAlgorithmName = rsaType == RSAType.RSA ? HashAlgorithmName.SHA1 : HashAlgorithmName.SHA256;
            _splitStr = splitStr;
        }
        #region ʹ��˽Կǩ��

        /// <summary>
        /// ʹ��˽Կǩ��
        /// </summary>
        /// <param name="data">ԭʼ����</param>
        /// <returns></returns>
        public string Sign(string data)
        {
            byte[] dataBytes = _encoding.GetBytes(data);

            var signatureBytes = _privateKeyRsaProvider.SignData(dataBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signatureBytes);
        }

        #endregion

        #region ʹ�ù�Կ��֤ǩ��

        /// <summary>
        /// ʹ�ù�Կ��֤ǩ��
        /// </summary>
        /// <param name="data">ԭʼ����</param>
        /// <param name="sign">ǩ��</param>
        /// <returns></returns>
        public bool Verify(string data, string sign)
        {
            byte[] dataBytes = _encoding.GetBytes(data);
            byte[] signBytes = Convert.FromBase64String(sign);

            var verify = _publicKeyRsaProvider.VerifyData(dataBytes, signBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return verify;
        }

        #endregion

        #region ����
        /// <summary>
        /// �ֶν���
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText)
        {
            if (_privateKeyRsaProvider == null)
            {
                throw new Exception("_privateKeyRsaProvider is null");
            }
            var sp = cipherText.Split(_splitStr);
            var result = "";
            foreach (var s in sp)
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    result += Encoding.UTF8.GetString(_privateKeyRsaProvider.Decrypt(Convert.FromBase64String(s), RSAEncryptionPadding.Pkcs1)); ;
                }
            }
            return result;
        }
        public T Decrypt<T>(string cipherText) where T : class
        {
            if (_privateKeyRsaProvider == null)
            {
                throw new Exception("_privateKeyRsaProvider is null");
            }
            var str = Encoding.UTF8.GetString(_privateKeyRsaProvider.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.Pkcs1));
            return JsonSerializer.Deserialize<T>(str);
        }
        #endregion

        #region ����

        public string Encrypt(string text)
        {
            if (_publicKeyRsaProvider == null)
            {
                throw new Exception("_publicKeyRsaProvider is null");
            }
            return Convert.ToBase64String(_publicKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.Pkcs1));
        }
        public string AppOldEncrypt(string text)
        {
            if (_publicAppKeyRsaProvider == null)
            {
                throw new Exception("_publicAppKeyRsaProvider is null");
            }
            return Convert.ToBase64String(_publicAppKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.Pkcs1));
        }
        public string AppEncrypt(ResponseDto dto)
        {
            //Formatting.Indented�������õ���ʾ��ʽ,�ɶ��Ը��á� ��һ����,Formatting.None����������Ҫ�Ŀո�ͻ��з�
            var jsonStr = JsonSerializer.Serialize(dto, options: new JsonSerializerOptions() { 
                //IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return this.AppEncrypt(jsonStr);
        }
        /// <summary>
        /// �ֶμ���
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string AppEncrypt(string text)
        {
            if (_publicAppKeyRsaProvider == null)
            {
                throw new Exception("_publicAppKeyRsaProvider is null");
            }
            var result = "";
            int t = (int)(Math.Ceiling((double)text.Length / (double)50));
            //�ָ�����
            for (int i = 0; i <= t - 1; i++)
            {
                var x = text.Substring(i * 50, text.Length - (i * 50) > 50 ? 50 : text.Length - (i * 50));
                result += Convert.ToBase64String(_publicAppKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(x), RSAEncryptionPadding.Pkcs1)) + _splitStr;
            }
            return result.Substring(0, result.Length - _splitStr.Length);
        }

        #endregion

        #region ʹ��˽Կ����RSAʵ��

        public RSA CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var privateKeyBits = Convert.FromBase64String(privateKey);

            var rsa = RSA.Create();
            var rsaParameters = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                rsaParameters.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.D = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.P = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Q = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DP = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DQ = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            rsa.ImportParameters(rsaParameters);
            return rsa;
        }

        #endregion

        #region ʹ�ù�Կ����RSAʵ��

        public RSA CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            var x509Key = Convert.FromBase64String(publicKeyString);

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using (MemoryStream mem = new MemoryStream(x509Key))
            {
                using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
                {
                    byte bt = 0;
                    ushort twobytes = 0;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    seq = binr.ReadBytes(15);       //read the Sequence OID
                    if (!CompareBytearrays(seq, seqOid))    //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    var rsa = RSA.Create();
                    RSAParameters rsaKeyInfo = new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent
                    };
                    rsa.ImportParameters(rsaKeyInfo);

                    return rsa;
                }

            }
        }

        #endregion

        #region ������Կ�㷨

        private int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
            if (bt == 0x82)
            {
                var highbyte = binr.ReadByte();
                var lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        private bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        #endregion
        #region stringתbase64
        public static string StringToBase64(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }
        #endregion
        #region base64תstring
        public static string Base64ToString(string base64)
        {
            var type = base64 == null ? new byte[0] : Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(type);
        }
        #endregion
        /// <summary>
        /// RAS����
        /// </summary>
        /// <param name="xmlPublicKey">��Կ</param>
        /// <param name="EncryptString">����</param>
        /// <returns>����</returns>

        public static string RSAEncrypt(string xmlPublicKey, string EncryptString)
        {
            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result = String.Empty;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            int t = (int)(Math.Ceiling((double)EncryptString.Length / (double)50));
            //�ָ�����
            for (int i = 0; i <= t - 1; i++)
            {

                PlainTextBArray = (new UnicodeEncoding()).GetBytes(EncryptString.Substring(i * 50, EncryptString.Length - (i * 50) > 50 ? 50 : EncryptString.Length - (i * 50)));
                CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
                Result += Convert.ToBase64String(CypherTextBArray) + "ThisIsSplit";
            }
            return Result;
        }
        /// <summary>
        /// RAS����
        /// </summary>
        /// <param name="xmlPrivateKey">˽Կ</param>
        /// <param name="DecryptString">����</param>
        /// <returns>����</returns>
        public static string RSADecrypt(string xmlPrivateKey, string DecryptString)
        {
            byte[] PlainTextBArray;
            byte[] DypherTextBArray;
            string Result = String.Empty;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            string[] Split = new string[1];
            Split[0] = "ThisIsSplit";
            //�ָ�����
            string[] mis = DecryptString.Split(Split, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < mis.Length; i++)
            {
                PlainTextBArray = Convert.FromBase64String(mis[i]);
                DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result += (new UnicodeEncoding()).GetString(DypherTextBArray);
            }
            return Result;
        }

        /// <summary>
        /// ������Կ��˽Կ��
        /// </summary>
        /// <returns>string[] 0:˽Կ;1:��Կ</returns>
        public static string[] RSAKey()
        {
            string[] keys = new string[2];
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            keys[0] = rsa.ToXmlString(true);
            keys[1] = rsa.ToXmlString(false);
            return keys;
        }
    }

    /// <summary>
    /// RSA�㷨����
    /// </summary>
    public enum RSAType
    {
        /// <summary>
        /// SHA1
        /// </summary>
        RSA = 0,
        /// <summary>
        /// RSA2 ��Կ��������Ϊ2048
        /// SHA256
        /// </summary>
        RSA2
    }
}
