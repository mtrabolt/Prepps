<script lang="ts">
    export let placeholder = 'Search'
    export let searchTerm = ''
</script>

<input placeholder={placeholder} bind:value={searchTerm} />

<style>
    input {
        padding: 10px;
        width: 100%;
    }
</style>