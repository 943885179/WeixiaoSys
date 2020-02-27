import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema } from '@delon/form';
import * as G6 from "@antv/g6";
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';
import { NzMessageService } from 'ng-zorro-antd';
import { ITEM_TYPE } from '@antv/g6/lib/types';
@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
    constructor(private http: HttpBasicService, private basic: BasicService, private msg: NzMessageService) { }
    schema: SFSchema = {
        properties: {
            name: {
                "type": "string"
            },
            password: {
                "type": "string",
                "ui": {
                    "type": "password"
                }
            }
        },
        "required": ["name", "password"]
    }
    graph: G6.Graph;
    type: ITEM_TYPE = "node"
    async  ngOnInit(): Promise<void> {
        this.http.get(this.basic.ApiUrl + "Flow/test").subscribe(res => {
            // tslint:disable-next-line: no-eval
            res.fLowGraph.width = eval(res.fLowGraph.width);
            // tslint:disable-next-line: no-eval
            res.fLowGraph.height = eval(res.fLowGraph.height);
            console.log(res.fLowGraph.modes);

            this.graph = new G6.Graph(res.fLowGraph);
            this.graph.data(res.flowData);
            res.ons.forEach((onFun: any) => {
                // tslint:disable-next-line: function-constructor
                this.graph.on(onFun.funName, new Function(onFun.funParameter, onFun.funBody));
            });
            this.graph.render();
        })
    }
    async save() {
        this.msg.info(JSON.stringify(this.graph.save()));
    }
    async add() {
        this.graph.add(this.type, { x: 50, y: 50 });
        this.graph.refresh();
    }
}
