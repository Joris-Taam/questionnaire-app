import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Question } from '../models/question.model';
import { Questionnaire } from '../models/questionnaire.model';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AnswerStats } from '../models/answerStats.model';

@Injectable({
  providedIn: 'root',
})

export class QuestionnaireService {
  #http = inject(HttpClient);
  #router = inject(Router);

  getAllQuestionnaires(): Observable<Questionnaire[]> {
    return this.#http.get<Questionnaire[]>('https://localhost:7281/api/Questionnaire');
  }

  getQuestionnaireById(id: string): Observable<Questionnaire> {
    return this.#http.get<Questionnaire>(`https://localhost:7281/api/Questionnaire/${id}`);
  }

  public sendQuestionnaireData(type: string, item: Questionnaire | Question, questionnaireId: string): void {
    let postUrl: string | undefined;

    if (item) {
      switch (type) {
        case 'All':
          postUrl = 'https://localhost:7281/api/Questionnaire';
          break;
        case 'General':
          postUrl = 'https://localhost:7281/api/Questionnaire/edit/General';
          break;
        case 'Question':
          postUrl = 'https://localhost:7281/api/Questionnaire/edit/Question';
          break;
        default:
          console.error('Invalid type provided.');
          return;
      }

      this.#http.post(postUrl, item, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }).subscribe({
        next: () => {
          console.log(`${item.id} successfully sent!`);
        },
        error: err => {
          console.error('Error while sending data:', err);
        },
        complete: () => {
          this.#router.navigateByUrl('/admin/questionoverview?id=' + questionnaireId);
        }
      });

    } else {
      console.error('No item received.');
    }
  }

  public deleteQuestion(questionId: string, questionnaireId: string): void {

    this.#http.delete(`https://localhost:7281/api/question/delete/${questionId}`)
      .subscribe({
        next: () => {
          console.log(`${questionId} successfully deleted!`);
          this.#router.navigateByUrl('/admin/questionoverview?id=' + questionnaireId);
        },
        error: err => {
          console.error('Error while sending data:', err);
        }
      });
  }

  public deleteQuestionnaire(questionnaireId: string): void {
    this.#http.delete(`https://localhost:7281/api/questionnaire/delete/${questionnaireId}`).subscribe({
      next: () => {
        console.log(`${questionnaireId} successfully deleted!`);
        this.#router.navigateByUrl('admin/questionnaireoverview')
      },
      error: err => {
        console.error('Error while sending data:', err);
      }
    })
  }

  public retrieveAnswerPercentages(questionnaireId: string):Observable<AnswerStats> {
    return this.#http.get<AnswerStats>(`https://localhost:7281/api/questionnaire/answerpercentages/${questionnaireId}`);
  }

  public getTargetGroups() {
     return this.#http.get<any>(`https://localhost:7281/api/Participant/target-groups`);
  }
} 