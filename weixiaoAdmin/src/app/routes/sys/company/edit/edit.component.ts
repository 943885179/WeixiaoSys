import { Component, OnInit, ViewChild } from '@angular/core';
import { NzDrawerRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFTreeSelectWidgetSchema, SFCascaderWidgetSchema } from '@delon/form';
import { BasicService } from 'src/app/service/basic.service';
import { CacheService } from '@delon/cache';

@Component({
  selector: 'app-sys-company-edit',
  templateUrl: './edit.component.html',
})
export class SysCompanyEditComponent implements OnInit {

  constructor(
    // private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
    private basic: BasicService,
    private csv: CacheService,
    private drawer: NzDrawerRef
  ) { }
  record: any = {};
  i: any;
  schema: SFSchema = {
    properties: {
    }
  };
  ui: SFUISchema = {
    '*': {
      spanLabelFixed: 100,
      grid: { span: 12 },
    },
    $no: {
      widget: 'text'
    },
    $href: {
      widget: 'string',
    },
    $description: {
      widget: 'textarea',
      grid: { span: 24 },
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
  async getSelectCompany(menu: any) {
    this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectArea).subscribe(res => {
      this.schema = {
        properties: {
          name: { type: 'string', title: '姓名' },
          code: { type: 'string', title: '编码', maxLength: 15 },
          area: {
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
          legal_person: { type: 'string', title: '法人' },
          email: { type: 'string', title: '邮箱' },
          phone: { type: 'string', title: '电话', format: "mobile" },
          pid: { type: 'string', title: '上级Id' },
        },
        required: ['name'],
      };
    });
  }
  save(value: any) {
    value.area = value.area.join();
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.AddOrEditCompany, value).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.drawer.close(true);
    });
  }
}
