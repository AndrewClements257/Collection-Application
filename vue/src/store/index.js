import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import CollectionService from '../services/CollectionService';
import ItemService from '../services/ItemService';

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));

if (currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    collections: [],
    items: [],
    currentCollection: {
      name: '',
      user_id: '',
      collection_ID: '1'
    },
    currentItem: {
      collection_ID: '',
      item_ID: '',
      name: '',
      url: '',
      description: ''
    }
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user', JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
    async GET_COLLECTIONS(state) {
      try {
        const response = await CollectionService.getCollections();
        state.collections = response.data;
      } catch (error) {
        console.log(error);
      }
    },
    SET_CURRENT_COLLECTION(state, collection) {
      state.currentCollection = collection;
    },
    SET_CURRENT_ITEM(state,item) {
      state.currentItem = item;
    },
    async GET_ITEMS_BY_COLLECTION_ID(state, collectionID) {
      const response = await ItemService.getItemsByCollectionID(collectionID);
      state.items = response.data;
    }
  }
})
