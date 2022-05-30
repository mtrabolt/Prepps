export interface ProductWriteModel {
    name: string;
    expiresAt: string;
}

export interface Product {
    id: string;
    name: string;
    expiresAt: string;
    isExpired: boolean;
}