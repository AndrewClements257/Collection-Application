<template>
  <div id="app">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
    </div>
    <router-view />
  </div>
</template>

<style scoped>

#app {
  background-image: url("assets/gradient-background.jpg");
  background-size: cover;
  min-height: 110vh;
  color: white;
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
}

</style>