import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { STColumn, STComponent } from '@delon/abc';
import { SFSchema } from '@delon/form';
import { SysMenuEditComponent } from './edit/edit.component';
import { CacheService } from '@delon/cache';
import { from } from 'rxjs';
import { MenuService } from 'src/app/service/menu.service';
import { BasicService } from 'src/app/service/basic.service';
import { type } from 'os';
@Component({
  selector: 'app-sys-menu',
  templateUrl: './menu.component.html',
})
export class SysMenuComponent implements OnInit {
  url: string;
  searchSchema: SFSchema = {
    properties: {
      id: {
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
          console.log(filter, record);
          return !filter.value || record.name.indexOf(filter.value) !== -1;
        },
      },
    },
    { title: '图标', index: 'icon', format: (item, col, index) => item.icon == null ? "" : `<i class='` + item.icon + `'></i> ` + item.icon },
    {
      title: '操作',
      buttons: [
        { text: '查看', icon: "search", click: (item: any) => `view/${item.id}` },
        { text: "删除", icon: "delete", type: "del" },
        {
          text: '编辑',
          icon: 'edit',
          type: "static",
          component: SysMenuEditComponent,
          click: (item: any) => item
        },

      ]
    }
  ];

  constructor(private http: _HttpClient, private modal: ModalHelper, private csv: CacheService, private menuService: MenuService, private basic: BasicService) {

  }

  ngOnInit() {
    this.url = this.basic.ApiUrl + this.basic.ApiRole.Menus;
    // this.data=this.menuService.getMenu()
  }

  add() {
    this.modal
      .createStatic(SysMenuEditComponent, { i: { id: 0 } })
      .subscribe(() => this.st.reload());
  }

}
