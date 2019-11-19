import { Component, OnInit, ViewChild } from '@angular/core';
import { NzDrawerRef, NzMessageService, NzModalRef } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFTreeSelectWidgetSchema, SFCascaderWidgetSchema, SFStringWidgetSchema, SFTextWidgetSchema, SFArrayWidgetSchema } from '@delon/form';
import { BasicService } from 'src/app/service/basic.service';
import { CacheService } from '@delon/cache';

@Component({
  selector: 'app-sys-company-edit',
  templateUrl: './edit.component.html',
})
export class SysCompanyEditComponent implements OnInit {

  constructor(
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
    private basic: BasicService,
    private csv: CacheService,
    // private drawer: NzDrawerRef
  ) { }
  record: any = {};
  i: any;
  schema: SFSchema = {
    properties: {
    }
  };
  ui: SFUISchema = {
    '*': {
      grid: { span: 24 },
    },
    $Id: {
      widget: 'text'
    },
  };
  async ngOnInit(): Promise<void> {
    if (this.record.id > 0) {
      await this.http
        .get(this.basic.ApiUrl + this.basic.ApiRole.CompanyById + `/${this.record.id}`)
        .subscribe(res => {
          this.i = res;
          this.getSelectCompany(res);
        });
      // await this.getSelectMenu();
    }
    else {
      await this.getSelectCompany(null);
    }
  }
  async getSelectCompany(com: any) {

    this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectCompany).subscribe(resp => {
      this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectArea).subscribe(res => {
        this.schema = {
          properties: {
            id: {
              title: '编号', type: 'number',
              // tslint:disable-next-line: no-object-literal-type-assertion
              ui: { widget: 'text' } as SFTextWidgetSchema
            },
            name: { type: 'string', title: '姓名' },
            code: { type: 'string', title: '编码', maxLength: 15 },
            areaCasCarder: {
              type: 'string',
              title: '地址',
              enum: res,
              // default: [],
              // tslint:disable-next-line: no-object-literal-type-assertion
              ui: {
                widget: 'cascader',
                showSearch: true,
                showArrow: true,
                showInput: true,
              } as SFCascaderWidgetSchema,
              default: com == null ? [1, 2, 52] : com.area.split(',').map(Number)
              // ui: {
              //   widget: 'tree-select',
              //   showExpand: true,
              //   showLine: true,
              //   dropdownMatchSelectWidth: false,
              //   dropdownStyle: {
              //     // width: '100%',
              //     overflow: 'auto',
              //     height: '200px',
              //   },
              //   allowClear: true,
              //   // multiple: true,
              //   // checkable: true,
              //   // asyncData: () => [{}]
              // } as SFTreeSelectWidgetSchema,
            },
            address: { type: 'string', title: '详细地址' },
            legalPerson: { type: 'string', title: '法人' },
            idcard: { type: "string", title: '身份证', format: "id-card" },
            email: {
              type: 'string', title: '邮箱', format: "email",
              // tslint:disable-next-line: no-object-literal-type-assertion
              ui: {
                autocomplete: "on",
                // addOnAfter: '.@qq.com'
              } as SFStringWidgetSchema
            },
            phone: { type: 'string', title: '电话', format: "mobile" },// format: 'id-card',
            pid: {
              type: 'string', title: '上级',
              enum: resp,
              // tslint:disable-next-line: no-object-literal-type-assertion
              ui: {
                widget: 'tree-select'
              } as SFTreeSelectWidgetSchema
            },
            // Shareholder: {
            //   maxItems: 10,

            //   type: "array",
            //   title: "添加股东",
            //   items: {
            //     type: "object",
            //     properties: {
            //       name: {
            //         type: "string",
            //         title: "姓名",
            //       },
            //       proportion: {
            //         type: "number",
            //         title: "比例"
            //       },
            //       payMoney: {
            //         type: "number",
            //         title: "金额",
            //       }
            //     },
            //     required: ['name', 'proportion', 'payMoney'],
            //   },
            //   // tslint:disable-next-line: no-object-literal-type-assertion
            //   ui: { grid: { arraySpan: 12 }, addTitle: "添加股东", removeTitle: "移除" } as SFArrayWidgetSchema,
            // }
          },
          required: ['name', 'legalPerson', 'idcard', 'code'],
        };
      });
    });
  }
  save(value: any) {
    value.area = value.areaCasCarder.join();
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.AddOrEditCompany, value).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
      // this.drawer.close(true);
    });
  }
  close() {
    this.modal.destroy();
  }
}
