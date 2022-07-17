import type { ItemCreateModel } from "src/models/ItemModel"
import type ItemModel from "src/models/ItemModel"
import API from "../services/Api"
import Notification from "../services/Toast"

export const getProducts = async () => {
    try {
        const response = await API.get<ItemModel[]>('products')

        return response
    } catch (error) {
        Notification.danger(`Error loading items: ${error.message}`)
        throw error
    }
}

export const addProduct = async (item: ItemCreateModel) => {
    try {
        const response = await API.post('products', item)

        return response
    } catch (error) {
        Notification.danger(`Error adding item:<br/> ${error.response.data}`)
        throw error
    }
}

export const deleteProduct = async (itemId: string) => {
    try {
        const response = await API.delete(`products/${itemId}`, null)

        return response
    } catch (error) {
        Notification.danger(`Error deleting item: ${error.message}`)
        throw error
    }
}