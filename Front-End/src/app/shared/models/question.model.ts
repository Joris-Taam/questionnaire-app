export class Question {
  constructor(
    public id: string,
    public question_text: string,
    public questionnaire_id :string,
    public answer_options: { option_text: string }[],
    public question_number: number,
    public required: boolean,
  ) {}
}
