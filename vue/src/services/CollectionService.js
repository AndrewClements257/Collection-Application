import axios from "axios";

const http = axios.create({
    baseURL: "https://localhost:44315"
});

export default {
    getCollections() {
        return http.get(`api/Collection`);
    },
    newCollection(dto) {
        return http.post(`api/Collection`,dto);
    },
    deleteCollection(collection_id) {
        return http.delete(`api/Collection`, {
            params: {
                collection_id: collection_id
            }
        });
    }
}