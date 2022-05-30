import useSWR from 'swr'
import {Product} from "../Models/Product";

const fetcher = (url: string) => fetch(url).then((res) => res.json())

interface ProductsResult {
    products: Product[],
    isLoading: boolean,
    isError: any,
}

export function useProducts(): ProductsResult {
    const url = `api/Products`;
    const {data, error} = useSWR<Product[], any>(url, fetcher)

    return {
        products: data ?? [],
        isLoading: !error && !data,
        isError: error
    }
}