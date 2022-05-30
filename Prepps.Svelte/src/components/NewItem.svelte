<script lang="ts">
    import { createEventDispatcher } from 'svelte'
    import ItemModel from '../models/ItemModel'
    import Button from './Button.svelte';

    let name = ''
    let expiresAt = ''

    const dispatch = createEventDispatcher()

    const addItem = () => {
        let item: ItemModel = {
            name: name,
            expiresAt: expiresAt,
            id: '',
            isExpired: false
        }
        dispatch('addItem', item)
        name = ''
        expiresAt = ''
    }
</script>

<h3>Create new item</h3>
<form class="new-item-container" on:submit|preventDefault={addItem}>
    <input placeholder="Name" bind:value={name} required />
    <input placeholder="Expiring date" bind:value={expiresAt} type='date' required />
    <Button value='Add' type='submit' />
    <!-- <input value="Add" type='submit' class="add-button" /> -->
</form>

<style>
    .new-item-container {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
    }

    input {
        padding: 10px;
    }

    @media(max-width: 500px) {
        .new-item-container {
            flex-direction: column;
            justify-content: initial;
            align-items: initial;
        }
        input {
            width: 100%;
        }
    }
</style>