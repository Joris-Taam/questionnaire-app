import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { CommonModule } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';
import { QuestionnaireService } from '../../shared/services/questionnaire.service';

@Component({
  selector: 'app-questionnaire-overview',
  imports: [CommonModule, SharedModule, RouterLink],
  templateUrl: './questionnaire-overview.component.html',
  styleUrls: ['./questionnaire-overview.component.scss']
})
export class QuestionnaireOverviewComponent implements OnInit {
  protected questionnaires: any[] = [];
  #destroy$ = new Subject<void>();
  #questionnaireService = inject(QuestionnaireService);
  protected errorMessage: string | undefined = "Geen vragenlijsten gevonden!";

  ngOnInit(): void {
    this.#loadQuestionnaires();
  }

  #loadQuestionnaires() {
    this.#questionnaireService.getAllQuestionnaires().pipe(takeUntil(this.#destroy$)).subscribe({
      next: (questionnaire) => {
        if (questionnaire.values.name) {
          questionnaire.forEach(q =>
            this.questionnaires.push(q)
          );
          return this.questionnaires;
        } else {
          this.errorMessage = "Geen geldige vragenlijsten gevonden!";
          return this.errorMessage;
        }
      },
      error: (err) => {
        if (err.status === 404 && err.error?.message) {
          this.errorMessage = err.error.message;
          return this.errorMessage;
        } else {
          this.errorMessage = "Er is iets fout gegaan!";
          return this.errorMessage;
        }

      }
    })
  }

  public getStatus(startDate: string, endDate: string): string {
    const currentDate = new Date();
    const start = new Date(startDate);
    const end = new Date(endDate);

    if (currentDate < start) {
      return 'orange';
    } else if (currentDate > end) {
      return 'red';
    } else {
      return 'green';
    }
  }

  ngOnDestroy() {
    this.#destroy$.next();
    this.#destroy$.complete();
  }
}

