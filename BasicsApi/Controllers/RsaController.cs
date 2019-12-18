using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicsApi.conmon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RsaController : ControllerBase
    {
        [HttpGet("rsa/{str}")]
        public string Get(string str)
        {
            //2048 公钥
            string publicKey =
                "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoKefXtIqc293nDjFpCkjUuyEL3bc15lVlocJJVdQovRLyTKXpzcD029U0MeVAd8Hq9huQtxWspV+/KSoXjN5BGc7XS/QZItG1irxtivIHlFmzra7Fk94r10F4/hR/mdq+H/WAIJJjGpw1Garncvh8AEJXJ2JBbiAyM0zNSqDNidTFFPuLoGQQ+EGsbCESMFy0mGeSBd8/b6ADwLiRXuAiNo3ArFRui2fwuljXyFP2EC1aRNIF8qc5GkikBkqUPKQrQ29H2cfQEpbxj2LP4hOLmuO+U2snMN3DRyTMnONJWN10x08VlCsZUS9fHJnR6kkU5PYodpJQ8hI1Uloy7TH5wIDAQAB";
            //2048 私钥
            string privateKey =
                "MIIEpAIBAAKCAQEAoKefXtIqc293nDjFpCkjUuyEL3bc15lVlocJJVdQovRLyTKXpzcD029U0MeVAd8Hq9huQtxWspV+/KSoXjN5BGc7XS/QZItG1irxtivIHlFmzra7Fk94r10F4/hR/mdq+H/WAIJJjGpw1Garncvh8AEJXJ2JBbiAyM0zNSqDNidTFFPuLoGQQ+EGsbCESMFy0mGeSBd8/b6ADwLiRXuAiNo3ArFRui2fwuljXyFP2EC1aRNIF8qc5GkikBkqUPKQrQ29H2cfQEpbxj2LP4hOLmuO+U2snMN3DRyTMnONJWN10x08VlCsZUS9fHJnR6kkU5PYodpJQ8hI1Uloy7TH5wIDAQABAoIBAQCMHgYyxix+J57zotRF/Bcx8NTNLOcqJzLtqXLBnajXpygeH8EC90mf8/7OZPPCAQqIx6hLKi93bEmoAdhS6KPIwlyVRumDd7HdgvDzyLWuM6Lt8ZO0vrVshT5o+SBSOVKjz6MPKJMLI56qsa1GYBb7o3vNyhxC4At6lvXtjdmItVp+Bs01w2RzfWL3RMTBNHJ4guOCKRQQTFgl6K2Uug2g+I4hmOvqp+V+0IefONjW5+nenSjvGo24pC35BBuzJTxf/1c5j2VhHw7QZv2FiONgJDYd82PhkWwJpPFrAi+oUYISgrkCYuQCFceZRMy1GYOdiJmC6ADzKeiutP90aZVRAoGBAN0aZRRnGBytUFkGYwzSPbPXdEndCJRTtppzp31gwwPV7NJu0wcVIHFsMtY+BCGEjRHEsO1/Fz97jke8jjndhJio49L4F9oIVWfbkNeTJeNnLPKzMVZodbFCT+Ji9at+MsHBGuAj9IdmZ4S+wN1BaVJFeX9JYCN85+yDBcHNLf8pAoGBALoC1Lh/2jvrAgh7RPT6x5v4WIBWIYakEoJu3/+QkkOU3qTZ64PV6S0AOWCt1EoOUFQqJDfLqFIz8rOIlO/j0bip3DriGNPAgvJnwfHgTNNVkAz0ITFDjphZw+4TScFFAtt5Y2ctly1Mm2K6mfVvErZE0G6IQQR45LASKDcBkUCPAoGAEA1YhVCuyXYzvSLfkhC5dhMQWER52Pry6Oe4ozuhLOgdF3IAVCVOg62NS0yZVC2haEbVaYiukWdQ/xhLYxwYAlVQpQJqCORN/wpLy7rdJ1NYSg6EaHeRA9uCnTb+CwNQgAya/ObfW+0tWs/WhLm5AcYVeg5Dso/g7qTciCNzUXECgYBUDtNyTNSiHyFcE9ilnG533VKhLEsaPSrgJpqzMvHl+HBkrtXvTcuBuogzWFqG3NEQN7sGO9Jk03TqDN7BSYKMoLYVJfdyOZzzTlAmreYJ2rCuKeSWDqFx157jB3RdEoKoC8MP7VpT7jqJ8yl/8CHUnGRUjt5S1w8BQjNKKDsroQKBgQDaMNCn0LVVUAQFK/CTLlYoYu6BTsNmlyyWtusU4/NpzFiZE9ztr7m05KKO8wRRj2md0YyH6yit5P7T1lQxOWu9LrfQSlA6Y8uAoLu+Ca1KLtXB1R1A84v+Co6V85e5Myncn4VfX9RN0ZnvpMLjEWhX/AlP3yTG0q7WvLyJ8E60PA==";
            string publicKeys =
                    "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtf10GO+uVVINJwMqdSDK12UwUcR0rVKBnDcn0hrAMvcH+Wy980+IX8wlOoytynPqKXcf+fmZ3j7X4icRNeztGV4HpRgsViEz0PIO9juJvGLng4ERR4ubk3jLSK/+lVw94ZJ9BtN7FG7NOO0u5tGcDHuMU9aNLwFb3is8UAWul5yACaZxmE8eNjX+CdtxyYIMUIUp5pLRHqo/c6T0bZdBQtDBX/tdF2bGDPcsV0tO3fuwoeJq4w9mBlbVD8mVV1qN/2jXTiX0mAwF59YJwjugPv4FHmu88vQAaR9J/YG1bDRw+XZm07d/aJo84nxgTs+mzxm43tUWOhMwp0ZURxVzawIDAQAB";
            var rsa = new RSAHelper(RSAType.RSA2, Encoding.UTF8, privateKey, publicKey, publicKeys, "asda");

            // string str = "博客园 http://www.cnblogs.com/";

            Console.WriteLine("原始字符串：" + str);

            //加密
            string enStr = rsa.Encrypt(str);

            string enStrs = rsa.AppEncrypt(str);
            Console.WriteLine("加密字符串：" + enStr);

            //解密
            string deStr = rsa.Decrypt(enStr);

            Console.WriteLine("解密字符串：" + deStr);

            //私钥签名
            string signStr = rsa.Sign(str);

            Console.WriteLine("字符串签名：" + signStr);

            //公钥验证签名
            bool signVerify = rsa.Verify(str, signStr);

            Console.WriteLine("验证签名：" + signVerify);
            return deStr;
            // return RSAHelper.StringToBase64("abcsdeasdweixiao");
        }
        [HttpPost()]
        public String Post(RsaDto dto)
        {
            return Rsas(dto);
            // return dto.rsaName;
        }
        [HttpPost("Rsas")]
        public static string Rsas(RsaDto dto)
        {

            //2048 公钥
            string publicKey =
                "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoKefXtIqc293nDjFpCkjUuyEL3bc15lVlocJJVdQovRLyTKXpzcD029U0MeVAd8Hq9huQtxWspV+/KSoXjN5BGc7XS/QZItG1irxtivIHlFmzra7Fk94r10F4/hR/mdq+H/WAIJJjGpw1Garncvh8AEJXJ2JBbiAyM0zNSqDNidTFFPuLoGQQ+EGsbCESMFy0mGeSBd8/b6ADwLiRXuAiNo3ArFRui2fwuljXyFP2EC1aRNIF8qc5GkikBkqUPKQrQ29H2cfQEpbxj2LP4hOLmuO+U2snMN3DRyTMnONJWN10x08VlCsZUS9fHJnR6kkU5PYodpJQ8hI1Uloy7TH5wIDAQAB";
            //2048 私钥
            string privateKey =
                "MIIEogIBAAKCAQEAzDueGtt83DiINqNbH9/i0gWES7WZFkGczpGHuz+slyuUrHw7pX7/IajNrIEuJvA0igNEZ8+Rf4cuIqo6jrqw96YyeZP+IoZFK8+6K64RPRwauo9TaAWELn8oaEwlwRltjTvDZQVMCI9NfEXVoVqLChx/vkubnzZOJsmvqmT7o4JBXc2nA3lq/qCruy2Fe8q1F11lITZQyl7bWgfVQQRHmUAn107Y4Hli6gXru90/QcDP/BobVkO+fN3AovwCgMqb96NssMBQbnOeL1PIXclI2G2iKU3yKKw62rxMG2EJrw/ZBVQTKAXdoUDI0wa8MxwFkAPzW1ihtQKS2UU+McfinwIDAQABAoIBACX7QGAGSaY67TocDypSXMBqPjxGPX4iHaNc9T0hjltew3uAbydMAu6jkfxu2cJsEZlJGkOkGo74+N+BgPpiRd8IjYKGv1B0YBDRxPGyoYoX2/CuDvjdbcOn6j/bSXor3G/TmXcEESvWWrat1hj32bu7qRYewYZOdyJHh9/Mf+/cSAjy/hIFmxz3kQShUWgob9Czn6+rA7JhFaFZe/bLwtPn10naj4F5BibPRFNrTPY/air4Pv2dIQZ1wJqi60KgkBWSjFklwehxaK8vxwgkrlAJSM6hmxAodsievnfdScnRyKVm5q6RW4Ax8P0Odq3dNphWFqu5QTdNBIplFxMxTMECgYEA/OFBj/5YGy1D40uLXPNehSFbULYNLrMZq/m7Hvc/S3OBIaLD5cxQxxhEVM0kUDKSMTEx18YpoGeGq6UKSLEKyrGT+QC5I9rI+WvTTBQKT0JoQooRWbdV9UskY314sGQatxCnJokSrGvPFXtv0qHJ+jM0sI6+wUqdHA9TY2Vdc/kCgYEAzsC0m1NbV/9Y9eKajdOUQ5jYUKx4H1+CLmaemeJvK1896CzxQPY8x9kJMLYaTTW877wnUc6GMSnzNJMAIw1r6Vd7CZSYH5eLs59H5Tof9ovdBxHei3eQbwrl+ssyq0/sbVYPjv66hXH9eudarX0TGyANCMoVBqG3d880HP99gVcCgYAX/h9EVDNz0KWiSSad2RFcvD93tu4lQiTrZjRUycydkgXsdQ71HJ+FZE4HZbdOTJ4GQM6j1E9awrfKTUxefT2y4YpSk7j9J+Ltl0di7nvT7U8LESJ4SqbDMS/wqJTs13KZb+EMDPFSnp/1P9LqPyN0s6sKHWEH4dZqNSIKSjHFsQKBgGdWQe7wdtNAuUIMhJsmiRBQMK4BxfhIUFTIzbS0TEQtUk/dRqflavOoMsO6AONeXJSmQjDNPJ0ODpTUdFkQuELkioZ7Up1XrWeV0OVta9Rai5qg/85NcE9P7yqurSCazdzICva5sphIJR1szRGxVf9Uwa8G8gNiiMTFN5LWz+/5AoGAeBflK25j80/kFK3+ZKe+33B+atN/OLV4qEh5G1m888oj8rWa+iWfQFCZZkRL7S+nMXnx9hzNRRjJoKPlT7qlePHEhVCr4ruQ8mzN8VPRSyaRBIV7lg5M9li43SWjsCD4yT390MmgJYrLpT/9txblUqhQUXn/tZ0kM3uEKHieOsc=";

            var rsa = new RSAHelper(RSAType.RSA2, Encoding.UTF8, privateKey, publicKey, "");

            return rsa.Decrypt(dto.rsaName);
        }
    }
    public class RsaDto
    {
        public string rsaName { get; set; }
    }
}
