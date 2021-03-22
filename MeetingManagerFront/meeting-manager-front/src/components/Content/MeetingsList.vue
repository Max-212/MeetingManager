<script>
import axios from "axios"
import MeetingView from './MeetingView.vue';

export default {
    components:
    {
        MeetingView
    },

    data()
    {
        return{
            meetings: []
        }
    },

    mounted() 
    {
        this.getAllMeetings();
    },

    methods:
    {
        getAllMeetings()
        {
            axios.get("https://localhost:44343/api/meetings/")
            .then(response =>
            {
                this.meetings = response.data;
            })
        },

        deleteMeeting(id)
        {
           axios.delete(`https://localhost:44343/api/meetings/${id}`)
           .then(() =>
           {
               this.getAllMeetings();
           }) 
        }
    }
}
</script>

<template>
    <router-link class="btn btn-dark btn-lg my-4" to="/users">users</router-link>
    <div class="fw-bold fs-1">Meetings</div>
    <div class="d-flex justify-content-around flex-wrap">
        <MeetingView v-for="meeting in meetings" :key="meeting.id" v-bind:meeting="meeting" v-on:deleteMeeting="deleteMeeting"></MeetingView>
    </div>
</template>

<style>
</style>
