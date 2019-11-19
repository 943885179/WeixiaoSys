import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient, ModalHelper, DrawerHelper } from '@delon/theme';
import { STColumn, STComponent, STReq, STChange } from '@delon/abc';
import { SFSchema } from '@delon/form';
import { SysCompanyEditComponent } from './edit/edit.component';
import { BasicService } from 'src/app/service/basic.service';
import { NzMessageService } from 'ng-zorro-antd';
import { SysCompanyViewComponent } from './view/view.component';
import { SysCompanyShareholderComponent } from './shareholder/shareholder.component';

@Component({
  selector: 'app-sys-company',
  templateUrl: './company.component.html',
})
export class SysCompanyComponent implements OnInit {
  url = ``;
  searchSchema: SFSchema = {
    properties: {
      name: {
        type: 'string',
        title: '编号'
      }
    }
  };
  @ViewChild('st', { static: false }) st: STComponent;
  columns: STColumn[] = [
    {
      title: '编号',
      index: 'id',
      type: "checkbox",
    },
    {
      title: '名称', index: 'name',
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
    { title: "编码", index: 'code' },
    { title: { text: '法人' }, index: "legalPerson" },
    { title: { text: '法人身份证' }, index: "idcard" },
    // { title: '图标', index: 'icon', format: (item, col, index) => item.icon == null ? "" : `<i class='${item.icon}'></i> ${item.icon}` },
    {
      title: '操作',
      buttons: [
        {
          text: '查看', icon: "search", type: "drawer", drawer: {
            component: SysCompanyViewComponent,
            title: "详情",
            params: (item) => {
              console.log(item);
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
              }
            })
          },
        },
        {
          text: '编辑',
          icon: 'edit',
          type: "modal",
          modal: {
            component: SysCompanyEditComponent,
            params: (item: any) => item
          },
          click: 'reload'
        },
        {
          text: "添加投资人",
          icon: "anticon anticon-folder-add",
          type: "modal",
          modal: {
            component: SysCompanyShareholderComponent,
            params: item => item
          }
        }

      ]
    }
  ];
  req: STReq = {
    method: "post",
    allInBody: true,
  };
  changeMenus: any = [];
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
    })
  }
  constructor(private http: _HttpClient, private modal: ModalHelper, private drawer: DrawerHelper, private basic: BasicService, private message: NzMessageService) {

  }

  ngOnInit() {
    this.url = this.basic.ApiUrl + this.basic.ApiRole.Companys;
  }

  add() {
    this.modal
      .createStatic(SysCompanyEditComponent, { i: { id: 0 } })
      .subscribe(() => this.st.reload());

    // this.modal.open(SysCompanyEditComponent, { i: { id: 0 } }, "lg", { nzTitle: "添加" })
    //   .subscribe(() => this.st.reload());
    // this.drawer.create("添加", SysCompanyEditComponent, { i: { id: 0 } }, { drawerOptions: { nzPlacement: "right", nzClosable: true } })
    //   .subscribe(() => this.st.reload());
  }

}
