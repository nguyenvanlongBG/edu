export interface User {
  id: string;
  name: string;
  avatarUrl: string;
}
export interface Answer {
  id: string;
  content: string;
}
export interface Question {
  id: string;
  content: string;
  type: number; // 1: Tự luận 2: Trắc nghiệm 1 đáp án 3: Chọn nhiều đáp án 4: Điền đáp án
  result: string;
  answers: Answer[];
}
export interface ResultValidateBase {
  isValid: boolean;
  message: string;
}
