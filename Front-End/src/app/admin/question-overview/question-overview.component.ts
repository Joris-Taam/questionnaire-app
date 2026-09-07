import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder } from '@angular/forms';
import { Question } from '../../shared/models/question.model';
import { QuestionnaireService } from '../../shared/services/questionnaire.service';
import { Questionnaire } from '../../shared/models/questionnaire.model';
import { Subject, takeUntil } from 'rxjs';
import { CdkDragDrop, CdkDropList, CdkDrag, moveItemInArray } from '@angular/cdk/drag-drop';
import { QuestionnaireEditComponent } from '../questionnaire-edit/questionnaire-edit.component';
import { SharedModule } from '../../shared/shared.module';


@Component({
  selector: 'app-question-overview',
  standalone: true,
  imports: [RouterModule, CommonModule, CdkDropList, CdkDrag, SharedModule],
  templateUrl: './question-overview.component.html',
  styleUrls: ['./question-overview.component.scss']
})
export class QuestionOverviewComponent implements OnInit, OnDestroy {

  protected questionnaire!: Questionnaire;
  protected questionnaires: any[] = [];
  protected questionsFormArray!: FormArray;
  public id: string | null = '';
  #destroy$ = new Subject<void>();
  #router = inject(Router);
  #questionnaireService = inject(QuestionnaireService);
  #route = inject(ActivatedRoute);
  #formBuilder = inject(FormBuilder);

  ngOnInit(): void {

    this.id = this.#route.snapshot.queryParamMap.get('id');

    if (this.id) {
      this.#questionnaireService.getQuestionnaireById(this.id).pipe(takeUntil(this.#destroy$)).subscribe({
        next: (data) => {
          this.questionnaire = data;
          this.questionnaire.questions = data.questions;
        },
        error: (err) => {
          console.warn('Error fetching Questionnaire data: ', err);
        }
      })
    }
  }

  protected preview() {
    this.#router.navigate(['users', 'form', this.id, 'preview'])
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.questionnaire.questions, event.previousIndex, event.currentIndex);
    console.log("entered");
    var i = 1;
    this.questionnaire.questions.forEach((question: any) => {

      question.questionnaire_id = this.questionnaire.id;
      question.question_number = i;
      i += 1;
    });
    this.#questionnaireService.sendQuestionnaireData('General', this.questionnaire, this.questionnaire.id)
  }

  protected getStatus(startDate: string, endDate: string): string {
    const currentDate = new Date();
    const start = new Date(startDate);
    const end = new Date(endDate);

    if (currentDate < start) {
      return 'Nog niet gepubliceerd';
    } else if (currentDate > end) {
      return 'Datum verstreken';
    } else {
      return 'Gepubliceerd';
    }
  }

  protected trackByQuestionId(_index: number, item: any): any {
    return item.id;
  }

  protected trackByAnswerId(_index: number, item: any): any {
    return item.id;
  }

  isQuestionnaireValid(): boolean {
    if (!this.questionnaire?.questions?.length) return false;

    return !this.questionnaire.questions.some((q: Question) => !q?.question_text || q.question_text.trim() === '');
  }

  protected removeQuestion(questionId: string): void {
    if (confirm("Weet u zeker dat u deze vraag wilt verwijderen?\n Dit kan niet ongedaan gemaakt worden!")) {

      if (questionId) {
        this.#questionnaireService.deleteQuestion(questionId, this.id!);
        this.questionnaire.questions = this.questionnaire.questions.filter(q => q.id !== questionId);
        this.#updateQuestionnaire();

      }
    }
  }

  #updateQuestionnaire(): void {
    var i = 1;
    this.questionnaire.questions.forEach((question: any) => {
      question.questionnaire_id = this.questionnaire.id;
      question.question_number = i;
      i += 1;
    });
    this.#questionnaireService.sendQuestionnaireData('General', this.questionnaire, this.questionnaire.id)
  }


  addQuestion(): void {
    const newQuestion = this.#formBuilder.group({
      question_text: [''],
      answer_options: [[]],
      required: [false]
    });

    this.questionsFormArray.push(newQuestion);
  }

  onAnswerChange(questionIndex: number, answerIndex: number, value: string): void {
    const question = this.questionsFormArray.at(questionIndex);
    const answer_options = question.get('answer_options')?.value || [];
    answer_options[answerIndex] = value;
    question.get('answer_options')?.setValue(answer_options);
  }

  ngOnDestroy() {
    this.#destroy$.next();
    this.#destroy$.complete();
  }
}