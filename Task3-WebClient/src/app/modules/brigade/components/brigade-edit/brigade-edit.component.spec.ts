import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrigadeEditComponent } from './brigade-edit.component';

describe('BrigadeEditComponent', () => {
  let component: BrigadeEditComponent;
  let fixture: ComponentFixture<BrigadeEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BrigadeEditComponent]
    });
    fixture = TestBed.createComponent(BrigadeEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
