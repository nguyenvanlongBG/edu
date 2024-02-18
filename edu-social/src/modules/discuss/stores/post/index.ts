import { defineStore } from 'pinia';
import { Post } from '../../models/post';
import { Comment } from '../../models/comment';
export const usePostStore = defineStore('post', {
  state: () => ({
    posts: [
      {
        id: '1',
        content: `<math>

		<!-- Creating Matrix -->
		<mrow>
			<mi>A</mi>
			<mo>=</mo>

			<mfenced open="[" close="]">

				<mtable>
					<mtr>
						<mtd>
							<mi>a</mi>
						</mtd>
						<mtd>
							<mi>b</mi>
						</mtd>
					</mtr>

					<mtr>
						<mtd>
							<mi>x</mi>
						</mtd>
						<mtd>
							<mi>y</mi>
						</mtd>
					</mtr>

				</mtable>

			</mfenced>
		</mrow>

		<!-- Creating equation -->
		<br><br>
		<msub>
			<mi>Geeks</mi>
			<mn>4</mn>
		</msub>
		<mo>+</mo>
		<mn>Geeks</mn>
		<mo>=</mo>
		<msub>
			<mi>G</mi>
		</msub>
		<mo>→</mo>
		<msub>
			<mi>e</mi>
			<mn>2</mn>
		</msub>
		<mo>→</mo>
		<mi>k</mi>
		<mi>s</mi>
		<mn>4
		</mn>
		<msub>
			<mi>G</mi>
		</msub>
		<mo>→</mo>
		<msub>
			<mi>e</mi>
			<mn>2</mn>
		</msub>
		<mo>→</mo>
		<mi>k</mi>
		<mi>s</mi>
	</math>`,
        evaluate: 4,
        comments: [],
        user: {
          id: '1',
          name: 'Nguyễn Long',
          avatarUrl:
            'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR4RGwl1mqaD9sd_JO7m0KPAlgZmnClgtkEqQ&usqp=CAU',
        },
      },
      {
        id: '2',
        content: 'Biến đổi Fourier phát biểu như thế nào ?',
        evaluate: 5,
        comments: [],
        user: {
          id: '2',
          name: 'Long BG',
          avatarUrl:
            'https://toigingiuvedep.vn/wp-content/uploads/2021/06/anh-chat-ngau-nu.jpg',
        },
      },
    ] as Post[],
  }),
  getters: {
    getPosts: (state) => {
      return state.posts;
    },
  },
  actions: {
    savePost(post: Post) {
      this.posts.push(post);
    },
    createComment(postID: string, comment: Comment) {
      const postNeedAddComment = this.posts.find((post) => {
        if (post.id == postID) return post;
      });
      if (postNeedAddComment) {
        postNeedAddComment.comments.push(comment);
      }
    },
  },
});
