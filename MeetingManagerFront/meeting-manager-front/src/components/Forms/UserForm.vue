<script>

export default {
    data()
    {
        return{
            email: this.user.email,
            firstName: this.user.firstName,
            lastName: this.user.lastName,
        }
    },
    props:
    {
        user:
        {
            type: Object,
            default(){
                return {}
            } 
        },
        errorMessage:{
            type: String,
            default(){
                return ''
            }
        }
    },
    methods:{
        saveUser()
        {
            this.$emit('saveUser', 
            {
                email : this.email,
                firstName : this.firstName,
                lastName : this.lastName
            });
        },
    }
}
</script>


<template>
    <div class= 'd-flex justify-content-center align-items-center'>
        <div class="shadow-lg card card-container w-100 user-form border-0 p-4">

            <div class="fw-bold fs-1 mb-4">User</div>

            <div class="form-group mb-4 px-3">
                <input type="input" class="form-control" placeholder='Email' v-model="email">
            </div>

            <div class="form-group mb-4 px-3">
                <input type="input" class="form-control" placeholder='First Name' v-model="firstName">
            </div>

            <div class="form-group mb-4 px-3">
                <input type="input" class="form-control" placeholder='Last Name' v-model="lastName">
            </div>

            <div class="form-outline mb-4 p-3 w-100">
                <input class="btn btn-success w-100" type='submit' value='Save'
                @click="saveUser"/>
            </div>

            <div v-if="errorMessage" className="form-group">
                <div className="alert alert-danger" role="alert">
                    {{errorMessage}}
                </div>
            </div>

        </div>
    </div>
</template>

<style>
    .user-form
    {
        max-width: 700px;
    }
</style>
