<template>
  <div class="conversation">
    <div class="menu-chat">
      <a href="#" @click="back" class="back"
        ><i class="fa fa-angle-left" v-if="isShowBack"></i>
        <img src="https://i.imgur.com/G4EjwqQ.jpg" draggable="false"
      /></a>
      <div>
        <div class="name">Haha Chat</div>
        <div class="members"><b>You</b>, Long Bg, Dragon, Eagle</div>
      </div>
    </div>
    <ol class="chat">
      <li class="other">
        <div class="msg">
          <div class="user">
            <!-- Avatar -->
            <div class="avatar">
              <a href="" class="circle">
                <img
                  width="45"
                  height="45"
                  src="http://www.gravatar.com/avatar/9017a5f22556ae0eb7fb0710711ec125?s=128"
                />
              </a>
            </div>
            You
            <span class="range admin">Admin</span>
          </div>
          <p>Want to go dinner? <emoji class="pizza"></emoji></p>
          <span>20:17</span>
        </div>
      </li>
      <li class="self">
        <div class="msg">
          <EditorComponent :readonly="true" />
          <span>20:18</span>
        </div>
      </li>
      <li class="other">
        <div class="msg">
          <div class="user">Dragon</div>
          <EditorComponent :readonly="true" />
          <span>20:18</span>
        </div>
      </li>
      <li class="self">
        <div class="msg">
          <EditorComponent :readonly="true" />
          <span>20:18</span>
        </div>
      </li>
      <li class="other">
        <div class="msg">
          <div class="user">Dragon</div>
          <p>
            <emoji class="scream"></emoji>
          </p>
          <p>Hand it to me! <emoji class="please"></emoji></p>
          <span>20:18</span>
        </div>
      </li>
      <li class="other">
        <div class="msg">
          <div class="user">Long Bg</div>
          <p>Thank you! <emoji class="hearth_blue"></emoji></p>
          <span>20:20</span>
        </div>
      </li>
      <div class="day">Today</div>
      <li class="self">
        <div class="msg">
          <p>Who wants to play Minecraft?</p>
          <span>18:03</span>
        </div>
      </li>
      <li class="other">
        <div class="msg">
          <div class="user">OK</div>
          <p>Come on, I didn't play it for four months</p>
          <span>18:07</span>
        </div>
      </li>
      <li class="self">
        <div class="msg">
          <p>Ehh, the launcher crash... <emoji class="cryalot"></emoji></p>
          <span>18:08</span>
        </div>
      </li>
      <li class="other">
        <div class="msg">
          <div class="user">Charo</div>
          <p>
            <emoji class="lmao"></emoji>
          </p>
          <p>Sure that is the base code</p>
          <p>I told it to Mojang</p>
          <span>18:08</span>
        </div>
      </li>
      <li class="self">
        <div class="msg">
          <p>It's a joke</p>
          <p>Moai attack!</p>
          <p>
            <emoji class="moai"></emoji>
            <emoji class="moai"></emoji>
            <emoji class="moai"></emoji>
            <emoji class="moai"></emoji>
            <emoji class="moai"></emoji>
            <emoji class="moai"></emoji>
          </p>
          <span>18:10</span>
        </div>
      </li>
      <li class="other">
        <div class="msg">
          <div class="user">Charo</div>
          <p>XD</p>
          <p>
            <emoji class="funny"></emoji>
          </p>
          <p>Heart for this awesome design!</p>
          <span>18:08</span>
        </div>
      </li>
      <p class="notification">
        <span>18:09</span><span>David joined the group</span>
      </p>
      <li class="self">
        <div class="msg">
          <p>
            Heeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeellooooooooooooooooooooooooooooooo
            David
            <emoji class="smile" />
          </p>
          <span>18:09</span>
        </div>
      </li>
      <li class="other">
        <div class="msg">
          <div class="user">David</div>
          <p>What is that <emoji class="shit"></emoji> ?</p>
          <span>18:10</span>
        </div>
      </li>
      <p class="notification">
        <span>18:11</span>
      </p>
      <p><span>David left the group</span></p>
      <li class="other">
        <div class="msg">
          <div class="user">Dragon</div>
          <p>Lol?</p>
          <span>18:12</span>
        </div>
      </li>
      <li class="other">
        <div class="msg">
          <div class="user">You<span class="range admin">Admin</span></div>
          <p>I'm boring...</p>
          <p>Who wants to do some logarithms? <emoji class="smile"></emoji></p>
          <span>18:15</span>
        </div>
      </li>
    </ol>
    <div class="typezone">
      <form>
        <!-- <textarea type="text" placeholder="Say something"> </textarea> -->
        <div class="text-editor">
          <EditorComponent />
        </div>
        <input type="submit" class="send-chat" value=" " />
      </form>
      <div class="emojis"></div>
    </div>
  </div>
</template>
<script>
import EditorComponent from '../rich-editor/EditorComponent.vue';
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'ChatComponent',
  components: { EditorComponent },
  props: {
    isShowBack: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
  emits: ['close'],
  setup(props, ctx) {
    async function back() {
      console.log('Chuẩn bị emit');
      await ctx.emit('close');
      console.log('Emit xong');
    }
    return {
      back,
    };
  },
});
</script>
<style scope lang="scss">
@import url(../../css/chat.scss);
</style>
