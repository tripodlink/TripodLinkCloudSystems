import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemTrackingComponent } from './item-tracking.component';

describe('ItemTrackingComponent', () => {
  let component: ItemTrackingComponent;
  let fixture: ComponentFixture<ItemTrackingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemTrackingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemTrackingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
