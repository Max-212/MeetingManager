<script>
export default {

    mounted()
    {
        this.rows.sort((a,b) =>
            a['firstName'] > b['firstName'] ? 1: -1 )
    },

    data()
    {
        return {
            rows : this.users,
            columns: ['email', 'firstName', 'lastName'],
            sort: {field: 'firstName', dir: 'asc'}
        }
    },

    watch:
    {
        users()
        {
            this.rows = this.users;
            this.sortUsers(this.sort.field);
            this.sortUsers(this.sort.field);
        }
    },
    
    props:
    {
        users:
        {
            type: Array,
            default(){
                return []
            }
        }
    },

    methods:
    {
        deleteUser(id)
        {
            this.$emit('userDelete', id);
        },

        sortUsers(field)
        {
            if(this.sort.field === field && this.sort.dir === 'asc')
            {
                this.rows.sort((a,b) =>
                    a[field] < b[field] ? 1: -1 )
                this.sort.dir = 'desc'
            }
            else
            {
                this.rows.sort((a,b) =>
                    a[field] > b[field] ? 1: -1 )
                this.sort.dir = 'asc'
            }
            this.sort.field = field;
        }
    },


}
</script>

<template>
    <div class="d-flex justify-content-center w-100 table-responsive">
    <table class="table user-table mt-5">
        <thead>
            <tr>
                <th scope="col" 
                    :class="{'sortActive sortActive-asc': sort.field === 'firstName'}"
                    @click="sortUsers('firstName')">
                    FirstName
                    <i v-if="sort.field === 'firstName' && sort.dir === 'asc'" class="fas fa-sort-up"></i>
                    <i v-if="sort.field === 'firstName' && sort.dir === 'desc'" class="fas fa-sort-down"></i>
                </th>

                <th scope="col"
                    :class="{'sortActive': sort.field === 'lastName'}"
                    @click="sortUsers('lastName')">
                    LastName
                    <i v-if="sort.field === 'lastName' && sort.dir === 'asc'" class="fas fa-sort-up"></i>
                    <i v-if="sort.field === 'lastName' && sort.dir === 'desc'" class="fas fa-sort-down"></i>
                </th>

                <th scope="col"
                    :class="{'sortActive': sort.field === 'email'}"
                    @click="sortUsers('email')">
                    Email
                    <i v-if="sort.field === 'email' && sort.dir === 'asc'" class="fas fa-sort-up"></i>
                    <i v-if="sort.field === 'email' && sort.dir === 'desc'" class="fas fa-sort-down"></i>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="user in rows" :key="user.id" >
                <td>{{user.firstName}}</td>
                <td>{{user.lastName}}</td>
                <td>{{user.email}}</td>
                <td></td>
                <td>
                    <div>
                        <router-link class="btn btn-secondary btn" v-bind:to="'/updateUser/' + user.id" >
                            <i class="fa fa-wrench" aria-hidden="true"></i>
                        </router-link>
                        <button class="btn btn-danger mx-3 btn" @click="deleteUser(user.id)">
                            <i class="fa fa-trash" aria-hidden="true"></i>
                        </button>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    </div>
</template>

<style>
    .user-table{
        max-width: 1000px;
        font-size: 18px;
        font-weight: 800;
    }

    .user-table__column:hover
    {
        cursor: pointer;
        background: rgba(175, 175, 175, 0.349);
    }

    .sortActive
    {
        color: rgb(133, 11, 11);
    }
</style>