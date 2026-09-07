import { Component, inject, Inject } from '@angular/core';
import { RouterModule } from '@angular/router';

import { Auth0ClientService, AuthService } from '@auth0/auth0-angular';
import { Auth0Client, GetTokenSilentlyOptions } from '@auth0/auth0-spa-js';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { QuestionnaireService } from '../shared/services/questionnaire.service';
import { Questionnaire } from '../shared/models/questionnaire.model';
import { SharedModule } from '../shared/shared.module';

@Component({
  selector: 'app-home',
  imports: [CommonModule, RouterModule, SharedModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  headerText: string = 'Home';
  btnVisible: Array<boolean> = [true, true, false];
  public title = 'venture-mfe';
  public accessToken: string | undefined;
  public idToken: string | undefined;
  #questionnaireService = inject(QuestionnaireService)
  recentQuestionnaires: Questionnaire[] = [];
  userStatistics: { totalQuestionnaires: number, totalResponses: number } = { totalQuestionnaires: 0, totalResponses: 0 };

  constructor(
    public auth: AuthService,
    @Inject(Auth0ClientService) private auth0Client: Auth0Client,
  ){}

  ngOnInit() {
    this.loadUserStatistics();
    this.#getIdToken();
  }

  loadUserStatistics(): void {
    this.#questionnaireService.getAllQuestionnaires().subscribe((data: Questionnaire[]) => {
      this.userStatistics.totalQuestionnaires = data.length;
      this.userStatistics.totalResponses = data.reduce((sum, q) => sum + (q.questions ? q.questions.length : 0), 0);
    });
  }

    public logout(){
      this.auth.logout({
        logoutParams: {
          returnTo: document.location.origin
        }
      });
    }

  #getIdToken() {

    this.auth0Client.getTokenSilently().then((token) => {
      this.idToken = token;
      localStorage.setItem('idToken', this.idToken);
    });
  }

}
