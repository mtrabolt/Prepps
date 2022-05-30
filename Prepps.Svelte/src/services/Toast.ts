import {toast} from '@zerodevx/svelte-toast'

const success = (message: string) => {
    toast.push(message, {
        theme: {
            '--toastBackground': '#48BB78',
            '--toastBarBackground': '#2F855A'
        }
    })
}

const danger = (message: string) => {
    toast.push(message, {
        theme: {
            '--toastBackground': '#F56565',
            '--toastBarBackground': '#C53030'
        }
    })
}

const Notification = {
    success,
    danger,
}

export default Notification