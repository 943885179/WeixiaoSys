import { Component, OnInit, Input } from '@angular/core';
import { NzModalRef, NzDrawerRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { BasicService } from 'src/app/service/basic.service';
import { HttpBasicService } from '@shared/utils/http-basic.service';
@Component({
  selector: 'app-sys-menu-view',
  templateUrl: './view.component.html',
})
export class SysMenuViewComponent implements OnInit {

  @Input()
  record: any;
  i: any;
  constructor(
    // private modal: NzModalRef,
    public msgSrv: NzMessageService,
    public http: HttpBasicService,
    private drawer: NzDrawerRef,
    private basic: BasicService
  ) { }

  ngOnInit(): void {
    this.http.get(this.basic.ApiUrl + this.basic.ApiRole.MenuById + `/${this.record.id}`).subscribe(res => this.i = res);
  }
  ok() {
    this.drawer.close(`new time: ${+new Date()}`);
    this.cancel();
  }
  cancel() {
    this.drawer.close();
  }
  close() {
    // this.modal.destroy();
    this.drawer.close();
  }
}
