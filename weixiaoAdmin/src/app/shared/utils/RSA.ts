
import { Injectable } from '@angular/core';
import { BasicService } from 'src/app/service/basic.service';
import * as JsEncryptModule from 'jsencrypt';
export class RSA {
    privateKey: string;
    publicKey: string;
    weixiaoApiKey: string;
    privateHeadStr: string;// 自定义的加密串（头部）
    privateFlootStr: string;// 自定义的加密串（尾部）
    constructor(private basic: BasicService) {
        this.privateKey = basic.AppData.RSA.privateKey;
        this.publicKey = basic.AppData.RSA.publicKey;
        this.weixiaoApiKey = basic.AppData.RSA.weixiaoApiKey;
        this.privateHeadStr = basic.AppData.RSA.privateHeadStr;
    }
    /**
     * 加密
     * @param timestr 原始报文
     */
    Encrypt(timestr: string) {
        const jsencrypt = new JsEncryptModule.JSEncrypt();
        jsencrypt.setPublicKey(this.publicKey);
        return jsencrypt.encrypt(this.privateHeadStr + timestr + this.privateFlootStr);
    }
    /**
     * 解密
     * @param timestr 加密文字
     */
    Decrypt(timestr: string) {
        const jsencrypt = new JsEncryptModule.JSEncrypt();
        jsencrypt.setPrivateKey(this.privateKey);
        return jsencrypt.decrypt(timestr);
    }
}
