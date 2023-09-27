<template>
    <div id="modal-container">
        <span class="exit" @click="close">x</span>
        <h2>New Collection</h2>
    <form  class="newCollectionForm" action="" v-on:submit.prevent="submitNewCollection()">
        <ul>
            <li>
                <input type="text" placeholder="Collection Name" v-model="newCollection.name" required>
            </li> 
        </ul>
      <button class="pickup-button">Create Collection</button>
    </form>
    </div>
</template>

<script>
import CollectionService from '../services/CollectionService';

export default {
    methods: {
        async submitNewCollection(){
            await CollectionService.newCollection(this.newCollection);
            this.$emit("close");
            this.$store.commit("GET_COLLECTIONS"); // Dispatch the GET_COLLECTIONS mutation
            this.newCollection.name = "";
        },
        close() {
      this.$emit("close");
    }
    },
    name: "collection-modal",
    data() {
        return {
            newCollection: {
                "user_id": this.$store.state.user.userId,
                "name": ""
            }
        }
    }
}
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

li > input {
  height: 2vw;
}

h2 {
  font-size: 32px;
  margin-bottom: 10px;
}

input, button {
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
  color:red;
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
  margin-bottom:15px;
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