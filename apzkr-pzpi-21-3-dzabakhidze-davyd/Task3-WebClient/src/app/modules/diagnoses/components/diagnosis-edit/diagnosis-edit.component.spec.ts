import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiagnosisEditComponent } from './diagnosis-edit.component';

describe('DiagnosisEditComponent', () => {
  let component: DiagnosisEditComponent;
  let fixture: ComponentFixture<DiagnosisEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DiagnosisEditComponent]
    });
    fixture = TestBed.createComponent(DiagnosisEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
