import { TestBed } from '@angular/core/testing';

import { TrucksModelsApiService } from './trucks-models-api.service';

describe('TrucksModelsApiService', () => {
  let service: TrucksModelsApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrucksModelsApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
