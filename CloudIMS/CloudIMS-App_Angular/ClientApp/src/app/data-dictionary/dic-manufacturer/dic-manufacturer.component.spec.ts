import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DICMANUFACTURERComponent } from './dic-manufacturer.component';

describe('DICMANUFACTURERComponent', () => {
  let component: DICMANUFACTURERComponent;
  let fixture: ComponentFixture<DICMANUFACTURERComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DICMANUFACTURERComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DICMANUFACTURERComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
