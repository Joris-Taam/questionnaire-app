import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { CommonModule } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';
import { QuestionnaireService } from '../../shared/services/questionnaire.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-administration-list',
  imports: [CommonModule, SharedModule, RouterLink],
  templateUrl: './administration-list.component.html',
  styleUrl: './administration-list.component.scss'
})
export class AdministrationListComponent {
  protected questionnaires: any[] = [];
  protected administrationsList: any = '';
  #destroy$ = new Subject<void>();
  protected errorMessage: string | undefined = "Geen vragenlijsten gevonden!";
  public idToken: string | undefined;
  #http = inject(HttpClient);
  protected headerText = 'Administratie beheer';
  protected isLoading: boolean = true;

  ngOnInit(): void {
    this.#loadAdministrations();
  }

  #loadAdministrations() {
    this.isLoading = true;
    this.idToken = localStorage.getItem('idToken')!;

    let params = { "take": 3, "searchTermsByField": {} }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.idToken}`,
      'Content-Type': 'application/json'
    });

    this.#http.post<any>(' https://api-tst.snelstart.nl/operations/administration-management/v1/administration-query/search', params, { headers }).subscribe(data => {
      this.administrationsList = data;
      this.isLoading = false;
    });
  }

  ngOnDestroy() {
    this.#destroy$.next();
    this.#destroy$.complete();
  }
}
