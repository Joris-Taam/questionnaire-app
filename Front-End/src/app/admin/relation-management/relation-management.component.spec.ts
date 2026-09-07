import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RelationManagementComponent } from './relation-management.component';

describe('RelationManagementComponent', () => {
  let component: RelationManagementComponent;
  let fixture: ComponentFixture<RelationManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RelationManagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RelationManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
