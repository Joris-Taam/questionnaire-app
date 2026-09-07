import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Questionnaire } from '../../shared/models/questionnaire.model';
import { Question } from '../../shared/models/question.model';
import { ParticipantAnswer } from '../../shared/models/ParticipantAnswer.model';
import { QuestionnaireService } from '../../shared/services/questionnaire.service';
import { ParticipantAnswerService } from '../../shared/services/participant-answer.service';
import { SharedModule } from "../../shared/shared.module";

@Component({
  imports: [CommonModule, SharedModule],
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent {
  questionnaire: Questionnaire | null = null;
  id: string | null = null;
  questions: Question[] = [];
  currentQuestionIndex: number = 0;
  currentQuestion: Question | null = null;
  totalQuestions: number = 0;
  selectedAnswers: { question_text: string; option_text: string }[] = [];
  showValidationMessage: boolean = false;
  isPreviewMode: boolean = false;
  #router = inject(Router);
  #route = inject(ActivatedRoute);
  #questionnaireService = inject(QuestionnaireService);
  #participantAnswerService = inject(ParticipantAnswerService);
  #participantId: string | null = null;

  ngOnInit(): void {
    this.#getParticipantIdFromRoute();

    const urlSegments = this.#route.snapshot.url;
    const idParam = this.#route.snapshot.paramMap.get('id');
    this.isPreviewMode = urlSegments.some(segment => segment.path === 'preview');

    if (idParam) {
      this.#questionnaireService.getQuestionnaireById(idParam).subscribe({
        next: (data) => {
          this.questionnaire = data;
          this.questions = data.questions || [];
          this.totalQuestions = this.questions.length;
        },
        error: (err) => {
          console.error('Error fetching questionnaire data:', err);
        },
      });
    } else {
      console.error('Invalid or missing questionnaire ID parameter.');
    }

  }

 #getParticipantIdFromRoute(): void {
    this.#route.queryParamMap.subscribe(params => {
      this.#participantId = params.get('participant');
    });
  }

  goBack(): void {
    const idParam = this.#route.snapshot.paramMap.get('id');
    if (idParam) {
      this.#router.navigate([`/admin/questionoverview`], { queryParams: { id: idParam } });
    }
  }

  protected isDateWithinRange(start: string, end: string, current: Date = new Date()): boolean {
    const startDate = new Date(start);
    const endDate = new Date(end);
    return current >= startDate && current <= endDate;
  }

  protected startForm(): void {
    if (this.questions.length > 0) {
      this.currentQuestionIndex = 0;
      this.currentQuestion = this.questions[this.currentQuestionIndex];
    } else {
      console.error('No questions available.');
    }
  }

  protected nextQuestion(): void {
    if (this.currentQuestion?.required) {
      const selectedAnswer = this.selectedAnswers.find(
        (item) => item.question_text === this.currentQuestion?.question_text
      );

      if (!selectedAnswer) {
        this.showValidationMessage = true;
        return;
      }
    }

    if (this.currentQuestionIndex < this.totalQuestions - 1) {
      this.currentQuestionIndex++;
      this.currentQuestion = this.questions[this.currentQuestionIndex];
      this.showValidationMessage = false;
    }
  }

  protected isAnswerSelectedForCurrentQuestion(): boolean {
    const selectedAnswer = this.selectedAnswers.find(
      (item) => item.question_text === this.currentQuestion?.question_text
    );
    return !!selectedAnswer;
  }


  protected previousQuestion(): void {
    if (this.currentQuestionIndex > 0) {
      this.currentQuestionIndex--;
      this.currentQuestion = this.questions[this.currentQuestionIndex];
    }
  }

  protected selectAnswer(answer: string): void {
    const questionText = this.currentQuestion?.question_text;

    if (!questionText) {
      console.warn('No question text found for current question');
      return;
    }

    const existingAnswerIndex = this.selectedAnswers.findIndex(
      (item) => item.question_text === questionText
    );

    if (existingAnswerIndex !== -1) {
      this.selectedAnswers[existingAnswerIndex].option_text = answer;
    } else {
      this.selectedAnswers.push({
        question_text: questionText,
        option_text: answer,
      });
    }
  }

  protected isOptionSelected(option: string): boolean {
    const selected = this.selectedAnswers.find(
      (item) => item.question_text === this.currentQuestion?.question_text
    );
    return selected?.option_text === option;
  }

  protected saveResults(answer: string): void {
    if (this.isPreviewMode) {
      return;
    }

    if (answer && this.#participantId) {
      const results = new ParticipantAnswer(
        this.questionnaire!.id,
        this.#participantId,
        this.currentQuestion!.question_text,
        answer
      );
      this.#participantAnswerService.postAnswers(results);
    } else {
      console.error('No answers selected or questionnaire not found');
    }
  }
  protected redirectToThanksPage(): void {
    this.#router.navigate(['/users/thanks-page']);
  }
}
