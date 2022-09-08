import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { BehaviorSubject, map, Observable, of } from 'rxjs';
import { Job } from './job';

@Injectable({
  providedIn: 'root'
})
export class JobDataService {
  readonly ROOT_URL = "";
  jobs: Observable<Job[]>;



  constructor(private http: HttpClient) {
    this.jobs = this.getJobs();
  }

  getJobs(): Observable<Job[]> {

    let params = new HttpParams();//.set('title', 'Fun');
    return this.http.get<JobApiResponse>(this.ROOT_URL + '/api/Job', { params }).pipe(map(val => val.jobs));
  }
}

interface JobApiResponse {
  jobs: Job[];
}
