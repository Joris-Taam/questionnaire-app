import { v4 as uuidv4 } from 'uuid';
import { Question } from './question.model';

export class Questionnaire {
  public id: string;
  public name: string;  
  public startDate: string;
  public endDate: string;
  public description: string;
  public targetGroup: string;
  public questions: Question[];

  constructor(obj: any) {
    this.id = obj.id ?? uuidv4();
    this.name = obj.name ?? ''; 
    this.startDate = obj.startDate ?? '';
    this.endDate = obj.endDate ?? '';
    this.description = obj.description ?? '';
    this.targetGroup = obj.targetGroup ?? '';
    this.questions = (obj.questions ?? []).map((q: any) => ({
      id: q.id,
      questionText: q.question_text,
      answerOptions: q.answer_options
    }));
  }
}
