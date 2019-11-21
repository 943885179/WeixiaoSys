import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFArrayWidgetSchema, SFNumberWidgetSchema } from '@delon/form';
import { BasicService } from 'src/app/service/basic.service';
import { promise } from 'protractor';

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

  async ngOnInit(): Promise<void> {
    if (this.record.id > 0) {
      this.i = this.record;
      await this.http.get(this.basic.ApiUrl + this.basic.ApiRole.ShareholderByCid + `/${this.record.id}`).subscribe(res => {
        this.i.shareholder = res;

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
                    title: "比例",
                    // tslint:disable-next-line: no-object-literal-type-assertion
                    ui: {
                      unit: '%',
                      precision: 2,
                      grid: {
                        span: 24
                      }
                    } as SFNumberWidgetSchema
                  },
                  payMoney: {
                    type: "number",
                    title: "金额",
                    // tslint:disable-next-line: no-object-literal-type-assertion
                    ui: {
                      prefix: '￥',
                      precision: 4,
                      grid: {
                        span: 24
                      }
                    } as SFNumberWidgetSchema
                  },
                  idcard: {
                    type: "string", title: '身份证', format: "id-card",
                    ui: {
                      grid: {
                        span: 24
                      }
                    }
                  },
                },
                required: ['name', 'proportion', 'payMoney', 'idcard'],
              },
              // tslint:disable-next-line: no-object-literal-type-assertion
              ui: { grid: { arraySpan: 24 }, addTitle: "添加股东", removeTitle: "移除" } as SFArrayWidgetSchema,
            }
          },
          required: ['owner', 'callNo', 'href', 'description'],
        };
      });

      /*this.http.get(this.basic.ApiUrl + this.basic.ApiRole.GetShareHolderByComId).subscribe(res => {
        this.i = res;
      });*/
    }
  }

  save(value: any) {
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.AddOrEditCompany + '/1', value).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
    });
  }

  close() {
    this.modal.destroy();
  }
}
