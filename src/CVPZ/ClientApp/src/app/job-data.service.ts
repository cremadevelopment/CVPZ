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
  private jobSource = new BehaviorSubject<Job[]>([]);
  jobs = this.jobSource.asObservable();



  constructor(private http: HttpClient) { }

  getJobs() {
    let params = new HttpParams();//.set('title', 'Fun');
    this.http
      .get<JobApiResponse>(this.ROOT_URL + '/api/Job', { params })
      .subscribe(resp => this.jobSource.next(resp.jobs));
  }
}

interface JobApiResponse {
  jobs: Job[];
}
