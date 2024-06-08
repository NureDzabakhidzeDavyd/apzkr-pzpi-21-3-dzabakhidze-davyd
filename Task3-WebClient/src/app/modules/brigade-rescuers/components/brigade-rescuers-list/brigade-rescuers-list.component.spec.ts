import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrigadeRescuersListComponent } from './brigade-rescuers-list.component';

describe('BrigadeRescuersListComponent', () => {
  let component: BrigadeRescuersListComponent;
  let fixture: ComponentFixture<BrigadeRescuersListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BrigadeRescuersListComponent]
    });
    fixture = TestBed.createComponent(BrigadeRescuersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
