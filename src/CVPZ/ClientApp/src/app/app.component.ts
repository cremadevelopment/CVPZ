import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public pingResult?: PingResponse;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(http: HttpClient, private breakpointObserver: BreakpointObserver) {
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
