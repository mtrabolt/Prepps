<script lang="ts">
    import {createEventDispatcher} from 'svelte'

    export let value = ''
    export let type = 'button'
    export let loading = false

    const dispatch = createEventDispatcher()
</script>

<button on:click={() =>(dispatch('click', {value})) } type={type} disabled={loading}>
    {value} {#if loading} <div class="lds-hourglass"></div> {/if}
</button>

<style>
    button {
        background-color: rgb(62, 156, 214);
        border-radius: 5px;
        color: white;
        cursor: pointer;
        min-width: 75px;
        padding: 10px;
    }

    button:disabled {
        background-color: rgb(192, 206, 219);
        cursor: not-allowed;
    }

    .lds-hourglass {
        display: inline-block;
        position: relative;
        width: 20px;
        height: 20px;
    }
    .lds-hourglass:after {
        content: " ";
        display: block;
        border-radius: 50%;
        width: 0;
        height: 0;
        /* margin: 8px; */
        margin: 4px;
        box-sizing: border-box;
        border: 10px solid #fff;
        border-color: #fff transparent #fff transparent;
        animation: lds-hourglass 1.2s infinite;
    }
    @keyframes lds-hourglass {
        0% {
            transform: rotate(0);
            animation-timing-function: cubic-bezier(0.55, 0.055, 0.675, 0.19);
        }
        50% {
            transform: rotate(900deg);
            animation-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
        }
        100% {
            transform: rotate(1800deg);
        }
    }
</style>