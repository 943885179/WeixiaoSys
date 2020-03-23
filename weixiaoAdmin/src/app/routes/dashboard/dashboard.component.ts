import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema } from '@delon/form';
import { Graph } from "@antv/g6";
import G6 from '@antv/g6';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';
import { NzMessageService } from 'ng-zorro-antd';
import { ITEM_TYPE, IShapeBase } from '@antv/g6/lib/types';
import * as insertCss from 'insert-css';
import { log } from 'util';
@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
    constructor(private http: HttpBasicService, private basic: BasicService, private msg: NzMessageService) { }
    graph: Graph;
    type: ITEM_TYPE = "node";
    item = {
        id: "",
        label: "",
        type: "rect"
    }
    btnList = [];
    async  ngOnInit(): Promise<void> {
        this.http.get(this.basic.ApiUrl + "Flow/test").subscribe(res => {
            insertCss(res.flowCss.insertCss);
            // tslint:disable-next-line: no-eval
            res.flowGraph.width = eval(res.flowGraph.width);
            // tslint:disable-next-line: no-eval
            res.flowGraph.height = eval(res.flowGraph.height);
            // tslint:disable-next-line: forin
            for (const key in res.flowGraph.modes) {
                this.btnList.push(key);
                res.flowGraph.modes[key].forEach(mode => {
                    if (mode.type === "tooltip") {
                        // tslint:disable-next-line: no-eval
                        mode.formatText = eval(mode.formatText);
                    }
                });
            }
            // 加载registerEdges
            res.registerEdges.forEach((registerEdge: any) => {
                G6.registerEdge(registerEdge.shapeType, {
                    // tslint:disable-next-line: function-constructor
                    draw: (cfg, group) => new Function(registerEdge.shapeOptions.draw.funParameter, registerEdge.shapeOptions.draw.funBody)(cfg, group)
                });
            });
            // 加载registerBehavior
            res.registerBehaviors.forEach((registerBehavior: any) => {
                let getEvents = ``;
                let funList = ``;
                // tslint:disable-next-line: forin
                for (const key in registerBehavior.behavior) {
                    getEvents += `'${key}':'${registerBehavior.behavior[key].funName}',`;
                    funList += `${registerBehavior.behavior[key].funName}(${registerBehavior.behavior[key].funParameter}){${registerBehavior.behavior[key].funBody}},`;
                }
                const funStr = `G6.registerBehavior('${registerBehavior.type}',{getEvents() {return {${getEvents}};},${funList}});`;
                // tslint:disable-next-line: function-constructor
                new Function(`G6,graph`, `${funStr}`)(G6, this.graph);
            });
            this.graph = new G6.Graph(res.flowGraph);
            this.graph.data(res.flowData);
            res.ons.forEach((onFun: any) => {
                // tslint:disable-next-line: function-constructor
                this.graph.on(onFun.funName, new Function(onFun.funParameter, onFun.funBody));
            });
            // 前置任务处理
            res.flowFronts.forEach(front => {
                // tslint:disable-next-line: function-constructor
                this.item = new Function(front.funParameter, front.funBody)(this.item, this.graph);
            });
            this.graph.render();
        })
    }
    async save() {
        this.msg.info(JSON.stringify(this.graph.save()));
    }
    async changeMode(mode) {
        this.graph.setMode(mode);
    }
}
