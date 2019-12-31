import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { STColumn, STComponent, STReq } from '@delon/abc';
import { SFSchema } from '@delon/form';
import { SysUserEditComponent } from './edit/edit.component';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';

@Component({
  selector: 'app-sys-user',
  templateUrl: './user.component.html',
})
export class SysUserComponent implements OnInit {
  url = `/user`;
  searchSchema: SFSchema = {
    properties: {
      name: {
        type: 'string',
        title: '用户名'
      }
    }
  };
  @ViewChild('st', { static: false }) st: STComponent;
  columns: STColumn[] = [
    { title: '用户名', index: 'name' },
    { title: '登录名', type: 'number', index: 'loginName' },
    { title: '头像', type: 'img', width: '50px', index: 'img' },
    {
      title: '',
      buttons: [
        { text: '查看', click: (item: any) => `/view/${item.id}` },
        { text: '编辑', type: 'static', component: SysUserEditComponent, click: 'reload' },
      ]
    }
  ];
  req: STReq = {}
  constructor(private http: HttpBasicService, private basic: BasicService, private modal: ModalHelper) {
    this.req = this.http.req;
  }

  ngOnInit() {
    this.url = this.basic.ApiUrl + this.basic.ApiRole.Emps;
  }

  add() {
    this.modal
      .createStatic(SysUserEditComponent, { i: { id: 0 } })
      .subscribe(() => this.st.reload());
  }

}
