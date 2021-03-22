<script>
export default {
    
}
</script>

<template>
    <div class="d-flex justify-content-around w-100 flex-wrap menu">
         <router-link class="btn btn-success btn-lg w-100 mb-4" to="/createMeeting">Create Meeting</router-link>
         <router-link class="btn btn-primary btn-lg w-100 mb-4 ml-4" to="/createUser">Create User</router-link>
    </div>
</template>

<style>
    .menu a{
        max-width: 300px;
    }
</style>
