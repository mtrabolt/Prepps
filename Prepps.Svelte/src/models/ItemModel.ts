export default class ItemModel {
    id: string = ''
    name: string = ''
    expiresAt: string = ''
    isExpired: boolean = false
}

export class ItemCreateModel {
    name: string
    expiresAt: string
}