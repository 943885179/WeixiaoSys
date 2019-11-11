import { Injectable, Injector, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { zip, of, interval } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import { MenuService, SettingsService, TitleService, ALAIN_I18N_TOKEN } from '@delon/theme';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { ACLService } from '@delon/acl';
import { TranslateService } from '@ngx-translate/core';
import { I18NService } from '../i18n/i18n.service';
import { CacheService } from '@delon/cache'
import { NzIconService } from 'ng-zorro-antd/icon';
import { ICONS_AUTO } from '../../../style-icons-auto';
import { ICONS } from '../../../style-icons';
import { BasicService } from 'src/app/service/basic.service';

/**
 * Used for application startup
 * Generally used to get the basic data of the application, like: Menu Data, User Data, etc.
 */
@Injectable()
export class StartupService {
  constructor(
    iconSrv: NzIconService,
    private menuService: MenuService,
    private translate: TranslateService,
    @Inject(ALAIN_I18N_TOKEN) private i18n: I18NService,
    private settingService: SettingsService,
    private aclService: ACLService,
    private titleService: TitleService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private httpClient: HttpClient,
    private injector: Injector,
    private cacheServer: CacheService,
    private basic: BasicService
  ) {
    iconSrv.addIcon(...ICONS_AUTO, ...ICONS);
  }
  private viaHttp(resolve: any, reject: any) {
    zip(
      this.cacheServer.get(`assets/tmp/i18n/${this.i18n.defaultLang}.json`),
      // this.cacheServer.get('assets/tmp/app-data.json'),
      this.httpClient.get(this.basic.ApiUrl + this.basic.ApiRole.Menu)
      // this.cacheServer.get("https://localhost:44361/api/menu/menu")
    ).pipe(
      retry(3), // 可以重试三次
      // map(units => this.httpClient.get(`/user`)),
      catchError(([langData, menuData]) => {
        resolve(null);
        return [langData, menuData];
      })
    ).subscribe(([langData, menuData]) => {
      console.log("菜单", menuData);
      // Setting language data
      this.translate.setTranslation(this.i18n.defaultLang, langData);
      this.translate.setDefaultLang(this.i18n.defaultLang);
      // Application data
      // Application information: including site name, description, year
      this.settingService.setApp(this.basic.AppData.app);
      // User information: including name, avatar, email address
      this.settingService.setUser(this.basic.AppData.user);
      // ACL: Set the permissions to full, https://ng-alain.com/acl/getting-started
      this.aclService.setFull(true);
      // Menu data, https://ng-alain.com/theme/menu
      const menu: any = menuData;
      this.menuService.add(menu);
      // Can be set page suffix title, https://ng-alain.com/theme/title
      this.titleService.suffix = this.basic.AppData.app.name;
      // 是否固定顶部菜单
      this.settingService.setLayout(`fixed`, false);
      // 是否折叠右边菜单
      this.settingService.setLayout(`collapsed`, false);
    },
      () => { },
      () => {
        resolve(null);
      });
  }
  load(): Promise<any> {
    // only works with promises
    // https://github.com/angular/angular/issues/15088
    return new Promise((resolve, reject) => {
      this.viaHttp(resolve, reject);
    });
  }
}
