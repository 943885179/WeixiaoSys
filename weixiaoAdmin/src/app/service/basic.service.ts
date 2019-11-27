import { Injectable, NgModule } from '@angular/core';
import { CacheService } from '@delon/cache';
import { zip } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { _HttpClient } from '@delon/theme';
import jsrsasign from 'jsrsasign';

import * as JsEncryptModule from 'jsencrypt';
// import { JSEncrypt } from 'jsencrypt';
import * as JsDecryptModule from 'jsdecrypt';
@Injectable({
  providedIn: 'root'
})
export class BasicService {
  ApiUrl: any;
  ApiRole: any;
  AppData: any;
  constructor(private csv: CacheService, private http: _HttpClient) {
    this.init();
    // this.csv.clearNotify();
    // this.csv.clear();
    // this.csv.remove("assets/tmp/app-data.json");
    // this.csv.has = () => false;
    // this.csv.freq = 20;
    // this.csv.get("assets/tmp/app-data.json", { mode: "promise", type: "s", expire: 10 }
  }
  /**
   * GetSetting
   */
  public init() {
    // 不要在json里面加//备注，会识别错误，认为不是正确的json格式
    // this.csv.get("assets/tmp/app-data.json", { mode: "promise", type: "s", expire: 60 * 60 })
    zip(this.csv.get("assets/tmp/app-data.json", { mode: "promise", type: "s", expire: 60 * 60 }))
      .pipe(
        retry(3),
        catchError(([res]) => {
          return res;
        }))
      .subscribe(([res]) => {
        this.ApiUrl = res.ApiUrl;
        this.ApiRole = res.ApiRole;
        this.AppData = res;
      });
  }
}
 /*  zip(this.csv.get("assets/tmp/app-data.json", { mode: "promise", type: "s", expire: 60 * 60 }))
.pipe(
retry(10),
catchError(([res]) => {
return res;
}))
.subscribe(([res]) => {
console.log(this.ApiUrl);
this.ApiUrl = res.ApiUrl;
this.ApiRole = res.ApiRole;
this.AppData = res;
});*
* RSA加密、签名 使用jsrsasign
* @param data 加密字符串
* @return {String} string，加密后的base64格式字符串
*/
  // rsaEncrypt(data) {
  //   // 创建一个对象
  //   let rsa = new jsrsasign.RSAKey();
  //   // 使用公钥加密
  //   const secretkey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoKefXtIqc293nDjFpCkjUuyEL3bc15lVlocJJVdQovRLyTKXpzcD029U0MeVAd8Hq9huQtxWspV+/KSoXjN5BGc7XS/QZItG1irxtivIHlFmzra7Fk94r10F4/hR/mdq+H/WAIJJjGpw1Garncvh8AEJXJ2JBbiAyM0zNSqDNidTFFPuLoGQQ+EGsbCESMFy0mGeSBd8/b6ADwLiRXuAiNo3ArFRui2fwuljXyFP2EC1aRNIF8qc5GkikBkqUPKQrQ29H2cfQEpbxj2LP4hOLmuO+U2snMN3DRyTMnONJWN10x08VlCsZUS9fHJnR6kkU5PYodpJQ8hI1Uloy7TH5wIDAQAB"
  //   const publicKey = "-----BEGIN PUBLIC KEY-----\n" + secretkey + "\n-----END PUBLIC KEY-----";
  //   // console.log(publicKey)
  //   rsa = jsrsasign.KEYUTIL.getKey(publicKey)
  //   const enc = jsrsasign.KJUR.crypto.Cipher.encrypt(data, rsa);
  //   // 转换成Base64格式
  //   const base64Sign = jsrsasign.hextob64(enc);
  //   return base64Sign;
  // }
  // /**
  //  * Rsa加密 使用jsencrypt
  //  * @param timestr 明文
  //  */
  // getSignStr(timestr: string) {
  //   const secretkey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoKefXtIqc293nDjFpCkjUuyEL3bc15lVlocJJVdQovRLyTKXpzcD029U0MeVAd8Hq9huQtxWspV+/KSoXjN5BGc7XS/QZItG1irxtivIHlFmzra7Fk94r10F4/hR/mdq+H/WAIJJjGpw1Garncvh8AEJXJ2JBbiAyM0zNSqDNidTFFPuLoGQQ+EGsbCESMFy0mGeSBd8/b6ADwLiRXuAiNo3ArFRui2fwuljXyFP2EC1aRNIF8qc5GkikBkqUPKQrQ29H2cfQEpbxj2LP4hOLmuO+U2snMN3DRyTMnONJWN10x08VlCsZUS9fHJnR6kkU5PYodpJQ8hI1Uloy7TH5wIDAQAB"
  //   const publicKey = "-----BEGIN PUBLIC KEY-----\n" + secretkey + "\n-----END PUBLIC KEY-----";
  //   const privateStr = "";// 自定义的加密串
  //   const encrypt = new JsEncryptModule.JSEncrypt();
  //   encrypt.setPublicKey(publicKey);
  //   const signature = encrypt.encrypt(privateStr + timestr);
  //   return signature;
  // }
  // getDecrypt(timestr: string) {
  //   // 设置私钥
  //   const prvKey = "MIIEpAIBAAKCAQEAoKefXtIqc293nDjFpCkjUuyEL3bc15lVlocJJVdQovRLyTKXpzcD029U0MeVAd8Hq9huQtxWspV+/KSoXjN5BGc7XS/QZItG1irxtivIHlFmzra7Fk94r10F4/hR/mdq+H/WAIJJjGpw1Garncvh8AEJXJ2JBbiAyM0zNSqDNidTFFPuLoGQQ+EGsbCESMFy0mGeSBd8/b6ADwLiRXuAiNo3ArFRui2fwuljXyFP2EC1aRNIF8qc5GkikBkqUPKQrQ29H2cfQEpbxj2LP4hOLmuO+U2snMN3DRyTMnONJWN10x08VlCsZUS9fHJnR6kkU5PYodpJQ8hI1Uloy7TH5wIDAQABAoIBAQCMHgYyxix+J57zotRF/Bcx8NTNLOcqJzLtqXLBnajXpygeH8EC90mf8/7OZPPCAQqIx6hLKi93bEmoAdhS6KPIwlyVRumDd7HdgvDzyLWuM6Lt8ZO0vrVshT5o+SBSOVKjz6MPKJMLI56qsa1GYBb7o3vNyhxC4At6lvXtjdmItVp+Bs01w2RzfWL3RMTBNHJ4guOCKRQQTFgl6K2Uug2g+I4hmOvqp+V+0IefONjW5+nenSjvGo24pC35BBuzJTxf/1c5j2VhHw7QZv2FiONgJDYd82PhkWwJpPFrAi+oUYISgrkCYuQCFceZRMy1GYOdiJmC6ADzKeiutP90aZVRAoGBAN0aZRRnGBytUFkGYwzSPbPXdEndCJRTtppzp31gwwPV7NJu0wcVIHFsMtY+BCGEjRHEsO1/Fz97jke8jjndhJio49L4F9oIVWfbkNeTJeNnLPKzMVZodbFCT+Ji9at+MsHBGuAj9IdmZ4S+wN1BaVJFeX9JYCN85+yDBcHNLf8pAoGBALoC1Lh/2jvrAgh7RPT6x5v4WIBWIYakEoJu3/+QkkOU3qTZ64PV6S0AOWCt1EoOUFQqJDfLqFIz8rOIlO/j0bip3DriGNPAgvJnwfHgTNNVkAz0ITFDjphZw+4TScFFAtt5Y2ctly1Mm2K6mfVvErZE0G6IQQR45LASKDcBkUCPAoGAEA1YhVCuyXYzvSLfkhC5dhMQWER52Pry6Oe4ozuhLOgdF3IAVCVOg62NS0yZVC2haEbVaYiukWdQ/xhLYxwYAlVQpQJqCORN/wpLy7rdJ1NYSg6EaHeRA9uCnTb+CwNQgAya/ObfW+0tWs/WhLm5AcYVeg5Dso/g7qTciCNzUXECgYBUDtNyTNSiHyFcE9ilnG533VKhLEsaPSrgJpqzMvHl+HBkrtXvTcuBuogzWFqG3NEQN7sGO9Jk03TqDN7BSYKMoLYVJfdyOZzzTlAmreYJ2rCuKeSWDqFx157jB3RdEoKoC8MP7VpT7jqJ8yl/8CHUnGRUjt5S1w8BQjNKKDsroQKBgQDaMNCn0LVVUAQFK/CTLlYoYu6BTsNmlyyWtusU4/NpzFiZE9ztr7m05KKO8wRRj2md0YyH6yit5P7T1lQxOWu9LrfQSlA6Y8uAoLu+Ca1KLtXB1R1A84v+Co6V85e5Myncn4VfX9RN0ZnvpMLjEWhX/AlP3yTG0q7WvLyJ8E60PA==";
  //   const jsencrypt = new JsEncryptModule.JSEncrypt();
  //   jsencrypt.setPrivateKey(prvKey);
  //   // 解密数据
  //   const dec_by_prv = jsencrypt.decrypt(timestr);
  //   return dec_by_prv;
  // }
  // const s = "微笑";
  // const bb = this.basic.getSignStr(s);
  // console.log(bb);
  //   this.http.post("https://localhost:5001/api/rsa", { rsaName: bb }).subscribe(res => {
  //   console.log(res);
  // });


  // const s = "微笑";
  // 加密
  // const bb = this.basic.getSignStr(s);
  // console.log(bb);
  // 解密
  // const dd = this.basic.getDecrypt(bb);
  // console.log(dd);
