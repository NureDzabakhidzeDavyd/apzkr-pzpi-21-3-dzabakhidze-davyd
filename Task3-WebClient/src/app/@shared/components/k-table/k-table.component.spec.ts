import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KTableComponent } from './k-table.component';

describe('KTableComponent', () => {
  let component: KTableComponent;
  let fixture: ComponentFixture<KTableComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [KTableComponent]
    });
    fixture = TestBed.createComponent(KTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
