<script lang="ts">
    import * as SubscriptionApi from "../apis/subscriptions";
    import Button from "./Button.svelte";
    import Notification from '../services/Toast'
    import type SubscriptionModel from "src/models/SubscriptionModel"

    let subscriptions = SubscriptionApi.getSubscriptions()

    let subscriptionEmail = ''

    let loadNewSubscription = false
    let loadDeleteSubscription = false

    const addSubscription = async () => {
        loadNewSubscription = true

        await SubscriptionApi.addSubscription(subscriptionEmail)
        subscriptions = SubscriptionApi.getSubscriptions()

        Notification.success(`${subscriptionEmail} successfully added`)

        subscriptionEmail = ''
        loadNewSubscription = false
    }

    const removeSubscription = (subscription: SubscriptionModel) => {
        loadDeleteSubscription = true

        SubscriptionApi.removeSubscription(subscription)
            .then(async () => {
                subscriptions = await SubscriptionApi.getSubscriptions()
                loadDeleteSubscription = false
            })
    }
</script>

<h3>Get notified when items expire</h3>
<form on:submit|preventDefault={addSubscription}>
    <input bind:value={subscriptionEmail} placeholder="Subscribe" type='email' required />
    <Button value='Subscribe' type='submit' bind:loading={loadNewSubscription} />
</form>

{#await subscriptions}
Loading...
{:then subscriptions}
{#each subscriptions as subscription, i }
<div class={i < subscriptions.length - 1 ? 'border-bottom' : ''}>
    <p>{subscription.email}</p>
    <Button on:click={() => removeSubscription(subscription)} value='Delete' bind:loading={loadDeleteSubscription} /> 
</div>
{:else}
<p>No one gets notified here 👀</p>
{/each}
{/await}

<style>
    form {
        display: flex;
        flex-direction: column;
    }

    input {
        padding: 10px;
    }

    div {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        padding: 8px 0 0 0;
    }

    .border-bottom {
        border-bottom: solid lightgrey 2px;
    }
</style>