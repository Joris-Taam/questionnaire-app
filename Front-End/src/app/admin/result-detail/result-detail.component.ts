import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Questionnaire } from '../../shared/models/questionnaire.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ParticipantService } from '../../shared/services/participant.service';
import { ParticipantAnswerResponse } from '../../shared/models/participant-answer-response.model';
import { Subject, Subscription, takeUntil } from 'rxjs';
import { QuestionnaireService } from '../../shared/services/questionnaire.service';
import { AnswerStats } from '../../shared/models/answerStats.model';
import { HeaderService } from '../../shared/services/header.service';
import { ZeroStateComponent } from "../../shared/components/zero-state/zero-state.component";
import { SharedModule } from '../../shared/shared.module';

interface ParticipantWithAnswers {
  public_id: string;
  participant_name: string;
  answers: ParticipantAnswerResponse[];
}

@Component({
  selector: 'app-result-detail',
  imports: [CommonModule, RouterModule, ZeroStateComponent, SharedModule],
  templateUrl: './result-detail.component.html',
  styleUrls: ['./result-detail.component.scss']
})
export class ResultDetailComponent implements OnInit, OnDestroy {
  questionnaireName: string | null = null;
  participantAnswers: ParticipantAnswerResponse[] = [];
  answerStatsGeneral: AnswerStats[] = [];
  participant: ParticipantWithAnswers[] = [];
  #activatedRoute = inject(ActivatedRoute);
  protected id: string | null = null;
  protected generalView: boolean = true;
  protected participantView: boolean = false;
  #participantService = inject(ParticipantService);
  #questionnaireService = inject(QuestionnaireService);
  #headerService = inject(HeaderService);
  #route = inject(ActivatedRoute);
  #questionnaireId: string | null = '';
  #questionnaireSubscription: Subscription | null = null;
  #participantAnswersSubscription: Subscription | null = null;
  private viewModeSubscription: Subscription = new Subscription();
  #destroy$ = new Subject<void>();
  protected isLoading: boolean = true;

  ngOnInit(): void {
    this.#questionnaireId = this.#route.snapshot.queryParamMap.get('id');
    this.#getIdFromRoute();
    this.getQuestionnaireData();
    this.#getAnswerPercentages();
    this.#changeResultView();

    if (this.#questionnaireId) {
      this.isLoading = true;
      this.#participantAnswersSubscription = this.#participantService.getAnswersByQuestionnaireGroup(this.#questionnaireId).subscribe({
        next: (data: ParticipantWithAnswers[]) => {
          this.participant = data;
          this.isLoading = false;
        },
        error: (err) => {
          console.error('Error fetching participant answers:', err);
          this.isLoading = false;
        }
      });
    } else {
      console.error('No questionnaire ID provided');
    }
  }

  #getIdFromRoute(): void {
    this.#activatedRoute.queryParamMap.subscribe(params => {
      this.id = params.get('id');
    });
  }

  #changeResultView() {
    this.viewModeSubscription = this.#headerService.viewMode$.subscribe(mode => {
      this.generalView = mode === 'general';
      this.participantView = mode === 'participant';
    });
  }

  getQuestionnaireData() {
    if (this.#questionnaireId) {
      this.#questionnaireSubscription = this.#questionnaireService.getQuestionnaireById(this.#questionnaireId).subscribe({
        next: (data: Questionnaire) => {
          this.questionnaireName = data.name;
        },
        error: (err) => {
          console.error('Error fetching questionnaire data:', err);
        }
      });
    }
  }

  #getAnswerPercentages() {
    this.isLoading = true;
    if (this.#questionnaireId) {
      this.#questionnaireService.retrieveAnswerPercentages(this.#questionnaireId)
        .pipe(takeUntil(this.#destroy$))
        .subscribe({
          next: (data: any) => {
            this.answerStatsGeneral = data;

            this.answerStatsGeneral.forEach((question: any) => {
              const totalAnswers = question.options.reduce((total: number, option: any) => {
                this.isLoading = false;
                return total + option.count;
              }, 0);

              question.totalAnswers = totalAnswers;
            });
          },
          error: (err) => {
            console.warn('Error fetching answer percentages: ', err);
            this.isLoading = false;
          }
        });
    }
  }

  ngOnDestroy(): void {
    if (this.#questionnaireSubscription) {
      this.#questionnaireSubscription.unsubscribe();
    }
    if (this.#participantAnswersSubscription) {
      this.#participantAnswersSubscription.unsubscribe();
    }
    if (this.viewModeSubscription) {
      this.viewModeSubscription.unsubscribe();
    }
    this.#destroy$.next();
    this.#destroy$.complete();
  }
}