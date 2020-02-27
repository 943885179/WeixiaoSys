import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { STColumn, STComponent, STReq, STRequestOptions, STChange } from '@delon/abc';
import { SFSchema } from '@delon/form';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';
import { RSA } from '@shared/utils/RSA';
import { SysDepEditComponent } from './edit/edit.component';
import { NzMessageService } from 'ng-zorro-antd';
import { SysDepViewComponent } from './view/view.component';

@Component({
    selector: 'app-sys-dep',
    templateUrl: './dep.component.html',
})
export class SysDepComponent implements OnInit {

    constructor(private http: HttpBasicService, private message: NzMessageService, private basic: BasicService, private modal: ModalHelper, private rsa: RSA) {
        this.req = http.req;
    }
    url = "";
    myContext = { $implicit: 'World', localSk: 'Svet' };
    changeDeps: any = [];
    req: STReq = {};
    i: any;
    searchSchema: SFSchema = {
        properties: {
            depName: {
                type: 'string',
                title: '部门名称'
            }
        }
    };
    @ViewChild('st', { static: false }) st: STComponent;
    columns: STColumn[] = [
        {
            title: '编号',
            index: 'id',
            type: "checkbox"
        },
        {
            title: '名称', index: 'depName',
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
        { title: "编码", index: "depCode" },
        {
            title: '操作',
            buttons: [
                {
                    text: '查看', icon: "search", type: "drawer", drawer: {
                        component: SysDepViewComponent,
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
                        component: SysDepEditComponent,
                        params: (item: any) => item
                    },
                    click: () => {
                        location.reload();
                    }
                },

            ]
        }
    ];

    async ngOnInit() {
        await this.sechTree();
        this.url = this.basic.ApiUrl + this.basic.ApiRole.Deps;
    }

    change(e: STChange) {
        if (e.type === 'checkbox') {
            this.changeDeps = [];
            for (const item of e.checkbox) {
                this.changeDeps.push({ id: item.id });
            }
        }
    }
    async deleteAll() {
        if (this.changeDeps == null || this.changeDeps.length === 0) {
            this.message.error("请选择数据");
            return;
        }
        this.http.post(this.basic.ApiUrl + this.basic.ApiRole.DeleteDeps, this.changeDeps).subscribe(res => {
            this.message.success("删除成功");
            // this.st.reload();
            location.reload();
        })
        // 使用_httpClient
        // this.http.post(this.basic.ApiUrl + this.basic.ApiRole.DeleteMenus, { data: this.rsa.ApiEncrypt(JSON.stringify(this.changeMenus)) }).subscribe(res => {
        //   this.message.success("删除成功");
        //   this.st.reload();
        // })
    }
    add() {
        this.modal
            .createStatic(SysDepEditComponent, { i: { id: 0 } })
            .subscribe(() => location.reload());
    }
    async sechTree(): Promise<void> {
        await this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectDep).subscribe(res => {
            this.i = res;
        });
    }
}
