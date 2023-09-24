<template>
  <div id="app">
    <div id="nav" v-if="this.$route.name == 'home' || this.$route.name == 'item'">
      <router-link
        v-bind:to="{ name: 'home' }"
        v-if="this.$route.name == 'profile'"
        >Home</router-link
      >&nbsp;&nbsp;
      <router-link
        v-bind:to="{ name: 'logout' }"
        v-if="$store.state.token != ''"
        >Logout</router-link
      >
    </div>
    <router-view />
  </div>
</template>

<style scoped>
#app {
  background-image: url("assets/gradient-background.jpg");
  background-position: center; /* Make sure the background is centered */
  background-size: 100% 100%;  /* This will stretch the image to fill the entire element */
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
  padding: 0 20px;
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  border: 1px solid rgba(255, 255, 255, 0.2);
  height: 50px;
  position: fixed;
  top: 0;
  z-index: 10000;
}

#nav > a {
  color: white;
}

#nav > a:hover {
  color: dodgerblue;
}
</style>