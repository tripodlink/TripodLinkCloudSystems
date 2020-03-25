import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DICSUPPLIERComponent } from './dic-supplier.component';

describe('DICSUPPLIERComponent', () => {
  let component: DICSUPPLIERComponent;
  let fixture: ComponentFixture<DICSUPPLIERComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DICSUPPLIERComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DICSUPPLIERComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
