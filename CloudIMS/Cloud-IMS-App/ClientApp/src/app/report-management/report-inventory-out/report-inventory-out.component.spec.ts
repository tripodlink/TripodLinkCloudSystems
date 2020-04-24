import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportInventoryOutComponent } from './report-inventory-out.component';

describe('ReportInventoryOutComponent', () => {
  let component: ReportInventoryOutComponent;
  let fixture: ComponentFixture<ReportInventoryOutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportInventoryOutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportInventoryOutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
