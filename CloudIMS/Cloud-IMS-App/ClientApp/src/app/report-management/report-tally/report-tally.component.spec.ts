import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportTallyComponent } from './report-tally.component';

describe('ReportTallyComponent', () => {
  let component: ReportTallyComponent;
  let fixture: ComponentFixture<ReportTallyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportTallyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportTallyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
