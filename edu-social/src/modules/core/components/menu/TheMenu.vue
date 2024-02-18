<template>
  <div class="side-bar">
    <div class="logo">
      <span class="edu">Edu</span>
      <span class="social">Social</span>
    </div>
    <div v-if="pageFocus == Menu.FORUM">
      <div class="main-menu-block">
        <ul class="main-menu">
          <li class="item-menu" @click="navigateToPage(1, null)">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/forum.png" alt="" />
            </div>
            <span class="title-menu-item">Diễn đàn</span>
          </li>
          <li class="item-menu">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/library.png" alt="" />
            </div>
            <span class="title-menu-item">Thư viện</span>
          </li>
          <li class="item-menu">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/exam.png" alt="" />
            </div>
            <span class="title-menu-item">Kỳ thi</span>
          </li>
          <li class="item-menu">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/study.png" alt="" />
            </div>
            <span class="title-menu-item">Ôn tập</span>
          </li>
          <li class="item-menu">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/classroom.png" alt="" />
            </div>
            <span class="title-menu-item">Lớp học</span>
          </li>
          <li class="item-menu" @click="navigateToPage(6, null)">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/study-group.png" alt="" />
            </div>
            <span class="title-menu-item">Học nhóm</span>
          </li>
          <li class="item-menu">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/online-course.png" alt="" />
            </div>
            <span class="title-menu-item">Khóa học</span>
          </li>
          <li class="item-menu">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/analysis.png" alt="" />
            </div>
            <span class="title-menu-item">Báo cáo</span>
          </li>
          <li class="item-menu">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/event.png" alt="" />
            </div>
            <span class="title-menu-item">Sự kiện</span>
          </li>
          <li class="item-menu">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/glory.png" alt="" />
            </div>
            <span class="title-menu-item">Vinh danh</span>
          </li>
          <li class="item-menu">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/emotional.png" alt="" />
            </div>
            <span class="title-menu-item">Dòng trạng thái</span>
          </li>
          <li class="item-menu">
            <div class="block-img-menu-item">
              <img src="../../assets/pictures/introduce.png" alt="" />
            </div>
            <span class="title-menu-item">Giới thiệu</span>
          </li>
        </ul>
        <div class="item-menu">
          <div class="block-img-menu-item">
            <q-icon name="expand_less" size="32px"></q-icon>
          </div>
          <span calss="title-menu-item">Ẩn bớt</span>
        </div>
      </div>
      <div class="line"></div>
      <div>Khối lối tắt</div>
    </div>
    <GroupMenu v-if="pageFocus == Menu.GROUP"> </GroupMenu>
  </div>
</template>
<script src="../ts/theMenu.ts" lang="ts"></script>
<style lang="scss">
@import '../../css/the-menu.scss';
.color-icon {
  color: red;
}
</style>
