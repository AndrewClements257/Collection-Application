<template>
  <div class="new-collection" v-on:click="displayCollectionModal()">
    <h3>Add New Collection</h3>
    <new-collection-modal v-if="showingCollectionModal"></new-collection-modal>
  </div>
</template>

<script>
import NewCollectionModal from "./NewCollectionModal.vue";

export default {
  components: { NewCollectionModal },
  data() {
    return {
      showingCollectionModal: false,
    };
  },
  methods: {
    async displayCollectionModal() {
      this.showingCollectionModal = !this.showingCollectionModal;
    },
  },
  name: "new-collection",
};
</script>

<style>
.new-collection {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  background-color: gray;
  border-radius: 10px;
  padding: 20px;
  width: 100%;
}

.new-collection h3 {
  color: white;
}
</style>