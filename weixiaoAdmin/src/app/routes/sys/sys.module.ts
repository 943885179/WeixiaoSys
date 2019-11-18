import { NgModule } from '@angular/core';
import { SharedModule } from '@shared';
import { SysRoutingModule } from './sys-routing.module';
import { SysMenuComponent } from './menu/menu.component';
import { SysMenuViewComponent } from './menu/view/view.component';
import { SysMenuEditComponent } from './menu/edit/edit.component';
import { SysDepComponent } from './dep/dep.component';
import { SysUserComponent } from './user/user.component';
import { SysUserEditComponent } from './user/edit/edit.component';
import { SysUserViewComponent } from './user/view/view.component';
import { SysCompanyComponent } from './company/company.component';
import { SysCompanyEditComponent } from './company/edit/edit.component';
import { SysCompanyViewComponent } from './company/view/view.component';
import { SysDepViewComponent } from './dep/view/view.component';
import { SysDepEditComponent } from './dep/edit/edit.component';

const COMPONENTS = [
  SysMenuComponent,
  SysDepComponent,
  SysUserComponent,
  SysCompanyComponent];
const COMPONENTS_NOROUNT = [
  SysMenuViewComponent,
  SysMenuEditComponent,
  SysUserEditComponent,
  SysUserViewComponent,
  SysCompanyEditComponent,
  SysCompanyViewComponent,
  SysDepViewComponent,
  SysDepEditComponent];

@NgModule({
  imports: [
    SharedModule,
    SysRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class SysModule { }
