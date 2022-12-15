import { Component, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Job } from '../job';
import { JobDataService } from '../job-data.service';

@Component({
  selector: 'app-jobs-list',
  templateUrl: './jobs-list.component.html',
  styleUrls: ['./jobs-list.component.css']
})
export class JobsListComponent implements OnInit {
  jobs: Job[] = [];

  constructor(private jobDataService: JobDataService) {}

  ngOnInit(): void {
    this.jobDataService.getJobs().subscribe(jobs => this.jobs = jobs);
  }
}
