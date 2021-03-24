<script>
import api from '../../api/api'
import UserTable from './UserTable'
// import VPagination from "vue3-pagination";
import Pagination from 'v-pagination-3';

export default {

    components:
    {
        UserTable,
        Pagination
    },

    data()
    {
        return {
            users: [],
            usersPage : [],
            currentPage : 1,
            countOfUsers : 0,
            perPage : 5,
        }
    },

    mounted() 
    {
        this.getAllUsers()
        
        
    },

    methods:
    {
        getAllUsers()
        {
            api.user.GetAll()
            .then(response =>
            {
                this.users = response.data;
                this.countOfUsers = response.data.length;
                this.getUsersPage();
            })
        },
        userDelete(id)
        {
            api.user.Delete(id)
            .then(() =>
            {
                this.getAllUsers();
            })
        },

        getUsersPage()
        {
            let users = [];
            let startPosition = 0
            if(this.currentPage !== 1)
            {
                startPosition = (this.currentPage - 1)*this.perPage;
            }
            for(let i = startPosition; i < this.countOfUsers; i++)
            {
                if(i === startPosition + this.perPage)
                {
                    break;
                }
                users.push(this.users[i]);
            }
            this.usersPage = users;
        }
    }
}
</script>


<template>
    <router-link class="btn btn-dark btn-lg my-4" to="/">meetings</router-link>
    <div class="fw-bold fs-1">Users</div>
    <UserTable v-if="usersPage.length > 0" :users="usersPage" v-on:userDelete="userDelete"></UserTable>
    <div class="d-flex justify-content-center">
        <Pagination v-model="currentPage" :records="countOfUsers" :per-page="perPage" @paginate="getUsersPage"/>
    </div>
    
</template>