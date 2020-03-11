import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema } from '@delon/form';
import { Graph } from "@antv/g6";
import G6 from '@antv/g6';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';
import { NzMessageService } from 'ng-zorro-antd';
import { ITEM_TYPE } from '@antv/g6/lib/types';
import * as insertCss from 'insert-css';
import { log } from 'util';
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
  thisNodeId = "";
  nodeIndex = 0;
  async  ngOnInit(): Promise<void> {
    await this.insertCSS();
    const conextMenuContainer = document.createElement('ul');
    conextMenuContainer.id = 'contextMenu';
    const firstLi = document.createElement('li');
    firstLi.innerText = '开始/结束';
    firstLi.onclick = () => { console.log(this.graph); this.graph.updateItem("node-1", { shape: "ellipse" }); this.graph.refreshItem("node-1"); };
    conextMenuContainer.appendChild(firstLi);

    const lastLi = document.createElement('li');
    lastLi.innerText = 'Option 2';
    conextMenuContainer.appendChild(lastLi);
    document.getElementById('mountNode').appendChild(conextMenuContainer);
    this.http.get(this.basic.ApiUrl + "Flow/test").subscribe(res => {
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
            'node:contextmenu': 'nodeContextmenu',//右键节点
            'node:mouseleave': 'nodeMouseleave',
          };
        }, onClickCanvas(ev: any) {
          if (this.nodeIndex === undefined) {
            this.nodeIndex = 1;
          }
          this.graph.addItem('node', {
            x: ev.canvasX,
            y: ev.canvasY,
            shape:'rect',
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
          console.log(ev);
          this.thisNodeId = ev.item.getModel().id;
          ev.preventDefault();
          ev.stopPropagation();
          conextMenuContainer.style.left = `${ev.clientX}px`;
          conextMenuContainer.style.top = `${ev.clientY}px`;
        }, nodeMouseleave(ev: any) {
          this.thisNodeId = "";
          conextMenuContainer.style.left = '-150px';
        }
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
  async Test() {
    alert(123);
  }
  async insertCSS() {
    insertCss(`
      /*.g6-tooltip {
          border: 1px solid #e2e2e2;
          border-radius: 4px;
          font-size: 12px;
          color: #545454;
          background-color: rgba(255, 255, 255, 0.9);
          padding: 10px 8px;
          box-shadow: rgb(174, 174, 174) 0px 0px 10px;
        }*/
        #contextMenu {
          position: absolute;
          list-style-type: none;
          padding: 10px 8px;
          left: -150px;
          background-color: rgba(255, 255, 255, 0.9);
          border: 1px solid #e2e2e2;
          border-radius: 4px;
          font-size: 12px;
          color: #545454;
        }
        #contextMenu li {
          cursor: pointer;
		      list-style-type:none;
          list-style: none;
          margin-left: 0px;
        }
        #contextMenu li:hover {
          color: #aaa;
        }
      `);
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
