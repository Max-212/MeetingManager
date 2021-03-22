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

    methods:{
        deleteUser()
        {
            this.$emit('userDelete', this.user.id);
        }
    }
}
</script>

<template>
    <div class="rounded d-flex justify-content-center align-items-center flex-wrap shadow mb-4 p-3 w-100 user-view">
        <div>{{user.firstName}}</div>
        <div class="mx-2 px-2">{{user.lastName}}</div>
        <div>{{user.email}}</div>
        <div class="d-flex flex-fill justify-content-end align-items-center">
            <router-link class="btn btn-secondary btn" v-bind:to="'/updateUser/' + user.id" >
                <i class="fa fa-wrench" aria-hidden="true"></i>
            </router-link>
            <button class="btn btn-danger mx-3 btn" @click="deleteUser">
                <i class="fa fa-trash" aria-hidden="true"></i>
            </button>
        </div>
    </div>
    
</template>

<style>
    .user-view{
        max-width: 700px;
        font-size: 18px;
        font-weight: 800;
    }

    .user-view div:nth-child(2)
    {
        box-shadow: 2px 0 0 rgba(0, 0, 0, 0.562)
    }
</style>