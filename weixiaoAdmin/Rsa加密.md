npm install jsrsasign --save

# 使用<https://www.jianshu.com/p/3b6ad30954b8>

import jsrsasign from 'jsrsasign';

/**

* RSA加密、签名
   * @param data 加密字符串
   * @return {String} string，加密后的base64格式字符串
   */
rsaEncrypt(data){
  let enc, base64Sign;
  let rsa = new jsrsasign.RSAKey();
  rsa = jsrsasign.KEYUTIL.getKey(this.publicKey)
  enc = jsrsasign.KJUR.crypto.Cipher.encrypt(data, rsa);
  base64Sign = jsrsasign.hextob64(enc);
  return base64Sign;
}
