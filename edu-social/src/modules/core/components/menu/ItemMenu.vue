<template>
  <div class="item-menu">
    <div class="item-parent" :class="getItemParentClass" @click="onCLickItem">
      <slot name="icon-item"></slot>
      <Transition name="slide-fade">
        <span class="name-item-menu" v-if="!isCollapse">
          {{ item.title }}
          <i
            class="fas fa-minus"
            v-if="isShowChild && item.childrenItem.length > 0"
          ></i>
          <i
            class="fas fa-plus"
            v-if="!isShowChild && item.childrenItem.length > 0"
          ></i>
        </span>
      </Transition>
    </div>
    <div :class="getSubMenuClasses" v-if="item.childrenItem.length > 0">
      <div
        class="item-child name-item-sub-menu"
        :class="getClassChild(itemChild.routeName)"
        v-for="(itemChild, index) in item.childrenItem"
        :key="index"
        @click="onClickItemChild"
      >
        {{ itemChild.title }}
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: 'ItemMenu',
  props: {
    item: {
      type: Object,
      required: true,
    },
    isCollapse: {
      type: Boolean,
      required: false,
      default: false,
    },
    isMain: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
  data() {
    return {
      status: 0,
      indexChildClicked: null,
      isShowChild: false,
      isMainItem: false,
    };
  },
  watch: {
    isMain() {
      this.isMainItem = this.isMain;
      if (!this.isMainItem) {
        if (this.item.childrenItem.length > 0) {
          this.isShowChild = false;
        }
      }
    },
  },
  computed: {
    getItemParentClass() {
      if (this.isMainItem) {
        if (this.isShowChild) {
          return 'item-parent--focus';
        } else {
          return 'item-parent--clicked';
        }
      } else {
        return '';
      }
    },
    getSubMenuClasses() {
      if (this.item.childrenItem.length > 0) {
        if (this.isCollapse) {
          return 'sub-menu-right';
        } else {
          if (!this.isShowChild) {
            return 'no-display';
          } else {
            return 'sub-menu-bottom';
          }
        }
      }
      return 'no-display';
    },
  },
  created() {
    if (this.item.routerName == this.$route.name) {
      this.isMainItem = true;
    }
  },
  methods: {
    getClassChild(routerName) {
      if (routerName == this.$route.path) {
        this.status = 2;
      }
      return {
        'item-child--clicked': routerName == this.$route.path,
      };
    },
    onCLickItem() {
      if (this.item.childrenItem.length > 0) {
        if (this.isShowChild) {
          this.isShowChild = false;
        } else {
          this.isShowChild = true;
        }
      } else {
        this.$emit('onClick');
      }
    },
    onClickItemChild() {
      this.$emit('onClick');
    },
  },
};
</script>
<style scoped>
.item-menu {
  position: relative;
}
.item-parent {
  display: flex;
  align-items: center;
  padding-top: 10px;
  padding-bottom: 10px;
  height: 48px;
  box-sizing: border-box;
  border-radius: 8px;
  padding-left: 12px;
  padding-right: 12px;
  column-gap: 12px;
  cursor: pointer;
  color: white;
  text-transform: uppercase;
}
.item-parent:hover {
  position: relative;
  background-color: #e0e0e0;
  color: #222;
}
.item-parent--clicked {
  background-color: #e0e0e0;
  color: #222;
}
.item-parent--focus {
  background-color: #bdbdbd;
  color: #222;
}
.slide-fade-enter-active {
  transition: all 0.3s ease-out;
}

.slide-fade-leave-active {
  transition: all 0.3s ease-out;
}

.slide-fade-enter-from,
.slide-fade-leave-to {
  transform: translateX(0px);
  opacity: 0;
}
.sub-menu-right {
  position: absolute;
  background-color: #bdbdbd;
  border-radius: 8px;
  top: 0px;
  left: 47px;
  visibility: hidden;
}
.item-menu:hover .sub-menu-right {
  visibility: visible;
}
.sub-menu-bottom {
  top: 48px;
  left: 0px;
  width: auto;
  background-color: #e0e0e0;
  color: #222;
  list-style-type: square;
  border-radius: 8px;
}
.no-display {
  display: none;
}
.item-child {
  height: 48px;
  box-sizing: border-box;
  border-radius: 8px;
  display: flex;
  justify-content: center;
  align-items: center;
  padding-top: 10px;
  padding-bottom: 10px;
  cursor: pointer;
  text-transform: uppercase;
}
.item-child:hover {
  background-color: #fafafa;
}
.item-child--clicked {
  background-color: #fafafa;
}
.name-item-sub-menu {
  width: 260px;
  display: flex;
}
.name-item-menu {
  width: 200px;
  display: flex;
  align-items: start;
}
.name-item-menu .fas {
  color: #ea4f4c;
  padding-left: 4px;
}
</style>
