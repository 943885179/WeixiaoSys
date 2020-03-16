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
    async  ngOnInit(): Promise<void> {
        let conextMenuContainer = await this.createMenu();
        this.http.get(this.basic.ApiUrl + "Flow/test").subscribe(res => {
            insertCss(res.flowStyle.insertCss);
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
            //加载registerEdges
            res.registerEdges.forEach((registerEdge: any) => {
                G6.registerEdge(registerEdge.shapeType, {
                    // tslint:disable-next-line: function-constructor
                    draw: (cfg, group) => new Function(registerEdge.shapeOptions.draw.funParameter, registerEdge.shapeOptions.draw.funBody)(cfg, group)
                });
            });

            G6.registerBehavior('addFlow', {
                getEvents() {
                    return {
                        'canvas:click': 'onClickCanvas',// 点击界面生成节点
                        'node:click': 'onClickNode',// 点击节点开始生成线路
                        'mousemove': 'onMousemove',// 移动时候绘画线路
                        'edge:click': 'onEdgeClick',// 中途停止时候删除线路
                        'node:contextmenu': 'nodeContextmenu',// 右键节点
                        'node:mouseleave': 'nodeMouseleave',
                    };
                }, onClickCanvas(ev: any) {
                    if (this.nodeIndex === undefined) {
                        this.nodeIndex = 1;
                    }
                    this.graph.addItem('node', {
                        x: ev.canvasX,
                        y: ev.canvasY,
                        shape: 'rect',
                        id: `node-` + this.nodeIndex,
                        label: `node-` + this.nodeIndex,
                        size: 60
                    });
                    this.nodeIndex++;
                }, onClickNode(ev: any) {
                    (document.getElementById('itemId') as HTMLInputElement).value = ev.item.getModel().id;
                    (document.getElementById('itemLabel') as HTMLInputElement).value = ev.item.getModel().label;
                    if (this.addingEdge && this.edge) {
                        this.graph.updateItem(this.edge, {
                            target: ev.item.getModel().id,
                        });
                        this.edge = null;
                        this.addingEdge = false;
                    } else {
                        this.edge = this.graph.addItem('edge', {
                            type: "line",
                            source: ev.item.getModel().id, ourceAnchor: true, targetAnchor: true,
                            target: { x: ev.x, y: ev.y },
                            style: {
                                fill: `#333`,
                                stroke: `#333`,
                                lineWidth: 3
                            }
                        });
                        this.addingEdge = true;
                    }
                },
                onMousemove(ev: any) {
                    if (this.addingEdge && this.edge) {
                        this.graph.updateItem(this.edge, {
                            target: { x: ev.x, y: ev.y },
                        });
                    }
                },
                onEdgeClick(ev: any) {
                    const self = this;
                    const currentEdge = ev.item;
                    if (self.addingEdge && self.edge === currentEdge) {
                        self.graph.removeItem(self.edge);
                        self.edge = null;
                        self.addingEdge = false;
                    }
                },
                nodeContextmenu(ev: any) {
                    conextMenuContainer.setAttribute("node", ev.item.getModel().id);
                    ev.preventDefault();
                    ev.stopPropagation();
                    conextMenuContainer.style.left = `${ev.canvasX}px`;
                    conextMenuContainer.style.top = `${ev.canvasY}px`;// clientY
                }, nodeMouseleave(ev: any) {
                    conextMenuContainer.removeAttribute("node");
                    conextMenuContainer.style.left = '-150px';
                }
            });
            console.log(conextMenuContainer);

            res.flowGraph.modes.addFlow = ['addFlow', 'click-select'];
            res.flowGraph.modes.Test = ['Test', 'click-select'];
            this.graph = new G6.Graph(res.flowGraph);
            this.graph.data(res.flowData);
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
            res.ons.forEach((onFun: any) => {
                // tslint:disable-next-line: function-constructor
                this.graph.on(onFun.funName, new Function(onFun.funParameter, onFun.funBody));
            });
            this.graph.render();
        })
    }
    async createMenu() {
        const conextMenuContainer = document.createElement('ul');
        conextMenuContainer.id = 'contextMenu';
        const removeLi = document.createElement('li');
        removeLi.innerText = '删除';
        removeLi.onclick = () => {
            this.graph.removeItem(conextMenuContainer.getAttribute("node"));
            this.graph.refresh();
            conextMenuContainer.removeAttribute("node");
            conextMenuContainer.style.left = '-150px';
        };
        conextMenuContainer.appendChild(removeLi);
        const firstLi = document.createElement('li');
        firstLi.innerText = '开始/结束';
        firstLi.onclick = () => {
            this.graph.updateItem(conextMenuContainer.getAttribute("node"), { shape: "ellipse", size: [60, 40] });
            this.graph.refreshItem(conextMenuContainer.getAttribute("node"));
        };
        conextMenuContainer.appendChild(firstLi);
        const diamond = document.createElement('li');
        diamond.innerText = '判断';
        diamond.onclick = () => {
            this.graph.updateItem(conextMenuContainer.getAttribute("node"), { shape: "diamond", size: 60 });
            this.graph.refreshItem(conextMenuContainer.getAttribute("node"));
        };
        conextMenuContainer.appendChild(diamond);
        const rect = document.createElement('li');
        rect.innerText = '过程';
        rect.onclick = () => { this.graph.updateItem(conextMenuContainer.getAttribute("node"), { shape: "rect", size: 60 }); this.graph.refreshItem(conextMenuContainer.getAttribute("node")); };
        conextMenuContainer.appendChild(rect);
        document.getElementById('mountNode').appendChild(conextMenuContainer);
        return conextMenuContainer;
    }
    async save() {
        this.msg.info(JSON.stringify(this.graph.save()));
    }
    async addFlow() {
        this.graph.setMode("Test");
    }
    async default() {
        this.graph.setMode("default");
    }
}
