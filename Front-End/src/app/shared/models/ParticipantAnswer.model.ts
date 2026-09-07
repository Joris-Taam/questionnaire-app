export class ParticipantAnswer {
  questionnaire_id: string;
  participant_id: string;

  question_text: string;
  option_text: string;

  constructor(
    questionnaire_id: string,
    participant_id: string,
    question_text: string,
    option_text: string
  ) {
    this.questionnaire_id = questionnaire_id;
    this.participant_id = participant_id;
    this.option_text = option_text;
    this.question_text = question_text;
  }
}
