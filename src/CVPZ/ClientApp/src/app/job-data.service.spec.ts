import { TestBed } from '@angular/core/testing';

import { JobDataService } from './job-data.service';

describe('JobDataService', () => {
  let service: JobDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JobDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
