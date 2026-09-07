import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrationManagementComponent } from './administration-management.component';

describe('AdministrationManagementComponent', () => {
  let component: AdministrationManagementComponent;
  let fixture: ComponentFixture<AdministrationManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdministrationManagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministrationManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
