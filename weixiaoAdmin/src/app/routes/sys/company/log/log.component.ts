import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzDrawerRef } from 'ng-zorro-antd';
import { BasicService } from 'src/app/service/basic.service';

@Component({
  selector: 'app-sys-company-log',
  templateUrl: './log.component.html',
})
export class SysCompanyLogComponent implements OnInit {
  record: any;
  i: any;
  constructor(private http: _HttpClient, private drawer: NzDrawerRef, private basic: BasicService) {

  }
  async ngOnInit() {
    this.i = this.record;
    await this.http.get(this.basic.ApiUrl + this.basic.ApiRole.CompanyLogByCid + `/${this.record.id}`).subscribe(res => this.i.companyLog = res);

  }
  close() {
    this.drawer.close();
  }
}
