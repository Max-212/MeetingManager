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
        this.getUsersPage()
    },

    methods:
    {
        userDelete(id)
        {
            api.user.Delete(id)
            .then(() =>
            {
                this.getUsersPage();
            })
        },

        getUsersPage()
        {
            api.user.GetPage(this.currentPage, this.perPage)
            .then(response =>
            {
                this.usersPage = response.data.data;
                this.countOfUsers = response.data.totalCount;
            })
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