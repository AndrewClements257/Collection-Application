<template>
  <div class="home">
    <div class="left-panel" v-if="isScreenWidthLarge">
      <user-info></user-info>
      <collections-list class="list"></collections-list>
      <button v-on:click="displayCollectionModal()">Add New Collection</button>
      <button v-on:click="displayItemModal()">Add New Item</button>
      <button v-on:click="displayDeleteItemModal()">Delete Item</button>
      <button v-on:click="displayDeleteCollectionModal()">Delete Collection</button>
    </div>
     <div class="mobile-menu" v-bind:class="{ open: menuOpen }">
      <user-info class="info"></user-info>
      <collections-list class="list-mobile"></collections-list>
      <div class="mobile-buttons">
      <button v-on:click="displayCollectionModal()">Add New Collection</button>
      <button v-on:click="displayItemModal()">Add New Item</button>
      <button v-on:click="displayDeleteItemModal()">Delete Item</button>
      <button v-on:click="displayDeleteCollectionModal()">Delete Collection</button>
     </div>
    </div>
    <div class="overlay" v-show="showingCollectionModal">
      <new-collection-modal
        v-show="showingCollectionModal"
        @close="close"
      ></new-collection-modal>
    </div>
    <div class="overlay" v-show="showingItemModal">
      <img :src="imageUrl" alt="Uploaded Image" v-if="imageUrl" />
      <new-item-modal v-show="showingItemModal" @close="close"></new-item-modal>
    </div>
    <div class="overlay" v-show="showingDeleteItemModal">
      <delete-item-modal
        v-show="showingDeleteItemModal"
        @close="close"
      ></delete-item-modal>
    </div>
    <div class="overlay" v-show="showingDeleteCollectionModal">
      <delete-collection-modal
        v-show="showingDeleteCollectionModal"
        @close="close"
      ></delete-collection-modal>
    </div>
    <h1 v-if="!isScreenWidthLarge" class="collection-name">{{ $store.state.currentCollection.name }} </h1>
    <user-collection class="collection"></user-collection>
  </div>
</template>

<script>
import UserInfo from "../components/UserInfo.vue";
import UserCollection from "../components/UserCollection.vue";
import CollectionsList from "../components/CollectionsList.vue";
import NewCollectionModal from "../components/NewCollectionModal.vue";
import NewItemModal from "../components/NewItemModal.vue";
import DeleteItemModal from "../components/DeleteItemModal.vue";
import DeleteCollectionModal from "../components/DeleteCollectionModal.vue";

export default {
  name: "home",
  components: {
    UserInfo,
    UserCollection,
    CollectionsList,
    NewCollectionModal,
    NewItemModal,
    DeleteItemModal,
    DeleteCollectionModal,
  },
  data() {
    return {
      showingCollectionModal: false,
      showingItemModal: false,
      showingDeleteItemModal: false,
      showingDeleteCollectionModal: false,
      imageUrl: "",
      screenWidth: window.innerWidth,
    };
  },
  methods: {
    async displayCollectionModal() {
      this.showingCollectionModal = !this.showingCollectionModal;
    },
    async displayItemModal() {
      this.showingItemModal = !this.showingItemModal;
    },
    async displayDeleteItemModal() {
      this.showingDeleteItemModal = !this.showingDeleteItemModal;
    },
    async displayDeleteCollectionModal() {
      this.showingDeleteCollectionModal = !this.showingCollectionModal;
    },
    close() {
      this.showingCollectionModal = false;
      this.showingItemModal = false;
      this.showingDeleteItemModal = false;
      this.showingDeleteCollectionModal = false;
    },
    updateScreenWidth() {
      this.screenWidth = window.innerWidth;
    },
    toggleMenu() {
      this.$emit("toggleMenu");
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
  props: {
    menuOpen: {
      type: Boolean,
      required: true,
    },
  },
};
</script>

<style scoped>
.home {
  display: flex;
  flex-direction: row;
  margin: 20px;
  margin-top: 60px;
}

.collection {
  flex: 5; /* Adjust the width of user-collection */
  margin-left: 22%;
  width: 75%;
}

/* Initial styles */
.mobile-menu {
  position: fixed;
  top: 0;
  left: -100%; /* Initially positioned off the screen */
  width: 100%;
  height: 100%;
  background: #333;
  transition: left 0.3s;
  overflow-y: auto; /* for longer menus */
  color: #fff;
  padding-top: 50px;
}

.mobile-menu a {
  color: #fff;
  text-decoration: none;
}

.mobile-menu ul {
  list-style: none;
  padding: 0;
}

/* Class that will be toggled */
.mobile-menu.open {
  left: 0;
}

.mobile-buttons {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.mobile-buttons button {
  margin-top: 10px;
  padding: 5px;
  width: 33%;
}

.left-panel {
  display: flex;
  flex-direction: column;
  margin-right: 10px;
  width: 20%;
  position: fixed;
}

.left-panel > button {
  margin-bottom: 5px;
  padding: 7px;
  border-radius: 5px;
  border: none;
}

.left-panel > button:hover {
  background-color: dodgerblue;
  color: white;
  cursor: pointer;
}

.list {
  margin-top: 10px;
  margin-bottom: 10px;
}

.list-mobile {
  margin-top: 10px;
  margin-bottom: 10px;
  background: none;
  border: none;
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

.collection-name {
  margin: auto;
  font-size: 50px;
}

.info {
  background: none;
  border: none;
}

@media screen and (max-width: 800px) {
  .home {
    width: 100%;
    flex-direction: column;
    margin: auto;
    margin-top: 50px;
  }

  .left-panel {
    position: relative;
    width: 95%;
  }

  .collection {
    width: 90%;
    margin: auto;
    margin-top: 20px;
  }
}
</style>
