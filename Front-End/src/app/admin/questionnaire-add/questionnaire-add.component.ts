import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocalStorageService } from '../../shared/services/localstorage.service';
import { FormGroup, FormArray, FormsModule, ReactiveFormsModule, Validators, AbstractControl, FormBuilder, ValidatorFn } from '@angular/forms';
import { v4 as uuidv4 } from 'uuid';
import { SharedModule } from '../../shared/shared.module';
import { QuestionnaireService } from '../../shared/services/questionnaire.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-questionnaire-add',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule, SharedModule],
  templateUrl: './questionnaire-add.component.html',
  styleUrls: ['./questionnaire-add.component.scss'],
})
export class QuestionnaireAddComponent {
  protected headerText: string = "Vragenlijst aanmaken";
  protected dateError: boolean = false;
  protected formGroup!: FormGroup;
  protected targetGroupOptions = [];
  #destroy$ = new Subject<void>();

  #questionnaireService = inject(QuestionnaireService);
  #fb = inject(FormBuilder);

  ngOnInit() {
    this.#buildFormGroup();
    this.addQuestion();
    this.#loadTargetGroups();
  }

  #loadTargetGroups() {
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

  #buildFormGroup() {
    this.formGroup = this.#fb.group({
      id: [uuidv4()],
      name: ['', [Validators.required, Validators.maxLength(50)]],
      startDate: ['', [Validators.required]],
      endDate: ['', [Validators.required]],
      description: ['', [Validators.required, Validators.maxLength(300)]],
      targetGroup: ['Later kiezen', [Validators.required]],
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

  protected get questionsFormArray(): FormArray {
    return this.formGroup.get('questions') as FormArray;
  }

  addQuestion(): void {
    const questionGroup = this.#fb.group({
      questionText: ['', []],
      required: false,
      answer_options: this.#fb.array([
        this.#fb.control({ option_text: '' }, []),
        this.#fb.control({ option_text: '' }, []),
      ]),
    });
    this.questionsFormArray.push(questionGroup);
  }

  protected saveQuestionnaire() {
    if (this.formGroup.valid) {
      const questionnaireData = this.formGroup.value;

      const questionnaire = {
        id: questionnaireData.id,
        name: questionnaireData.name,
        startDate: questionnaireData.startDate,
        endDate: questionnaireData.endDate,
        description: questionnaireData.description,
        targetGroup: questionnaireData.targetGroup,
        questions: questionnaireData.questions.map((q: any) => ({
          id: uuidv4(),
          question_text: q.questionText ?? "",
          questionnaire_id: questionnaireData.id,
          answer_options: q.answer_options.map((a: any) =>
            typeof a === 'string' ? { option_text: a } : a
          ),
          required: q.required ?? false
        }))
      };
      console.log(questionnaire);
      this.#questionnaireService.sendQuestionnaireData('All', questionnaire, questionnaire.id);
    }
  }

  protected isFormValid(): boolean {
    console.log('Form validity:', this.formGroup.valid);
    return this.formGroup.valid;
  }

  protected onSaveClicked() {
    if (!this.formGroup.valid) {
      this.formGroup.markAllAsTouched();
    } else {
      this.saveQuestionnaire();
    }
  }

  ngOnDestroy() {
    this.#destroy$.next();
    this.#destroy$.complete();
  }
}
