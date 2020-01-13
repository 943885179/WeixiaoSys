import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient, ModalHelper, MenuService } from '@delon/theme';
import { STColumn, STComponent, STReq, STChange, STReqReNameType, STRequestOptions } from '@delon/abc';
import { SFSchema, SFButton } from '@delon/form';
import { SysMenuEditComponent } from './edit/edit.component';
import { CacheService } from '@delon/cache';
import { from } from 'rxjs';
import { BasicService } from 'src/app/service/basic.service';
import { type } from 'os';
import { SysMenuViewComponent } from './view/view.component';
import { NzMessageService } from 'ng-zorro-antd';
import { RSA } from '@shared/utils/RSA';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { StartupService } from '@core';
@Component({
  selector: 'app-sys-menu',
  templateUrl: './menu.component.html',
})
export class SysMenuComponent implements OnInit {
  constructor(private startupSrv: StartupService, private message: NzMessageService, private http: HttpBasicService, private modal: ModalHelper, private csv: CacheService, private menuService: MenuService, private basic: BasicService, private rsa: RSA) {
    this.req = http.req;
  }
  req: STReq = {}
  url: string;
  searchSchema: SFSchema = {
    properties: {
      text: {
        type: 'string',
        title: '菜单名称',
        //  description: `支持模糊查询`
      },
    }
  };
  @ViewChild('st', { static: false }) st: STComponent;
  columns: STColumn[] = [
    {
      title: '编号',
      index: 'id',
      type: "checkbox",
      selections: [
        {
          text: "一级菜单",
          select: data => data.forEach(item => item.checked = item.pid == null)
        },
        {
          text: "非一级菜单",
          select: data => data.forEach(item => item.checked = item.pid != null)
        }
      ]
    },
    {
      title: '名称', index: 'text',
      sort: {
        compare: (a, b) => a.name.length - b.name.length,
      },
      filter: {
        type: 'keyword',
        fn: (filter, record) => {
          return !filter.value || record.name.indexOf(filter.value) !== -1;
        },
      },
    },
    { title: { text: '图标', optionalHelp: `https://ng.ant.design/components/icon/zh#components-icon-demo-basic` }, render: 'icon', },
    // { title: '图标', index: 'icon', format: (item, col, index) => item.icon == null ? "" : `<i class='${item.icon}'></i> ${item.icon}` },
    {
      title: '操作',
      buttons: [
        {
          text: '查看', icon: "search", type: "drawer", drawer: {
            component: SysMenuViewComponent,
            title: "详情",
            params: (item) => {
              return { id: item.Id };
            }

          }
        },
        {
          text: "删除",
          icon: "delete",
          type: "del",
          pop: {
            title: "确认删除吗？", trigger: "click", placement: "bottomRight",
            okType: 'danger',
            icon: 'delete',
          },
          iif: record => record.children.length === 0,
          click: (record, _modal, comp) => {
            this.http.post(this.basic.ApiUrl + this.basic.ApiRole.DeleteMenu + `/${record.id}`).subscribe(res => {
              if (res != null) {
                this.message.success(`成功删除【${record.text}】`);
                comp!.removeRow(record);
                this.startupSrv.load()
              }
            })
          },
        },
        {
          text: '编辑',
          icon: 'edit',
          type: "static",
          // component: SysMenuEditComponent,
          modal: {
            component: SysMenuEditComponent,
            params: (item: any) => item
          },
          click: (record, modal, ins) => {
            this.st.reload();
            this.startupSrv.load()
          }
        },

      ]
    }
  ];
  changeMenus: any = [];
  // header: any = {
  //   "Content-Type": "application/json"
  // }
  // rname: STReqReNameType = {
  //   "pi"
  // }
  change(e: STChange) {
    if (e.type === 'checkbox') {
      this.changeMenus = [];
      for (const item of e.checkbox) {
        this.changeMenus.push({ id: item.id });
      }
    }
    // this.changeMenus = e.checkbox;
    // tslint:disable-next-line: forin
    // e.checkbox.forEach(x => {
    //   this.changeMenus.push({ id: x.id });
    //   console.log(this.changeMenus);
    // })
    // console.log('change', e.checkbox);
  }
  async deleteAll() {
    if (this.changeMenus == null || this.changeMenus.length === 0) {
      this.message.error("请选择数据");
      return;
    }
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.DeleteMenus, this.changeMenus).subscribe(res => {
      this.message.success("删除成功");
      this.st.reload();
      this.startupSrv.load()
    })
    // 使用_httpClient
    // this.http.post(this.basic.ApiUrl + this.basic.ApiRole.DeleteMenus, { data: this.rsa.ApiEncrypt(JSON.stringify(this.changeMenus)) }).subscribe(res => {
    //   this.message.success("删除成功");
    //   this.st.reload();
    // })
  }

  ngOnInit() {
    this.url = this.basic.ApiUrl + this.basic.ApiRole.Menus;
    // this.data=this.menuService.getMenu()
  }

  add() {
    this.modal
      .createStatic(SysMenuEditComponent, { i: { id: 0 } })
      .subscribe(() => {
        this.st.reload();
        this.startupSrv.load()
      });
  }

}
