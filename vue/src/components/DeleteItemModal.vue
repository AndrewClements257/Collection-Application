<template>
  <div id="modal-container">
    <span class="exit" @click="close">x</span>
    <h2>Delete Item</h2>
    <form class="deleteItemForm" v-on:submit.prevent="deleteItem()">
      <select v-model="selectedItemID" required>
        <option
          v-for="item in $store.state.items"
          :key="item.item_ID"
          :value="item.item_ID"
        >
          {{ item.name }}
        </option>
      </select>
      <button class="pickup-button">Delete Item</button>
    </form>
  </div>
</template>

<script>
import ItemService from "../services/ItemService";

export default {
  data() {
    return {
      fileName: "",
      selectedItemID: null,
      selectedItem: {
        item_ID: "",
        collection_ID: "",
        name: "",
        url: "",
      },
    };
  },
  methods: {
    async deleteItem() {
      // Call the ItemService.GetItemByID method to fetch the item details
      const itemDetails = await ItemService.getItemByItemID(
        this.selectedItemID
      );

      // Update the selectedItem object with the fetched details
      this.selectedItem = {
        item_ID: itemDetails.data.item_ID,
        collection_ID: itemDetails.data.collection_ID,
        name: itemDetails.data.name,
        url: itemDetails.data.url,
      };
      this.fileName = this.selectedItem.url;

      try {
        // Upload the image to the local file system (you may need to customize this part)
        await ItemService.deleteItem(this.selectedItemID);
        await ItemService.deleteImage(this.fileName);
        // Reset the selected item
        this.selectedItem = {
          item_ID: "",
          collection_ID: "",
          name: "",
          url: "",
        };
        this.$emit("close");
      } catch (error) {
        console.error(error);
      }
    },
    close() {
      this.$emit("close");
      event.preventDefault(); // Prevent form submission
    },
  },
  name: "delete-item-modal",
};
</script>

<style scoped>
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

select {
  height: 2vw;
}

h2 {
  font-size: 32px;
  margin-bottom: 20px;
}

select,
button {
  width: 75%;
}

.exit {
  position: absolute;
  top: 0;
  right: 0;
  padding-right: 18px;
  font-size: 25px;
  color: #c0c0c0;
  align-content: center;
  cursor: pointer;
}

.exit:hover {
  color: red;
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
  margin-top: 10px;
  margin-bottom: 15px;
}

.pickup-button:hover {
  background-color: dodgerblue;
  color: white;
}

.deleteItemForm {
  display: flex;
  flex-direction: column;
  align-items: center;
}

@media screen and (max-width: 1300px) {
  select {
    height: 4vw;
  }
}

@media screen and (max-width: 768px) {
  select {
    height: 5vw;
  }
}

@media screen and (max-width: 450px) {
  select {
    height: 9vw;
  }
}
</style>