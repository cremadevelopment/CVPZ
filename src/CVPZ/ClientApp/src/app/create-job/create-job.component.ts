import { Component, OnInit } from '@angular/core';
import { Observable, from, BehaviorSubject } from 'rxjs';
import { Job } from '../job';
import { JobDataService } from '../job-data.service';

@Component({
  selector: 'app-create-job',
  templateUrl: './create-job.component.html',
  styleUrls: ['./create-job.component.css']
})
export class CreateJobComponent implements OnInit {
  job: any = {EmployerName:"", Title:"", Description:"", StartDate: Date.now};
  constructor(private jobDataService: JobDataService) { }

  ngOnInit(): void {
  }
  //need an observable to pass into the create method
  createJob() {
    //we could convert here
    this.jobDataService.createJob(this.job);
  }
}
/*   
  jobId: number;
  employerName: string;
  title: string;
  description: string;
  startDate: Date;
  endDate: Date; 
*/