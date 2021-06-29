<script>
    import api from '../../api/api'
    import UserForm from '../Forms/UserForm';

    export default {
        data(){
            return{
                errorMessage: ''
            }
        },

        components:
        {
            UserForm
        },

        methods:
        {
            createUser(user)
            {
                api.user.Create(
                {
                    Email : user.email,
                    FirstName : user.firstName,
                    LastName : user.lastName 
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
        }
    }
</script>

<template>
    <UserForm  v-bind:user="{}" v-bind:errorMessage="errorMessage"  v-on:saveUser="createUser"></UserForm>
</template>
