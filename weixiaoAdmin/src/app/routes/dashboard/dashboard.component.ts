import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema } from '@delon/form';
import * as G6 from "@antv/g6";
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';
@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
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
    constructor(private http: HttpBasicService, private basic: BasicService) { }
    graph;
    async  ngOnInit(): Promise<void> {
        this.http.get(this.basic.ApiUrl + "Flow/test").subscribe(res => {
            this.graph = new G6.Graph(res.fLowGraph);
            this.graph.data(res.flowData);
            res.ons.forEach((onFun: any) => {
                // tslint:disable-next-line: function-constructor
                this.graph.on(onFun.funName, new Function(onFun.funParameter, onFun.funBody));
            });
            this.graph.render();
            // console.log(graph.save()); 获取当前数据
        })
    }

}
