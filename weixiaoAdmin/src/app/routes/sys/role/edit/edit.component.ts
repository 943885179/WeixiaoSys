import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFTreeSelectWidgetSchema } from '@delon/form';
import { BasicService } from 'src/app/service/basic.service';
import { HttpBasicService } from '@shared/utils/http-basic.service';

@Component({
  selector: 'app-sys-role-edit',
  templateUrl: './edit.component.html',
})
export class SysRoleEditComponent implements OnInit {
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
    console.log(this.record);
    if (this.record.id > 0)
      // this.http.get(this.basic.ApiUrl + this.basic.ApiRole.RoleById + `/${this.record.id}`).subscribe(res => (this.i = res));
      this.i = this.record;
    this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectPower).subscribe(res => {
      const defaultTree = [];
      if (this.record.id > 0)
        this.i.rolePower.forEach(e => {
          defaultTree.push(e.power.id)
        });
      this.schema = {
        properties: {
          id: { type: 'string', title: '编号' },
          name: { type: 'string', title: '姓名', maxLength: 15 },
          powerIds: {
            // tslint:disable-next-line: no-object-literal-type-assertion
            type: `string`, title: `权限`, ui: {
              widget: `tree-select`,
              multiple: true,
              checkable: true, showExpand: true,
            } as SFTreeSelectWidgetSchema, enum: res, default: defaultTree
          }
        },
        required: ['name', 'id'],
      };
    });
  }

  save(value: any) {
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.AddOrEditRole, value).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
    });
  }

  close() {
    this.modal.destroy();
  }
}
