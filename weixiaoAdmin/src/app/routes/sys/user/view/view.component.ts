import { Component, OnInit } from '@angular/core';
import { NzDrawerRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { BasicService } from 'src/app/service/basic.service';

@Component({
  selector: 'app-sys-user-view',
  templateUrl: './view.component.html',
})
export class SysUserViewComponent implements OnInit {
  record: any = {};
  i: any;

  constructor(
    private drawer: NzDrawerRef,
    public msgSrv: NzMessageService,
    public http: _HttpClient,
    public basic: BasicService
  ) { }

  ngOnInit(): void {
    this.i = this.record;
  }

  close() {
    this.drawer.close();
  }

  showImg(value: any) {
    // window.location.href =this.basic.serverUrl+ this.i.img;
    window.open(value);
  }
}
