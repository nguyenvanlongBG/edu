import { User } from 'src/modules/core/models';
import { Comment } from './comment';
export interface Post {
  id: string;
  content: string;
  evaluate: number;
  comments: Comment[];
  user: User;
}
