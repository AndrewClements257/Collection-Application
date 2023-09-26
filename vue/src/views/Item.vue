<template>
  <div class="item">
    <h1>{{ $store.state.currentItem.name }}</h1>
    <div class="info">
      <img class="main-img" :src="dynamicImage" alt="Image 1" />
      <div class="stats">
        <h2><strong>Year:</strong> 1999</h2>
        <h2><strong>Brand:</strong> Bandai</h2>
        <h2><strong>Condition:</strong> New</h2>
      </div>
    </div>
    <p>
      {{ $store.state.currentItem.description }}
    </p>

    <h1>Images</h1>
    <div class="images"></div>
  </div>
</template>

<script>
export default {
  computed: {
    dynamicImage() {
      try {
        // Load the image dynamically based on the name from Vuex store
        return require(`@/images/${this.$store.state.currentItem.url}`);
      } catch (err) {
        // Handle the error, e.g., return a default image
        console.error(err);
        return require('@/images/anguirus.jpg');
      }
    },
  },
};
</script>

<style scoped>
.item {
  margin-top: 60px;
  display: flex;
  flex-direction: column;
  margin-left: auto;
  margin-right: auto;
  padding: 0px 40px;
  max-width: 750px;
}

.info {
  display: flex;
  flex-direction: row;
}

h1 {
  font-size: 32px;
}

.main-img {
  margin-bottom: 10px;
  margin-right: 20px;
  height: 455px;
  width: 455px;
}

strong {
  color: white;
}

@media screen and (max-width: 600px) {
  .info {
  flex-direction: column;
}

.stats {
  margin-top: 10px;
  margin-bottom: 10px;
}

.main-img {
  margin-top: 5px;
  height: 100%;
  width: 100%;
}
}
</style>