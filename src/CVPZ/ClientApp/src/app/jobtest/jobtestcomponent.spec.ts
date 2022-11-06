import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobTestComponent } from './jobtest.component';

describe('JobTestComponent', () => {
  let component: JobTestComponent;
  let fixture: ComponentFixture<JobTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobTestComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
