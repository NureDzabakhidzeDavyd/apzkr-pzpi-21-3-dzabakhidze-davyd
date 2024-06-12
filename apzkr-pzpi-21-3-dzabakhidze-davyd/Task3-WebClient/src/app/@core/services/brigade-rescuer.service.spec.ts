import { TestBed } from '@angular/core/testing';

import { BrigadeRescuerService } from './brigade-rescuer.service';

describe('BrigadeRescuerService', () => {
  let service: BrigadeRescuerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BrigadeRescuerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
