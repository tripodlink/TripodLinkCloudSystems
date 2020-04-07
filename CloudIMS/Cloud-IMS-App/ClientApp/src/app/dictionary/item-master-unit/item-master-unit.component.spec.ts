import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemMasterUnitComponent } from './item-master-unit.component';

describe('ItemMasterUnitComponent', () => {
  let component: ItemMasterUnitComponent;
  let fixture: ComponentFixture<ItemMasterUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemMasterUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemMasterUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
