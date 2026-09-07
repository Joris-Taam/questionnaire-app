import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BindUsersComponent } from './bind-users.component';

describe('BindUsersComponent', () => {
  let component: BindUsersComponent;
  let fixture: ComponentFixture<BindUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BindUsersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BindUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
