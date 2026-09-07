import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultOverviewComponent } from './result-overview.component';

describe('ResultOverviewComponent', () => {
  let component: ResultOverviewComponent;
  let fixture: ComponentFixture<ResultOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResultOverviewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ResultOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
