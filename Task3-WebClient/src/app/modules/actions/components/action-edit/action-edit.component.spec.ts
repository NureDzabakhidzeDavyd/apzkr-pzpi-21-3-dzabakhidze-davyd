import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionEditComponent } from './action-edit.component';

describe('ActionEditComponent', () => {
  let component: ActionEditComponent;
  let fixture: ComponentFixture<ActionEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActionEditComponent]
    });
    fixture = TestBed.createComponent(ActionEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
