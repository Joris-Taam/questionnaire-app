import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { SharedModule } from './shared/shared.module';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { AdminModule } from './admin/admin.module';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AdminModule, SharedModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Front-end';
  isFormValidInParent = false; 
  shouldShowFooter: boolean = false;

  constructor(private router: Router) {}

  ngOnInit(): void {
  }

}
