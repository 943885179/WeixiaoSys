import { Component, OnInit } from '@angular/core';
import { NzMessageService, NzDrawerRef } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { BasicService } from 'src/app/service/basic.service';

@Component({
  selector: 'app-sys-company-view',
  templateUrl: './view.component.html',
})
export class SysCompanyViewComponent implements OnInit {
  record: any = {};
  i: any;
  area: string;
  constructor(
    private drawer: NzDrawerRef,
    public msgSrv: NzMessageService,
    public http: _HttpClient,
    private basic: BasicService,
  ) { }

  async ngOnInit(): Promise<void> {
    // await this.http.get(this.basic.ApiUrl + this.basic.ApiRole.CompanyById + `/${this.record.id}`).subscribe(res => this.i = res);
    this.i = this.record;
    await this.http.get(this.basic.ApiUrl + this.basic.ApiRole.ShareholderByCid + `/${this.record.id}`).subscribe(res => this.i.shareholder = res);
  }

  close() {
    this.drawer.close();
  }
}
