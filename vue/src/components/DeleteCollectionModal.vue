<template>
    <div id="modal-container">
    <form  class="deleteCollectionForm" v-on:submit.prevent="deleteCollection()">
        <select v-model="selectedCollectionID">
        <option v-for="collection in $store.state.collections" :key="collection.collection_ID" :value="collection.collection_ID">
        {{collection.name}}
        </option>
        </select>
      <button class="pickup-button">Delete Collection</button>
    </form>
    <button class="pickup-button" @click="close">Nevermind</button>
    <br/>
    </div>
</template>

<script>
import CollectionService from '../services/CollectionService';
import ItemService from '../services/ItemService';

export default {
    data() {
        return {
            fileName: "",
            selectedCollectionID: null,
            selectedCollection: {
                "collection_ID": "",
                "name": "",
                "user_ID": ""
            }
        }
    },
    methods: {
        async deleteCollection() {
    try {
        // Filter items in the selected collection from the store's state
        const items = this.$store.state.items.filter(
            item => item.collection_ID === this.selectedCollectionID
        );

        // Loop through each item in the collection
        for (const item of items) {
            // Delete each item
            await ItemService.deleteItem(item.item_ID);
            // Delete each item's image
            await ItemService.deleteImage(item.url);
        }

        // After all items and images are deleted, delete the collection itself
        await CollectionService.deleteCollection(this.selectedCollectionID);

        this.$store.commit('GET_COLLECTIONS'); 

        // Emit close event
        this.$emit("close");

    } catch (error) {
        console.error(error);
    }
},
        close() { 
      this.$emit("close");
      event.preventDefault(); // Prevent form submission
    }
    },
    name: "delete-collection-modal",
}
</script>

<style>
#modal-container {
    text-align: center;
    width: 100%;
    color: white;
    background: rgba(255, 255, 255, 0.1); 
  border-radius: 10px; 
  backdrop-filter: blur(3px); 
  border: 1px solid rgba(255, 255, 255, 0.2);
  max-width: 350px;
  width: 90%;
  padding: 20px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);

  margin: 25px;
}

.title {
    font-size: 25px;
    color: white;
}

li {
    padding: 5px;
}

li > input {
    height: 2vw;
}


.pickup-button {
  padding: 12px 16px;
  border: none;
  border-radius: 4px;
  background-color: #ffc700;
  color: black;
  font-weight: normal;
  font-size: 15px;
  cursor: pointer;
  float: center;
}
.parkingSpots{
    display: inline;
}
.deleteItemForm{
    width: 100%;
}

@media screen and (max-width: 1300px) {
  li > input {
    height: 4vw;
}
}

@media screen and (max-width: 768px) {
  li > input {
    height: 5vw;
}
}


@media screen and (max-width: 450px) {
  li > input {
    height: 9vw;
}
}

</style>