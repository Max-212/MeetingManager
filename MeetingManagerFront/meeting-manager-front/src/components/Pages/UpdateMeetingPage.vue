<script>
import MeetingForm from '../Forms/MeetingForm'
import api from '../../api/api'

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
            api.meeting.GetOne(this.$route.params.id)
            .then(response =>
            {
                this.meeting = response.data
            })
        },

        updateMeeting(meetingData)
        {
            api.meeting.Update(
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