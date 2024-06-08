import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrigadeRescuerDetailsComponent } from './brigade-rescuer-details.component';

describe('BrigadeRescuerDetailsComponent', () => {
  let component: BrigadeRescuerDetailsComponent;
  let fixture: ComponentFixture<BrigadeRescuerDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BrigadeRescuerDetailsComponent]
    });
    fixture = TestBed.createComponent(BrigadeRescuerDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
