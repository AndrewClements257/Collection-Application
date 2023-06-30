import axios from "axios";

const http = axios.create({
    baseURL: "https://localhost:44315"
});

export default {
    getItemsByCollectionID(collectionID) {
        return http.get(`api/Item/${collectionID}`);
    },
    uploadImage(formData) {
        return axios.post(`api/Item/upload`, formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        });
    },
    newItem(dto) {
        return http.post(`api/Item`, dto);
    }
}