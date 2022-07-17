import axios from 'axios'

const axiosApi = axios.create({
    baseURL: '/apis' //import.meta.env.API_ENDPOINT,
})

const apiRequest = <T>(method, url, request) => {
    const headers = {
        // authorization: "",
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
    }

    return axiosApi({
        method,
        url,
        data: request,
        headers,
    }).then(res => {
        return Promise.resolve<T>(res.data)
    }).catch(error => {
        return Promise.reject(error)
    })
}

const get = <T>(url) => apiRequest<T>('get', url, {})

const del = (url, request) => apiRequest('delete', url, request)

const post = (url, request) => apiRequest('post', url, request)

const put = (url, request) => apiRequest('put', url, request)

const API = {
    get,
    delete: del,
    post, 
    put,
}

export default API