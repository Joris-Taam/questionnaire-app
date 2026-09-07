import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrationListComponent } from './administration-list.component';

describe('AdministrationListComponent', () => {
  let component: AdministrationListComponent;
  let fixture: ComponentFixture<AdministrationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdministrationListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministrationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
