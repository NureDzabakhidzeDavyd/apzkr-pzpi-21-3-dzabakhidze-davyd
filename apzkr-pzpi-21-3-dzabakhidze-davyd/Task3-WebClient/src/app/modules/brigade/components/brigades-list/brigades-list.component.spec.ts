import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrigadesListComponent } from './brigades-list.component';

describe('BrigadesListComponent', () => {
  let component: BrigadesListComponent;
  let fixture: ComponentFixture<BrigadesListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BrigadesListComponent]
    });
    fixture = TestBed.createComponent(BrigadesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
