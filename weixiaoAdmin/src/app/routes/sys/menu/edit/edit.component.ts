import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFSchemaEnumType, SFTextWidgetSchema } from '@delon/form';
import { BasicService } from 'src/app/service/basic.service';
import { of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { RSA } from '@shared/utils/RSA';

@Component({
  selector: 'app-sys-menu-edit',
  templateUrl: './edit.component.html',
})
export class SysMenuEditComponent implements OnInit {
  constructor(
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
    private basic: BasicService,
    private rsa: RSA
  ) {
  }
  record: any = {};
  test: any = {};
  i: any;
  schema: SFSchema = {
    properties: {
    }
  };
  ui: SFUISchema = {
    '*': {
      // spanLabelFixed: 100,
      grid: { span: 24 },
    },
    $id: {
      widget: 'text',
    },
    $text: {
      widget: 'string',
      // optional: "aaaaa",
      optionalHelp: '菜单名称',
    },
    $icon: {
      optionalHelp: '图标，只对一级菜单有用',
    },
    // $description: {
    //   widget: 'textarea',
    //   grid: { span: 24 },
    // },
  };
  async ngOnInit(): Promise<void> {
    if (this.record.id > 0) {
      await this.http
        .get(this.basic.ApiUrl + this.basic.ApiRole.MenuById + `/${this.record.id}`)
        .subscribe(res => {
          this.i = res;
          this.getSelectMenu(res);
        });
      // await this.getSelectMenu();
    }
    else {
      await this.getSelectMenu(null);
    }
  }

  async getSelectMenu(menu: any) {
    this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectMenu).subscribe(res => {
      this.schema = {
        properties: {
          id: {
            title: '编号', type: 'number',
            // tslint:disable-next-line: no-object-literal-type-assertion
            ui: { widget: 'text' } as SFTextWidgetSchema
          },
          text: { type: 'string', title: '菜单名称', maxLength: 15 },
          icon: { type: 'string', title: '图标' },
          pid: {
            type: 'string',
            title: '上级菜单',
            enum: res,
            // default: [],
            ui: {
              widget: 'tree-select',
              // checkable: true,
              // asyncData: () => [{}]
            },
          },
          i18n: { type: 'string', title: 'i18n' },
          link: { type: 'string', title: '路由' },
          externalLink: { type: 'string', title: '外部链接' },
          target: {
            type: `string`,
            title: '链接target',
            enum: [
              { label: '_blank', value: "_blank" },
              { label: '_self', value: "_self" },
              { label: '_parent', value: "_parent" },
              { label: '_top', value: "_top" }
            ],
            ui: {
              widget: 'radio',
              styleType: 'button',
              buttonStyle: 'solid',
            },
            default: menu == null ? "" : menu.target
          },
          disabled: {
            type: "boolean",
            title: "是否禁用",
            default: menu == null ? false : menu.disabled
          },
          hide: {
            type: "boolean",
            title: "是否隐藏",
            default: menu == null ? false : menu.hide
          }
        },
        required: ['text'],
      };
    });
  }
  save(value: any) {
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.AddOrEditMenu, { data: this.rsa.ApiLongEncrypt(JSON.stringify(value)) }).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
    });
  }

  close() {
    this.modal.destroy();
  }
}
