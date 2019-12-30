import { Component, OnInit } from '@angular/core';
import { NzModalRef, NzMessageService, NzDrawerRef } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';

@Component({
  selector: 'app-sys-dep-view',
  templateUrl: './view.component.html',
})
export class SysDepViewComponent implements OnInit {
  record: any = {};
  i: any;

  constructor(
    private drawer: NzDrawerRef,
    public msgSrv: NzMessageService,
    public http: HttpBasicService,
    private basic: BasicService
  ) { }

  ngOnInit(): void {
    this.http.get(`${this.basic.ApiUrl}${this.basic.ApiRole.DepById}/${this.record.id}`).subscribe(res => this.i = res);
  }

  close() {
    this.drawer.close();
  }
}
