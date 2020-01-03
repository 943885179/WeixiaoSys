import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { STColumn, STComponent, STReq } from '@delon/abc';
import { SFSchema } from '@delon/form';
import { SysUserEditComponent } from './edit/edit.component';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';
import { SysUserViewComponent } from './view/view.component';

@Component({
  selector: 'app-sys-user',
  templateUrl: './user.component.html',
})
export class SysUserComponent implements OnInit {
  url: string;
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
    { title: '登录名', index: 'loginName' },
    { title: '身份证', index: 'idcard' },
    { title: '电话', index: 'phone' },
    { title: '部门', index: 'dep.depName' },
    { title: '公司', index: 'dep.company.name' },
    // { title: '头像', type: 'img', index: 'img', format: (item, col, index) => this.basic.serverUrl + item.img },
    {
      title: '头像', type: 'img',
      renderTitle: 'customTitle',
      render: 'custom',
    },
    {
      title: '',
      buttons: [
        {
          text: '查看', icon: 'search', type: "drawer", drawer: {
            component: SysUserViewComponent,
            params: item => item
          }
        },
        {
          text: '编辑',
          icon: 'edit',
          type: "static",
          // component: SysMenuEditComponent,
          modal: {
            component: SysUserEditComponent,
            params: (item: any) => item
          },
          click: () => {
            this.st.reload();
          }
        },
      ]
    }
  ];
  req: STReq = {}
  constructor(private http: HttpBasicService, public basic: BasicService, private modal: ModalHelper) {
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
