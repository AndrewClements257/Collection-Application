<template>
    <div id="modal-container">
    <form  class="newCollectionForm" action="" v-on:submit.prevent="submitNewCollection()">
        <ul>
            <li>
                <input type="text" placeholder="Collection Name" v-model="newCollection.name" required>
            </li> 
        </ul>
      <button class="pickup-button">Create Collection</button>
    </form>
    <button class="pickup-button" @click="close">Nevermind</button>
    <br/>
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