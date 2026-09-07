import { Component, inject } from '@angular/core';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocalStorageService } from '../../shared/services/localstorage.service';
import { FormsModule, ReactiveFormsModule, FormGroup, FormArray, FormBuilder, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { Questionnaire } from '../../shared/models/questionnaire.model';
import { Question } from '../../shared/models/question.model';
import { SharedModule } from '../../shared/shared.module';
import { QuestionnaireService } from '../../shared/services/questionnaire.service';
import { v4 as uuidv4 } from 'uuid';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-questionnaire-edit',
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule, SharedModule],
  templateUrl: './questionnaire-edit.component.html',
  styleUrls: ['./questionnaire-edit.component.scss'],
})
export class QuestionnaireEditComponent {
  protected id: string | null = null;
  questionId: string | null = null;
  protected headerText: string = 'Vragenlijst Bewerken';
  public formGroup!: FormGroup;
  #localStorageService = inject(LocalStorageService);
  #questionnaireService = inject(QuestionnaireService);
  #activatedRoute = inject(ActivatedRoute);
  #fb = inject(FormBuilder);
  #destroy$ = new Subject<void>();
  protected errorMessage: string | undefined = "";
  protected targetGroupOptions = [];

  ngOnInit(): void {
    this.#getIdFromRoute();
    this.#buildFormGroup();
    this.#populateFormFields();
    this.#getQuestionFromRoute();
    this.#addQuestion();
  }

  #getIdFromRoute(): void {
    this.#activatedRoute.queryParamMap.subscribe(params => {
      this.id = params.get('id');
    });
  }

  #getQuestionFromRoute(): string | null {
    const questionParam = this.#activatedRoute.snapshot.queryParamMap.get('question');

    if (questionParam === null) {
      console.warn('No question index found in the route.');
    } else {
      this.questionId = questionParam;
    }
    return questionParam;
  }

  #addQuestion() {
    if (this.questionId == 'new') {
      const newId = uuidv4();

      const questionFormGroup = this.#fb.group({
        id: [newId],
        question_text: ['', [Validators.required, Validators.maxLength(150)]],
        required: [false],
        answer_options: this.#fb.array([
          this.#fb.control('', [Validators.required, Validators.maxLength(150)]),
          this.#fb.control('', [Validators.required, Validators.maxLength(150)])
        ])
      });

      this.questionsFormArray.push(questionFormGroup);
      this.questionId = newId;

      const questionsArray = this.questionsFormArray;

      if (questionsArray.length > 0) {
        const lastQuestionInArray = questionsArray.at(questionsArray.length - 1) as FormGroup;
        lastQuestionInArray.patchValue({
          question_text: '',
          required: false,
          answer_options: ['', '']
        });
      }
    }
  }

  #buildFormGroup() {
    this.formGroup = this.#fb.group({
      id: [this.id],
      name: ['', [Validators.required, Validators.maxLength(50)]],
      startDate: ['', [Validators.required]],
      endDate: ['', [Validators.required]],
      description: ['', [Validators.required, Validators.maxLength(300)]],
      targetGroup: ['', [Validators.required]],
      questions: this.#fb.array([]),
    }, { validators: this.dateRangeValidator });
  }

  dateRangeValidator: ValidatorFn = (control: AbstractControl) => {
    const formGroup = control as FormGroup;
    const startDateControl = formGroup.get('startDate');
    const endDateControl = formGroup.get('endDate');
    const startDate = new Date(startDateControl?.value);
    const endDate = new Date(endDateControl?.value);

    if (startDate && endDate) {
      if (startDate >= endDate) {
        endDateControl?.setErrors({ dateRange: 'De einddatum ligt voor de begin datum' });
        return { dateRange: true };
      } else if (endDate <= new Date()) {
        endDateControl?.setErrors({ dateRange: 'De einddatum ligt in het verleden' });
        return { dateRange: true };
      }
    }

    return null;
  };


  get questionsFormArray(): FormArray {
    return this.formGroup?.get('questions') as FormArray ?? new FormArray([]);
  }
  get specificQuestion(): FormGroup | null {
    const match = this.questionsFormArray.controls.find(
      (group: AbstractControl) => group.get('id')?.value === this.questionId
    );

    return (match as FormGroup) ?? null;
  }

  get specificanswer_options(): FormArray {
    const specificQuestion = this.specificQuestion;

    if (!specificQuestion?.get('answer_options')) {
      console.error('answer_options form array is undefined');
      return this.#fb.array([]);
    }

    return specificQuestion.get('answer_options') as FormArray;
  }

  public addQuestion(): void {

    const newQuestion = this.#fb.group({
      id: uuidv4(),
      question_text: ['', []],
      required: [false],
      answer_options: this.#fb.array([
        this.#fb.control('', []),
        this.#fb.control('', [])
      ]),
    });

    this.questionsFormArray.push(newQuestion);

    if (this.id) {
      const storedData = this.#localStorageService.getItemById('questionnaire', this.id);
      if (storedData) {
        storedData.questions.push(newQuestion.value);
        this.#localStorageService.editItem('questionnaire', storedData);
      }
    }
  }

  protected getanswer_optionsFormArray(questionId: string): FormArray {
    const match = this.questionsFormArray.controls.find(
      (group: AbstractControl) => group.get('id')?.value === questionId
    );

    if (!match) {
      console.warn(`No question found with ID: ${questionId}`);
      return this.#fb.array([]);
    }

    return match.get('answer_options') as FormArray;
  }

  protected addAnswer(): void {
    const answer_optionsArray = this.specificanswer_options;
    if (answer_optionsArray.length < 4) {
      answer_optionsArray.push(this.#fb.control('', []));
    }
  }

  protected removeAnswer(answerIndex: number): void {
    const answer_optionsArray = this.specificanswer_options;
    if (answer_optionsArray.length > 2) {
      answer_optionsArray.removeAt(answerIndex);
    }
  }

  #populateFormFields(): void {
    if (this.id) {
      this.#questionnaireService.getQuestionnaireById(this.id).pipe(takeUntil(this.#destroy$)).subscribe({
        next: (storedData) => {
          this.formGroup.patchValue({
            id: storedData.id,
            name: storedData.name,
            startDate: storedData.startDate.slice(0, 10),
            endDate: storedData.endDate.slice(0, 10),
            description: storedData.description,
            targetGroup: storedData.targetGroup,
          })

          const questionsFormArray = this.formGroup.get('questions') as FormArray;

          storedData.questions.forEach((q: any) => {
            const questionGroup = this.#fb.group({
              id: [q.id],
              question_text: [q.question_text, []],
              required: [q.required],
              answer_options: this.#fb.array(
                q.answer_options.map((answerObj: any) =>
                  this.#fb.control(typeof answerObj === 'string' ? answerObj : answerObj?.option_text || '', [])
                )
              )

            });

            questionsFormArray.push(questionGroup);
          }
          )
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
      });
      this.#questionnaireService.getTargetGroups().pipe(
        takeUntil(this.#destroy$)
      ).subscribe({
        next: (storedData) => {
          this.targetGroupOptions = storedData.map((group: any) => group.target_group_name);
        },
        error: (err) => {
          console.error('Error fetching target groups', err);
        }
      });
    }
  }


  public editQuestionnaire() {
    const questionnaireData = this.formGroup.value;

    if (this.formGroup.valid) {
      if (this.questionId == null) {

        const updatedQuestionnaire = {
          id: questionnaireData.id,
          name: questionnaireData.name,
          startDate: questionnaireData.startDate,
          endDate: questionnaireData.endDate,
          description: questionnaireData.description,
          targetGroup: questionnaireData.targetGroup,
          questions: questionnaireData.questions.map((q: any) => ({
            id: q.id,
            question_number: this.questionsFormArray.length,
            question_text: q.questionText ?? "",
            questionnaire_id: questionnaireData.id,
            answer_options: q.answer_options.map((a: any) =>
              typeof a === 'string' ? { option_text: a } : a
            ),
            required: q.required ?? false
          }))
        };

        this.#questionnaireService.sendQuestionnaireData('General', updatedQuestionnaire, questionnaireData.id);
      } else {
        const updatedQuestion = questionnaireData.questions.find((q: any) => q.id === this.questionId);

        if (updatedQuestion) {
          const updatedQuestionData = {
            id: updatedQuestion.id,
            question_number: this.questionsFormArray.length,
            question_text: updatedQuestion.question_text ?? "",
            questionnaire_id: questionnaireData.id,
            answer_options: updatedQuestion.answer_options.map((a: any) => ({
              option_text: typeof a === 'string' ? a : a.option_text ?? ""
            })),
            required: updatedQuestion.required ?? false
          };

          this.#questionnaireService.sendQuestionnaireData('Question', updatedQuestionData, questionnaireData.id);
        }

      }
    }
  }

  protected onEditClicked() {
    if (!this.formGroup.valid) {
      this.formGroup.markAllAsTouched();
    } else {
      this.editQuestionnaire();
    }
  }

  protected getQuestionValidationState(questionId: string): boolean {
    const questionIndex = Number(questionId);

    if (isNaN(questionIndex) || questionIndex < 0 || questionIndex >= this.questionsFormArray.length) {
      return false;
    }

    const questionControl = this.questionsFormArray.at(questionIndex);
    const question_text = questionControl.get('question_text')?.value;

    return questionControl.invalid && questionControl.touched && !question_text.trim();
  }

  protected getAnswerValidationState(questionId: string, answerIndex: number): boolean {
    const answerControl = this.getanswer_optionsFormArray(questionId).at(answerIndex);
    return answerControl.invalid && answerControl.touched;
  }


  protected onDeleteClicked() {
    if (this.id) {
      if (confirm("Verwijderen kan niet ongedaan gemaakt worden!\n Weet je het zeker?")) {
        this.#questionnaireService.deleteQuestionnaire(this.id);
      }
    }
  }

  ngOnDestroy() {
    this.#destroy$.next();
    this.#destroy$.complete();
  }
}
