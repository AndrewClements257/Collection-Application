<template>
  <div id="modal-container">
    <span class="exit" @click="close">x</span>
    <h2>Add New Item</h2>
    <form class="newItemForm" @submit.prevent="createItem">
      <input
        type="text"
        id="name"
        placeholder="Item Name"
        v-model="newItem.name"
        required
      />
      <label for="image">Image:</label>
      <input
        type="file"
        id="image"
        @change="handleFileChange"
        accept="image/*"
        required
      />
      <button type="submit" class="pickup-button">Create Item</button>
      <div v-if="showStatus" :class="statusClass">{{ uploadStatus }}</div>
    </form>
  </div>
</template>

<script>
import ItemService from "../services/ItemService";

export default {
  name: "new-item-modal",
  data() {
    return {
      selectedFile: null,
      showStatus: false,
      uploadStatus: "",
      statusClass: "",
      newItem: {
        item_id: 0,
        collection_id: this.$store.state.currentCollection.collection_ID,
        name: "",
        url: "",
      },
    };
  },
  methods: {
    handleFileChange(event) {
      this.selectedFile = event.target.files[0];
      this.newItem.url = this.selectedFile.name;
      this.showStatus = false; // Reset status when a new file is selected
    },
    async createItem() {
      try {
        // Upload the image to the local file system (you may need to customize this part)
        await this.uploadImage(this.selectedFile);

        this.newItem.collection_id =
          this.$store.state.currentCollection.collection_ID;
        await ItemService.newItem(this.newItem);
        // Reset the selected file
        this.selectedFile = null;
        this.$emit("close");
      } catch (error) {
        console.error(error);
        this.showStatus = true;
        this.uploadStatus = "Failed to add new item";
        this.statusClass = "error";
      }
    },

    uploadImage() {
      const formData = new FormData();
      formData.append("image", this.selectedFile);

      ItemService.uploadImage(formData)
        .then((response) => {
          // Handle successful response
          this.showStatus = true;
          this.uploadStatus = "Image uploaded successfully";
          this.statusClass = "success";
          console.log(response.data); // Access the response data
        })
        .catch((error) => {
          // Handle error
          this.showStatus = true;
          this.uploadStatus = "Failed to upload image";
          this.statusClass = "error";
          console.error(error); // Log the error for debugging

          // Reset the selected file
          this.selectedFile = null;
        });
    },
    close() {
      this.selectedFile = null;
      this.newItem.name = "";
      this.$emit("close");
      event.preventDefault(); // Prevent form submission
    },
  },
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

h2 {
  font-size: 32px;
  margin-bottom: 20px;
}

input,
button {
  width: 75%;
}

label {
  margin-top: 0px;
  font-size: 24px;
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

input {
  height: 2vw;
  margin-bottom: 10px;
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
  margin-bottom: 15px;
}

.pickup-button:hover {
  background-color: dodgerblue;
  color: white;
}

.newItemForm {
  display: flex;
  flex-direction: column;
  align-items: center;
}

@media screen and (max-width: 1300px) {
  input {
    height: 4vw;
  }
}

@media screen and (max-width: 768px) {
  input {
    height: 5vw;
  }
}

@media screen and (max-width: 450px) {
  input {
    height: 9vw;
  }
}
</style>