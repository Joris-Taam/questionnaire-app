import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ParticipantService } from '../../shared/services/participant.service';
import { ParticipantAnswerResponse } from '../../shared/models/participant-answer-response.model';
import { CommonModule } from '@angular/common';
import { TargetGroup } from '../../shared/models/TargetGroup.model';
import { Participant } from '../../shared/models/participant.model';

@Component({
  selector: 'app-participant-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './participant-detail.component.html',
  styleUrls: ['./participant-detail.component.scss']
})
export class ParticipantDetailComponent implements OnInit {
  participant_name: string | null = null;
  participant!: Participant;
  questionnaireId: string | null = null;
  participantAnswers: ParticipantAnswerResponse[] = [];
  targetGroups: TargetGroup[] | null = null;
  participantId: string | null = null;

  #route = inject(ActivatedRoute);
  #participantService = inject(ParticipantService);


  ngOnInit(): void {
    this.questionnaireId = this.#route.snapshot.queryParamMap.get('questionnaireid');
    this.participantId = this.#route.snapshot.queryParamMap.get('id');
    this.participant_name= this.#route.snapshot.queryParamMap.get('name');

    if (this.questionnaireId && this.participantId) {
      this.#participantService.getAnswersByparticipantId(this.questionnaireId, this.participantId).subscribe({
        next: (data: ParticipantAnswerResponse[]) => {
          this.participantAnswers = data;

          if (data.length > 0) {
            // this.targetGroups = data[0].participant?.target_groups ?? null;
          }
        },
        error: (err) => {
          console.error('Error fetching participant answers:', err);
        }
      });
      // this.#participantService.getParticipantByPublicId(this.participantId).subscribe({
      //   next: (data: Participant) => {
      //     console.log(data);
      //     this.participant = data;
      //   },
      //   error: (err) => {
      //     console.error('Error fetching participant answers:', err);
      //   }
      // });
    } else {
      console.error('No questionnaire ID provided');
    }
  }

}
