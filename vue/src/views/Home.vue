<template>
  <div class="home">
    <div class="left-panel">
    <user-info class="info"></user-info>
    <collections-list class="list"></collections-list>
    <button v-on:click="displayCollectionModal()">Add New Collection</button>
    <button v-on:click="displayItemModal()">Add New Item</button>
    </div>
    <div class="overlay" v-show="showingCollectionModal">
    <new-collection-modal v-show="showingCollectionModal" @close="close"></new-collection-modal>
    </div>
    <div class="overlay" v-show="showingItemModal">
      <img :src="imageUrl" alt="Uploaded Image" v-if="imageUrl" />
      <new-item-modal v-show="showingItemModal" @close="close"></new-item-modal>
    </div>
    <user-collection class="collection"></user-collection>
  </div>
</template>

<script>
import UserInfo from '../components/UserInfo.vue';
import UserCollection from '../components/UserCollection.vue';
import CollectionsList from '../components/CollectionsList.vue';
import NewCollectionModal from '../components/NewCollectionModal.vue';
import NewItemModal from '../components/NewItemModal.vue';

export default {
  name: "home",
  components: {
    UserInfo,
    UserCollection,
    CollectionsList,
    NewCollectionModal,
    NewItemModal
  },
  data() {
    return{
    showingCollectionModal: false,
    showingItemModal: false,
    imageUrl: ''
    }
  },
  methods: {
    async displayCollectionModal() {
    this.showingCollectionModal = !this.showingCollectionModal;
  },
  async displayItemModal() {
    this.showingItemModal = !this.showingItemModal;
  },
  close() {
      this.showingCollectionModal = false;
      this.showingItemModal = false;
    },
  }
};
</script>

<style scoped>

.home {
  display: flex;
  flex-direction: row;
  margin: 20px;
}

.collection {
  flex: 5; /* Adjust the width of user-collection */
}

.left-panel {
  flex: 1; 
  display: flex;
  flex-direction: column;
  margin-right: 10px;
}

.list {
  margin-top: 10px;
}

.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: rgba(0, 0, 0, 0.6);
  z-index: 9999;
}
</style>
