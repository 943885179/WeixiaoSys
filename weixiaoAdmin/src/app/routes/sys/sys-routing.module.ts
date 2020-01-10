import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SysMenuComponent } from './menu/menu.component';
import { SysDepComponent } from './dep/dep.component';
import { SysUserComponent } from './user/user.component';
import { SysCompanyComponent } from './company/company.component';
import { SysMenuViewComponent } from './menu/view/view.component';
import { SysMenuEditComponent } from './menu/edit/edit.component';
import { SysCompanyLogComponent } from './company/log/log.component';
import { SysRoleComponent } from './role/role.component';
import { SysPowerComponent } from './power/power.component';

const routes: Routes = [
  { path: 'menu', component: SysMenuComponent, data: { title: `菜单管理`, breadcrumb: "菜单管理" } },
  // { path: 'menu/view', component: SysMenuViewComponent },
  // { path: 'menu/Edit/:id', component: SysMenuEditComponent },
  { path: 'dep', component: SysDepComponent, data: { title: `部门管理`, breadcrumb: `部门管理` } },
  { path: 'user', component: SysUserComponent, data: { title: `人员管理`, breadcrumb: `人员管理` } },
  { path: 'company', component: SysCompanyComponent, data: { title: `公司管理`, breadcrumb: "公司管理" } },
  { path: 'role', component: SysRoleComponent, data: { title: `角色管理`, breadcrumb: "角色管理" } },
  { path: 'power', component: SysPowerComponent, data: { title: `权限管理`, breadcrumb: "权限管理" } }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SysRoutingModule { }
