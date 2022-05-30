<script lang="ts">
    import ItemModel from "../models/ItemModel";
    import Button from "./Button.svelte";

    export let item: ItemModel
    export let deleteFunc

    // Create store for items
    let loadingDelete = false

    let editMode = false
</script>

<div class="item-container">
    {#if editMode}
    <form on:submit|preventDefault={(e) =>{console.log(e)}}>
        <input bind:value={item.name} type='text' />
        <input bind:value={item.expiresAt} type='date' />
        <Button value="Save" on:click={() => {editMode = !editMode}} type='submit' />
        <Button value="Cancel" on:click={() => {editMode = !editMode}} />
    </form>
    {:else}
    <span class={item.isExpired ? 'expired status-bubble' : 'valid status-bubble'}></span>
    <span>{item.name}</span>
    <span>{item.expiresAt}</span>
    <Button value="Edit" on:click={() => {editMode = !editMode}} />
    <Button on:click={async () => {await deleteFunc(item.id)}} value="Delete" loading={loadingDelete} />
    {/if}
</div>

<style>
    span {
        min-width: 100px;
    }

    form {
        width: 100%;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: left;
    }

    form input {
        max-width: 120px;
    }

    .item-container {
        min-width: 200px;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: left;
        padding: 15px 15px 7px 15px;
    }

    .item-container:hover {
        background-color: rgb(196, 196, 196);
    }

    .status-bubble {
        border-radius: 100%;
        min-width: initial;
        width: 20px;
        height: 20px;
    }

    .expired {
        background-color: red;
    }

    .valid {
        background-color: green;
    }
</style>