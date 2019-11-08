import { Injectable } from '@angular/core';
import { CacheService } from "@delon/cache";
import { _HttpClient } from '@delon/theme';
import { BasicService } from './basic.service';
import { zip } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class MenuService {
  constructor(private csv: CacheService, private http: _HttpClient, private basic: BasicService) {
  }
  getMenu() {
    zip(this.csv.get(this.basic.ApiUrl + this.basic.ApiRole.Menus));
  }
}
