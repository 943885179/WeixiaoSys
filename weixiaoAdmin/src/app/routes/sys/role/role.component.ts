import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { STColumn, STComponent } from '@delon/abc';
import { SFSchema } from '@delon/form';
import { SysRoleEditComponent } from './edit/edit.component';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { NzMessageService } from 'ng-zorro-antd';
import { BasicService } from 'src/app/service/basic.service';
import { SysRoleViewComponent } from './view/view.component';

@Component({
  selector: 'app-sys-role',
  templateUrl: './role.component.html',
})
export class SysRoleComponent implements OnInit {
  url;
  req;
  searchSchema: SFSchema = {
    properties: {
      no: {
        type: 'string',
        title: '编号'
      }
    }
  };
  @ViewChild('st', { static: false }) st: STComponent;
  columns: STColumn[] = [
    { title: '编号', index: 'id' },
    { title: '名称', index: 'name' },
    { title: '权限', index: 'rolePower', render: 'custom', renderTitle: 'customTitle' },
    {
      title: '操作',
      buttons: [
        { text: '查看', icon: 'search', type: `drawer`, drawer: { component: SysRoleViewComponent, params: item => item } },
        {
          text: '编辑', icon: `edit`, type: 'modal', modal: {
            component: SysRoleEditComponent,
            params: item => item
          }, click: 'reload'
        },
        {
          text: '删除', type: 'del', icon: 'anticon anticon-delete', pop: {
            title: '确认删除？', trigger: 'click', placement: "bottomRight",
            okType: 'danger',
            icon: 'delete',
          },
          click: (record, _modal, comp) => {
            this.http.post(this.basic.ApiUrl + this.basic.ApiRole.DeleteRole + `/${record.id}`).subscribe(res => {
              if (res != null) {
                this.message.success(`成功删除【${record.name}】`);
                comp!.removeRow(record);
              }
            })
          },
        }
      ]
    }
  ];

  constructor(private http: HttpBasicService, private message: NzMessageService, public basic: BasicService, private modal: ModalHelper) {
    this.req = http.req;
  }

  ngOnInit() {
    this.url = this.basic.ApiUrl + this.basic.ApiRole.Roles;
  }

  add() {
    this.modal
      .createStatic(SysRoleEditComponent, { i: { id: 0 } })
      .subscribe(() => this.st.reload());
  }

}
