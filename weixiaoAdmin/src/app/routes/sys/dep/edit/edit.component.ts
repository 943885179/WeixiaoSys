import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFRenderSchema } from '@delon/form';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';

@Component({
  selector: 'app-sys-dep-edit',
  templateUrl: './edit.component.html',
})
export class SysDepEditComponent implements OnInit {
  record: any = {};
  i: any;
  schema: SFSchema = {
    properties: {}
  };
  ui: SFUISchema = {
    '*': {
      spanLabelFixed: 100,
      grid: { span: 24 },
    },
  };

  constructor(
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public http: HttpBasicService,
    private basic: BasicService
  ) { }
  async GetInit() {
    this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectCompany).subscribe(com => {
      this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectDep).subscribe(res => {
        this.schema = {
          properties: {
            depName: { type: 'string', title: '名称' },
            depCode: { type: 'string', title: '编码', maxLength: 15 },
            companyId: {
              type: 'string',
              title: '公司',
              enum: com,
              ui: {
                widget: "tree-select"
              },
            },
            pid: {
              type: 'string',
              title: '上级',
              enum: res,
              ui: {
                widget: 'tree-select',
              },
            },
          },
          required: ['depName', 'depCode', `companyId`],
        };
      })
    })
  }
  async ngOnInit(): Promise<void> {
    if (this.record.id > 0) {
      await this.http
        .get(this.basic.ApiUrl + this.basic.ApiRole.DepById + `/${this.record.id}`)
        .subscribe(res => {
          this.i = res;
        });
    }
    await this.GetInit();
  }

  save(value: any) {
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.AddOrEditDep, value).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
    });
  }

  close() {
    this.modal.destroy();
  }
}
