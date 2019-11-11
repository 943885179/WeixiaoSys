import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema } from '@delon/form';
import { BasicService } from 'src/app/service/basic.service';

@Component({
  selector: 'app-sys-menu-edit',
  templateUrl: './edit.component.html',
})
export class SysMenuEditComponent implements OnInit {

  constructor(
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
    private basic: BasicService
  ) { }
  record: any = {};
  test: any = {};
  i: any;
  schema: SFSchema = {
    properties: {
      id: { title: '编号', type: "string" },
      text: { type: 'string', title: '菜单名称', maxLength: 15 },
      icon: { type: 'string', title: '图标' },
    },
    required: ['text', 'icon'],
  };
  ui: SFUISchema = {
    '*': {
      // spanLabelFixed: 100,
      grid: { span: 24 },
    },
    $id: {
      widget: 'text'
    },
    $text: {
      widget: 'string',
      // optional: "aaaaa",
      optionalHelp: "菜单名称"

    },
    // $description: {
    //   widget: 'textarea',
    //   grid: { span: 24 },
    // },
  };
  else

  async ngOnInit(): Promise<void> {
    if (this.record.id > 0)
      await this.http.get(this.basic.ApiUrl + this.basic.ApiRole.MenuById + `/${this.record.id}`).subscribe(res => this.i = res);
  }

  save(value: any) {
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.AddOrEditMenu, value).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
    });
  }

  close() {
    this.modal.destroy();
  }
}
