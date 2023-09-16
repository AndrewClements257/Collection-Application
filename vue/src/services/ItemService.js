import axios from "axios";

const http = axios.create({
    baseURL: "https://localhost:44315"
});

export default {
    getItemsByCollectionID(collectionID) {
        return http.get(`api/Item/Collection/${collectionID}`);
    },
    getItemByItemID(itemID) {
        return http.get(`api/Item/${itemID}`)
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
    },
    deleteItem(selectedItemID) {
        return http.delete(`api/Item`, {
            params: {
                selectedItemID: selectedItemID
            }
        });
    },
    deleteImage(imageName) {
        return http.delete(`api/Item/DeleteImage?imageName=${imageName}`);
    }
}