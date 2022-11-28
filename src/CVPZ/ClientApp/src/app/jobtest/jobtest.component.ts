import { Component, OnInit } from '@angular/core';
import { Observable, from, BehaviorSubject } from 'rxjs';
import { Job } from '../job';
import { JobDataService } from '../job-data.service';

@Component({
  selector: 'app-test-job',
  templateUrl: './jobtest.component.html',
  styleUrls: ['./jobtest.component.css']
})
export class JobTestComponent implements OnInit {
  newJob: any = {};
  job: any = {};
  constructor(private jobDataService: JobDataService) { }

  ngOnInit(): void {
  }
  createJob() {
    this.jobDataService.createJob(this.newJob);
  }
  endJob() {
    this.jobDataService.endJob(this.job);
  }
}
