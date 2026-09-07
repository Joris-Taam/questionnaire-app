import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ParticipantAnswer } from '../models/ParticipantAnswer.model';

@Injectable({
  providedIn: 'root'
})
export class ParticipantAnswerService {

  private apiUrl = 'https://localhost:7281/api/ParticipantAnswer';
  #http = inject(HttpClient);
  
  postAnswers(result: ParticipantAnswer): void {
    if (result) {
      this.#http.post(this.apiUrl, result, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }).subscribe({
        error: err => console.error(err),
      })
    } else {
      console.log("invalid result");
    }
  }
}

