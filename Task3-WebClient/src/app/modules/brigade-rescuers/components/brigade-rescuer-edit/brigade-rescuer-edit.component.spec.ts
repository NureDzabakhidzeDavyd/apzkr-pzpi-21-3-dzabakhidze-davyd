import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrigadeRescuerEditComponent } from './brigade-rescuer-edit.component';

describe('BrigadeRescuerEditComponent', () => {
  let component: BrigadeRescuerEditComponent;
  let fixture: ComponentFixture<BrigadeRescuerEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BrigadeRescuerEditComponent]
    });
    fixture = TestBed.createComponent(BrigadeRescuerEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
