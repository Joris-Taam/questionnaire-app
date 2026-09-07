import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Participant } from '../models/participant.model';
import { ParticipantAnswerResponse } from '../models/participant-answer-response.model';

import { SaveTargetGroup } from '../models/SaveTargetGroup.model';
import { TargetGroup } from '../models/TargetGroup.model';
import { ParticipantGroup } from '../models/ParticipantGroup.model';

interface ParticipantWithAnswers {
  public_id: string;
  participant_name: string;
  answers: ParticipantAnswerResponse[];
}


@Injectable({
  providedIn: 'root'
})
export class ParticipantService {
  #http = inject(HttpClient);

  private apiUrl = 'https://localhost:7281/api/participant';

  public getParticipantByPublicId(publicId: string): Observable<Participant> {
    return this.#http.get<Participant>(`${this.apiUrl}/${publicId}`);
  }

  public getAnswersByparticipantId(questionnaireId: string, participantId: string): Observable<ParticipantAnswerResponse[]> {
    return this.#http.get<ParticipantAnswerResponse[]>(
      `${this.apiUrl}/${questionnaireId}/${participantId}`
    );
  }

  getAnswersByQuestionnaireGroup(questionnaireId: string): Observable<ParticipantWithAnswers[]> {
    return this.#http.get<ParticipantWithAnswers[]>(

      `${this.apiUrl}/by-questionnaire-group/${questionnaireId}`
    );
  }
  public getTargetGroups(): Observable<TargetGroup[]> {
    return this.#http.get<TargetGroup[]>(`${this.apiUrl}/target-groups`);
  }

  public getParticipantLinkedGroups(name: string): Observable<any[]> {
    return this.#http.get<any[]>(`${this.apiUrl}/participant-linked-groups?name=${encodeURIComponent(name)}`);
  }

  public saveTargetGroup(data: SaveTargetGroup): Observable<SaveTargetGroup> {
    return this.#http.post<SaveTargetGroup>(`${this.apiUrl}/save-target-group`, data);
  }

  public removeTargetGroup(contactPerson: string, targetGroupName: string): void {
    const params = new HttpParams()
      .set('contactPerson', contactPerson)
      .set('targetGroupName', targetGroupName);

    this.#http.delete(`${this.apiUrl}/remove-target-group`, { params }).subscribe({
      next: () => {
        console.log('Target group removed successfully');
      }
    });
  }

}
