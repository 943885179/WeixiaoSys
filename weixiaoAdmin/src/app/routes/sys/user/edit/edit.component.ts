import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema } from '@delon/form';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
@Component({
  selector: 'app-sys-user-edit',
  templateUrl: './edit.component.html',
})
export class SysUserEditComponent implements OnInit {
  validateForm: FormGroup;
  record: any = {};
  i: any;
  constructor(
    private fb: FormBuilder,
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public http: HttpBasicService,
    private basic: BasicService
  ) {
    this.validateForm = fb.group({
      userName: [null, [Validators.required, Validators.pattern(/A/)]],
      password: [null, [Validators.required]],
      remember: [true],
    });

  }

  ngOnInit(): void {
    if (this.record.id > 0)
      this.http.get(this.basic.ApiUrl + this.basic.ApiRole.EmpById + `/${this.record.id}`).subscribe(res => (this.i = res));
  }

  save() {
    this.http.post(`/user/${this.record.id}`, this.i).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
    });
  }

  close() {
    this.modal.destroy();
  }
}
