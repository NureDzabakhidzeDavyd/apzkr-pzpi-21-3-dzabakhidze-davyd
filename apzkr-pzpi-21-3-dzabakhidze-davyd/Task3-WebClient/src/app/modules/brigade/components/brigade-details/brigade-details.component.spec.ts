import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrigadeDetailsComponent } from './brigade-details.component';

describe('BrigadeDetailsComponent', () => {
  let component: BrigadeDetailsComponent;
  let fixture: ComponentFixture<BrigadeDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BrigadeDetailsComponent]
    });
    fixture = TestBed.createComponent(BrigadeDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
