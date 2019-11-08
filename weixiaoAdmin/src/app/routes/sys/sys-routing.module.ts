import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SysMenuComponent } from './menu/menu.component';
import { SysDepComponent } from './dep/dep.component';
import { SysUserComponent } from './user/user.component';
import { SysCompanyComponent } from './company/company.component';

const routes: Routes = [

  { path: 'menu', component: SysMenuComponent },
  { path: 'dep', component: SysDepComponent },
  { path: 'user', component: SysUserComponent },
  { path: 'company', component: SysCompanyComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SysRoutingModule { }
