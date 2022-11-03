import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { HomeComponent } from './home/home.component';
import { JobJournalComponent } from './job-journal/job-journal.component';
import { JobsListComponent } from './jobs-list/jobs-list.component';

const routes: Routes = [
  {
    path: 'jobs',
    component: JobsListComponent,
    canActivate: [MsalGuard]
  },
  {
    path: 'journal',
    component: JobJournalComponent,
    canActivate: [MsalGuard]
  },
  {
    path: '',
    component: HomeComponent
  },
];

const isIframe = window !== window.parent && !window.opener;

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
