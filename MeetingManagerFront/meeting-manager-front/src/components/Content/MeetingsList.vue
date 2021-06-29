<script>
import api from '../../api/api'
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
            api.meeting.GetAll()
            .then(response =>
            {
                this.meetings = response.data;
            })
        },

        deleteMeeting(id)
        {
           api.meeting.Delete(id)
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
