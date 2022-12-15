import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { BehaviorSubject, map, Observable, of } from 'rxjs';
import { Job } from './job';
import { JobJournalComponent } from './job-journal/job-journal.component';

@Injectable({
  providedIn: 'root'
})
export class JobDataService {
  readonly ROOT_URL = "";

  constructor(private http: HttpClient) { }

  getJobs() : Observable<Job[]> {
    let params = new HttpParams();
    return this.http
      .get<JobApiResponse>(this.ROOT_URL + '/api/Job', { params })
      .pipe(map(jar => jar.jobs));
  }

  getMyJobs() : Observable<Job[]> {
    return this.http
      .get<JobApiResponse>(this.ROOT_URL + '/api/MyJobs')
      .pipe(map(jar => jar.jobs));
  }

  createJob(job: Job)
  {
    return this.http.post<Job>(this.ROOT_URL + '/api/Job/Create', job).subscribe(res => console.log(res));
  }

  endJob(job: any) {
    return this.http.post<string>(this.ROOT_URL + '/api/Job/End', job).subscribe(res => console.log(res));
  }
}

interface JobApiResponse {
  jobs: Job[];
}
