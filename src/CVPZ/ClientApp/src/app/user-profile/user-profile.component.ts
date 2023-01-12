import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me';

type ProfileType = {
  givenName?: string,
  surname?: string,
  userPrincipalName?: string,
  id?: string
};

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  profile!: ProfileType;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getProfile();
  }

  getProfile() {
    this.http.get<ProfileType>(GRAPH_ENDPOINT)
      .subscribe(profile => {
        this.profile = profile;
      });
  }

}
