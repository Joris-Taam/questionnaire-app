import { Participant } from "./participant.model";

export interface ParticipantAnswerResponse {
  participant_id: string;
  questionnaire_id: string;
  question_text: string;
  option_text: string;
  participant: Participant; 
}

