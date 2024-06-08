import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VictimsListComponent } from './victims-list.component';

describe('VictimsListComponent', () => {
  let component: VictimsListComponent;
  let fixture: ComponentFixture<VictimsListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VictimsListComponent]
    });
    fixture = TestBed.createComponent(VictimsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
