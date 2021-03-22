<script>
import MeetingForm from '../Forms/MeetingForm'
import axios from 'axios'

export default {
    data(){
        return{
            errors: {},
            meeting: null
        }
    },
    components:
    {
        MeetingForm
    },
    mounted() 
    {
        this.getMeeting();
    },
    methods:
    {
        getMeeting()
        {
            axios.get(`https://localhost:44343/api/meetings/${this.$route.params.id}`)
            .then(response =>
            {
                this.meeting = response.data
            })
        },

        updateMeeting(meetingData)
        {
            axios.put(`https://localhost:44343/api/meetings/`,
            {
                Id : Number.parseInt(this.$route.params.id),
                From: meetingData.from,
                Till: meetingData.till,
                Description: meetingData.description,
                Participants: meetingData.participants 
            }
            )
            .then(() =>
            {
                this.$router.replace('/')   
            })
            .catch(error =>
            {
                this.errors= error.response.data.errors;
            })
        }
    }
}

</script>

<template>
    <MeetingForm v-if="meeting" :meeting="meeting" :errors="errors" v-on:saveMeeting="updateMeeting"></MeetingForm>
</template>