<template>
  <div class="grid-container">
    <div
      v-for="item in $store.state.items"
      :key="item.item_id"
      class="grid-item"
    >
      <router-link v-bind:to="{ name: 'item' }">
        <img :src="require(`@/images/` + item.url)" alt="Image 1" @click="setCurrentItem(item)"/>
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  mounted() {
    this.collectionID = this.$store.state.currentCollection.collection_ID;
    this.getItemsByCollection(this.collectionID);
  },
  data() {
    return {
      collectionID: "",
    };
  },
  methods: {
    async getItemsByCollection(collectionID) {
      this.$store.commit("GET_ITEMS_BY_COLLECTION_ID", collectionID);
    },
    setCurrentItem(item) {
      this.$store.commit("SET_CURRENT_ITEM", item);
    }
  },
  watch: {
    "$store.state.currentCollection.collection_ID": {
      immediate: true, // Trigger the watcher immediately on component mount
      handler(newCollectionId) {
        this.collectionID = newCollectionId;
        this.getItemsByCollection(newCollectionId);
      },
    },
    "$store.state.items": {
      immediate: true,
      handler() {
        this.getItemsByCollection(this.collectionID);
      },
    },
  },
};
</script>

<style>
.grid-container {
  display: flex;
  flex-wrap: wrap; /* Wrap items to the next row */
  justify-content: space-between; /* Distribute items evenly with space in between */
}

.grid-item {
  flex-basis: calc(33% - 10px); /* Adjust the width of each grid item */
  margin-bottom: 10px; /* Add some spacing between items */
}

.grid-item img {
  width: 100%; /* Make the images fill the container */
  height: auto; /* Maintain aspect ratio */
  max-width: 100%; /* Limit the image width to the container */
  max-height: 100%; /* Limit the image height to the container */
}
</style>
