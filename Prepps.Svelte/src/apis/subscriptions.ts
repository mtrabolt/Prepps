import type SubscriptionModel from 'src/models/SubscriptionModel'
import API from '../services/Api'
import Notification from '../services/Toast'

export const getSubscriptions = () => {
    try {
        let subscriptions = API.get<SubscriptionModel[]>('subscriptions')

        return subscriptions || []
    } catch (error) {
        Notification.danger(`Error loading subscriptions: ${error.message}`)
        throw error
    }
}

export const addSubscription = async (subscriptionEmail: string) => {
    try {
        await API.post(`subscriptions`, subscriptionEmail)
    } catch (error) {
        Notification.danger(`Error saving email subscription: ${error.message}`)
        throw error
    }
}

export const removeSubscription = async (subscription: SubscriptionModel) => {
    try {
        await API.delete(`subscriptions`, subscription.id)
    } catch (error) {
        Notification.danger(`Error removing email subscription: ${error.message}`)
        throw error
    }
}