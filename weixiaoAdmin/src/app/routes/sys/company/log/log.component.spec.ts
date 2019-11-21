import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SysCompanyLogComponent } from './log.component';

describe('SysCompanyLogComponent', () => {
  let component: SysCompanyLogComponent;
  let fixture: ComponentFixture<SysCompanyLogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SysCompanyLogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SysCompanyLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
