<script>
    import axios from 'axios';
    import UserForm from '../Forms/UserForm';

    export default {
        data(){
            return{
                errorMessage: '',
                user: null
            }
        },
        components:
        {
            UserForm
        },

        mounted() 
        {
           this.getUser();
        },

        methods:
        {
            updateUser(userData)
            {
                console.log(userData);
                axios.put(`https://localhost:44343/api/users/`,
                {
                    Id : Number.parseInt(this.$route.params.id),
                    Email : userData.email,
                    FirstName : userData.firstName,
                    LastName : userData.lastName 
                }
                )
                .then(() =>
                {
                    this.$router.replace('/users')   
                })
                .catch(error =>
                {
                    this.errorMessage = error.response.data.errors.Email[0];
                })
            },

            getUser()
            {
                axios.get(`https://localhost:44343/api/users/${this.$route.params.id}`)
                .then((response) =>
                {
                    this.user = response.data; 
                })
            }
        }
    }
</script>

<template>
    <UserForm v-if="user" v-bind:user="user" v-on:saveUser="updateUser" :errorMessage="errorMessage"></UserForm>
</template>