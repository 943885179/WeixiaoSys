import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { _HttpHeaders } from '@delon/theme/src/services/http/http.client';
import { Observable } from 'rxjs';
import { RSA } from './RSA';

@Injectable({
  providedIn: 'root'
})
export class HttpBasicService {

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
