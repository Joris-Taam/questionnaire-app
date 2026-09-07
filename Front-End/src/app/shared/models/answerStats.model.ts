export class AnswerStats {
    constructor(
      public questionText: string,
      public totalAnswers: number,
      public options: { optionText: string, count: number, percentage: number }[]
    ) { }
  }
  