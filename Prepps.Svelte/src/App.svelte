<script lang="ts">
	import { SvelteToast } from '@zerodevx/svelte-toast' // https://github.com/zerodevx/svelte-toast
	import Item from './components/Item.svelte'
	import Filter from './components/Filter.svelte'
	import ItemModel, { ItemCreateModel } from './models/ItemModel'
	import NewItem from './components/NewItem.svelte';
	import { onMount } from 'svelte';
	import * as ProductApi from './apis/products';
	import Subscribe from './components/Subscribe.svelte';
	import Notification from './services/Toast'

	let items: ItemModel[] = []

	let searchTerm = '';

	const filterItems = (searchTerm: string, items: ItemModel[]) => 
		searchTerm ? items.filter((i:ItemModel) => i.name.toLowerCase().includes(searchTerm.toLowerCase()) 
		|| i.expiresAt.includes(searchTerm.toLowerCase())) 
		: items

	let addItem = async (item: ItemCreateModel) => {
		await ProductApi.addProduct(item)

		items = await ProductApi.getProducts()

        Notification.success(`${item.name} added to list`)
	}

	let deleteItem = async (itemId: string) => {
		await ProductApi.deleteProduct(itemId)

		items = await ProductApi.getProducts()
	}

	onMount(async () => {
		let products = await ProductApi.getProducts()

		items = products || []
	})
</script>

<svelte:head>
	<title>Prepps App</title>
</svelte:head>

<main>
	<h1>Welcome to Prepps 🎒</h1>
	<h5>For preppers and alike</h5>
	<SvelteToast />
	<NewItem on:addItem={(item) => addItem(item.detail)} />
	<Subscribe />
	<h3>Items</h3>
	<Filter bind:searchTerm />
	{#await items}
	Loading...
	{:then items}
	{#each filterItems(searchTerm, items) as item (item.id)}
	<Item deleteFunc={deleteItem} {item} />
	{:else}
	<div>Nothing to see here 😢</div>
	{/each}
	{/await}
</main>

<style>
	main {
		text-align: left;
		padding: 1em;
		max-width: 500px;
		margin: 0 auto;
		display: flex;
		flex-direction: column;
		justify-content: center;
	}
	h1, h5 {
		text-align: center;
		margin-top: 0;
	}
</style>