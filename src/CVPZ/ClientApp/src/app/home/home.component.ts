import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public pingResult?: PingResponse;

  constructor(
    http: HttpClient
  ) {
    http.get<PingResponse>('/system/ping').subscribe(result => {
      this.pingResult = result;
    }, error => console.error(error));}

  ngOnInit(): void {
  }
}

interface PingResponse {
  date: string;
  value: string;
}
