import { Component, Inject, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Auth0ClientService, AuthService } from '@auth0/auth0-angular';
import { Auth0Client } from '@auth0/auth0-spa-js';


@Component({
  selector: 'app-administration-management',
  imports: [CommonModule, SharedModule, RouterLink],
  templateUrl: './administration-management.component.html',
  styleUrl: './administration-management.component.scss'
})
export class AdministrationManagementComponent {
  protected questionnaires: any[] = [];
  protected relationsList: any = '';
  #destroy$ = new Subject<void>();
  protected errorMessage: string | undefined = "Geen vragenlijsten gevonden!";
  public idToken: string | undefined;
  #http = inject(HttpClient);
  protected headerText = '';
  accessToken: string | null = '';
  administrationId: string | null = ''
  #route = inject(ActivatedRoute);
  protected isLoading: boolean = true;

  constructor(
    public auth: AuthService,
  ) { }

  ngOnInit(): void {
    this.#getAdministrationIdFromURL();
    this.#getAccessToken();
    this.#getAdministrationNameFromURL();
  }

  #getAdministrationNameFromURL() {
    this.headerText = this.#route.snapshot.queryParamMap.get('name')!;
  }

  #getAdministrationIdFromURL() {
    this.administrationId = this.#route.snapshot.queryParamMap.get('id');
  }

  #loadRelations() {
    this.isLoading = true;
    let params = { "searchTerm": "", "page": 0, "pageSize": 100, "sortByColumn": "Code", "sortDirection": "Ascending", "filters": { "alphaNumeric": [{ "columnName": "IsActive", "type": "EqualTo", "value": "True" }], "numeric": [{ "columnName": "Code", "type": "GreaterThanOrEqualTo", "value": 2 }], "date": [] } }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.accessToken}`,
      'Content-Type': 'application/json'
    });

    this.#http.post<any>('', params, { headers }).subscribe(data => {
      this.relationsList = data;
      this.isLoading = false;
    });
  }

  #getAccessToken() {
    this.auth.getAccessTokenSilently({
      cacheMode: 'off',
      authorizationParams: { administrationId: this.administrationId }
    }).subscribe((token) => {
      this.accessToken = token;
      localStorage.setItem('accessToken', token);

      this.#loadRelations();
    });
  }

  ngOnDestroy() {
    this.#destroy$.next();
    this.#destroy$.complete();
  }
}
