import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SysMenuComponent } from './menu/menu.component';
import { SysDepComponent } from './dep/dep.component';
import { SysUserComponent } from './user/user.component';
import { SysCompanyComponent } from './company/company.component';
import { SysMenuViewComponent } from './menu/view/view.component';
import { SysMenuEditComponent } from './menu/edit/edit.component';
import { SysCompanyLogComponent } from './company/log/log.component';

const routes: Routes = [
  { path: 'menu', component: SysMenuComponent, data: { title: `菜单管理`, breadcrumb: "菜单管理" } },
  // { path: 'menu/view', component: SysMenuViewComponent },
  // { path: 'menu/Edit/:id', component: SysMenuEditComponent },
  { path: 'dep', component: SysDepComponent },
  { path: 'user', component: SysUserComponent },
  { path: 'company', component: SysCompanyComponent, data: { title: `公司管理`, breadcrumb: "公司管理" } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SysRoutingModule { }
