<template>
    <div id="modal-container">
      <h1>Add New Item</h1>
      <form @submit.prevent="createItem">
        <label for="name" >Name:</label>
        <input type="text" id="name" v-model="newItem.name" required/>
        <br>
        <label for="image">Image:</label>
        <input type="file" id="image" @change="handleFileChange" accept="image/*" required>
        <br>
        <button type="submit">Create Item</button>
        <div v-if="showStatus" :class="statusClass">{{ uploadStatus }}</div>
        <button class="pickup-button" @click="close">Nevermind</button>
      </form>
    </div>
</template>

<script>
import ItemService from '../services/ItemService';

export default {
  name: "new-item-modal",
  data() {
    return {
      selectedFile: null,
      showStatus: false,
      uploadStatus: '',
      statusClass: '',
      newItem: {
        "item_id": 0,
        "collection_id": this.$store.state.currentCollection.collection_ID,
        "name": "",
        "url": ""
      }
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

        this.newItem.collection_id = this.$store.state.currentCollection.collection_ID;
        await ItemService.newItem(this.newItem);
        // Reset the selected file
        this.selectedFile = null;
        this.$emit("close");
      } catch (error) {
        console.error(error);
        this.showStatus = true;
        this.uploadStatus = 'Failed to add new item';
        this.statusClass = 'error';
      }
    },
    
    uploadImage() {
  const formData = new FormData();
  formData.append('image', this.selectedFile);

  ItemService.uploadImage(formData)
    .then(response => {
      // Handle successful response
      this.showStatus = true;
      this.uploadStatus = 'Image uploaded successfully';
      this.statusClass = 'success';
      console.log(response.data);  // Access the response data
    })
    .catch(error => {
      // Handle error
      this.showStatus = true;
      this.uploadStatus = 'Failed to upload image';
      this.statusClass = 'error';
      console.error(error);  // Log the error for debugging

      // Reset the selected file
      this.selectedFile = null;
    });
},
    close() {
      this.selectedFile = null;
      this.newItem.name = "";
      this.$emit("close");
      event.preventDefault(); // Prevent form submission
    }
  }
};
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
.newCollectionForm{
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