<template>
  <div id="app">
    <div
      id="nav"
      v-if="this.$route.name == 'home' || this.$route.name == 'item'"
    >
      <div class="menu" @click="toggleMenu()" v-if="!isScreenWidthLarge">
        <div class="bar"></div>
        <div class="bar"></div>
        <div class="bar"></div>
      </div>
      <div>
        <router-link
          v-bind:to="{ name: 'logout' }"
          v-if="$store.state.token != ''"
          >Logout</router-link
        >&nbsp;&nbsp;
        <router-link
          v-bind:to="{ name: 'home' }"
          v-if="this.$route.name == 'profile' || this.$route.name == 'item'"
          >Home</router-link
        >
      </div>
    </div>
    <router-view :menuOpen="menuOpen" @toggleMenu="toggleMenu"></router-view>
  </div>
</template>

<script>
export default {
  data() {
    return {
      menuOpen: false,
      screenWidth: window.innerWidth,
    };
  },
  methods: {
    toggleMenu() {
      this.menuOpen = !this.menuOpen;
    },
    updateScreenWidth() {
      this.screenWidth = window.innerWidth;
    },
  },
  computed: {
    isScreenWidthLarge() {
      // Change 800 to your desired breakpoint
      return this.screenWidth > 800;
    },
  },
  created() {
    // Add event listener to update screenWidth when the window is resized
    window.addEventListener("resize", this.updateScreenWidth);
  },
  destroyed() {
    // Remove event listener when the component is destroyed
    window.removeEventListener("resize", this.updateScreenWidth);
  },
};
</script>

<style scoped>
#app {
  background-image: url("assets/gradient-background.jpg");
  background-position: center; /* Make sure the background is centered */
  background-size: 100% 100%; /* This will stretch the image to fill the entire element */
  background-repeat: no-repeat; /* This will prevent the image from repeating */
  min-height: 100vh;
  color: white;
  display: flex;
  flex-direction: column; /* this allows your content to stretch vertically */
}

#nav {
  width: 100%;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  padding: 0 20px;
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  border: 1px solid rgba(255, 255, 255, 0.2);
  height: 50px;
  position: fixed;
  top: 0;
  z-index: 10000;
}

a {
  color: white;
}

a:hover {
  color: dodgerblue;
}

.menu:hover {
  cursor: pointer;
  color: dodgerblue;
}

.menu {
  cursor: pointer;
  margin-left: 5px;
}
.bar {
  width: 30px;
  height: 4px;
  background-color: white;
  margin: 6px 0;

}

</style>