import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { _HttpHeaders } from '@delon/theme/src/services/http/http.client';
import { Observable } from 'rxjs';
import { RSA } from './RSA';
import { STReq, STRequestOptions } from '@delon/abc';

@Injectable({
  providedIn: 'root'
})
export class HttpBasicService {
  req: STReq = {
    method: "post",
    allInBody: true,
    headers: { "Content-Type": "application/json" },
    // lazyLoad: true,开启后进入界面没数据
    process: (options: STRequestOptions) => {
      options.body = { data: this.rsa.ApiEncrypt(JSON.stringify(options.body)) };
      return options;
    }
  };
  constructor(private http: _HttpClient, private rsa: RSA) { }
  get(url: string, params?: any, options?: {
    headers?: _HttpHeaders;
    observe?: 'response';
    reportProgress?: boolean;
    responseType?: 'json';
    withCredentials?: boolean;
  }) {
    return this.http.get(url, params, options);
  };
  post(url: string, body?: any, params?: any, options?: {
    headers?: _HttpHeaders;
    observe: 'events';
    reportProgress?: boolean;
    responseType?: 'arraybuffer' | 'blob' | 'json' | 'text';
    withCredentials?: boolean;
  }) {
    if (body != null) {
      const newBody = { data: this.rsa.ApiSpEncrypt(JSON.stringify(body)) }
      return this.http.post(url, newBody, params, options);
    }
    else {
      return this.http.post(url, body, params, options);
    }
  }

}
