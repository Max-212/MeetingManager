<script>
    export default {
        props:
        {
            user:
            {
                type: Object,
                default(){
                    return {}
                }
            }
        },
    }
</script>

<template>
    <div class="border dropdown-user__el d-flex justify-content-center align-items-center p-1">
        <h5 class="fw-bold">{{user.email}}</h5>        
    </div>
</template>

<style>
    .dropdown-user__el:hover
    {
        background-color: rgba(29, 28, 28, 0.158);
        cursor: pointer;
    }
</style>

