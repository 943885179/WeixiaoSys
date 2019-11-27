
import { Injectable, NgModule } from '@angular/core';
import { BasicService } from 'src/app/service/basic.service';
import * as JsEncryptModule from 'jsencrypt';
import * as JsEncryptLongModule from 'encryptlong';
import { NzMessageService } from 'ng-zorro-antd';
// @NgModule({
//     providers: [BasicService]
// })
@Injectable({
    providedIn: 'root'
})
export class RSA {
    privateKey: string;
    publicKey: string;
    weixiaoApiKey: string;
    privateHeadStr: string;// 自定义的加密串（头部）
    privateFlootStr: string;// 自定义的加密串（尾部）
    rsaSpiltStr: string;// 分割字符串
    constructor(private basic: BasicService, private msg: NzMessageService) {
        this.privateKey = basic.AppData.RSA.privateKey;
        this.publicKey = basic.AppData.RSA.publicKey;
        this.weixiaoApiKey = basic.AppData.RSA.weixiaoApiKey;
        this.privateHeadStr = basic.AppData.RSA.privateHeadStr;
        this.rsaSpiltStr = basic.AppData.RSA.rsaSpiltStr;
    }
    /**
     * 本地加密加密
     * @param timestr 原始报文
     */
    Encrypt(timestr: string) {
        const jsencrypt = new JsEncryptModule.JSEncrypt();
        const publicKey = "-----BEGIN PUBLIC KEY-----\n" + this.publicKey + "\n-----END PUBLIC KEY-----";
        jsencrypt.setPublicKey(publicKey);
        this.privateHeadStr = this.privateHeadStr === undefined ? "" : this.privateHeadStr;
        this.privateFlootStr = this.privateFlootStr === undefined ? "" : this.privateFlootStr;
        const rsaStr = jsencrypt.encrypt(this.privateHeadStr + timestr + this.privateFlootStr);
        if (!rsaStr) {
            this.msg.error("加密错误");
            return;
        }
        return rsaStr;
    }
    /**
     * Wepapi加密
     * @param timestr 原始报文
     */
    ApiEncrypt(timestr: string) {
        const jsencrypt = new JsEncryptModule.JSEncrypt();
        const publicKey = "-----BEGIN PUBLIC KEY-----\n" + this.weixiaoApiKey + "\n-----END PUBLIC KEY-----";
        jsencrypt.setPublicKey(publicKey);
        this.privateHeadStr = this.privateHeadStr === undefined ? "" : this.privateHeadStr;
        this.privateFlootStr = this.privateFlootStr === undefined ? "" : this.privateFlootStr;
        const rsaStr = jsencrypt.encrypt(this.privateHeadStr + timestr + this.privateFlootStr);
        if (!rsaStr) {
            this.msg.error("加密错误");
            return;
        }
        return rsaStr;
    }
    /**
     * Wepapi长文本加密
     * @param timestr 原始报文
     */
    ApiLongEncrypt(timestr: string) {
        const jsencrypt = new JsEncryptLongModule.JSEncrypt();
        const publicKey = "-----BEGIN PUBLIC KEY-----\n" + this.weixiaoApiKey + "\n-----END PUBLIC KEY-----";
        jsencrypt.setPublicKey(publicKey);
        this.privateHeadStr = this.privateHeadStr === undefined ? "" : this.privateHeadStr;
        this.privateFlootStr = this.privateFlootStr === undefined ? "" : this.privateFlootStr;
        alert(timestr);
        const rsaStr = jsencrypt.encryptLong(this.privateHeadStr + timestr + this.privateFlootStr);
        if (!rsaStr) {
            this.msg.error("加密错误");
            return;
        }
        return rsaStr;
    }
    /**
     * 解密
     * @param timestr 加密文字
     */
    Decrypt(timestr: string) {
        try {
            if (timestr === null || timestr === "") {
                return "";
            }
            const jsencrypt = new JsEncryptModule.JSEncrypt();
            jsencrypt.setPrivateKey(this.privateKey);
            const strSp = timestr.split(this.rsaSpiltStr);
            let result = "";
            for (const str of strSp) {
                if (str !== "") {
                    const xs = jsencrypt.decrypt(str);
                    result += xs;
                }
            }
            return result;
        }
        catch (e) {
            return timestr;
        }
        // return jsencrypt.decrypt(timestr);
    }
}
