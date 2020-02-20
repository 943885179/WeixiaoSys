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

    async  ngOnInit(): Promise<void> {

        /* //const graph = new G6.Graph({ container: 'mountNode', width: 1000, height: 1000, }); graph.data(this.data); graph.render();

         //let sum = new Function('a', 'b', 'return a + b');
         //alert(sum(1, 2)); // 3
         var temptest = `{"container": "mountNode","width": 1000, "height": 1000}`;
         //var graph = new G6.Graph(eval("(" + temptest + ")")); //可以
         var graph = new G6.Graph(JSON.parse(temptest));//也可以
         //(new Function("G6", temptest))();
         graph.data(this.data);
         const response = await fetch('https://gw.alipayobjects.com/os/basement_prod/6cae02ab-4c29-44b2-b1fd-4005688febcb.json');
         const remoteData = await response.json();

         graph.render();*/
        await this.http.get(this.basic.ApiUrl + "Flow/test").subscribe(res => {
            console.log(res);
        })
    }

}
