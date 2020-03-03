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
            res.flowGraph.modes.addNode = ['click-add-node', 'click-select'];
            res.flowGraph.modes.addEdge = ['click-add-edge', 'click-select'];
            G6.registerBehavior('click-add-node', {
                getEvents() { return { 'canvas:click': 'onClick' }; },
                onClick(ev: any) {
                    console.log(this);

                    this.graph.addItem('node', {
                        x: ev.canvasX,
                        y: ev.canvasY,
                        id: `node-` + this.nodeIndex,
                        lable: `node-` + this.nodeIndex
                    });
                    alert(this.nodeIndex);
                    this.nodeIndex++;
                },
            });
            G6.registerBehavior('click-add-edge', {
                // Set the events and the corresponding responsing function for this behavior
                getEvents() {
                    return {
                        'node:click': 'onClick', // The event is canvas:click, the responsing function is onClick
                        'mousemove': 'onMousemove', // The event is mousemove, the responsing function is onMousemove
                        'edge:click': 'onEdgeClick', // The event is edge:click, the responsing function is onEdgeClick
                    };
                }, onClick(ev: any) {
                    const self = this;
                    const node = ev.item;
                    const graph = self.graph;
                    // The position where the mouse clicks
                    const point = { x: ev.x, y: ev.y };
                    const model = node.getModel();
                    if (self.addingEdge && self.edge) {
                        graph.updateItem(self.edge, {
                            target: model.id,
                        });

                        self.edge = null;
                        self.addingEdge = false;
                    } else {
                        // Add anew edge, the end node is the current node user clicks
                        self.edge = graph.addItem('edge', {
                            source: model.id,
                            target: point,
                        });
                        self.addingEdge = true;
                    }
                },
                // The responsing function for mousemove defined in getEvents
                onMousemove(ev: any) {
                    const self = this;
                    // The current position the mouse clicks
                    const point = { x: ev.x, y: ev.y };
                    if (self.addingEdge && self.edge) {
                        // Update the end node to the current node the mouse clicks
                        self.graph.updateItem(self.edge, {
                            target: point,
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
    async addNode() {
        this.graph.setMode("addNode");
    }
    async addEdge() {
        this.graph.setMode("addEdge");
    }
    async default() {
        this.graph.setMode("default");
    }
}
