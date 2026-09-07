import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { QuestionnaireService } from '../../shared/services/questionnaire.service';
import { Subject, takeUntil } from 'rxjs';
import { ZeroStateComponent } from "../../shared/components/zero-state/zero-state.component";
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-result-overview',
  imports: [RouterModule, CommonModule, ZeroStateComponent, SharedModule],
  templateUrl: './result-overview.component.html',
  styleUrls: ['./result-overview.component.scss']
})
export class ResultOverviewComponent implements OnInit, OnDestroy {
  questionnaires: any[] = [];
  #destroy$ = new Subject<void>();
  protected errorMessage: string | undefined = "Geen vragenlijsten gevonden!";
  protected headerText: string = 'Resultaten overzicht';
  protected isLoading: boolean = true;
  #questionnaireService = inject(QuestionnaireService);

  ngOnInit(): void {
    this.#loadQuestionnaires();
  }

  #loadQuestionnaires() {
    this.isLoading = true;
    this.#questionnaireService.getAllQuestionnaires().pipe(takeUntil(this.#destroy$)).subscribe({
      next: (questionnaire) => {
        if (questionnaire.values.name) {
          questionnaire.forEach(q =>
            this.questionnaires.push(q)
          );
          this.isLoading = false;
          return this.questionnaires;
        } else {
          this.errorMessage = "Geen geldige vragenlijsten gevonden!";
          this.isLoading = false;
          return this.errorMessage;
        }
      },
      error: (err) => {
        if (err.status === 404 && err.error?.message) {
          this.errorMessage = err.error.message;
          this.isLoading = false;
          return this.errorMessage;
        } else {
          this.errorMessage = "Er is iets fout gegaan!";
          this.isLoading = false;
          return this.errorMessage;
        }

      }
    })
  }

  ngOnDestroy() {
    this.#destroy$.next();
    this.#destroy$.complete();
  }
}
