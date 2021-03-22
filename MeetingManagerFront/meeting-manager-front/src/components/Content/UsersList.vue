<script>
import axios from 'axios';
import UserView from './UserView';

export default {
    components:
    {
        UserView
    },

    data()
    {
        return{
            users: []
        }
    },

    mounted() 
    {
        this.getAllUsers();
    },

    methods:
    {
        getAllUsers()
        {
            axios.get("https://localhost:44343/api/users/")
            .then(response =>
            {
                this.users = response.data;
            })
        },
        userDelete(id)
        {
            axios.delete(`https://localhost:44343/api/users/${id}`)
            .then(() =>
            {
                this.getAllUsers();
            })
        }
    }
}
</script>


<template>
    <router-link class="btn btn-dark btn-lg my-4" to="/">meetings</router-link>
    <div class="fw-bold fs-1">Users</div>
    <div class="d-flex flex-column align-items-center">
        <UserView v-for="user in users" :key="user.id" v-bind:user="user" v-on:userDelete="userDelete"></UserView>
    </div>
</template>