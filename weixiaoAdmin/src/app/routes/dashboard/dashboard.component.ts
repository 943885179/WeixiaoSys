import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema } from '@delon/form';
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

  constructor(private http: _HttpClient) { }

  ngOnInit() {
  }

}
