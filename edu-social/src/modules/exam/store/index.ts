import { defineStore } from 'pinia';
import { Answer, Question } from 'src/modules/core/models';
import request from '../../core/utils/request';
export const useExamStore = defineStore('exam', {
  state: () => ({
    questions: [
      {
        id: '1',
        content: '1+1',
        type: 2,
        result: '1',
        answers: [
          { id: '1', content: '3' },
          { id: '2', content: '4' },
          { id: '3', content: '5' },
          { id: '4', content: '6' },
        ] as Answer[],
      },
      {
        id: '2',
        content: '1+2',
        type: 2,
        result: '4',
        answers: [
          { id: '1', content: '3' },
          { id: '2', content: '4' },
          { id: '3', content: '5' },
          { id: '4', content: '6' },
        ] as Answer[],
      },
      {
        id: '3',
        content: '2+2',
        type: 3,
        result: '2,3',
        answers: [
          { id: '1', content: '8' },
          { id: '2', content: '9' },
          { id: '3', content: '81' },
          { id: '4', content: '91' },
        ] as Answer[],
      },
      {
        id: '4',
        content: '2+3',
        type: 2,
        result: '2',
        answers: [
          { id: '1', content: '6' },
          { id: '2', content: '7' },
          { id: '3', content: '8' },
          { id: '4', content: '9' },
        ] as Answer[],
      },
    ] as Question[],
  }),
  getters: {
    getQuestions: (state) => state.questions,
  },
  actions: {
    async getQuestionsExamAsymc() {
      const dataQuestions = (await request({
        url: 'test',
        method: 'get',
        params: { testId: 1 },
      })) as Question[];
      this.questions = dataQuestions;
    },
    async updateExam(id: string) {
      await request({ url: 'test/' + id, method: 'put', data: {} });
    },
  },
});
