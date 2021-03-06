import { Component, OnInit } from '@angular/core';
import { NzDrawerRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'app-sys-power-view',
  templateUrl: './view.component.html',
})
export class SysPowerViewComponent implements OnInit {
  record: any = {};
  i: any;

  constructor(
    private drawer: NzDrawerRef,
    public msgSrv: NzMessageService,
    public http: _HttpClient
  ) { }

  ngOnInit(): void {
    this.i = this.record;
  }

  close() {
    this.drawer.close();
  }
}
