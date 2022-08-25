import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public pingResult?: PingResponse;

  constructor(http: HttpClient) {
    http.get<PingResponse>('/system/ping').subscribe(result => {
      this.pingResult = result;
    }, error => console.error(error));
  }

  title = 'cvpzng';
}

interface PingResponse {
  date: string;
  value: string;
}
