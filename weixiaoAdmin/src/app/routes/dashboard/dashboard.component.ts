import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema } from '@delon/form';
import { Graph } from "@antv/g6";
import G6 from '@antv/g6';
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
    graph: Graph;
    type: ITEM_TYPE = "node";
    nodeIndex = 0;
    async  ngOnInit(): Promise<void> {
        this.http.get(this.basic.ApiUrl + "Flow/test").subscribe(res => {
            console.log(res.flowGraph);

            // tslint:disable-next-line: no-eval
            res.flowGraph.width = eval(res.flowGraph.width);
            // tslint:disable-next-line: no-eval
            res.flowGraph.height = eval(res.flowGraph.height);
            res.flowGraph.modes.default.forEach(mode => {
                if (mode.type === "tooltip") {
                    // tslint:disable-next-line: no-eval
                    mode.formatText = eval(mode.formatText);
                }
            });
            res.flowGraph.modes.addFlow = ['addFlow', 'click-select'];
            G6.registerBehavior('addFlow', {
                getEvents() {
                    return {
                        'canvas:click': 'onClickCanvas',// 点击界面生成节点
                        'node:click': 'onClickNode',// 点击节点开始生成线路
                        'mousemove': 'onMousemove',// 移动时候绘画线路
                        'edge:click': 'onEdgeClick',// 中途停止时候删除线路
                    };
                }, onClickCanvas(ev: any) {
                    if (this.nodeIndex === undefined) {
                        this.nodeIndex = 1;
                    }
                    this.graph.addItem('node', {
                        x: ev.canvasX,
                        y: ev.canvasY,
                        id: `node-` + this.nodeIndex,
                        label: `node-` + this.nodeIndex,
                        size: 60
                    });
                    this.nodeIndex++;
                }, onClickNode(ev: any) {
                    if (this.addingEdge && this.edge) {
                        this.graph.updateItem(this.edge, {
                            target: ev.item.getModel().id,
                        });
                        this.edge = null;
                        this.addingEdge = false;
                    } else {
                        this.edge = this.graph.addItem('edge', {
                            type: "polyline",
                            source: ev.item.getModel().id,
                            target: { x: ev.x, y: ev.y },
                            style: {
                                //fill: `yellow`,
                                //stroke: `red`,
                                lineWidth: 3
                            }
                        });
                        this.addingEdge = true;
                    }
                },
                // The responsing function for mousemove defined in getEvents
                onMousemove(ev: any) {
                    if (this.addingEdge && this.edge) {
                        // Update the end node to the current node the mouse clicks
                        this.graph.updateItem(this.edge, {
                            target: { x: ev.x, y: ev.y },
                        });
                    }
                },
                // The responsing function for edge:click defined in getEvents
                onEdgeClick(ev: any) {
                    const self = this;
                    const currentEdge = ev.item;
                    if (self.addingEdge && self.edge === currentEdge) {
                        self.graph.removeItem(self.edge);
                        self.edge = null;
                        self.addingEdge = false;
                    }
                },
            });
            this.graph = new G6.Graph(res.flowGraph);

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
    async add(type: string) {
        this.graph.add(this.type, { shape: type, id: this.type + this.nodeIndex, label: type + this.nodeIndex, x: 100, y: 100 });
        this.nodeIndex++;
        this.graph.refresh();
    }
    async addFlow() {
        this.graph.setMode("addFlow");
    }
    async default() {
        this.graph.setMode("default");
    }
}
