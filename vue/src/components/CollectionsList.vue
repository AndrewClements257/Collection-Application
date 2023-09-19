<template>
  <div class="collection-list">
    <div v-for="collection in $store.state.collections" :key="collection.collection_id">
        <h3 @click="setCurrentCollection(collection)">{{ collection.name }}</h3>
    </div>
  </div>
</template>

<script>

export default {
  mounted() {
    this.$store.commit('GET_COLLECTIONS'); 
  },
methods: {
  setCurrentCollection(collection) {
      this.$store.commit('SET_CURRENT_COLLECTION', collection);
    },
  getCollections() {
   this.$store.commit('GET_COLLECTIONS'); 
  }
},
name: "collections-list",
data() {
  return {
    collections: []
  }
}
}
</script>

<style>
.collection-list {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  background: rgba(255, 255, 255, 0.1); 
  backdrop-filter: blur(10px); 
  border: 1px solid rgba(255, 255, 255, 0.2);
  border-radius: 10px;
  padding: 20px;
  width: 100%;
}

.collection-list h3 {
  color: white;
}

.collection-list h3:hover {
  color: dodgerblue;
  cursor: pointer;
}
</style>