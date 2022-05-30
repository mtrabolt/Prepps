export let formatDate = (date: Date) => {
    let convertSingleDigit = (value: number) => value > 9 ? value : '0' + value

    return date.getFullYear() + '-' 
        + convertSingleDigit(date.getMonth()) + '-' 
        + convertSingleDigit(date.getDate())
}