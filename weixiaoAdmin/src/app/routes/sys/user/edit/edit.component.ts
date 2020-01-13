import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService, NzTreeNodeOptions, UploadFile } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema } from '@delon/form';
import { HttpBasicService } from '@shared/utils/http-basic.service';
import { BasicService } from 'src/app/service/basic.service';
import { FormGroup, FormControl } from '@angular/forms';
import { Observable, Observer } from 'rxjs';
@Component({
  selector: 'app-sys-user-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class SysUserEditComponent implements OnInit {
  constructor(
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public http: HttpBasicService,
    private basic: BasicService,
    private msg: NzMessageService
  ) { }
  record: any = {};
  i: any;
  action: string;
  img: string;
  dep: any = [{ title: "选择部门", key: '0' }];
  role: any = [{ title: "选择角色", key: '0' }];
  loading = false;
  async ngOnInit(): Promise<void> {
    this.action = this.basic.ApiUrl + this.basic.ApiRole.upload;
    if (this.record.id > 0) {
      this.i = this.record;
      if (this.i.img != null) {
        this.img = this.basic.serverUrl + this.i.img;
      }
      this.i.empRoleIds = this.i.empRole.map(item => item.roleId);
    }
    await this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectDep).subscribe(res => { this.dep = res; })
    await this.http.get(this.basic.ApiUrl + this.basic.ApiRole.SelectRole).subscribe(res => { this.role = res; })
  }
  save(value: any) {
    const data = this.i;
    const oldRole = this.i.empRole.map(item => item.roleId);
    const noAdd = this.i.empRoleIds.filter(x => !oldRole.includes(x));
    data.empRole = this.i.empRole.filter(x => this.i.empRoleIds.includes(x.roleId))
    noAdd.forEach(rid => {
      data.empRole.push({ roleId: rid, empId: this.i.id });
    });
    this.http.post(this.basic.ApiUrl + this.basic.ApiRole.AddOrEditEmp, data).subscribe(res => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
    });
  }
  beforeUpload = (file: File) => {
    return new Observable((observer: Observer<boolean>) => {
      const isJPG = file.type === 'image/jpeg' || file.type === 'image/png';
      if (!isJPG) {
        this.msg.error('You can only upload JPG OR PNG file!');
        observer.complete();
        return;
      }
      const isLt2M = file.size / 1024 / 1024 < 2;
      if (!isLt2M) {
        this.msg.error('Image must smaller than 2MB!');
        observer.complete();
        return;
      }
      // check height
      this.checkImageDimension(file).then(dimensionRes => {
        /*if (!dimensionRes) {
          this.msg.error('Image only 300x300 above');
          observer.complete();
          return;
        }*/
        observer.next(isJPG && isLt2M && dimensionRes);
        observer.complete();
      });
    });
  };
  private checkImageDimension(file: File): Promise<boolean> {
    return new Promise(resolve => {
      const img = new Image(); // create image
      img.src = window.URL.createObjectURL(file);
      img.onload = () => {
        const width = img.naturalWidth;
        const height = img.naturalHeight;
        window.URL.revokeObjectURL(img.src!);
        // resolve(width === height && width >= 300);
        resolve(true);
      };
    });
  }
  handleChange(info: { file: UploadFile }): void {
    switch (info.file.status) {
      case 'uploading':
        this.loading = true;
        break;
      case 'done':
        // Get this url from response in real world.
        // 获取base64位
        // this.getBase64(info.file!.originFileObj!, (img: string) => {
        //   this.loading = false;
        //   this.i.img = img;
        // });
        this.i.img = info.file.response;
        this.img = this.basic.serverUrl + this.i.img;
        break;
      case 'error':
        this.msg.error('Network error');
        this.loading = false;
        break;
    }
  }
  private getBase64(img: File, callback: (img: string) => void): void {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result!.toString()));
    reader.readAsDataURL(img);
  }
  close() {
    this.modal.destroy();
  }
}
