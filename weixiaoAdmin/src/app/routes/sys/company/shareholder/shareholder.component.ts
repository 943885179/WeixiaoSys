import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFArrayWidgetSchema } from '@delon/form';
import { BasicService } from 'src/app/service/basic.service';

@Component({
  selector: 'app-sys-company-shareholder',
  templateUrl: './shareholder.component.html',
})
export class SysCompanyShareholderComponent implements OnInit {
  record: any = {};
  i: any;
  schema: SFSchema = {
    properties: {}
  };
  ui: SFUISchema = {
    '*': {
      grid: { span: 24 },
    },
  };
  constructor(
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
    private basic: BasicService
  ) { }

  ngOnInit(): void {
    if (this.record.id > 0) {
      this.i = this.record;
      /*this.http.get(this.basic.ApiUrl + this.basic.ApiRole.GetShareHolderByComId).subscribe(res => {
        this.i = res;
      });*/
      this.schema = {
        properties: {
          shareholder: {
            maxItems: 10,
            type: "array",
            title: "添加股东",
            items: {
              type: "object",
              properties: {
                name: {
                  type: "string",
                  title: "姓名",
                },
                proportion: {
                  type: "number",
                  title: "比例"
                },
                payMoney: {
                  type: "number",
                  title: "金额",
                }
              },
              required: ['name', 'proportion', 'payMoney'],
            },
            // tslint:disable-next-line: no-object-literal-type-assertion
            ui: { grid: { arraySpan: 12 }, addTitle: "添加股东", removeTitle: "移除" } as SFArrayWidgetSchema,
          }
        },
        required: ['owner', 'callNo', 'href', 'description'],
      };
    }
  }

  save(value: any) {
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.AddOrEditCompany, value).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
    });
  }

  close() {
    this.modal.destroy();
  }
}
