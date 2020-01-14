import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFTreeSelectWidgetSchema } from '@delon/form';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';

@Component({
  selector: 'app-sys-power-edit',
  templateUrl: './edit.component.html',
})
export class SysPowerEditComponent implements OnInit {
  record: any = {};
  i: any;
  schema: SFSchema = {
    properties: {
    }
  };
  ui: SFUISchema = {
    '*': {
      spanLabelFixed: 100,
      grid: { span: 24 },
    },
    $id: {
      widget: 'text'
    }
  };

  constructor(
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public http: HttpBasicService,
    public basic: BasicService
  ) { }

  ngOnInit(): void {
    if (this.record.id > 0)
      this.i = this.record;
    console.log(this.i);
    this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectMenu).subscribe(res => {
      const defalutMenu = [];
      if (this.record.id > 0 && this.record.roleMenu != null)
        this.i.roleMenu.forEach(e => {
          defalutMenu.push(e.menuId)
        });
      this.schema = {
        properties: {
          id: { type: 'string', title: '编号' },
          name: { type: 'string', title: '姓名', maxLength: 15 },
          menuIds: {
            // tslint:disable-next-line: no-object-literal-type-assertion
            type: `string`, title: `菜单`, enum: res, default: defalutMenu, ui: {
              widget: `tree-select`,
              multiple: true,
              checkable: true, showExpand: true,
            } as SFTreeSelectWidgetSchema
          }
        },
        required: ['name', 'id'],
      };
    });

  }

  save(value: any) {
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.AddOrEditPower, value).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
    });
  }

  close() {
    this.modal.destroy();
  }
}
