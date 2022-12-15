import { Component, OnInit } from '@angular/core';
import { Job } from '../job';
import { JobDataService } from '../job-data.service';

@Component({
  selector: 'app-my-jobs',
  templateUrl: './my-jobs.component.html',
  styleUrls: ['./my-jobs.component.css']
})
export class MyJobsComponent implements OnInit {

  jobs: Job[] = [];

  constructor(private jobDataService: JobDataService) {}

  ngOnInit(): void {
    this.jobDataService.getMyJobs().subscribe(j => this.jobs = j);
  }

}
