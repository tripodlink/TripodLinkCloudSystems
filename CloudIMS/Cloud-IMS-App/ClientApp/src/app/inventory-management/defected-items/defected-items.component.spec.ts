import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefectedItemsComponent } from './defected-items.component';

describe('DefectedItemsComponent', () => {
  let component: DefectedItemsComponent;
  let fixture: ComponentFixture<DefectedItemsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefectedItemsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefectedItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
