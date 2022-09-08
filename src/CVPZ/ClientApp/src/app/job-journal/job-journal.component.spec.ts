import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobJournalComponent } from './job-journal.component';

describe('JobJournalComponent', () => {
  let component: JobJournalComponent;
  let fixture: ComponentFixture<JobJournalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobJournalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobJournalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
