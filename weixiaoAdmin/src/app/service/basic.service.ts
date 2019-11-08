import { Injectable } from '@angular/core';
import { CacheService } from '@delon/cache';
import { zip } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { _HttpClient } from '@delon/theme';

@Injectable({
  providedIn: 'root'
})
export class BasicService {
  ApiUrl: string;
  ApiRole: any;
  AppData: any;
  constructor(private csv: CacheService, private http: _HttpClient) {
    // this.csv.clearNotify();
    // this.csv.clear();
    // this.csv.get("assets/tmp/app-data.json", { mode: "promise", type: "s", expire: 10 }
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
