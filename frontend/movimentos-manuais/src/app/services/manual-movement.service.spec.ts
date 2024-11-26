import { TestBed } from '@angular/core/testing';

import { ManualMovementService } from './manual-movement.service';

describe('ManualMovementService', () => {
  let service: ManualMovementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ManualMovementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
