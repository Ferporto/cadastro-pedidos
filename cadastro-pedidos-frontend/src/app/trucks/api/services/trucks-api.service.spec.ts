import { TestBed } from '@angular/core/testing';

import { TrucksApiService } from './trucks-api.service';

describe('TrucksApiService', () => {
  let service: TrucksApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrucksApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
